IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo._GetInCompanyId') AND type='P')
DROP PROCEDURE dbo._GetInCompanyId
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE dbo._GetInCompanyId
 @wClause	varchar(4000)
/* WITH ENCRYPTION */
AS
DECLARE	 @sClause		varchar(4000)
	,@fClause		varchar(4000)
	,@oClause		varchar(1000)
SET NOCOUNT ON
SELECT @sClause = 'SELECT a.CompanyId, a.CompanyName'
SELECT @fClause = 'FROM ROCmon.dbo.Company a'
SELECT @oClause = 'ORDER BY a.CompanyName'
EXEC (@sClause + ' ' + @fClause + ' ' + @wClause + ' ' + @oClause)
RETURN 0  
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo._GetS05Rptwiz46R') AND type='P')
DROP PROCEDURE dbo._GetS05Rptwiz46R
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE dbo._GetS05Rptwiz46R
 @wClause	nvarchar(4000)
,@CompanyId10	SmallInt
/* WITH ENCRYPTION */
AS
DECLARE	 @sClause		nvarchar(4000)
	,@fClause		nvarchar(4000)
	,@oClause		nvarchar(4000)
SET NOCOUNT ON
SELECT @sClause = 'SELECT t1.CompanyName, t1.Address1, t1.City, t1.Phone'
SELECT @fClause = 'FROM dbo.Company t1'
IF @CompanyId10 is not null SELECT @wClause = @wClause + ' AND t1.CompanyId = ' + convert(varchar,@CompanyId10)
SELECT @oClause = ''
EXEC (@sClause + ' ' + @fClause + ' ' + @wClause + ' ' + @oClause)
RETURN 0
 
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo._GetS05Rptwiz46V') AND type='P')
DROP PROCEDURE dbo._GetS05Rptwiz46V
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE dbo._GetS05Rptwiz46V
 @wClause	nvarchar(4000)
,@CompanyId10	SmallInt
/* WITH ENCRYPTION */
AS
DECLARE	 @sClause		nvarchar(4000)
SET NOCOUNT ON
/* It is mandatory for this procedure to return at least one row */
SELECT @sClause = 'SELECT ReportName=''TestCompany_'''
EXEC (@sClause)
RETURN 0
 
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo._GetS05Rptwiz51R') AND type='P')
DROP PROCEDURE dbo._GetS05Rptwiz51R
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE dbo._GetS05Rptwiz51R
 @wClause	nvarchar(4000)
,@CompanyId10	SmallInt
,@CompanyName20	NVarChar(100)
/* WITH ENCRYPTION */
AS
DECLARE	 @sClause		nvarchar(4000)
	,@fClause		nvarchar(4000)
	,@oClause		nvarchar(4000)
SET NOCOUNT ON
SELECT @sClause = 'SELECT t1.CompanyName, t1.Address1, t1.City, t1.Phone'
SELECT @fClause = 'FROM dbo.Company t1'
IF @CompanyId10 is not null SELECT @wClause = @wClause + ' AND t1.CompanyId = ' + convert(varchar,@CompanyId10)
IF @CompanyName20 is not null SELECT @wClause = @wClause + ' AND t1.CompanyName like ''%' + @CompanyName20 + '%'''
SELECT @oClause = ''
EXEC (@sClause + ' ' + @fClause + ' ' + @wClause + ' ' + @oClause)
RETURN 0
 
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo._GetS05Rptwiz51V') AND type='P')
DROP PROCEDURE dbo._GetS05Rptwiz51V
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE dbo._GetS05Rptwiz51V
 @wClause	nvarchar(4000)
,@CompanyId10	SmallInt
,@CompanyName20	NVarChar(100)
/* WITH ENCRYPTION */
AS
DECLARE	 @sClause		nvarchar(4000)
SET NOCOUNT ON
/* It is mandatory for this procedure to return at least one row */
SELECT @sClause = 'SELECT ReportName=''Testcompany1_'''
EXEC (@sClause)
RETURN 0
 
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.GetAdmTestt2ById') AND type='P')
DROP PROCEDURE dbo.GetAdmTestt2ById
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE GetAdmTestt2ById
 @KeyId1		nvarchar(1000)
/* WITH ENCRYPTION */
AS
SET NOCOUNT ON
DECLARE	 @sClause		nvarchar(max)
	,@fClause		nvarchar(max)
	,@wClause		nvarchar(max)
