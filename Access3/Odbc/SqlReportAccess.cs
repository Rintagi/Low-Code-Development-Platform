using System;
using System.Text;
using System.Data;
//using System.Data.OleDb;
using System.Data.Odbc;
using System.Linq;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;

namespace RO.Access3.Odbc
{
	public class SqlReportAccess : SqlReportAccessBase, IDisposable
	{
		private OdbcDataAdapter da;
        private int _CommandTimeOut = 1800;

        private static OdbcCommand TransformCmd(OdbcCommand cmd)
        {
            if (cmd.Parameters != null
                && cmd.Parameters.Count > 0
                && cmd.CommandType == CommandType.StoredProcedure
                && !string.IsNullOrEmpty(cmd.CommandText)
                && !cmd.CommandText.StartsWith("{CALL")
                )
            {
                cmd.CommandText = string.Format("{{CALL {0}({1})}}"
                    , cmd.CommandText
                    , string.Join(",", Enumerable.Repeat("?", cmd.Parameters.Count).ToArray())
                    );
            }
            return cmd;
        }

		public SqlReportAccess(int CommandTimeOut = 1800)
		{
			da = new OdbcDataAdapter();
            _CommandTimeOut = CommandTimeOut;
		}

		public override void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true); // as a service to those who might inherit from us
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
				return;

