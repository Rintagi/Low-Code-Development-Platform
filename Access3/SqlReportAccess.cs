using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;

namespace RO.Access3
{
	public class SqlReportAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;

		public SqlReportAccess()
		{
			da = new OleDbDataAdapter();
		}

		public void Dispose()
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

		public DataTable GetDocImage(string ReportId, Int16 TemplateId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetDocImage", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
			cmd.Parameters.Add("@TemplateId", OleDbType.Numeric).Value = TemplateId;
			da.SelectCommand = cmd;
			cmd.CommandTimeout = 1800;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetGaugeValue(string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetGaugeValue", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			cmd.CommandTimeout = 1800;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetSqlReport(string reportId, string programName, DataView dvCri, UsrImpr ui, UsrCurr uc, DataSet ds, string dbConnectionString, string dbPassword, bool bUpd, bool bXls, bool bVal)
		{
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
			OleDbCommand cmd = new OleDbCommand("Get" + programName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			DataRow dr = ds.Tables["DtSqlReportIn"].Rows[0];
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@UsrGroups", OleDbType.VarChar).Value = ui.UsrGroups;
			cmd.Parameters.Add("@Cultures", OleDbType.VarChar).Value = ui.Cultures;
			cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@Projects", OleDbType.VarChar).Value = ui.Projects;
			cmd.Parameters.Add("@Agents", OleDbType.VarChar).Value = ui.Agents;
			cmd.Parameters.Add("@Brokers", OleDbType.VarChar).Value = ui.Brokers;
			cmd.Parameters.Add("@Customers", OleDbType.VarChar).Value = ui.Customers;
			cmd.Parameters.Add("@Investors", OleDbType.VarChar).Value = ui.Investors;
			cmd.Parameters.Add("@Members", OleDbType.VarChar).Value = ui.Members;
			cmd.Parameters.Add("@Vendors", OleDbType.VarChar).Value = ui.Vendors;
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			foreach (DataRowView drv in dvCri)
			{
				if (drv["RequiredValid"].ToString() == "N" && dr[drv["ColumnName"].ToString()].ToString().Trim() == string.Empty)
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Numeric).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Single).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Double).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Currency).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Binary).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarBinary).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBTimeStamp).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Decimal).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBDate).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Char).Value = System.DBNull.Value; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarChar).Value = System.DBNull.Value; }
				}
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "WChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.WChar).Value = dr[drv["ColumnName"].ToString()]; }
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "VarWChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarWChar).Value = dr[drv["ColumnName"].ToString()]; }
				else
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Single).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Double).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Currency).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Binary).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarBinary).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBTimeStamp).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Decimal).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBDate).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Char).Value = dr[drv["ColumnName"].ToString()]; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarChar).Value = dr[drv["ColumnName"].ToString()]; }
				}
			}
			if (bUpd) { cmd.Parameters.Add("@bUpd", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bUpd", OleDbType.Char).Value = "N"; }
			if (bXls) {cmd.Parameters.Add("@bXls", OleDbType.Char).Value = "Y";} else {cmd.Parameters.Add("@bXls", OleDbType.Char).Value = "N";}
			if (bVal) {cmd.Parameters.Add("@bVal", OleDbType.Char).Value = "Y";} else {cmd.Parameters.Add("@bVal", OleDbType.Char).Value = "N";}
			da.SelectCommand = cmd;
			cmd.CommandTimeout = 1800;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void UpdSqlReport(string reportId, string programName, DataView dvCri, Int32 usrId, DataSet ds, string dbConnectionString, string dbPassword)
		{
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("Upd" + programName, cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1800;
			cmd.Transaction = tr;
			DataRow dr = ds.Tables["DtSqlReportIn"].Rows[0];
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = usrId;
			foreach (DataRowView drv in dvCri)
			{
				if (drv["RequiredValid"].ToString() == "N" && dr[drv["ColumnName"].ToString()].ToString().Trim() == string.Empty)
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Numeric).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Single).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Double).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Currency).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Binary).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarBinary).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBTimeStamp).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Decimal).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBDate).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Char).Value = System.DBNull.Value; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarChar).Value = System.DBNull.Value; }
				}
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "WChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.WChar).Value = dr[drv["ColumnName"].ToString()]; }
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "VarWChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarWChar).Value = dr[drv["ColumnName"].ToString()]; }
				else
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Single).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Double).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Currency).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Binary).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarBinary).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBTimeStamp).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Decimal).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBDate).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Char).Value = dr[drv["ColumnName"].ToString()]; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarChar).Value = dr[drv["ColumnName"].ToString()]; }
				}
			}
			try
			{
				da.UpdateCommand = cmd;
				cmd.ExecuteNonQuery();
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

		public string MemSqlReport(string PublicAccess, string RptMemCriId, string RptMemFldId, string RptMemCriName, string RptMemCriDesc, string RptMemCriLink, string reportId, string programName, DataView dvCri, Int32 usrId, DataSet ds, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("Mem" + programName, cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1800;
			cmd.Transaction = tr;
			DataRow dr = ds.Tables["DtSqlReportIn"].Rows[0];
			cmd.Parameters.Add("@PublicAccess", OleDbType.Char).Value = PublicAccess;
			if (RptMemCriId == string.Empty)
			{
				cmd.Parameters.Add("@RptMemCriId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptMemCriId", OleDbType.Numeric).Value = RptMemCriId;
			}
			if (RptMemFldId == string.Empty) 
			{
				ApplicationAssert.CheckCondition(false, "SqlReportAccess", "MemSqlReport", "Please select a folder and try again.");
			}
			else
			{
				cmd.Parameters.Add("@RptMemFldId", OleDbType.Numeric).Value = RptMemFldId;
			}
			if (RptMemCriName == string.Empty)
			{
				ApplicationAssert.CheckCondition(false, "SqlReportAccess", "MemSqlReport", "Please enter a name for this memorized criteria and try again.");
			}
			else
			{
				cmd.Parameters.Add("@RptMemCriName", OleDbType.VarWChar).Value = RptMemCriName;
			}
			if (RptMemCriDesc == string.Empty)
			{
				cmd.Parameters.Add("@RptMemCriDesc", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptMemCriDesc", OleDbType.VarWChar).Value = RptMemCriDesc;
			}
			cmd.Parameters.Add("@RptMemCriLink", OleDbType.VarChar).Value = RptMemCriLink;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = usrId;
			foreach (DataRowView drv in dvCri)
			{
				if (drv["RequiredValid"].ToString() == "N" && dr[drv["ColumnName"].ToString()].ToString().Trim() == string.Empty)
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Numeric).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Single).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Double).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Currency).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Binary).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarBinary).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBTimeStamp).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Decimal).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBDate).Value = System.DBNull.Value; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Char).Value = System.DBNull.Value; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarChar).Value = System.DBNull.Value; }
				}
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "WChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.WChar).Value = dr[drv["ColumnName"].ToString()]; }
				else if (Config.DoubleByteDb && drv["DataTypeDByteOle"].ToString() == "VarWChar") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarWChar).Value = dr[drv["ColumnName"].ToString()]; }
				else
				{
					if (drv["DataTypeSByteOle"].ToString() == "Numeric") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Numeric).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Single") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Single).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Double") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Double).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Currency") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Currency).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Binary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Binary).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "VarBinary") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarBinary).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBTimeStamp") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBTimeStamp).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Decimal") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Decimal).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "DBDate") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.DBDate).Value = dr[drv["ColumnName"].ToString()]; }
					else if (drv["DataTypeSByteOle"].ToString() == "Char") { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.Char).Value = dr[drv["ColumnName"].ToString()]; }
					else { cmd.Parameters.Add("@" + drv["ColumnName"].ToString(), OleDbType.VarChar).Value = dr[drv["ColumnName"].ToString()]; }
				}
			}
			try
			{
				da.SelectCommand = cmd;
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

		public void UpdMemViewdt(string GenPrefix, string RptMemCriId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("UpdMemViewdt", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1800;
			cmd.Transaction = tr;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@RptMemCriId", OleDbType.Numeric).Value = RptMemCriId;
			try
			{
				da.UpdateCommand = cmd;
				cmd.ExecuteNonQuery();
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

		public void DelRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DelRptCriteria", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public void IniRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("IniRptCriteria", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public DataTable GetRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetRptCriteria", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetReportCriteria(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetReportCriteria", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetReportCriteria", "Report Criteria Issue", "Criteria for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetReportColumns(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetReportColumns", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetSqlColumns", "Report Columns Issue", "Columns for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetCriReportGrp(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetCriReportGrp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetCriReportGrp", "Report Groups Issue", "Groups for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public void MkReportGetIn(string GenPrefix, string reportCriId, string procedureName, string appDatabase, string sysDatabase, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName);}
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("MkReportGetIn", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportCriId", OleDbType.Numeric).Value = reportCriId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			return;
		}

        public DataTable GetIn(string reportId, string programName, int TotalCnt, string RequiredValid, bool bAll, string keyId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetIn" + programName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@UsrGroups", OleDbType.VarChar).Value = ui.UsrGroups;
			cmd.Parameters.Add("@Cultures", OleDbType.VarChar).Value = ui.Cultures;
			cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@Projects", OleDbType.VarChar).Value = ui.Projects;
			cmd.Parameters.Add("@Agents", OleDbType.VarChar).Value = ui.Agents;
			cmd.Parameters.Add("@Brokers", OleDbType.VarChar).Value = ui.Brokers;
			cmd.Parameters.Add("@Customers", OleDbType.VarChar).Value = ui.Customers;
			cmd.Parameters.Add("@Investors", OleDbType.VarChar).Value = ui.Investors;
			cmd.Parameters.Add("@Members", OleDbType.VarChar).Value = ui.Members;
			cmd.Parameters.Add("@Vendors", OleDbType.VarChar).Value = ui.Vendors;
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@bAll", OleDbType.Char).Value = bAll ? "Y" : "N";
            cmd.Parameters.Add("@keyId", OleDbType.VarChar).Value = string.IsNullOrEmpty(keyId) ? System.DBNull.Value : (object)keyId;
            da.SelectCommand = cmd;
            cmd.CommandTimeout = 1800;
			DataTable dt = new DataTable();
			da.Fill(dt);
            if (RequiredValid != "Y" && dt.Rows.Count >= TotalCnt) { dt.Rows.InsertAt(dt.NewRow(), 0); }
			return dt;
		}

        public int CountRptCri(string ReportCriId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("CountRptCri", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReportCriId", OleDbType.Numeric).Value = ReportCriId;
            int rtn = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
            return rtn;
        }

        public DataTable GetDdlRptMemCri(string GenPrefix, string reportId, bool bAll, int topN, string keyId, string dbConnectionString, string dbPassword, string filterTxt, UsrImpr ui, UsrCurr uc)
        {
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetDdlRptMemCri", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            if (filterTxt == string.Empty)
            {
                cmd.Parameters.Add("@filterTxt", OleDbType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                if (Config.DoubleByteDb) { cmd.Parameters.Add("@filterTxt", OleDbType.VarWChar).Value = filterTxt; } else { cmd.Parameters.Add("@filterTxt", OleDbType.VarChar).Value = filterTxt; }
            }
            cmd.Parameters.Add("@topN", OleDbType.Numeric).Value = topN; cmd.CommandTimeout = 1800;
            cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		public DataTable GetDdlRptMemFld(string GenPrefix, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetDdlRptMemFld", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		public string GetRptWizId(string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetRptWizId", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			if (dt != null && dt.Rows.Count > 0) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
		}

		public DataTable GetAllowSelect(string reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetAllowSelect", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
            return dt;
        }

		public DataTable GetRptHlp(string GenPrefix, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = null;
			if (dbConnectionString == string.Empty || dbPassword == string.Empty)
			{
				cmd = new OleDbCommand("GetRptHlp", new OleDbConnection(GetDesConnStr()));
			}
			else
			{
				cmd = new OleDbCommand("GetRptHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetReportHlp", "Report Issue", "Default help message not available for Report #'" + reportId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
			return dt;
		}

		public DataTable GetRptCriHlp(string GenPrefix, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetRptCriHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetRptCriHlp", "Report Issue", "Report Criteria Column Headers not available for Report #'" + reportId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
			return dt;
		}

		public void DelMemCri(string GenPrefix, string reportId, string MemCriId, string DelHeader, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DelMemCri", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@MemCriId", OleDbType.Numeric).Value = MemCriId;
			cmd.Parameters.Add("@DelHeader", OleDbType.Char).Value = DelHeader;
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public void IniMemCri(string GenPrefix, string reportId, string MemCriId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("IniMemCri", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@MemCriId", OleDbType.Numeric).Value = MemCriId;
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public DataTable GetMemCri(string GenPrefix, string reportId, string MemCriId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetMemCri", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@MemCriId", OleDbType.Numeric).Value = MemCriId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetReportObjHlp(string GenPrefix, Int32 ReportId, Int16 CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetReportObjHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetDdlAccessCd(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetDdlAccessCd", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OleDbType.Char).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OleDbType.Char).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void DelMemFld(string GenPrefix, string MemFldId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DelMemFld", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@MemFldId", OleDbType.Numeric).Value = MemFldId;
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
			finally { cn.Close(); }
			cmd.Dispose();
			cmd = null;
			return;
		}

		public string UpdMemFld(string GenPrefix, string PublicAccess, string RptMemFldId, string RptMemFldName, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("UpdMemFld", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1800;
			cmd.Transaction = tr;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@PublicAccess", OleDbType.Char).Value = PublicAccess;
			if (RptMemFldId == string.Empty)
			{
				cmd.Parameters.Add("@RptMemFldId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptMemFldId", OleDbType.Numeric).Value = RptMemFldId;
			}
			cmd.Parameters.Add("@RptMemFldName", OleDbType.VarWChar).Value = RptMemFldName;
			cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = usrId;
			try
			{
				da.SelectCommand = cmd;
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