SELECT @fClause = 'FROM ROCmon.dbo.Company b1'
SELECT @sClause = 'SELECT CompanyId1=b1.CompanyId'
+ ', ParentId1=b1.ParentId'
+ ', CompanyDesc1=b1.CompanyDesc'
+ ', CompanyName1=b1.CompanyName'
+ ', LegalName1=b1.LegalName'
+ ', Address11=b1.Address1'
+ ', Address21=b1.Address2'
+ ', City1=b1.City'
+ ', ProvStateId1=b1.ProvStateId'
+ ', CountryId1=b1.CountryId'
+ ', PostalZip1=b1.PostalZip'
+ ', TollFree1=b1.TollFree'
+ ', Phone1=b1.Phone'
+ ', Fax1=b1.Fax'
+ ', WebUrl1=b1.WebUrl'
+ ', CurrencyId1=b1.CurrencyId'
+ ', HomeCurrencyId1=b1.HomeCurrencyId'
+ ', OperCurrencyId1=b1.OperCurrencyId'
+ ', FiscalStartMonth1=b1.FiscalStartMonth'
+ ', PurgedOn1=b1.PurgedOn'
+ ', Active1=b1.Active'
SELECT @wClause = 'WHERE b1.CompanyId' + isnull('='+@KeyId1,' is null')
EXEC (@sClause + ' ' + @fClause + ' ' + @wClause)
RETURN 0
 
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.GetExpAdmTestt2') AND type='P')
DROP PROCEDURE dbo.GetExpAdmTestt2
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE GetExpAdmTestt2
 @useGlobalFilter	char(1)
,@screenId		int
,@Usrs			varchar(1000)
,@RowAuthoritys		varchar(1000)
,@Customers		varchar(1000)
,@Vendors		varchar(1000)
,@Members		varchar(1000)
,@Investors		varchar(1000)
,@Agents		varchar(1000)
,@Brokers		varchar(1000)
,@UsrGroups		varchar(1000)
,@Companys		varchar(1000)
,@Projects		varchar(1000)
,@Cultures		varchar(1000)
,@Key		nvarchar(500)
,@FilterTxt		nvarchar(500)
,@screenFilterId	int
,@currCompanyId	smallint
,@currProjectId	smallint
,@topN	smallint = null
/* WITH ENCRYPTION */
AS
SET NOCOUNT ON
DECLARE	 @sClause		nvarchar(max)
	,@fClause		nvarchar(max)
	,@wClause		nvarchar(max)
	,@oClause		nvarchar(max)
	,@filterClause		nvarchar(2000)
	,@bUsr		char(1)
	,@UsrId			int
	,@tClause		nvarchar(max)
	,@cc		varchar(max)
	,@rr		varchar(1000)
	,@pp		varchar(1000)
	,@SelUsr		char(1)
	,@SelUsrGroup		char(1)
	,@SelCulture		char(1)
	,@SelCompany		char(1)
	,@SelAgent		char(1)
	,@SelBroker		char(1)
	,@SelCustomer		char(1)
	,@SelInvestor		char(1)
	,@SelMember		char(1)
	,@SelVendor		char(1)
	,@SelProject		char(1)
	,@RowAuthorityId	smallint
	,@CompanyId		SmallInt
SELECT @fClause='FROM ROCmon.dbo.Company b1 (NOLOCK)'
SELECT @sClause='SELECT DISTINCT ' + CASE WHEN @topN IS NULL OR @topN <= 0 THEN '' ELSE ' TOP ' + CONVERT(varchar(10),@topN) END + ' CompanyId1Text=b1.LegalName'
+ ', CompanyId1=b1.CompanyId'
+ ', ParentId1=b1.ParentId'
+ ', CompanyDesc1=b1.CompanyDesc'
+ ', CompanyName1=b1.CompanyName'
+ ', LegalName1=b1.LegalName'
+ ', Address11=b1.Address1'
+ ', Address21=b1.Address2'
+ ', City1=b1.City'
+ ', ProvStateId1=b1.ProvStateId'
+ ', CountryId1=b1.CountryId'
+ ', PostalZip1=b1.PostalZip'
+ ', TollFree1=b1.TollFree'
+ ', Phone1=b1.Phone'
+ ', Fax1=b1.Fax'
+ ', WebUrl1=b1.WebUrl'
+ ', CurrencyId1=b1.CurrencyId'
+ ', HomeCurrencyId1=b1.HomeCurrencyId'
+ ', OperCurrencyId1=b1.OperCurrencyId'
+ ', FiscalStartMonth1=b1.FiscalStartMonth'
+ ', PurgedOn1=b1.PurgedOn'
+ ', Active1=b1.Active'
SELECT @oClause='ORDER BY b1.LegalName'
SELECT @wClause='WHERE 1=1', @bUsr='Y'
SELECT @pp = @Usrs
WHILE @pp <> '' AND datalength(@pp) > 0
BEGIN
	EXEC RODesign.dbo.Pop1Int @pp OUTPUT,@UsrId OUTPUT
	IF @bUsr='Y'
	BEGIN
		SELECT @bUsr='N'
		SELECT @filterClause=rtrim(FilterClause) FROM ROCmonD.dbo.ScreenFilter WHERE ScreenFilterId=@screenFilterId AND ApplyToMst='Y'
		IF @@ROWCOUNT <> 0 SELECT @wClause=@wClause + ' AND ' + @filterClause
		IF @useGlobalFilter='Y'
		BEGIN
			SELECT @filterClause=rtrim(FilterClause) FROM ROCmonD.dbo.GlobalFilter WHERE UsrId=@UsrId AND ScreenId=@screenId
			IF @@ROWCOUNT <> 0 SELECT @fClause=@fClause + ' ' + replace(@filterClause,'~~.','b1.')
			ELSE
			BEGIN
				SELECT @filterClause=rtrim(FilterClause) FROM ROCmonD.dbo.GlobalFilter WHERE UsrId=@UsrId AND FilterDefault='Y'
				IF @@ROWCOUNT <> 0 SELECT @fClause=@fClause + ' ' + replace(@filterClause,'~~.','b1.')
			END
		END
	END