			if (da != null)
			{
				if(da.SelectCommand != null)
				{
					if( da.SelectCommand.Connection != null)
					{
						da.SelectCommand.Connection.Dispose();
					}
					da.SelectCommand.Dispose();
				}
				da.Dispose();
				da = null;
			}
		}

		public override DataTable GetDocImage(string ReportId, Int16 TemplateId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetDocImage", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = ReportId;
			cmd.Parameters.Add("@TemplateId", OdbcType.Numeric).Value = TemplateId;
			da.SelectCommand = TransformCmd(cmd);
			cmd.CommandTimeout = _CommandTimeOut;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetGaugeValue(string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetGaugeValue", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			cmd.CommandTimeout = _CommandTimeOut;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetSqlReport(string reportId, string programName, DataView dvCri, UsrImpr ui, UsrCurr uc, DataSet ds, string dbConnectionString, string dbPassword, bool bUpd, bool bXls, bool bVal)
		{
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
            DataRow dr = ds != null ? ds.Tables["DtSqlReportIn"].Rows[0] : null;
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            try {
                /* create the temp table that can be used between SPs */
                OdbcCommand setupCmd = new OdbcCommand("SET NOCOUNT ON CREATE TABLE #ReportTemp (Name varchar(100), Val nvarchar(max))", cn);

                setupCmd.ExecuteNonQuery();
                if (dr != null && ds.Tables[0].Columns.Contains("tzInfo"))
                {
                    setupCmd.CommandText = string.Format("INSERT INTO #ReportTemp VALUES ('{0}','{1}')", "TZInfo", dr["tzInfo"].ToString().Replace("'", "''"))
                                            + (dr.Table.Columns.Contains("tzUtcOffset") ? string.Format(" INSERT INTO #ReportTemp VALUES ('{0}','{1}')", "TZUtcOffset", dr["tzUtcOffset"].ToString().Replace("'", "''")) : "")
                                            ;
                    setupCmd.ExecuteNonQuery();
                }
                setupCmd.Dispose();

			    OdbcCommand cmd = new OdbcCommand("Get" + programName, cn);
			    cmd.CommandType = CommandType.StoredProcedure;
			    cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			    cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			    cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = ui.Usrs;
			    cmd.Parameters.Add("@UsrGroups", OdbcType.VarChar).Value = ui.UsrGroups;
			    cmd.Parameters.Add("@Cultures", OdbcType.VarChar).Value = ui.Cultures;
			    cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
			    cmd.Parameters.Add("@Projects", OdbcType.VarChar).Value = ui.Projects;
			    cmd.Parameters.Add("@Agents", OdbcType.VarChar).Value = ui.Agents;
			    cmd.Parameters.Add("@Brokers", OdbcType.VarChar).Value = ui.Brokers;
			    cmd.Parameters.Add("@Customers", OdbcType.VarChar).Value = ui.Customers;
			    cmd.Parameters.Add("@Investors", OdbcType.VarChar).Value = ui.Investors;
			    cmd.Parameters.Add("@Members", OdbcType.VarChar).Value = ui.Members;
			    cmd.Parameters.Add("@Vendors", OdbcType.VarChar).Value = ui.Vendors;
                cmd.Parameters.Add("@Borrowers", OdbcType.VarChar).Value = ui.Borrowers;
                cmd.Parameters.Add("@Guarantors", OdbcType.VarChar).Value = ui.Guarantors;
                cmd.Parameters.Add("@Lenders", OdbcType.VarChar).Value = ui.Lenders;
                cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			    cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
			    foreach (DataRowView drv in dvCri)
			    {
				    if (drv["RequiredValid"].ToString() == "N" && dr[drv["ColumnName"].ToString()].ToString().Trim() == string.Empty)
				    {
					    if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = System.DBNull.Value; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Real).Value = System.DBNull.Value; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Double).Value = System.DBNull.Value; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = System.DBNull.Value; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Binary).Value = System.DBNull.Value; }
					    else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarBinary).Value = System.DBNull.Value; }
                        else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.DateTime).Value = System.DBNull.Value; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Decimal).Value = System.DBNull.Value; }
					    else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Date).Value = System.DBNull.Value; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Char).Value = System.DBNull.Value; }
					    else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarChar).Value = System.DBNull.Value; }
				    }
				    else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "WChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.NChar).Value = dr[drv["ColumnName"].ToString()]; }
				    else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "VarWChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.NVarChar).Value = dr[drv["ColumnName"].ToString()]; }
				    else
				    {
					    if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Real).Value = dr[drv["ColumnName"].ToString()]; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Double).Value = dr[drv["ColumnName"].ToString()]; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Binary).Value = dr[drv["ColumnName"].ToString()]; }
					    else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarBinary).Value = dr[drv["ColumnName"].ToString()]; }
                        else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.DateTime).Value = dr[drv["ColumnName"].ToString()]; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Decimal).Value = dr[drv["ColumnName"].ToString()]; }
					    else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Date).Value = dr[drv["ColumnName"].ToString()]; }
					    else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Char).Value = dr[drv["ColumnName"].ToString()]; }
					    else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarChar).Value = dr[drv["ColumnName"].ToString()]; }
				    }
			    }
			    if (bUpd) { cmd.Parameters.Add("@bUpd", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bUpd", OdbcType.Char).Value = "N"; }
			    if (bXls) {cmd.Parameters.Add("@bXls", OdbcType.Char).Value = "Y";} else {cmd.Parameters.Add("@bXls", OdbcType.Char).Value = "N";}
			    if (bVal) {cmd.Parameters.Add("@bVal", OdbcType.Char).Value = "Y";} else {cmd.Parameters.Add("@bVal", OdbcType.Char).Value = "N";}
			    da.SelectCommand = TransformCmd(cmd);
			    cmd.CommandTimeout = _CommandTimeOut;
			    DataTable dt = new DataTable();
			    da.Fill(dt);
			    return dt;
            } 
            finally
            {
                cn.Close();
            }
		}

		public override void UpdSqlReport(string reportId, string programName, DataView dvCri, Int32 usrId, DataSet ds, string dbConnectionString, string dbPassword)
		{
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("Upd" + programName, cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = _CommandTimeOut;
			cmd.Transaction = tr;
			DataRow dr = ds.Tables["DtSqlReportIn"].Rows[0];
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@usrId", OdbcType.Numeric).Value = usrId;
			foreach (DataRowView drv in dvCri)
			{
				if (drv["RequiredValid"].ToString() == "N" && dr[drv["ColumnName"].ToString()].ToString().Trim() == string.Empty)
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Real).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Double).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Binary).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarBinary).Value = System.DBNull.Value; }
                    else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.DateTime).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Decimal).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Date).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Char).Value = System.DBNull.Value; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarChar).Value = System.DBNull.Value; }
				}
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "WChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.NChar).Value = dr[drv["ColumnName"].ToString()]; }
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "VarWChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.NVarChar).Value = dr[drv["ColumnName"].ToString()]; }
				else
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Real).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Double).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Binary).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarBinary).Value = dr[drv["ColumnName"].ToString()]; }
                    else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.DateTime).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Decimal).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Date).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Char).Value = dr[drv["ColumnName"].ToString()]; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarChar).Value = dr[drv["ColumnName"].ToString()]; }
				}
			}
			try
			{
				da.UpdateCommand = cmd;
				TransformCmd(cmd).ExecuteNonQuery();
				tr.Commit();
			}
			catch(Exception e)
			{
				tr.Rollback();
				ApplicationAssert.CheckCondition(false, "", "", e.Message);
			}
			finally
			{
				cn.Close();
			}
			if ( ds.HasErrors )
			{
				ds.Tables["DtSqlReportIn"].GetErrors()[0].ClearErrors();
			}
			else
			{
				ds.AcceptChanges();
			}
			return;
		}

		public override string MemSqlReport(string PublicAccess, string RptMemCriId, string RptMemFldId, string RptMemCriName, string RptMemCriDesc, string RptMemCriLink, string reportId, string programName, DataView dvCri, Int32 usrId, DataSet ds, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("Mem" + programName, cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = _CommandTimeOut;
			cmd.Transaction = tr;
			DataRow dr = ds.Tables["DtSqlReportIn"].Rows[0];
			cmd.Parameters.Add("@PublicAccess", OdbcType.Char).Value = PublicAccess;
			if (RptMemCriId == string.Empty)
			{
				cmd.Parameters.Add("@RptMemCriId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptMemCriId", OdbcType.Numeric).Value = RptMemCriId;
			}
			if (RptMemFldId == string.Empty) 
			{
				ApplicationAssert.CheckCondition(false, "SqlReportAccess", "MemSqlReport", "Please select a folder and try again.");
			}
			else
			{
				cmd.Parameters.Add("@RptMemFldId", OdbcType.Numeric).Value = RptMemFldId;
			}
			if (RptMemCriName == string.Empty)
			{
				ApplicationAssert.CheckCondition(false, "SqlReportAccess", "MemSqlReport", "Please enter a name for this memorized criteria and try again.");
			}
			else
			{
				cmd.Parameters.Add("@RptMemCriName", OdbcType.NVarChar).Value = RptMemCriName;
			}
			if (RptMemCriDesc == string.Empty)
			{
				cmd.Parameters.Add("@RptMemCriDesc", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptMemCriDesc", OdbcType.NVarChar).Value = RptMemCriDesc;
			}
			cmd.Parameters.Add("@RptMemCriLink", OdbcType.VarChar).Value = RptMemCriLink;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@usrId", OdbcType.Numeric).Value = usrId;
			foreach (DataRowView drv in dvCri)
			{
				if (drv["RequiredValid"].ToString() == "N" && dr[drv["ColumnName"].ToString()].ToString().Trim() == string.Empty)
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Real).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Double).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Binary).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarBinary).Value = System.DBNull.Value; }
                    else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.DateTime).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Decimal).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Date).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Char).Value = System.DBNull.Value; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarChar).Value = System.DBNull.Value; }
				}
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "WChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.NChar).Value = dr[drv["ColumnName"].ToString()]; }
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "VarWChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.NVarChar).Value = dr[drv["ColumnName"].ToString()]; }
				else
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Real).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Double).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Binary).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarBinary).Value = dr[drv["ColumnName"].ToString()]; }
                    else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.DateTime).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Decimal).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Date).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.Char).Value = dr[drv["ColumnName"].ToString()]; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OdbcType.VarChar).Value = dr[drv["ColumnName"].ToString()]; }
				}
			}
			try
			{
				da.SelectCommand = TransformCmd(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0) { rtn = dt.Rows[0][0].ToString(); }
				tr.Commit();
			}
			catch (Exception e)
			{
				tr.Rollback();
				ApplicationAssert.CheckCondition(false, "", "", e.Message);
			}
			finally
			{
				cn.Close();
			}
			if (ds.HasErrors)
			{
				ds.Tables["DtSqlReportIn"].GetErrors()[0].ClearErrors();
			}
			else
			{
				ds.AcceptChanges();
			}
			return rtn;
		}

		public override void UpdMemViewdt(string GenPrefix, string RptMemCriId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("UpdMemViewdt", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = _CommandTimeOut;
			cmd.Transaction = tr;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@RptMemCriId", OdbcType.Numeric).Value = RptMemCriId;
			try
			{
				da.UpdateCommand = cmd;
				TransformCmd(cmd).ExecuteNonQuery();
				tr.Commit();
			}
			catch (Exception e)
			{
				tr.Rollback();
				ApplicationAssert.CheckCondition(false, "", "", e.Message);
			}
			finally
			{
				cn.Close();
			}
			return;
		}

		public override void DelRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("DelRptCriteria", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = usrId;
			cmd.CommandTimeout = _CommandTimeOut;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public override void IniRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("IniRptCriteria", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = usrId;
			cmd.CommandTimeout = _CommandTimeOut;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public override DataTable GetRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetRptCriteria", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = usrId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetReportCriteria(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetReportCriteria", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetReportCriteria", "Report Criteria Issue", "Criteria for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetReportColumns(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetReportColumns", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetSqlColumns", "Report Columns Issue", "Columns for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetCriReportGrp(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetCriReportGrp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetCriReportGrp", "Report Groups Issue", "Groups for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public override void MkReportGetIn(string GenPrefix, string reportCriId, string procedureName, string appDatabase, string sysDatabase, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName);}
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("MkReportGetIn", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportCriId", OdbcType.Numeric).Value = reportCriId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			return;
		}

        public override DataTable GetIn(string reportId, string programName, int TotalCnt, string RequiredValid, bool bAll, string keyId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetIn" + programName, new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@UsrGroups", OdbcType.VarChar).Value = ui.UsrGroups;
			cmd.Parameters.Add("@Cultures", OdbcType.VarChar).Value = ui.Cultures;
			cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@Projects", OdbcType.VarChar).Value = ui.Projects;
			cmd.Parameters.Add("@Agents", OdbcType.VarChar).Value = ui.Agents;
			cmd.Parameters.Add("@Brokers", OdbcType.VarChar).Value = ui.Brokers;
			cmd.Parameters.Add("@Customers", OdbcType.VarChar).Value = ui.Customers;
			cmd.Parameters.Add("@Investors", OdbcType.VarChar).Value = ui.Investors;
			cmd.Parameters.Add("@Members", OdbcType.VarChar).Value = ui.Members;
			cmd.Parameters.Add("@Vendors", OdbcType.VarChar).Value = ui.Vendors;
            cmd.Parameters.Add("@Borrowers", OdbcType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OdbcType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OdbcType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@bAll", OdbcType.Char).Value = bAll ? "Y" : "N";
            cmd.Parameters.Add("@keyId", OdbcType.VarChar).Value = string.IsNullOrEmpty(keyId) ? System.DBNull.Value : (object)keyId;
            da.SelectCommand = TransformCmd(cmd);
            cmd.CommandTimeout = _CommandTimeOut;
			DataTable dt = new DataTable();
			da.Fill(dt);
            if (RequiredValid != "Y" && dt.Rows.Count >= TotalCnt) { dt.Rows.InsertAt(dt.NewRow(), 0); }
			return dt;
		}

        public override int CountRptCri(string ReportCriId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())); } else { cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))); }
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("CountRptCri", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReportCriId", OdbcType.Numeric).Value = ReportCriId;
            int rtn = Convert.ToInt32(TransformCmd(cmd).ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
            return rtn;
        }

        public override DataTable GetDdlRptMemCri(string GenPrefix, string reportId, bool bAll, int topN, string keyId, string dbConnectionString, string dbPassword, string filterTxt, UsrImpr ui, UsrCurr uc)
        {
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetDdlRptMemCri", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			if (bAll) { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
            if (filterTxt == string.Empty)
            {
                cmd.Parameters.Add("@filterTxt", OdbcType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                if (Config.DoubleByteDb) { cmd.Parameters.Add("@filterTxt", OdbcType.NVarChar).Value = filterTxt; } else { cmd.Parameters.Add("@filterTxt", OdbcType.VarChar).Value = filterTxt; }
            }
            cmd.Parameters.Add("@topN", OdbcType.Numeric).Value = topN; 
            cmd.CommandTimeout = _CommandTimeOut;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		public override DataTable GetDdlRptMemFld(string GenPrefix, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetDdlRptMemFld", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			if (bAll) { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = _CommandTimeOut;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		public override string GetRptWizId(string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetRptWizId", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			if (dt != null && dt.Rows.Count > 0) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
		}

		public override DataTable GetAllowSelect(string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetAllowSelect", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
            return dt;
        }

		public override DataTable GetRptHlp(string GenPrefix, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = null;
			if (dbConnectionString == string.Empty || dbPassword == string.Empty)
			{
				cmd = new OdbcCommand("GetRptHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			}
			else
			{
				cmd = new OdbcCommand("GetRptHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@cultureId", OdbcType.Numeric).Value = cultureId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetReportHlp", "Report Issue", "Default help message not available for Report #'" + reportId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
			return dt;
		}

		public override DataTable GetRptCriHlp(string GenPrefix, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetRptCriHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@cultureId", OdbcType.Numeric).Value = cultureId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetRptCriHlp", "Report Issue", "Report Criteria Column Headers not available for Report #'" + reportId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
			return dt;
		}

		public override void DelMemCri(string GenPrefix, string reportId, string MemCriId, string DelHeader, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("DelMemCri", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@MemCriId", OdbcType.Numeric).Value = MemCriId;
			cmd.Parameters.Add("@DelHeader", OdbcType.Char).Value = DelHeader;
			cmd.CommandTimeout = _CommandTimeOut;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public override void IniMemCri(string GenPrefix, string reportId, string MemCriId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("IniMemCri", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@MemCriId", OdbcType.Numeric).Value = MemCriId;
			cmd.CommandTimeout = _CommandTimeOut;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public override DataTable GetMemCri(string GenPrefix, string reportId, string MemCriId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetMemCri", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@MemCriId", OdbcType.Numeric).Value = MemCriId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetReportObjHlp(string GenPrefix, Int32 ReportId, Int16 CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetReportObjHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = ReportId;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetDdlAccessCd(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetDdlAccessCd", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			if (bAll) { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OdbcType.Char).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OdbcType.Char).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = _CommandTimeOut;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void DelMemFld(string GenPrefix, string MemFldId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("DelMemFld", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@MemFldId", OdbcType.Numeric).Value = MemFldId;
			cmd.CommandTimeout = _CommandTimeOut;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public override string UpdMemFld(string GenPrefix, string PublicAccess, string RptMemFldId, string RptMemFldName, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("UpdMemFld", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = _CommandTimeOut;
			cmd.Transaction = tr;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@PublicAccess", OdbcType.Char).Value = PublicAccess;
			if (RptMemFldId == string.Empty)
			{
				cmd.Parameters.Add("@RptMemFldId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptMemFldId", OdbcType.Numeric).Value = RptMemFldId;
			}
			cmd.Parameters.Add("@RptMemFldName", OdbcType.NVarChar).Value = RptMemFldName;
			cmd.Parameters.Add("@usrId", OdbcType.Numeric).Value = usrId;
			try
			{
				da.SelectCommand = TransformCmd(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0) { rtn = dt.Rows[0][0].ToString(); }
				tr.Commit();
			}
			catch (Exception e)
			{
				tr.Rollback();
				ApplicationAssert.CheckCondition(false, "", "", e.Message);
			}
			finally
			{
				cn.Close();
			}
			return rtn;
		}
	}
}
