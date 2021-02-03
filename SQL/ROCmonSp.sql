IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.fGetIndex') AND type='IF')
DROP FUNCTION dbo.fGetIndex
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fGetIndex]
(
)
RETURNS TABLE
/* WITH ENCRYPTION */  
AS
RETURN 
(
/* retrieve all index definition of the current database */

/* WARNING ! 
   THIS IS Rintagi supplied function, any local change would be overwritten when upgrade DEV package is applied
 */  
SELECT
TableName = t.name, 
IndexName = i.name,
Cluster = CASE WHEN is_primary_key=1 THEN 'CLUSTERED' 
			WHEN is_primary_key=0 THEN 'NONCLUSTERED'
			END ,
[Unique] = CASE WHEN is_unique=1 THEN 'UNIQUE' 
			WHEN is_unique=0 THEN 'NONCLUSTERED'
			END,
[Create] = 'CREATE' 
+
CASE WHEN is_unique=0 THEN '' 
ELSE ' UNIQUE'
END   
+ 
CASE WHEN is_primary_key=1 THEN ' CLUSTERED' 
WHEN is_primary_key=0 THEN ' NONCLUSTERED'
END  

+ ' INDEX ' +
QUOTENAME(i.name) + ' ON ' +
QUOTENAME(t.name) + ' ( '  + 
STUFF(REPLACE(REPLACE((
        SELECT QUOTENAME(c.name) + CASE WHEN ic.is_descending_key = 1 THEN ' DESC' ELSE '' END AS [data()]
        FROM sys.index_columns AS ic
        INNER JOIN sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 0
        ORDER BY ic.key_ordinal
        FOR XML PATH
    ), '<row>', ', '), '</row>', ''), 1, 2, '') + ' ) '  -- keycols
+ COALESCE(' INCLUDE ( ' +
    STUFF(REPLACE(REPLACE((
        SELECT QUOTENAME(c.name) AS [data()]
        FROM sys.index_columns AS ic
        INNER JOIN sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 1
        ORDER BY ic.index_column_id
        FOR XML PATH
    ), '<row>', ', '), '</row>', ''), 1, 2, '') + ' ) ',    -- included cols
    '')
+ ISNULL('WHERE ' + i.filter_definition,''),
[Drop] =
CASE WHEN is_primary_key = 1 THEN
	'ALTER TABLE ' + QUOTENAME(t.name) + ' DROP CONSTRAINT ' + QUOTENAME(i.name)
ELSE 
	'DROP INDEX ' + QUOTENAME(i.name) + ' ON ' + QUOTENAME(t.name)
END 
,
[Rebuild] = 'ALTER INDEX ' + QUOTENAME(i.name)  + ' ON ' +QUOTENAME(t.name) + ' REBUILD '
FROM sys.tables AS t
INNER JOIN sys.indexes AS i ON t.object_id = i.object_id
--LEFT JOIN sys.dm_db_index_usage_stats AS u ON i.object_id = u.object_id AND i.index_id = u.index_id
WHERE t.is_ms_shipped = 0
AND i.type <> 0
)
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MkStoredProcedure') AND type='P')
EXEC('CREATE PROCEDURE dbo.MkStoredProcedure AS SELECT 1')
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
ALTER PROCEDURE [dbo].[MkStoredProcedure]
  @spString1	nvarchar(max)
, @spString2	nvarchar(max) = ''
, @spString3	nvarchar(max) = ''
, @spString4	nvarchar(max) = ''
, @spString5	nvarchar(max) = ''
, @spString6	nvarchar(max) = ''
, @spString7	nvarchar(max) = ''
, @spString8	nvarchar(max) = ''
, @spString9	nvarchar(max) = ''
/* WITH ENCRYPTION */
AS
/* WARNING ! 
   THIS IS Rintagi supplied SP, any local change would be overwritten when upgrade DEV package is applied
 */
-- Although PRINT will only print a maximum of max characters, EXEC will execute more than max characters if concatenated as follow.
EXEC ('SET QUOTED_IDENTIFIER ON SET ANSI_NULLS ON')
EXEC (@spString1 + @spString2 + @spString3 + @spString4 + @spString5 + @spString6 + @spString7 + @spString8 + @spString9)
EXEC ('SET QUOTED_IDENTIFIER OFF')
RETURN 0
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.WrGetIndex') AND type='P')
EXEC('CREATE PROCEDURE dbo.WrGetIndex AS SELECT 1')
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
ALTER PROCEDURE [dbo].[WrGetIndex]
@TableName	varchar(256)=NULL
,@Cluster	varchar(20)=NULL
,@Unique	varchar(20)=NULL	
/* WITH ENCRYPTION */
AS
/* retrieve all index definition */

/* WARNING ! 
   THIS IS Rintagi supplied SP, any local change would be overwritten when upgrade DEV package is applied
 */

SET NOCOUNT ON

SELECT
*
FROM
dbo.fGetIndex()
WHERE
(TableName = @TableName OR @TableName IS NULL)
AND
(Cluster = @Cluster OR @Cluster IS NULL)
AND
([Unique] = @Unique OR @Unique IS NULL)
ORDER BY
QUOTENAME(TableName), Cluster, QUOTENAME(IndexName)

RETURN 0
GO
SET QUOTED_IDENTIFIER OFF
GO
