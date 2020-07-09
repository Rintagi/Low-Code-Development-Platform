IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_AppZip' AND o.name = 'AppZip')
    DROP INDEX AppZip.IX_AppZip 

CREATE INDEX IX_AppZip ON AppZip(MasterId, DocName)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_DbKey_KeyName' AND o.name = 'DbKey')
    DROP INDEX DbKey.IX_DbKey_KeyName 

CREATE  UNIQUE INDEX IX_DbKey_KeyName ON DbKey(KeyName)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IU_DbTable' AND o.name = 'DbTable')
    DROP INDEX DbTable.IU_DbTable 

CREATE  UNIQUE INDEX IU_DbTable ON DbTable(SystemId, TableName)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_Label' AND o.name = 'Label')
    DROP INDEX Label.IX_Label 

CREATE  UNIQUE INDEX IX_Label ON Label(CultureId, LabelCat, LabelKey, CompanyId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IU_MemTrans' AND o.name = 'MemTrans')
    DROP INDEX MemTrans.IU_MemTrans 

CREATE  UNIQUE INDEX IU_MemTrans ON MemTrans(InStr, CultureTypeId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_MenuHlp_MenuId' AND o.name = 'MenuHlp')
    DROP INDEX MenuHlp.IX_MenuHlp_MenuId 

CREATE  UNIQUE INDEX IX_MenuHlp_MenuId ON MenuHlp(CultureId, MenuId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_MsgCenter' AND o.name = 'MsgCenter')
    DROP INDEX MsgCenter.IX_MsgCenter 

CREATE  UNIQUE INDEX IX_MsgCenter ON MsgCenter(CultureId, MsgId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ReportCriHlp_ReportCriId' AND o.name = 'ReportCriHlp')
    DROP INDEX ReportCriHlp.IX_ReportCriHlp_ReportCriId 

CREATE  UNIQUE INDEX IX_ReportCriHlp_ReportCriId ON ReportCriHlp(CultureId, ReportCriId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ReportHlp_ReportId' AND o.name = 'ReportHlp')
    DROP INDEX ReportHlp.IX_ReportHlp_ReportId 

CREATE  UNIQUE INDEX IX_ReportHlp_ReportId ON ReportHlp(CultureId, ReportId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ReportLstCri' AND o.name = 'ReportLstCri')
    DROP INDEX ReportLstCri.IX_ReportLstCri 

CREATE  UNIQUE INDEX IX_ReportLstCri ON ReportLstCri(UsrId, ReportId, ReportCriId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ReportObjHlp_ReportObjId' AND o.name = 'ReportObjHlp')
    DROP INDEX ReportObjHlp.IX_ReportObjHlp_ReportObjId 

CREATE  UNIQUE INDEX IX_ReportObjHlp_ReportObjId ON ReportObjHlp(CultureId, ReportObjId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_RptMemCri' AND o.name = 'RptMemCri')
    DROP INDEX RptMemCri.IX_RptMemCri 

CREATE INDEX IX_RptMemCri ON RptMemCri(UsrId, ReportId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_RptTemplate' AND o.name = 'RptTemplate')
    DROP INDEX RptTemplate.IX_RptTemplate 

CREATE INDEX IX_RptTemplate ON RptTemplate(MasterId, DocName)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ScreenCriHlp_ScreenCriId' AND o.name = 'ScreenCriHlp')
    DROP INDEX ScreenCriHlp.IX_ScreenCriHlp_ScreenCriId 

CREATE  UNIQUE INDEX IX_ScreenCriHlp_ScreenCriId ON ScreenCriHlp(CultureId, ScreenCriId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ScreenFilter_Id_Name' AND o.name = 'ScreenFilter')
    DROP INDEX ScreenFilter.IX_ScreenFilter_Id_Name 

CREATE  UNIQUE INDEX IX_ScreenFilter_Id_Name ON ScreenFilter(ScreenId, ScreenFilterName)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ScreenFilterHlp_FilterId' AND o.name = 'ScreenFilterHlp')
    DROP INDEX ScreenFilterHlp.IX_ScreenFilterHlp_FilterId 

CREATE  UNIQUE INDEX IX_ScreenFilterHlp_FilterId ON ScreenFilterHlp(CultureId, ScreenFilterId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ScreenLstCri' AND o.name = 'ScreenLstCri')
    DROP INDEX ScreenLstCri.IX_ScreenLstCri 

CREATE  UNIQUE INDEX IX_ScreenLstCri ON ScreenLstCri(UsrId, ScreenId, ScreenCriId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ScreenLstInf' AND o.name = 'ScreenLstInf')
    DROP INDEX ScreenLstInf.IX_ScreenLstInf 

CREATE INDEX IX_ScreenLstInf ON ScreenLstInf(UsrId, ScreenId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ScreenTabHlp_ScreenTabId' AND o.name = 'ScreenTabHlp')
    DROP INDEX ScreenTabHlp.IX_ScreenTabHlp_ScreenTabId 

CREATE  UNIQUE INDEX IX_ScreenTabHlp_ScreenTabId ON ScreenTabHlp(CultureId, ScreenTabId)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_ServerRule' AND o.name = 'ServerRule')
    DROP INDEX ServerRule.IX_ServerRule 

CREATE INDEX IX_ServerRule ON ServerRule(ScreenId, RuleOrder)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_Template_TmplDefault' AND o.name = 'Template')
    DROP INDEX Template.IX_Template_TmplDefault 

CREATE  UNIQUE INDEX IX_Template_TmplDefault ON Template(TmplDefault DESC, TemplateName)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_Usage_UsrId' AND o.name = 'Usage')
    DROP INDEX Usage.IX_Usage_UsrId 

CREATE INDEX IX_Usage_UsrId ON Usage(UsrId, EntityTitle)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_UtReport_ProgramName' AND o.name = 'UtReport')
    DROP INDEX UtReport.IX_UtReport_ProgramName 

CREATE  UNIQUE INDEX IX_UtReport_ProgramName ON UtReport(ProgramName)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IU_Wizard' AND o.name = 'Wizard')
    DROP INDEX Wizard.IU_Wizard 

CREATE  UNIQUE INDEX IU_Wizard ON Wizard(ProgramName)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_WizardObj_WizardId' AND o.name = 'WizardObj')
    DROP INDEX WizardObj.IX_WizardObj_WizardId 

CREATE INDEX IX_WizardObj_WizardId ON WizardObj(WizardId, TabIndex)
GO

IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_WizardRule_WizardId' AND o.name = 'WizardRule')
    DROP INDEX WizardRule.IX_WizardRule_WizardId 

CREATE  UNIQUE INDEX IX_WizardRule_WizardId ON WizardRule(WizardId, RuleOrder)
GO