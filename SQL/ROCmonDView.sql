if not exists (select * from dbo.sysobjects where id = object_id(N'dbo.VwAppItem') and OBJECTPROPERTY(id, N'IsView') = 1)
EXEC('CREATE VIEW dbo.VwAppItem AS SELECT DUMMY=1')
GO
ALTER VIEW [dbo].[VwAppItem] AS
	SELECT a.AppInfoDesc, b.ItemOrder, b.DbProviderCd, b.AppItemName, b.MultiDesignDb, b.AppItemCode
	FROM dbo.AppInfo a INNER JOIN dbo.AppItem b ON a.AppInfoId = b.AppInfoId
	WHERE a.VersionDt is not null AND b.ObjectTypeCd = 'D'
	--15 year max of history
	AND a.VersionDt > dateadd(mm,-180,convert(datetime,convert(varchar,getdate(),102)))
GO
if not exists (select * from dbo.sysobjects where id = object_id(N'dbo.VwClnAppItem') and OBJECTPROPERTY(id, N'IsView') = 1)
EXEC('CREATE VIEW dbo.VwClnAppItem AS SELECT DUMMY=1')
GO
ALTER VIEW [dbo].[VwClnAppItem] AS
	SELECT a.AppInfoDesc, b.ItemOrder, b.ObjectTypeCd, b.AppItemName, b.AppItemCode
	FROM dbo.AppInfo a INNER JOIN dbo.AppItem b ON a.AppInfoId = b.AppInfoId
	WHERE a.VersionDt is not null AND b.ObjectTypeCd = 'C'
	AND a.VersionDt > dateadd(mm,-120,convert(datetime,convert(varchar,getdate(),102)))
GO
if not exists (select * from dbo.sysobjects where id = object_id(N'dbo.VwLabel') and OBJECTPROPERTY(id, N'IsView') = 1)
EXEC('CREATE VIEW dbo.VwLabel AS SELECT DUMMY=1')
GO
ALTER VIEW [dbo].[VwLabel] AS
SELECT LabelId, CultureId, LabelCat, LabelKey = '[['+LabelKey+']]', LabelText, SortOrder
	, LabelLink = 'AdmLabel.aspx?col=LabelId&typ=N&key=' + convert(varchar(20),LabelId)
	FROM dbo.Label
	WHERE CompanyId is null
GO
if not exists (select * from dbo.sysobjects where id = object_id(N'dbo.VwReportCriHlp') and OBJECTPROPERTY(id, N'IsView') = 1)
EXEC('CREATE VIEW dbo.VwReportCriHlp AS SELECT DUMMY=1')
GO
ALTER VIEW [dbo].[VwReportCriHlp]
AS
SELECT a.ReportCriHlpId, a.ReportCriHlpDesc, a.ReportCriId, b.ReportId, a.CultureId
FROM dbo.ReportCriHlp a INNER JOIN dbo.ReportCri b ON a.ReportCriId = b.ReportCriId
GO
if not exists (select * from dbo.sysobjects where id = object_id(N'dbo.VwRulAppItem') and OBJECTPROPERTY(id, N'IsView') = 1)
EXEC('CREATE VIEW dbo.VwRulAppItem AS SELECT DUMMY=1')
GO
ALTER VIEW [dbo].[VwRulAppItem] AS
	SELECT a.AppInfoDesc, b.ItemOrder, b.ObjectTypeCd, b.AppItemName, b.AppItemCode
	FROM dbo.AppInfo a INNER JOIN dbo.AppItem b ON a.AppInfoId = b.AppInfoId
	WHERE a.VersionDt is not null AND b.ObjectTypeCd = 'R'
	AND a.VersionDt > dateadd(mm,-120,convert(datetime,convert(varchar,getdate(),102)))
GO
if not exists (select * from dbo.sysobjects where id = object_id(N'dbo.VwScreenCriHlp') and OBJECTPROPERTY(id, N'IsView') = 1)
EXEC('CREATE VIEW dbo.VwScreenCriHlp AS SELECT DUMMY=1')
GO
ALTER VIEW [dbo].[VwScreenCriHlp]
AS
SELECT a.ScreenCriHlpId, a.ScreenCriHlpDesc, a.ScreenCriId, b.ScreenId, a.CultureId
FROM dbo.ScreenCriHlp a INNER JOIN dbo.ScreenCri b ON a.ScreenCriId = b.ScreenCriId
GO
if not exists (select * from dbo.sysobjects where id = object_id(N'dbo.VwScreenObjHlp') and OBJECTPROPERTY(id, N'IsView') = 1)
EXEC('CREATE VIEW dbo.VwScreenObjHlp AS SELECT DUMMY=1')
GO
ALTER VIEW [dbo].[VwScreenObjHlp]
AS
SELECT a.ScreenObjHlpId, a.ScreenObjHlpDesc, a.ScreenObjId, b.ScreenId, a.CultureId
FROM dbo.ScreenObjHlp a INNER JOIN dbo.ScreenObj b ON a.ScreenObjId = b.ScreenObjId
GO
if not exists (select * from dbo.sysobjects where id = object_id(N'dbo.VwServerRuleRunMode') and OBJECTPROPERTY(id, N'IsView') = 1)
EXEC('CREATE VIEW dbo.VwServerRuleRunMode AS SELECT DUMMY=1')
GO
ALTER VIEW [dbo].[VwServerRuleRunMode]
AS

SELECT
*
FROM
RODesign.dbo.VwServerRuleRunMode
GO