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

	public class GenReportsRules
	{
		public bool DeleteProgram(string GenPrefix, string programName, Int32 reportId, string appDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar)
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
				// Delete web service tier programs:
				//DeleteProgW(programName, CPrj, CPrj.SrcWsProgramPath, CPrj.SrcClientFrwork);
				// Delete target client tier programs:
                //if (CPrj.SrcClientProgramPath != CPrj.TarClientProgramPath)
                //{
                //    DeleteProgW(programName, CPrj, CPrj.TarWsProgramPath, CPrj.TarClientFrwork);
                //}
				// Delete source rule tier programs:
				//DeleteProgR(programName, CPrj, CSrc, CPrj.SrcRuleProgramPath, CPrj.SrcRuleFrwork);
				// Delete target rule tier programs:
                //if (CPrj.SrcRuleProgramPath != CPrj.TarRuleProgramPath)
                //{
                //    DeleteProgR(programName, CPrj, CSrc, CPrj.TarRuleProgramPath, CPrj.TarRuleFrwork);
                //}
				// Delete source data tier programs:
				DeleteProgD(GenPrefix, CSrc.SrcDbDatabase, appDatabase, CPrj.SrcDesDatabase, programName, reportId, CSrc.SrcConnectionString, CSrc.SrcDbPassword);
				// Delete target data tier programs:
				if (CTar.TarDbServer != CSrc.SrcDbServer && CPrj.SrcDesProviderCd == "M")
				{
					DeleteProgD(GenPrefix, CTar.TarDbDatabase, appDatabase, CPrj.TarDesDatabase, programName, reportId, CTar.TarConnectionString, CTar.TarDbPassword);
				}
			}
			catch (Exception e) {ApplicationAssert.CheckCondition(false,"","",e.Message); return false;}
			return true;
		}
		
		private void DeleteProgC(string programName, CurrPrj CPrj, string clientProgramPath, string clientFrwork)
		{
            //if (clientFrwork == "1")
            //{
            //    Robot.ModifyCsproj(true, clientProgramPath + @"Web.csproj", programName + ".aspx", clientFrwork, string.Empty);
            //    Robot.ModifyCsproj(true, clientProgramPath + @"Web.csproj", programName + ".aspx.cs", clientFrwork, string.Empty);
            //    Robot.ModifyCsproj(true, clientProgramPath + @"Web.csproj", @"reports\" + programName + "Module.ascx", clientFrwork, string.Empty);
            //    Robot.ModifyCsproj(true, clientProgramPath + @"Web.csproj", @"reports\" + programName + "Module.ascx.cs", clientFrwork, string.Empty);
            //}
			FileInfo fi = null;
			fi = new FileInfo(clientProgramPath + programName + ".aspx"); if (fi.Exists) {fi.Delete();}
			fi = new FileInfo(clientProgramPath + programName + ".aspx.cs"); if (fi.Exists) {fi.Delete();}
			fi = new FileInfo(clientProgramPath + @"reports\" + programName + "Module.ascx"); if (fi.Exists) {fi.Delete();}
			fi = new FileInfo(clientProgramPath + @"reports\" + programName + "Module.ascx.cs"); if (fi.Exists) {fi.Delete();}
			fi = new FileInfo(clientProgramPath + @"reports\" + programName + "Report.rdl"); if (fi.Exists) { fi.Delete(); }
		}

        //private void DeleteProgW(string programName, CurrPrj CPrj, string wsProgramPath, string clientFrwork)
        //{
        //    FileInfo fi = null;
        //    fi = new FileInfo(wsProgramPath + programName + "Ws.asmx"); if (fi.Exists) { fi.Delete(); }
        //}

        //private void DeleteProgR(string programName, CurrPrj CPrj, CurrSrc CSrc, string ruleProgramPath, string ruleFrwork)
        //{
        //    Robot.ModifyCsproj(true, ruleProgramPath + "Service" + CSrc.SrcSystemId.ToString() + "\\Service" + CSrc.SrcSystemId.ToString() + ".csproj", programName + "Ws.cs", string.Empty, ruleFrwork);
        //    Robot.ModifyCsproj(true, ruleProgramPath + "Facade" + CSrc.SrcSystemId.ToString() + "\\Facade" + CSrc.SrcSystemId.ToString() + ".csproj", programName + "System.cs", string.Empty, ruleFrwork);
        //    Robot.ModifyCsproj(true, ruleProgramPath + "Access" + CSrc.SrcSystemId.ToString() + "\\Access" + CSrc.SrcSystemId.ToString() + ".csproj", programName + "Access.cs", string.Empty, ruleFrwork);
        //    Robot.ModifyCsproj(true, ruleProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Common" + CSrc.SrcSystemId.ToString() + ".csproj", @"Data\Ds" + programName + ".cs", string.Empty, ruleFrwork);
        //    Robot.ModifyCsproj(true, ruleProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Common" + CSrc.SrcSystemId.ToString() + ".csproj", @"Data\Ds" + programName + "In.cs", string.Empty, ruleFrwork);
        //    FileInfo fi = null;
        //    fi = new FileInfo(ruleProgramPath + "Service" + CSrc.SrcSystemId.ToString() + "\\" + programName + "Ws.cs"); if (fi.Exists) { fi.Delete(); }
        //    fi = new FileInfo(ruleProgramPath + "Facade" + CSrc.SrcSystemId.ToString() + "\\" + programName + "System.cs"); if (fi.Exists) { fi.Delete(); }
        //    fi = new FileInfo(ruleProgramPath + "Access" + CSrc.SrcSystemId.ToString() + "\\" + programName + "Access.cs"); if (fi.Exists) {fi.Delete();}
        //    fi = new FileInfo(ruleProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Data\\Ds" + programName + ".cs"); if (fi.Exists) {fi.Delete();}
        //    fi = new FileInfo(ruleProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Data\\Ds" + programName + "In.cs"); if (fi.Exists) {fi.Delete();}
        //}

		private void DeleteProgD(string GenPrefix, string dbDatabase, string appDatabase, string desDatabase, string programName, Int32 reportId, string dbConnectionString, string dbPassword)
		{
			using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
			{
				dac.DelReportDel(GenPrefix, dbDatabase, appDatabase, desDatabase, programName, dbConnectionString, dbPassword);
			}
			DataView dvCri = null;
			using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
			{
				dvCri = new DataView(dac.GetReportCriDel(GenPrefix, reportId, dbConnectionString, dbPassword));
			}
			if (dvCri != null)
			{
				foreach (DataRowView drv in dvCri)
				{
					using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
					{
						dac.DelReportCriDel(GenPrefix, appDatabase, reportId, drv["ProcedureName"].ToString(), dbConnectionString, dbPassword);
					}
				}
			}
		}

        public bool CreateProgram(string GenPrefix, Int32 reportId, string reportTitle, string dbAppDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
			DataTable dt = null;
			using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
			{
				dt = dac.GetReportById(GenPrefix, reportId, CPrj, CSrc);
			}
			if (dt.Rows[0]["GenerateRp"].ToString() == "Y")
			{
				DataView dvCri = null;
				using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
				{
					dvCri = new DataView(dac.GetReportCriteria(GenPrefix, reportId, CPrj, CSrc));
				}
				DataView dvObj = null;
				using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
				{
					dvObj = new DataView(dac.GetReportColumns(GenPrefix, reportId, CPrj, CSrc));
				}
				DataView dvGrp = null;
				using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
				{
					dvGrp = new DataView(dac.GetCriReportGrp(GenPrefix, reportId, CPrj, CSrc));
				}
				try
				{
					// Create source rule tier programs before CreateProgW:
					//CreateProgR(dt.Rows[0], reportId, dvCri, dvObj, CPrj, CSrc, CPrj.SrcRuleProgramPath, CPrj.SrcRuleFrwork);
                    // Create target rule tier programs:
                    //if (CPrj.SrcRuleProgramPath != CPrj.TarRuleProgramPath)
                    //{
                    //    CreateProgR(dt.Rows[0], reportId, dvCri, dvObj, CPrj, CSrc, CPrj.TarRuleProgramPath, CPrj.TarRuleFrwork);
                    //}
					// Create source Web Service tier programs:
					//CreateProgW(dt.Rows[0], reportId, dvCri, CPrj, CSrc);
					// Create source client tier programs:
					CreateProgC(GenPrefix, dt.Rows[0], reportId, reportTitle, dbAppDatabase, dvCri, dvObj, dvGrp, CPrj, CSrc, CPrj.SrcClientProgramPath, CPrj.SrcClientFrwork);
					// Create target client tier programs:
					if (CPrj.SrcClientProgramPath != CPrj.TarClientProgramPath)
					{
						CreateProgC(GenPrefix, dt.Rows[0], reportId, reportTitle, dbAppDatabase, dvCri, dvObj, dvGrp, CPrj, CSrc, CPrj.TarClientProgramPath, CPrj.TarClientFrwork);
					}
					// Create source data tier programs:
					using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
					{
						dac.MkReportGet(GenPrefix, reportId, dt.Rows[0]["ProgramName"].ToString(), CSrc, dt.Rows[0]["dbAppDatabase"].ToString(), dt.Rows[0]["dbDesDatabase"].ToString());
						dac.MkReportUpd(GenPrefix, reportId, dt.Rows[0]["ProgramName"].ToString(), CSrc, dt.Rows[0]["dbAppDatabase"].ToString(), dt.Rows[0]["dbDesDatabase"].ToString());
					}
					// Create target data tier programs:
					if (CTar.TarDbServer != CSrc.SrcDbServer && CPrj.SrcDesProviderCd == "M")
					{
						CreateProgD(dt.Rows[0], reportId, dbAppDatabase, dvCri, dvObj, CPrj, CSrc, CTar, dbConnectionString, dbPassword);
					}
                    // Reset regen flag to No:
                    using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
                    {
                        dac.SetRptNeedRegen(reportId, CSrc);
                    }
                }
				catch (Exception e) {ApplicationAssert.CheckCondition(false,"","",e.Message); return false;}
			}
			return true;
		}

        //public void ProxyProgram(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    DataTable dt = null;
        //    using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
        //    {
        //        dt = dac.GetReportById(GenPrefix, reportId, CPrj, CSrc);
        //    }
        //    if (dt.Rows[0]["GenerateRp"].ToString() == "Y" && "C,G,X".IndexOf(dt.Rows[0]["ReportTypeCd"].ToString()) >= 0)
        //    {
        //        StreamWriter sw = new StreamWriter(CPrj.SrcRuleProgramPath + "Service" + CSrc.SrcSystemId.ToString() + "\\" + dt.Rows[0]["ProgramName"].ToString() + "Ws.cs");
        //        try { sw.Write(Robot.MkWsProxy(dt.Rows[0]["ProgramName"].ToString(), CPrj, CSrc)); }
        //        catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message); }
        //        finally { sw.Close(); }
        //        Robot.ModifyCsproj(false, CPrj.SrcRuleProgramPath + "Service" + CSrc.SrcSystemId.ToString() + "\\Service" + CSrc.SrcSystemId.ToString() + ".csproj", dt.Rows[0]["ProgramName"].ToString() + "Ws.cs", string.Empty, CPrj.SrcClientFrwork);
        //    }
        //}

        //private void CreateProgW(DataRow dw, Int32 reportId, DataView dvCri, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    if ("C,G,X".IndexOf(dw["ReportTypeCd"].ToString()) >= 0)
        //    {
        //        StreamWriter sw = new StreamWriter(CPrj.SrcWsProgramPath + dw["ProgramName"].ToString() + "Ws.asmx");
        //        try { sw.Write(MakeAsmx(dw, reportId, dvCri, CPrj, CSrc)); }
        //        finally { sw.Close(); }
        //    }
        //}

        private void CreateProgC(string GenPrefix, DataRow dw, Int32 reportId, string reportTitle, string dbAppDatabase, DataView dvCri, DataView dvObj, DataView dvGrp, CurrPrj CPrj, CurrSrc CSrc, string clientProgramPath, string clientFrwork)
		{
			StreamWriter sw = null;
			// Create rdl report format for SQL Reporting Service:
			if (dw["ReportTypeCd"].ToString() == "S")
			{
				sw = new StreamWriter(clientProgramPath + @"reports\" + dw["ProgramName"].ToString() + "Report.rdl");
				try { sw.Write(MakeRdl(GenPrefix, dw, reportId, dbAppDatabase, dvCri, dvObj, CSrc)); }
				finally { sw.Close(); }
			}
			else if ("C,G,X".IndexOf(dw["ReportTypeCd"].ToString()) >= 0)
			{
				sw = new StreamWriter(clientProgramPath + dw["ProgramName"].ToString() + ".aspx");
				try { sw.Write(MakeAspx(dw, reportId, reportTitle, CPrj, clientFrwork)); } finally { sw.Close(); }
				if (clientFrwork == "1")
				{
					Robot.ModifyCsproj(false, clientProgramPath + @"Web.csproj", dw["ProgramName"].ToString() + ".aspx", clientFrwork, string.Empty);
				}
				sw = new StreamWriter(clientProgramPath + dw["ProgramName"].ToString() + ".aspx.cs");
				try { sw.Write(MakeAspxCs(dw, reportTitle, CPrj, clientFrwork)); }
				finally { sw.Close(); }
				if (clientFrwork == "1")
				{
					Robot.ModifyCsproj(false, clientProgramPath + @"Web.csproj", dw["ProgramName"].ToString() + ".aspx.cs", clientFrwork, string.Empty);
				}
				sw = new StreamWriter(clientProgramPath + @"reports\" + dw["ProgramName"].ToString() + "Module.ascx");
				try { sw.Write(MakeAscx(dw, reportTitle, dvCri, dvObj, dvGrp, CPrj, clientFrwork)); }
				finally { sw.Close(); }
				if (clientFrwork == "1")
				{
					Robot.ModifyCsproj(false, clientProgramPath + @"Web.csproj", @"reports\" + dw["ProgramName"].ToString() + "Module.ascx", clientFrwork, string.Empty);
				}
				sw = new StreamWriter(clientProgramPath + @"reports\" + dw["ProgramName"].ToString() + "Module.ascx.cs");
				try 
                {
                    sw.Write(MakeAscxCs(dw, reportId, reportTitle, dvCri, dvObj, dvGrp, CPrj, CSrc, clientFrwork));
                }
				finally { sw.Close(); }
				if (clientFrwork == "1")
				{
					Robot.ModifyCsproj(false, clientProgramPath + @"Web.csproj", @"reports\" + dw["ProgramName"].ToString() + "Module.ascx.cs", clientFrwork, string.Empty);
				}
			}
		}

        //private void CreateProgR(DataRow dw, Int32 reportId, DataView dvCri, DataView dvObj, CurrPrj CPrj, CurrSrc CSrc, string ruleProgramPath, string ruleFrwork)
        //{
        //    if ("C,G,X".IndexOf(dw["ReportTypeCd"].ToString()) >= 0)
        //    {
        //        StreamWriter sw = new StreamWriter(ruleProgramPath + "Facade" + CSrc.SrcSystemId.ToString() + "\\" + dw["ProgramName"].ToString() + "System.cs");
        //        try { sw.Write(MakeSystemCs(dw, reportId, dvCri, CPrj, CSrc)); }
        //        finally { sw.Close(); }
        //        Robot.ModifyCsproj(false, ruleProgramPath + "Facade" + CSrc.SrcSystemId.ToString() + "\\Facade" + CSrc.SrcSystemId.ToString() + ".csproj", dw["ProgramName"].ToString() + "System.cs", string.Empty, ruleFrwork);
        //        sw = new StreamWriter(ruleProgramPath + "Access" + CSrc.SrcSystemId.ToString() + "\\" + dw["ProgramName"].ToString() + "Access.cs");
        //        try { sw.Write(MakeAccessCs(dw, reportId, dvCri, CPrj, CSrc)); }
        //        finally { sw.Close(); }
        //        Robot.ModifyCsproj(false, ruleProgramPath + "Access" + CSrc.SrcSystemId.ToString() + "\\Access" + CSrc.SrcSystemId.ToString() + ".csproj", dw["ProgramName"].ToString() + "Access.cs", string.Empty, ruleFrwork);
        //        //sw = new StreamWriter(ruleProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Data\\Ds" + dw["ProgramName"].ToString() + ".cs");
        //        //try { sw.Write(MakeDataCs(dw, dvObj, CPrj, CSrc)); }
        //        //finally { sw.Close(); }
        //        //Robot.ModifyCsproj(false, ruleProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Common" + CSrc.SrcSystemId.ToString() + ".csproj", @"Data\Ds" + dw["ProgramName"].ToString() + ".cs", string.Empty, ruleFrwork);
        //        if (dvCri.Count > 0)
        //        {
        //            sw = new StreamWriter(ruleProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Data\\Ds" + dw["ProgramName"].ToString() + "In.cs");
        //            try { sw.Write(MakeDataInCs(dw, dvCri, CPrj, CSrc)); }
        //            finally { sw.Close(); }
        //            Robot.ModifyCsproj(false, ruleProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Common" + CSrc.SrcSystemId.ToString() + ".csproj", @"Data\Ds" + dw["ProgramName"].ToString() + "In.cs", string.Empty, ruleFrwork);
        //        }
        //    }
        //}

		public void CreateProgD(DataRow dw, Int32 reportId, string dbAppDatabase, DataView dvCri, DataView dvObj, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
			foreach (DataRowView drv in dvCri)
			{
				if (",ComboBox,DropDownList,ListBox,RadioButtonList,".IndexOf(","+drv["DisplayName"].ToString()+",") >= 0)
				{
					PortProg(dbAppDatabase, "GetIn" + reportId.ToString() + drv["ColumnName"].ToString(), "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);
					PortProg(dbAppDatabase, "_GetIn" + drv["ColumnName"].ToString(), "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);
				}
			}
			PortProg(dbAppDatabase, "Get" + dw["ProgramName"].ToString(), "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);
			PortProg(dbAppDatabase, "Upd" + dw["ProgramName"].ToString(), "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);
			if (dw["RegClause"].ToString() != string.Empty) {PortProg(dbAppDatabase, "_Get" + dw["ProgramName"].ToString() + "R", "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);}
			if (dw["UpdClause"].ToString() != string.Empty) {PortProg(dbAppDatabase, "_Get" + dw["ProgramName"].ToString() + "U", "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);}
			if (dw["XlsClause"].ToString() != string.Empty) {PortProg(dbAppDatabase, "_Get" + dw["ProgramName"].ToString() + "X", "P", CPrj, CSrc, CTar, dbConnectionString, dbPassword);}
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

		private StringBuilder MakeRdl(string GenPrefix, DataRow dw, Int32 reportId, string dbAppDatabase, DataView dvCri, DataView dvObj, CurrSrc CSrc)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine);
			sb.Append("<Report xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition\">" + Environment.NewLine);
			sb.Append("	<TopMargin>" + dw["TopMargin"].ToString() + dw["UnitCd"].ToString() + "</TopMargin>");
			sb.Append("<BottomMargin>" + dw["BottomMargin"].ToString() + dw["UnitCd"].ToString() + "</BottomMargin>");
			sb.Append("<LeftMargin>" + dw["LeftMargin"].ToString() + dw["UnitCd"].ToString() + "</LeftMargin>");
			sb.Append("<RightMargin>" + dw["RightMargin"].ToString() + dw["UnitCd"].ToString() + "</RightMargin>" + Environment.NewLine);
			sb.Append("	<Width>" + dw["RptWidth"].ToString() + dw["UnitCd"].ToString() + "</Width>" + Environment.NewLine);
			sb.Append("	<PageWidth>" + dw["PageWidth"].ToString() + dw["UnitCd"].ToString() + "</PageWidth><PageHeight>" + dw["PageHeight"].ToString() + dw["UnitCd"].ToString() + "</PageHeight>" + Environment.NewLine);
//			sb.Append("	<Language>en-US</Language>" + Environment.NewLine);
			sb.Append("	<DataSources><DataSource Name=\"" + dbAppDatabase + "\"><DataSourceReference>" + dbAppDatabase + "</DataSourceReference></DataSource></DataSources>" + Environment.NewLine);
			sb.Append("	<DataSets>" + Environment.NewLine);
			sb.Append("		<DataSet Name=\"" + dbAppDatabase + "\">" + Environment.NewLine);
			sb.Append("		<Query>" + Environment.NewLine);
			sb.Append("			<DataSourceName>" + dbAppDatabase + "</DataSourceName>" + Environment.NewLine);
			sb.Append("			<CommandType>StoredProcedure</CommandType>" + Environment.NewLine);
			sb.Append("			<CommandText>Get" + dw["ProgramName"].ToString() + "</CommandText>" + Environment.NewLine);
			sb.Append("			<QueryParameters>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@reportId\"><Value>=Parameters!reportId.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@RowAuthoritys\"><Value>=Parameters!RowAuthoritys.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Usrs\"><Value>=Parameters!Usrs.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@UsrGroups\"><Value>=Parameters!UsrGroups.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Cultures\"><Value>=Parameters!Cultures.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Companys\"><Value>=Parameters!Companys.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Projects\"><Value>=Parameters!Projects.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Agents\"><Value>=Parameters!Agents.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Brokers\"><Value>=Parameters!Brokers.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Customers\"><Value>=Parameters!Customers.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Investors\"><Value>=Parameters!Investors.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Members\"><Value>=Parameters!Members.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@Vendors\"><Value>=Parameters!Vendors.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@currCompanyId\"><Value>=Parameters!currCompanyId.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@currProjectId\"><Value>=Parameters!currProjectId.Value</Value></QueryParameter>" + Environment.NewLine);
			foreach (DataRowView drv in dvCri)
			{
				sb.Append("				<QueryParameter Name=\"@" + drv["ColumnName"].ToString() + "\"><Value>=Parameters!" + drv["ColumnName"].ToString() + ".Value</Value></QueryParameter>" + Environment.NewLine);
			}
			sb.Append("				<QueryParameter Name=\"@bUpd\"><Value>=Parameters!bUpd.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@bXls\"><Value>=Parameters!bXls.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("				<QueryParameter Name=\"@bVal\"><Value>=Parameters!bVal.Value</Value></QueryParameter>" + Environment.NewLine);
			sb.Append("			</QueryParameters>" + Environment.NewLine);
			sb.Append("		</Query>" + Environment.NewLine);
			sb.Append("		<Fields>" + Environment.NewLine);
			foreach (DataRowView drv in dvObj)
			{
				if (drv["OperatorId"].ToString() == string.Empty)
				{
					sb.Append("			<Field Name=\"" + drv["ColumnName"].ToString() + "\"><DataField>" + drv["ColumnName"].ToString() + "</DataField></Field>" + Environment.NewLine);
				}
			}
			sb.Append("		</Fields>" + Environment.NewLine);
			sb.Append("		</DataSet>" + Environment.NewLine);
			sb.Append("	</DataSets>" + Environment.NewLine);
			sb.Append("	<ReportParameters>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"reportId\"><DataType>Integer</DataType><Prompt>reportId</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"RowAuthoritys\"><DataType>String</DataType><Prompt>RowAuthoritys</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Usrs\"><DataType>String</DataType><Prompt>Usrs</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"UsrGroups\"><DataType>String</DataType><Prompt>UsrGroups</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Cultures\"><DataType>String</DataType><Prompt>Cultures</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Companys\"><DataType>String</DataType><Prompt>Companys</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Projects\"><DataType>String</DataType><Prompt>Projects</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Agents\"><DataType>String</DataType><Prompt>Agents</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Brokers\"><DataType>String</DataType><Prompt>Brokers</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Customers\"><DataType>String</DataType><Prompt>Customers</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Investors\"><DataType>String</DataType><Prompt>Investors</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Members\"><DataType>String</DataType><Prompt>Members</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"Vendors\"><DataType>String</DataType><Prompt>Vendors</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"currCompanyId\"><DataType>Integer</DataType><Prompt>currCompanyId</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"currProjectId\"><DataType>Integer</DataType><Prompt>currProjectId</Prompt></ReportParameter>" + Environment.NewLine);
			foreach (DataRowView drv in dvCri)
			{
				sb.Append("		<ReportParameter Name=\"" + drv["ColumnName"].ToString() + "\">");
				if (drv["DataTypeSysName"].ToString() == "String") { sb.Append("<AllowBlank>true</AllowBlank>"); }
				if (drv["RequiredValid"].ToString() == "N") { sb.Append("<Nullable>true</Nullable>"); }
				sb.Append("<DataType>" + drv["DataTypeRdlParm"].ToString() + "</DataType><Prompt>" + drv["ColumnName"].ToString() + "</Prompt></ReportParameter>" + Environment.NewLine);
			}
			sb.Append("		<ReportParameter Name=\"bUpd\"><DataType>String</DataType><Prompt>bUpd</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"bXls\"><DataType>String</DataType><Prompt>bXls</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"bVal\"><DataType>String</DataType><Prompt>bVal</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"ReportTitle\"><DataType>String</DataType><Prompt>ReportTitle</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"UsrName\"><DataType>String</DataType><Prompt>UsrName</Prompt></ReportParameter>" + Environment.NewLine);
			sb.Append("		<ReportParameter Name=\"UrlBase\"><DataType>String</DataType><Prompt>UrlBase</Prompt></ReportParameter>" + Environment.NewLine);
			foreach (DataRowView drv in dvObj)
			{
				if (drv["OperatorId"].ToString() == string.Empty)
				{
					if (drv["RptObjTypeCd"].ToString() == "P")
					{
						sb.Append("		<ReportParameter Name=\"" + drv["ColumnName"].ToString() + "\">");
						if (drv["DataTypeSysName"].ToString() == "String") { sb.Append("<AllowBlank>true</AllowBlank>"); }
						sb.Append("<DataType>" + drv["DataTypeRdlParm"].ToString() + "</DataType><Prompt>" + drv["ColumnName"].ToString() + "</Prompt></ReportParameter>" + Environment.NewLine);
					}
					sb.Append("		<ReportParameter Name=\"L_" + drv["ColumnName"].ToString() + "\"><AllowBlank>true</AllowBlank><DataType>String</DataType><Prompt>L_" + drv["ColumnName"].ToString() + "</Prompt></ReportParameter>" + Environment.NewLine);
				}
			}
			sb.Append("	</ReportParameters>" + Environment.NewLine);
			DataView dvElm = null;
			using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
			{
				dvElm = new DataView(dac.GetReportElm(GenPrefix, reportId, CSrc));
			}
			foreach (DataRowView drv in dvElm)
			{
				sb.Append("	<" + drv["RptElmTypeName"].ToString() + ">" + Environment.NewLine);
				if (drv["RptElmTypeCd"].ToString() == "H" || drv["RptElmTypeCd"].ToString() == "F")	// PageHeader & PageFooter:
				{
					sb.Append("		<Height>" + drv["ElmHeight"].ToString() + "</Height>");
					MakeReportStyle(ref sb, drv, string.Empty, string.Empty);
					sb.Append("<PrintOnFirstPage>" + TrueFalse(drv["ElmPrintFirst"].ToString()) + "</PrintOnFirstPage>");
					sb.Append("<PrintOnLastPage>" + TrueFalse(drv["ElmPrintLast"].ToString()) + "</PrintOnLastPage>");
				}
				else if (drv["RptElmTypeCd"].ToString() == "B")	// Body:
				{
					sb.Append("		<Height>" + drv["ElmHeight"].ToString() + "</Height>");
					MakeReportStyle(ref sb, drv, string.Empty, string.Empty);
					if (drv["ElmColumns"].ToString() != null && drv["ElmColumns"].ToString() != string.Empty) { sb.Append("<Columns>" + drv["ElmColumns"].ToString() + "</Columns>"); }
					if (drv["ElmColSpacing"].ToString() != null && drv["ElmColSpacing"].ToString() != string.Empty) { sb.Append("<ColumnSpacing>" + drv["ElmColSpacing"].ToString() + "</ColumnSpacing>"); }
				}
				sb.Append(Environment.NewLine);
				MakeReportCtr(GenPrefix, ref sb, dbAppDatabase, null, drv["RptElmId"].ToString(), null, "		", CSrc);
				sb.Append("	</" + drv["RptElmTypeName"].ToString() + ">" + Environment.NewLine);
			}
			sb.Append("</Report>" + Environment.NewLine);
			sb.Append("" + Environment.NewLine);
			return sb;
		}

		private string TrueFalse(string cb)
		{
			if (cb == "Y") return "true"; else return "false";
		}

		// Report Style:
		private void MakeReportStyle(ref StringBuilder sb, DataRowView drv, string pfix2, string sfix)
		{
			if (drv["RptStyleId"].ToString() != null && drv["RptStyleId"].ToString() != string.Empty)
			{
				sb.Append(pfix2 + "<Style>");
				if ((drv["BorderColorD"].ToString() != null && drv["BorderColorD"].ToString() != string.Empty)
					|| (drv["BorderColorL"].ToString() != null && drv["BorderColorL"].ToString() != string.Empty)
					|| (drv["BorderColorR"].ToString() != null && drv["BorderColorR"].ToString() != string.Empty)
					|| (drv["BorderColorT"].ToString() != null && drv["BorderColorT"].ToString() != string.Empty)
					|| (drv["BorderColorB"].ToString() != null && drv["BorderColorB"].ToString() != string.Empty))
				{
					sb.Append("<BorderColor>");
					if (drv["BorderColorD"].ToString() != null && drv["BorderColorD"].ToString() != string.Empty) { sb.Append("<Default>" + drv["BorderColorD"].ToString() + "</Default>"); }
					if (drv["BorderColorL"].ToString() != null && drv["BorderColorL"].ToString() != string.Empty) { sb.Append("<Left>" + drv["BorderColorL"].ToString() + "</Left>"); }
					if (drv["BorderColorR"].ToString() != null && drv["BorderColorR"].ToString() != string.Empty) { sb.Append("<Right>" + drv["BorderColorR"].ToString() + "</Right>"); }
					if (drv["BorderColorT"].ToString() != null && drv["BorderColorT"].ToString() != string.Empty) { sb.Append("<Top>" + drv["BorderColorT"].ToString() + "</Top>"); }
					if (drv["BorderColorB"].ToString() != null && drv["BorderColorB"].ToString() != string.Empty) { sb.Append("<Bottom>" + drv["BorderColorB"].ToString() + "</Bottom>"); }
					sb.Append("</BorderColor>");
				}
				if ((drv["BorderStyleD"].ToString() != null && drv["BorderStyleD"].ToString() != string.Empty)
					|| (drv["BorderStyleL"].ToString() != null && drv["BorderStyleL"].ToString() != string.Empty)
					|| (drv["BorderStyleR"].ToString() != null && drv["BorderStyleR"].ToString() != string.Empty)
					|| (drv["BorderStyleT"].ToString() != null && drv["BorderStyleT"].ToString() != string.Empty)
					|| (drv["BorderStyleB"].ToString() != null && drv["BorderStyleB"].ToString() != string.Empty))
				{
					sb.Append("<BorderStyle>");
					if (drv["BorderStyleD"].ToString() != null && drv["BorderStyleD"].ToString() != string.Empty) { sb.Append("<Default>" + drv["BorderStyleD"].ToString() + "</Default>"); }
					if (drv["BorderStyleL"].ToString() != null && drv["BorderStyleL"].ToString() != string.Empty) { sb.Append("<Left>" + drv["BorderStyleL"].ToString() + "</Left>"); }
					if (drv["BorderStyleR"].ToString() != null && drv["BorderStyleR"].ToString() != string.Empty) { sb.Append("<Right>" + drv["BorderStyleR"].ToString() + "</Right>"); }
					if (drv["BorderStyleT"].ToString() != null && drv["BorderStyleT"].ToString() != string.Empty) { sb.Append("<Top>" + drv["BorderStyleT"].ToString() + "</Top>"); }
					if (drv["BorderStyleB"].ToString() != null && drv["BorderStyleB"].ToString() != string.Empty) { sb.Append("<Bottom>" + drv["BorderStyleB"].ToString() + "</Bottom>"); }
					sb.Append("</BorderStyle>");
				}
				if ((drv["BorderWidthD"].ToString() != null && drv["BorderWidthD"].ToString() != string.Empty)
					|| (drv["BorderWidthL"].ToString() != null && drv["BorderWidthL"].ToString() != string.Empty)
					|| (drv["BorderWidthR"].ToString() != null && drv["BorderWidthR"].ToString() != string.Empty)
					|| (drv["BorderWidthT"].ToString() != null && drv["BorderWidthT"].ToString() != string.Empty)
					|| (drv["BorderWidthB"].ToString() != null && drv["BorderWidthB"].ToString() != string.Empty))
				{
					sb.Append("<BorderWidth>");
					if (drv["BorderWidthD"].ToString() != null && drv["BorderWidthD"].ToString() != string.Empty) { sb.Append("<Default>" + drv["BorderWidthD"].ToString() + "pt</Default>"); }
					if (drv["BorderWidthL"].ToString() != null && drv["BorderWidthL"].ToString() != string.Empty) { sb.Append("<Left>" + drv["BorderWidthL"].ToString() + "pt</Left>"); }
					if (drv["BorderWidthR"].ToString() != null && drv["BorderWidthR"].ToString() != string.Empty) { sb.Append("<Right>" + drv["BorderWidthR"].ToString() + "pt</Right>"); }
					if (drv["BorderWidthT"].ToString() != null && drv["BorderWidthT"].ToString() != string.Empty) { sb.Append("<Top>" + drv["BorderWidthT"].ToString() + "pt</Top>"); }
					if (drv["BorderWidthB"].ToString() != null && drv["BorderWidthB"].ToString() != string.Empty) { sb.Append("<Bottom>" + drv["BorderWidthB"].ToString() + "pt</Bottom>"); }
					sb.Append("</BorderWidth>");
				}
				if (drv["Color"].ToString() != null && drv["Color"].ToString() != string.Empty) { sb.Append("<Color>" + drv["Color"].ToString() + "</Color>"); }
				if (drv["BgColor"].ToString() != null && drv["BgColor"].ToString() != string.Empty)
				{
					sb.Append("<BackgroundColor>" + drv["BgColor"].ToString() + "</BackgroundColor>");
				}
				else { sb.Append("<BackgroundColor>White</BackgroundColor>"); }
				if (drv["BgGradType"].ToString() != null && drv["BgGradType"].ToString() != string.Empty) { sb.Append("<BackgroundGradientType>" + drv["BgGradType"].ToString() + "</BackgroundGradientType>"); }
				if (drv["BgGradColor"].ToString() != null && drv["BgGradColor"].ToString() != string.Empty) { sb.Append("<BackgroundGradientEndColor>" + drv["BgGradColor"].ToString() + "</BackgroundGradientEndColor>"); }
				if (drv["BgImage"].ToString() != null && drv["BgImage"].ToString() != string.Empty) { sb.Append("<BackgroundImage><Source>External</Source><Value>" + drv["BgImage"].ToString() + "</Value></BackgroundImage>"); }
				if (drv["LineHeight"].ToString() != null && drv["LineHeight"].ToString() != string.Empty) { sb.Append("<LineHeight>" + drv["LineHeight"].ToString() + "pt</LineHeight>"); }
				if (drv["Format"].ToString() != null && drv["Format"].ToString() != string.Empty) { sb.Append("<Format>" + drv["Format"].ToString() + "</Format>"); }
				if (drv["FontStyle"].ToString() != null && drv["FontStyle"].ToString() != string.Empty) { sb.Append("<FontStyle>" + drv["FontStyle"].ToString() + "</FontStyle>"); }
				if (drv["FontFamily"].ToString() != null && drv["FontFamily"].ToString() != string.Empty) { sb.Append("<FontFamily>" + drv["FontFamily"].ToString() + "</FontFamily>"); }
				if (drv["FontSize"].ToString() != null && drv["FontSize"].ToString() != string.Empty) { sb.Append("<FontSize>" + drv["FontSize"].ToString() + "pt</FontSize>"); }
				if (drv["FontWeight"].ToString() != null && drv["FontWeight"].ToString() != string.Empty) { sb.Append("<FontWeight>" + drv["FontWeight"].ToString() + "</FontWeight>"); }
				if (drv["TextDecor"].ToString() != null && drv["TextDecor"].ToString() != string.Empty) { sb.Append("<TextDecoration>" + drv["TextDecor"].ToString() + "</TextDecoration>"); }
				if (drv["TextAlign"].ToString() != null && drv["TextAlign"].ToString() != string.Empty) { sb.Append("<TextAlign>" + drv["TextAlign"].ToString() + "</TextAlign>"); }
				if (drv["VerticalAlign"].ToString() != null && drv["VerticalAlign"].ToString() != string.Empty) { sb.Append("<VerticalAlign>" + drv["VerticalAlign"].ToString() + "</VerticalAlign>"); }
				if (drv["PadLeft"].ToString() != null && drv["PadLeft"].ToString() != string.Empty) { sb.Append("<PaddingLeft>" + drv["PadLeft"].ToString() + "pt</PaddingLeft>"); }
				if (drv["PadRight"].ToString() != null && drv["PadRight"].ToString() != string.Empty) { sb.Append("<PaddingRight>" + drv["PadRight"].ToString() + "pt</PaddingRight>"); }
				if (drv["PadTop"].ToString() != null && drv["PadTop"].ToString() != string.Empty) { sb.Append("<PaddingTop>" + drv["PadTop"].ToString() + "pt</PaddingTop>"); }
				if (drv["PadBottom"].ToString() != null && drv["PadBottom"].ToString() != string.Empty) { sb.Append("<PaddingBottom>" + drv["PadBottom"].ToString() + "pt</PaddingBottom>"); }
				if (drv["Direction"].ToString() != null && drv["Direction"].ToString() != string.Empty) { sb.Append("<Direction>" + drv["Direction"].ToString() + "</Direction>"); }
				if (drv["WritingMode"].ToString() != null && drv["WritingMode"].ToString() != string.Empty) { sb.Append("<WritingMode>" + drv["WritingMode"].ToString() + "</WritingMode>"); }
				sb.Append("</Style>" + sfix);
			}
			else { sb.Append(pfix2 + "<Style><BackgroundColor>White</BackgroundColor></Style>" + sfix);}
		}

		// Report Controls:
		private bool MakeReportCtr(string GenPrefix, ref StringBuilder sb, string dbAppDatabase, string PRptCtrId, string RptElmId, string RptCelId, string sIndent, CurrSrc CSrc)
		{
			string pfix1;
			string pfix2;
			string sfix;
			DataView dvCtr = null;
			using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
			{
				dvCtr = new DataView(dac.GetReportCtr(GenPrefix, PRptCtrId, RptElmId, RptCelId, CSrc));
			}
			if (dvCtr.Count > 0)
			{
				sb.Append(sIndent + "<ReportItems>" + Environment.NewLine);
				foreach (DataRowView drv in dvCtr)
				{
					if (drv["HasChildCtr"].ToString() == "Y")
					{
						pfix1 = sIndent + "	"; pfix2 = sIndent + "		"; sfix = Convert.ToString(Environment.NewLine);
					}
					else
					{
						pfix1 = string.Empty; pfix2 = string.Empty; sfix = string.Empty;
					}
					sb.Append(sIndent + "	<" + drv["RptCtrTypeName"].ToString() + " Name=\"" + drv["RptCtrName"].ToString() + "\">" + sfix);
					if (drv["CtrTop"].ToString() != null && drv["CtrTop"].ToString() != string.Empty) { sb.Append(pfix2 + "<Top>" + drv["CtrTop"].ToString() + "</Top>" + sfix); }
					if (drv["CtrLeft"].ToString() != null && drv["CtrLeft"].ToString() != string.Empty) { sb.Append(pfix2 + "<Left>" + drv["CtrLeft"].ToString() + "</Left>" + sfix); }
					if (drv["CtrHeight"].ToString() != null && drv["CtrHeight"].ToString() != string.Empty) { sb.Append(pfix2 + "<Height>" + drv["CtrHeight"].ToString() + "</Height>" + sfix); }
					if (drv["CtrWidth"].ToString() != null && drv["CtrWidth"].ToString() != string.Empty) { sb.Append(pfix2 + "<Width>" + drv["CtrWidth"].ToString() + "</Width>" + sfix); }
					if (drv["CtrZIndex"].ToString() != null && drv["CtrZIndex"].ToString() != string.Empty) { sb.Append(pfix2 + "<ZIndex>" + drv["CtrZIndex"].ToString() + "</ZIndex>" + sfix); }
					if (drv["CtrAction"].ToString() != null && drv["CtrAction"].ToString() != string.Empty) { sb.Append(pfix2 + drv["CtrAction"].ToString() + sfix); }
					if (drv["CtrVisibility"].ToString() != null && drv["CtrVisibility"].ToString() != string.Empty) { sb.Append(pfix2 + drv["CtrVisibility"].ToString() + sfix); }
					if (drv["CtrToolTip"].ToString() != null && drv["CtrToolTip"].ToString() != string.Empty) { sb.Append(pfix2 + "<ToolTip>" + drv["CtrToolTip"].ToString() + "</ToolTip>" + sfix); }
					MakeReportStyle(ref sb, drv, pfix2, sfix);
					if (drv["RptCtrTypeCd"].ToString() == "R")	// Rectangle:
					{
						sb.Append(pfix2 + "<PageBreakAtStart>" + TrueFalse(drv["CtrPgBrStart"].ToString()) + "</PageBreakAtStart>" + sfix);
						sb.Append(pfix2 + "<PageBreakAtEnd>" + TrueFalse(drv["CtrPgBrEnd"].ToString()) + "</PageBreakAtEnd>" + sfix);
					}
					else if (drv["RptCtrTypeCd"].ToString() == "X")	// TextBox:
					{
						sb.Append(pfix2 + "<Value>" + drv["CtrValue"].ToString() + "</Value>" + sfix);
						sb.Append(pfix2 + "<CanGrow>" + TrueFalse(drv["CtrCanGrow"].ToString()) + "</CanGrow>" + sfix);
						sb.Append(pfix2 + "<CanShrink>" + TrueFalse(drv["CtrCanShrink"].ToString()) + "</CanShrink>" + sfix);
					}
					else if (drv["RptCtrTypeCd"].ToString() == "I")	// Image:
					{
						sb.Append(pfix2 + "<Source>External</Source><Value>" + drv["CtrValue"].ToString() + "</Value>" + sfix);
					}
					else if (drv["RptCtrTypeCd"].ToString() == "C")	// Chart:
					{
						sb.Append(pfix2 + "<DataSetName>" + dbAppDatabase + "</DataSetName>" + sfix);
						sb.Append(pfix2 + "<PointWidth>0</PointWidth><Palette>Default</Palette><PlotArea><Style><BorderStyle><Default>None</Default></BorderStyle><BackgroundColor>White</BackgroundColor></Style></PlotArea>" + sfix);
						sb.Append(pfix2 + "<Legend><Visible>true</Visible><Style><BorderStyle><Default>Solid</Default></BorderStyle><FontSize>7pt</FontSize></Style><Position>BottomCenter</Position><Layout>Table</Layout></Legend>" + sfix);
					}
					else if ("T,L".IndexOf(drv["RptCtrTypeCd"].ToString()) >= 0)	// Table, List:
					{
						sb.Append(pfix2 + "<DataSetName>" + dbAppDatabase + "</DataSetName>" + sfix);
						sb.Append(pfix2 + "<KeepTogether>" + TrueFalse(drv["CtrTogether"].ToString()) + "</KeepTogether>" + sfix);
						if (drv["RptCtrTypeCd"].ToString() == "L")	// List:
						{
							if (drv["CtrGrouping"].ToString() != null && drv["CtrGrouping"].ToString() != string.Empty) { sb.Append(pfix2 + drv["CtrGrouping"].ToString() + sfix); }
						}
					}
					if (drv["HasChildCtr"].ToString() == "Y")
					{
						MakeReportCtr(GenPrefix, ref sb, dbAppDatabase, drv["RptCtrId"].ToString(), null, null, pfix2, CSrc);
						MakeReportCha(GenPrefix, ref sb, drv["RptCtrId"].ToString(), pfix2, CSrc);
						MakeReportTbl(GenPrefix, ref sb, dbAppDatabase, drv["RptCtrId"].ToString(), pfix2, CSrc);
					}
					sb.Append(pfix1 + "</" + drv["RptCtrTypeName"].ToString() + ">" + Environment.NewLine);
				}
				sb.Append(sIndent + "</ReportItems>" + Environment.NewLine);
				return true;
			}
			else { return false; }
		}

		// Chart Data Region:
		private void MakeReportCha(string GenPrefix, ref StringBuilder sb, string RptCtrId, string sIndent, CurrSrc CSrc)
		{
			DataView dvCha = null;
			using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
			{
				dvCha = new DataView(dac.GetReportCha(GenPrefix, RptCtrId, CSrc));
			}
			if (dvCha.Count == 1)
			{
				foreach (DataRowView drv in dvCha)
				{
					if (drv["RptChaTypeCd"].ToString() == "AH") { sb.Append(sIndent + "<Type>Area</Type><Subtype>PercentStacked</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "AP") { sb.Append(sIndent + "<Type>Area</Type><Subtype>Plain</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "AS") { sb.Append(sIndent + "<Type>Area</Type><Subtype>Stacked</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "BH") { sb.Append(sIndent + "<Type>Bar</Type><Subtype>PercentStacked</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "BP") { sb.Append(sIndent + "<Type>Bar</Type><Subtype>Plain</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "BS") { sb.Append(sIndent + "<Type>Bar</Type><Subtype>Stacked</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "CH") { sb.Append(sIndent + "<Type>Column</Type><Subtype>PercentStacked</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "CP") { sb.Append(sIndent + "<Type>Column</Type><Subtype>Plain</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "CS") { sb.Append(sIndent + "<Type>Column</Type><Subtype>Stacked</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "DE") { sb.Append(sIndent + "<Type>Doughnut</Type><Subtype>Exploded</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "DP") { sb.Append(sIndent + "<Type>Doughnut</Type><Subtype>Plain</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "LP") { sb.Append(sIndent + "<Type>Line</Type><Subtype>Plain</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "LS") { sb.Append(sIndent + "<Type>Line</Type><Subtype>Smooth</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "PE") { sb.Append(sIndent + "<Type>Pie</Type><Subtype>Exploded</Subtype>" + Environment.NewLine); }
					else if (drv["RptChaTypeCd"].ToString() == "PP") { sb.Append(sIndent + "<Type>Pie</Type><Subtype>Plain</Subtype>" + Environment.NewLine); }
					sb.Append(sIndent + "<ThreeDProperties><Enabled>");
					if (drv["ThreeD"].ToString() == "Y") { sb.Append("true"); } else { sb.Append("false"); }
					sb.Append("</Enabled><Rotation>30</Rotation><Inclination>30</Inclination><Shading>Simple</Shading><WallThickness>50</WallThickness></ThreeDProperties>" + Environment.NewLine);
					sb.Append(sIndent + "<CategoryAxis><Axis>" + Environment.NewLine);
					sb.Append(sIndent + "	<Title><Caption>" + drv["CategoryTtl"].ToString() + "</Caption></Title>" + Environment.NewLine);
					sb.Append(sIndent + "	<MajorGridLines><Style><BorderStyle><Default>Solid</Default></BorderStyle></Style></MajorGridLines>" + Environment.NewLine);
					sb.Append(sIndent + "	<MinorGridLines><Style><BorderStyle><Default>Solid</Default></BorderStyle></Style></MinorGridLines>" + Environment.NewLine);
					sb.Append(sIndent + "	<MajorTickMarks>Outside</MajorTickMarks><Visible>true</Visible>" + Environment.NewLine);
					sb.Append(sIndent + "</Axis></CategoryAxis>" + Environment.NewLine);
					sb.Append(sIndent + "<ValueAxis><Axis>" + Environment.NewLine);
					sb.Append(sIndent + "	<Title><Caption>" + drv["CtrValue"].ToString() + "</Caption></Title>" + Environment.NewLine);
					sb.Append(sIndent + "	<MajorGridLines><ShowGridLines>true</ShowGridLines><Style><BorderStyle><Default>Solid</Default></BorderStyle></Style></MajorGridLines>" + Environment.NewLine);
					sb.Append(sIndent + "	<MinorGridLines><Style><BorderStyle><Default>Solid</Default></BorderStyle></Style></MinorGridLines>" + Environment.NewLine);
					sb.Append(sIndent + "	<Margin>true</Margin><Scalar>true</Scalar>" + Environment.NewLine);
					sb.Append(sIndent + "	<MajorTickMarks>Outside</MajorTickMarks><Visible>true</Visible>" + Environment.NewLine);
					sb.Append(sIndent + "</Axis></ValueAxis>" + Environment.NewLine);
					sb.Append(sIndent + "<CategoryGroupings><CategoryGrouping><DynamicCategories>" + drv["CategoryGrp"].ToString() + "<Label>" + drv["CategoryLbl"].ToString() + "</Label></DynamicCategories></CategoryGrouping></CategoryGroupings>" + Environment.NewLine);
					sb.Append(sIndent + "<ChartData><ChartSeries><DataPoints><DataPoint><DataValues><DataValue><Value>" + drv["ChartData"].ToString() + "</Value></DataValue></DataValues><Marker><Size>6pt</Size></Marker></DataPoint></DataPoints></ChartSeries></ChartData>" + Environment.NewLine);
					if (drv["SeriesGrp"].ToString() != null && drv["SeriesGrp"].ToString() != string.Empty)
					{
						sb.Append(sIndent + "<SeriesGroupings><SeriesGrouping><DynamicSeries>" + drv["SeriesGrp"].ToString() + "<Label>" + drv["SeriesLbl"].ToString() + "</Label></DynamicSeries></SeriesGrouping></SeriesGroupings>" + Environment.NewLine);
					}
				}
			}
		}

		// Table Data Region:
		private void MakeReportTbl(string GenPrefix, ref StringBuilder sb, string dbAppDatabase, string RptCtrId, string sIndent, CurrSrc CSrc)
		{
			string PrevTypeCd = string.Empty;
			string PrevRowNum = string.Empty;
			DataView dvTbl = null;
			DataView dvTbg = null;
			DataView dvCtr = null;
			using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
			{
				dvTbl = new DataView(dac.GetReportTbl(GenPrefix, RptCtrId, string.Empty, CSrc));
			}
			if (dvTbl.Count > 0)
			{
				foreach (DataRowView drv in dvTbl)
				{
					if (PrevTypeCd != drv["RptTblTypeCd"].ToString())
					{
						if (PrevTypeCd != string.Empty)
						{
							if (PrevRowNum != string.Empty) { sb.Append(sIndent + "	</TableCells></TableRow>" + Environment.NewLine); }
							if ("H,D,F".IndexOf(PrevTypeCd) >= 0) { sb.Append(sIndent + "	</TableRows>" + Environment.NewLine); }
							if (PrevTypeCd == "C") { sb.Append(sIndent + "</TableColumns>"); }
							else if (PrevTypeCd == "H") { sb.Append(sIndent + "</Header>"); }
							else if (PrevTypeCd == "D") { sb.Append(sIndent + "</Details>"); }
							else if (PrevTypeCd == "F") { sb.Append(sIndent + "</Footer>"); }
							else { sb.Append(sIndent + "</TableGroups>"); }
							sb.Append(Environment.NewLine);
						}
						if (drv["RptTblTypeCd"].ToString() == "C") { sb.Append(sIndent + "<TableColumns>"); }
						else if (drv["RptTblTypeCd"].ToString() == "H") { sb.Append(sIndent + "<Header>"); }
						else if (drv["RptTblTypeCd"].ToString() == "D") { sb.Append(sIndent + "<Details>"); }
						else if (drv["RptTblTypeCd"].ToString() == "F") { sb.Append(sIndent + "<Footer>"); }
						else { sb.Append(sIndent + "<TableGroups>"); }
						if (drv["RptTblTypeCd"].ToString() == "H" || drv["RptTblTypeCd"].ToString() == "F")
						{
							sb.Append("<RepeatOnNewPage>" + TrueFalse(drv["TblRepeatNew"].ToString()) + "</RepeatOnNewPage>");
						}
						else if (drv["RptTblTypeCd"].ToString() == "D")
						{
							if (drv["TblVisibility"].ToString() != null && drv["TblVisibility"].ToString() != string.Empty) { sb.Append(drv["TblVisibility"].ToString()); }
						}
						if ("H,D,F".IndexOf(drv["RptTblTypeCd"].ToString()) >= 0) { sb.Append("<TableRows>"); }
						sb.Append(Environment.NewLine);
						PrevTypeCd = drv["RptTblTypeCd"].ToString(); PrevRowNum = string.Empty;
					}
					if (drv["RptTblTypeCd"].ToString() == "C")
					{
						sb.Append(sIndent + "	<TableColumn><Width>" + drv["ColWidth"].ToString() + "</Width>");
						if (drv["TblVisibility"].ToString() != null && drv["TblVisibility"].ToString() != string.Empty) { sb.Append(drv["TblVisibility"].ToString()); }
						sb.Append("</TableColumn>" + Environment.NewLine);
					}
					else if (drv["RptTblTypeCd"].ToString() == "G")
					{
						sb.Append(sIndent + "	<TableGroup>");
						if (drv["TblGrouping"].ToString() != null && drv["TblGrouping"].ToString() != string.Empty)  { sb.Append(drv["TblGrouping"].ToString()); }
						if (drv["TblVisibility"].ToString() != null && drv["TblVisibility"].ToString() != string.Empty) { sb.Append(drv["TblVisibility"].ToString()); }
						sb.Append(Environment.NewLine);
						using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
						{
							dvTbg = new DataView(dac.GetReportTbl(GenPrefix, RptCtrId, drv["RptTblId"].ToString(), CSrc));
						}
						if (dvTbg.Count > 0)
						{
							string PrevCd = string.Empty;
							string PrevNo = string.Empty;
							foreach (DataRowView drg in dvTbg)
							{
								if (PrevCd != drg["RptTblTypeCd"].ToString())
								{
									if (PrevCd != string.Empty)
									{
										if (PrevNo != string.Empty) { sb.Append(sIndent + "		</TableCells></TableRow>" + Environment.NewLine); }
										sb.Append(sIndent + "		</TableRows>" + Environment.NewLine);
										if (PrevCd == "H") { sb.Append(sIndent + "	</Header>" + Environment.NewLine); } else { sb.Append(sIndent + "	</Footer>" + Environment.NewLine); };
									}
									if (drg["RptTblTypeCd"].ToString() == "H") { sb.Append(sIndent + "	<Header>"); } else { sb.Append(sIndent + "	<Footer>"); }
									sb.Append("<RepeatOnNewPage>" + TrueFalse(drg["TblRepeatNew"].ToString()) + "</RepeatOnNewPage><TableRows>" + Environment.NewLine);
									PrevCd = drg["RptTblTypeCd"].ToString(); PrevNo = string.Empty;
								}
								if (drg["RowNum"].ToString() != null && drg["RowNum"].ToString() != string.Empty && PrevNo != drg["RowNum"].ToString())
								{
									if (PrevNo != string.Empty)
									{
										sb.Append(sIndent + "		</TableCells></TableRow>" + Environment.NewLine);
									}
									using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
									{
                                        // Test to see if the first table cell is being used.
										dvCtr = new DataView(dac.GetReportCtr(GenPrefix, null, null, drg["RptCelId"].ToString(), CSrc));
									}
									if (dvCtr.Count <= 0)
									{
										sb.Append(sIndent + "		<TableRow><Height>0.0in</Height>");
									}
									else
									{
										sb.Append(sIndent + "		<TableRow><Height>" + drg["RowHeight"].ToString() + "</Height>");
									}
									if (drg["RowVisibility"].ToString() != null && drg["RowVisibility"].ToString() != string.Empty) { sb.Append(drg["RowVisibility"].ToString()); }
									sb.Append("<TableCells>" + Environment.NewLine);
									PrevNo = drg["RowNum"].ToString();
								}
								if (drg["RptCelId"].ToString() != null && drg["RptCelId"].ToString() != string.Empty)
								{
									sb.Append(sIndent + "			<TableCell>");
									if (drg["CelColSpan"].ToString() != null && drg["CelColSpan"].ToString() != string.Empty) { sb.Append("<ColSpan>" + drg["CelColSpan"].ToString() + "</ColSpan>"); }
									sb.Append(Environment.NewLine);
									if (!MakeReportCtr(GenPrefix, ref sb, dbAppDatabase, null, null, drg["RptCelId"].ToString(), sIndent + "			", CSrc))
									{
										sb.Append(sIndent + "			<ReportItems><Textbox Name=\"X" + drg["RptCelId"].ToString() + "\"><Value>=\"\"</Value></Textbox></ReportItems>" + Environment.NewLine);
									}
									sb.Append(sIndent + "			</TableCell>" + Environment.NewLine);
								}
							}
							// Take care of the very last:
							if (PrevCd != string.Empty)
							{
								if (PrevNo != string.Empty) { sb.Append(sIndent + "		</TableCells></TableRow>" + Environment.NewLine); }
								sb.Append(sIndent + "		</TableRows>" + Environment.NewLine);
								if (PrevCd == "H") { sb.Append(sIndent + "	</Header>" + Environment.NewLine); } else { sb.Append(sIndent + "	</Footer>" + Environment.NewLine); };
							}
						}
						sb.Append(sIndent + "	</TableGroup>" + Environment.NewLine);
					}
					if (drv["RowNum"].ToString() != null && drv["RowNum"].ToString() != string.Empty && PrevRowNum != drv["RowNum"].ToString())
					{
						if (PrevRowNum != string.Empty)
						{
							sb.Append(sIndent + "	</TableCells></TableRow>" + Environment.NewLine);
						}
						sb.Append(sIndent + "	<TableRow><Height>" + drv["RowHeight"].ToString() + "</Height>");
						if (drv["RowVisibility"].ToString() != null && drv["RowVisibility"].ToString() != string.Empty) { sb.Append(drv["RowVisibility"].ToString()); }
						sb.Append("<TableCells>" + Environment.NewLine);
						PrevRowNum = drv["RowNum"].ToString();
					}
					if (drv["RptCelId"].ToString() != null && drv["RptCelId"].ToString() != string.Empty)
					{
						sb.Append(sIndent + "		<TableCell>");
						if (drv["CelColSpan"].ToString() != null && drv["CelColSpan"].ToString() != string.Empty) { sb.Append("<ColSpan>" + drv["CelColSpan"].ToString() + "</ColSpan>"); }
						sb.Append(Environment.NewLine);
						MakeReportCtr(GenPrefix, ref sb, dbAppDatabase, null, null, drv["RptCelId"].ToString(), sIndent + "		", CSrc);
						sb.Append(sIndent + "		</TableCell>" + Environment.NewLine); 
					}
				}
				// Take care of the very last:
				if (PrevTypeCd != string.Empty)
				{
					if (PrevRowNum != string.Empty) { sb.Append(sIndent + "	</TableCells></TableRow>" + Environment.NewLine); }
					if ("H,D,F".IndexOf(PrevTypeCd) >= 0) { sb.Append(sIndent + "	</TableRows>" + Environment.NewLine); }
					if (PrevTypeCd == "C") { sb.Append(sIndent + "</TableColumns>"); }
					else if (PrevTypeCd == "H") { sb.Append(sIndent + "</Header>"); }
					else if (PrevTypeCd == "D") { sb.Append(sIndent + "</Details>"); }
					else if (PrevTypeCd == "F") { sb.Append(sIndent + "</Footer>"); }
					else { sb.Append(sIndent + "</TableGroups>"); }
					sb.Append(Environment.NewLine);
				}
			}
		}

        //private StringBuilder MakeAsmx(DataRow dw, Int32 reportId, DataView dvCri, CurrPrj CPrj, CurrSrc CSrc)
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
        //    foreach (DataRowView drv in dvCri)
        //    {
        //        if ("ComboBox,DropDownList,ListBox,RadioButtonList".IndexOf(drv["DisplayName"].ToString()) >= 0)
        //        {
        //            sb.Append(Environment.NewLine);
        //            sb.Append("	[WebMethod]" + Environment.NewLine);
        //            sb.Append("	public string GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "(Int32 reportId, string ui, string uc" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //            sb.Append("	{" + Environment.NewLine);
        //            sb.Append("		return XmlUtils.DataTableToXml((new " + dw["ProgramName"].ToString() + "System()).GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "(reportId,ui.XmlToObject<UsrImpr>(),uc.XmlToObject<UsrCurr>()" + Robot.GetCnParm("N", "N") + "));" + Environment.NewLine);
        //            sb.Append("	}" + Environment.NewLine);
        //        }
        //    }
        //    sb.Append(Environment.NewLine);
        //    sb.Append("	[WebMethod]" + Environment.NewLine);
        //    sb.Append("	public string Get" + dw["ProgramName"].ToString() + "(Int32 reportId, string ui, string uc, string ds" + Robot.GetCnDclr("N", "N") + ", bool bUpd, bool bXls, bool bVal)" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    sb.Append("		return XmlUtils.DataTableToXml((new " + dw["ProgramName"].ToString() + "System()).Get" + dw["ProgramName"].ToString() + "(reportId,ui.XmlToObject<UsrImpr>(),uc.XmlToObject<UsrCurr>(),ds.XmlToDataSet<Ds" + dw["ProgramName"].ToString() + "In>()" + Robot.GetCnParm("N", "N") + ",bUpd,bXls,bVal));" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("	[WebMethod]" + Environment.NewLine);
        //    sb.Append("	public bool Upd" + dw["ProgramName"].ToString() + "(Int32 reportId, Int32 usrId, string ds" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    sb.Append("		return (new " + dw["ProgramName"].ToString() + "System()).Upd" + dw["ProgramName"].ToString() + "(reportId, usrId, ds.XmlToDataSet<Ds" + dw["ProgramName"].ToString() + "In>()" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append("}" + Environment.NewLine);
        //    return sb;
        //}

        private StringBuilder MakeAspx(DataRow dw, Int32 reportId, string reportTitle, CurrPrj CPrj, string clientFrwork)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<%@ Page language=\"c#\" MasterPageFile=\"Default.master\" EnableEventValidation=\"false\"");
			sb.Append(" Inherits=\"" + CPrj.EntityCode + ".Web." + dw["ProgramName"].ToString() + "\" CodeFile=\"" + dw["ProgramName"].ToString() + ".aspx.cs\" Title=\"" + reportTitle + "\" %>" + Environment.NewLine);
			sb.Append("<%@ Register TagPrefix=\"Module\" TagName=\"" + dw["ProgramName"].ToString() + "\" Src=\"reports/" + dw["ProgramName"].ToString() + "Module.ascx\" %>" + Environment.NewLine);
			sb.Append("<asp:Content ContentPlaceHolderID=\"MHR\" Runat=\"Server\"><Module:" + dw["ProgramName"].ToString() + " id=\"M" + reportId.ToString() + "\" runat=\"server\" /></asp:Content>" + Environment.NewLine);
			return sb;
		}

		private StringBuilder MakeAspxCs(DataRow dw, string reportTitle, CurrPrj CPrj, string clientFrwork)
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

		private StringBuilder MakeAscx(DataRow dw, string reportTitle, DataView dvCri, DataView dvObj, DataView dvGrp, CurrPrj CPrj, string clientFrwork)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<%@ Control Language=\"c#\" Inherits=\"" + CPrj.EntityCode + ".Web." + dw["ProgramName"].ToString() + "Module\" CodeFile=\"" + dw["ProgramName"].ToString() + "Module.ascx.cs\" CodeFileBaseClass=\"RO.Web.ModuleBase\" %>" + Environment.NewLine);
            sb.Append("<%@ Register TagPrefix=\"cr\" Namespace=\"CrystalDecisions.Web\" Assembly=\"CrystalDecisions.Web\" %>" + Environment.NewLine);
            sb.Append("<%@ Register TagPrefix=\"ajwc\" Assembly=\"AjaxControlToolkit\" Namespace=\"AjaxControlToolkit\" %>" + Environment.NewLine);
            sb.Append("<%@ Register TagPrefix=\"rcasp\" NameSpace=\"RoboCoder.WebControls\" Assembly=\"WebControls, Culture=neutral\" %>" + Environment.NewLine);
            sb.Append("<%@ Register TagPrefix=\"Module\" TagName=\"Help\" Src=\"../modules/HelpModule.ascx\" %>" + Environment.NewLine);
			sb.Append("<script type=\"text/javascript\" lang=\"javascript\">" + Environment.NewLine);
            sb.Append("	Sys.Application.add_load(function () { ApplyJQueryWidget('', ''); });" + Environment.NewLine);
            sb.Append("	Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)" + Environment.NewLine);
			sb.Append("	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)" + Environment.NewLine);
            sb.Append("	function beginRequestHandler() {try {$('input[Behaviour=\\'Date\\']').datepicker('destroy');  myFocus = document.forms[0].__EVENTTARGET.value; } catch (e) { };   ShowProgress(); document.body.style.cursor = 'wait'; }" + Environment.NewLine);
            sb.Append("	function endRequestHandler() { HideProgress(); document.body.style.cursor = 'auto'; try {var f = $('#' + myFocus.replace(/\\$/g,'_')); if (f.attr('Behaviour') == 'Date') nextOnTabIndex(f).focus(); } catch (e) { }; }" + Environment.NewLine);
            sb.Append("	function IniVars(BannerWidth) {BannerWidth = getWidth(document.getElementById('BannerTable'));}" + Environment.NewLine);
			sb.Append("	Event.observe(window,'load',function() {IniVars(document.getElementById('<%=cViewerWidth.ClientID%>'));});" + Environment.NewLine);
			sb.Append("	Event.observe(window,'resize',function() {IniVars(document.getElementById('<%=cViewerWidth.ClientID%>'));});" + Environment.NewLine);
			sb.Append("</script>" + Environment.NewLine);
			sb.Append("<div id=\"AjaxSpinner\" class=\"AjaxSpinner\" style=\"display:none;\">" + Environment.NewLine);
			sb.Append("	<div style=\"padding:10px;\">" + Environment.NewLine);
			sb.Append("		<img alt=\"\" src=\"images/indicator.gif\" />&nbsp;<asp:Label ID=\"AjaxSpinnerLabel\" Text=\"This may take a moment...\" runat=\"server\" />" + Environment.NewLine);
			sb.Append("	</div>" + Environment.NewLine);
			sb.Append("</div>" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "G")
			{
				sb.Append("<asp:UpdatePanel UpdateMode=\"Conditional\" runat=\"server\"><ContentTemplate>" + Environment.NewLine);
			}
			else if (dw["ReportTypeCd"].ToString() == "X")
			{
				sb.Append("<asp:UpdatePanel UpdateMode=\"Conditional\" runat=\"server\"><Triggers><asp:PostBackTrigger ControlID=\"cExpXmlButton\" /><asp:PostBackTrigger ControlID=\"cExpTxtButton\" /></Triggers><ContentTemplate>" + Environment.NewLine);
			}
			else
			{
                sb.Append("<asp:UpdatePanel UpdateMode=\"Conditional\" runat=\"server\"><Triggers><asp:PostBackTrigger ControlID=\"cExpPdfButton\" /><asp:PostBackTrigger ControlID=\"cExpDocButton\" /><asp:PostBackTrigger ControlID=\"cExpXlsButton\" /><asp:PostBackTrigger ControlID=\"cExpTxtButton\" /><asp:PostBackTrigger ControlID=\"cViewButton\" /><asp:PostBackTrigger ControlID=\"cShowCriButton\" /></Triggers><ContentTemplate>" + Environment.NewLine);
			}
            sb.Append("<div class=\"r-table BannerGrp\">" + Environment.NewLine);
            sb.Append("<div class=\"r-tr\">" + Environment.NewLine);
            sb.Append("    <div class=\"r-td rc-1-4\">" + Environment.NewLine);
            sb.Append("        <div class=\"BannerNam\"><asp:label id=\"cTitleLabel\" CssClass=\"screen-title\" runat=\"server\" /><input type=\"image\" name=\"cDefaultFocus\" id=\"cClientFocusButton\" src=\"images/Help_x.jpg\" onclick=\"return false;\" style=\"visibility:hidden;\" /></div>" + Environment.NewLine);
            sb.Append("    </div>" + Environment.NewLine);
            sb.Append("    <div class=\"r-td rc-5-12\">" + Environment.NewLine);
            sb.Append("        <div class=\"BannerBtn\">" + Environment.NewLine);
            sb.Append("        <asp:Panel id=\"cButPanel\" runat=\"server\">" + Environment.NewLine);
            sb.Append("        <div class=\"BtnTbl\">" + Environment.NewLine);
            if (dw["ReportTypeCd"].ToString() == "G")
            {
                sb.Append("            <div><asp:Button id=\"cSearchButton\" onclick=\"cSearchButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
            }
            else if (dw["ReportTypeCd"].ToString() == "X")
            {
                sb.Append("            <div><asp:Button id=\"cExpXmlButton\" onclick=\"cExpXmlButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
                sb.Append("            <div><asp:Button id=\"cExpTxtButton\" onclick=\"cExpTxtButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
            }
            else
            {
                sb.Append("            <div><asp:Button id=\"cViewButton\" onclick=\"cViewButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
                sb.Append("            <div><asp:Button id=\"cExpPdfButton\" onclick=\"cExpPdfButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
                sb.Append("            <div><asp:Button id=\"cExpDocButton\" onclick=\"cExpDocButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
                sb.Append("            <div><asp:Button id=\"cExpXlsButton\" onclick=\"cExpXlsButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
                sb.Append("            <div><asp:Button id=\"cExpTxtButton\" onclick=\"cExpTxtButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
                sb.Append("            <div><asp:Button id=\"cPrintButton\" onclick=\"cPrintButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
                sb.Append("            <div style=\"margin-top:4px;\"><asp:DropDownList id=\"cPrinter\" CssClass=\"inp-ddl\" runat=\"server\" DataTextField=\"PrinterName\" DataValueField=\"PrinterPath\" AutoPostBack=\"false\" /></div>" + Environment.NewLine);
            }
            sb.Append("            <div><asp:Button id=\"cShowCriButton\" onclick=\"cShowCriButton_Click\" runat=\"server\" /></div>" + Environment.NewLine);
            sb.Append("            <div><Module:Help id=\"cHelpMsg\" runat=\"server\" /></div>" + Environment.NewLine);
            sb.Append("            <div style=\"clear:both;\"></div>" + Environment.NewLine);
            sb.Append("        </div>" + Environment.NewLine);
            sb.Append("        </asp:Panel>" + Environment.NewLine);
            sb.Append("        </div>" + Environment.NewLine);
            sb.Append("    </div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("</div>" + Environment.NewLine);
            sb.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"margin:15px 10px 10px 10px;\">" + Environment.NewLine);
			sb.Append("	<tr>" + Environment.NewLine);
			sb.Append("		<td>" + Environment.NewLine);
			int ii = 0;
			MakeReportGrp(ref sb, ii, dvCri, dvGrp, "			", clientFrwork);
			sb.Append("		</td>" + Environment.NewLine);
			sb.Append("	</tr>" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "G" && dvObj.Count > 0)
			{
				sb.Append("	<tr>" + Environment.NewLine);
				sb.Append("		<td>" + Environment.NewLine);
				sb.Append("			<asp:Panel id=\"cGlobal\" runat=\"server\" width=\"10%\">" + Environment.NewLine);
				sb.Append("			<br />" + Environment.NewLine);
				sb.Append("			<table cellspacing=\"2\">" + Environment.NewLine);
				foreach (DataRowView drv in dvObj)
				{
					sb.Append("			<tr valign=\"top\">" + Environment.NewLine);
					sb.Append("			<td width=\"5px\"></td>" + Environment.NewLine);
					sb.Append("			<td class=\"inp-lbl\" nowrap>" + drv["ColumnHeader"].ToString() + ":</td>" + Environment.NewLine);
                    sb.Append("			<td><div><asp:Label id=\"c" + drv["ColumnName"].ToString() + "\" CssClass=\"inp-lbl\" runat=\"server\" AutoPostBack=\"false\" /></div></td>" + Environment.NewLine);
					sb.Append("			</tr>" + Environment.NewLine);
				}
				sb.Append("			</table>" + Environment.NewLine);
				sb.Append("			</asp:Panel>" + Environment.NewLine);
				sb.Append("		</td>" + Environment.NewLine);
				sb.Append("	</tr>" + Environment.NewLine);
			}
			else if ("G,C".IndexOf(dw["ReportTypeCd"].ToString()) >= 0)
			{
				sb.Append("	<tr>" + Environment.NewLine);
				sb.Append("		<td>" + Environment.NewLine);
                sb.Append("			<cr:crystalreportviewer id=\"cViewer\" runat=\"server\" Visible=\"false\" BestFitPage=\"true\" HasPrintButton=\"false\" HasToggleGroupTreeButton=\"false\" HasExportButton=\"false\" />" + Environment.NewLine);
				sb.Append("		</td>" + Environment.NewLine);
				sb.Append("	</tr>" + Environment.NewLine);
			}
			sb.Append("</table>" + Environment.NewLine);
            sb.Append("<asp:Label ID=\"cMsgContent\" runat=\"server\" style=\"display:none;\" EnableViewState=\"false\"/>" + Environment.NewLine);
//            sb.Append("<asp:Image ID=\"cMsgImage\" ImageUrl=\"~/images/info.gif\" runat=\"server\" style=\"display:none;\" EnableViewState=\"false\" />" + Environment.NewLine);
			if (clientFrwork != "1") { sb.Append("</ContentTemplate></asp:UpdatePanel>" + Environment.NewLine); }
			sb.Append("<input id=\"cViewerWidth\" type=\"hidden\" runat=\"server\" />" + Environment.NewLine);
//			sb.Append("<asp:Button ID=\"cMsgPopupButton\" runat=\"server\" style=\"display:none;\" OnClientClick=\"return false;\"/>" + Environment.NewLine);
//            sb.Append("<asp:Panel ID=\"cMsgPanel\" runat=\"server\" style=\"display:none;\" CssClass=\"MsgPopup\">" + Environment.NewLine);
//            sb.Append(" <asp:ImageButton ID=\"cMsgCloseButton\" CssClass=\"MsgCloseButton\" ImageUrl=\"~/images/Help_x.jpg\" runat=\"server\" OnClientClick=\"$find('cMsgPopup').hide(); return false;\"  CausesValidation=\"false\" style=\"cursor:pointer;\"/>" + Environment.NewLine);
//            sb.Append(" <asp:Image id=\"cMsgClsImage\" CssClass=\"MsgClsImage\" ImageUrl=\"~/images/Help_x.jpg\" runat=\"server\" />" + Environment.NewLine);
//            sb.Append(" <p class=\"MsgPopupContent\"><asp:Label ID=\"cMsg\" runat=\"server\" /></p>" + Environment.NewLine);
//            sb.Append("</asp:Panel>" + Environment.NewLine);
//            sb.Append("<ajwc:ModalPopupExtender BehaviorID=\"cMsgPopup\" ID=\"cMsgPopup\" runat=\"server\" TargetControlID=\"cMsgPopupButton\" PopupControlID=\"cMsgPanel\" CancelControlID=\"cMsgCloseButton\"  BackgroundCssClass=\"modalBackground\" Drag=\"true\" PopupDragHandleControlID=\"cMsgPanel\" />" + Environment.NewLine);
            return sb;
		}

        private StringBuilder MakeAscxCs(DataRow dw, Int32 reportId, string reportTitle, DataView dvCri, DataView dvObj, DataView dvGrp, CurrPrj CPrj, CurrSrc CSrc, string clientFrwork)
		{
			StringBuilder sb = new StringBuilder();
			int ii;
			bool bListBox;
			sb.Append("using System;" + Environment.NewLine);
			sb.Append("using System.IO;" + Environment.NewLine);
			sb.Append("using System.Text;" + Environment.NewLine);
			sb.Append("using System.Data;" + Environment.NewLine);
			//sb.Append("using System.Runtime.Serialization;" + Environment.NewLine);
			sb.Append("using System.Web;" + Environment.NewLine);
			sb.Append("using System.Web.UI;" + Environment.NewLine);
			sb.Append("using System.Web.UI.WebControls;" + Environment.NewLine);
			sb.Append("using System.Web.UI.HtmlControls;" + Environment.NewLine);
			sb.Append("using System.Globalization;" + Environment.NewLine);
			sb.Append("using System.Threading;" + Environment.NewLine);
            sb.Append("using System.Linq;" + Environment.NewLine);
            sb.Append("using CrystalDecisions.Web;" + Environment.NewLine);
            sb.Append("using CrystalDecisions.Shared;" + Environment.NewLine);
			sb.Append("using CrystalDecisions.CrystalReports.Engine;" + Environment.NewLine);
            sb.Append("using RO.Facade3;" + Environment.NewLine);
            sb.Append("using RO.Common3;" + Environment.NewLine);
            sb.Append("using RO.Common3.Data;" + Environment.NewLine);
			if (CSrc.SrcSystemId != 3)	// Admin.
			{
                sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ".Data;" + Environment.NewLine);
            }
            //sb.Append("using " + CPrj.EntityCode + ".Service" + CSrc.SrcSystemId.ToString() + ";" + Environment.NewLine);
            //if (CSrc.SrcSystemId != 3)	// Admin.
            //{
            //    sb.Append("using RO.Service3;" + Environment.NewLine);
            //}
            MakeDataInCs(sb, dw, dvCri, CPrj, CSrc);
            sb.Append(Environment.NewLine);
			sb.Append("namespace " + CPrj.EntityCode + ".Web" + Environment.NewLine);
			sb.Append("{" + Environment.NewLine);
			sb.Append("	public");
			if (clientFrwork == "1") {sb.Append(" abstract");} else {sb.Append(" partial");}
			sb.Append(" class " + dw["ProgramName"].ToString() + "Module : RO.Web.ModuleBase" + Environment.NewLine);
			sb.Append("	{" + Environment.NewLine);
			if (clientFrwork == "1")
			{
				sb.Append("		protected System.Web.UI.WebControls.Label cTitleLabel;" + Environment.NewLine);
				foreach (DataRowView drv in dvCri)
				{
					sb.Append("		protected System.Web.UI.WebControls.TableCell c" + drv["ColumnName"].ToString() + "P1;" + Environment.NewLine);
					sb.Append("		protected System.Web.UI.WebControls.Label c" + drv["ColumnName"].ToString() + "Label;" + Environment.NewLine);
					if (drv["DisplayName"].ToString() == "ComboBox")
					{
						sb.Append("		protected RoboCoder.WebControls.");
					}
					else
					{
						sb.Append("		protected System.Web.UI.WebControls.");
					}
					sb.Append(drv["DisplayName"].ToString() + " c" + drv["ColumnName"].ToString() + ";" + Environment.NewLine);
				}
				foreach (DataRowView drv in dvGrp)
				{
					if (drv["ParentGrpId"].ToString() == null || drv["ParentGrpId"].ToString() == "")
					{
						sb.Append("		protected System.Web.UI.WebControls.Panel cCriteria;" + Environment.NewLine);
					}
					else
					{
						sb.Append("		protected System.Web.UI.WebControls.Panel cGrp" + drv["reportGrpId"].ToString() + ";" + Environment.NewLine);
					}
				}
				sb.Append("		protected System.Web.UI.WebControls.Button cClearCriButton;" + Environment.NewLine);
				sb.Append("		protected System.Web.UI.WebControls.Button cShowCriButton;" + Environment.NewLine);
				if (dw["ReportTypeCd"].ToString() == "G")
				{
					if (dvObj.Count > 0)
					{
						sb.Append("		protected System.Web.UI.WebControls.Panel cGlobal;" + Environment.NewLine);
						foreach (DataRowView drv in dvObj)
						{
							sb.Append("		protected System.Web.UI.WebControls.Label c" + drv["ColumnName"].ToString() + ";" + Environment.NewLine);
						}
					}
					sb.Append("		protected System.Web.UI.WebControls.Button cSearchButton;" + Environment.NewLine);
				}
				else if (dw["TemplateName"].ToString() == "" || dw["TemplateName"].ToString() == null)
				{
					sb.Append("		protected System.Web.UI.WebControls.Button cExpTxtButton;" + Environment.NewLine);
					if (dw["ReportTypeCd"].ToString() == "X")
					{
						sb.Append("		protected System.Web.UI.WebControls.Button cExpXmlButton;" + Environment.NewLine);
					}
					else
					{
						sb.Append("		protected System.Web.UI.WebControls.Button cExpPdfButton;" + Environment.NewLine);
						sb.Append("		protected System.Web.UI.WebControls.Button cExpDocButton;" + Environment.NewLine);
                        sb.Append("		protected System.Web.UI.WebControls.Button cExpXlsButton;" + Environment.NewLine);
                        sb.Append("		protected System.Web.UI.WebControls.Button cViewButton;" + Environment.NewLine);
						sb.Append("		protected System.Web.UI.WebControls.Button cPrintButton;" + Environment.NewLine);
						sb.Append("		protected System.Web.UI.WebControls.DropDownList cPrinter;" + Environment.NewLine);
						sb.Append("		protected CrystalDecisions.Web.CrystalReportViewer cViewer;" + Environment.NewLine);
					}
				}
			}
			sb.Append(Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "C")
			{
                sb.Append("		public ReportDocument rp = new ReportDocument();" + Environment.NewLine);
				sb.Append("		private const string KEY_dtReportSct = \"Cache:dtReportSct" + reportId.ToString() + "\";" + Environment.NewLine);
				sb.Append("		private const string KEY_dtReportItem = \"Cache:dtReportItem" + reportId.ToString() + "\";" + Environment.NewLine);
			}
			sb.Append("		private const string KEY_dtReportHlp = \"Cache:dtReportHlp" + reportId.ToString() + "\";" + Environment.NewLine);
			sb.Append("		private const string KEY_dtCri = \"Cache:dtCri" + reportId.ToString() + "\";" + Environment.NewLine);
			sb.Append("		private const string KEY_dtClientRule = \"Cache:dtClientRule" + reportId.ToString() + "\";" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append("		private const string KEY_dtPrinters = \"Cache:dtPrinters" + reportId.ToString() + "\";" + Environment.NewLine);
			}
			sb.Append("		private const string KEY_bClCriVisible = \"Cache:bClCriVisible" + reportId.ToString() + "\";" + Environment.NewLine);
			sb.Append("		private const string KEY_bShCriVisible = \"Cache:bShCriVisible" + reportId.ToString() + "\";" + Environment.NewLine);
			sb.Append("		private enum exportTo {TXT, RTF, XML, XLS, PDF, DOC, VIEW};" + Environment.NewLine);
			sb.Append("		private string LcSysConnString;" + Environment.NewLine);
			sb.Append("		private string LcAppConnString;" + Environment.NewLine);
			sb.Append("		private string LcAppPw;" + Environment.NewLine);
			//sb.Append(Environment.NewLine);
            //sb.Append("		private AdminWs AdminFacade()" + Environment.NewLine);
            //sb.Append("		{" + Environment.NewLine);
            //sb.Append("		    AdminWs ws = new AdminWs();" + Environment.NewLine);
            //sb.Append("		    ws.Url = Config.WsBaseUrl + \"/AdminWs.asmx\";" + Environment.NewLine);
            //sb.Append("		    ws.Credentials = new System.Net.NetworkCredential(Config.WsUserName, Config.WsPassword, Config.WsDomain);" + Environment.NewLine);
            //sb.Append("		    return ws;" + Environment.NewLine);
            //sb.Append("		}" + Environment.NewLine);
            //sb.Append(Environment.NewLine);
            //sb.Append("		private " + dw["ProgramName"].ToString() + "Ws " + dw["ProgramName"].ToString() + "Facade()" + Environment.NewLine);
            //sb.Append("		{" + Environment.NewLine);
            //sb.Append("		    " + dw["ProgramName"].ToString() + "Ws ws = new " + dw["ProgramName"].ToString() + "Ws();" + Environment.NewLine);
            //sb.Append("		    ws.Url = Config.WsBaseUrl + \"/" + dw["ProgramName"].ToString() + "Ws.asmx\";" + Environment.NewLine);
            //sb.Append("		    ws.Credentials = new System.Net.NetworkCredential(Config.WsUserName, Config.WsPassword, Config.WsDomain);" + Environment.NewLine);
            //sb.Append("		    return ws;" + Environment.NewLine);
            //sb.Append("		}" + Environment.NewLine);
            sb.Append(Environment.NewLine);
			sb.Append("		public " + dw["ProgramName"].ToString() + "Module()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			this.Init += new System.EventHandler(Page_Init);" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append(Environment.NewLine);
				sb.Append("		protected void Page_Unload(object sender, System.EventArgs e)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			rp.Dispose();" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
			}
			sb.Append(Environment.NewLine);
			sb.Append("		protected void Page_Load(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			CheckAuthentication(true);" + Environment.NewLine);
			sb.Append("			Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LUser.Culture);" + Environment.NewLine);
            sb.Append("			string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;" + Environment.NewLine);
            sb.Append("			string lang = \"en,zh\".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;" + Environment.NewLine);
            sb.Append("			ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), \"datepicker_i18n\", \"scripts/i18n/jquery.ui.datepicker-\" + lang + \".js\");" + Environment.NewLine);
            if (dw["ReportTypeCd"].ToString() == "C")
            {
                sb.Append("			rp.Load(Server.MapPath(@\"reports/" + dw["ProgramName"].ToString() + "Report.rpt\"));" + Environment.NewLine);
            }
            sb.Append("			if (!IsPostBack)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
            sb.Append("				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId," + CSrc.SrcSystemId.ToString() + ", base.LImpr, LcSysConnString, LcAppPw, null, " + reportId.ToString() + ", null);" + Environment.NewLine);
            sb.Append("				if (dtMenuAccess.Rows.Count == 0)" + Environment.NewLine);
            sb.Append("				{" + Environment.NewLine);
            sb.Append("				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, \"cSystem\", \"AccessDeny\", null, null, null);" + Environment.NewLine);
            sb.Append("				    throw new Exception(message);" + Environment.NewLine);
            sb.Append("				}" + Environment.NewLine);
            sb.Append("				/* Stop IIS from Caching but allowing export to Excel to work */" + Environment.NewLine);
            sb.Append("				HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);" + Environment.NewLine);
            sb.Append("				HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);" + Environment.NewLine);
            sb.Append("				HttpContext.Current.Response.Cache.SetNoStore();" + Environment.NewLine);
            sb.Append("				Response.Cache.SetExpires(DateTime.Now.AddSeconds(-60));" + Environment.NewLine);
            sb.Append("				Response.Cache.SetValidUntilExpires(true);" + Environment.NewLine);
            if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append("				Session.Remove(KEY_dtReportSct);" + Environment.NewLine);
				sb.Append("				Session.Remove(KEY_dtReportItem);" + Environment.NewLine);
			}
			sb.Append("				Session.Remove(KEY_dtReportHlp);" + Environment.NewLine);
			sb.Append("				Session.Remove(KEY_dtCri);" + Environment.NewLine);
			sb.Append("				Session.Remove(KEY_dtClientRule);" + Environment.NewLine);
			sb.Append("				SetButtonHlp();" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append("				bool bBatchPrint = false;" + Environment.NewLine);
				sb.Append("				if (base.LUser.InternalUsr == \"N\")" + Environment.NewLine);
				sb.Append("				{" + Environment.NewLine);
				sb.Append("					cPrintButton.Visible = false; cPrinter.Visible = false;" + Environment.NewLine);
				sb.Append("				}" + Environment.NewLine);
				sb.Append("				else" + Environment.NewLine);
				sb.Append("				{" + Environment.NewLine);
				sb.Append("					Session.Remove(KEY_dtPrinters);" + Environment.NewLine);
				sb.Append("					DataTable dtPrinters;" + Environment.NewLine);
                //sb.Append("					if (Config.Architect == \"W\")" + Environment.NewLine);
                //sb.Append("					{" + Environment.NewLine);
                //sb.Append("						dtPrinters = XmlUtils.XmlToDataTable(AdminFacade().GetPrinterList(base.LImpr.UsrGroups, base.LImpr.Members));" + Environment.NewLine);
                //sb.Append("					}" + Environment.NewLine);
                //sb.Append("					else" + Environment.NewLine);
                //sb.Append("					{" + Environment.NewLine);
                sb.Append("					dtPrinters = (new AdminSystem()).GetPrinterList(base.LImpr.UsrGroups, base.LImpr.Members);" + Environment.NewLine);
                //sb.Append("					}" + Environment.NewLine);
				sb.Append("					if (dtPrinters != null && dtPrinters.Rows.Count > 0)" + Environment.NewLine);
				sb.Append("					{" + Environment.NewLine);
				sb.Append("						cPrinter.DataSource = dtPrinters;" + Environment.NewLine);
				sb.Append("						cPrinter.DataBind();" + Environment.NewLine);
				sb.Append("						cPrinter.SelectedIndex = 0;" + Environment.NewLine);
				sb.Append("						Session[KEY_dtPrinters] = dtPrinters;" + Environment.NewLine);
				sb.Append("					}" + Environment.NewLine);
				sb.Append("					int ii = 0;	//Update criteria for Batch reporting." + Environment.NewLine);
				sb.Append("					while (Request.QueryString[\"Cri\"+ii.ToString()] != null && Request.QueryString[\"Val\"+ii.ToString()] != null)" + Environment.NewLine);
				sb.Append("					{" + Environment.NewLine);
                //sb.Append("						if (Config.Architect == \"W\")" + Environment.NewLine);
                //sb.Append("						{" + Environment.NewLine);
                //sb.Append("							AdminFacade().UpdLastCriteria(0," + reportId.ToString() + ",base.LUser.UsrId,Int32.Parse(Request.QueryString[\"Cri\"+ii.ToString()]),Request.QueryString[\"Val\"+ii.ToString()],LcSysConnString,LcAppPw);" + Environment.NewLine);
                //sb.Append("						}" + Environment.NewLine);
                //sb.Append("						else" + Environment.NewLine);
                //sb.Append("						{" + Environment.NewLine);
                sb.Append("						(new AdminSystem()).UpdLastCriteria(0," + reportId.ToString() + ",base.LUser.UsrId,Int32.Parse(Request.QueryString[\"Cri\"+ii.ToString()]),Request.QueryString[\"Val\"+ii.ToString()],LcSysConnString,LcAppPw);" + Environment.NewLine);
                //sb.Append("						}" + Environment.NewLine);
				sb.Append("						bBatchPrint = true; ii = ii +1;" + Environment.NewLine);
				sb.Append("					}" + Environment.NewLine);
				sb.Append("				}" + Environment.NewLine);
			}
			sb.Append("				DataTable dt;" + Environment.NewLine);
            sb.Append("				DataView dv;" + Environment.NewLine);
            //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            //sb.Append("					dt = XmlUtils.XmlToDataTable(AdminFacade().GetLastCriteria(" + dvCri.Count.ToString() + ",0," + reportId.ToString() + ",base.LUser.UsrId,LcSysConnString,LcAppPw));" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
            //sb.Append("				else" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            sb.Append("				dt = (new AdminSystem()).GetLastCriteria(" + dvCri.Count.ToString() + ",0," + reportId.ToString() + ",base.LUser.UsrId,LcSysConnString,LcAppPw);" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
			sb.Append("				if ((bool)Session[KEY_bClCriVisible]) {cClearCriButton.Visible = cCriteria.Visible;} else {cClearCriButton.Visible = false;}" + Environment.NewLine);
			sb.Append("				if ((bool)Session[KEY_bShCriVisible]) {cShowCriButton.Visible = !cCriteria.Visible;} else {cShowCriButton.Visible = false;}" + Environment.NewLine);
			ii = 0;
			bListBox = false;
			sb.Append("				DataTable dtCri = GetReportCriHlp();" + Environment.NewLine);
            sb.Append("				string selectedVal = null;" + Environment.NewLine);
            foreach (DataRowView drv in dvCri)
            {
                DataView dvDependent = new DataView(dvCri.Table, "DdlFtrColumnId = " + drv["ReportCriId"].ToString() + " AND DisplayMode <> 'AutoComplete' AND DisplayName in ('ComboBox','DropDownList','ListBox','RadioButtonList')", "", DataViewRowState.CurrentRows);
                sb.Append("				base.SetCriBehavior(c" + drv["ColumnName"].ToString() + ", c" + drv["ColumnName"].ToString() + "P1, c" + drv["ColumnName"].ToString() + "Label, dtCri.Rows[" + ii.ToString() + "]);" + Environment.NewLine);
				//sb.Append("				c" + drv["ColumnName"].ToString() + "Label.Text = dtCri.Rows[" + ii.ToString() + "][\"ColumnHeader\"].ToString();");
				//sb.Append(" c" + drv["ColumnName"].ToString() + "P1.Width = new Unit(dtCri.Rows[" + ii.ToString() + "][\"HeaderWidth\"].ToString()+\"px\");" + Environment.NewLine);
                if ("TextBox,CheckBox,ComboBox,DropDownList,ListBox,RadioButtonList".IndexOf(drv["DisplayName"].ToString()) >= 0) { sb.Append("				c" + drv["ColumnName"].ToString() + ".AutoPostBack = " + (dvDependent.Count > 0 ? "true" : "false") + ";" + Environment.NewLine); }
                if ("CheckBox".IndexOf(drv["DisplayName"].ToString()) >= 0)
                {
					sb.Append("				if (dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString() != string.Empty)" + Environment.NewLine);
					sb.Append("				{" + Environment.NewLine);
					sb.Append("					c" + drv["ColumnName"].ToString() + ".Checked = base.GetBool(dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString());" + Environment.NewLine);
					sb.Append("				}" + Environment.NewLine);
				}
				else
				{
					if ("ComboBox,DropDownList,ListBox,RadioButtonList".IndexOf(drv["DisplayName"].ToString()) >= 0)
					{
                        string ddlFtrColumeName = drv["DdlFtrColumnName"].ToString();
                        using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
						{
							dac.MkReportGetIn(string.Empty, Int32.Parse(drv["ReportCriId"].ToString()), reportId.ToString() + drv["ColumnName"].ToString(), CSrc, dw["dbAppDatabase"].ToString(), dw["dbDesDatabase"].ToString());
						}
                        //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
                        //sb.Append("				{" + Environment.NewLine);
                        //sb.Append("					c" + drv["ColumnName"].ToString() + ".DataSource = new DataView(XmlUtils.XmlToDataTable(" + dw["ProgramName"].ToString() + "Facade().GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "(" + reportId.ToString() + ",XmlUtils.ObjectToXml(base.LImpr),XmlUtils.ObjectToXml(base.LCurr)" + Robot.GetCnCall("N", "N") + ")));" + Environment.NewLine);
                        //sb.Append("				}" + Environment.NewLine);
                        //sb.Append("				else" + Environment.NewLine);
                        //sb.Append("				{" + Environment.NewLine);
                        //sb.Append("				c" + drv["ColumnName"].ToString() + ".DataSource = new DataView((new AdminSystem()).GetIn(" + reportId.ToString() + ",\"GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "\"," + (drv["RequiredValid"].ToString() == "N" ? "true" : "false") + ",base.LImpr,base.LCurr" + Robot.GetCnStr("N", "N") + "));" + Environment.NewLine);
                        sb.Append("				try {selectedVal = dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString();} catch { selectedVal = null;};" + Environment.NewLine);
                        sb.Append("				dv = new DataView((new AdminSystem()).GetIn(" + reportId.ToString() + ",\"GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "\"," + (drv["DisplayName"].ToString() == "ListBox" ? "0" : "(new SqlReportSystem()).CountRptCri(\"" + drv["ReportCriId"].ToString() + "\",LcSysConnString,LcAppPw)") + ",\"" + drv["RequiredValid"].ToString() + "\"" + (drv["DisplayMode"].ToString() == "AutoListBox" ? ",false,selectedVal" : "") + ",base.LImpr,base.LCurr" + Robot.GetCnStr("N", "N") + "));" + Environment.NewLine);
                        if (!string.IsNullOrEmpty(ddlFtrColumeName)) sb.Append("				dv.RowFilter = GetCriteriaRowFilter(dv.Table," + "\"" + ddlFtrColumeName + "\"" + ",GetCriteriaColumnValue(cCriteria," + "\"" + "c" + ddlFtrColumeName + "\"" + ")" + ");" + Environment.NewLine);
                        sb.Append("				c" + drv["ColumnName"].ToString() + ".DataSource = dv;" + Environment.NewLine);
                        //sb.Append("				c" + drv["ColumnName"].ToString() + ".DataSource = new DataView((new " + dw["ProgramName"].ToString() + "System()).GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "(" + reportId.ToString() + ",base.LImpr,base.LCurr" + Robot.GetCnCall("N", "N") + "));" + Environment.NewLine);
                        //sb.Append("				}" + Environment.NewLine);
                        if (
                            (drv["DisplayName"].ToString() == "ComboBox" && drv["DisplayMode"].ToString() == "AutoComplete")
                            ||
                            (drv["DisplayName"].ToString() == "ListBox" && drv["DisplayMode"].ToString() == "AutoListBox")
                            )
                        {
                            sb.Append("				if (true)" + Environment.NewLine);
                            sb.Append("				{" + Environment.NewLine);
                            sb.Append("					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();" + Environment.NewLine);
                            sb.Append("					context[\"method\"] = \"" + reportId.ToString() + drv["ColumnName"].ToString() + "\";" + Environment.NewLine);
                            sb.Append("					context[\"addnew\"] = \"Y\";" + Environment.NewLine);
                            sb.Append("					context[\"sp\"] = \"" + reportId.ToString() + drv["ColumnName"].ToString() + "\";" + Environment.NewLine);
                            sb.Append("					context[\"requiredValid\"] = \"" + drv["RequiredValid"].ToString() + "\";" + Environment.NewLine);
                            sb.Append("					context[\"mKey\"] = c" + drv["ColumnName"].ToString() + ".DataValueField;" + Environment.NewLine);
                            sb.Append("					context[\"mVal\"] = c" + drv["ColumnName"].ToString() + ".DataTextField;" + Environment.NewLine);
                            sb.Append("					context[\"mTip\"] = c" + drv["ColumnName"].ToString() + ".DataTextField;" + Environment.NewLine);
                            sb.Append("					context[\"mImg\"] = c" + drv["ColumnName"].ToString() + ".DataTextField;" + Environment.NewLine);
                            sb.Append("					context[\"ssd\"] = Request.QueryString[\"ssd\"];" + Environment.NewLine);
                            sb.Append("					context[\"rpt\"] = \"" + reportId.ToString() + "\";" + Environment.NewLine);
                            sb.Append("					context[\"reportCriId\"] = \"" + drv["ReportCriId"].ToString() + "\";" + Environment.NewLine);
                            sb.Append("					context[\"csy\"] = \"" + CSrc.SrcSystemId.ToString() + "\";" + Environment.NewLine);
                            sb.Append("					context[\"genPrefix\"] = \"\";" + Environment.NewLine);
                            sb.Append("					context[\"filter\"] = \"0\";" + Environment.NewLine);
                            sb.Append("					context[\"isSys\"] = \"N\";" + Environment.NewLine);
                            sb.Append("					context[\"conn\"] = null;" + Environment.NewLine);
                            sb.Append("					context[\"refColCID\"] = " + (string.IsNullOrEmpty(@ddlFtrColumeName) ? "null" : "c" + @ddlFtrColumeName + ".ClientID") + ";" + Environment.NewLine);
                            sb.Append("					context[\"refCol\"] = " + (string.IsNullOrEmpty(@ddlFtrColumeName) ? "null" : "\"" + @ddlFtrColumeName + "\"") + ";" + Environment.NewLine);
                            if (drv["DisplayMode"].ToString() == "AutoComplete")
                            {
                                sb.Append("					c" + drv["ColumnName"].ToString() + ".AutoCompleteUrl = \"AutoComplete.aspx/RptCriDdlSuggests\";" + Environment.NewLine);
                                sb.Append("					c" + drv["ColumnName"].ToString() + ".DataContext = context;" + Environment.NewLine);
                                sb.Append("					c" + drv["ColumnName"].ToString() + ".Mode = \"A\";" + Environment.NewLine);
                            }
                            else
                            {
                                sb.Append("					Session[\"" + dw["ProgramName"].ToString() + drv["ColumnName"].ToString() + "\"] = context;" + Environment.NewLine);
                                sb.Append("					c" + drv["ColumnName"].ToString() + ".Attributes[\"ac_url\"] = \"AutoComplete.aspx/RptCriDdlSuggestsEx\";" + Environment.NewLine);
                                sb.Append("					c" + drv["ColumnName"].ToString() + ".Attributes[\"ac_context\"] = \"" + dw["ProgramName"].ToString() + drv["ColumnName"].ToString() + "\";" + Environment.NewLine);
                                sb.Append("					c" + drv["ColumnName"].ToString() + ".Attributes[\"refColCID\"] = " + (string.IsNullOrEmpty(@ddlFtrColumeName) ? "null" : "c" + @ddlFtrColumeName + ".ClientID") + ";" + Environment.NewLine);
                            }
                            if ("TextBox,CheckBox,ComboBox,DropDownList,ListBox,RadioButtonList".IndexOf(drv["DisplayName"].ToString()) >= 0) { sb.Append("					c" + drv["ColumnName"].ToString() + ".AutoPostBack = " + (dvDependent.Count > 0 ? "true" : "false") + ";" + Environment.NewLine); }
                            sb.Append("				}" + Environment.NewLine);

                        }
                        if ("ComboBox".IndexOf(drv["DisplayName"].ToString()) < 0)
						{
							sb.Append("				c" + drv["ColumnName"].ToString() + ".DataBind();" + Environment.NewLine);
						}
						if ("ListBox".IndexOf(drv["DisplayName"].ToString()) >= 0)
						{
							sb.Append("				GetSelectedItems(c" + drv["ColumnName"].ToString() + ", dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString());" + Environment.NewLine);
							bListBox = true;
						}
						else
						{
							sb.Append("				try" + Environment.NewLine);
							sb.Append("				{" + Environment.NewLine);
							if ("ComboBox".IndexOf(drv["DisplayName"].ToString()) >= 0)
							{
								sb.Append("					c" + drv["ColumnName"].ToString() + ".SelectByValue(dt.Rows[" + ii.ToString() + "][\"LastCriteria\"],string.Empty,false);" + Environment.NewLine);
							}
							else
							{
								sb.Append("					c" + drv["ColumnName"].ToString() + ".Items.FindByValue(dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString()).Selected = true;" + Environment.NewLine);
							}
							sb.Append("				}" + Environment.NewLine);
							sb.Append("				catch" + Environment.NewLine);
							sb.Append("				{" + Environment.NewLine);
							sb.Append("					try {c" + drv["ColumnName"].ToString() + ".SelectedIndex = 0;} catch {}" + Environment.NewLine);
							sb.Append("				}" + Environment.NewLine);
						}
					}
					else
					{
						if ("Calendar".IndexOf(drv["DisplayName"].ToString()) >= 0)
						{
							sb.Append("				if (dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString() != string.Empty)" + Environment.NewLine);
							sb.Append("				{" + Environment.NewLine);
							sb.Append("					c" + drv["ColumnName"].ToString() + ".SelectedDate = DateTime.Parse(dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString());" + Environment.NewLine);
							sb.Append("					c" + drv["ColumnName"].ToString() + ".VisibleDate = c" + drv["ColumnName"].ToString() + ".SelectedDate;" + Environment.NewLine);
							sb.Append("				}" + Environment.NewLine);
						}
						else //TextBox
						{
							sb.Append("				if (dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString() != string.Empty)" + Environment.NewLine);
							sb.Append("				{" + Environment.NewLine);
							sb.Append("					c" + drv["ColumnName"].ToString() + ".Text = dt.Rows[" + ii.ToString() + "][\"LastCriteria\"].ToString();" + Environment.NewLine);
							sb.Append("				}" + Environment.NewLine);
						}
					}
				}
				ii = ii + 1;
			}
			//sb.Append("				if (Request.QueryString[\"export\"] != null)" + Environment.NewLine);
			//sb.Append("				{" + Environment.NewLine);
			//sb.Append("					if (Request.QueryString[\"export\"].Equals(\"TXT\"))" + Environment.NewLine);
			//sb.Append("					{" + Environment.NewLine);
			//sb.Append("						getReport(false, exportTo.TXT);" + Environment.NewLine);
			//sb.Append("					}" + Environment.NewLine);
			//if (dw["ReportTypeCd"].ToString() == "X")
			//{
			//    sb.Append("					else if (Request.QueryString[\"export\"].Equals(\"XML\"))" + Environment.NewLine);
			//    sb.Append("					{" + Environment.NewLine);
			//    sb.Append("						getReport(false, exportTo.XML);" + Environment.NewLine);
			//    sb.Append("					}" + Environment.NewLine);
			//}
			//else if (dw["ReportTypeCd"].ToString() == "C")
			//{
			//    sb.Append("					else if (Request.QueryString[\"export\"].Equals(\"XLS\"))" + Environment.NewLine);
			//    sb.Append("					{" + Environment.NewLine);
			//    sb.Append("						getReport(false, exportTo.XLS);" + Environment.NewLine);
			//    sb.Append("					}" + Environment.NewLine);
			//    sb.Append("					else if (Request.QueryString[\"export\"].Equals(\"PDF\"))" + Environment.NewLine);
			//    sb.Append("					{" + Environment.NewLine);
			//    sb.Append("						getReport(false, exportTo.PDF);" + Environment.NewLine);
			//    sb.Append("					}" + Environment.NewLine);
			//    sb.Append("					else if (Request.QueryString[\"export\"].Equals(\"DOC\"))" + Environment.NewLine);
			//    sb.Append("					{" + Environment.NewLine);
			//    sb.Append("						getReport(false, exportTo.DOC);" + Environment.NewLine);
			//    sb.Append("					}" + Environment.NewLine);
			//}
			//sb.Append("				}" + Environment.NewLine);
			sb.Append("				DataTable dtHlp = GetReportHlp();" + Environment.NewLine);
			sb.Append("				cHelpMsg.HelpTitle = dtHlp.Rows[0][\"ReportTitle\"].ToString(); cHelpMsg.HelpMsg = dtHlp.Rows[0][\"DefaultHlpMsg\"].ToString();" + Environment.NewLine);
			sb.Append("				cTitleLabel.Text = dtHlp.Rows[0][\"ReportTitle\"].ToString();" + Environment.NewLine);
            //sb.Append("				if (base.LPref != null && base.LPref.PageBannerImg != null && base.LPref.PageBannerImg.Trim() != string.Empty) {cTitleLabel.Visible = false;} else {cTitleLabel.Text = dtHlp.Rows[0][\"ReportTitle\"].ToString();}" + Environment.NewLine);
            sb.Append("				SetClientRule();" + Environment.NewLine);
            //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            //sb.Append("					AdminFacade().LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0][\"ReportTitle\"].ToString(), 0, " + reportId.ToString() + ", 0, string.Empty, LcSysConnString, LcAppPw);" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
            //sb.Append("				else" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            sb.Append("				(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0][\"ReportTitle\"].ToString(), 0, " + reportId.ToString() + ", 0, string.Empty, LcSysConnString, LcAppPw);" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append("				if (bBatchPrint)" + Environment.NewLine);
				sb.Append("				{" + Environment.NewLine);
				sb.Append("					getReport(true, exportTo.VIEW);" + Environment.NewLine);
				sb.Append("					Response.Write(\"<script lang='javascript'>opener=self;window.close();</script>\");" + Environment.NewLine);
				sb.Append("				}" + Environment.NewLine);
			}
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
			if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append("			this.cViewer.Search += new CrystalDecisions.Web.SearchEventHandler(this.cViewer_Search);" + Environment.NewLine);
				sb.Append("			this.cViewer.ViewZoom += new CrystalDecisions.Web.ZoomEventHandler(this.cViewer_ViewZoom);" + Environment.NewLine);
				sb.Append("			this.cViewer.Navigate += new CrystalDecisions.Web.NavigateEventHandler(this.cViewer_Navigate);" + Environment.NewLine);
				sb.Append("			this.cViewer.Drill += new CrystalDecisions.Web.DrillEventHandler(this.cViewer_Drill);" + Environment.NewLine);
				//sb.Append("			this.cViewer.PreRender += new System.EventHandler(this.cViewer_PreRender);" + Environment.NewLine);
			}
			if (clientFrwork == "1") {sb.Append("			this.Load += new System.EventHandler(this.Page_Load);" + Environment.NewLine);}
			sb.Append(Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append("		#endregion" + Environment.NewLine);
			foreach (DataRowView drv in dvCri)
			{
				sb.Append(Environment.NewLine);
				if (",ComboBox,DropDownList,ListBox,RadioButtonList,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
				{
					sb.Append("		protected void c" + drv["ColumnName"].ToString() + "_SelectedIndexChanged(object sender, System.EventArgs e)" + Environment.NewLine);
				}
				else if (",CheckBox,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
				{
					sb.Append("		protected void c" + drv["ColumnName"].ToString() + "_CheckedChanged(object sender, System.EventArgs e)" + Environment.NewLine);
				}
				else if (",Calendar,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
				{
					sb.Append("		protected void c" + drv["ColumnName"].ToString() + "_SelectionChanged(object sender, System.EventArgs e)" + Environment.NewLine);
				}
				else if (",Button,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
				{
					sb.Append("		protected void c" + drv["ColumnName"].ToString() + "_Click(object sender, System.EventArgs e)" + Environment.NewLine);
				}
				else if (",ImageButton,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
				{
					sb.Append("		protected void c" + drv["ColumnName"].ToString() + "_Click(object sender, System.Web.UI.ImageClickEventArgs e)" + Environment.NewLine);
				}
				else
				{
					sb.Append("		protected void c" + drv["ColumnName"].ToString() + "_TextChanged(object sender, System.EventArgs e)" + Environment.NewLine);
				}
				sb.Append("		{" + Environment.NewLine);
                sb.Append("			if (!IsPostBack) return;" + Environment.NewLine);
                if (",ComboBox,DropDownList,ListBox,RadioButtonList,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
                {
                    string ddlFtrColumeName = drv["DdlFtrColumnName"].ToString();
                    if ("ComboBox".IndexOf(drv["DisplayName"].ToString()) < 0 && !string.IsNullOrEmpty(ddlFtrColumeName))
                    {
                        sb.Append("			DataView dv = new DataView((new AdminSystem()).GetIn(" + reportId.ToString() + ",\"GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "\"," + (drv["DisplayName"].ToString() == "ListBox" ? "0" : "(new SqlReportSystem()).CountRptCri(\"" + drv["ReportCriId"].ToString() + "\",LcSysConnString,LcAppPw)") + ",\"" + drv["RequiredValid"].ToString() + "\",base.LImpr,base.LCurr" + Robot.GetCnStr("N", "N") + "));" + Environment.NewLine);
                        sb.Append("			dv.RowFilter = GetCriteriaRowFilter(dv.Table," + "\"" + ddlFtrColumeName + "\"" + ",GetCriteriaColumnValue(cCriteria," + "\"" + "c" + ddlFtrColumeName + "\"" + ")" + ");" + Environment.NewLine);
                        sb.Append("			c" + drv["ColumnName"].ToString() + ".DataSource = dv;" + Environment.NewLine);
                        sb.Append("			c" + drv["ColumnName"].ToString() + ".DataBind();" + Environment.NewLine);
                    }
                }
                DataView dvDependent = new DataView(dvCri.Table, "DdlFtrColumnId = " + drv["ReportCriId"].ToString() + " AND DisplayMode <> 'AutoComplete' AND DisplayName in ('ComboBox','DropDownList','ListBox','RadioButtonList')", "", DataViewRowState.CurrentRows);
                foreach (DataRowView drvDependent in dvDependent)
                {
                    sb.Append("			c" + drvDependent["ColumnName"].ToString() + "_SelectedIndexChanged(c" + drvDependent["ColumnName"].ToString() + ", e);" + Environment.NewLine);
                }
                if (",ComboBox,DropDownList,ListBox,RadioButtonList,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
                {
                    if (clientFrwork == "1")
                    {
                        sb.Append("			Session[\"CtrlToFocus\"] = ((");
                        if (",ComboBox,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0) { sb.Append("RoboCoder.WebControls.ComboBox)sender).FocusID;" + Environment.NewLine); } else { sb.Append(drv["DisplayName"].ToString() + ")sender).ClientID;" + Environment.NewLine); }
                    }
                    else
                    {
                        sb.Append("			ScriptManager.GetCurrent(Parent.Page).SetFocus(((");
                        if (",ComboBox,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0) { sb.Append("RoboCoder.WebControls.ComboBox)sender).FocusID);" + Environment.NewLine); } else { sb.Append(drv["DisplayName"].ToString() + ")sender).ClientID);" + Environment.NewLine); }
                    }
                }
                sb.Append("		}" + Environment.NewLine);
            }
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
            sb.Append("				if ((Config.DeployType == \"DEV\" || row[\"dbAppDatabase\"].ToString() == base.CPrj.EntityCode + \"View\") && !(base.CPrj.EntityCode != \"RO\" && row[\"SysProgram\"].ToString() == \"Y\") && (new AdminSystem()).IsRegenNeeded(string.Empty,0," + reportId.ToString() + ",0,LcSysConnString,LcAppPw))" + Environment.NewLine);
            sb.Append("				{" + Environment.NewLine);
            sb.Append("					(new GenReportsSystem()).CreateProgram(string.Empty," + reportId.ToString() + ", \"" + reportTitle + "\", row[\"dbAppDatabase\"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);" + Environment.NewLine);
            sb.Append("					Response.Redirect(Request.RawUrl);" + Environment.NewLine);
            sb.Append("				}" + Environment.NewLine);
            sb.Append("			}" + Environment.NewLine);
            sb.Append("			catch (Exception e) { PreMsgPopup(e.Message); }" + Environment.NewLine);
            sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private void SetButtonHlp()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			DataTable dt;" + Environment.NewLine);
            //sb.Append("			if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("			{" + Environment.NewLine);
            //sb.Append("				dt = XmlUtils.XmlToDataTable(AdminFacade().GetButtonHlp(0," + reportId.ToString() + ",0,base.LUser.CultureId,LcSysConnString,LcAppPw));" + Environment.NewLine);
            //sb.Append("			}" + Environment.NewLine);
            //sb.Append("			else" + Environment.NewLine);
            //sb.Append("			{" + Environment.NewLine);
            sb.Append("			dt = (new AdminSystem()).GetButtonHlp(0," + reportId.ToString() + ",0,base.LUser.CultureId,LcSysConnString,LcAppPw);" + Environment.NewLine);
            //sb.Append("			}" + Environment.NewLine);
			sb.Append("			if (dt != null && dt.Rows.Count > 0)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				foreach (DataRow dr in dt.Rows)" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
            sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"ClearCri\") { cClearCriButton.CssClass = \"ButtonImg ClearCriButtonImg\"; Session[KEY_bClCriVisible] = base.GetBool(dr[\"ButtonVisible\"].ToString()); cClearCriButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
            sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"ShowCri\") { cShowCriButton.CssClass = \"ButtonImg ShowCriButtonImg\"; cShowCriButton.Text = dr[\"ButtonName\"].ToString(); Session[KEY_bShCriVisible] = base.GetBool(dr[\"ButtonVisible\"].ToString()); cShowCriButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "G")
			{
                sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"Search\") { cSearchButton.CssClass = \"ButtonImg GSearchButtonImg\"; cSearchButton.Text = dr[\"ButtonName\"].ToString(); cSearchButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cSearchButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
			}
			else
			{
                sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"ExpTxt\") { cExpTxtButton.CssClass = \"ButtonImg ExpTxtButtonImg\"; cExpTxtButton.Text = dr[\"ButtonName\"].ToString(); cExpTxtButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cExpTxtButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
				if (dw["ReportTypeCd"].ToString() == "X")
				{
                    sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"ExpXml\") { cExpXmlButton.CssClass = \"ButtonImg ExpXmlButtonImg\"; cExpXmlButton.Text = dr[\"ButtonName\"].ToString(); cExpXmlButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cExpXmlButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
				}
				else
				{
                    sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"View\") { cViewButton.CssClass = \"ButtonImg ViewButtonImg\"; cViewButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cViewButton.Text = dr[\"ButtonName\"].ToString(); cViewButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
                    sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"ExpPdf\") { cExpPdfButton.CssClass = \"ButtonImg ExpPdfButtonImg\"; cExpPdfButton.Text = dr[\"ButtonName\"].ToString(); cExpPdfButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cExpPdfButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
                    sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"ExpDoc\") { cExpDocButton.CssClass = \"ButtonImg ExpDocButtonImg\"; cExpDocButton.Text = dr[\"ButtonName\"].ToString(); cExpDocButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cExpDocButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
                    sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"ExpXls\") { cExpXlsButton.CssClass = \"ButtonImg ExpXlsButtonImg\"; cExpXlsButton.Text = dr[\"ButtonName\"].ToString(); cExpXlsButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cExpXlsButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
                    sb.Append("					if (dr[\"ButtonTypeName\"].ToString() == \"Print\") { cPrintButton.CssClass = \"ButtonImg PrintButtonImg\"; cPrintButton.Text = dr[\"ButtonName\"].ToString(); cPrintButton.Visible = base.GetBool(dr[\"ButtonVisible\"].ToString()); cPrintButton.ToolTip = dr[\"ButtonToolTip\"].ToString(); }" + Environment.NewLine);
				}
			}
			sb.Append("				}" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private DataTable GetClientRule()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			DataTable dtRul = (DataTable)Session[KEY_dtClientRule];" + Environment.NewLine);
			sb.Append("			if (dtRul == null)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				CheckAuthentication(false);" + Environment.NewLine);
            //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            //sb.Append("					dtRul = XmlUtils.XmlToDataTable(AdminFacade().GetClientRule(0," + reportId.ToString() + ",base.LUser.CultureId,LcSysConnString,LcAppPw));" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
            //sb.Append("				else" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            sb.Append("				dtRul = (new AdminSystem()).GetClientRule(0," + reportId.ToString() + ",base.LUser.CultureId,LcSysConnString,LcAppPw);" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
			sb.Append("				Session[KEY_dtClientRule] = dtRul;" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("			return dtRul;" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private void SetClientRule()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			DataView dvRul = new DataView(GetClientRule());" + Environment.NewLine);
			sb.Append("			if (dvRul.Count > 0)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				WebControl cc = null;" + Environment.NewLine);
			sb.Append("				string srp = string.Empty;" + Environment.NewLine);
			sb.Append("				string sn = string.Empty;" + Environment.NewLine);
			sb.Append("				string st = string.Empty;" + Environment.NewLine);
			sb.Append("				int ii = 0;" + Environment.NewLine);
			sb.Append("				foreach (DataRowView drv in dvRul)" + Environment.NewLine);
			sb.Append("				{" + Environment.NewLine);
			sb.Append("					srp = drv[\"ScriptName\"].ToString();" + Environment.NewLine);
			sb.Append("					if (drv[\"ParamName\"].ToString() != string.Empty)" + Environment.NewLine);
			sb.Append("					{" + Environment.NewLine);
			sb.Append("						StringBuilder sbName =  new StringBuilder();" + Environment.NewLine);
			sb.Append("						StringBuilder sbType =  new StringBuilder();" + Environment.NewLine);
			sb.Append("						sbName.Append(drv[\"ParamName\"].ToString().Trim());" + Environment.NewLine);
			sb.Append("						sbType.Append(drv[\"ParamType\"].ToString().Trim());" + Environment.NewLine);
			sb.Append("						ii = 0;" + Environment.NewLine);
			sb.Append("						while (sbName.Length > 0)" + Environment.NewLine);
			sb.Append("						{" + Environment.NewLine);
			sb.Append("							sn = Utils.PopFirstWord(sbName,(char)44); st = Utils.PopFirstWord(sbType,(char)44);" + Environment.NewLine);
			sb.Append("							if (st.ToLower() == \"combobox\") {srp = srp.Replace(\"@\" + ii.ToString() + \"@\",((RoboCoder.WebControls.ComboBox)this.FindControl(sn)).FocusID);} else {srp = srp.Replace(\"@\" + ii.ToString() + \"@\",((WebControl)this.FindControl(sn)).ClientID);}" + Environment.NewLine);
			sb.Append("							ii = ii + 1;" + Environment.NewLine);
			sb.Append("						}" + Environment.NewLine);
			sb.Append("					}" + Environment.NewLine);
			sb.Append("					cc = this.FindControl(drv[\"ColName\"].ToString()) as WebControl;" + Environment.NewLine);
			sb.Append("					if (cc != null && (cc.Attributes[drv[\"ScriptEvent\"].ToString()] == null || cc.Attributes[drv[\"ScriptEvent\"].ToString()].IndexOf(srp) < 0)) {cc.Attributes[drv[\"ScriptEvent\"].ToString()] += srp;}" + Environment.NewLine);
			sb.Append("				}" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private DataTable GetReportCriHlp()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			DataTable dtCri = (DataTable)Session[KEY_dtCri];" + Environment.NewLine);
			sb.Append("			if (dtCri == null)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				CheckAuthentication(false);" + Environment.NewLine);
            //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            //sb.Append("					dtCri = XmlUtils.XmlToDataTable(AdminFacade().GetReportCriHlp(" + reportId.ToString() + ",base.LUser.CultureId,LcSysConnString,LcAppPw));" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
            //sb.Append("				else" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            sb.Append("				dtCri = (new AdminSystem()).GetReportCriHlp(" + reportId.ToString() + ",base.LUser.CultureId,LcSysConnString,LcAppPw);" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
			sb.Append("				Session[KEY_dtCri] = dtCri;" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("			return dtCri;" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private DataTable GetReportHlp()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			DataTable dtHlp = (DataTable)Session[KEY_dtReportHlp];" + Environment.NewLine);
			sb.Append("			if (dtHlp == null)" + Environment.NewLine);
			sb.Append("			{" + Environment.NewLine);
			sb.Append("				CheckAuthentication(false);" + Environment.NewLine);
            //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            //sb.Append("					dtHlp = XmlUtils.XmlToDataTable(AdminFacade().GetReportHlp(" + reportId.ToString() + ",base.LUser.CultureId,LcSysConnString,LcAppPw));" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
            //sb.Append("				else" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            sb.Append("			    dtHlp = (new AdminSystem()).GetReportHlp(" + reportId.ToString() + ",base.LUser.CultureId,LcSysConnString,LcAppPw);" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
			sb.Append("				Session[KEY_dtReportHlp] = dtHlp;" + Environment.NewLine);
			sb.Append("			}" + Environment.NewLine);
			sb.Append("			return dtHlp;" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			if (bListBox)
			{
				sb.Append(Environment.NewLine);
				sb.Append("		private void GetSelectedItems(ListBox cObj, string selectedItems)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			string selectedItem;" + Environment.NewLine);
				sb.Append("			bool finish;" + Environment.NewLine);
				sb.Append("			if (selectedItems == string.Empty)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				try {cObj.SelectedIndex = 0;} catch {}" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			else" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				finish = false;" + Environment.NewLine);
				sb.Append("				while (!finish)" + Environment.NewLine);
				sb.Append("				{" + Environment.NewLine);
				sb.Append("					selectedItem = GetSelectedItem(ref selectedItems);" + Environment.NewLine);
				sb.Append("					if (selectedItem == string.Empty)" + Environment.NewLine);
				sb.Append("					{" + Environment.NewLine);
				sb.Append("						finish = true;" + Environment.NewLine);
				sb.Append("					}" + Environment.NewLine);
				sb.Append("					else" + Environment.NewLine);
				sb.Append("					{" + Environment.NewLine);
				sb.Append("						try" + Environment.NewLine);
				sb.Append("						{" + Environment.NewLine);
				sb.Append("							cObj.Items.FindByValue(selectedItem).Selected = true;" + Environment.NewLine);
				sb.Append("						}" + Environment.NewLine);
				sb.Append("						catch" + Environment.NewLine);
				sb.Append("						{" + Environment.NewLine);
				sb.Append("							finish = true;" + Environment.NewLine);
				sb.Append("							try {cObj.SelectedIndex = 0;} catch {}" + Environment.NewLine);
				sb.Append("						}" + Environment.NewLine);
				sb.Append("					}" + Environment.NewLine);
				sb.Append("				}" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
				sb.Append(Environment.NewLine);
				sb.Append("		private string GetSelectedItem(ref string selectedItems)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			string selectedItem;" + Environment.NewLine);
				sb.Append("			int sIndex = selectedItems.IndexOf(\"'\");" + Environment.NewLine);
				sb.Append("			int eIndex = selectedItems.IndexOf(\"'\",sIndex + 1);" + Environment.NewLine);
				sb.Append("			if (sIndex >= 0 && eIndex >= 0)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				selectedItem = selectedItems.Substring(sIndex + 1, eIndex - sIndex - 1);" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			else" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				selectedItem = string.Empty;" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			selectedItems = selectedItems.Substring(eIndex + 1, selectedItems.Length - eIndex - 1);" + Environment.NewLine);
				sb.Append("			return selectedItem;" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
			}
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
            sb.Append("		private DataView GetRptCriteria()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
            sb.Append("			return ((new SqlReportSystem()).GetReportCriteria(string.Empty,\"" + reportId.ToString() + "\",LcSysConnString,LcAppPw)).DefaultView;" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private Ds" + dw["ProgramName"].ToString() + "In UpdCriteria(bool bUpdate)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			Ds" + dw["ProgramName"].ToString() + "In ds = new Ds" + dw["ProgramName"].ToString() + "In();" + Environment.NewLine);
			sb.Append("			DataRow dr = ds.Tables[\"Dt" + dw["ProgramName"].ToString() + "In\"].NewRow();" + Environment.NewLine);
            sb.Append("			bool bAll = false; string selectedVal = null; DataView dv = null;int TotalChoiceCnt = 0;int CriCnt=0;bool noneSelected=true;" + Environment.NewLine);
            foreach (DataRowView drv in dvCri)
            {
                if ("ListBox".IndexOf(drv["DisplayName"].ToString()) >= 0)
                {
                    sb.Append("			bAll = false;" + Environment.NewLine);
                    sb.Append("			CriCnt = (new SqlReportSystem()).CountRptCri(\"" + drv["ReportCriId"].ToString() + "\",LcSysConnString,LcAppPw);" + Environment.NewLine);
                    sb.Append("			TotalChoiceCnt = new DataView((new AdminSystem()).GetIn(" + reportId.ToString() + ",\"GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "\",0,\"" + drv["RequiredValid"].ToString() + "\"" + ",base.LImpr,base.LCurr" + Robot.GetCnStr("N", "N") + ")).Count;" + Environment.NewLine);
                    if (drv["DisplayMode"].ToString() == "AutoListBox")
                    {
                        sb.Append("			try { selectedVal = c" + drv["ColumnName"].ToString() + "Hidden.Text; } catch { selectedVal = null; };" + Environment.NewLine);
                        sb.Append("			dv = new DataView((new AdminSystem()).GetIn(" + reportId.ToString() + ",\"GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "\",0,\"" + drv["RequiredValid"].ToString() + "\"" + (drv["DisplayMode"].ToString() == "AutoListBox" ? ",false,selectedVal" : "") + ",base.LImpr,base.LCurr" + Robot.GetCnStr("N", "N") + "));" + Environment.NewLine);
                        sb.Append("			c" + drv["ColumnName"].ToString() + ".DataSource = dv;c" + drv["ColumnName"].ToString() + ".DataBind();" + Environment.NewLine);
                        sb.Append("			GetSelectedItems(c" + drv["ColumnName"].ToString() + ", selectedVal);" + Environment.NewLine);
                    }
                    sb.Append("			selectedVal = string.Join(\",\", c" + drv["ColumnName"].ToString() + ".Items.Cast<ListItem>().Where(x => x.Selected).Select(x => \"'\" + x.Value + \"'\").ToArray());" + Environment.NewLine);
                    sb.Append("			noneSelected = string.IsNullOrEmpty(selectedVal) || selectedVal == \"''\";" + Environment.NewLine);
                    if (drv["RequiredValid"].ToString() == "Y") sb.Append("			if (IsPostBack && noneSelected) { throw new ApplicationException(\"Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.\");};" + Environment.NewLine);
                    sb.Append("			dr[\"" + drv["ColumnName"].ToString() + "\"] = \"(\";" + Environment.NewLine);
                    sb.Append("			foreach (ListItem li in c" + drv["ColumnName"].ToString() + ".Items)" + Environment.NewLine);
                    sb.Append("			{" + Environment.NewLine);
                    sb.Append("				if (li.Selected || (noneSelected && CriCnt+1 > TotalChoiceCnt))" + Environment.NewLine);
                    sb.Append("				{" + Environment.NewLine);
                    sb.Append("					if ((li.Value.Trim() == string.Empty && noneSelected) || (noneSelected && CriCnt + 1 > TotalChoiceCnt)) { bAll = true; noneSelected = true; }" + Environment.NewLine);
                    sb.Append("					if (bAll || li.Value.Trim() == string.Empty || li.Selected)" + Environment.NewLine);
                    sb.Append("					{" + Environment.NewLine);
                    sb.Append("					    if (dr[\"" + drv["ColumnName"].ToString() + "\"].ToString() != \"(\")" + Environment.NewLine);
                    sb.Append("					    {" + Environment.NewLine);
                    sb.Append("						    dr[\"" + drv["ColumnName"].ToString() + "\"] = dr[\"" + drv["ColumnName"].ToString() + "\"].ToString() + \",\";" + Environment.NewLine);
                    sb.Append("					    }" + Environment.NewLine);
                    sb.Append("					    dr[\"" + drv["ColumnName"].ToString() + "\"] = dr[\"" + drv["ColumnName"].ToString() + "\"].ToString() + \"'\" + (li.Value.ToString().Trim() == string.Empty && CriCnt+1 > TotalChoiceCnt ? \"0\" : li.Value.ToString()) + \"'\";" + Environment.NewLine);
                    sb.Append("					}" + Environment.NewLine);
                    sb.Append("				}" + Environment.NewLine);
                    sb.Append("			}" + Environment.NewLine);
                    if (drv["RequiredValid"].ToString() == "Y")
                    {
                        sb.Append("			dr[\"" + drv["ColumnName"].ToString() + "\"] = dr[\"" + drv["ColumnName"].ToString() + "\"].ToString() + \")\";" + Environment.NewLine);
                    }
                    else
                    {
                        sb.Append("			if (dr[\"" + drv["ColumnName"].ToString() + "\"].ToString() == \"(''\" || dr[\"" + drv["ColumnName"].ToString() + "\"].ToString() == \"(\") {dr[\"" + drv["ColumnName"].ToString() + "\"] = string.Empty;} else {dr[\"" + drv["ColumnName"].ToString() + "\"] = dr[\"" + drv["ColumnName"].ToString() + "\"].ToString() + \")\";}" + Environment.NewLine);
                    }
                }
                else
				{
					if ("Calendar".IndexOf(drv["DisplayName"].ToString()) >= 0)
					{
						sb.Append("			if (c" + drv["ColumnName"].ToString() + ".SelectedDate > DateTime.Parse(\"0001-01-01\")) {dr[\"" + drv["ColumnName"].ToString() + "\"] = c" + drv["ColumnName"].ToString() + ".SelectedDate;}" + Environment.NewLine);
					}
					else
					{
						if ("ComboBox,DropDownList,ListBox,RadioButtonList".IndexOf(drv["DisplayName"].ToString()) >= 0)
						{
							sb.Append("			if (c" + drv["ColumnName"].ToString() + ".SelectedIndex >= 0 && c" + drv["ColumnName"].ToString() + ".SelectedValue != string.Empty) {dr[\"" + drv["ColumnName"].ToString() + "\"] = c" + drv["ColumnName"].ToString() + ".SelectedValue;}" + Environment.NewLine);
                            if (drv["RequiredValid"].ToString() == "Y") sb.Append("			if (IsPostBack && c" + drv["ColumnName"].ToString() + ".SelectedValue == string.Empty) { throw new ApplicationException(\"Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.\");};" + Environment.NewLine);
                        }
                        else
						{
							if ("CheckBox".IndexOf(drv["DisplayName"].ToString()) >= 0)
							{
								sb.Append("			dr[\"" + drv["ColumnName"].ToString() + "\"] = base.SetBool(c" + drv["ColumnName"].ToString() + ".Checked);" + Environment.NewLine);
							}
                            else if (",DateUTC,".IndexOf("," + drv["DisplayMode"].ToString() + ",") >= 0)   // Non-existing for now.
                            {
                                // should be DateUTC to signal coversion needed
                                sb.Append("			if (c" + drv["ColumnName"].ToString() + ".Text != string.Empty) {dr[\"" + drv["ColumnName"].ToString() + "\"] = base.SetDateTimeUTC(c" + drv["ColumnName"].ToString() + ".Text, !bUpdate);}" + Environment.NewLine);
                            }
                            else if (drv["DisplayName"].ToString().IndexOf("Date") >= 0)
                            {
                                sb.Append("			if (c" + drv["ColumnName"].ToString() + ".Text != string.Empty) {dr[\"" + drv["ColumnName"].ToString() + "\"] = DateTime.Parse(c" + drv["ColumnName"].ToString() + ".Text,System.Threading.Thread.CurrentThread.CurrentCulture);}" + Environment.NewLine);
                            }
                            else	//TextBox
                            {
                                sb.Append("			if (c" + drv["ColumnName"].ToString() + ".Text != string.Empty) {dr[\"" + drv["ColumnName"].ToString() + "\"] = c" + drv["ColumnName"].ToString() + ".Text;}" + Environment.NewLine);
                            }
                            if (drv["RequiredValid"].ToString() == "Y" && "CheckBox".IndexOf(drv["DisplayName"].ToString()) < 0) sb.Append("			if (IsPostBack && c" + drv["ColumnName"].ToString() + ".Text == string.Empty) { throw new ApplicationException(\"Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.\");};" + Environment.NewLine);
                        }
					}
				}
			}
			sb.Append("			ds.Tables[\"Dt" + dw["ProgramName"].ToString() + "In\"].Rows.Add(dr);" + Environment.NewLine);
            //sb.Append("			if (Config.Architect == \"W\")" + Environment.NewLine);
            //sb.Append("			{" + Environment.NewLine);
            //sb.Append("				if (bUpdate) {" + dw["ProgramName"].ToString() + "Facade().Upd" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ", base.LUser.UsrId, XmlUtils.DataSetToXml(ds)" + Robot.GetCnCall("N", "N") + ");}" + Environment.NewLine);
            //sb.Append("			}" + Environment.NewLine);
            //sb.Append("			else" + Environment.NewLine);
            //sb.Append("			{" + Environment.NewLine);
            sb.Append("			if (bUpdate) {(new AdminSystem()).UpdRptDt(" + reportId.ToString() + ",\"Upd" + dw["ProgramName"].ToString() + "\",base.LUser.UsrId,ds,GetRptCriteria()" + Robot.GetCnStr("N", "N") + ");}" + Environment.NewLine);
            //sb.Append("			if (bUpdate) {(new " + dw["ProgramName"].ToString() + "System()).Upd" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ", base.LUser.UsrId, ds" + Robot.GetCnCall("N", "N") + ");}" + Environment.NewLine);
            //sb.Append("			}" + Environment.NewLine);
			sb.Append("			return ds;" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append(Environment.NewLine);
				sb.Append("		private DataView GetReportSct()" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			DataTable dtSct = (DataTable)Session[KEY_dtReportSct];" + Environment.NewLine);
				sb.Append("			if (dtSct == null)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				CheckAuthentication(false);" + Environment.NewLine);
                //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
                //sb.Append("				{" + Environment.NewLine);
                //sb.Append("					dtSct = XmlUtils.XmlToDataTable(AdminFacade().GetReportSct());" + Environment.NewLine);
                //sb.Append("				}" + Environment.NewLine);
                //sb.Append("				else" + Environment.NewLine);
                //sb.Append("				{" + Environment.NewLine);
                sb.Append("				dtSct = (new AdminSystem()).GetReportSct();" + Environment.NewLine);
                //sb.Append("				}" + Environment.NewLine);
				sb.Append("				Session[KEY_dtReportSct] = dtSct;" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			return dtSct.DefaultView;" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
				sb.Append(Environment.NewLine);
				sb.Append("		private DataView GetReportItem()" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			DataTable dtItem = (DataTable)Session[KEY_dtReportItem];" + Environment.NewLine);
				sb.Append("			if (dtItem == null)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				CheckAuthentication(false);" + Environment.NewLine);
                //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
                //sb.Append("				{" + Environment.NewLine);
                //sb.Append("					dtItem = XmlUtils.XmlToDataTable(AdminFacade().GetReportItem(" + reportId.ToString() + ",LcSysConnString,LcAppPw));" + Environment.NewLine);
                //sb.Append("				}" + Environment.NewLine);
                //sb.Append("				else" + Environment.NewLine);
                //sb.Append("				{" + Environment.NewLine);
                sb.Append("				dtItem = (new AdminSystem()).GetReportItem(" + reportId.ToString() + ",LcSysConnString,LcAppPw);" + Environment.NewLine);
                //sb.Append("				}" + Environment.NewLine);
				sb.Append("				DataView dvSct = GetReportSct();" + Environment.NewLine);
				sb.Append("				foreach (DataRowView drv in dvSct) {dtItem = MapReportSct(dtItem, drv[\"ReportSctName\"].ToString());}" + Environment.NewLine);
				sb.Append("				Session[KEY_dtReportItem] = dtItem;" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			return dtItem.DefaultView;" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
				sb.Append(Environment.NewLine);
				sb.Append("		// In case formula referencecs not in order." + Environment.NewLine);
				sb.Append("		private DataTable MapReportSct(DataTable dt, string SctName)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			int ii = 0;" + Environment.NewLine);
				sb.Append("			DataView dv = dt.DefaultView;" + Environment.NewLine);
				sb.Append("			dv.RowFilter = \"ReportSctName = '\" + SctName + \"'\";" + Environment.NewLine);
				sb.Append("			foreach (DataRowView drv in dv)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				ii = ii + 1;" + Environment.NewLine);
				sb.Append("				drv[\"InternalField\"] = SctName + ii.ToString();" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			return dt;" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
				sb.Append(Environment.NewLine);
				sb.Append("		private void SetReportSct(DataView dv, string SctName)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			DataView dvCopy = new DataView(dv.Table.Copy());" + Environment.NewLine);
				sb.Append("			CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition ffd;" + Environment.NewLine);
				sb.Append("			CrystalDecisions.CrystalReports.Engine.FieldObject fo;" + Environment.NewLine);
				sb.Append("			dv.RowFilter = \"ReportSctName = '\" + SctName + \"'\";" + Environment.NewLine);
				sb.Append("			int iPos1;" + Environment.NewLine);
				sb.Append("			int iPos2;" + Environment.NewLine);
                sb.Append("			string sKey;" + Environment.NewLine);
                sb.Append("			foreach (DataRowView drv in dv)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				ffd = rp.DataDefinition.FormulaFields[drv[\"InternalField\"].ToString()];" + Environment.NewLine);
				sb.Append("				fo = (FieldObject)rp.ReportDefinition.ReportObjects[drv[\"InternalField\"].ToString()];" + Environment.NewLine);
				sb.Append("				if (drv[\"FormulaReady\"].ToString() == \"N\")" + Environment.NewLine);
				sb.Append("				{" + Environment.NewLine);
				sb.Append("					drv[\"FormulaReady\"] = \"Y\";" + Environment.NewLine);
				sb.Append("					iPos1 = drv[\"ItemFormula\"].ToString().IndexOf(\"{@\");" + Environment.NewLine);
				sb.Append("					while (iPos1 >= 0)" + Environment.NewLine);
				sb.Append("					{" + Environment.NewLine);
				sb.Append("						iPos2 = drv[\"ItemFormula\"].ToString().IndexOf(\"}\", iPos1 + 1);" + Environment.NewLine);
				sb.Append("						if (iPos2 > iPos1)" + Environment.NewLine);
				sb.Append("						{" + Environment.NewLine);
                sb.Append("							sKey = drv[\"ItemFormula\"].ToString().Substring(iPos1 + 1, iPos2 - iPos1 - 1);" + Environment.NewLine);
                sb.Append("							dvCopy.RowFilter = \"ReportItemName = '\" + sKey + \"'\";" + Environment.NewLine);
                sb.Append("							if (dvCopy.Count != 1) { throw new Exception(\"Referenced Item Issue: Non-unique report item name or referenced item '\" + sKey + \"' in item formula not found!\"); }" + Environment.NewLine);
                sb.Append("							drv[\"ItemFormula\"] = drv[\"ItemFormula\"].ToString().Replace(\"{\" + sKey + \"}\", \"{@\" + dvCopy[0][\"InternalField\"].ToString() + \"}\");" + Environment.NewLine);
                sb.Append("							iPos1 = drv[\"ItemFormula\"].ToString().IndexOf(\"{@\", iPos2 + 1);" + Environment.NewLine);
				sb.Append("						}" + Environment.NewLine);
				sb.Append("						else {iPos1 = -1;}" + Environment.NewLine);
				sb.Append("					}" + Environment.NewLine);
				sb.Append("				}" + Environment.NewLine);
				sb.Append("				ffd.Text = drv[\"ItemFormula\"].ToString();" + Environment.NewLine);
				sb.Append("				if (drv[\"FontBold\"].ToString() == \"Y\" && drv[\"FontItalic\"].ToString() == \"Y\") {fo.ApplyFont(new System.Drawing.Font(drv[\"FontFamily\"].ToString(),float.Parse(drv[\"FontSize\"].ToString()),System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Italic));}" + Environment.NewLine);
				sb.Append("				else if (drv[\"FontBold\"].ToString() == \"Y\") {fo.ApplyFont(new System.Drawing.Font(drv[\"FontFamily\"].ToString(),float.Parse(drv[\"FontSize\"].ToString()),System.Drawing.FontStyle.Bold));}" + Environment.NewLine);
				sb.Append("				else if (drv[\"FontItalic\"].ToString() == \"Y\") {fo.ApplyFont(new System.Drawing.Font(drv[\"FontFamily\"].ToString(),float.Parse(drv[\"FontSize\"].ToString()),System.Drawing.FontStyle.Italic));}" + Environment.NewLine);
				sb.Append("				else {fo.ApplyFont(new System.Drawing.Font(drv[\"FontFamily\"].ToString(),float.Parse(drv[\"FontSize\"].ToString()),System.Drawing.FontStyle.Regular));}" + Environment.NewLine);
				sb.Append("				fo.Left = Int32.Parse(drv[\"PosLeft\"].ToString());" + Environment.NewLine);
				sb.Append("				fo.Top = Int32.Parse(drv[\"PosTop\"].ToString());" + Environment.NewLine);
				sb.Append("				fo.Width = Int32.Parse(drv[\"PosWidth\"].ToString());" + Environment.NewLine);
				sb.Append("				fo.Height = Int32.Parse(drv[\"PosHeight\"].ToString());" + Environment.NewLine);
				sb.Append("				if (drv[\"Suppress\"].ToString() == \"Y\") {fo.ObjectFormat.EnableSuppress = true;} else {fo.ObjectFormat.EnableSuppress = false;}" + Environment.NewLine);
				sb.Append("				if (drv[\"Alignment\"].ToString() == \"C\") {fo.ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.HorizontalCenterAlign;}" + Environment.NewLine);
				sb.Append("				else if (drv[\"Alignment\"].ToString() == \"L\") {fo.ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.LeftAlign;}" + Environment.NewLine);
				sb.Append("				else {fo.ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.RightAlign;}" + Environment.NewLine);
				sb.Append("				if (drv[\"LineTop\"].ToString() == \"S\") {fo.Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine;}" + Environment.NewLine);
				sb.Append("				else if (drv[\"LineTop\"].ToString() == \"D\") {fo.Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.DoubleLine;}" + Environment.NewLine);
				sb.Append("				if (drv[\"LineBottom\"].ToString() == \"S\") {fo.Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine;}" + Environment.NewLine);
				sb.Append("				else if (drv[\"LineBottom\"].ToString() == \"D\") {fo.Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.DoubleLine;}" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
			}
			sb.Append(Environment.NewLine);
			sb.Append("		private void getReport(bool sendToPrinter, exportTo eExport)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "G")		// do not cache results.
			{
				if (dvObj.Count > 0)
				{
					//sb.Append("			DataTable dt;" + Environment.NewLine);
                    //sb.Append("			if (Config.Architect == \"W\")" + Environment.NewLine);
                    //sb.Append("			{" + Environment.NewLine);
                    //sb.Append("				dt = XmlUtils.XmlToDataTable(" + dw["ProgramName"].ToString() + "Facade().Get" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ",XmlUtils.ObjectToXml(base.LImpr),XmlUtils.ObjectToXml(base.LCurr),XmlUtils.DataSetToXml(UpdCriteria(false))" + Robot.GetCnCall("N", "N") + ",false,false,false));" + Environment.NewLine);
                    //sb.Append("			}" + Environment.NewLine);
                    //sb.Append("			else" + Environment.NewLine);
                    //sb.Append("			{" + Environment.NewLine);
                    //sb.Append("			dt = (new " + dw["ProgramName"].ToString() + "System()).Get" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ",base.LImpr,base.LCurr,UpdCriteria(false)" + Robot.GetCnCall("N", "N") + ",false,false,false);" + Environment.NewLine);
                    sb.Append("			DataTable dt = (new AdminSystem()).GetRptDt(" + reportId.ToString() + ",\"Get" + dw["ProgramName"].ToString() + "\",base.LImpr,base.LCurr,UpdCriteria(false),GetRptCriteria()" + Robot.GetCnStr("N", "N") + ",false,false,false);" + Environment.NewLine);
                    //sb.Append("			}" + Environment.NewLine);
					sb.Append("			if (dt.Rows.Count > 0)" + Environment.NewLine);
					sb.Append("			{" + Environment.NewLine);
					foreach (DataRowView drv in dvObj)
					{
						sb.Append("				if (dt.Columns.Contains(\""	+ drv["ColumnName"].ToString() + "\"))");
						sb.Append(" {c" + drv["ColumnName"].ToString() + ".Text = dt.Rows[0][\"" + drv["ColumnName"].ToString() + "\"].ToString();}" + Environment.NewLine);
					}
					sb.Append("			}" + Environment.NewLine);
				}
			}
			else if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append("			string reportName = \"" + dw["ProgramName"].ToString() + "\";" + Environment.NewLine);
				//sb.Append("			DataTable dt;" + Environment.NewLine);
				sb.Append("			cCriteria.Visible = false; cClearCriButton.Visible = false; cShowCriButton.Visible = (bool)Session[KEY_bShCriVisible];" + Environment.NewLine);
                //sb.Append("			if (Config.Architect == \"W\")" + Environment.NewLine);
                //sb.Append("			{" + Environment.NewLine);
                //sb.Append("				dt = XmlUtils.XmlToDataTable(" + dw["ProgramName"].ToString() + "Facade().Get" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ",XmlUtils.ObjectToXml(base.LImpr),XmlUtils.ObjectToXml(base.LCurr),XmlUtils.DataSetToXml(UpdCriteria(false))" + Robot.GetCnCall("N", "N") + ",false,false,false));" + Environment.NewLine);
                //sb.Append("			}" + Environment.NewLine);
                //sb.Append("			else" + Environment.NewLine);
                //sb.Append("			{" + Environment.NewLine);
                sb.Append("			DataTable dt = (new AdminSystem()).GetRptDt(" + reportId.ToString() + ",\"Get" + dw["ProgramName"].ToString() + "\",base.LImpr,base.LCurr,UpdCriteria(false),GetRptCriteria()" + Robot.GetCnStr("N", "N") + ",false,false,false);" + Environment.NewLine);
                //sb.Append("			dt = (new " + dw["ProgramName"].ToString() + "System()).Get" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ",base.LImpr,base.LCurr,UpdCriteria(false)" + Robot.GetCnCall("N", "N") + ",false,false,false);" + Environment.NewLine);
                //sb.Append("			}" + Environment.NewLine);
                sb.Append("			CovertRptUTC(dt);" + Environment.NewLine);
                sb.Append("			if (dt.Rows.Count > 0) {if (dt.Columns.Contains(\"ReportName\")) {reportName = dt.Rows[0][\"ReportName\"].ToString();}}" + Environment.NewLine);
                sb.Append("			else {PreMsgPopup(\"For your information, no data is currently available as per your reporting criteria.\");}" + Environment.NewLine);
                sb.Append("			if (Config.DeployType == \"DEV\" && Config.AppNameSpace == \"" + CPrj.EntityCode + "\")" + Environment.NewLine);
                sb.Append("			{" + Environment.NewLine);
                sb.Append("				DataSet ds = new DataSet();" + Environment.NewLine);
                sb.Append("				ds.Tables.Add(dt);" + Environment.NewLine);
                sb.Append("				ds.DataSetName = \"Ds" + dw["ProgramName"].ToString() + "\";" + Environment.NewLine);
                sb.Append("				ds.Tables[0].TableName = \"Dt" + dw["ProgramName"].ToString() + "\";" + Environment.NewLine);
                sb.Append("				string xsdPath = Server.MapPath(\"~/reports/" + dw["ProgramName"].ToString() + "Report.xsd\");" + Environment.NewLine);
                sb.Append("				using (System.IO.StreamWriter writer = new System.IO.StreamWriter(xsdPath))" + Environment.NewLine);
                sb.Append("				{" + Environment.NewLine);
                sb.Append("					ds.WriteXmlSchema(writer);" + Environment.NewLine);
                sb.Append("					writer.Close();" + Environment.NewLine);
                sb.Append("				}" + Environment.NewLine);
                sb.Append("			}" + Environment.NewLine);
                sb.Append("			reportName = reportName + \"_\" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();" + Environment.NewLine);
                sb.Append("			DataView dvItem = GetReportItem(); DataView dvSct = GetReportSct();" + Environment.NewLine);
				sb.Append("			foreach (DataRowView drv in dvSct) {SetReportSct(dvItem, drv[\"ReportSctName\"].ToString());}" + Environment.NewLine);
				sb.Append("			rp.Refresh();" + Environment.NewLine);
				sb.Append("			if (sendToPrinter)" + Environment.NewLine);	// Must set before SetDataSource.
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				rp.PrintOptions.PrinterName = cPrinter.SelectedItem.Value;" + Environment.NewLine);
				sb.Append("				rp.PrintOptions.PaperOrientation = PaperOrientation.");
				if (dw["OrientationCd"].ToString() == "P") { sb.Append("Portrait"); } else { sb.Append("Landscape"); }
				sb.Append(";" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			rp.SetDataSource(dt);" + Environment.NewLine);
				sb.Append("			cViewer.ReportSource = rp;" + Environment.NewLine);
				sb.Append("			if (cViewerWidth.Value != string.Empty) { cViewer.Width = Unit.Pixel(int.Parse(cViewerWidth.Value)); }" + Environment.NewLine);
				sb.Append("			cViewer.Visible = true;" + Environment.NewLine);
                sb.Append("			cViewer.DisplayGroupTree = false;" + Environment.NewLine);
                sb.Append("			if (sendToPrinter) { rp.PrintToPrinter(1,false,0,0); }" + Environment.NewLine);
                sb.Append("			if (eExport == exportTo.TXT)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				StringBuilder sb = new StringBuilder();" + Environment.NewLine);
				foreach (DataRowView drv in dvObj)
				{
					sb.Append("				if (dt.Columns.Contains(\"" + drv["ColumnName"].ToString() + "\")) {sb.Append(\"" + drv["ColumnHeader"].ToString() + "\" + (char)9);}" + Environment.NewLine);
				}
				sb.Append("				sb.Append(Environment.NewLine);" + Environment.NewLine);
				sb.Append("				DataView dv = new DataView(dt);" + Environment.NewLine);
				sb.Append("				foreach (DataRowView drv in dv)" + Environment.NewLine);
				sb.Append("				{" + Environment.NewLine);
				foreach (DataRowView drv in dvObj)
				{
					sb.Append("					if (dt.Columns.Contains(\"" + drv["ColumnName"].ToString() + "\")) {sb.Append(drv[\"" + drv["ColumnName"].ToString() + "\"].ToString() + (char)9);}" + Environment.NewLine);
				}
				sb.Append("					sb.Append(Environment.NewLine);" + Environment.NewLine);
				sb.Append("				}" + Environment.NewLine);
				sb.Append("				ExportToStream(null, reportName + \".xls\", sb.Replace(\"\\r\\n\",\"\\n\"), exportTo.TXT);" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			else if (eExport == exportTo.XLS)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				ExportToStream(rp, reportName + \".xls\", null, exportTo.XLS);" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			else if (eExport == exportTo.PDF)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				ExportToStream(rp, reportName + \".pdf\", null, exportTo.PDF);" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			else if (eExport == exportTo.DOC)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				ExportToStream(rp, reportName + \".doc\", null, exportTo.DOC);" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
			}
			else //dw["ReportTypeCd"].ToString() == "X":
			{
				sb.Append("			string reportName = \"" + dw["ProgramName"].ToString() + "\";" + Environment.NewLine);
				sb.Append("			Response.Buffer = true;" + Environment.NewLine);
				sb.Append("			Response.ClearHeaders();" + Environment.NewLine);
				sb.Append("			Response.ClearContent();" + Environment.NewLine);
                sb.Append("			Response.ContentType = \"application/vnd.ms-excel\";" + Environment.NewLine);
				sb.Append("			DataTable dt;" + Environment.NewLine);
				sb.Append("			cCriteria.Visible = false; cClearCriButton.Visible = false; cShowCriButton.Visible = (bool)Session[KEY_bShCriVisible];" + Environment.NewLine);
				sb.Append("			if (eExport == exportTo.TXT)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
                //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
                //sb.Append("				{" + Environment.NewLine);
                //sb.Append("					dt = XmlUtils.XmlToDataTable(" + dw["ProgramName"].ToString() + "Facade().Get" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ",XmlUtils.ObjectToXml(base.LImpr),XmlUtils.ObjectToXml(base.LCurr),XmlUtils.DataSetToXml(UpdCriteria(false))" + Robot.GetCnCall("N", "N") + ",false,false,false));" + Environment.NewLine);
                //sb.Append("				}" + Environment.NewLine);
                //sb.Append("				else" + Environment.NewLine);
                //sb.Append("				{" + Environment.NewLine);
                sb.Append("			    dt = (new AdminSystem()).GetRptDt(" + reportId.ToString() + ",\"Get" + dw["ProgramName"].ToString() + "\",base.LImpr,base.LCurr,UpdCriteria(false),GetRptCriteria()" + Robot.GetCnStr("N", "N") + ",false,false,false);" + Environment.NewLine);
                //sb.Append("				dt = (new " + dw["ProgramName"].ToString() + "System()).Get" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ",base.LImpr,base.LCurr,UpdCriteria(false)" + Robot.GetCnCall("N", "N") + ",false,false,false);" + Environment.NewLine);
                //sb.Append("				}" + Environment.NewLine);
                sb.Append("				CovertRptUTC(dt);" + Environment.NewLine);
                sb.Append("				if (dt.Rows.Count > 0) {if (dt.Columns.Contains(\"ReportName\")) {reportName = dt.Rows[0][\"ReportName\"].ToString();}}" + Environment.NewLine);
                sb.Append("				else {PreMsgPopup(\"For your information, no data is currently available as per your reporting criteria.\");}" + Environment.NewLine);
                sb.Append("			    Response.AppendHeader(\"Content-Disposition\", \"Attachment; Filename=\\\"\" + reportName + \".xls\\\"\");" + Environment.NewLine);
                foreach (DataRowView drv in dvObj)
				{
					sb.Append("				if (dt.Columns.Contains(\"" + drv["ColumnName"].ToString() + "\")) {Response.Write(\"" + drv["ColumnHeader"].ToString() + "\" + (char)9);}" + Environment.NewLine);
				}
				sb.Append("				Response.Write(Environment.NewLine);" + Environment.NewLine);
				sb.Append("				DataView dv = new DataView(dt);" + Environment.NewLine);
				sb.Append("				foreach (DataRowView drv in dv)" + Environment.NewLine);
				sb.Append("				{" + Environment.NewLine);
				foreach (DataRowView drv in dvObj)
				{
					sb.Append("					if (dt.Columns.Contains(\"" + drv["ColumnName"].ToString() + "\")) {Response.Write(drv[\"" + drv["ColumnName"].ToString() + "\"].ToString() + (char)9);}" + Environment.NewLine);
				}
				sb.Append("					Response.Write(Environment.NewLine);" + Environment.NewLine);
				sb.Append("				}" + Environment.NewLine);
				sb.Append("			}" + Environment.NewLine);
				sb.Append("			else if (eExport == exportTo.XML)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
                //sb.Append("				if (Config.Architect == \"W\")" + Environment.NewLine);
                //sb.Append("				{" + Environment.NewLine);
                //sb.Append("					dt = XmlUtils.XmlToDataTable(" + dw["ProgramName"].ToString() + "Facade().Get" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ",XmlUtils.ObjectToXml(base.LImpr),XmlUtils.ObjectToXml(base.LCurr),XmlUtils.DataSetToXml(UpdCriteria(false))" + Robot.GetCnCall("N", "N") + ",false,true,false));" + Environment.NewLine);
                //sb.Append("				}" + Environment.NewLine);
                //sb.Append("				else" + Environment.NewLine);
                //sb.Append("				{" + Environment.NewLine);
                sb.Append("			    dt = (new AdminSystem()).GetRptDt(" + reportId.ToString() + ",\"Get" + dw["ProgramName"].ToString() + "\",base.LImpr,base.LCurr,UpdCriteria(false),GetRptCriteria()" + Robot.GetCnStr("N", "N") + ",false,true,false);" + Environment.NewLine);
                //sb.Append("				dt = (new " + dw["ProgramName"].ToString() + "System()).Get" + dw["ProgramName"].ToString() + "(" + reportId.ToString() + ",base.LImpr,base.LCurr,UpdCriteria(false)" + Robot.GetCnCall("N", "N") + ",false,true,false);" + Environment.NewLine);
                //sb.Append("				}" + Environment.NewLine);
				sb.Append("				reportName = \"" + dw["ProgramName"].ToString() + "\";" + Environment.NewLine);
                sb.Append("				CovertRptUTC(dt);" + Environment.NewLine);
                sb.Append("				if (dt.Rows.Count > 0) {if (dt.Columns.Contains(\"ReportName\")) {reportName = dt.Rows[0][\"ReportName\"].ToString();}}" + Environment.NewLine);
                sb.Append("				else {PreMsgPopup(\"For your information, no data is currently available as per your reporting criteria.\");}" + Environment.NewLine);
                sb.Append("			    if (System.IntPtr.Size == 8)" + Environment.NewLine);
                sb.Append("			    {" + Environment.NewLine);
                sb.Append("			    	StringBuilder sb = new StringBuilder();" + Environment.NewLine);
                sb.Append("					sb.Append(\"<?xml version=\\\"1.0\\\"?>\" + Environment.NewLine);" + Environment.NewLine);
                sb.Append("					sb.Append(\"<Workbook xmlns=\\\"urn:schemas-microsoft-com:office:spreadsheet\\\"\" + Environment.NewLine);" + Environment.NewLine);
                sb.Append("					sb.Append(\"xmlns:o=\\\"urn:schemas-microsoft-com:office:office\\\"\" + Environment.NewLine);" + Environment.NewLine);
                sb.Append("					sb.Append(\"xmlns:x=\\\"urn:schemas-microsoft-com:office:excel\\\"\" + Environment.NewLine);" + Environment.NewLine);
                sb.Append("					sb.Append(\"xmlns:ss=\\\"urn:schemas-microsoft-com:office:spreadsheet\\\"\" + Environment.NewLine);" + Environment.NewLine);
                sb.Append("					sb.Append(\"xmlns:html=\\\"http://www.w3.org/TR/REC-html40\\\">\" + Environment.NewLine);" + Environment.NewLine);
                sb.Append("					DataView dv = new DataView(dt);" + Environment.NewLine);
                sb.Append("					foreach (DataRowView drv in dv)" + Environment.NewLine);
                sb.Append("					{" + Environment.NewLine);
                sb.Append("						sb.Append(drv[\"ReportXml\"].ToString() + Environment.NewLine);" + Environment.NewLine);
                sb.Append("					}" + Environment.NewLine);
                sb.Append("					sb.Append(\"</Workbook>\" + Environment.NewLine);" + Environment.NewLine);
                sb.Append("			    	byte[] content = ConvertXML2XLS(Encoding.UTF8.GetBytes(sb.ToString()));" + Environment.NewLine);
                sb.Append("			    	string fileSig = System.Text.Encoding.UTF8.GetString(content, 0, 5);" + Environment.NewLine);
                sb.Append("			    	bool isXLSX = fileSig.StartsWith(\"PK\");" + Environment.NewLine);
                sb.Append("			    	Response.AppendHeader(\"Content-Disposition\", \"Attachment; Filename=\\\"\" + reportName + \".xls\" + (isXLSX ? \"x\" : \"\") + \"\\\"\");" + Environment.NewLine);
                sb.Append("			    	Response.BinaryWrite(content);" + Environment.NewLine);
                sb.Append("			    }" + Environment.NewLine);
                sb.Append("			    else" + Environment.NewLine);
                sb.Append("			    {" + Environment.NewLine);
                sb.Append("			    	Response.AppendHeader(\"Content-Disposition\", \"Attachment; Filename=\\\"\" + reportName + \".xls\\\"\");" + Environment.NewLine);
                sb.Append("					UnicodeEncoding ue = new UnicodeEncoding();" + Environment.NewLine);
                sb.Append("					Response.BinaryWrite(ue.GetPreamble());" + Environment.NewLine);
                sb.Append("					Response.BinaryWrite(ue.GetBytes(\"<?xml version=\\\"1.0\\\"?>\" + Environment.NewLine));" + Environment.NewLine);
                sb.Append("					Response.BinaryWrite(ue.GetBytes(\"<Workbook xmlns=\\\"urn:schemas-microsoft-com:office:spreadsheet\\\"\" + Environment.NewLine));" + Environment.NewLine);
                sb.Append("					Response.BinaryWrite(ue.GetBytes(\"xmlns:o=\\\"urn:schemas-microsoft-com:office:office\\\"\" + Environment.NewLine));" + Environment.NewLine);
                sb.Append("					Response.BinaryWrite(ue.GetBytes(\"xmlns:x=\\\"urn:schemas-microsoft-com:office:excel\\\"\" + Environment.NewLine));" + Environment.NewLine);
                sb.Append("					Response.BinaryWrite(ue.GetBytes(\"xmlns:ss=\\\"urn:schemas-microsoft-com:office:spreadsheet\\\"\" + Environment.NewLine));" + Environment.NewLine);
                sb.Append("					Response.BinaryWrite(ue.GetBytes(\"xmlns:html=\\\"http://www.w3.org/TR/REC-html40\\\">\" + Environment.NewLine));" + Environment.NewLine);
                sb.Append("					DataView dv = new DataView(dt);" + Environment.NewLine);
                sb.Append("					foreach (DataRowView drv in dv)" + Environment.NewLine);
                sb.Append("					{" + Environment.NewLine);
                sb.Append("						Response.BinaryWrite(ue.GetBytes(drv[\"ReportXml\"].ToString() + Environment.NewLine));" + Environment.NewLine);
                sb.Append("					}" + Environment.NewLine);
                sb.Append("					Response.BinaryWrite(ue.GetBytes(\"</Workbook>\" + Environment.NewLine));" + Environment.NewLine);
                sb.Append("			    }" + Environment.NewLine);
                sb.Append("			}" + Environment.NewLine);
                sb.Append("			Response.End();" + Environment.NewLine);
			}
			sb.Append("		}" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "C")
			{
				sb.Append(Environment.NewLine);
				sb.Append("		private void ExportToStream(object oReport, string sFileName, StringBuilder sb, exportTo eExport)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			System.IO.Stream oStream =  null;" + Environment.NewLine);
				sb.Append("			StreamWriter sw = null;" + Environment.NewLine);
				sb.Append("			ExportOptions oOptions = new ExportOptions();" + Environment.NewLine);
				sb.Append("			ExportRequestContext oRequest = new ExportRequestContext();" + Environment.NewLine);
                sb.Append("			Response.Buffer = true;" + Environment.NewLine);
                sb.Append("			Response.ClearHeaders();" + Environment.NewLine);
                sb.Append("			Response.ClearContent();" + Environment.NewLine);
                sb.Append("			if (eExport == exportTo.TXT)" + Environment.NewLine);
                sb.Append("			{" + Environment.NewLine);
				sb.Append("				oStream = new MemoryStream();" + Environment.NewLine);
				sb.Append("				sw = new StreamWriter(oStream,System.Text.Encoding.Default);" + Environment.NewLine);
				sb.Append("				sw.WriteLine(sb);" + Environment.NewLine);
				sb.Append("				sw.Flush();" + Environment.NewLine);
				sb.Append("				oStream.Seek(0,SeekOrigin.Begin);" + Environment.NewLine);
                sb.Append("				Response.ContentType = \"application/vnd.ms-excel\";" + Environment.NewLine);
                sb.Append("			}" + Environment.NewLine);
                sb.Append("			else if (eExport == exportTo.XLS)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				oOptions.ExportFormatType = ExportFormatType.Excel;" + Environment.NewLine);
				sb.Append("				oOptions.FormatOptions = new ExcelFormatOptions();" + Environment.NewLine);
				sb.Append("				oRequest.ExportInfo = oOptions;" + Environment.NewLine);
                sb.Append("				oStream = ((ReportDocument)oReport).ExportToStream(ExportFormatType.Excel);" + Environment.NewLine);
                sb.Append("				Response.ContentType = \"application/vnd.ms-excel\";" + Environment.NewLine);
                sb.Append("			}" + Environment.NewLine);
				sb.Append("			else if (eExport == exportTo.PDF)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				oOptions.ExportFormatType = ExportFormatType.PortableDocFormat;" + Environment.NewLine);
				sb.Append("				oOptions.FormatOptions = new PdfRtfWordFormatOptions();" + Environment.NewLine);
				sb.Append("				oRequest.ExportInfo = oOptions;" + Environment.NewLine);
                sb.Append("				oStream = ((ReportDocument)oReport).ExportToStream(ExportFormatType.PortableDocFormat);" + Environment.NewLine);
                sb.Append("				Response.ContentType = \"application/pdf\";" + Environment.NewLine);
                sb.Append("			}" + Environment.NewLine);
				sb.Append("			else if (eExport == exportTo.DOC)" + Environment.NewLine);
				sb.Append("			{" + Environment.NewLine);
				sb.Append("				oOptions.ExportFormatType = ExportFormatType.WordForWindows;" + Environment.NewLine);
				sb.Append("				oOptions.FormatOptions = new PdfRtfWordFormatOptions();" + Environment.NewLine);
				sb.Append("				oRequest.ExportInfo = oOptions;" + Environment.NewLine);
                sb.Append("				oStream = ((ReportDocument)oReport).ExportToStream(ExportFormatType.WordForWindows);" + Environment.NewLine);
                sb.Append("				Response.ContentType = \"application/msword\";" + Environment.NewLine);
                sb.Append("			}" + Environment.NewLine);
                sb.Append("			Response.AppendHeader(\"Content-Disposition\", \"Attachment; Filename=\\\"\" + sFileName.Replace(\" \",\"_\") + \"\\\"\");" + Environment.NewLine);
				sb.Append("			byte[] streamByte = new byte[oStream.Length];" + Environment.NewLine);
				sb.Append("			oStream.Read(streamByte, 0, (int)oStream.Length);" + Environment.NewLine);
				sb.Append("			Response.BinaryWrite(streamByte);" + Environment.NewLine);
				sb.Append("			Response.End();" + Environment.NewLine);
				sb.Append("			if (oStream != null) {oStream.Close();}" + Environment.NewLine);
				sb.Append("			if (sw != null) {sw.Close();}" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
			}
			sb.Append(Environment.NewLine);
			sb.Append("		public void cShowCriButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
            sb.Append("			cCriteria.Visible = true; cShowCriButton.Visible = false;" + Environment.NewLine);
            sb.Append("			cClearCriButton.Visible = (bool)Session[KEY_bClCriVisible];" + Environment.NewLine);
            if (dw["ReportTypeCd"].ToString() == "C") { sb.Append("			cViewer.Visible = false;" + Environment.NewLine); }
            sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		public void cClearCriButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			foreach (DataRowView drv in dvCri)
			{
				if ("ComboBox".IndexOf(drv["DisplayName"].ToString()) >= 0)	// Reset to page 1 by reassigning the datasource:
				{
					sb.Append("			if (c" + drv["ColumnName"].ToString() + ".Items.Count > 0) {c" + drv["ColumnName"].ToString() + ".DataSource = c" + drv["ColumnName"].ToString() + ".DataSource; c" + drv["ColumnName"].ToString() + ".SelectByValue(c" + drv["ColumnName"].ToString() + ".Items[0].Value,string.Empty,true);}" + Environment.NewLine);
				}
				else
				{
					if ("DropDownList,ListBox,RadioButtonList".IndexOf(drv["DisplayName"].ToString()) >= 0)
					{
						sb.Append("			if (c" + drv["ColumnName"].ToString() + ".Items.Count > 0) {c" + drv["ColumnName"].ToString() + ".SelectedIndex = 0;}" + Environment.NewLine);
					}
					else
					{
						if ("Calendar".IndexOf(drv["DisplayName"].ToString()) >= 0)
						{
							if (drv["RequiredValid"].ToString() == "N")
							{
								sb.Append("			c" + drv["ColumnName"].ToString() + ".SelectedDates.Clear();" + Environment.NewLine);
							}
							else
							{
								sb.Append("			c" + drv["ColumnName"].ToString() + ".SelectedDate = System.DateTime.Today;" + Environment.NewLine);
							}
						}
						else
						{
							if ("CheckBox".IndexOf(drv["DisplayName"].ToString()) >= 0)
							{
								sb.Append("			c" + drv["ColumnName"].ToString() + ".Checked = false;" + Environment.NewLine);
							}
							else
							{
								if (drv["RequiredValid"].ToString() == "N")
								{
									sb.Append("			c" + drv["ColumnName"].ToString() + ".Text = \"\";" + Environment.NewLine);
								}
								else
								{
									sb.Append("			c" + drv["ColumnName"].ToString() + ".Text = \"0\";" + Environment.NewLine);
								}
							}
						}
					}
				}
			}
			sb.Append("		}" + Environment.NewLine);
			if (dw["ReportTypeCd"].ToString() == "G")
			{
				sb.Append(Environment.NewLine);
				sb.Append("		public void cSearchButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			UpdCriteria(true);" + Environment.NewLine);
				sb.Append("			getReport(false, exportTo.VIEW);" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
			}
			else if (dw["TemplateName"].ToString() == "" || dw["TemplateName"].ToString() == null)
			{
				sb.Append(Environment.NewLine);
				sb.Append("		public void cExpTxtButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			UpdCriteria(true);" + Environment.NewLine);
				sb.Append("			getReport(false, exportTo.TXT);" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
				if (dw["ReportTypeCd"].ToString() == "X")
				{
					sb.Append(Environment.NewLine);
					sb.Append("		public void cExpXmlButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
					sb.Append("		{" + Environment.NewLine);
					sb.Append("			UpdCriteria(true);" + Environment.NewLine);
					sb.Append("			getReport(false, exportTo.XML);" + Environment.NewLine);
					sb.Append("		}" + Environment.NewLine);
				}
				else
				{
					sb.Append(Environment.NewLine);
					sb.Append("		public void cViewButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
					sb.Append("		{" + Environment.NewLine);
					sb.Append("			UpdCriteria(true);" + Environment.NewLine);
					sb.Append("			getReport(false, exportTo.VIEW);" + Environment.NewLine);
					sb.Append("		}" + Environment.NewLine);
					if ("G,C".IndexOf(dw["ReportTypeCd"].ToString()) >= 0)
					{
						sb.Append(Environment.NewLine);
						sb.Append("		public void cExpXlsButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			UpdCriteria(true);" + Environment.NewLine);
						sb.Append("			getReport(false, exportTo.XLS);" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
						sb.Append("		public void cExpPdfButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			UpdCriteria(true);" + Environment.NewLine);
						sb.Append("			getReport(false, exportTo.PDF);" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
						sb.Append("		public void cExpDocButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			UpdCriteria(true);" + Environment.NewLine);
						sb.Append("			getReport(false, exportTo.DOC);" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
						sb.Append("		public void cPrintImage_Click(object sender, System.Web.UI.ImageClickEventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			cPrintButton_Click(sender, new EventArgs());" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
						sb.Append("		public void cPrintButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			UpdCriteria(true);" + Environment.NewLine);
						sb.Append("			getReport(true, exportTo.VIEW);" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
						sb.Append("		private void cViewer_Search(object source, CrystalDecisions.Web.SearchEventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			getReport(false, exportTo.VIEW);" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
						sb.Append("		private void cViewer_ViewZoom(object source, CrystalDecisions.Web.ZoomEventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			getReport(false, exportTo.VIEW);" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
                        //sb.Append("		private void cViewer_PreRender(object sender, System.EventArgs e)" + Environment.NewLine);
                        //sb.Append("		{" + Environment.NewLine);
                        //sb.Append("			getReport(false, exportTo.VIEW);" + Environment.NewLine);
                        //sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
						sb.Append("		private void cViewer_Drill(object source, CrystalDecisions.Web.DrillEventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			getReport(false, exportTo.VIEW);" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
						sb.Append(Environment.NewLine);
						sb.Append("		private void cViewer_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)" + Environment.NewLine);
						sb.Append("		{" + Environment.NewLine);
						sb.Append("			getReport(false, exportTo.VIEW);" + Environment.NewLine);
						sb.Append("		}" + Environment.NewLine);
					}
				}
			}
			else if (dw["TemplateName"].ToString().IndexOf(".rtf") >= 0)
			{
				sb.Append(Environment.NewLine);
				sb.Append("		public void cExpRtfImage_Click(object sender, System.Web.UI.ImageClickEventArgs e)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			cExpRtfButton_Click(sender, new EventArgs());" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
				sb.Append(Environment.NewLine);
				sb.Append("		public void cExpRtfButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			UpdCriteria(true);" + Environment.NewLine);
				sb.Append("			getReport(false, exportTo.RTF);" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
			}
			else if (dw["TemplateName"].ToString().IndexOf(".txt") >= 0)
			{
				sb.Append(Environment.NewLine);
				sb.Append("		public void cPrintImage_Click(object sender, System.Web.UI.ImageClickEventArgs e)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			cPrintButton_Click(sender, new EventArgs());" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
				sb.Append(Environment.NewLine);
				sb.Append("		public void cPrintButton_Click(object sender, System.EventArgs e)" + Environment.NewLine);
				sb.Append("		{" + Environment.NewLine);
				sb.Append("			UpdCriteria(true); getReport(true, exportTo.TXT);" + Environment.NewLine);
				sb.Append("		}" + Environment.NewLine);
			}
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
            //sb.Append("		    cMsgContent.Text = msg;" + Environment.NewLine);
            //sb.Append("		    cMsgImage.ImageUrl = \"~/Images/info.gif\";" + Environment.NewLine);
            //sb.Append("		    cMsgPopup.Show();" + Environment.NewLine);
            //sb.Append("			ScriptManager.GetCurrent(Parent.Page).SetFocus((cMsgCloseButton));" + Environment.NewLine);
            //sb.Append("			string script =" + Environment.NewLine);
            //sb.Append("			@\"<script type='text/javascript' language='javascript'>" + Environment.NewLine);
            //sb.Append("			function MyMsgPopup()" + Environment.NewLine);
            //sb.Append("			{" + Environment.NewLine);
            //sb.Append("				var mpeid = '\" + cMsgPopup.BehaviorID + @\"';" + Environment.NewLine);
            //sb.Append("				var msgid = '\" + cMsg.ClientID + @\"';" + Environment.NewLine);
            //sb.Append("				var imgid = '\" + cMsgClsImage.ClientID + @\"';" + Environment.NewLine);
            //sb.Append("				var simgid = '\" + cMsgImage.ClientID + @\"';" + Environment.NewLine);
            //sb.Append("				var smsgid = '\" + cMsgContent.ClientID + @\"';" + Environment.NewLine);
            //sb.Append("				var closeid = '\" + cMsgCloseButton.ClientID + @\"';" + Environment.NewLine);
            //sb.Append("				var mpe = $find(mpeid);" + Environment.NewLine);
            //sb.Append("				if (mpe != null)" + Environment.NewLine);
            //sb.Append("				{" + Environment.NewLine);
            //sb.Append("					var shown = function ()" + Environment.NewLine);
            //sb.Append("					{" + Environment.NewLine);
            //sb.Append("						var m1 = $get(msgid);" + Environment.NewLine);
            //sb.Append("						var m2 = $get(smsgid);" + Environment.NewLine);
            //sb.Append("						m1.innerHTML = m2.innerHTML;" + Environment.NewLine);
            //sb.Append("						var i1 = $get(imgid);" + Environment.NewLine);
            //sb.Append("						var i2 = $get(simgid);" + Environment.NewLine);
            //sb.Append("						i1.src = i2.src;" + Environment.NewLine);
            //sb.Append("					}" + Environment.NewLine);
            //sb.Append("					mpe.add_shown(shown);" + Environment.NewLine);
            //sb.Append("				}" + Environment.NewLine);
            //sb.Append("			}" + Environment.NewLine);
            //sb.Append("			MyMsgPopup();" + Environment.NewLine);
            //sb.Append("			</script>\";" + Environment.NewLine);
            //sb.Append("			ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), \"Popup\", script, false);" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
            sb.Append("	}" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
			return sb;
		}

        //private StringBuilder MakeSystemCs(DataRow dw, Int32 reportId, DataView dvCri, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("using System;" + Environment.NewLine);
        //    sb.Append("using System.Data;" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ";" + Environment.NewLine);
        //    sb.Append("using " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ".Data;" + Environment.NewLine);
        //    if (CSrc.SrcSystemId != 3)	// Admin.
        //    {
        //        sb.Append("using RO.Common3;" + Environment.NewLine);
        //        sb.Append("using RO.Common3.Data;" + Environment.NewLine);
        //    }
        //    sb.Append(Environment.NewLine);
        //    sb.Append("namespace " + CPrj.EntityCode + ".Facade" + CSrc.SrcSystemId.ToString() + Environment.NewLine);
        //    sb.Append("{" + Environment.NewLine);
        //    sb.Append("	public class " + dw["ProgramName"].ToString() + "System : MarshalByRefObject" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    //foreach (DataRowView drv in dvCri)
        //    //{
        //    //    if ("ComboBox,DropDownList,ListBox,RadioButtonList".IndexOf(drv["DisplayName"].ToString()) >= 0)
        //    //    {
        //    //        sb.Append(Environment.NewLine);
        //    //        sb.Append("		public DataTable GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "(Int32 reportId, UsrImpr ui, UsrCurr uc" + Robot.GetCnDclr("N", "N") + ")" + Environment.NewLine);
        //    //        sb.Append("		{" + Environment.NewLine);
        //    //        sb.Append("			using (Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access dac = new Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access())" + Environment.NewLine);
        //    //        sb.Append("			{" + Environment.NewLine);
        //    //        sb.Append("				return dac.GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "(reportId,ui,uc" + Robot.GetCnParm("N", "N") + ");" + Environment.NewLine);
        //    //        sb.Append("			}" + Environment.NewLine);
        //    //        sb.Append("		}" + Environment.NewLine);
        //    //    }
        //    //}
        //    //sb.Append(Environment.NewLine);
        //    //sb.Append("		public DataTable Get" + dw["ProgramName"].ToString() + "(Int32 reportId, UsrImpr ui, UsrCurr uc, Ds" + dw["ProgramName"].ToString() + "In ds" + Robot.GetCnDclr("N", "N") + ", bool bUpd, bool bXls, bool bVal)" + Environment.NewLine);
        //    //sb.Append("		{" + Environment.NewLine);
        //    //sb.Append("			using (Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access dac = new Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access())" + Environment.NewLine);
        //    //sb.Append("			{" + Environment.NewLine);
        //    //sb.Append("				return dac.Get" + dw["ProgramName"].ToString() + "(reportId,ui,uc,ds" + Robot.GetCnParm("N", "N") + ",bUpd,bXls,bVal);" + Environment.NewLine);
        //    //sb.Append("			}" + Environment.NewLine);
        //    //sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		public bool Upd" + dw["ProgramName"].ToString() + "(Int32 reportId, Int32 usrId, Ds" + dw["ProgramName"].ToString() + "In ds" + Robot.GetCnDclr("N","N") + ")" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			using (Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access dac = new Access" + CSrc.SrcSystemId.ToString() + "." + dw["ProgramName"].ToString() + "Access())" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				return dac.Upd" + dw["ProgramName"].ToString() + "(reportId, usrId, ds" + Robot.GetCnParm("N","N") + ");" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append("}" + Environment.NewLine);
        //    return sb;
        //}

        //private StringBuilder MakeAccessCs(DataRow dw, Int32 reportId, DataView dvCri, CurrPrj CPrj, CurrSrc CSrc)
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
        //    //foreach (DataRowView drv in dvCri)
        //    //{
        //    //    if ("ComboBox,DropDownList,ListBox,RadioButtonList".IndexOf(drv["DisplayName"].ToString()) >= 0)
        //    //    {
        //    //        sb.Append(Environment.NewLine);
        //    //        sb.Append("		public DataTable GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "(Int32 reportId, UsrImpr ui, UsrCurr uc" + Robot.GetCnDclr("N","N") + ")" + Environment.NewLine);
        //    //        sb.Append("		{" + Environment.NewLine);
        //    //        sb.Append("			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}" + Environment.NewLine);
        //    //        sb.Append("			OleDbCommand cmd = new OleDbCommand(\"GetIn" + reportId.ToString() + drv["ColumnName"].ToString() + "\",new OleDbConnection(" + Robot.GetCnPass("N","N") + "));" + Environment.NewLine);
        //    //        sb.Append("			cmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@reportId\", OleDbType.Numeric).Value = reportId;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@RowAuthoritys\", OleDbType.VarChar).Value = ui.RowAuthoritys;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Usrs\", OleDbType.VarChar).Value = ui.Usrs;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@UsrGroups\", OleDbType.VarChar).Value = ui.UsrGroups;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Cultures\", OleDbType.VarChar).Value = ui.Cultures;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Companys\", OleDbType.VarChar).Value = ui.Companys;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Projects\", OleDbType.VarChar).Value = ui.Projects;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Agents\", OleDbType.VarChar).Value = ui.Agents;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Brokers\", OleDbType.VarChar).Value = ui.Brokers;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Customers\", OleDbType.VarChar).Value = ui.Customers;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Investors\", OleDbType.VarChar).Value = ui.Investors;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Members\", OleDbType.VarChar).Value = ui.Members;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@Vendors\", OleDbType.VarChar).Value = ui.Vendors;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@currCompanyId\", OleDbType.Numeric).Value = uc.CompanyId;" + Environment.NewLine);
        //    //        sb.Append("			cmd.Parameters.Add(\"@currProjectId\", OleDbType.Numeric).Value = uc.ProjectId;" + Environment.NewLine);
        //    //        sb.Append("			cmd.CommandTimeout = 1800;" + Environment.NewLine);
        //    //        sb.Append("			da.SelectCommand = cmd;" + Environment.NewLine);
        //    //        sb.Append("			DataTable dt = new DataTable();" + Environment.NewLine);
        //    //        sb.Append("			da.Fill(dt);" + Environment.NewLine);
        //    //        if (drv["RequiredValid"].ToString() == "N")
        //    //        {
        //    //            sb.Append("			dt.Rows.InsertAt(dt.NewRow(),0);" + Environment.NewLine);
        //    //        }
        //    //        sb.Append("			return dt;" + Environment.NewLine);
        //    //        sb.Append("		}" + Environment.NewLine);
        //    //    }
        //    //}
        //    //sb.Append(Environment.NewLine);
        //    //sb.Append("		public DataTable Get" + dw["ProgramName"].ToString() + "(Int32 reportId, UsrImpr ui, UsrCurr uc, Ds" + dw["ProgramName"].ToString() + "In ds" + Robot.GetCnDclr("N", "N") + ", bool bUpd, bool bXls, bool bVal)" + Environment.NewLine);
        //    //sb.Append("		{" + Environment.NewLine);
        //    //sb.Append("			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}" + Environment.NewLine);
        //    //sb.Append("			OleDbCommand cmd = new OleDbCommand(\"Get" + dw["ProgramName"].ToString() + "\",new OleDbConnection(" + Robot.GetCnPass("N","N") + "));" + Environment.NewLine);
        //    //sb.Append("			cmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine);
        //    //sb.Append("			DataRow dr = ds.Tables[\"Dt" + dw["ProgramName"].ToString() + "In\"].Rows[0];" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@reportId\", OleDbType.Numeric).Value = reportId;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@RowAuthoritys\", OleDbType.VarChar).Value = ui.RowAuthoritys;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Usrs\", OleDbType.VarChar).Value = ui.Usrs;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@UsrGroups\", OleDbType.VarChar).Value = ui.UsrGroups;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Cultures\", OleDbType.VarChar).Value = ui.Cultures;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Companys\", OleDbType.VarChar).Value = ui.Companys;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Projects\", OleDbType.VarChar).Value = ui.Projects;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Agents\", OleDbType.VarChar).Value = ui.Agents;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Brokers\", OleDbType.VarChar).Value = ui.Brokers;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Customers\", OleDbType.VarChar).Value = ui.Customers;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Investors\", OleDbType.VarChar).Value = ui.Investors;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Members\", OleDbType.VarChar).Value = ui.Members;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@Vendors\", OleDbType.VarChar).Value = ui.Vendors;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@currCompanyId\", OleDbType.Numeric).Value = uc.CompanyId;" + Environment.NewLine);
        //    //sb.Append("			cmd.Parameters.Add(\"@currProjectId\", OleDbType.Numeric).Value = uc.ProjectId;" + Environment.NewLine);
        //    //foreach (DataRowView drv in dvCri)
        //    //{
        //    //    if (drv["RequiredValid"].ToString() == "N")
        //    //    {
        //    //        sb.Append("			if (dr[\"" + drv["ColumnName"].ToString() + "\"].ToString().Trim() == string.Empty)" + Environment.NewLine);
        //    //        sb.Append("			{" + Environment.NewLine);
        //    //        sb.Append("				cmd.Parameters.Add(\"@" + drv["ColumnName"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = System.DBNull.Value;" + Environment.NewLine);
        //    //        sb.Append("			}" + Environment.NewLine);
        //    //        sb.Append("			else" + Environment.NewLine);
        //    //        sb.Append("			{" + Environment.NewLine);
        //    //        sb.Append("	");
        //    //    }
        //    //    if (drv["DataTypeSByteOle"].ToString() == drv["DataTypeDByteOle"].ToString())
        //    //    {
        //    //        sb.Append("			cmd.Parameters.Add(\"@" + drv["ColumnName"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = dr[\"" + drv["ColumnName"].ToString() + "\"];" + Environment.NewLine);
        //    //    }
        //    //    else
        //    //    {
        //    //        sb.Append("			if (Config.DoubleByteDb) {cmd.Parameters.Add(\"@" + drv["ColumnName"].ToString() + "\", OleDbType." + drv["DataTypeDByteOle"].ToString() + ").Value = dr[\"" + drv["ColumnName"].ToString() + "\"];} else {cmd.Parameters.Add(\"@" + drv["ColumnName"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = dr[\"" + drv["ColumnName"].ToString() + "\"];}" + Environment.NewLine);
        //    //    }
        //    //    if (drv["RequiredValid"].ToString() == "N") {sb.Append("			}" + Environment.NewLine);}
        //    //}
        //    //sb.Append("			if (bUpd) {cmd.Parameters.Add(\"@bUpd\", OleDbType.Char).Value = \"Y\";} else {cmd.Parameters.Add(\"@bUpd\", OleDbType.Char).Value = \"N\";}" + Environment.NewLine);
        //    //sb.Append("			if (bXls) {cmd.Parameters.Add(\"@bXls\", OleDbType.Char).Value = \"Y\";} else {cmd.Parameters.Add(\"@bXls\", OleDbType.Char).Value = \"N\";}" + Environment.NewLine);
        //    //sb.Append("			if (bVal) {cmd.Parameters.Add(\"@bVal\", OleDbType.Char).Value = \"Y\";} else {cmd.Parameters.Add(\"@bVal\", OleDbType.Char).Value = \"N\";}" + Environment.NewLine);
        //    //sb.Append("			da.SelectCommand = cmd;" + Environment.NewLine);
        //    //sb.Append("			cmd.CommandTimeout = 1800;" + Environment.NewLine);
        //    //sb.Append("			DataTable dt = new DataTable();" + Environment.NewLine);
        //    //sb.Append("			da.Fill(dt);" + Environment.NewLine);
        //    //sb.Append("			return dt;" + Environment.NewLine);
        //    //sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		public bool Upd" + dw["ProgramName"].ToString() + "(Int32 reportId, Int32 usrId, Ds" + dw["ProgramName"].ToString() + "In ds" + Robot.GetCnDclr("N","N") + ")" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}" + Environment.NewLine);
        //    sb.Append("			OleDbConnection cn =  new OleDbConnection(" + Robot.GetCnPass("N","N") + ");" + Environment.NewLine);
        //    sb.Append("			cn.Open();" + Environment.NewLine);
        //    sb.Append("			OleDbTransaction tr = cn.BeginTransaction();" + Environment.NewLine);
        //    sb.Append("			OleDbCommand cmd = new OleDbCommand(\"Upd" + dw["ProgramName"].ToString() + "\", cn);" + Environment.NewLine);
        //    sb.Append("			cmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine);
        //    sb.Append("			cmd.CommandTimeout = 1800;" + Environment.NewLine);
        //    sb.Append("			da.UpdateCommand = cmd;" + Environment.NewLine);
        //    sb.Append("			da.UpdateCommand.Transaction = tr;" + Environment.NewLine);
        //    sb.Append("			DataRow dr = ds.Tables[\"Dt" + dw["ProgramName"].ToString() + "In\"].Rows[0];" + Environment.NewLine);
        //    sb.Append("			cmd.Parameters.Add(\"@reportId\", OleDbType.Numeric).Value = reportId;" + Environment.NewLine);
        //    sb.Append("			cmd.Parameters.Add(\"@usrId\", OleDbType.Numeric).Value = usrId;" + Environment.NewLine);
        //    foreach (DataRowView drv in dvCri)
        //    {
        //        if ("Button,ImageButton".IndexOf(drv["DisplayName"].ToString()) < 0)
        //        {
        //            if (drv["RequiredValid"].ToString() == "N")
        //            {
        //                sb.Append("			if (dr[\"" + drv["ColumnName"].ToString() + "\"].ToString() == string.Empty)" + Environment.NewLine);
        //                sb.Append("			{" + Environment.NewLine);
        //                sb.Append("				cmd.Parameters.Add(\"@" + drv["ColumnName"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = System.DBNull.Value;" + Environment.NewLine);
        //                sb.Append("			}" + Environment.NewLine);
        //                sb.Append("			else" + Environment.NewLine);
        //                sb.Append("			{" + Environment.NewLine);
        //                sb.Append("	");
        //            }
        //            if (drv["DataTypeSByteOle"].ToString() == drv["DataTypeDByteOle"].ToString())
        //            {
        //                sb.Append("			cmd.Parameters.Add(\"@" + drv["ColumnName"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = dr[\"" + drv["ColumnName"].ToString() + "\"];" + Environment.NewLine);
        //            }
        //            else
        //            {
        //                sb.Append("			if (Config.DoubleByteDb) {cmd.Parameters.Add(\"@" + drv["ColumnName"].ToString() + "\", OleDbType." + drv["DataTypeDByteOle"].ToString() + ").Value = dr[\"" + drv["ColumnName"].ToString() + "\"];} else {cmd.Parameters.Add(\"@" + drv["ColumnName"].ToString() + "\", OleDbType." + drv["DataTypeSByteOle"].ToString() + ").Value = dr[\"" + drv["ColumnName"].ToString() + "\"];}" + Environment.NewLine);
        //            }
        //            if (drv["RequiredValid"].ToString() == "N") {sb.Append("			}" + Environment.NewLine);}
        //        }
        //    }
        //    sb.Append("			try" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				da.UpdateCommand.ExecuteNonQuery();" + Environment.NewLine);
        //    sb.Append("				tr.Commit();" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			catch(Exception e)" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				tr.Rollback();" + Environment.NewLine);
        //    sb.Append("				ApplicationAssert.CheckCondition(false, \"\", \"\", e.Message);" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			finally" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				cn.Close();" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			if ( ds.HasErrors )" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				ds.Tables[\"Dt" + dw["ProgramName"].ToString() + "In\"].GetErrors()[0].ClearErrors();" + Environment.NewLine);
        //    sb.Append("				return false;" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("			else" + Environment.NewLine);
        //    sb.Append("			{" + Environment.NewLine);
        //    sb.Append("				ds.AcceptChanges();" + Environment.NewLine);
        //    sb.Append("				return true;" + Environment.NewLine);
        //    sb.Append("			}" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append("}" + Environment.NewLine);
        //    return sb;
        //}

        //private StringBuilder MakeDataCs(DataRow dw, DataView dvObj, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("using System;" + Environment.NewLine);
        //    sb.Append("using System.Data;" + Environment.NewLine);
        //    sb.Append("using System.Runtime.Serialization;" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("namespace " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ".Data" + Environment.NewLine);
        //    sb.Append("{" + Environment.NewLine);
        //    sb.Append("	[SerializableAttribute]" + Environment.NewLine);
        //    sb.Append("	public class Ds" + dw["ProgramName"].ToString() + " : DataSet" + Environment.NewLine);
        //    sb.Append("	{" + Environment.NewLine);
        //    sb.Append("		public Ds" + dw["ProgramName"].ToString() + "(SerializationInfo info, StreamingContext context) : base(info, context)" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		public Ds" + dw["ProgramName"].ToString() + "()" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			this.Tables.Add(MakeColumns(new DataTable(\"Dt" + dw["ProgramName"].ToString() + "\")));" + Environment.NewLine);
        //    sb.Append("			this.DataSetName = \"Ds" + dw["ProgramName"].ToString() + "\";" + Environment.NewLine);
        //    sb.Append("			this.Namespace = \"http://Rintagi.com/DataSet/Ds" + dw["ProgramName"].ToString() + "\";" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("		private DataTable MakeColumns(DataTable dt)" + Environment.NewLine);
        //    sb.Append("		{" + Environment.NewLine);
        //    sb.Append("			DataColumnCollection columns = dt.Columns;" + Environment.NewLine);
        //    sb.Append("			columns.Add(\"AuthorityLevel\", typeof(String));" + Environment.NewLine);
        //    foreach (DataRowView drv in dvObj)
        //    {
        //        if (drv["OperatorId"].ToString() == string.Empty)
        //        {
        //            sb.Append("			columns.Add(\"" + drv["ColumnName"].ToString() + "\", typeof(" + drv["DataTypeSysName"].ToString() + "));" + Environment.NewLine);
        //        }
        //    }
        //    sb.Append("			return dt;" + Environment.NewLine);
        //    sb.Append("		}" + Environment.NewLine);
        //    sb.Append("	}" + Environment.NewLine);
        //    sb.Append("}" + Environment.NewLine);
        //    return sb;
        //}

        private void MakeDataInCs(StringBuilder sb, DataRow dw, DataView dvCri, CurrPrj CPrj, CurrSrc CSrc)
		{
			sb.Append(Environment.NewLine);
			sb.Append("namespace " + CPrj.EntityCode + ".Common" + CSrc.SrcSystemId.ToString() + ".Data" + Environment.NewLine);
			sb.Append("{" + Environment.NewLine);
			//sb.Append("	[SerializableAttribute]" + Environment.NewLine);
			sb.Append("	public class Ds" + dw["ProgramName"].ToString() + "In : DataSet" + Environment.NewLine);
            sb.Append("	{" + Environment.NewLine);
            //sb.Append("		public Ds" + dw["ProgramName"].ToString() + "In(SerializationInfo info, StreamingContext context) : base(info, context)" + Environment.NewLine);
            //sb.Append("		{" + Environment.NewLine);
            //sb.Append("		}" + Environment.NewLine);
			//sb.Append(Environment.NewLine);
			sb.Append("		public Ds" + dw["ProgramName"].ToString() + "In()" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			this.Tables.Add(MakeColumns(new DataTable(\"Dt" + dw["ProgramName"].ToString() + "In\")));" + Environment.NewLine);
			sb.Append("			this.DataSetName = \"Ds" + dw["ProgramName"].ToString() + "In\";" + Environment.NewLine);
			sb.Append("			this.Namespace = \"http://Rintagi.com/DataSet/Ds" + dw["ProgramName"].ToString() + "In\";" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("		private DataTable MakeColumns(DataTable dt)" + Environment.NewLine);
			sb.Append("		{" + Environment.NewLine);
			sb.Append("			DataColumnCollection columns = dt.Columns;" + Environment.NewLine);
			foreach (DataRowView drv in dvCri)
			{
				sb.Append("			columns.Add(\"" + drv["ColumnName"].ToString() + "\", typeof(" + drv["DataTypeSysName"].ToString() + "));" + Environment.NewLine);
			}
			sb.Append("			return dt;" + Environment.NewLine);
			sb.Append("		}" + Environment.NewLine);
			sb.Append("	}" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
		}

		private void MakeReportGrp(ref StringBuilder sb, int ii, DataView dvCri, DataView dvGrp, string sIndent, string clientFrwork)
		{
            string SsIndent = string.Empty;
			bool bContentVertical = false;
			if (dvGrp[ii]["ContentVertical"].ToString() == "Y") {bContentVertical = true;}
			bool bLabelVertical = false;
			if (dvGrp[ii]["LabelVertical"].ToString() == "Y") {bLabelVertical = true;}
			string sGrpId = dvGrp[ii]["ReportGrpId"].ToString();
			string sBorderWidth = dvGrp[ii]["BorderWidth"].ToString();
			string sGrpStyle = dvGrp[ii]["GrpStyle"].ToString();
			bool bLeaf = true;
			if (ii + 1 < dvGrp.Count && dvGrp[ii + 1]["ParentGrpId"].ToString() == sGrpId) {bLeaf = false;}
			if (dvGrp[ii]["ParentGrpId"].ToString() == null || dvGrp[ii]["ParentGrpId"].ToString() == string.Empty)
			{
                sb.Append(sIndent + "<asp:Panel id=\"cCriteria\" runat=\"server\" wrap=\"false\" BorderWidth=\"" + sBorderWidth + "px\" style=\"min-height:440px;\">" + Environment.NewLine);
                sb.Append(sIndent + "<fieldset class=\"criteria-grp\" style=\"padding:10px;\"><legend>CRITERIA<span><asp:Button id=\"cClearCriButton\" onclick=\"cClearCriButton_Click\" runat=\"server\" CausesValidation=\"false\" /></span></legend>" + Environment.NewLine);
                SsIndent = sIndent;     // To close fieldset only once.
            }
			else
			{
				sb.Append(sIndent + "<asp:Panel id=\"cGrp" + sGrpId + "\" runat=\"server\" BorderWidth=\"" + sBorderWidth + "px\"");
                if (sGrpStyle != string.Empty)
                {
                    if (sGrpStyle.StartsWith("."))
                    {
                        sb.Append(" CssClass=\"" + sGrpStyle.Substring(1) + "\"");
                    }
                    else
                    {
                        sb.Append(" CssClass=\"group-panel\" style=\"" + sGrpStyle + "\"");
                    }
                }
                else { sb.Append(" CssClass=\"group-panel\""); }
                sb.Append(">" + Environment.NewLine);
            }
            sb.Append(sIndent + "<asp:Table cellspacing=\"0\" cellpadding=\"0\" runat=\"server\">" + Environment.NewLine);
			dvCri.RowFilter = "ReportGrpId = " + sGrpId;
			if (dvCri.Count > 0)
			{
				if (bLabelVertical)
				{
					if (bContentVertical)
					{
						foreach (DataRowView drv in dvCri)
						{
							sb.Append(sIndent + "<asp:TableRow VerticalAlign=\"top\">" + Environment.NewLine);
							MakeReportGrpLab(ref sb, drv, sIndent + (char)9);
							sb.Append(sIndent + "</asp:TableRow>" + Environment.NewLine);
							sb.Append(sIndent + "<asp:TableRow VerticalAlign=\"top\">" + Environment.NewLine);
							MakeReportGrpInp(ref sb, drv, sIndent + (char)9, clientFrwork);
							sb.Append(sIndent + "</asp:TableRow>" + Environment.NewLine);
						}
					}
					else
					{
						sb.Append(sIndent + "<asp:TableRow VerticalAlign=\"top\">" + Environment.NewLine);
						foreach (DataRowView drv in dvCri)
						{
							MakeReportGrpLab(ref sb, drv, sIndent + (char)9);
						}
						sb.Append(sIndent + "</asp:TableRow>" + Environment.NewLine);
						sb.Append(sIndent + "<asp:TableRow VerticalAlign=\"top\">" + Environment.NewLine);
						foreach (DataRowView drv in dvCri)
						{
							MakeReportGrpInp(ref sb, drv, sIndent + (char)9, clientFrwork);
						}
						sb.Append(sIndent + "</asp:TableRow>" + Environment.NewLine);
					}
				}
				else
				{
					if (bContentVertical)
					{
						foreach (DataRowView drv in dvCri)
						{
							sb.Append(sIndent + "<asp:TableRow VerticalAlign=\"top\">" + Environment.NewLine);
							MakeReportGrpLab(ref sb, drv, sIndent + (char)9);
							MakeReportGrpInp(ref sb, drv, sIndent + (char)9, clientFrwork);
							sb.Append(sIndent + "</asp:TableRow>" + Environment.NewLine);
						}
					}
					else
					{
						sb.Append(sIndent + "<asp:TableRow VerticalAlign=\"top\">" + Environment.NewLine);
						foreach (DataRowView drv in dvCri)
						{
							MakeReportGrpLab(ref sb, drv, sIndent + (char)9);
							MakeReportGrpInp(ref sb, drv, sIndent + (char)9, clientFrwork);
						}
						sb.Append(sIndent + "</asp:TableRow>" + Environment.NewLine);
					}
				}
			}
			if (!bContentVertical && !bLeaf) {sb.Append(sIndent + "<asp:TableRow VerticalAlign=\"top\">" + Environment.NewLine);}
			while (ii + 1 < dvGrp.Count)
			{
				ii = ii + 1;
				if (dvGrp[ii]["ParentGrpId"].ToString() == sGrpId)
				{
					if (!bContentVertical) {sb.Append(sIndent + "<asp:TableCell>" + Environment.NewLine);}
					else {sb.Append(sIndent + "<asp:TableRow VerticalAlign=\"top\"><asp:TableCell>" + Environment.NewLine);}
					MakeReportGrp(ref sb, ii, dvCri, dvGrp, sIndent + (char)9, clientFrwork);
					if (!bContentVertical) {sb.Append(sIndent + "</asp:TableCell>" + Environment.NewLine);}
					else {sb.Append(sIndent + "</asp:TableCell></asp:TableRow>" + Environment.NewLine);}
				}
			}
			if (!bContentVertical && !bLeaf) {sb.Append(sIndent + "</asp:TableRow>" + Environment.NewLine);}
			sb.Append(sIndent + "</asp:Table>" + Environment.NewLine);
            if (sIndent == SsIndent) { sb.Append(sIndent + "</fieldset>" + Environment.NewLine); }
            sb.Append(sIndent + "</asp:Panel>" + Environment.NewLine);
			dvCri.RowFilter = "";
		}

		private void MakeReportGrpLab(ref StringBuilder sb, DataRowView drv, string sIndent)
		{
			sb.Append(sIndent + "<asp:TableCell id=\"c" + drv["ColumnName"].ToString() + "P1\"");
			string sLabelCss = drv["LabelCss"].ToString();
			if (sLabelCss != string.Empty)
			{
				if (sLabelCss.StartsWith(".")) { sb.Append(" CssClass=\"" + sLabelCss.Substring(1) + "\""); } else { sb.Append(" CssClass=\"GrpLabel\" style=\"" + sLabelCss + "\""); }
			}
			else { sb.Append(" CssClass=\"GrpContent\""); }
            sb.Append(" runat=\"server\"><div><asp:Label id=\"c" + drv["ColumnName"].ToString() + "Label\" CssClass=\"inp-lbl\" runat=\"server\" /></div></asp:TableCell>" + Environment.NewLine);
		}

		private void MakeReportGrpInp(ref StringBuilder sb, DataRowView drv, string sIndent, string clientFrwork)
		{
			sb.Append(sIndent + "<asp:TableCell");
            string sContentCss = drv["ContentCss"].ToString();
            if (sContentCss != string.Empty)
            {
                if (sContentCss.StartsWith(".")) { sb.Append(" CssClass=\"" + sContentCss.Substring(1) + "\""); } else { sb.Append(" CssClass=\"GrpContent\" style=\"" + sContentCss + "\""); }
            }
            else { sb.Append(" CssClass=\"GrpContent\""); }
            sb.Append(">");
            if (drv["DisplayName"].ToString().ToLower() == "radiobuttonlist") { sb.Append("<div class=\"PickList\">"); }
            if (drv["DisplayName"].ToString() == "ComboBox") { sb.Append("<rcasp:"); } else { sb.Append("<asp:"); }
            sb.Append(drv["DisplayName"].ToString());
            sb.Append(" id=\"c" + drv["ColumnName"].ToString() + "\"");
            if (drv["DisplayName"].ToString().ToLower() != "radiobuttonlist")
            {
                sb.Append(" CssClass=");
                if ("ListBox".IndexOf(drv["DisplayName"].ToString()) >= 0) { sb.Append("\"inp-pic\""); }
                else if ("ComboBox,DropDownList".IndexOf(drv["DisplayName"].ToString()) >= 0) { sb.Append("\"inp-ddl\""); }
                else if ("Calendar".IndexOf(drv["DisplayName"].ToString()) >= 0) { sb.Append("\"inp-txt calendar\""); }
                else { sb.Append("\"inp-txt\""); }
            }
			if ("Password,MultiLine".IndexOf(drv["DisplayMode"].ToString()) >= 0)
			{
				sb.Append(" TextMode=\"" + drv["DisplayMode"].ToString() + "\"");
			}
			if ("ListBox".IndexOf(drv["DisplayName"].ToString()) >= 0)
			{
				sb.Append(" SelectionMode=\"Multiple\"");
				if (drv["RowSize"].ToString() != string.Empty)
				{
					sb.Append(" height=\"" + (int.Parse(drv["RowSize"].ToString()) * 16 + 7).ToString() + "\"");
                    sb.Append(" Rows=\"" + (int.Parse(drv["RowSize"].ToString())).ToString() + "\"");
                }
			}
			if (",ComboBox,DropDownList,ListBox,RadioButtonList,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
			{
				sb.Append(" DataValueField=\"" + drv["DdlKeyColumnName"].ToString() + "\" DataTextField=\"" + drv["DdlRefColumnName"].ToString() + "\"");
				if (drv["DisplayName"].ToString() == "RadioButtonList") { sb.Append("  RepeatDirection=\"Horizontal\""); }
				if (drv["DisplayName"].ToString() == "ListBox") { sb.Append(" AutoPostBack=\"false\""); } else { sb.Append(" AutoPostBack=\"true\""); }
				sb.Append(" OnSelectedIndexChanged=\"c" + drv["ColumnName"].ToString() + "_SelectedIndexChanged\"");
			}
			else if (",CheckBox,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
			{
				sb.Append(" AutoPostBack=\"true\" OnCheckedChanged=\"c" + drv["ColumnName"].ToString() + "_CheckedChanged\"");
			}
			else if (",Calendar,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
			{
				sb.Append(" OnSelectionChanged=\"c" + drv["ColumnName"].ToString() + "_SelectionChanged\"");
			}
			else if (",Button,ImageButton,".IndexOf("," + drv["DisplayName"].ToString() + ",") >= 0)
			{
				// AutoPostBack not available for Button and ImageButton:
				sb.Append(" OnClick=\"c" + drv["ColumnName"].ToString() + "_Click\"");
			}
			else
			{
				sb.Append(" AutoPostBack=\"true\" OnTextChanged=\"c" + drv["ColumnName"].ToString() + "_TextChanged\"");
			}
            if (drv["DisplayMode"].ToString() == "AutoListBox")
            {
                sb.Append(" searchable ");
            }
            sb.Append(" runat=\"server\" />");
            if (drv["DisplayMode"].ToString() == "AutoListBox") { sb.Append("<asp:TextBox ID=\"c" + drv["ColumnName"].ToString() + "Hidden\" runat=\"server\" style=\"display:none\" />"); };
            //if (drv["DisplayMode"].ToString().IndexOf("Date") >= 0)
            //{
            //    sb.Append("<ajwc:CalendarExtender TargetControlID=\"c" + drv["ColumnName"].ToString() + "\"");
            //    if (drv["DisplayMode"].ToString() == "LongDate") { sb.Append(" Format=\"D\""); }
            //    else if (drv["DisplayMode"].ToString() == "ShortDate") { sb.Append(" Format=\"d\""); }
            //    else if (drv["DisplayMode"].ToString() == "LongDateTime") { sb.Append(" Format=\"F\""); }
            //    else if (drv["DisplayMode"].ToString() == "ShortDateTime") { sb.Append(" Format=\"f\""); }
            //    else if (drv["DisplayMode"].ToString() == "Date") { sb.Append(" Format=\"d-MMM-yyyy\""); }
            //    else if (drv["DisplayMode"].ToString() == "DateTime") { sb.Append(" Format=\"d-MMM-yyyy hh:mm\""); }
            //    sb.Append(" runat=\"server\" />");
            //}
            if (drv["DisplayName"].ToString().ToLower() == "radiobuttonlist") { sb.Append("</div>"); }
            sb.Append("</asp:TableCell>" + Environment.NewLine);
		}
	}
}