END
EXEC ROCmonD.dbo.GetPermFilter @screenId,null,@RowAuthoritys,@Companys,'CompanyId','Company','b1.','Y','N',null,'N','CompanyId',@wClause OUTPUT
SELECT @tClause = ''
IF @tClause <> '' SELECT @wClause = @wClause + ' AND (' + right(@tClause,len(@tClause)-4) + ')'
EXEC ROCmonD.dbo.GetCurrFilter @currCompanyId,'CompanyId','Company','b1.','N',null,'CompanyId',@wClause OUTPUT
IF @key is not null SELECT @wClause = @wClause + ' AND (b1.CompanyId = ' + @key + ')'
EXEC (@sClause + ' ' + @fClause + ' ' + @wClause + ' ' + @oClause)
RETURN 0
 
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.GetIn12CompanyId10') AND type='P')
DROP PROCEDURE dbo.GetIn12CompanyId10
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE GetIn12CompanyId10
 @reportId		int
,@RowAuthoritys		varchar(1000)
,@Usrs		varchar(1000)
,@UsrGroups		varchar(1000)
,@Cultures		varchar(1000)
,@Companys		varchar(1000)
,@Projects		varchar(1000)
,@Agents		varchar(1000)
,@Brokers		varchar(1000)
,@Customers		varchar(1000)
,@Investors		varchar(1000)
,@Members		varchar(1000)
,@Vendors		varchar(1000)
,@currCompanyId		smallint
,@currProjectId		smallint
/* WITH ENCRYPTION */
AS
SET NOCOUNT ON
DECLARE	 @wClause		varchar(max)
	,@tClause		varchar(max)
	,@cc		varchar(max)
	,@rr		varchar(1000)
	,@pp		varchar(1000)
	,@AllowSel		char(1)
	,@RowAuthorityId	smallint
SELECT @wClause = 'WHERE 1=1'
EXEC dbo._GetInCompanyId @wClause
RETURN 0
 
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.GetLisAdmTestt2') AND type='P')
DROP PROCEDURE dbo.GetLisAdmTestt2
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE GetLisAdmTestt2
 @useGlobalFilter	char(1)
,@screenId		int
,@Usrs			varchar(1000)
,@RowAuthoritys		varchar(1000)
,@Customers		varchar(1000)
,@Vendors		varchar(1000)
,@Members		varchar(1000)
,@Investors		varchar(1000)
,@Agents		varchar(1000)
,@Brokers		varchar(1000)
,@UsrGroups		varchar(1000)
,@Companys		varchar(1000)
,@Projects		varchar(1000)
,@Cultures		varchar(1000)
,@Key		nvarchar(500)
,@FilterTxt		nvarchar(500)
,@screenFilterId	int
,@currCompanyId	smallint
,@currProjectId	smallint
,@topN	smallint = null
/* WITH ENCRYPTION */
AS
SET NOCOUNT ON
DECLARE	 @sClause		nvarchar(max)
	,@fClause		nvarchar(max)
	,@wClause		nvarchar(max)
	,@oClause		nvarchar(max)
	,@filterClause		nvarchar(2000)
	,@bUsr		char(1)
	,@UsrId			int
	,@tClause		nvarchar(max)
	,@cc		varchar(max)
	,@rr		varchar(1000)
	,@pp		varchar(1000)
	,@SelUsr		char(1)
	,@SelUsrGroup		char(1)
	,@SelCulture		char(1)
	,@SelCompany		char(1)
	,@SelAgent		char(1)
	,@SelBroker		char(1)
	,@SelCustomer		char(1)
	,@SelInvestor		char(1)
	,@SelMember		char(1)
	,@SelVendor		char(1)
	,@SelProject		char(1)
	,@RowAuthorityId	smallint
	,@CompanyId		SmallInt
