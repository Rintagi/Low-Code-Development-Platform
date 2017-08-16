<%@ WebService Language="C#" Class="AdminWs" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;

// Need to run the following 3 lines at Windows SDK v6.1: CMD manually if AdminWs.asmx is changed:
// C:\
// CD\Rintagi\RO\Service3
// "C:\Program Files\Microsoft SDKs\Windows\v6.1\Bin\wsdl.exe" /nologo /namespace:RO.Service3 /out:"AdminWs.cs" "http://RND08/ROWs/AdminWs.asmx"

[WebService(Namespace = "http://Rintagi.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class AdminWs : WebService
{
    // For screens:

    [WebMethod]
    public string GetLastPageInfo(Int32 screenId, Int32 usrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetLastPageInfo(screenId, usrId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void UpdLastPageInfo(Int32 screenId, Int32 usrId, string lastPageInfo, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).UpdLastPageInfo(screenId, usrId, lastPageInfo, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetLastCriteria(Int32 rowsExpected, Int32 screenId, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetLastCriteria(rowsExpected, screenId, reportId, usrId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void DelDshFldDtl(string DshFldDtlId, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).DelDshFldDtl(DshFldDtlId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public void DelDshFld(string DshFldId, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).DelDshFld(DshFldId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string UpdDshFld(string PublicAccess, string DshFldId, string DshFldName, Int32 usrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).UpdDshFld(PublicAccess, DshFldId, DshFldName, usrId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetSchemaScrImp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetSchemaScrImp(screenId, cultureId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetButtonHlp(Int32 screenId, Int32 reportId, Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetButtonHlp(screenId, reportId, wizardId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetClientRule(Int32 screenId, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetClientRule(screenId, reportId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenHlp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenHlp(screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetGlobalFilter(Int32 usrId, Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetGlobalFilter(usrId, screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenFilter(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenFilter(screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenTab(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenTab(screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenCriHlp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenCriHlp(screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void LogUsage(Int32 UsrId, string UsageNote, string EntityTitle, Int32 ScreenId, Int32 ReportId, Int32 WizardId, string Miscellaneous, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).LogUsage(UsrId, UsageNote, EntityTitle, ScreenId, ReportId, WizardId, Miscellaneous, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetInfoByCol(Int32 ScreenId, string ColumnName, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetInfoByCol(ScreenId, ColumnName, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public bool IsValidOvride(string cr, Int32 usrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).IsValidOvride(cr.XmlToObject<Credential>(), usrId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetMsg(string Msg, Int16 CultureId, string TechnicalUsr, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetMsg(Msg, CultureId, TechnicalUsr, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetLabel(Int16 CultureId, string LabelCat, string LabelKey, string CompanyId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetLabel(CultureId, LabelCat, LabelKey, CompanyId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetLabels(Int16 CultureId, string LabelCat, string CompanyId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetLabels(CultureId, LabelCat, CompanyId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetFormat()
    {
        return (new AdminSystem()).GetFormat().DataTableToXml();
    }

    [WebMethod]
    public string GetScrCriteria(string screenId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScrCriteria(screenId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetCriScreenGrp(string screenId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetCriScreenGrp(screenId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void MkGetScreenIn(string screenId, string screenCriId, string procedureName, string appDatabase, string sysDatabase, string multiDesignDb, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).MkGetScreenIn(screenId, screenCriId, procedureName, appDatabase, sysDatabase, multiDesignDb, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetScreenIn(string screenId, string procedureName, string RequiredValid, int topN, string FilterTxt, string ui, string uc, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenIn(screenId, procedureName, RequiredValid, topN, FilterTxt, ui.XmlToObject<UsrImpr>(), uc.XmlToObject<UsrCurr>(), dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void UpdScrCriteria(string screenId, string programName, string dvCri, Int32 usrId, bool isCriVisible, string ds, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).UpdScrCriteria(screenId, programName, XmlUtils.XmlToDataView(dvCri), usrId, isCriVisible, XmlUtils.XmlToDataSet(ds), dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string EncryptString(string inStr)
    {
        return (new AdminSystem()).EncryptString(inStr);
    }

	[WebMethod]
	public string GetAuthRow(Int32 ScreenId, string RowAuthoritys, string dbConnectionString, string dbPassword)
	{
		return (new AdminSystem()).GetAuthRow(ScreenId, RowAuthoritys, dbConnectionString, dbPassword).DataTableToXml();
	}

	[WebMethod]
	public string GetAuthCol(Int32 ScreenId, string ui, string uc, string dbConnectionString, string dbPassword)
	{
		return (new AdminSystem()).GetAuthCol(ScreenId, ui.XmlToObject<UsrImpr>(), uc.XmlToObject<UsrCurr>(), dbConnectionString, dbPassword).DataTableToXml();
	}

	[WebMethod]
	public string GetAuthExp(Int32 ScreenId, Int16 CultureId, string ui, string uc, string dbConnectionString, string dbPassword)
	{
		return (new AdminSystem()).GetAuthExp(ScreenId, CultureId, ui.XmlToObject<UsrImpr>(), uc.XmlToObject<UsrCurr>(), dbConnectionString, dbPassword).DataTableToXml();
	}

	[WebMethod]
	public string GetScreenLabel(Int32 ScreenId, Int16 CultureId, string ui, string uc, string dbConnectionString, string dbPassword)
	{
		return (new AdminSystem()).GetScreenLabel(ScreenId, CultureId, ui.XmlToObject<UsrImpr>(), uc.XmlToObject<UsrCurr>(), dbConnectionString, dbPassword).DataTableToXml();
	}

    // For reports:

    [WebMethod]
    public string GetPrinterList(string UsrGroups, string Members)
    {
        return (new AdminSystem()).GetPrinterList(UsrGroups, Members).DataTableToXml();
    }

    [WebMethod]
    public void UpdLastCriteria(Int32 screenId, Int32 reportId, Int32 usrId, Int32 criId, string lastCriteria, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).UpdLastCriteria(screenId, reportId, usrId, criId, lastCriteria, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetReportHlp(Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetReportHlp(reportId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetReportCriHlp(Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetReportCriHlp(reportId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetReportSct()
    {
        return (new AdminSystem()).GetReportSct().DataTableToXml();
    }

    [WebMethod]
    public string GetReportItem(Int32 ReportId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetReportItem(ReportId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetRptPwd(string pwd)
    {
        return (new AdminSystem()).GetRptPwd(pwd);
    }

    [WebMethod]
    public string GetQueryStr(string QueryStrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetQueryStr(QueryStrId, dbConnectionString, dbPassword);
    }

    // For Wizards:

    [WebMethod]
    public string GetSchemaWizImp(Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetSchemaWizImp(wizardId, cultureId, dbConnectionString, dbPassword);
    }
}