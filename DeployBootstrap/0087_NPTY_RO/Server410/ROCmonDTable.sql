IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.VwAppItem') AND type='V')
DROP VIEW dbo.VwAppItem
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.VwClnAppItem') AND type='V')
DROP VIEW dbo.VwClnAppItem
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.VwLabel') AND type='V')
DROP VIEW dbo.VwLabel
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.VwReportCriHlp') AND type='V')
DROP VIEW dbo.VwReportCriHlp
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.VwRulAppItem') AND type='V')
DROP VIEW dbo.VwRulAppItem
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.VwScreenCriHlp') AND type='V')
DROP VIEW dbo.VwScreenCriHlp
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.VwScreenObjHlp') AND type='V')
DROP VIEW dbo.VwScreenObjHlp
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AdvRule') AND type='U')
DROP TABLE dbo.AdvRule
GO
CREATE TABLE AdvRule ( 
AdvRuleId int IDENTITY(1,1) NOT NULL ,
RuleLayerCd char (1) NOT NULL ,
RuleName nvarchar (100) NOT NULL ,
RuleDesc nvarchar (150) NULL ,
RuleDescription nvarchar (500) NULL ,
ScreenId int NOT NULL ,
RmFuncProc varchar (100) NULL ,
AdvRuleProg nvarchar (max) NOT NULL ,
CONSTRAINT PK_AdvRule PRIMARY KEY CLUSTERED (
AdvRuleId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AppInfo') AND type='U')
DROP TABLE dbo.AppInfo
GO
CREATE TABLE AppInfo ( 
AppInfoId int IDENTITY(1,1) NOT NULL ,
AppInfoDesc varchar (50) NULL ,
VersionMa smallint NOT NULL ,
VersionMi smallint NOT NULL ,
VersionDt datetime NULL ,
AppZipId int NULL ,
Prerequisite nvarchar (max) NULL ,
Readme nvarchar (max) NULL ,
AppItemLink varchar (200) NULL ,
CultureTypeName varchar (10) NULL ,
VersionValue money NULL ,
CONSTRAINT PK_AppInfo PRIMARY KEY CLUSTERED (
AppInfoId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AppItem') AND type='U')
DROP TABLE dbo.AppItem
GO
CREATE TABLE AppItem ( 
AppItemId int IDENTITY(1,1) NOT NULL ,
AppItemDesc varchar (200) NULL ,
AppInfoId int NOT NULL ,
ItemOrder smallint NULL ,
ObjectTypeCd char (1) NOT NULL ,
LanguageCd char (1) NULL ,
FrameworkCd char (1) NULL ,
DbProviderCd char (1) NULL ,
RelativePath varchar (100) NULL ,
AppItemName varchar (50) NOT NULL ,
MultiDesignDb char (1) NOT NULL ,
AppItemCode nvarchar (max) NULL ,
RemoveItem char (1) NOT NULL ,
ScreenId int NULL ,
ReportId int NULL ,
WizardId int NULL ,
CustomId int NULL ,
AppItemLink varchar (200) NULL ,
CONSTRAINT PK_AppItem PRIMARY KEY CLUSTERED (
AppItemId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_AppZip')
DROP INDEX AppZip.IX_AppZip 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AppZip') AND type='U')
DROP TABLE dbo.AppZip
GO
CREATE TABLE AppZip ( 
DocId int IDENTITY(1,1) NOT NULL ,
MasterId int NOT NULL ,
DocName nvarchar (100) NOT NULL ,
MimeType varchar (100) NOT NULL ,
DocSize bigint NOT NULL ,
DocImage varbinary (max) NOT NULL ,
InputBy int NULL ,
InputOn datetime NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_AppZip PRIMARY KEY CLUSTERED (
DocId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ButtonHlp') AND type='U')
DROP TABLE dbo.ButtonHlp
GO
CREATE TABLE ButtonHlp ( 
ButtonHlpId int IDENTITY(1,1) NOT NULL ,
ScreenId int NULL ,
ReportId int NULL ,
WizardId int NULL ,
CultureId smallint NOT NULL ,
ButtonTypeId tinyint NOT NULL ,
ButtonName nvarchar (200) NULL ,
ButtonLongNm nvarchar (400) NULL ,
ButtonToolTip nvarchar (400) NULL ,
ButtonVisible char (1) NOT NULL ,
TopVisible char (1) NOT NULL CONSTRAINT DF_ButtonHlp_TopVisible DEFAULT ('N'),
RowVisible char (1) NOT NULL CONSTRAINT DF_ButtonHlp_RowVisible DEFAULT ('N'),
BotVisible char (1) NOT NULL CONSTRAINT DF_ButtonHlp_BotVisible DEFAULT ('N'),
CONSTRAINT PK_ButtonHlp PRIMARY KEY CLUSTERED (
ButtonHlpId,
CultureId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ClientRule') AND type='U')
DROP TABLE dbo.ClientRule
GO
CREATE TABLE ClientRule ( 
ClientRuleId int IDENTITY(1,1) NOT NULL ,
RuleTypeId tinyint NOT NULL ,
RuleName nvarchar (100) NOT NULL ,
RuleDesc nvarchar (150) NULL ,
RuleDescription nvarchar (500) NULL ,
RuleMethodId tinyint NOT NULL CONSTRAINT DF_ClientRule_RuleMethodId DEFAULT ((1)),
ScreenId int NULL ,
ReportId int NULL ,
CultureId smallint NOT NULL ,
ScreenObjHlpId int NULL ,
ScreenCriHlpId int NULL ,
ReportCriHlpId int NULL ,
ClientScript smallint NULL ,
UserScriptEvent varchar (50) NULL ,
UserScriptName nvarchar (1000) NULL ,
ScriptParam nvarchar (500) NULL ,
RuleCntTypeId tinyint NULL ,
ClientRuleProg nvarchar (max) NULL ,
CONSTRAINT PK_ClientRule PRIMARY KEY CLUSTERED (
ClientRuleId,
CultureId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ColOvrd') AND type='U')
DROP TABLE dbo.ColOvrd
GO
CREATE TABLE ColOvrd ( 
ColOvrdId int IDENTITY(1,1) NOT NULL ,
ColOvrdDesc varchar (200) NULL ,
ScreenObjId int NOT NULL ,
ScreenId int NULL ,
ColVisible char (1) NOT NULL ,
ColReadOnly char (1) NOT NULL ,
ColExport char (1) NOT NULL ,
ToolTip nvarchar (200) NULL ,
ColumnHeader nvarchar (50) NULL ,
ErrMessage nvarchar (300) NULL ,
PermKeyId smallint NULL ,
PermId int NULL ,
Priority smallint NULL ,
PermKeyRowId int NULL ,
CONSTRAINT PK_ColOvrd PRIMARY KEY CLUSTERED (
ColOvrdId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CronJob') AND type='U')
DROP TABLE dbo.CronJob
GO
CREATE TABLE CronJob ( 
CronJobId int IDENTITY(1,1) NOT NULL ,
CronJobName nvarchar (200) NOT NULL ,
Year smallint NULL ,
Month tinyint NULL ,
Day tinyint NULL ,
Hour tinyint NULL ,
Minute tinyint NULL ,
DayOfWeek tinyint NULL ,
LastRun datetime NULL ,
NextRun datetime NULL ,
JobLink varchar (200) NOT NULL ,
LastStatus nvarchar (500) NULL ,
CONSTRAINT PK_CronJob PRIMARY KEY CLUSTERED (
CronJobId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.DbColumn') AND type='U')
DROP TABLE dbo.DbColumn
GO
CREATE TABLE DbColumn ( 
ColumnId int IDENTITY(1,1) NOT NULL ,
TableId int NOT NULL ,
ColumnIndex smallint NULL ,
ExternalTable varchar (50) NULL ,
ColumnName varchar (50) NOT NULL ,
ColumnDesc varchar (100) NULL ,
DataType tinyint NOT NULL ,
ColumnLength smallint NOT NULL ,
ColumnScale tinyint NULL ,
DefaultValue nvarchar (50) NULL ,
AllowNulls char (1) NOT NULL ,
ColumnIdentity char (1) NOT NULL ,
PrimaryKey char (1) NOT NULL ,
IsIndexU char (1) NOT NULL CONSTRAINT DF_DbColumn_IsIndexU DEFAULT ('N'),
IsIndex char (1) NOT NULL CONSTRAINT DF_DbColumn_IsIndex DEFAULT ('N'),
ColObjective nvarchar (200) NULL ,
PrevColName varchar (20) NULL ,
CONSTRAINT PK_DbColumn PRIMARY KEY CLUSTERED (
ColumnId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_DbKey_KeyName')
DROP INDEX DbKey.IX_DbKey_KeyName 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.DbKey') AND type='U')
DROP TABLE dbo.DbKey
GO
CREATE TABLE DbKey ( 
KeyId int IDENTITY(1,1) NOT NULL ,
KeyName varchar (50) NOT NULL ,
TableId int NOT NULL ,
ColumnId int NOT NULL ,
RefTableId int NOT NULL ,
RefColumnId int NOT NULL ,
CONSTRAINT PK_DbKey PRIMARY KEY CLUSTERED (
KeyId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IU_DbTable')
DROP INDEX DbTable.IU_DbTable 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.DbTable') AND type='U')
DROP TABLE dbo.DbTable
GO
CREATE TABLE DbTable ( 
TableId int IDENTITY(1,1) NOT NULL ,
SystemId tinyint NOT NULL ,
TableName varchar (500) NOT NULL ,
TableDesc nvarchar (100) NOT NULL ,
MultiDesignDb char (1) NOT NULL ,
VirtualTbl char (1) NOT NULL CONSTRAINT DF_DbTable_VirtualTbl DEFAULT ('N'),
VirtualSql nvarchar (max) NULL ,
TblObjective nvarchar (500) NULL ,
PrevTblName varchar (20) NULL ,
ModifiedBy int NULL ,
ModifiedOn datetime NULL ,
LastSyncDt datetime NULL ,
CONSTRAINT PK_DbTable PRIMARY KEY CLUSTERED (
TableId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Deleted') AND type='U')
DROP TABLE dbo.Deleted
GO
CREATE TABLE Deleted ( 
DbName varchar (50) NOT NULL ,
TableName varchar (50) NOT NULL ,
PKeyId varchar (100) NOT NULL ,
DContent nvarchar (max) NULL ,
DeletedBy int NOT NULL ,
DeletedOn datetime NOT NULL 
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Document') AND type='U')
DROP TABLE dbo.Document
GO
CREATE TABLE Document ( 
DocumentId int IDENTITY(1,1) NOT NULL ,
DocuTblName varchar (50) NOT NULL ,
MenuTblName varchar (50) NOT NULL ,
HelpTblName varchar (50) NOT NULL ,
DocuFilePath varchar (100) NOT NULL ,
DocumentTitle nvarchar (50) NOT NULL ,
ProgramName varchar (50) NOT NULL ,
DocumentNotes varchar (4000) NULL ,
CONSTRAINT PK_Document PRIMARY KEY CLUSTERED (
DocumentId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.GlobalFilter') AND type='U')
DROP TABLE dbo.GlobalFilter
GO
CREATE TABLE GlobalFilter ( 
UsrId int NOT NULL ,
ScreenId int NOT NULL ,
FilterClause varchar (1800) NOT NULL ,
FilterDesc ntext NOT NULL ,
FilterDefault char (1) NOT NULL ,
CONSTRAINT PK_GlobalFilter PRIMARY KEY CLUSTERED (
UsrId,
ScreenId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.GlobalFilterKey') AND type='U')
DROP TABLE dbo.GlobalFilterKey
GO
CREATE TABLE GlobalFilterKey ( 
UsrId int NOT NULL ,
ScreenId int NOT NULL ,
GlobalFilterKey bigint NOT NULL ,
CONSTRAINT PK_GlobalFilterKey PRIMARY KEY CLUSTERED (
UsrId,
ScreenId,
GlobalFilterKey
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Label')
DROP INDEX Label.IX_Label 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Label') AND type='U')
DROP TABLE dbo.Label
GO
CREATE TABLE Label ( 
LabelId int IDENTITY(1,1) NOT NULL ,
LabelDesc varchar (200) NULL ,
CultureId smallint NOT NULL ,
LabelCat varchar (50) NOT NULL CONSTRAINT DF_Label_LabelCat DEFAULT ('NA'),
LabelKey varchar (50) NOT NULL ,
LabelText nvarchar (max) NOT NULL ,
CompanyId int NULL ,
SortOrder smallint NULL ,
CONSTRAINT PK_Label PRIMARY KEY CLUSTERED (
LabelId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IU_MemTrans')
DROP INDEX MemTrans.IU_MemTrans 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MemTrans') AND type='U')
DROP TABLE dbo.MemTrans
GO
CREATE TABLE MemTrans ( 
MemTransId int IDENTITY(1,1) NOT NULL ,
InStr nvarchar (2000) NOT NULL ,
CultureTypeId smallint NOT NULL ,
OutStr nvarchar (2000) NOT NULL ,
CONSTRAINT PK_MemTrans PRIMARY KEY CLUSTERED (
MemTransId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Menu') AND type='U')
DROP TABLE dbo.Menu
GO
CREATE TABLE Menu ( 
MenuId int IDENTITY(1,1) NOT NULL ,
MenuIndex smallint NOT NULL ,
Miscellaneous varchar (1000) NULL ,
ParentId int NULL ,
ReportId int NULL ,
ScreenId int NULL ,
WizardId int NULL ,
StaticPgId int NULL ,
IconUrl nvarchar (300) NULL ,
Popup char (1) NOT NULL CONSTRAINT DF_Menu_Popup DEFAULT ('N'),
CONSTRAINT PK_Menu PRIMARY KEY CLUSTERED (
MenuId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_MenuHlp_MenuId')
DROP INDEX MenuHlp.IX_MenuHlp_MenuId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MenuHlp') AND type='U')
DROP TABLE dbo.MenuHlp
GO
CREATE TABLE MenuHlp ( 
MenuHlpId int IDENTITY(1,1) NOT NULL ,
MenuId int NOT NULL ,
CultureId smallint NOT NULL ,
MenuText nvarchar (50) NOT NULL ,
CONSTRAINT PK_MenuHlp PRIMARY KEY CLUSTERED (
MenuHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MenuPrm') AND type='U')
DROP TABLE dbo.MenuPrm
GO
CREATE TABLE MenuPrm ( 
MenuPrmId int IDENTITY(1,1) NOT NULL ,
MenuId int NOT NULL ,
GrantDeny char (1) NOT NULL ,
PermKeyId smallint NOT NULL ,
PermId int NOT NULL ,
CONSTRAINT PK_MenuPrm PRIMARY KEY CLUSTERED (
MenuPrmId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Msg') AND type='U')
DROP TABLE dbo.Msg
GO
CREATE TABLE Msg ( 
MsgId int IDENTITY(1,1) NOT NULL ,
MsgTypeCd char (1) NOT NULL ,
MsgSource varchar (50) NOT NULL ,
MsgName nvarchar (500) NOT NULL ,
CONSTRAINT PK_Msg PRIMARY KEY CLUSTERED (
MsgId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_MsgCenter')
DROP INDEX MsgCenter.IX_MsgCenter 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MsgCenter') AND type='U')
DROP TABLE dbo.MsgCenter
GO
CREATE TABLE MsgCenter ( 
MsgCenterId int IDENTITY(1,1) NOT NULL ,
MsgId int NOT NULL ,
CultureId smallint NOT NULL ,
Msg nvarchar (800) NOT NULL ,
CONSTRAINT PK_MsgCenter PRIMARY KEY CLUSTERED (
MsgCenterId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Report') AND type='U')
DROP TABLE dbo.Report
GO
CREATE TABLE Report ( 
ReportId int IDENTITY(1,1) NOT NULL ,
ProgramName varchar (20) NOT NULL ,
ReportDesc nvarchar (50) NULL ,
OrientationCd char (1) NOT NULL ,
ReportTypeCd char (1) NOT NULL ,
TemplateName varchar (50) NULL ,
RptTemplate int NULL ,
CopyReportId int NULL ,
TopMargin decimal (8,2) NOT NULL CONSTRAINT DF_Report_TopMargin DEFAULT ((1)),
BottomMargin decimal (8,2) NOT NULL CONSTRAINT DF_Report_BottomMargin DEFAULT ((1)),
LeftMargin decimal (8,2) NOT NULL CONSTRAINT DF_Report_LeftMargin DEFAULT ((1)),
RightMargin decimal (8,2) NOT NULL CONSTRAINT DF_Report_RightMargin DEFAULT ((1)),
PageHeight decimal (8,2) NOT NULL CONSTRAINT DF_Report_PageHeight DEFAULT ((11)),
PageWidth decimal (8,2) NOT NULL CONSTRAINT DF_Report_PageWidth DEFAULT ((8.5)),
UnitCd char (2) NOT NULL CONSTRAINT DF_Report_UnitCd DEFAULT ('in'),
WhereClause varchar (1000) NULL ,
RegClause varchar (400) NULL ,
RegCode nvarchar (max) NULL ,
ValClause varchar (400) NULL ,
ValCode nvarchar (max) NULL ,
UpdClause varchar (200) NULL ,
UpdCode nvarchar (max) NULL ,
XlsClause varchar (200) NULL ,
XlsCode nvarchar (max) NULL ,
GenerateRp char (1) NOT NULL ,
AllowSelect char (1) NOT NULL CONSTRAINT DF_Report_AllowSelect DEFAULT ('N'),
ModifiedBy int NULL ,
ModifiedOn datetime NULL ,
LastGenDt datetime NULL ,
CompanyLs varchar (1000) NULL ,
NeedRegen char (1) NOT NULL CONSTRAINT DF_Report_NeedRegen DEFAULT ('N'),
AuthRequired char (1) NOT NULL CONSTRAINT DF_Report_AuthRequired DEFAULT ('Y'),
CONSTRAINT PK_Report PRIMARY KEY CLUSTERED (
ReportId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReportCri') AND type='U')
DROP TABLE dbo.ReportCri
GO
CREATE TABLE ReportCri ( 
ReportCriId int IDENTITY(1,1) NOT NULL ,
ReportId int NOT NULL ,
ReportCriDesc varchar (100) NULL ,
ReportGrpId int NOT NULL ,
LabelCss varchar (100) NULL ,
ContentCss varchar (100) NULL ,
TableId int NULL ,
TableAbbr varchar (10) NULL ,
ColumnName varchar (50) NOT NULL ,
TabIndex smallint NOT NULL ,
DataTypeId tinyint NOT NULL ,
DataTypeSize smallint NULL ,
DisplayModeId tinyint NOT NULL ,
ColumnSize smallint NULL ,
RowSize smallint NULL ,
DdlKeyColumnName varchar (50) NULL ,
DdlRefColumnName varchar (50) NULL ,
DdlSrtColumnName varchar (50) NULL ,
DdlFtrColumnId int NULL ,
RequiredValid char (1) NOT NULL ,
DefaultValue nvarchar (100) NULL ,
WhereClause varchar (1000) NULL ,
RegClause varchar (400) NULL ,
CONSTRAINT PK_ReportCri PRIMARY KEY CLUSTERED (
ReportCriId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ReportCriHlp_ReportCriId')
DROP INDEX ReportCriHlp.IX_ReportCriHlp_ReportCriId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReportCriHlp') AND type='U')
DROP TABLE dbo.ReportCriHlp
GO
CREATE TABLE ReportCriHlp ( 
ReportCriHlpId int IDENTITY(1,1) NOT NULL ,
ReportCriHlpDesc varchar (200) NULL ,
ReportCriId int NOT NULL ,
CultureId smallint NOT NULL ,
ColumnHeader nvarchar (50) NULL ,
CONSTRAINT PK_ReportCriHlp PRIMARY KEY CLUSTERED (
ReportCriHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReportDel') AND type='U')
DROP TABLE dbo.ReportDel
GO
CREATE TABLE ReportDel ( 
ReportDelId int IDENTITY(1,1) NOT NULL ,
ReportId int NOT NULL ,
ProgramName varchar (100) NOT NULL ,
CONSTRAINT PK_ReportDel PRIMARY KEY CLUSTERED (
ReportDelId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReportGrp') AND type='U')
DROP TABLE dbo.ReportGrp
GO
CREATE TABLE ReportGrp ( 
ReportGrpId int IDENTITY(1,1) NOT NULL ,
ReportId int NULL ,
ParentGrpId int NULL ,
ReportGrpName nvarchar (50) NOT NULL ,
ReportGrpIndex varchar (20) NULL ,
TabIndex smallint NULL ,
ContentVertical char (1) NOT NULL ,
LabelVertical char (1) NOT NULL ,
BorderWidth tinyint NOT NULL ,
GrpStyle varchar (200) NULL ,
CONSTRAINT PK_ReportGrp PRIMARY KEY CLUSTERED (
ReportGrpId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ReportHlp_ReportId')
DROP INDEX ReportHlp.IX_ReportHlp_ReportId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReportHlp') AND type='U')
DROP TABLE dbo.ReportHlp
GO
CREATE TABLE ReportHlp ( 
ReportHlpId int IDENTITY(1,1) NOT NULL ,
ReportId int NOT NULL ,
CultureId smallint NOT NULL ,
DefaultHlpMsg nvarchar (max) NOT NULL ,
ReportTitle nvarchar (50) NOT NULL ,
CONSTRAINT PK_ReportHlp PRIMARY KEY CLUSTERED (
ReportHlpId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ReportLstCri')
DROP INDEX ReportLstCri.IX_ReportLstCri 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReportLstCri') AND type='U')
DROP TABLE dbo.ReportLstCri
GO
CREATE TABLE ReportLstCri ( 
ReportLstCriId int IDENTITY(1,1) NOT NULL ,
UsrId int NOT NULL ,
ReportId int NOT NULL ,
ReportCriId int NOT NULL ,
LastCriteria nvarchar (900) NULL ,
CONSTRAINT PK_ReportLstCri PRIMARY KEY CLUSTERED (
ReportLstCriId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReportObj') AND type='U')
DROP TABLE dbo.ReportObj
GO
CREATE TABLE ReportObj ( 
ReportObjId int IDENTITY(1,1) NOT NULL ,
ReportId int NOT NULL ,
RptObjTypeCd char (1) NOT NULL CONSTRAINT DF_ReportObj_RptObjTypeCd DEFAULT ('F'),
ColumnName varchar (50) NOT NULL ,
ColumnDesc varchar (100) NULL ,
TabIndex smallint NOT NULL ,
ColumnFormat varchar (20) NULL ,
PaddSize tinyint NULL ,
PaddChar nchar (1) NULL ,
DataTypeId tinyint NOT NULL ,
OperatorId tinyint NULL ,
ReportCriId int NULL ,
CONSTRAINT PK_ReportObj PRIMARY KEY CLUSTERED (
ReportObjId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ReportObjHlp_ReportObjId')
DROP INDEX ReportObjHlp.IX_ReportObjHlp_ReportObjId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReportObjHlp') AND type='U')
DROP TABLE dbo.ReportObjHlp
GO
CREATE TABLE ReportObjHlp ( 
ReportObjHlpId int IDENTITY(1,1) NOT NULL ,
ReportObjHlpDesc varchar (200) NULL ,
ReportObjId int NOT NULL ,
CultureId smallint NOT NULL ,
ColumnHeader nvarchar (50) NULL ,
HeaderWidth smallint NULL ,
CONSTRAINT PK_ReportObjHlp PRIMARY KEY CLUSTERED (
ReportObjHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RowOvrd') AND type='U')
DROP TABLE dbo.RowOvrd
GO
CREATE TABLE RowOvrd ( 
RowOvrdId smallint IDENTITY(1,1) NOT NULL ,
RowOvrdDesc varchar (200) NULL ,
ScreenId int NULL ,
ReportId int NULL ,
RowAuthId smallint NOT NULL ,
AllowSel char (1) NOT NULL ,
AllowAdd char (1) NOT NULL ,
AllowUpd char (1) NOT NULL ,
AllowDel char (1) NOT NULL ,
CONSTRAINT PK_RowOvrd PRIMARY KEY CLUSTERED (
RowOvrdId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RowOvrdPrm') AND type='U')
DROP TABLE dbo.RowOvrdPrm
GO
CREATE TABLE RowOvrdPrm ( 
RowOvrdPrmId smallint IDENTITY(1,1) NOT NULL ,
RowOvrdId smallint NOT NULL ,
PermKeyId smallint NOT NULL ,
SelLevel char (1) NOT NULL ,
AndCondition char (1) NOT NULL CONSTRAINT DF_RowOvrdPrm_AndCondition DEFAULT ('Y'),
CONSTRAINT PK_RowOvrdPrm PRIMARY KEY CLUSTERED (
RowOvrdPrmId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptCel') AND type='U')
DROP TABLE dbo.RptCel
GO
CREATE TABLE RptCel ( 
RptCelId int IDENTITY(1,1) NOT NULL ,
RptCelDesc nvarchar (200) NULL ,
RptTblId int NOT NULL ,
RowNum smallint NOT NULL ,
RowHeight decimal (8,2) NOT NULL ,
RowVisibility varchar (1000) NULL ,
CelNum int NOT NULL ,
CelColSpan smallint NULL ,
CONSTRAINT PK_RptCel PRIMARY KEY CLUSTERED (
RptCelId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptCha') AND type='U')
DROP TABLE dbo.RptCha
GO
CREATE TABLE RptCha ( 
RptChaId int IDENTITY(1,1) NOT NULL ,
RptChaDesc nvarchar (200) NULL ,
RptCtrId int NOT NULL ,
ReportId int NULL ,
RptChaTypeCd char (2) NOT NULL ,
ThreeD char (1) NOT NULL ,
CategoryGrp int NOT NULL ,
ChartData nvarchar (1000) NOT NULL ,
SeriesGrp int NULL ,
CONSTRAINT PK_RptCha PRIMARY KEY CLUSTERED (
RptChaId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptCtr') AND type='U')
DROP TABLE dbo.RptCtr
GO
CREATE TABLE RptCtr ( 
RptCtrId int IDENTITY(1,1) NOT NULL ,
RptCtrDesc nvarchar (200) NULL ,
PRptCtrId int NULL ,
RptElmId int NULL ,
RptCelId int NULL ,
ReportId int NOT NULL ,
RptCtrTypeCd char (1) NOT NULL ,
RptCtrName nvarchar (100) NOT NULL ,
RptStyleId int NULL ,
CtrTop decimal (8,2) NULL ,
CtrLeft decimal (8,2) NULL ,
CtrHeight decimal (8,2) NULL ,
CtrWidth decimal (8,2) NULL ,
CtrZIndex smallint NULL ,
CtrAction varchar (500) NULL ,
CtrGrouping int NULL ,
CtrVisibility char (1) NULL ,
CtrToggle int NULL ,
CtrToolTip nvarchar (200) NULL ,
CtrPgBrStart char (1) NOT NULL ,
CtrPgBrEnd char (1) NOT NULL ,
CtrValue nvarchar (1000) NULL ,
CtrCanGrow char (1) NOT NULL ,
CtrCanShrink char (1) NOT NULL ,
CtrTogether char (1) NOT NULL ,
CONSTRAINT PK_RptCtr PRIMARY KEY CLUSTERED (
RptCtrId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptElm') AND type='U')
DROP TABLE dbo.RptElm
GO
CREATE TABLE RptElm ( 
RptElmId int IDENTITY(1,1) NOT NULL ,
RptElmDesc nvarchar (200) NULL ,
ReportId int NOT NULL ,
RptElmTypeCd char (1) NOT NULL ,
RptStyleId int NULL ,
ElmHeight decimal (8,2) NOT NULL ,
ElmColumns smallint NULL ,
ElmColSpacing decimal (8,2) NULL ,
ElmPrintFirst char (1) NOT NULL ,
ElmPrintLast char (1) NOT NULL ,
CONSTRAINT PK_RptElm PRIMARY KEY CLUSTERED (
RptElmId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_RptMemCri')
DROP INDEX RptMemCri.IX_RptMemCri 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptMemCri') AND type='U')
DROP TABLE dbo.RptMemCri
GO
CREATE TABLE RptMemCri ( 
RptMemCriId int IDENTITY(1,1) NOT NULL ,
RptMemFldId int NOT NULL ,
RptMemCriName nvarchar (200) NOT NULL ,
RptMemCriDesc nvarchar (500) NULL ,
RptMemCriLink varchar (200) NULL ,
ReportId int NOT NULL ,
UsrId int NULL ,
InputBy int NULL ,
InputOn datetime NULL ,
ModifiedOn datetime NULL ,
ViewedOn datetime NULL ,
CompanyLs varchar (1000) NULL ,
CONSTRAINT PK_RptMemCri PRIMARY KEY CLUSTERED (
RptMemCriId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptMemCriDtl') AND type='U')
DROP TABLE dbo.RptMemCriDtl
GO
CREATE TABLE RptMemCriDtl ( 
RptMemCriDtlId int IDENTITY(1,1) NOT NULL ,
RptMemCriId int NOT NULL ,
ReportCriId int NOT NULL ,
MemCriteria nvarchar (900) NULL ,
CONSTRAINT PK_RptMemCriDtl PRIMARY KEY CLUSTERED (
RptMemCriDtlId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptMemFld') AND type='U')
DROP TABLE dbo.RptMemFld
GO
CREATE TABLE RptMemFld ( 
RptMemFldId int IDENTITY(1,1) NOT NULL ,
RptMemFldName nvarchar (200) NOT NULL ,
UsrId int NULL ,
InputBy int NULL ,
CompanyLs varchar (1000) NULL ,
AccessCd char (1) NOT NULL CONSTRAINT DF_RptMemFld_AccessCd DEFAULT ('V'),
CONSTRAINT PK_RptMemFld PRIMARY KEY CLUSTERED (
RptMemFldId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptStyle') AND type='U')
DROP TABLE dbo.RptStyle
GO
CREATE TABLE RptStyle ( 
RptStyleId int IDENTITY(1,1) NOT NULL ,
RptStyleDesc nvarchar (300) NOT NULL ,
BorderColorD varchar (100) NULL ,
BorderColorL varchar (100) NULL ,
BorderColorR varchar (100) NULL ,
BorderColorT varchar (100) NULL ,
BorderColorB varchar (100) NULL ,
BorderStyleD tinyint NULL ,
BorderStyleL tinyint NULL ,
BorderStyleR tinyint NULL ,
BorderStyleT tinyint NULL ,
BorderStyleB tinyint NULL ,
BorderWidthD tinyint NULL ,
BorderWidthL tinyint NULL ,
BorderWidthR tinyint NULL ,
BorderWidthT tinyint NULL ,
BorderWidthB tinyint NULL ,
BgColor varchar (100) NULL ,
BgGradType tinyint NULL ,
BgGradColor varchar (100) NULL ,
BgImage varchar (200) NULL ,
FontStyle char (1) NULL ,
FontFamily varchar (100) NULL ,
FontSize tinyint NULL ,
FontWeight tinyint NULL ,
Format varchar (100) NULL ,
TextDecor char (1) NULL ,
TextAlign char (1) NULL ,
VerticalAlign char (1) NULL ,
Color varchar (100) NULL ,
PadLeft smallint NULL ,
PadRight smallint NULL ,
PadTop smallint NULL ,
PadBottom smallint NULL ,
LineHeight smallint NULL ,
Direction char (1) NULL ,
WritingMode char (1) NULL ,
DefaultCd char (2) NULL ,
CONSTRAINT PK_RptStyle PRIMARY KEY CLUSTERED (
RptStyleId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptTbl') AND type='U')
DROP TABLE dbo.RptTbl
GO
CREATE TABLE RptTbl ( 
RptTblId int IDENTITY(1,1) NOT NULL ,
RptTblDesc nvarchar (200) NULL ,
ParentId int NULL ,
RptCtrId int NOT NULL ,
ReportId int NULL ,
RptTblTypeCd char (1) NOT NULL ,
TblRepeatNew char (1) NOT NULL ,
TblOrder smallint NULL ,
ColWidth decimal (8,2) NULL ,
TblGrouping int NULL ,
TblVisibility char (1) NULL ,
TblToggle int NULL ,
CONSTRAINT PK_RptTbl PRIMARY KEY CLUSTERED (
RptTblId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_RptTemplate')
DROP INDEX RptTemplate.IX_RptTemplate 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptTemplate') AND type='U')
DROP TABLE dbo.RptTemplate
GO
CREATE TABLE RptTemplate ( 
DocId int IDENTITY(1,1) NOT NULL ,
MasterId int NOT NULL ,
DocName nvarchar (100) NOT NULL ,
MimeType varchar (100) NOT NULL ,
DocSize bigint NOT NULL ,
DocImage varbinary (max) NOT NULL ,
InputBy int NULL ,
InputOn datetime NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_RptTemplate PRIMARY KEY CLUSTERED (
DocId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Rptwiz') AND type='U')
DROP TABLE dbo.Rptwiz
GO
CREATE TABLE Rptwiz ( 
RptwizId int IDENTITY(1,1) NOT NULL ,
RptwizName nvarchar (50) NOT NULL ,
RptwizDesc nvarchar (400) NOT NULL ,
RptwizTypeCd char (1) NOT NULL ,
RptwizCatId smallint NOT NULL ,
TemplateName varchar (50) NULL ,
ReportId int NULL ,
AccessCd char (1) NOT NULL ,
UsrId int NOT NULL ,
TopMargin decimal (8,2) NULL ,
BottomMargin decimal (8,2) NULL ,
LeftMargin decimal (8,2) NULL ,
RightMargin decimal (8,2) NULL ,
OrientationCd char (1) NOT NULL CONSTRAINT DF_Rptwiz_OrientationCd DEFAULT ('Y'),
UnitCd char (2) NOT NULL CONSTRAINT DF_Rptwiz_UnitCd DEFAULT ('in'),
RptChaTypeCd char (2) NULL ,
ThreeD char (1) NOT NULL CONSTRAINT DF_Rptwiz_ThreeD DEFAULT ('N'),
GMinValue decimal (19,4) NULL ,
GLowRange decimal (19,4) NULL ,
GMidRange decimal (19,4) NULL ,
GMaxValue decimal (19,4) NULL ,
GNeedle decimal (19,4) NULL ,
GMinValueId int NULL ,
GLowRangeId int NULL ,
GMidRangeId int NULL ,
GMaxValueId int NULL ,
GNeedleId int NULL ,
GPositive char (1) NULL ,
GFormat char (1) NULL ,
CONSTRAINT PK_Rptwiz PRIMARY KEY CLUSTERED (
RptwizId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptwizCat') AND type='U')
DROP TABLE dbo.RptwizCat
GO
CREATE TABLE RptwizCat ( 
RptwizCatId smallint IDENTITY(1,1) NOT NULL ,
RptwizTypId smallint NOT NULL CONSTRAINT DF_RptwizCat_RptwizTypId DEFAULT ((1)),
RptwizCatName nvarchar (100) NOT NULL ,
RptwizCatDesc nvarchar (200) NULL ,
CatDescription nvarchar (400) NULL ,
TableId int NOT NULL CONSTRAINT DF_RptwizCat_TableId DEFAULT ((0)),
SampleImage varbinary (max) NULL ,
CONSTRAINT PK_RptwizCat PRIMARY KEY CLUSTERED (
RptwizCatId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptwizCatDtl') AND type='U')
DROP TABLE dbo.RptwizCatDtl
GO
CREATE TABLE RptwizCatDtl ( 
RptwizCatDtlId int IDENTITY(1,1) NOT NULL ,
RptwizCatId smallint NOT NULL ,
ColumnId int NOT NULL ,
DisplayModeId tinyint NULL ,
DdlKeyColNm varchar (50) NULL ,
DdlRefColNm varchar (50) NULL ,
ColumnSize smallint NULL ,
RowSize smallint NULL ,
RegClause varchar (400) NULL ,
StoredProc varchar (max) NULL ,
CONSTRAINT PK_RptwizCatDtl PRIMARY KEY CLUSTERED (
RptwizCatDtlId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptwizDtl') AND type='U')
DROP TABLE dbo.RptwizDtl
GO
CREATE TABLE RptwizDtl ( 
RptwizDtlId int IDENTITY(1,1) NOT NULL ,
RptwizId int NOT NULL ,
ColumnId int NOT NULL ,
ColHeader nvarchar (50) NULL ,
CriOperatorId tinyint NULL ,
CriSelect smallint NULL ,
CriHeader nvarchar (50) NULL ,
ColSelect smallint NULL ,
ColGroup smallint NULL ,
ColSort smallint NULL ,
AggregateCd char (1) NULL ,
RptChartCd char (1) NULL ,
CONSTRAINT PK_RptwizDtl PRIMARY KEY CLUSTERED (
RptwizDtlId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RptwizTyp') AND type='U')
DROP TABLE dbo.RptwizTyp
GO
CREATE TABLE RptwizTyp ( 
RptwizTypId smallint IDENTITY(1,1) NOT NULL ,
RptwizTypName nvarchar (100) NOT NULL ,
CONSTRAINT PK_RptwizTyp PRIMARY KEY CLUSTERED (
RptwizTypId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RuleAsmx') AND type='U')
DROP TABLE dbo.RuleAsmx
GO
CREATE TABLE RuleAsmx ( 
RuleAsmxId int IDENTITY(1,1) NOT NULL ,
RuleAsmxTypeId tinyint NOT NULL ,
RuleAsmxName nvarchar (100) NOT NULL ,
RuleAsmxDesc nvarchar (150) NULL ,
RuleDescription nvarchar (500) NULL ,
ScreenId int NOT NULL ,
RuleAsmxProg nvarchar (max) NOT NULL ,
CONSTRAINT PK_RuleAsmx PRIMARY KEY CLUSTERED (
RuleAsmxId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.RuleReact') AND type='U')
DROP TABLE dbo.RuleReact
GO
CREATE TABLE RuleReact ( 
RuleReactId int IDENTITY(1,1) NOT NULL ,
RuleReactTypeId tinyint NOT NULL ,
RuleReactName nvarchar (100) NOT NULL ,
RuleReactDesc nvarchar (150) NULL ,
RuleDescription nvarchar (500) NULL ,
ScreenId int NOT NULL ,
ScreenObjId int NULL ,
RuleReactProg nvarchar (max) NOT NULL ,
CONSTRAINT PK_RuleReact PRIMARY KEY CLUSTERED (
RuleReactId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScrAudit') AND type='U')
DROP TABLE dbo.ScrAudit
GO
CREATE TABLE ScrAudit ( 
ScrAuditId bigint IDENTITY(1,1) NOT NULL ,
CudAction char (1) NOT NULL ,
ScreenId int NOT NULL ,
MasterTable char (1) NOT NULL ,
TableId int NOT NULL ,
RowId bigint NOT NULL ,
RowDesc nvarchar (max) NOT NULL ,
ChangedBy int NOT NULL ,
ChangedOn datetime NOT NULL 
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScrAuditDtl') AND type='U')
DROP TABLE dbo.ScrAuditDtl
GO
CREATE TABLE ScrAuditDtl ( 
ScrAuditDtlId bigint IDENTITY(1,1) NOT NULL ,
ScrAuditId bigint NOT NULL ,
ScreenObjId int NOT NULL ,
ScreenObjDesc nvarchar (max) NOT NULL ,
ColumnId int NOT NULL ,
ColumnDesc nvarchar (max) NOT NULL ,
ChangedFr nvarchar (max) NULL ,
ChangedTo nvarchar (max) NULL 
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Screen') AND type='U')
DROP TABLE dbo.Screen
GO
CREATE TABLE Screen ( 
ScreenId int IDENTITY(1,1) NOT NULL ,
ProgramName varchar (20) NOT NULL ,
ScreenDesc nvarchar (50) NULL ,
ScreenTypeId tinyint NOT NULL ,
MasterTableId int NULL ,
DetailTableId int NULL ,
SearchTableId int NULL ,
SearchId int NULL ,
SearchAscending char (1) NOT NULL ,
SearchIdR int NULL ,
SearchDtlId int NULL ,
SearchDtlIdR int NULL ,
SearchUrlId int NULL ,
SearchImgId int NULL ,
GridRows tinyint NULL ,
ScreenObj varchar (100) NULL ,
ScreenFilter varchar (100) NULL ,
GenerateSc char (1) NOT NULL CONSTRAINT DF_Screen_GenerateSc DEFAULT ('Y'),
GenerateSr char (1) NOT NULL CONSTRAINT DF_Screen_GenerateSr DEFAULT ('Y'),
ReactGenerated char (1) NOT NULL CONSTRAINT DF_Screen_ReactGenerated DEFAULT ('N'),
HasDeleteAll char (1) NOT NULL ,
ShowGridHead char (1) NOT NULL CONSTRAINT DF_Screen_ShowGridHead DEFAULT ('Y'),
ValidateReq char (1) NOT NULL ,
DeferError char (1) NOT NULL CONSTRAINT DF_Screen_DeferError DEFAULT ('N'),
AuthRequired char (1) NOT NULL CONSTRAINT DF_Screen_AuthRequired DEFAULT ('Y'),
ViewOnly char (1) NOT NULL CONSTRAINT DF_Screen_ViewOnly DEFAULT ('N'),
GenAudit char (1) NOT NULL CONSTRAINT DF_Screen_GenAudit DEFAULT ('N'),
NeedRegen char (1) NOT NULL CONSTRAINT DF_Screen_NeedRegen DEFAULT ('N'),
NeedReactRegen char (1) NOT NULL CONSTRAINT DF_Screen_NeedReactRegen DEFAULT ('N'),
CONSTRAINT PK_Screen PRIMARY KEY CLUSTERED (
ScreenId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenCri') AND type='U')
DROP TABLE dbo.ScreenCri
GO
CREATE TABLE ScreenCri ( 
ScreenCriId int IDENTITY(1,1) NOT NULL ,
ScreenId int NOT NULL ,
ScreenCriDesc varchar (100) NULL ,
LabelCss varchar (100) NULL ,
ContentCss varchar (100) NULL ,
ColumnId int NOT NULL ,
OperatorId tinyint NOT NULL CONSTRAINT DF_ScreenCri_OperatorId DEFAULT ((1)),
TabIndex smallint NOT NULL ,
DisplayModeId tinyint NOT NULL ,
ColumnJustify char (1) NULL ,
ColumnSize smallint NULL ,
RowSize smallint NULL ,
DdlKeyColumnId int NULL ,
DdlRefColumnId int NULL ,
DdlSrtColumnId int NULL ,
DdlFtrColumnId int NULL ,
RequiredValid char (1) NOT NULL ,
CONSTRAINT PK_ScreenCri PRIMARY KEY CLUSTERED (
ScreenCriId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ScreenCriHlp_ScreenCriId')
DROP INDEX ScreenCriHlp.IX_ScreenCriHlp_ScreenCriId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenCriHlp') AND type='U')
DROP TABLE dbo.ScreenCriHlp
GO
CREATE TABLE ScreenCriHlp ( 
ScreenCriHlpId int IDENTITY(1,1) NOT NULL ,
ScreenCriHlpDesc varchar (200) NULL ,
ScreenCriId int NOT NULL ,
CultureId smallint NOT NULL ,
ColumnHeader nvarchar (50) NULL ,
CONSTRAINT PK_ScreenCriHlp PRIMARY KEY CLUSTERED (
ScreenCriHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenDel') AND type='U')
DROP TABLE dbo.ScreenDel
GO
CREATE TABLE ScreenDel ( 
ScreenDelId int IDENTITY(1,1) NOT NULL ,
ScreenId int NOT NULL ,
ProgramName varchar (100) NOT NULL ,
MultiDesignDb char (1) NOT NULL ,
CONSTRAINT PK_ScreenDel PRIMARY KEY CLUSTERED (
ScreenDelId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ScreenFilter_Id_Name')
DROP INDEX ScreenFilter.IX_ScreenFilter_Id_Name 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenFilter') AND type='U')
DROP TABLE dbo.ScreenFilter
GO
CREATE TABLE ScreenFilter ( 
ScreenFilterId int IDENTITY(1,1) NOT NULL ,
ScreenId int NOT NULL ,
ScreenFilterName varchar (50) NOT NULL ,
ScreenFilterDesc varchar (100) NULL ,
FilterClause varchar (1500) NOT NULL ,
FilterOrder tinyint NOT NULL ,
ApplyToMst char (1) NOT NULL CONSTRAINT DF_ScreenFilter_ApplyToMst DEFAULT ('Y'),
CONSTRAINT PK_ScreenFilter PRIMARY KEY CLUSTERED (
ScreenFilterId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ScreenFilterHlp_FilterId')
DROP INDEX ScreenFilterHlp.IX_ScreenFilterHlp_FilterId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenFilterHlp') AND type='U')
DROP TABLE dbo.ScreenFilterHlp
GO
CREATE TABLE ScreenFilterHlp ( 
ScreenFilterHlpId int IDENTITY(1,1) NOT NULL ,
ScreenFilterId int NOT NULL ,
CultureId smallint NOT NULL ,
FilterName nvarchar (50) NOT NULL ,
CONSTRAINT PK_ScreenFilterHlp PRIMARY KEY CLUSTERED (
ScreenFilterHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenHlp') AND type='U')
DROP TABLE dbo.ScreenHlp
GO
CREATE TABLE ScreenHlp ( 
ScreenHlpId int IDENTITY(1,1) NOT NULL ,
ScreenId int NOT NULL ,
CultureId smallint NOT NULL ,
DefaultHlpMsg nvarchar (max) NOT NULL ,
FootNote nvarchar (400) NULL ,
ScreenTitle nvarchar (50) NOT NULL ,
AddMsg nvarchar (100) NULL ,
UpdMsg nvarchar (100) NULL ,
DelMsg nvarchar (100) NULL ,
IncrementMsg nvarchar (100) NULL ,
MasterLstTitle nvarchar (100) NULL ,
MasterLstSubtitle nvarchar (100) NULL ,
MasterRecTitle nvarchar (100) NULL ,
MasterRecSubtitle nvarchar (100) NULL ,
DetailLstTitle nvarchar (100) NULL ,
DetailLstSubtitle nvarchar (100) NULL ,
DetailRecTitle nvarchar (100) NULL ,
DetailRecSubtitle nvarchar (100) NULL ,
NoMasterMsg nvarchar (100) NULL ,
NoDetailMsg nvarchar (100) NULL ,
AddMasterMsg nvarchar (100) NULL ,
AddDetailMsg nvarchar (100) NULL ,
MasterFoundMsg nvarchar (100) NULL ,
DetailFoundMsg nvarchar (100) NULL ,
CONSTRAINT PK_ScreenHlp PRIMARY KEY CLUSTERED (
ScreenHlpId,
CultureId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ScreenLstCri')
DROP INDEX ScreenLstCri.IX_ScreenLstCri 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenLstCri') AND type='U')
DROP TABLE dbo.ScreenLstCri
GO
CREATE TABLE ScreenLstCri ( 
ScreenLstCriId int IDENTITY(1,1) NOT NULL ,
UsrId int NOT NULL ,
ScreenId int NOT NULL ,
ScreenCriId int NOT NULL ,
LastCriteria nvarchar (900) NULL ,
CONSTRAINT PK_ScreenLstCri PRIMARY KEY CLUSTERED (
ScreenLstCriId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ScreenLstInf')
DROP INDEX ScreenLstInf.IX_ScreenLstInf 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenLstInf') AND type='U')
DROP TABLE dbo.ScreenLstInf
GO
CREATE TABLE ScreenLstInf ( 
ScreenLstInfId int IDENTITY(1,1) NOT NULL ,
UsrId int NOT NULL ,
ScreenId int NOT NULL ,
LastPageInfo nvarchar (900) NULL ,
CONSTRAINT PK_ScreenLstInf PRIMARY KEY CLUSTERED (
ScreenLstInfId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenObj') AND type='U')
DROP TABLE dbo.ScreenObj
GO
CREATE TABLE ScreenObj ( 
ScreenObjId int IDENTITY(1,1) NOT NULL ,
ScreenId int NOT NULL ,
MasterTable char (1) NOT NULL ,
NewGroupRow char (1) NOT NULL CONSTRAINT DF_ScreenObj_NewGroupRow DEFAULT ('N'),
GroupRowId smallint NOT NULL CONSTRAINT DF_ScreenObj_GroupRowId DEFAULT ((78)),
GroupColId smallint NOT NULL ,
LabelVertical char (1) NOT NULL CONSTRAINT DF_ScreenObj_LabelVertical DEFAULT ('N'),
LabelCss varchar (1000) NULL ,
ContentCss varchar (1000) NULL ,
ColumnId int NULL ,
ColumnName varchar (50) NOT NULL ,
ColumnDesc varchar (200) NULL ,
DefaultValue nvarchar (200) NULL ,
HyperLinkUrl nvarchar (200) NULL ,
DefAfter char (1) NOT NULL CONSTRAINT DF_ScreenObj_DefAfter DEFAULT ('N'),
SystemValue nvarchar (200) NULL ,
DefAlways char (1) NOT NULL CONSTRAINT DF_ScreenObj_DefAlways DEFAULT ('N'),
ColumnWrap char (1) NOT NULL ,
GridGrpCd char (1) NOT NULL CONSTRAINT DF_ScreenObj_GridGrpCd DEFAULT ('N'),
HideOnTablet char (1) NOT NULL CONSTRAINT DF_ScreenObj_HideOnTablet DEFAULT ('N'),
HideOnMobile char (1) NOT NULL CONSTRAINT DF_ScreenObj_HideOnMobile DEFAULT ('N'),
ColumnJustify char (1) NULL ,
ColumnSize smallint NULL ,
ResizeWidth smallint NULL ,
ColumnHeight smallint NULL ,
ResizeHeight smallint NULL ,
DisplayModeId tinyint NOT NULL ,
DdlKeyColumnId int NULL ,
DdlRefColumnId int NULL ,
DdlSrtColumnId int NULL ,
DdlAdnColumnId int NULL ,
DdlFtrColumnId int NULL ,
AggregateCd char (1) NULL ,
GenerateSp char (1) NOT NULL ,
TabFolderId int NOT NULL ,
TabIndex smallint NOT NULL ,
SortOrder smallint NULL ,
DtlLstPosId tinyint NULL ,
RequiredValid char (1) NOT NULL ,
MaskValid varchar (100) NULL ,
RangeValidType varchar (50) NULL ,
RangeValidMax varchar (50) NULL ,
RangeValidMin varchar (50) NULL ,
ColumnLink varchar (1000) NULL ,
RefreshOnCUD char (1) NOT NULL ,
TrimOnEntry char (1) NOT NULL CONSTRAINT DF_ScreenObj_TrimOnEntry DEFAULT ('Y'),
MatchCd char (1) NULL ,
IgnoreConfirm char (1) NOT NULL CONSTRAINT DF_ScreenObj_IgnoreConfirm DEFAULT ('Y'),
CONSTRAINT PK_ScreenObj PRIMARY KEY CLUSTERED (
ScreenObjId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenObjHlp') AND type='U')
DROP TABLE dbo.ScreenObjHlp
GO
CREATE TABLE ScreenObjHlp ( 
ScreenObjHlpId int IDENTITY(1,1) NOT NULL ,
ScreenObjHlpDesc varchar (200) NULL ,
ScreenObjId int NOT NULL ,
CultureId smallint NOT NULL ,
ColumnHeader nvarchar (max) NULL ,
ToolTip nvarchar (1000) NULL ,
ErrMessage nvarchar (1000) NULL ,
TbHint nvarchar (1000) NULL ,
CONSTRAINT PK_ScreenObjHlp PRIMARY KEY CLUSTERED (
ScreenObjHlpId,
CultureId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenTab') AND type='U')
DROP TABLE dbo.ScreenTab
GO
CREATE TABLE ScreenTab ( 
ScreenTabId int IDENTITY(1,1) NOT NULL ,
ScreenTabDesc nvarchar (200) NULL ,
ScreenId int NOT NULL ,
TabFolderName nvarchar (30) NOT NULL ,
TabFolderOrder tinyint NOT NULL ,
CONSTRAINT PK_ScreenTab PRIMARY KEY CLUSTERED (
ScreenTabId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ScreenTabHlp_ScreenTabId')
DROP INDEX ScreenTabHlp.IX_ScreenTabHlp_ScreenTabId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScreenTabHlp') AND type='U')
DROP TABLE dbo.ScreenTabHlp
GO
CREATE TABLE ScreenTabHlp ( 
ScreenTabHlpId int IDENTITY(1,1) NOT NULL ,
ScreenTabId int NOT NULL ,
CultureId smallint NOT NULL ,
TabFolderName nvarchar (50) NOT NULL ,
CONSTRAINT PK_ScreenTabHlp PRIMARY KEY CLUSTERED (
ScreenTabHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScrMemCri') AND type='U')
DROP TABLE dbo.ScrMemCri
GO
CREATE TABLE ScrMemCri ( 
ScrMemCriId int IDENTITY(1,1) NOT NULL ,
ScrMemCriName nvarchar (200) NOT NULL ,
ScreenId int NOT NULL ,
UsrId int NULL ,
CONSTRAINT PK_ScrMemCri PRIMARY KEY CLUSTERED (
ScrMemCriId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ScrMemCriDtl') AND type='U')
DROP TABLE dbo.ScrMemCriDtl
GO
CREATE TABLE ScrMemCriDtl ( 
ScrMemCriDtlId int IDENTITY(1,1) NOT NULL ,
ScrMemCriId int NOT NULL ,
ScreenCriId int NOT NULL ,
MemCriteria nvarchar (900) NULL ,
CONSTRAINT PK_ScrMemCriDtl PRIMARY KEY CLUSTERED (
ScrMemCriDtlId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ServerRule')
DROP INDEX ServerRule.IX_ServerRule 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ServerRule') AND type='U')
DROP TABLE dbo.ServerRule
GO
CREATE TABLE ServerRule ( 
ServerRuleId int IDENTITY(1,1) NOT NULL ,
ScreenId int NOT NULL ,
RuleTypeId tinyint NOT NULL ,
MasterTable char (1) NOT NULL ,
RuleName nvarchar (100) NOT NULL ,
RuleDesc nvarchar (150) NULL ,
RuleDescription nvarchar (500) NULL ,
RuleOrder smallint NOT NULL ,
ProcedureName varchar (50) NOT NULL ,
ParameterNames varchar (max) NULL ,
ParameterTypes varchar (max) NULL ,
CallingParams varchar (max) NULL ,
OnAdd char (1) NOT NULL ,
OnUpd char (1) NOT NULL ,
OnDel char (1) NOT NULL ,
BeforeCRUD char (1) NOT NULL ,
RuleCode nvarchar (max) NULL ,
ModifiedBy int NULL ,
ModifiedOn datetime NULL ,
LastGenDt datetime NULL ,
CONSTRAINT PK_ServerRule PRIMARY KEY CLUSTERED (
ServerRuleId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StaticCs') AND type='U')
DROP TABLE dbo.StaticCs
GO
CREATE TABLE StaticCs ( 
StaticCsId smallint IDENTITY(1,1) NOT NULL ,
StaticCsNm nvarchar (200) NOT NULL ,
StyleDef nvarchar (max) NULL ,
CONSTRAINT PK_StaticCs PRIMARY KEY CLUSTERED (
StaticCsId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StaticFi') AND type='U')
DROP TABLE dbo.StaticFi
GO
CREATE TABLE StaticFi ( 
StaticFiId int IDENTITY(1,1) NOT NULL ,
StaticFiUrl nvarchar (200) NOT NULL ,
CONSTRAINT PK_StaticFi PRIMARY KEY CLUSTERED (
StaticFiId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StaticJs') AND type='U')
DROP TABLE dbo.StaticJs
GO
CREATE TABLE StaticJs ( 
StaticJsId smallint IDENTITY(1,1) NOT NULL ,
StaticJsNm nvarchar (200) NOT NULL ,
ScriptDef nvarchar (max) NULL ,
CONSTRAINT PK_StaticJs PRIMARY KEY CLUSTERED (
StaticJsId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StaticPg') AND type='U')
DROP TABLE dbo.StaticPg
GO
CREATE TABLE StaticPg ( 
StaticPgId int IDENTITY(1,1) NOT NULL ,
StaticPgDesc nvarchar (300) NULL ,
StaticPgNm varchar (50) NOT NULL CONSTRAINT DF_StaticPg_StaticPgNm DEFAULT ('PgNm'),
StaticPgTitle nvarchar (100) NOT NULL ,
StaticMeta nvarchar (1000) NULL ,
StaticPgCss nvarchar (max) NULL ,
StaticPgJs nvarchar (max) NULL ,
StaticPgHtm nvarchar (max) NOT NULL ,
MasterPgFile varchar (100) NULL ,
StaticCsId smallint NULL ,
StaticJsId smallint NULL ,
StaticPgUrl varchar (200) NULL ,
CONSTRAINT PK_StaticPg PRIMARY KEY CLUSTERED (
StaticPgId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.TbdRule') AND type='U')
DROP TABLE dbo.TbdRule
GO
CREATE TABLE TbdRule ( 
TbdRuleId int IDENTITY(1,1) NOT NULL ,
ScreenId int NOT NULL ,
TbdRuleName nvarchar (100) NOT NULL ,
TbdRuleDesc nvarchar (max) NULL ,
CONSTRAINT PK_TbdRule PRIMARY KEY CLUSTERED (
TbdRuleId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Template_TmplDefault')
DROP INDEX Template.IX_Template_TmplDefault 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Template') AND type='U')
DROP TABLE dbo.Template
GO
CREATE TABLE Template ( 
TemplateId smallint IDENTITY(1,1) NOT NULL ,
TemplateName nvarchar (30) NOT NULL ,
TmplPrefix varchar (10) NOT NULL ,
TmplDefault char (1) NOT NULL ,
AltTemplateId smallint NULL ,
CONSTRAINT PK_Template PRIMARY KEY CLUSTERED (
TemplateId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Usage_UsrId')
DROP INDEX Usage.IX_Usage_UsrId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Usage') AND type='U')
DROP TABLE dbo.Usage
GO
CREATE TABLE Usage ( 
UsageDt datetime NOT NULL ,
UsrId int NOT NULL ,
UsageNote nvarchar (200) NULL ,
EntityTitle nvarchar (50) NOT NULL ,
ScreenId int NULL ,
ReportId int NULL ,
WizardId int NULL ,
Miscellaneous varchar (100) NULL 
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_UtReport_ProgramName')
DROP INDEX UtReport.IX_UtReport_ProgramName 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtReport') AND type='U')
DROP TABLE dbo.UtReport
GO
CREATE TABLE UtReport ( 
ReportId int IDENTITY(1,1) NOT NULL ,
ProgramName varchar (20) NOT NULL ,
ReportDesc nvarchar (50) NULL ,
OrientationCd char (1) NOT NULL ,
ReportTypeCd char (1) NOT NULL ,
TemplateName varchar (50) NULL ,
RptTemplate int NULL ,
CopyReportId int NULL ,
TopMargin decimal (8,2) NOT NULL CONSTRAINT DF_UtReport_TopMargin DEFAULT ((1)),
BottomMargin decimal (8,2) NOT NULL CONSTRAINT DF_UtReport_BottomMargin DEFAULT ((1)),
LeftMargin decimal (8,2) NOT NULL CONSTRAINT DF_UtReport_LeftMargin DEFAULT ((1)),
RightMargin decimal (8,2) NOT NULL CONSTRAINT DF_UtReport_RightMargin DEFAULT ((1)),
PageHeight decimal (8,2) NOT NULL CONSTRAINT DF_UtReport_PageHeight DEFAULT ((11)),
PageWidth decimal (8,2) NOT NULL CONSTRAINT DF_UtReport_PageWidth DEFAULT ((8.5)),
UnitCd char (2) NOT NULL CONSTRAINT DF_UtReport_UnitCd DEFAULT ('in'),
WhereClause varchar (1000) NULL ,
RegClause varchar (400) NULL ,
RegCode nvarchar (max) NULL ,
ValClause varchar (400) NULL ,
ValCode nvarchar (max) NULL ,
UpdClause varchar (200) NULL ,
UpdCode nvarchar (max) NULL ,
XlsClause varchar (200) NULL ,
XlsCode nvarchar (max) NULL ,
GenerateRp char (1) NOT NULL ,
AllowSelect char (1) NOT NULL CONSTRAINT DF_UtReport_AllowSelect DEFAULT ('N'),
ModifiedBy int NULL ,
ModifiedOn datetime NULL ,
LastGenDt datetime NULL ,
CompanyLs varchar (1000) NULL ,
NeedRegen char (1) NOT NULL CONSTRAINT DF_UtReport_NeedRegen DEFAULT ('N'),
CONSTRAINT PK_UtReport PRIMARY KEY CLUSTERED (
ReportId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtReportCri') AND type='U')
DROP TABLE dbo.UtReportCri
GO
CREATE TABLE UtReportCri ( 
ReportCriId int IDENTITY(1,1) NOT NULL ,
ReportId int NOT NULL ,
ReportCriDesc varchar (100) NULL ,
ReportGrpId int NOT NULL ,
LabelCss varchar (100) NULL ,
ContentCss varchar (100) NULL ,
TableId int NULL ,
TableAbbr varchar (10) NULL ,
ColumnName varchar (50) NULL ,
TabIndex smallint NOT NULL ,
DataTypeId tinyint NOT NULL ,
DataTypeSize smallint NULL ,
DisplayModeId tinyint NOT NULL ,
ColumnSize smallint NULL ,
RowSize smallint NULL ,
DdlKeyColumnName varchar (50) NULL ,
DdlRefColumnName varchar (50) NULL ,
DdlSrtColumnName varchar (50) NULL ,
DdlFtrColumnId int NULL ,
RequiredValid char (1) NOT NULL ,
WhereClause varchar (1000) NULL ,
RegClause varchar (400) NULL ,
CONSTRAINT PK_UtReportCri PRIMARY KEY CLUSTERED (
ReportCriId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtReportCriHlp') AND type='U')
DROP TABLE dbo.UtReportCriHlp
GO
CREATE TABLE UtReportCriHlp ( 
ReportCriHlpId int IDENTITY(1,1) NOT NULL ,
ReportCriHlpDesc varchar (200) NULL ,
ReportCriId int NOT NULL ,
CultureId smallint NOT NULL ,
ColumnHeader nvarchar (50) NULL ,
CONSTRAINT PK_UtReportCriHlp PRIMARY KEY CLUSTERED (
ReportCriHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtReportDel') AND type='U')
DROP TABLE dbo.UtReportDel
GO
CREATE TABLE UtReportDel ( 
UtReportDelId int IDENTITY(1,1) NOT NULL ,
ReportId int NOT NULL ,
ProgramName varchar (100) NOT NULL ,
CONSTRAINT PK_UtReportDel PRIMARY KEY CLUSTERED (
UtReportDelId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtReportHlp') AND type='U')
DROP TABLE dbo.UtReportHlp
GO
CREATE TABLE UtReportHlp ( 
ReportHlpId int IDENTITY(1,1) NOT NULL ,
ReportId int NOT NULL ,
CultureId smallint NOT NULL ,
DefaultHlpMsg nvarchar (max) NOT NULL ,
ReportTitle nvarchar (50) NOT NULL ,
CONSTRAINT PK_UtReportHlp PRIMARY KEY CLUSTERED (
ReportHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtReportLstCri') AND type='U')
DROP TABLE dbo.UtReportLstCri
GO
CREATE TABLE UtReportLstCri ( 
ReportLstCriId int IDENTITY(1,1) NOT NULL ,
UsrId int NOT NULL ,
ReportId int NOT NULL ,
ReportCriId int NOT NULL ,
LastCriteria nvarchar (900) NULL ,
CONSTRAINT PK_UtReportLstCri PRIMARY KEY CLUSTERED (
ReportLstCriId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtReportObj') AND type='U')
DROP TABLE dbo.UtReportObj
GO
CREATE TABLE UtReportObj ( 
ReportObjId int IDENTITY(1,1) NOT NULL ,
ReportId int NOT NULL ,
RptObjTypeCd char (1) NOT NULL CONSTRAINT DF_UtReportObj_RptObjTypeCd DEFAULT ('F'),
ColumnName varchar (50) NULL ,
ColumnDesc varchar (100) NULL ,
TabIndex smallint NOT NULL ,
ColumnFormat varchar (20) NULL ,
PaddSize tinyint NULL ,
PaddChar nchar (1) NULL ,
DataTypeId tinyint NOT NULL ,
OperatorId tinyint NULL ,
ReportCriId int NULL ,
CONSTRAINT PK_UtReportObj PRIMARY KEY CLUSTERED (
ReportObjId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtReportObjHlp') AND type='U')
DROP TABLE dbo.UtReportObjHlp
GO
CREATE TABLE UtReportObjHlp ( 
ReportObjHlpId int IDENTITY(1,1) NOT NULL ,
ReportObjHlpDesc varchar (200) NULL ,
ReportObjId int NOT NULL ,
CultureId smallint NOT NULL ,
ColumnHeader nvarchar (50) NULL ,
HeaderWidth smallint NULL ,
CONSTRAINT PK_UtReportObjHlp PRIMARY KEY CLUSTERED (
ReportObjHlpId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtRptCel') AND type='U')
DROP TABLE dbo.UtRptCel
GO
CREATE TABLE UtRptCel ( 
RptCelId int IDENTITY(1,1) NOT NULL ,
RptCelDesc nvarchar (200) NULL ,
RptTblId int NOT NULL ,
RowNum smallint NOT NULL ,
RowHeight decimal (8,2) NOT NULL ,
RowVisibility varchar (1000) NULL ,
CelNum smallint NOT NULL ,
CelColSpan smallint NULL ,
CONSTRAINT PK_UtRptCel PRIMARY KEY CLUSTERED (
RptCelId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtRptCha') AND type='U')
DROP TABLE dbo.UtRptCha
GO
CREATE TABLE UtRptCha ( 
RptChaId int IDENTITY(1,1) NOT NULL ,
RptChaDesc nvarchar (200) NULL ,
RptCtrId int NOT NULL ,
ReportId int NULL ,
RptChaTypeCd char (2) NOT NULL ,
ThreeD char (1) NOT NULL ,
CategoryGrp int NOT NULL ,
ChartData nvarchar (1000) NOT NULL ,
SeriesGrp int NULL ,
CONSTRAINT PK_UtRptCha PRIMARY KEY CLUSTERED (
RptChaId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtRptCtr') AND type='U')
DROP TABLE dbo.UtRptCtr
GO
CREATE TABLE UtRptCtr ( 
RptCtrId int IDENTITY(1,1) NOT NULL ,
RptCtrDesc nvarchar (200) NULL ,
PRptCtrId int NULL ,
RptElmId int NULL ,
RptCelId int NULL ,
ReportId int NOT NULL ,
RptCtrTypeCd char (1) NOT NULL ,
RptCtrName nvarchar (100) NOT NULL ,
RptStyleId int NULL ,
CtrTop decimal (8,2) NULL ,
CtrLeft decimal (8,2) NULL ,
CtrHeight decimal (8,2) NULL ,
CtrWidth decimal (8,2) NULL ,
CtrZIndex smallint NULL ,
CtrAction varchar (500) NULL ,
CtrGrouping int NULL ,
CtrVisibility char (1) NULL ,
CtrToggle int NULL ,
CtrToolTip nvarchar (200) NULL ,
CtrPgBrStart char (1) NOT NULL ,
CtrPgBrEnd char (1) NOT NULL ,
CtrValue nvarchar (1000) NULL ,
CtrCanGrow char (1) NOT NULL ,
CtrCanShrink char (1) NOT NULL ,
CtrTogether char (1) NOT NULL ,
CONSTRAINT PK_UtRptCtr PRIMARY KEY CLUSTERED (
RptCtrId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtRptElm') AND type='U')
DROP TABLE dbo.UtRptElm
GO
CREATE TABLE UtRptElm ( 
RptElmId int IDENTITY(1,1) NOT NULL ,
RptElmDesc nvarchar (200) NULL ,
ReportId int NOT NULL ,
RptElmTypeCd char (1) NOT NULL ,
RptStyleId int NULL ,
ElmHeight decimal (8,2) NOT NULL ,
ElmColumns smallint NULL ,
ElmColSpacing decimal (8,2) NULL ,
ElmPrintFirst char (1) NOT NULL ,
ElmPrintLast char (1) NOT NULL ,
CONSTRAINT PK_UtRptElm PRIMARY KEY CLUSTERED (
RptElmId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtRptMemCri') AND type='U')
DROP TABLE dbo.UtRptMemCri
GO
CREATE TABLE UtRptMemCri ( 
RptMemCriId int IDENTITY(1,1) NOT NULL ,
RptMemFldId int NOT NULL ,
RptMemCriName nvarchar (200) NOT NULL ,
RptMemCriDesc nvarchar (500) NULL ,
RptMemCriLink varchar (200) NULL ,
ReportId int NOT NULL ,
UsrId int NULL ,
InputBy int NULL ,
InputOn datetime NULL ,
ModifiedOn datetime NULL ,
ViewedOn datetime NULL ,
CompanyLs varchar (1000) NULL ,
CONSTRAINT PK_UtRptMemCri PRIMARY KEY CLUSTERED (
RptMemCriId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtRptMemCriDtl') AND type='U')
DROP TABLE dbo.UtRptMemCriDtl
GO
CREATE TABLE UtRptMemCriDtl ( 
RptMemCriDtlId int IDENTITY(1,1) NOT NULL ,
RptMemCriId int NOT NULL ,
ReportCriId int NOT NULL ,
MemCriteria nvarchar (900) NULL ,
CONSTRAINT PK_UtRptMemCriDtl PRIMARY KEY CLUSTERED (
RptMemCriDtlId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtRptMemFld') AND type='U')
DROP TABLE dbo.UtRptMemFld
GO
CREATE TABLE UtRptMemFld ( 
RptMemFldId int IDENTITY(1,1) NOT NULL ,
RptMemFldName nvarchar (200) NOT NULL ,
UsrId int NULL ,
InputBy int NULL ,
CompanyLs varchar (1000) NULL ,
AccessCd char (1) NOT NULL CONSTRAINT DF_UtRptMemFld_AccessCd DEFAULT ('V'),
CONSTRAINT PK_UtRptMemFld PRIMARY KEY CLUSTERED (
RptMemFldId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.UtRptTbl') AND type='U')
DROP TABLE dbo.UtRptTbl
GO
CREATE TABLE UtRptTbl ( 
RptTblId int IDENTITY(1,1) NOT NULL ,
RptTblDesc nvarchar (200) NULL ,
ParentId int NULL ,
RptCtrId int NOT NULL ,
ReportId int NULL ,
RptTblTypeCd char (1) NOT NULL ,
TblRepeatNew char (1) NOT NULL ,
TblOrder smallint NULL ,
ColWidth decimal (8,2) NULL ,
TblGrouping int NULL ,
TblVisibility char (1) NULL ,
TblToggle int NULL ,
CONSTRAINT PK_UtRptTbl PRIMARY KEY CLUSTERED (
RptTblId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.WebRule') AND type='U')
DROP TABLE dbo.WebRule
GO
CREATE TABLE WebRule ( 
WebRuleId int IDENTITY(1,1) NOT NULL ,
RuleTypeId tinyint NOT NULL ,
RuleName nvarchar (100) NOT NULL ,
RuleDesc nvarchar (150) NULL ,
RuleDescription nvarchar (500) NULL ,
ScreenId int NOT NULL ,
ScreenObjId int NULL ,
ButtonTypeId tinyint NULL ,
EventId tinyint NOT NULL ,
WebRuleProg nvarchar (max) NOT NULL ,
ReactEventId tinyint NULL ,
ReactRuleProg nvarchar (max) NULL ,
ReduxEventId tinyint NULL ,
ReduxRuleProg nvarchar (max) NULL ,
ServiceEventId tinyint NULL ,
ServiceRuleProg nvarchar (max) NULL ,
AsmxEventId tinyint NULL ,
AsmxRuleProg nvarchar (max) NULL ,
CONSTRAINT PK_WebRule PRIMARY KEY CLUSTERED (
WebRuleId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IU_Wizard')
DROP INDEX Wizard.IU_Wizard 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Wizard') AND type='U')
DROP TABLE dbo.Wizard
GO
CREATE TABLE Wizard ( 
WizardId int IDENTITY(1,1) NOT NULL ,
WizardTypeId tinyint NOT NULL ,
MasterTableId int NOT NULL ,
WizardTitle nvarchar (50) NOT NULL ,
ProgramName varchar (50) NOT NULL ,
DefWorkSheet nvarchar (50) NOT NULL ,
DefStartRow smallint NOT NULL ,
DefOverwrite char (1) NOT NULL ,
OvwrReadonly char (1) NOT NULL ,
NeedRegen char (1) NOT NULL CONSTRAINT DF_Wizard_NeedRegen DEFAULT ('N'),
AuthRequired char (1) NOT NULL CONSTRAINT DF_Wizard_AuthRequired DEFAULT ('Y'),
CONSTRAINT PK_Wizard PRIMARY KEY CLUSTERED (
WizardId
)
)
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.WizardDel') AND type='U')
DROP TABLE dbo.WizardDel
GO
CREATE TABLE WizardDel ( 
WizardDelId int IDENTITY(1,1) NOT NULL ,
WizardId int NOT NULL ,
ProgramName varchar (100) NOT NULL ,
CONSTRAINT PK_WizardDel PRIMARY KEY CLUSTERED (
WizardDelId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_WizardObj_WizardId')
DROP INDEX WizardObj.IX_WizardObj_WizardId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.WizardObj') AND type='U')
DROP TABLE dbo.WizardObj
GO
CREATE TABLE WizardObj ( 
WizardObjId int IDENTITY(1,1) NOT NULL ,
WizardId int NOT NULL ,
ColumnId int NULL ,
ColumnDesc varchar (100) NULL ,
TabIndex smallint NOT NULL ,
CONSTRAINT PK_WizardObj PRIMARY KEY CLUSTERED (
WizardObjId
)
)
GO
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_WizardRule_WizardId')
DROP INDEX WizardRule.IX_WizardRule_WizardId 
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.WizardRule') AND type='U')
DROP TABLE dbo.WizardRule
GO
CREATE TABLE WizardRule ( 
WizardRuleId int IDENTITY(1,1) NOT NULL ,
WizardId int NOT NULL ,
RuleTypeId tinyint NOT NULL ,
RuleName nvarchar (100) NOT NULL ,
RuleDesc nvarchar (150) NULL ,
RuleDescription nvarchar (500) NULL ,
RuleOrder smallint NOT NULL ,
ProcedureName varchar (50) NOT NULL ,
BeforeCRUD char (1) NOT NULL ,
CONSTRAINT PK_WizardRule PRIMARY KEY CLUSTERED (
WizardRuleId
)
)
GO