SELECT @fClause='FROM ROCmon.dbo.Company b1 (NOLOCK)'
SELECT @sClause='SELECT DISTINCT ' + CASE WHEN @topN IS NULL OR @topN <= 0 THEN '' ELSE ' TOP ' + CONVERT(varchar(10),@topN) END + ' CompanyId1=b1.CompanyId, CompanyId1Text=b1.LegalName, b1.Active'
SELECT @oClause='ORDER BY b1.Active DESC,b1.LegalName'
SELECT @wClause='WHERE 1=1', @bUsr='Y'
SELECT @pp = @Usrs
WHILE @pp <> '' AND datalength(@pp) > 0
BEGIN
	EXEC RODesign.dbo.Pop1Int @pp OUTPUT,@UsrId OUTPUT
	IF @bUsr='Y'
	BEGIN
		SELECT @bUsr='N'
		SELECT @filterClause=rtrim(FilterClause) FROM ROCmonD.dbo.ScreenFilter WHERE ScreenFilterId=@screenFilterId AND ApplyToMst='Y'
		IF @@ROWCOUNT <> 0 SELECT @wClause=@wClause + ' AND ' + @filterClause
		IF @useGlobalFilter='Y'
		BEGIN
			SELECT @filterClause=rtrim(FilterClause) FROM ROCmonD.dbo.GlobalFilter WHERE UsrId=@UsrId AND ScreenId=@screenId
			IF @@ROWCOUNT <> 0 SELECT @fClause=@fClause + ' ' + replace(@filterClause,'~~.','b1.')
			ELSE
			BEGIN
				SELECT @filterClause=rtrim(FilterClause) FROM ROCmonD.dbo.GlobalFilter WHERE UsrId=@UsrId AND FilterDefault='Y'
				IF @@ROWCOUNT <> 0 SELECT @fClause=@fClause + ' ' + replace(@filterClause,'~~.','b1.')
			END
		END
	END
END
EXEC ROCmonD.dbo.GetPermFilter @screenId,null,@RowAuthoritys,@Companys,'CompanyId','Company','b1.','Y','N',null,'N','CompanyId',@wClause OUTPUT
SELECT @tClause = ''
IF @tClause <> '' SELECT @wClause = @wClause + ' AND (' + right(@tClause,len(@tClause)-4) + ')'
EXEC ROCmonD.dbo.GetCurrFilter @currCompanyId,'CompanyId','Company','b1.','N',null,'CompanyId',@wClause OUTPUT
IF @key is not null SELECT @wClause = @wClause + ' AND (b1.CompanyId = ' + @key + ')'

SELECT @FilterTxt = REPLACE(@FilterTxt, '''','''''') 
IF @FilterTxt is not null AND @FilterTxt <> '' SELECT @wClause = @wClause + ' AND (b1.LegalName LIKE N''%' + REPLACE(@FilterTxt,' ','%') + '%'') '
EXEC (@sClause + ' ' + @fClause + ' ' + @wClause + ' ' + @oClause)
RETURN 0
 
GO
SET QUOTED_IDENTIFIER OFF
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MkStoredProcedure') AND type='P')
DROP PROCEDURE dbo.MkStoredProcedure
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


CREATE PROCEDURE [dbo].[MkStoredProcedure]
  @spString1	varchar(max)
, @spString2	varchar(max) = ''
, @spString3	varchar(max) = ''
, @spString4	varchar(max) = ''
, @spString5	varchar(max) = ''
, @spString6	varchar(max) = ''
, @spString7	varchar(max) = ''
, @spString8	varchar(max) = ''
, @spString9	varchar(max) = ''
/* WITH ENCRYPTION */
AS
-- Although PRINT will only print a maximum of max characters, EXEC will execute more than max characters if concatenated as follow.
EXEC ('SET QUOTED_IDENTIFIER ON SET ANSI_NULLS ON')
EXEC (@spString1 + @spString2 + @spString3 + @spString4 + @spString5 + @spString6 + @spString7 + @spString8 + @spString9)
EXEC ('SET QUOTED_IDENTIFIER OFF')
RETURN 0
 
 
GO
SET QUOTED_IDENTIFIER OFF
GO
