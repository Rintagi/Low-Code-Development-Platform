namespace RO.Rule3
{
	using System;
	using System.Data;
	using System.IO;
	using System.Text;
	using System.Text.RegularExpressions;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.SystemFramewk;
	using RO.Access3;

	public class GenWizardsRules
	{
		public bool DeleteProgram(string programName, Int32 wizardId, string appDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar)
		{
			try
			{
				// Delete source client tier programs:
				DeleteProgC(programName, CPrj, CPrj.SrcClientProgramPath, CPrj.SrcClientFrwork);
				// Delete target client tier programs:
				if (CPrj.SrcClientProgramPath != CPrj.TarClientProgramPath)
				{
					DeleteProgC(programName, CPrj, CPrj.TarClientProgramPath, CPrj.TarClientFrwork);
				}
				// Delete source rule tier programs:
				//DeleteProgR(programName, CPrj, CSrc, CPrj.SrcRuleProgramPath, CPrj.SrcRuleFrwork);
				// Delete target rule tier programs:
                //if (CPrj.SrcRuleProgramPath != CPrj.TarRuleProgramPath)
                //{
                //    DeleteProgR(programName, CPrj, CSrc, CPrj.TarRuleProgramPath, CPrj.TarRuleFrwork);
                //}
				// Delete source data tier programs:
				DeleteProgD(CSrc.SrcDbDatabase, appDatabase, CPrj.SrcDesDatabase, programName, CSrc.SrcConnectionString, CSrc.SrcDbPassword);
				// Delete target data tier programs:
				if (CTar.TarDbServer != CSrc.SrcDbServer && CPrj.SrcDesProviderCd == "M")
				{
					DeleteProgD(CTar.TarDbDatabase, appDatabase, CPrj.TarDesDatabase, programName, CTar.TarConnectionString, CTar.TarDbPassword);
				}
			}
			catch (Exception e) {ApplicationAssert.CheckCondition(false,"","",e.Message); return false;}
			return true;
		}

		private void DeleteProgC(string programName, CurrPrj CPrj, string clientProgramPath, string clientFrwork)
		{
			if (clientFrwork == "1")
			{
				Robot.ModifyCsproj(true, clientProgramPath + @"Web.csproj", programName + ".aspx", clientFrwork, string.Empty);
				Robot.ModifyCsproj(true, clientProgramPath + @"Web.csproj", programName + ".aspx.cs", clientFrwork, string.Empty);
				Robot.ModifyCsproj(true, clientProgramPath + @"Web.csproj", @"modules\" + programName + "Module.ascx", clientFrwork, string.Empty);
				Robot.ModifyCsproj(true, clientProgramPath + @"Web.csproj", @"modules\" + programName + "Module.ascx.cs", clientFrwork, string.Empty);
			}
			FileInfo fi = null;
			fi = new FileInfo(clientProgramPath + programName + ".aspx"); if (fi.Exists) {fi.Delete();}
			fi = new FileInfo(clientProgramPath + programName + ".aspx.cs"); if (fi.Exists) {fi.Delete();}
			fi = new FileInfo(clientProgramPath + @"modules\" + programName + "Module.ascx"); if (fi.Exists) {fi.Delete();}
			fi = new FileInfo(clientProgramPath + @"modules\" + programName + "Module.ascx.cs"); if (fi.Exists) {fi.Delete();}
		}

        //private void DeleteProgR(string programName, CurrPrj CPrj, CurrSrc CSrc, string ruleProgramPath, string ruleFrwork)
        //{
        //    Robot.ModifyCsproj(true, ruleProgramPath + "Facade" + CSrc.SrcSystemId.ToString() + "\\Facade" + CSrc.SrcSystemId.ToString() + ".csproj", programName + "System.cs", string.Empty, ruleFrwork);
        //    Robot.ModifyCsproj(true, ruleProgramPath + "Rule" + CSrc.SrcSystemId.ToString() + "\\Rule" + CSrc.SrcSystemId.ToString() + ".csproj", programName + "Rules.cs", string.Empty, ruleFrwork);
        //    Robot.ModifyCsproj(true, ruleProgramPath + "Access" + CSrc.SrcSystemId.ToString() + "\\Access" + CSrc.SrcSystemId.ToString() + ".csproj", programName + "Access.cs", string.Empty, ruleFrwork);
        //    FileInfo fi = null;
        //    fi = new FileInfo(ruleProgramPath + "Facade" + CSrc.SrcSystemId.ToString() + "\\" + programName + "System.cs"); if (fi.Exists) {fi.Delete();}
        //    fi = new FileInfo(ruleProgramPath + "Rule" + CSrc.SrcSystemId.ToString() + "\\" + programName + "Rules.cs"); if (fi.Exists) {fi.Delete();}
        //    fi = new FileInfo(ruleProgramPath + "Access" + CSrc.SrcSystemId.ToString() + "\\" + programName + "Access.cs"); if (fi.Exists) {fi.Delete();}
        //}

		private void DeleteProgD(string dbDatabase, string appDatabase, string desDatabase, string programName, string dbConnectionString, string dbPassword)
		{
			using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
			{
				dac.DelWizardDel(dbDatabase, appDatabase, desDatabase, programName, dbConnectionString, dbPassword);
			}
		}

		public bool CreateProgram(Int32 wizardId, string wizardTitle, string dbAppDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
			DataTable dt = null;
			using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
			{
				dt = dac.GetWizardById(wizardId, CPrj, CSrc);
			}
			DataView dv = null;
			using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
			{
				dv = new DataView(dac.GetWizardColumns(wizardId, CPrj, CSrc));
			}
			DataView dvRule = null;
			using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
			{
				dvRule = new DataView(dac.GetWizardRule(wizardId, CPrj, CSrc));
			}
			try
			{
				// Create source rule tier programs before CreateProgW:
				//CreateProgR(dt.Rows[0], wizardId, dv, dvRule, CPrj, CSrc, CPrj.SrcRuleProgramPath, CPrj.SrcRuleFrwork);
                // Create target rule tier programs:
                //if (CPrj.SrcRuleProgramPath != CPrj.TarRuleProgramPath)
                //{
                //    CreateProgR(dt.Rows[0], wizardId, dv, dvRule, CPrj, CSrc, CPrj.TarRuleProgramPath, CPrj.TarRuleFrwork);
                //}
				// Create source Web Service tier programs:
				//CreateProgW(dt.Rows[0], wizardId, CPrj, CSrc);
				// Create source client tier programs:
                CreateProgC(dt.Rows[0], wizardId, dv, wizardTitle, CPrj, CSrc, CPrj.SrcClientProgramPath, CPrj.SrcClientFrwork);
				// Create target client tier programs:
				if (CPrj.SrcClientProgramPath != CPrj.TarClientProgramPath)
				{
                    CreateProgC(dt.Rows[0], wizardId, dv, wizardTitle, CPrj, CSrc, CPrj.TarClientProgramPath, CPrj.TarClientFrwork);
				}
				// Create source data tier programs:
				using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
				{
					dac.MkWizardW1Upd(wizardId, dt.Rows[0]["ProgramName"].ToString(), CSrc, dt.Rows[0]["dbAppDatabase"].ToString(), dt.Rows[0]["dbDesDatabase"].ToString());
				}
				// Create target data tier programs:
				if (CTar.TarDbServer != CSrc.SrcDbServer && CPrj.SrcDesProviderCd == "M")
				{
					CreateProgD(dt.Rows[0], dbAppDatabase, dvRule, CPrj, CSrc, CTar, dbConnectionString, dbPassword);
				}
                // Reset regen flag to No:
                using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
                {
                    dac.SetWizNeedRegen(wizardId, CSrc);
                }
            }
			catch (Exception e) {ApplicationAssert.CheckCondition(false,"","",e.Message); return false;}
			return true;
		}

        //public void ProxyProgram(Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    DataTable dt = null;
        //    using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
        //    {
        //        dt = dac.GetWizardById(wizardId, CPrj, CSrc);
        //    }
        //    StreamWriter sw = new StreamWriter(CPrj.SrcRuleProgramPath + "Service" + CSrc.SrcSystemId.ToString() + "\\" + dt.Rows[0]["ProgramName"].ToString() + "Ws.cs");
        //    try { sw.Write(Robot.MkWsProxy(dt.Rows[0]["ProgramName"].ToString(), CPrj, CSrc)); }
        //    catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message); }
        //    finally { sw.Close(); }
        //    Robot.ModifyCsproj(false, CPrj.SrcRuleProgramPath + "Service" + CSrc.SrcSystemId.ToString() + "\\Service" + CSrc.SrcSystemId.ToString() + ".csproj", dt.Rows[0]["ProgramName"].ToString() + "Ws.cs", string.Empty, CPrj.SrcClientFrwork);
        //}

        //private void CreateProgW(DataRow dw, Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    StreamWriter sw = new StreamWriter(CPrj.SrcWsProgramPath + dw["ProgramName"].ToString() + "Ws.asmx");
        //    try { sw.Write(MakeAsmx(dw, wizardId, CPrj, CSrc)); }
        //    finally { sw.Close(); }
        //}

		private void CreateProgC(DataRow dw, Int32 wizardId, DataView dv, string wizardTitle, CurrPrj CPrj, CurrSrc CSrc, string clientProgramPath, string clientFrwork)
		{
			StreamWriter sw = new StreamWriter(clientProgramPath + dw["ProgramName"].ToString() + ".aspx");
			try {sw.Write(MakeAspx(dw, wizardId, wizardTitle, CPrj, clientFrwork));} finally {sw.Close();}
			if (clientFrwork == "1")
			{
				Robot.ModifyCsproj(false, clientProgramPath + @"Web.csproj", dw["ProgramName"].ToString() + ".aspx", clientFrwork, string.Empty);
			}
			sw = new StreamWriter(clientProgramPath + dw["ProgramName"].ToString() + ".aspx.cs");
			try {sw.Write(MakeAspxCs(dw, wizardTitle, CPrj, clientFrwork));} finally {sw.Close();}
			if (clientFrwork == "1")
			{
				Robot.ModifyCsproj(false, clientProgramPath + @"Web.csproj", dw["ProgramName"].ToString() + ".aspx.cs", clientFrwork, string.Empty);
			}
			sw = new StreamWriter(clientProgramPath + @"modules\" + dw["ProgramName"].ToString() + "Module.ascx");
			try {sw.Write(MakeAscx(dw, CPrj, clientFrwork));} finally {sw.Close();}
			if (clientFrwork == "1")
			{
				Robot.ModifyCsproj(false, clientProgramPath + @"Web.csproj", @"modules\" + dw["ProgramName"].ToString() + "Module.ascx", clientFrwork, string.Empty);
			}
			sw = new StreamWriter(clientProgramPath + @"modules\" + dw["ProgramName"].ToString() + "Module.ascx.cs");
            try 
            {
                sw.Write(MakeAscxCs(dw, dv, wizardId, CSrc.SrcSystemId, wizardTitle, CPrj, CSrc, clientFrwork)); 
            }
            finally { sw.Close(); }
			if (clientFrwork == "1")
			{
				Robot.ModifyCsproj(false, clientProgramPath + @"Web.csproj", @"modules\" + dw["ProgramName"].ToString() + "Module.ascx.cs", clientFrwork, string.Empty);
			}
		}

        //private void CreateProgR(DataRow dw, Int32 wizardId, DataView dv, DataView dvRule, CurrPrj CPrj, CurrSrc CSrc, string ruleProgramPath, string ruleFrwork)
        //{
        //    StreamWriter sw = new StreamWriter(ruleProgramPath + "Facade" + CSrc.SrcSystemId.ToString() + "\\" + dw["ProgramName"].ToString() + "System.cs");
        //    try {sw.Write(MakeSystemCs(dw, wizardId, CPrj, CSrc));} finally {sw.Close();}
        //    Robot.ModifyCsproj(false, ruleProgramPath + "Facade" + CSrc.SrcSystemId.ToString() + "\\Facade" + CSrc.SrcSystemId.ToString() + ".csproj", dw["ProgramName"].ToString() + "System.cs", string.Empty, ruleFrwork);
        //    sw = new StreamWriter(ruleProgramPath + "Rule" + CSrc.SrcSystemId.ToString() + "\\" + dw["ProgramName"].ToString() + "Rules.cs");
        //    try {sw.Write(MakeRulesCs(dw, wizardId, dv, CPrj, CSrc));} finally {sw.Close();}
        //    Robot.ModifyCsproj(false, ruleProgramPath + "Rule" + CSrc.SrcSystemId.ToString() + "\\Rule" + CSrc.SrcSystemId.ToString() + ".csproj", dw["ProgramName"].ToString() + "Rules.cs", string.Empty, ruleFrwork);
        //    sw = new StreamWriter(ruleProgramPath + "Access" + CSrc.SrcSystemId.ToString() + "\\" + dw["ProgramName"].ToString() + "Access.cs");
        //    try {sw.Write(MakeAccessCs(dw, wizardId, dv, dvRule, dw["dbAppDatabase"].ToString(), CPrj, CSrc));} finally {sw.Close();}
        //    Robot.ModifyCsproj(false, ruleProgramPath + "Access" + CSrc.SrcSystemId.ToString() + "\\Access" + CSrc.SrcSystemId.ToString() + ".csproj", dw["ProgramName"].ToString() + "Access.cs", string.Empty, ruleFrwork);
        //}

		public void CreateProgD(DataRow dw, string dbAppDatabase, DataView dvRule, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
			PortProg(dbAppDatabase, "Wiz" + dw["ProgramName"].ToString(), "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);
			foreach (DataRowView drv in dvRule)
			{
				PortProg(dbAppDatabase, drv["ProcedureName"].ToString(), "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);
			}
		}

		private void PortProg(string dbAppDatabase, string SpName, string SpType, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
			string db = CSrc.SrcDbDatabase;		// Save for later restore.
			CSrc.SrcDbDatabase = dbAppDatabase; CTar.TarDbDatabase = dbAppDatabase;
			DbScript ds = new DbScript(string.Empty, false);
			DbPorting pt = new DbPorting();
			string ss = ds.ScriptCreaSp(SpName, SpType, CPrj.SrcDesProviderCd, true, CSrc, CTar);
			if (ss != string.Empty)
			{
				if (CPrj.TarDesProviderCd == "S") {ss = pt.SqlToSybase (CPrj.EntityId,CPrj.TarDesDatabase,ss,dbConnectionString,dbPassword);}
				using (Access3.RobotAccess dac = new Access3.RobotAccess())
				{
					dac.ExecSql(ds.ScriptDropSp(SpName, SpType), CTar.TarConnectionString, CTar.TarDbPassword);
				}
				using (Access3.RobotAccess dac = new Access3.RobotAccess())
				{
					dac.ExecSql("SET QUOTED_IDENTIFIER ON", CTar.TarConnectionString, CTar.TarDbPassword);
				}
				using (Access3.RobotAccess dac = new Access3.RobotAccess())
				{
					if (CPrj.TarDesProviderCd == "S")
					{
						dac.ExecSql("SET ANSINULL ON", CTar.TarConnectionString, CTar.TarDbPassword);
					}
					else
					{
						dac.ExecSql("SET ANSI_NULLS ON", CTar.TarConnectionString, CTar.TarDbPassword);
					}
				}
				using (Access3.RobotAccess dac = new Access3.RobotAccess())
				{
					dac.ExecSql(ss, CTar.TarConnectionString, CTar.TarDbPassword);
				}
				using (Access3.RobotAccess dac = new Access3.RobotAccess())
				{
					dac.ExecSql("SET QUOTED_IDENTIFIER OFF", CTar.TarConnectionString, CTar.TarDbPassword);
				}
			}
			CSrc.SrcDbDatabase = db; CTar.TarDbDatabase = db;
		}

        //private StringBuilder MakeAsmx(DataRow dw, Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<%@ WebService language=\"c#\" Class=\"" + dw["ProgramName"].ToString() + "Ws\" %>" + Environment.NewLine);
        //    sb.Append("using System;" + Environment.NewLine);
        //    sb.Append("using System.Data;" + Environment.NewLine);
        //    sb.Append("using System.Web;" + Environment.NewLine);
        //    sb.Append("using System.Web.Services;" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Facade" + CSrc.SrcSystemId.ToString() + ";" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ";" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ".Data;" + Environment.NewLine);
        //    if (CSrc.SrcSystemId != 3)	// Admin.
        //    {
        //        sb.Append("using RO.Facade3;" + Environment.NewLine);
        //        sb.Append("using RO.Common3;" + Environment.NewLine);
        //        sb.Append("using RO.Common3.Data;" + Environment.NewLine);
        //    }
        //    sb.Append(Environment.NewLine);
        //    sb.Append("[WebService(Namespace = \"http://Rintagi.com/\")]" + Environment.NewLine);
        //    sb.Append("[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]" + Environment.NewLine);
        //    sb.Append("public partial class " + dw["ProgramName"].ToString() + "Ws: WebService" + Environment.NewLine);
        //    sb.Append("{" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("	[WebMethod]" + Environment.NewLine);
        //    sb.Append("	public string GetFileList" + wizardId.ToString() + "(string fileFolder)" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    sb.Append("		return XmlUtils.DataTableToXml((new " + dw["ProgramName"].ToString() + "System()).GetFileList" + wizardId.ToString() + "(fileFolder));" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("	[WebMethod]" + Environment.NewLine);
        //    sb.Append("	public int ImportFile" + wizardId.ToString() + "(bool bOverwrite, Int32 usrId, string fileName, string workSheet, string startRow, string fileFullName" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    sb.Append("		return (new " + dw["ProgramName"].ToString() + "System()).ImportFile" + wizardId.ToString() + "(bOverwrite,usrId,fileName,workSheet,startRow,fileFullName" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("	[WebMethod]" + Environment.NewLine);
        //    sb.Append("	public int ImportRows" + wizardId.ToString() + "(bool bOverwrite, Int32 usrId, string ds, int iStart, string fileName" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    sb.Append("		return (new " + dw["ProgramName"].ToString() + "System()).ImportRows" + wizardId.ToString() + "(bOverwrite,usrId,XmlUtils.XmlToDataSet(ds),iStart,fileName" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append("}" + Environment.NewLine);
        //    return sb;
        //}

        private StringBuilder MakeAspx(DataRow dw, Int32 wizardId, string wizardTitle, CurrPrj CPrj, string clientFrwork)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<%@ Page language=\"c#\" MasterPageFile=\"Default.master\" EnableEventValidation=\"false\"");
			sb.Append(" Inherits=\"" + CPrj.EntityCode + ".Web." + dw["ProgramName"].ToString() + "\" CodeFile=\"" + dw["ProgramName"].ToString() + ".aspx.cs\" Title=\"" + wizardTitle + "\" %>" + Environment.NewLine);
			sb.Append("<%@ Register TagPrefix=\"Module\" TagName=\"" + dw["ProgramName"].ToString() + "\" Src=\"modules/" + dw["ProgramName"].ToString() + "Module.ascx\" %>" + Environment.NewLine);
            sb.Append("<asp:Content ContentPlaceHolderID=\"MHR\" Runat=\"Server\"><Module:" + dw["ProgramName"].ToString() + " id=\"M" + wizardId.ToString() + "\" runat=\"server\" /></asp:Content>" + Environment.NewLine);
			return sb;
		}

		private StringBuilder MakeAspxCs(DataRow dw, string wizardTitle, CurrPrj CPrj, string clientFrwork)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("using System;" + Environment.NewLine);
			sb.Append("using System.Collections;" + Environment.NewLine);
			sb.Append("using System.ComponentModel;" + Environment.NewLine);
			sb.Append("using System.Data;" + Environment.NewLine);
			sb.Append("using System.Drawing;" + Environment.NewLine);
			sb.Append("using System.Web;" + Environment.NewLine);
			sb.Append("using System.Web.SessionState;" + Environment.NewLine);
			sb.Append("using System.Web.UI;" + Environment.NewLine);
			sb.Append("using System.Web.UI.WebControls;" + Environment.NewLine);
			sb.Append("using System.Web.UI.HtmlControls;" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("namespace " + CPrj.EntityCode + ".Web" + Environment.NewLine);
			sb.Append("{" + Environment.NewLine);
			sb.Append("	public partial class " + dw["ProgramName"].ToString() + " : RO.Web.PageBase" + Environment.NewLine);
			sb.Append("	{" + Environment.NewLine);
			sb.Append("		public " + dw["ProgramName"].ToString() + "()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
            sb.Append("			Page.PreInit += new System.EventHandler(Page_PreInit);" + Environment.NewLine);
            sb.Append("			Page.Init += new System.EventHandler(Page_Init);" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		protected void Page_Load(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
            sb.Append("		protected void Page_PreInit(object sender, EventArgs e)" + Environment.NewLine);
            sb.Append("		{" + Environment.NewLine);
            sb.Append("			SetMasterPage();" + Environment.NewLine);
            sb.Append("		}" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("		protected void Page_Init(object sender, EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			InitializeComponent();" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		#region Web Form Designer generated code" + Environment.NewLine);
			sb.Append("		/// <summary>" + Environment.NewLine);
			sb.Append("		/// Required method for Designer support - do not modify" + Environment.NewLine);
			sb.Append("		/// the contents of this method with the code editor." + Environment.NewLine);
			sb.Append("		/// </summary>" + Environment.NewLine);
			sb.Append("		private void InitializeComponent()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append("		#endregion" + Environment.NewLine);
			sb.Append("	}" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
			return sb;
		}

		private StringBuilder MakeAscx(DataRow dw, CurrPrj CPrj, string clientFrwork)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<%@ Control Language=\"c#\" Inherits=\"" + CPrj.EntityCode + ".Web." + dw["ProgramName"].ToString() + "Module\" CodeFile=\"" + dw["ProgramName"].ToString() + "Module.ascx.cs\" CodeFileBaseClass=\"RO.Web.ModuleBase\" %>" + Environment.NewLine);
            sb.Append("<script type=\"text/javascript\" lang=\"javascript\">" + Environment.NewLine);
            sb.Append("    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)" + Environment.NewLine);
            sb.Append("    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)" + Environment.NewLine);
            sb.Append("    function beginRequestHandler() { ShowProgress(); document.body.style.cursor = 'wait'; }" + Environment.NewLine);
            sb.Append("    function endRequestHandler() { HideProgress(); document.body.style.cursor = 'auto'; }" + Environment.NewLine);
            sb.Append("</script>" + Environment.NewLine);
            sb.Append("<div id=\"AjaxSpinner\" class=\"AjaxSpinner\" style=\"display:none;\">" + Environment.NewLine);
            sb.Append("	<div style=\"padding:10px;\">" + Environment.NewLine);
            sb.Append("		<img alt=\"\" src=\"images/indicator.gif\" />&nbsp;<asp:Label ID=\"AjaxSpinnerLabel\" Text=\"This may take a moment...\" runat=\"server\" />" + Environment.NewLine);
            sb.Append("	</div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("<div class=\"r-table wizard-header\">" + Environment.NewLine);
            sb.Append("<div class=\"r-tr\">" + Environment.NewLine);
            sb.Append("    <div class=\"r-td rc-1-6\">" + Environment.NewLine);
            sb.Append("        <div class=\"wizard-title\"><asp:label id=\"cTitleLabel\" runat=\"server\" /></div>" + Environment.NewLine);
            sb.Append("    </div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("<div class=\"r-table\">" + Environment.NewLine);
            sb.Append("<div class=\"r-tr\">" + Environment.NewLine);
            sb.Append("    <div class=\"r-td rc-1-12\">" + Environment.NewLine);
            sb.Append("        <div class=\"wizard-help\">" + Environment.NewLine);
            sb.Append("            <asp:label id=\"cHelpLabel\" runat=\"server\" />" + Environment.NewLine);
            sb.Append("        </div>" + Environment.NewLine);
            sb.Append("    </div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("<asp:UpdatePanel ID=\"PanelTop\" UpdateMode=\"Conditional\" runat=\"server\"><Triggers><asp:PostBackTrigger ControlID=\"cBrowseButton\" /></Triggers><ContentTemplate>" + Environment.NewLine);
            sb.Append("<div class=\"r-table\">" + Environment.NewLine);
            sb.Append("<div class=\"r-tr\">" + Environment.NewLine);
            sb.Append("    <div class=\"r-td rc-1-4\">" + Environment.NewLine);
            sb.Append("        <div class=\"wizard-image\">" + Environment.NewLine);
            sb.Append("            <div style=\"float:right;\"><img src=\"./images/wizard/report-dsk.jpg\" class=\"wizard-image-dsk\" style=\"max-width:200px\" /></div>" + Environment.NewLine);
            sb.Append("            <div style=\"float:none;\"><img src=\"./images/wizard/report-mob.jpg\" class=\"wizard-image-mob\" style=\"max-width:500px\" /></div>" + Environment.NewLine);
            sb.Append("            <div style=\"clear:both;\"></div>" + Environment.NewLine);
            sb.Append("        </div>" + Environment.NewLine);
            sb.Append("    </div>" + Environment.NewLine);
            sb.Append("    <div class=\"r-td rc-5-12\">" + Environment.NewLine);
            sb.Append("        <div class=\"wizard-content\">" + Environment.NewLine);
            sb.Append("        <table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" + Environment.NewLine);
            sb.Append("        <tr>" + Environment.NewLine);
            sb.Append("            <td colspan=\"3\"><h3>Server Location:</h3></td>" + Environment.NewLine);
            sb.Append("        </tr>" + Environment.NewLine);
            sb.Append("        <tr>" + Environment.NewLine);
            sb.Append("            <td colspan=\"2\"><asp:CheckBox id=\"cAllFile\" CssClass=\"inp-chk\" runat=\"server\" AutoPostBack=\"true\" onCheckedChanged=\"cAllFile_CheckedChanged\" ToolTip=\"Check here to prepare all spreadsheets for import.\" /><span>All SpreadSheets</span></td>" + Environment.NewLine);
            sb.Append("            <td align=\"right\"><span>WorkSheets:</span><asp:TextBox id=\"cWorkSheetM\" CssClass=\"inp-txt\" Text=\"" + dw["DefWorkSheet"].ToString() + "\" runat=\"server\" Style=\"max-width:200px;\" ToolTip=\"Please enter a worksheet name or if an integer is entered instead, it could be interpreted as either worksheet order or name. May not function properly on Excel-97 spreadsheet.\" /></td>" + Environment.NewLine);
            sb.Append("        </tr>" + Environment.NewLine);
            sb.Append("        <tr>" + Environment.NewLine);
            sb.Append("            <td colspan=\"3\"><asp:textbox id=\"cLocation\" CssClass=\"inp-txt\" runat=\"server\" width=\"100%\" /></td>" + Environment.NewLine);
            sb.Append("            <td><asp:Button id=\"cListButton\" onclick=\"cListButton_Click\" runat=\"server\" CausesValidation=\"false\" /></td>" + Environment.NewLine);
            sb.Append("        </tr>" + Environment.NewLine);
            sb.Append("        <tr>" + Environment.NewLine);
            sb.Append("            <td colspan=\"3\"><asp:textbox id=\"cSearch\" CssClass=\"inp-txt\" runat=\"server\" width=\"100%\" MaxLength=\"25\" /></td>" + Environment.NewLine);
            sb.Append("            <td><asp:Button id=\"cSearchButton\" onclick=\"cSearchButton_Click\" runat=\"server\" CausesValidation=\"false\" /></td>" + Environment.NewLine);
            sb.Append("        </tr>" + Environment.NewLine);
            sb.Append("        <tr>" + Environment.NewLine);
            sb.Append("            <td colspan=\"3\"><asp:listbox id=\"cFileList\" CssClass=\"inp-pic\" SelectionMode=\"Multiple\" runat=\"server\" AutoPostBack=\"true\" onSelectedIndexChanged=\"cFileList_SelectedIndexChanged\" rows=\"8\" width=\"100%\" DataValueField=\"FileName\" DataTextField=\"FileFullName\" /></td>" + Environment.NewLine);
            sb.Append("        </tr>" + Environment.NewLine);
            sb.Append("        <tr>" + Environment.NewLine);
            sb.Append("            <td colspan=\"2\"><h3>Local Location:</h3></td>" + Environment.NewLine);
            sb.Append("            <td align=\"right\"><span>StartRow:</span><asp:TextBox id=\"cStartRow\" CssClass=\"inp-ctr\" Text=\"" + dw["DefStartRow"].ToString() + "\" runat=\"server\" Style=\"max-width:30px;\" ToolTip=\"Please enter the starting row to be imported and must be greater than or equal to 2 because heading is compulsory.\" /></td>" + Environment.NewLine);
            sb.Append("        </tr>" + Environment.NewLine);
            sb.Append("        <tr>" + Environment.NewLine);
            sb.Append("            <td colspan=\"3\">" + Environment.NewLine);
            sb.Append("            <div class=\"rg-1-5\"><asp:FileUpload id=\"cBrowse\" CssClass=\"inp-txt\" runat=\"server\" style=\"padding: 0px;\" /><asp:button id=\"cBrowseButton\" OnClick=\"cBrowseButton_Click\" Style=\"display:none;\" runat=\"server\" /></div>" + Environment.NewLine);
            sb.Append("            <div class=\"rg-6-10\"><span>WorkSheet:</span><asp:DropDownList id=\"cWorkSheet\" CssClass=\"inp-ddl\" runat=\"server\" style=\"max-width:120px;\" /></div>" + Environment.NewLine);
            sb.Append("            <div class=\"rg-11-12\"><span>Overwrite:</span><asp:CheckBox id=\"cOverwrite\" CssClass=\"inp-chk\" runat=\"server\"");
            if (dw["DefOverwrite"].ToString() == "Y") { sb.Append(" Checked=\"true\""); } else { sb.Append(" Checked=\"false\""); }
            if (dw["OvwrReadonly"].ToString() == "Y") { sb.Append(" Enabled=\"false\""); } else { sb.Append(" Enabled=\"true\""); }
            sb.Append(" AutoPostBack=\"false\" ToolTip=\"Check here to overwrite previous import, if applicable.\" /><asp:ImageButton id=\"cSchemaImage\" style=\"vertical-align:middle;\" runat=\"server\" ImageUrl=\"../images/Schema.gif\" /></div>" + Environment.NewLine);
            sb.Append("            </td>" + Environment.NewLine);
            sb.Append("        </tr>" + Environment.NewLine);
            sb.Append("        <tr>" + Environment.NewLine);
            sb.Append("            <td colspan=\"3\"><hr /><span style=\"float:right; text-align:right;\"><asp:label id=\"cMsgLabel\" CssClass=\"MsgText\" runat=\"server\" width=\"100%\" /><asp:Button id=\"cImportButton\" onclick=\"cImportButton_Click\" runat=\"server\" /></span></td>" + Environment.NewLine);
            sb.Append("        </tr>" + Environment.NewLine);
            sb.Append("        </table>" + Environment.NewLine);
            sb.Append("        </div>" + Environment.NewLine);
            sb.Append("    </div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("<asp:Label ID=\"cMsgContent\" runat=\"server\" style=\"display:none;\" EnableViewState=\"false\"/>" + Environment.NewLine);
            sb.Append("</ContentTemplate></asp:UpdatePanel>" + Environment.NewLine);
            sb.Append("<asp:TextBox id=\"cFNameO\" text=\"\" Width=\"0px\" style=\"visibility:hidden;\" runat=\"server\" />" + Environment.NewLine);
			sb.Append("<asp:TextBox id=\"cFName\" text=\"\" Width=\"0px\" style=\"visibility:hidden;\" runat=\"server\" />" + Environment.NewLine);
            return sb;
		}

		private StringBuilder MakeAscxCs(DataRow dw, DataView dv, Int32 wizardId, byte systemId, string wizardTitle, CurrPrj CPrj, CurrSrc CSrc, string clientFrwork)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("using System;" + Environment.NewLine);
			sb.Append("using System.Data;" + Environment.NewLine);
			sb.Append("using System.Data.OleDb;" + Environment.NewLine);
			sb.Append("using System.Drawing;" + Environment.NewLine);
			sb.Append("using System.IO;" + Environment.NewLine);
			sb.Append("using System.Web;" + Environment.NewLine);
			sb.Append("using System.Web.UI;" + Environment.NewLine);
			sb.Append("using System.Web.UI.WebControls;" + Environment.NewLine);
			sb.Append("using System.Web.UI.HtmlControls;" + Environment.NewLine);
			sb.Append("using System.Globalization;" + Environment.NewLine);
			sb.Append("using System.Threading;" + Environment.NewLine);
            sb.Append("using RO.Facade3;" + Environment.NewLine);
            sb.Append("using RO.Common3;" + Environment.NewLine);
            sb.Append("using RO.Common3.Data;" + Environment.NewLine);
            //if (CSrc.SrcSystemId != 3)	// Admin.
            //{
            //    sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ".Data;" + Environment.NewLine);
            //}
            sb.Append(Environment.NewLine);
			sb.Append("namespace " + CPrj.EntityCode + ".Web" + Environment.NewLine);
			sb.Append("{" + Environment.NewLine);
			sb.Append("	public");
			if (clientFrwork == "1") {sb.Append(" abstract");} 
			else {sb.Append(" partial");}
			sb.Append(" class " + dw["ProgramName"].ToString() + "Module : RO.Web.ModuleBase" + Environment.NewLine);
			sb.Append("	{" + Environment.NewLine);
			sb.Append("		private const string KEY_dt" + dw["ProgramName"].ToString() + " = \"Cache:dt" + dw["ProgramName"].ToString() + "\";" + Environment.NewLine);
			sb.Append("		private string LcSysConnString;" + Environment.NewLine);
			sb.Append("		private string LcAppConnString;" + Environment.NewLine);
			sb.Append("		private string LcAppPw;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
			sb.Append("		public " + dw["ProgramName"].ToString() + "Module()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			this.Init += new System.EventHandler(Page_Init);" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		protected void Page_Load(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			CheckAuthentication(true);" + Environment.NewLine);
            sb.Append("			Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LUser.Culture);" + Environment.NewLine);
            sb.Append("			string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;" + Environment.NewLine);
            sb.Append("			string lang = \"en,zh\".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;" + Environment.NewLine);
            sb.Append("			ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), \"datepicker_i18n\", \"scripts/i18n/jquery.ui.datepicker-\" + lang + \".js\");" + Environment.NewLine);
            sb.Append("			cMsgLabel.Text = \"\";" + Environment.NewLine);
            sb.Append("			if (!IsPostBack)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
            sb.Append("				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, " + CSrc.SrcSystemId.ToString() + ", base.LImpr, LcSysConnString, LcAppPw, null, null, " + wizardId.ToString() + ");" + Environment.NewLine);
            sb.Append("				if (dtMenuAccess.Rows.Count == 0)" + Environment.NewLine);
            sb.Append("				{" + Environment.NewLine);
            sb.Append("				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, \"cSystem\", \"AccessDeny\", null, null, null);" + Environment.NewLine);
            sb.Append("				    throw new Exception(message);" + Environment.NewLine);
            sb.Append("				}" + Environment.NewLine);
            sb.Append("				SetButtonHlp();" + Environment.NewLine);
			sb.Append("				Session.Remove(KEY_dt" + dw["ProgramName"].ToString() + ");" + Environment.NewLine);
			sb.Append("				cHelpLabel.Text = \"Please backup the database, check \\\"All SpreadSheets\\\" or select the appropriate import SpreadSheet then press Import button to import. Use the buttons on the right to filter the directory or list all import files respectively. Only Local SpreadSheet will be processed if selected.\";" + Environment.NewLine);
			sb.Append("				cTitleLabel.Text = \"" + wizardTitle + "\";" + Environment.NewLine);
			sb.Append("				cLocation.Text = @Config.PathXlsImport;" + Environment.NewLine);
            sb.Append("				(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, \"" + wizardTitle + "\", 0, 0, " + wizardId.ToString() + ", string.Empty, LcSysConnString, LcAppPw);" + Environment.NewLine);
            sb.Append("				Session[\"ImportSchema\"] = (new AdminSystem()).GetSchemaWizImp(" + wizardId.ToString() + ",base.LUser.CultureId,LcSysConnString,LcAppPw);" + Environment.NewLine);
            sb.Append("				if (cSchemaImage.Attributes[\"OnClick\"] == null || cSchemaImage.Attributes[\"OnClick\"].IndexOf(\"ImportSchema.aspx\") < 0) {cSchemaImage.Attributes[\"OnClick\"] += \"window.open('ImportSchema.aspx?scm=W&key=" + wizardId.ToString() + "&csy=" + CSrc.SrcSystemId.ToString() + "','ImportSchemaW" + wizardId.ToString() + "','resizable=yes,scrollbars=yes,status=yes,width=700,height=500'); return false;\";}" + Environment.NewLine);
			sb.Append("				cBrowse.Attributes.Add(\"OnChange\", \"__doPostBack('\" + cBrowseButton.ClientID.Replace(\"_\", \"$\") + \"','')\");" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		protected void Page_Init(object sender, EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			InitializeComponent();" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		#region Web Form Designer generated code" + Environment.NewLine);
			sb.Append("		/// <summary>" + Environment.NewLine);
			sb.Append("		/// Required method for Designer support - do not modify" + Environment.NewLine);
			sb.Append("		/// the contents of this method with the code editor." + Environment.NewLine);
			sb.Append("		/// </summary>" + Environment.NewLine);
			sb.Append("		private void InitializeComponent()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			if (LcSysConnString == null) { SetSystem(" + CSrc.SrcSystemId.ToString() + "); }" + Environment.NewLine);
			if (clientFrwork == "1") { sb.Append("			this.Load += new System.EventHandler(this.Page_Load);" + Environment.NewLine); }
			sb.Append(Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append("		#endregion" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private void SetSystem(byte SystemId)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			LcSysConnString = base.SysConnectStr(SystemId);" + Environment.NewLine);
			sb.Append("			LcAppConnString = base.AppConnectStr(SystemId);" + Environment.NewLine);
			sb.Append("			LcAppPw = base.AppPwd(SystemId);" + Environment.NewLine);
            sb.Append("			try" + Environment.NewLine);
            sb.Append("			{" + Environment.NewLine);
            sb.Append("				base.CPrj = new CurrPrj(((new RobotSystem()).GetEntityList()).Rows[0]);" + Environment.NewLine);
            sb.Append("				DataRow row = base.SystemsList.Rows.Find(SystemId);" + Environment.NewLine);
            sb.Append("				base.CSrc = new CurrSrc(true, row);" + Environment.NewLine);
            sb.Append("				base.CTar = new CurrTar(true, row);" + Environment.NewLine);
            sb.Append("				if ((Config.DeployType == \"DEV\" || row[\"dbAppDatabase\"].ToString() == base.CPrj.EntityCode + \"View\") && !(base.CPrj.EntityCode != \"RO\" && row[\"SysProgram\"].ToString() == \"Y\") && (new AdminSystem()).IsRegenNeeded(string.Empty,0,0," + wizardId.ToString() + ",LcSysConnString,LcAppPw))" + Environment.NewLine);
            sb.Append("				{" + Environment.NewLine);
            sb.Append("					(new GenWizardsSystem()).CreateProgram(" + wizardId.ToString() + ", \"" + wizardTitle + "\", row[\"dbAppDatabase\"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);" + Environment.NewLine);
            sb.Append("					Response.Redirect(Request.RawUrl);" + Environment.NewLine);
            sb.Append("				}" + Environment.NewLine);
            sb.Append("			}" + Environment.NewLine);
            sb.Append("			catch (Exception e) { PreMsgPopup(e.Message); }" + Environment.NewLine);
            sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private void CheckAuthentication(bool pageLoad)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
            sb.Append("			CheckAuthentication(pageLoad, " + (dw["AuthRequired"].ToString() == "N" ? "false" : "true") + ");" + Environment.NewLine);
            //sb.Append("			if (!Request.IsAuthenticated || base.LUser == null)" + Environment.NewLine);
            //sb.Append("			{" + Environment.NewLine);
            //sb.Append("				string loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;" + Environment.NewLine);
            //sb.Append("				if (string.IsNullOrEmpty(loginUrl)) loginUrl = \"Default.aspx\";" + Environment.NewLine);
            //sb.Append("				Response.Redirect(loginUrl + (loginUrl.IndexOf('?') > 0 ? \"&\" : \"?\") + \"wrn=\" + (pageLoad ? \"1\" : \"2\") + \"&ReturnUrl=\" + Server.UrlEncode(Request.Url.PathAndQuery));" + Environment.NewLine);
            //sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private void SetButtonHlp()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			DataTable dt;" + Environment.NewLine);
            sb.Append("			dt = (new AdminSystem()).GetButtonHlp(0,0," + wizardId.ToString() + ",base.LUser.CultureId,LcSysConnString,LcAppPw);" + Environment.NewLine);
			sb.Append("			if (dt != null && dt.Rows.Count > 0)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				foreach (DataRow dr in dt.Rows)" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
            sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"Search\") { cSearchButton.CssClass = \"ButtonImg SearchButtonImg\"; cSearchButton.Text = string.Empty; cSearchButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cSearchButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
            sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"List\") { cListButton.CssClass = \"ButtonImg ListButtonImg\"; cListButton.Text = string.Empty; cListButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cListButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
            sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"Import\") { cImportButton.CssClass = \"small blue button\"; cImportButton.Text = dr[\"ButtonName\"].ToString(); cImportButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cImportButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
			sb.Append("				}" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		public void cSearchButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			DataTable dt = (DataTable)Session[KEY_dt" + dw["ProgramName"].ToString() + "];" + Environment.NewLine);
			sb.Append("			if (dt != null)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				DataView dv = dt.DefaultView;" + Environment.NewLine);
			sb.Append("				dv.RowFilter = \"FileFullName like '*\" + cSearch.Text + \"*'\";" + Environment.NewLine);
			sb.Append("				cFileList.DataSource = dv; cFileList.DataBind();" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		public void cListButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			PopFileList(cSearch.Text);" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		protected void cBrowseButton_Click(object sender, EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			if (cBrowse.HasFile && cBrowse.PostedFile.FileName != string.Empty)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
            sb.Append("				string fNameO = (new FileInfo(cBrowse.PostedFile.FileName)).Name;" + Environment.NewLine);
            sb.Append("				try" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
			sb.Append("					string fName;" + Environment.NewLine);
			sb.Append("					if (fNameO.LastIndexOf(\".\") >= 0)" + Environment.NewLine);
			sb.Append("					{" + Environment.NewLine);
			sb.Append("						fName = fNameO.Insert(fNameO.LastIndexOf(\".\"), \"_\" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());" + Environment.NewLine);
			sb.Append("					}" + Environment.NewLine);
			sb.Append("					else" + Environment.NewLine);
			sb.Append("					{" + Environment.NewLine);
			sb.Append("						fName = fNameO + \"_\" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();" + Environment.NewLine);
			sb.Append("					}" + Environment.NewLine);
            sb.Append("					fName = fName.Replace(\":\",\"\").Replace(\"..\",\"\");" + Environment.NewLine);
            sb.Append("					if (!Directory.Exists(Config.PathTmpImport)) { Directory.CreateDirectory(Config.PathTmpImport); }" + Environment.NewLine);
            sb.Append("					cBrowse.PostedFile.SaveAs(Config.PathTmpImport + fName);" + Environment.NewLine);
			sb.Append("					cWorkSheet.DataSource = (new XLSImport()).GetSheetNames(Config.PathTmpImport + fName); cWorkSheet.DataBind();" + Environment.NewLine);
			sb.Append("					cFNameO.Text = fNameO; cFName.Text = fName;" + Environment.NewLine);
			sb.Append("				}" + Environment.NewLine);
            // Cannnot use PreMsgPopup, must throw because of full postback;
            sb.Append("				catch (Exception err) {throw new Exception(\"Unable to retrieve sheet names from \\\"\" + fNameO + \"\\\": \" + err.Message);}" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		public void cImportButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			int iCnt;" + Environment.NewLine);
			sb.Append("			if (cFNameO.Text != string.Empty)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				if (cWorkSheet.Items.Count > 0 && cWorkSheet.SelectedItem.Text != string.Empty && cStartRow.Text != string.Empty)" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
			sb.Append("					cAllFile.Checked = false; cFileList.ClearSelection();" + Environment.NewLine);
			sb.Append("					try" + Environment.NewLine);
			sb.Append("					{" + Environment.NewLine);
			sb.Append("						iCnt = ImportClient(cOverwrite.Checked,LUser.UsrId,cFNameO.Text,cWorkSheet.SelectedItem.Text,cStartRow.Text,Config.PathTmpImport + cFName.Text);" + Environment.NewLine);
			sb.Append("						cMsgLabel.Text = cMsgLabel.Text + iCnt.ToString() + \" rows from \\\"\" + cFNameO.Text + \"\\\" has been imported;<br />\";" + Environment.NewLine);
			sb.Append("					}" + Environment.NewLine);
            sb.Append("					catch (Exception err) {PreMsgPopup(\"Error in spreadsheet \\\"\" + cFNameO.Text + \"\\\": \" + err.Message);}" + Environment.NewLine);
			sb.Append("					finally {cWorkSheet.Items.Clear(); cFNameO.Text = string.Empty;}" + Environment.NewLine);
			// Do not "File.Delete(Config.PathTmpImport + cFName.Text);" because Excel-97/network spreadsheets may hang the process saying file-still-open-by-another-process.
			sb.Append("				}" + Environment.NewLine);
			sb.Append("				else" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
            sb.Append("					PreMsgPopup(\"Please indicate a worksheet for the selected local spreadsheet, enter the starting row and try again.\");" + Environment.NewLine);
			sb.Append("				}" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("			else	// Multiple server spreadsheets import." + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				if (cWorkSheetM.Text != string.Empty && cStartRow.Text != string.Empty)" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
			sb.Append("					if (cAllFile.Checked)" + Environment.NewLine);
			sb.Append("					{" + Environment.NewLine);
			sb.Append("						DataTable dt = (DataTable)Session[KEY_dt" + dw["ProgramName"].ToString() + "];" + Environment.NewLine);
			sb.Append("						if (dt != null)" + Environment.NewLine);
			sb.Append("						{" + Environment.NewLine);
			sb.Append("							foreach (DataRowView drv in dt.DefaultView)" + Environment.NewLine);
			sb.Append("							{" + Environment.NewLine);
			sb.Append("								try" + Environment.NewLine);
			sb.Append("								{" + Environment.NewLine);
            //sb.Append("									if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("									{" + Environment.NewLine);
            //sb.Append("										iCnt = " + dw["ProgramName"].ToString() + "Facade().ImportFile" + wizardId.ToString() + "(cOverwrite.Checked,LUser.UsrId,drv[\"FileName\"].ToString(),cWorkSheetM.Text,cStartRow.Text,drv[\"FileFullName\"].ToString()" + Robot.GetCnCall("N", "N") + ");" + Environment.NewLine);
            //sb.Append("									}" + Environment.NewLine);
            //sb.Append("									else" + Environment.NewLine);
            //sb.Append("									{" + Environment.NewLine);
            sb.Append("									iCnt = ImportFile(cOverwrite.Checked,LUser.UsrId,drv[\"FileName\"].ToString(),cWorkSheetM.Text,cStartRow.Text,drv[\"FileFullName\"].ToString());" + Environment.NewLine);
            //sb.Append("									iCnt = (new " + dw["ProgramName"].ToString() + "System()).ImportFile" + wizardId.ToString() + "(cOverwrite.Checked,LUser.UsrId,drv[\"FileName\"].ToString(),cWorkSheetM.Text,cStartRow.Text,drv[\"FileFullName\"].ToString()" + Robot.GetCnCall("N", "N") + ");" + Environment.NewLine);
            //sb.Append("									}" + Environment.NewLine);
			sb.Append("									cMsgLabel.Text = cMsgLabel.Text + iCnt.ToString() + \" rows from \\\"\" + drv[\"FileName\"].ToString() + \"\\\" has been imported;<br />\";" + Environment.NewLine);
			sb.Append("								}" + Environment.NewLine);
            sb.Append("								catch (Exception err) {PreMsgPopup(\"Error in spreadsheet \\\"\" + drv[\"FileName\"].ToString() + \"\\\": \" + err.Message); break;}" + Environment.NewLine);
			sb.Append("							}" + Environment.NewLine);
			sb.Append("						}" + Environment.NewLine);
			sb.Append("					}" + Environment.NewLine);
			sb.Append("					else" + Environment.NewLine);
			sb.Append("					{" + Environment.NewLine);
			sb.Append("						foreach (ListItem li in cFileList.Items)" + Environment.NewLine);
			sb.Append("						{" + Environment.NewLine);
			sb.Append("							if (li.Selected)" + Environment.NewLine);
			sb.Append("							{" + Environment.NewLine);
			sb.Append("								try" + Environment.NewLine);
			sb.Append("								{" + Environment.NewLine);
            //sb.Append("									if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("									{" + Environment.NewLine);
            //sb.Append("										iCnt = " + dw["ProgramName"].ToString() + "Facade().ImportFile" + wizardId.ToString() + "(cOverwrite.Checked,LUser.UsrId,li.Value,cWorkSheetM.Text,cStartRow.Text,li.Text" + Robot.GetCnCall("N", "N") + ");" + Environment.NewLine);
            //sb.Append("									}" + Environment.NewLine);
            //sb.Append("									else" + Environment.NewLine);
            //sb.Append("									{" + Environment.NewLine);
            sb.Append("									iCnt = ImportFile(cOverwrite.Checked,LUser.UsrId,li.Value,cWorkSheetM.Text,cStartRow.Text,li.Text);" + Environment.NewLine);
            //sb.Append("									iCnt = (new " + dw["ProgramName"].ToString() + "System()).ImportFile" + wizardId.ToString() + "(cOverwrite.Checked,LUser.UsrId,li.Value,cWorkSheetM.Text,cStartRow.Text,li.Text" + Robot.GetCnCall("N", "N") + ");" + Environment.NewLine);
            //sb.Append("									}" + Environment.NewLine);
			sb.Append("									cMsgLabel.Text = cMsgLabel.Text + iCnt.ToString() + \" rows from \\\"\" + li.Value + \"\\\" has been imported;<br />\";" + Environment.NewLine);
			sb.Append("								}" + Environment.NewLine);
            sb.Append("								catch (Exception err) {PreMsgPopup(\"Error in spreadsheet \\\"\" + li.Value + \"\\\": \" + err.Message); break;}" + Environment.NewLine);
			sb.Append("							}" + Environment.NewLine);
			sb.Append("						}" + Environment.NewLine);
			sb.Append("					}" + Environment.NewLine);
			sb.Append("				}" + Environment.NewLine);
			sb.Append("				else" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
            sb.Append("					PreMsgPopup(\"If import from server location is your choice, please enter a worksheet name or the sheet order for the selected spreadsheets, indicate the starting row and try again.\");" + Environment.NewLine);
			sb.Append("				}" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private int ImportClient(bool bOverwrite, Int32 usrId, string fileName, string workSheet, string startRow, string fileFullName)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			try" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				DataSet ds = RO.Common3.XmlUtils.XmlToDataSet(((new XLSImport()).ImportFile(fileName, workSheet, startRow, fileFullName)));" + Environment.NewLine);
			sb.Append("				DataRowCollection rows = ds.Tables[0].Rows;" + Environment.NewLine);
			sb.Append("				DataColumnCollection cols = ds.Tables[0].Columns;" + Environment.NewLine);
			sb.Append("				int iStart = int.Parse(startRow) - 1;" + Environment.NewLine);
			sb.Append("				for ( int iRow = iStart; iRow < rows.Count; iRow++ )" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
			sb.Append("					if (");
			int iNotNull = 0;
			bool bNotNullFound = false;
			bool bStringColFound = false;
			foreach (DataRowView drv in dv)
			{
				if (drv["AllowNulls"].ToString() == "N" && drv["ColumnIdentity"].ToString() == "N")
				{
					if (bNotNullFound) { sb.Append(" && "); } else { bNotNullFound = true; }
					sb.Append("rows[iRow][" + iNotNull.ToString() + "].ToString() == string.Empty");
				}
				iNotNull = iNotNull + 1;
				if (drv["DataTypeSysName"].ToString() == "String") { bStringColFound = true; }
			}
			if (!bNotNullFound) { throw new Exception("SpreadSheet import must have at least one non-nullable column. Please try again."); }
			sb.Append(") {rows[iRow].Delete();}" + Environment.NewLine);
			if (bStringColFound)
			{
				sb.Append("					else" + Environment.NewLine);
				sb.Append("					{" + Environment.NewLine);
				int iCol = 0;
				foreach (DataRowView drv in dv)
				{
					if (drv["DataTypeSysName"].ToString() == "String")
					{
                        sb.Append("						try {rows[iRow][" + iCol.ToString() + "] = rows[iRow][" + iCol.ToString() + "].ToString().Replace(\"\\r\",\"\").Replace(\"\\n\",\"\");} catch {};" + Environment.NewLine);
                    }
					iCol = iCol + 1;
				}
				sb.Append("					}" + Environment.NewLine);
			}
			sb.Append("				}" + Environment.NewLine);
            //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            //sb.Append("					return " + dw["ProgramName"].ToString() + "Facade().ImportRows" + wizardId.ToString() + "(bOverwrite,usrId,XmlUtils.DataSetToXml(ds),iStart,fileName" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
            //sb.Append("				else" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            sb.Append("				return (new AdminSystem()).ImportRows(" + wizardId.ToString() + ",\"Wiz" + dw["ProgramName"].ToString() + "\",bOverwrite,usrId,ds,iStart,fileName" + Robot.GetCnStr("N", "N") + ",CPrj,CSrc);" + Environment.NewLine);
            //sb.Append("				return (new " + dw["ProgramName"].ToString() + "System()).ImportRows" + wizardId.ToString() + "(bOverwrite,usrId,ds,iStart,fileName" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("			catch(Exception e) { throw e; return 0; }" + Environment.NewLine);
			//sb.Append("			finally" + Environment.NewLine);
			//sb.Append("			{" + Environment.NewLine);
			//sb.Append("				conn.Close(); conn = null;" + Environment.NewLine);
			//sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private void PopFileList(string searchTxt)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			//sb.Append("			DataTable dt;" + Environment.NewLine);
            //sb.Append("			if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("			{" + Environment.NewLine);
            //sb.Append("				dt = XmlUtils.XmlToDataTable(" + dw["ProgramName"].ToString() + "Facade().GetFileList" + wizardId.ToString() + "(cLocation.Text));" + Environment.NewLine);
            //sb.Append("			}" + Environment.NewLine);
            //sb.Append("			else" + Environment.NewLine);
            //sb.Append("			{" + Environment.NewLine);
            sb.Append("			DataTable dt = GetFileList(cLocation.Text);" + Environment.NewLine);
            //sb.Append("			dt = (new " + dw["ProgramName"].ToString() + "System()).GetFileList" + wizardId.ToString() + "(cLocation.Text);" + Environment.NewLine);
            //sb.Append("			}" + Environment.NewLine);
			sb.Append("			if (dt != null)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				cSearch.Text = string.Empty;" + Environment.NewLine);
			sb.Append("				DataView dv = dt.DefaultView;" + Environment.NewLine);
			sb.Append("				dv.Sort = \"FileFullName\";" + Environment.NewLine);
			sb.Append("				Session[KEY_dt" + dw["ProgramName"].ToString() + "] = dt;" + Environment.NewLine);
			sb.Append("				cFileList.DataSource = dv; cFileList.DataBind();" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("		private DataTable GetFileList(string fileFolder)" + Environment.NewLine);
            sb.Append("		{" + Environment.NewLine);
            sb.Append("			DataTable dt = new DataTable();" + Environment.NewLine);
            sb.Append("			dt.Columns.Add(\"FileName\", typeof(String));" + Environment.NewLine);
            sb.Append("			dt.Columns.Add(\"FileFullName\", typeof(String));" + Environment.NewLine);
            sb.Append("			DirectoryInfo di = new DirectoryInfo(fileFolder);" + Environment.NewLine);
            sb.Append("			// Capture SpreadSheets in current folder" + Environment.NewLine);
            sb.Append("			FileInfo[] files = di.GetFiles(\"*.xls\",SearchOption.AllDirectories);" + Environment.NewLine);
            sb.Append("			foreach (FileInfo fi in files)" + Environment.NewLine);
            sb.Append("			{" + Environment.NewLine);
            sb.Append("				dt.Rows.InsertAt(dt.NewRow(), 0);" + Environment.NewLine);
            sb.Append("				dt.Rows[0][\"FileName\"] = fi.Name;" + Environment.NewLine);
            sb.Append("				dt.Rows[0][\"FileFullName\"] = fi.FullName;" + Environment.NewLine);
            sb.Append("			}" + Environment.NewLine);
            sb.Append("			return dt;" + Environment.NewLine);
            sb.Append("		}" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("		private int ImportFile(bool bOverwrite, Int32 usrId, string fileName, string workSheet, string startRow, string fileFullName)" + Environment.NewLine);
            sb.Append("		{" + Environment.NewLine);
            sb.Append("			try" + Environment.NewLine);
            sb.Append("			{" + Environment.NewLine);
            sb.Append("				DataSet ds = RO.Common3.XmlUtils.XmlToDataSet(((new XLSImport()).ImportFile(fileName, workSheet, startRow, fileFullName)));" + Environment.NewLine);
            sb.Append("				DataRowCollection rows = ds.Tables[0].Rows;" + Environment.NewLine);
            sb.Append("				DataColumnCollection cols = ds.Tables[0].Columns;" + Environment.NewLine);
            sb.Append("				int iStart = int.Parse(startRow) - 1;" + Environment.NewLine);
            sb.Append("				for ( int iRow = iStart; iRow < rows.Count; iRow++ )" + Environment.NewLine);
            sb.Append("				{" + Environment.NewLine);
            sb.Append("					if (");
            iNotNull = 0;
            bNotNullFound = false;
            bStringColFound = false;
            foreach (DataRowView drv in dv)
            {
                if (drv["AllowNulls"].ToString() == "N" && drv["ColumnIdentity"].ToString() == "N")
                {
                    if (bNotNullFound) { sb.Append(" && "); } else { bNotNullFound = true; }
                    sb.Append("rows[iRow][" + iNotNull.ToString() + "].ToString() == string.Empty");
                }
                iNotNull = iNotNull + 1;
                if (drv["DataTypeSysName"].ToString() == "String") { bStringColFound = true; }
            }
            if (!bNotNullFound) { throw new Exception("SpreadSheet import must have at least one non-nullable column. Please try again."); }
            sb.Append(") {rows[iRow].Delete();}" + Environment.NewLine);
            if (bStringColFound)
            {
                sb.Append("					else" + Environment.NewLine);
                sb.Append("					{" + Environment.NewLine);
                int iCol = 0;
                foreach (DataRowView drv in dv)
                {
                    if (drv["DataTypeSysName"].ToString() == "String")
                    {
                        sb.Append("						rows[iRow][" + iCol.ToString() + "] = rows[iRow][" + iCol.ToString() + "].ToString().Replace(\"\\r\",\"\").Replace(\"\\n\",\"\");" + Environment.NewLine);
                    }
                    iCol = iCol + 1;
                }
                sb.Append("					}" + Environment.NewLine);
            }
            sb.Append("				}" + Environment.NewLine);
            sb.Append("				return (new AdminSystem()).ImportRows(" + wizardId.ToString() + ",\"Wiz" + dw["ProgramName"].ToString() + "\",bOverwrite,usrId,ds,iStart,fileName" + Robot.GetCnStr("N", "N") + ",CPrj,CSrc);" + Environment.NewLine);
            sb.Append("			}" + Environment.NewLine);
            sb.Append("			catch (Exception e) { PreMsgPopup(e.Message); return 0; }" + Environment.NewLine);
            sb.Append("		}" + Environment.NewLine);
            sb.Append(Environment.NewLine);
			sb.Append("		protected void cFileList_SelectedIndexChanged(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			cAllFile.Checked = false;" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		protected void cAllFile_CheckedChanged(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			if (cAllFile.Checked) {cFileList.ClearSelection();}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("		private void PreMsgPopup(string msg)" + Environment.NewLine);
            sb.Append("		{" + Environment.NewLine);
            sb.Append("		    int MsgPos = msg.IndexOf(\"RO.SystemFramewk.ApplicationAssert\");" + Environment.NewLine);
            sb.Append("		    string iconUrl = \"images/info.gif\";" + Environment.NewLine);  // Client-side no '~/'.
            sb.Append("		    string focusOnCloseId = string.Empty;" + Environment.NewLine);
            sb.Append("		    string msgContent = ReformatErrMsg(msg);" + Environment.NewLine);
            sb.Append("		    if (MsgPos >= 0 && LUser.TechnicalUsr != \"Y\") { msgContent = ReformatErrMsg(msg.Substring(0, MsgPos - 3)); }" + Environment.NewLine);
            sb.Append("			string script =" + Environment.NewLine);
            sb.Append("			@\"<script type='text/javascript' lang='javascript'>" + Environment.NewLine);
            sb.Append("			PopDialog('\" + iconUrl + \"','\" + msgContent.Replace(@\"\\\", @\"\\\\\").Replace(\"'\", @\"\\'\") + \"','\" + focusOnCloseId + @\"');" + Environment.NewLine);
            sb.Append("			</script>\";" + Environment.NewLine);
            sb.Append("			ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), \"Popup\", script, false);" + Environment.NewLine);
            sb.Append("		}" + Environment.NewLine);
            sb.Append("	}" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			return sb;
		}

        //private StringBuilder MakeSystemCs(DataRow dw, Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("using System;" + Environment.NewLine);
        //    sb.Append("using System.Data;" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Rule" + CSrc.SrcSystemId.ToString() + ";" + Environment.NewLine);
        //    if (CSrc.SrcSystemId != 3)	// Admin.
        //    {
        //        sb.Append("using RO.Rule3;" + Environment.NewLine);
        //    }
        //    sb.Append(Environment.NewLine);
        //    sb.Append("namespace " + CPrj.EntityCode + ".Facade" + CSrc.SrcSystemId.ToString() + Environment.NewLine);
        //    sb.Append("{" + Environment.NewLine);
        //    sb.Append("	public class " + dw["ProgramName"].ToString() + "System : MarshalByRefObject" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    //sb.Append(Environment.NewLine);
        //    //sb.Append("		public DataTable GetFileList" + wizardId.ToString() + "(string fileFolder)" + Environment.NewLine);
        //    //sb.Append("		{" + Environment.NewLine);
        //    //sb.Append("			return (new " + dw["ProgramName"].ToString() + "Rules()).GetFileList" + wizardId.ToString() + "(fileFolder);" + Environment.NewLine);
        //    //sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		public int ImportFile" + wizardId.ToString() + "(bool bOverwrite, Int32 usrId, string fileName, string workSheet, string startRow, string fileFullName" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			return (new " + dw["ProgramName"].ToString() + "Rules()).ImportFile" + wizardId.ToString() + "(bOverwrite,usrId,fileName,workSheet,startRow,fileFullName" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		public int ImportRows" + wizardId.ToString() + "(bool bOverwrite, Int32 usrId, DataSet ds, int iStart, string fileName" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			using (Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access dac = new Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access())" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				return dac.ImportRows" + wizardId.ToString() + "(bOverwrite,usrId,ds,iStart,fileName" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append("}" + Environment.NewLine);
        //    return sb;
        //}

        //private StringBuilder MakeRulesCs(DataRow dw, Int32 wizardId, DataView dv, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("using System;" + Environment.NewLine);
        //    sb.Append("using System.Data;" + Environment.NewLine);
        //    sb.Append("using System.Data.OleDb;" + Environment.NewLine);
        //    sb.Append("using System.Text.RegularExpressions;" + Environment.NewLine);
        //    sb.Append("using System.IO;" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ";" + Environment.NewLine);
        //    if (CSrc.SrcSystemId != 3)	// Admin.
        //    {
        //        sb.Append("using RO.Common3;" + Environment.NewLine);
        //        sb.Append("using RO.Common3.Data;" + Environment.NewLine);
        //    }
        //    sb.Append("using RO.SystemFramewk;" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("namespace " + CPrj.EntityCode + ".Rule" + CSrc.SrcSystemId.ToString() + "" + Environment.NewLine);
        //    sb.Append("{" + Environment.NewLine);
        //    sb.Append("	public class " + dw["ProgramName"].ToString() + "Rules" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    sb.Append("		public int ImportFile" + wizardId.ToString() + "(bool bOverwrite, Int32 usrId, string fileName, string workSheet, string startRow, string fileFullName" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    //sb.Append("			OleDbDataAdapter da = new OleDbDataAdapter();" + Environment.NewLine);
        //    //sb.Append("			OleDbConnection conn = new OleDbConnection();" + Environment.NewLine);
        //    sb.Append("			try" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    //sb.Append("				conn.ConnectionString = \"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\" + fileFullName + \";Extended Properties=\\\"Excel 8.0; HDR=NO; IMEX=1;\\\"\";" + Environment.NewLine);
        //    //sb.Append("				conn.Open();" + Environment.NewLine);
        //    // The following codes enables worksheet order to be entered instead of a name;
        //    // The prerequisite are for the development server to have:
        //    // 1. Two ddl copied to C:\windows\system32; They are Microsoft.Office.Interop.Excel.dll and Microsoft.Vbe.Interop.dll;
        //    // 2. Rule3 project includes "Microsoft.Office.Interop.Excel" as references;
        //    // This is needed here for multiple spreadsheets import.
        //    // Begin.
        //    //sb.Append("				if (Config.EnableWebExcel)" + Environment.NewLine);
        //    //sb.Append("				{" + Environment.NewLine);
        //    //sb.Append("					Regex rx = new Regex(@\"^[\\d]+$\");" + Environment.NewLine);
        //    //sb.Append("					if (rx.IsMatch(workSheet))" + Environment.NewLine);
        //    //sb.Append("					{" + Environment.NewLine);
        //    //sb.Append("						Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();" + Environment.NewLine);
        //    //sb.Append("						Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(fileFullName,0,true,5,\"\",\"\",false,Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,\"\",true,false,0,true,false,false);" + Environment.NewLine);
        //    //sb.Append("						Microsoft.Office.Interop.Excel.Sheets excelSheets = excelWorkbook.Worksheets;" + Environment.NewLine);
        //    //sb.Append("						if (int.Parse(workSheet) <= excelSheets.Count)" + Environment.NewLine);
        //    //sb.Append("						{" + Environment.NewLine);
        //    //sb.Append("							workSheet = ((Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(int.Parse(workSheet))).Name.Replace(\"'\",string.Empty).Replace(\"$\",string.Empty);" + Environment.NewLine);
        //    //sb.Append("						}" + Environment.NewLine);
        //    //sb.Append("						excelApp.Workbooks.Close();" + Environment.NewLine);
        //    //sb.Append("						excelApp.Quit();" + Environment.NewLine);
        //    //sb.Append("					}" + Environment.NewLine);
        //    //sb.Append("				}" + Environment.NewLine);
        //    // End.
        //    //sb.Append("				string myQuery = @\"SELECT * From [\" + workSheet + \"$]\";" + Environment.NewLine);
        //    //sb.Append("				OleDbCommand myCmd = new OleDbCommand(myQuery, conn);" + Environment.NewLine);
        //    //sb.Append("				da.SelectCommand = myCmd;" + Environment.NewLine);
        //    //sb.Append("				DataSet ds = new DataSet();" + Environment.NewLine);
        //    //sb.Append("				da.Fill(ds, \"" + dw["MasterTableName"].ToString() + "\");" + Environment.NewLine);
        //    sb.Append("				DataSet ds = RO.Common3.XmlUtils.XmlToDataSet(((new XLSImport()).ImportFile(fileName, workSheet, startRow, fileFullName)));" + Environment.NewLine);
        //    sb.Append("				DataRowCollection rows = ds.Tables[0].Rows;" + Environment.NewLine);
        //    sb.Append("				DataColumnCollection cols = ds.Tables[0].Columns;" + Environment.NewLine);
        //    sb.Append("				int iStart = int.Parse(startRow) - 1;" + Environment.NewLine);
        //    sb.Append("				for ( int iRow = iStart; iRow < rows.Count; iRow++ )" + Environment.NewLine);
        //    sb.Append("				{" + Environment.NewLine);
        //    sb.Append("					if (");
        //    int iNotNull = 0;
        //    bool bNotNullFound = false;
        //    bool bStringColFound = false;
        //    foreach (DataRowView drv in dv)
        //    {
        //        if (drv["AllowNulls"].ToString() == "N" && drv["ColumnIdentity"].ToString() == "N")
        //        {
        //            if (bNotNullFound) {sb.Append(" && ");} else {bNotNullFound = true;}
        //            sb.Append("rows[iRow][" + iNotNull.ToString() + "].ToString() == string.Empty");
        //        }
        //        iNotNull = iNotNull + 1;
        //        if (drv["DataTypeSysName"].ToString() == "String") {bStringColFound = true;}
        //    }
        //    if (!bNotNullFound) {throw new Exception("SpreadSheet import must have at least one non-nullable column. Please try again.");}
        //    sb.Append(") {rows[iRow].Delete();}" + Environment.NewLine);
        //    if (bStringColFound)
        //    {
        //        sb.Append("					else" + Environment.NewLine);
        //        sb.Append("					{" + Environment.NewLine);
        //        int iCol = 0;
        //        foreach (DataRowView drv in dv)
        //        {
        //            if (drv["DataTypeSysName"].ToString() == "String")
        //            {
        //                sb.Append("						rows[iRow][" + iCol.ToString() + "] = rows[iRow][" + iCol.ToString() + "].ToString().Replace(\"\\r\",\"\").Replace(\"\\n\",\"\");" + Environment.NewLine);
        //            }
        //            iCol = iCol + 1;
        //        }
        //        sb.Append("					}" + Environment.NewLine);
        //    }
        //    sb.Append("				}" + Environment.NewLine);
        //    sb.Append("				using (Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access dac = new Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access())" + Environment.NewLine);
        //    sb.Append("				{" + Environment.NewLine);
        //    sb.Append("					return dac.ImportRows" + wizardId.ToString() + "(bOverwrite,usrId,ds,iStart,fileName" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
        //    sb.Append("				}" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			catch(Exception e)" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				ApplicationAssert.CheckCondition(false, \"" + dw["ProgramName"].ToString() + "Rules\", \"\", e.Message); return 0;" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    //sb.Append("			finally" + Environment.NewLine);
        //    //sb.Append("			{" + Environment.NewLine);
        //    //sb.Append("				conn.Close(); conn = null;" + Environment.NewLine);
        //    //sb.Append("			}" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append("}" + Environment.NewLine);
        //    return sb;
        //}

		private void MakeRulesCsPrepRule(StringBuilder sb, DataView dvRule, string beforeCRUD)
		{
			foreach (DataRowView drv in dvRule)
			{
				if (drv["BeforeCRUD"].ToString() == beforeCRUD)
				{
					sb.Append("				" + drv["ProcedureName"].ToString() + "(bOverwrite,usrId,fileName,cn,tr);" + Environment.NewLine);
				}
			}
		}

        //private StringBuilder MakeAccessCs(DataRow dw, Int32 wizardId, DataView dv, DataView dvRule, string appDatabase, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("using System;" + Environment.NewLine);
        //    sb.Append("using System.Text;" + Environment.NewLine);
        //    sb.Append("using System.Data;" + Environment.NewLine);
        //    sb.Append("using System.Data.OleDb;" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ";" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ".Data;" + Environment.NewLine);
        //    if (CSrc.SrcSystemId != 3)	// Admin.
        //    {
        //        sb.Append("using RO.Common3;" + Environment.NewLine);
        //        sb.Append("using RO.Common3.Data;" + Environment.NewLine);
        //    }
        //    sb.Append("using RO.SystemFramewk;" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("namespace " + CPrj.EntityCode + ".Access" + CSrc.SrcSystemId.ToString() + Environment.NewLine);
        //    sb.Append("{" + Environment.NewLine);
        //    sb.Append("	public class " + dw["ProgramName"].ToString() + "Access : Encryption, IDisposable" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    sb.Append("		private OleDbDataAdapter da;" + Environment.NewLine);
        //    sb.Append("		private DataRowCollection rows;" + Environment.NewLine);
        //    sb.Append("		private int col;" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		public " + dw["ProgramName"].ToString() + "Access()" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			da = new OleDbDataAdapter();" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		public void Dispose()" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			Dispose(true);" + Environment.NewLine);
        //    sb.Append("			GC.SuppressFinalize(true); // as a service to those who might inherit from us" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		protected virtual void Dispose(bool disposing)" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			if (!disposing)" + Environment.NewLine);
        //    sb.Append("				return;" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("			if (da != null)" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("				if(da.SelectCommand != null)" + Environment.NewLine);
        //    sb.Append("				{" + Environment.NewLine);
        //    sb.Append("					if( da.SelectCommand.Connection != null)" + Environment.NewLine);
        //    sb.Append("					{" + Environment.NewLine);
        //    sb.Append("						da.SelectCommand.Connection.Dispose();" + Environment.NewLine);
        //    sb.Append("					}" + Environment.NewLine);
        //    sb.Append("					da.SelectCommand.Dispose();" + Environment.NewLine);
        //    sb.Append("				}" + Environment.NewLine);
        //    sb.Append("				da.Dispose();" + Environment.NewLine);
        //    sb.Append("				da = null;" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		private void ImportRow(string ImportFileName, DataRow row, OleDbConnection cn, OleDbTransaction tr)" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			OleDbCommand cmd = new OleDbCommand(\"Wiz" + dw["ProgramName"].ToString() + "\", cn);" + Environment.NewLine);
        //    sb.Append("			cmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine);
        //    sb.Append("			cmd.Parameters.Add(\"@ImportFileName\", OleDbType.VarChar).Value = ImportFileName;" + Environment.NewLine);
        //    int ii = 0;
        //    int jj = 1;
        //    foreach (DataRowView drv in dv)
        //    {
        //        sb.Append("			col = " + jj.ToString() + ";" + Environment.NewLine);
        //        if (drv["TableId"].ToString() != string.Empty)
        //        {
        //            if (drv["RequiredValid"].ToString() == "N")
        //            {
        //                sb.Append("			if (row[" + ii.ToString() + "].ToString().Trim() == string.Empty)" + Environment.NewLine);
        //                sb.Append("			{" + Environment.NewLine);
        //                sb.Append("				cmd.Parameters.Add(\"@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = System.DBNull.Value;" + Environment.NewLine);
        //                sb.Append("			}" + Environment.NewLine);
        //                sb.Append("			else" + Environment.NewLine);
        //                sb.Append("			{" + Environment.NewLine);
        //                sb.Append("	");
        //            }
        //            if (drv["DataTypeSByteOle"].ToString() == drv["DataTypeDByteOle"].ToString())
        //            {
        //                sb.Append("			cmd.Parameters.Add(\"@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = " + Robot.ParseRequired(drv["DataTypeSysName"].ToString(),true) + "row[" + ii.ToString() + "].ToString().Trim()");
        //                if (drv["DataTypeSByteOle"].ToString() == "Currency") {sb.Append(",System.Globalization.NumberStyles.Currency");}
        //                sb.Append(Robot.ParseRequired(drv["DataTypeSysName"].ToString(),false) + ";" + Environment.NewLine);
        //            }
        //            else
        //            {
        //                sb.Append("			if (Config.DoubleByteDb) {cmd.Parameters.Add(\"@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString() + "\", OleDbType." + drv["DataTypeDByteOle"].ToString() + ").Value = " + Robot.ParseRequired(drv["DataTypeSysName"].ToString(),true) + "row[" + ii.ToString() + "].ToString().Trim()" + Robot.ParseRequired(drv["DataTypeSysName"].ToString(),false) + ";} else {cmd.Parameters.Add(\"@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = " + Robot.ParseRequired(drv["DataTypeSysName"].ToString(),true) + "row[" + ii.ToString() + "].ToString().Trim()" + Robot.ParseRequired(drv["DataTypeSysName"].ToString(),false) + ";}" + Environment.NewLine);
        //            }
        //            if (drv["RequiredValid"].ToString() == "N") {sb.Append("			}" + Environment.NewLine);}
        //        }
        //        ii = ii + 1; jj = jj + 1;
        //    }
        //    sb.Append("			cmd.CommandTimeout = 1800;" + Environment.NewLine);
        //    sb.Append("			cmd.Transaction = tr;" + Environment.NewLine);
        //    sb.Append("			cmd.ExecuteNonQuery();" + Environment.NewLine);
        //    sb.Append("			cmd.Dispose();" + Environment.NewLine);
        //    sb.Append("			cmd = null;" + Environment.NewLine);
        //    sb.Append("			return;" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		public int ImportRows" + wizardId.ToString() + "(bool bOverwrite, Int32 usrId, DataSet ds, int iStart, string fileName" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			int ii = 1;" + Environment.NewLine);
        //    sb.Append("			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}" + Environment.NewLine);
        //    sb.Append("			OleDbConnection cn =  new OleDbConnection(" + Robot.GetCnPass("N","N") + ");" + Environment.NewLine);
        //    sb.Append("			cn.Open();" + Environment.NewLine);
        //    sb.Append("			OleDbTransaction tr = cn.BeginTransaction();" + Environment.NewLine);
        //    sb.Append("			try" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    MakeRulesCsPrepRule(sb, dvRule, "Y");
        //    sb.Append("				rows = ds.Tables[0].Rows;" + Environment.NewLine);
        //    sb.Append("				for ( ii = iStart; ii < rows.Count; ii++ )" + Environment.NewLine);	// Ignore the header row[0].
        //    sb.Append("				{" + Environment.NewLine);
        //    sb.Append("					if (rows[ii].RowState != System.Data.DataRowState.Deleted)" + Environment.NewLine);
        //    sb.Append("					{" + Environment.NewLine);
        //    sb.Append("						try {ImportRow(fileName, rows[ii], cn, tr);}" + Environment.NewLine);
        //    sb.Append("						catch (Exception e)" + Environment.NewLine);
        //    sb.Append("						{" + Environment.NewLine);
        //    sb.Append("							ApplicationAssert.CheckCondition(false, \"" + dw["ProgramName"].ToString() + "Access\", \"\", \"Row \" + (ii + 1).ToString() + \", Col \" + Utils.Num2ExcelCol(col) + \" \" + e.Message);" + Environment.NewLine);
        //    sb.Append("						}" + Environment.NewLine);
        //    sb.Append("					}" + Environment.NewLine);
        //    sb.Append("				}" + Environment.NewLine);
        //    MakeRulesCsPrepRule(sb, dvRule, "N");
        //    sb.Append("				tr.Commit();" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			catch (Exception e)" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				tr.Rollback();" + Environment.NewLine);
        //    sb.Append("				ApplicationAssert.CheckCondition(false, \"" + dw["ProgramName"].ToString() + "Access\", \"\", e.Message);" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			finally" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				cn.Close();" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			if ( ds.HasErrors )" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				ds.Tables[\"" + dw["ProgramName"].ToString() + "\"].GetErrors()[0].ClearErrors();" + Environment.NewLine);
        //    sb.Append("				return 0;" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			else" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				ds.AcceptChanges();" + Environment.NewLine);
        //    sb.Append("				return (rows.Count - iStart);" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    foreach (DataRowView drv in dvRule)
        //    {
        //        sb.Append(Environment.NewLine);
        //        sb.Append("		public void " + drv["ProcedureName"].ToString() + "(bool bOverwrite, Int32 usrId, string ImportFileName, OleDbConnection cn, OleDbTransaction tr)" + Environment.NewLine);
        //        sb.Append("		{" + Environment.NewLine);
        //        sb.Append("			OleDbCommand cmd = new OleDbCommand(\"" + drv["ProcedureName"].ToString() + "\", cn);" + Environment.NewLine);
        //        sb.Append("			cmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine);
        //        sb.Append("			if (bOverwrite) {cmd.Parameters.Add(\"@Overwrite\", OleDbType.Char).Value = \"Y\";} else {cmd.Parameters.Add(\"@Overwrite\", OleDbType.Char).Value = \"N\";}" + Environment.NewLine);
        //        sb.Append("			cmd.Parameters.Add(\"@UsrId\", OleDbType.Numeric).Value = usrId;" + Environment.NewLine);
        //        sb.Append("			cmd.Parameters.Add(\"@ImportFileName\", OleDbType.VarChar).Value = ImportFileName;" + Environment.NewLine);
        //        sb.Append("			cmd.Transaction = tr;" + Environment.NewLine);
        //        sb.Append("			cmd.CommandTimeout = 1800;" + Environment.NewLine);
        //        sb.Append("			cmd.ExecuteNonQuery();" + Environment.NewLine);
        //        sb.Append("			cmd.Dispose();" + Environment.NewLine);
        //        sb.Append("			cmd = null;" + Environment.NewLine);
        //        sb.Append("			return;" + Environment.NewLine);
        //        sb.Append("		}" + Environment.NewLine);
        //    }
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append("}" + Environment.NewLine);
        //    return sb;
        //}
	}
}