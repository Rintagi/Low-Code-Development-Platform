namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	// Stock rules only. Written over by robot on each deployment.
	public class WebAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;
        private DataRowCollection rows;
        private DataRow row;
	
		public WebAccess()
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
					if( da.SelectCommand.Connection != null  )
					{
						da.SelectCommand.Connection.Dispose();
					}
					da.SelectCommand.Dispose();
				}    
				da.Dispose();
				da = null;
			}
		}

        public bool WrIsUniqueEmail(string UsrEmail)
        {
            DataTable dt = new DataTable();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON IF EXISTS (SELECT 1 FROM dbo.Usr WHERE UsrEmail = '" + UsrEmail.Trim().Replace("'", "''") + "') SELECT 'false' ELSE SELECT 'true'", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            try { da.SelectCommand = cmd; da.Fill(dt); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrIsUniqueEmail", "", e.Message); }
            finally { cn.Close(); }
            bool rtn = false;
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "true") { rtn = true; }
            return rtn;
        }

        // Return TableId to capture uploads of documents:
        public DataTable WrAddDocTbl(byte SystemId, string TableName, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrAddDocTbl", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
            cmd.Parameters.Add("@TableName", OleDbType.VarChar).Value = TableName;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Return TableId to capture workflow status:
        public DataTable WrAddWfsTbl(byte SystemId, string TableName, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrAddWfsTbl", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
            cmd.Parameters.Add("@TableName", OleDbType.VarChar).Value = TableName;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Get a list of active user emails.
        public DataTable WrGetActiveEmails(string MaintMsgId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrGetActiveEmails", new OleDbConnection(GetDesConnStr()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaintMsgId", OleDbType.Numeric).Value = MaintMsgId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Get email and other info for a specific user.
        public DataTable WrGetUsrEmail(string UsrId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrGetUsrEmail", new OleDbConnection(GetDesConnStr()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

		// Get default CultureId.
		public string WrGetDefCulture(bool CultureIdOnly)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetDefCulture", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "WrGetDefCulture", "Culture Missing", "Default culture not found. Please contact systems adminstrator.");
			if (CultureIdOnly) {return dt.Rows[0][0].ToString();} else {return dt.Rows[0][1].ToString();}
		}

		// Obtain table-valued function from the physical database for virtual table.
		public string WrGetVirtualTbl(string TableId, byte DbId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrGetVirtualTbl", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@TableId", OleDbType.Numeric).Value = TableId;
			cmd.Parameters.Add("@DbId", OleDbType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0)
				{
					DataView dv = new DataView(dt);
					string ss = string.Empty;
					foreach (DataRowView drv in dv)
					{
						ss = ss + drv[0].ToString();
					}
					rtn = ss.Trim();
				}
			}
			catch (Exception e)
			{
				rtn = "Error: (WrGetVirtualTbl)" + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally { cn.Close(); }
			return rtn;
		}

		// Synchronize by physical database and return new TableId upon insert.
		public string WrSyncByDb(int UsrId, byte SystemId, byte DbId, string TableId, string TableName, string TableDesc, bool MultiDesignDb, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("WrSyncByDb", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
			cmd.Parameters.Add("@DbId", OleDbType.Numeric).Value = DbId;
			if (TableId == string.Empty)
			{
				cmd.Parameters.Add("@TableId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@TableId", OleDbType.Numeric).Value = TableId;
			}
			cmd.Parameters.Add("@TableName", OleDbType.VarChar).Value = TableName;
			cmd.Parameters.Add("@TableDesc", OleDbType.VarChar).Value = TableDesc;
			if (MultiDesignDb) {cmd.Parameters.Add("@MultiDesignDb", OleDbType.Char).Value = "Y";} else {cmd.Parameters.Add("@MultiDesignDb", OleDbType.Char).Value = "N";}
			cmd.CommandTimeout = 1800;
			cmd.Transaction = tr;
			try
			{
				da.SelectCommand = cmd;
				DataTable dt = new DataTable();
				da.Fill(dt);
				tr.Commit();
				rtn = dt.Rows[0][0].ToString();
			}
			catch(Exception e)
			{
				tr.Rollback();
				rtn = "Error: (WrSyncByDb) " + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally {cn.Close();}
			return rtn;
		}

		// Return error message if physical table has not been synchronized successfully.
		public string WrSyncToDb(byte SystemId, string TableId, string TableName, bool MultiDesignDb, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("WrSyncToDb", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
			cmd.Parameters.Add("@TableId", OleDbType.Numeric).Value = TableId;
			cmd.Parameters.Add("@TableName", OleDbType.VarChar).Value = TableName;
			if (MultiDesignDb) {cmd.Parameters.Add("@MultiDesignDb", OleDbType.Char).Value = "Y";} else {cmd.Parameters.Add("@MultiDesignDb", OleDbType.Char).Value = "N";}
			cmd.CommandTimeout = 1800;
			cmd.Transaction = tr;
			try
			{
				da.SelectCommand = cmd;
				DataTable dt = new DataTable();
				da.Fill(dt);
				tr.Commit();
			}
			catch(Exception e)
			{
				tr.Rollback();
				rtn = "Error: (WrSyncToDb) " + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally {cn.Close();}
			return rtn;
		}

		// Obtain stored procedure from the physical database for Custom Content.
		public string WrGetCustomSp(string CustomDtlId, byte DbId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrGetCustomSp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CustomDtlId", OleDbType.Numeric).Value = CustomDtlId;
			cmd.Parameters.Add("@DbId", OleDbType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0)
				{
					DataView dv = new DataView(dt);
					string ss = string.Empty;
					foreach (DataRowView drv in dv)
					{
						ss = ss + drv[0].ToString();
					}
					return ss.Trim();
				}
			}
			catch (Exception e)
			{
				rtn = "Error: (WrGetCustomSp)" + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally { cn.Close(); }
			return rtn;
		}

		// Obtain stored procedure from the physical database for Server Rule.
		public string WrGetSvrRule(string ServerRuleId, byte DbId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrGetSvrRule", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ServerRuleId", OleDbType.Numeric).Value = ServerRuleId;
			cmd.Parameters.Add("@DbId", OleDbType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0)
				{
					DataView dv = new DataView(dt);
					string ss = string.Empty;
					foreach (DataRowView drv in dv)
					{
						ss = ss + drv[0].ToString();
					}
					rtn = ss.Trim();
				}
			}
			finally { cn.Close(); }
			return rtn;
		}

        // Return databases information for Data Table.
        public DataTable WrGetDbTableSys(string TableId, byte DbId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrGetDbTableSys", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TableId", OleDbType.Numeric).Value = TableId;
            cmd.Parameters.Add("@DbId", OleDbType.Numeric).Value = DbId.ToString();
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

		// Return databases affected for synchronization of this stored procedure to physical database.
		public DataTable WrGetSvrRuleSys(string ScreenId, byte DbId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetSvrRuleSys", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@DbId", OleDbType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		// Return error message if physical s.proc. has not been synchronized to database successfully.
		public string WrSyncProc(string ProcedureName, string ProcCode, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("EXEC('IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(''"
				+ ProcedureName + "'') AND type=''P'') DROP PROCEDURE " + ProcedureName + "')"
				+ " EXEC(?)", cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 1800;
			da.UpdateCommand = cmd;
			da.UpdateCommand.Transaction = tr;
			cmd.Parameters.Add("@ProcCode", OleDbType.VarChar).Value = ProcCode;
			try
			{
				da.UpdateCommand.ExecuteNonQuery();
				tr.Commit();
			}
			catch (Exception e)
			{
				tr.Rollback();
				rtn = "Error: (WrSyncProc)" + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally
			{
				cmd.Dispose();
				cmd = null;
				cn.Close();
			}
			return rtn;
		}

		// Return error message if physical function has not been synchronized to database successfully.
		public string WrSyncFunc(string FunctionName, string ProcCode, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
            FunctionName = FunctionName.Replace("(", string.Empty).Replace(")", string.Empty);  // strip "()" from name;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("EXEC('IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(''"
				+ FunctionName + "'') AND type=''IF'') DROP FUNCTION " + FunctionName + "')"
				+ " EXEC(?)", cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 1800;
			da.UpdateCommand = cmd;
			da.UpdateCommand.Transaction = tr;
			cmd.Parameters.Add("@ProcCode", OleDbType.VarChar).Value = ProcCode;
			try
			{
				da.UpdateCommand.ExecuteNonQuery();
				tr.Commit();
			}
			catch (Exception e)
			{
				tr.Rollback();
				rtn = "Error: (WrSyncFunc)" + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally
			{
				cmd.Dispose();
				cmd = null;
				cn.Close();
			}
			return rtn;
		}

		// Update last synchronization date to the physical database.
		public string WrUpdSvrRule(string ServerRuleId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON DECLARE @now datetime"
				+ " SELECT @now = getdate() UPDATE dbo.ServerRule SET LastGenDt = @now WHERE ServerRuleId = ?"
				+ " SELECT @now", cn);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ServerRuleId", OleDbType.Numeric).Value = ServerRuleId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0) { rtn = dt.Rows[0][0].ToString(); }
			}
			finally { cn.Close(); }
			return rtn;
		}

		// Return application database affected for synchronization of this stored procedure to physical database.
		public DataTable WrGetReportApp(byte DbId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetReportApp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@DbId", OleDbType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		// Obtain stored procedure from the physical database for Report Definition.
		public string WrGetRptProc(string ProcName, byte DbId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrGetRptProc", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ProcName", OleDbType.VarChar).Value = ProcName;
			cmd.Parameters.Add("@DbId", OleDbType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0)
				{
					DataView dv = new DataView(dt);
					string ss = string.Empty;
					foreach (DataRowView drv in dv)
					{
						ss = ss + drv[0].ToString();
					}
					rtn = ss.Trim();
				}
			}
			catch (Exception e)
			{
				rtn = "Error: (WrGetRptProc)" + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally { cn.Close(); }
			return rtn;
		}

		// Update last synchronization date to the physical database.
		public string WrUpdRptProc(string ReportId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON DECLARE @now datetime"
				+ " SELECT @now = getdate() UPDATE dbo.Report SET LastGenDt = @now WHERE ReportId = ?"
				+ " SELECT @now", cn);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0) { rtn = dt.Rows[0][0].ToString(); }
			}
			catch (Exception e)
			{
				rtn = "Error: (WrUpdRptProc)" + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally { cn.Close(); }
			return rtn;
		}

        // Search memorized translation assuming default culture.
        public string WrGetMemTranslate(string InStr, string CultureId, string dbConnectionString, string dbPassword)
        {
            string rtn = string.Empty;
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("WrGetMemTranslate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InStr", OleDbType.VarWChar).Value = InStr;
            cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0) { rtn = dt.Rows[0][0].ToString(); }
            }
            catch (Exception e)
            {
                rtn = "Error: (WrGetMemTranslate)" + e.Message;	// Application.Assert does not work in Ajax.
            }
            finally { cn.Close(); }
            return rtn;
        }

		// Return information needed for crawling target websites:
		public DataTable WrGetCtCrawler(string CrawlerCd)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetCtCrawler", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CrawlerCd", OleDbType.Char).Value = CrawlerCd;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "WrGetCtCrawler", "Info Missing", "Crawler info for \"" + CrawlerCd + "\" not available. Please try again.");
			return dt;
		}

		// Return untranslated items from CtButtonHlp table:
		public DataTable WrGetCtButtonHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetCtButtonHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsCtButtonHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsCtButtonHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ButtonHlpId", OleDbType.Numeric).Value = dr["ButtonHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@ButtonName", OleDbType.VarWChar).Value = dr["ButtonName"].ToString();
            cmd.Parameters.Add("@ButtonLongNm", OleDbType.VarWChar).Value = dr["ButtonLongNm"].ToString();
            cmd.Parameters.Add("@ButtonToolTip", OleDbType.VarWChar).Value = dr["ButtonToolTip"].ToString();
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsCtButtonHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenHlp table:
		public DataTable WrGetButtonHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetButtonHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsButtonHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsButtonHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ButtonHlpId", OleDbType.Numeric).Value = dr["ButtonHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			if (dr["ButtonName"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ButtonName", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ButtonName", OleDbType.VarWChar).Value = dr["ButtonName"].ToString();
			}
            if (dr["ButtonLongNm"].ToString() == string.Empty)
            {
                cmd.Parameters.Add("@ButtonLongNm", OleDbType.VarWChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ButtonLongNm", OleDbType.VarWChar).Value = dr["ButtonLongNm"].ToString();
            }
            if (dr["ButtonToolTip"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ButtonToolTip", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ButtonToolTip", OleDbType.VarWChar).Value = dr["ButtonToolTip"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsButtonHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from MenuHlp table:
		public DataTable WrGetMenuHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetMenuHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsMenuHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsMenuHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@MenuHlpId", OleDbType.Numeric).Value = dr["MenuHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@MenuText", OleDbType.VarWChar).Value = dr["MenuText"].ToString();
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsMenuHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from MsgCenter table:
		public DataTable WrGetMsgCenter(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetMsgCenter", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsMsgCenter(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsMsgCenter", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@MsgCenterId", OleDbType.Numeric).Value = dr["MsgCenterId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@Msg", OleDbType.VarWChar).Value = dr["Msg"].ToString();
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsMsgCenter", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from designated Culture table:
		public DataTable WrGetCultureLbl(string CultureId)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetCultureLbl", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsCultureLbl(DataRow dr, string CultureId)
		{
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsCultureLbl", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureLblId", OleDbType.Numeric).Value = dr["CultureLblId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@Label", OleDbType.VarWChar).Value = dr["Label"].ToString();
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsCultureLbl", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ReportHlp table:
		public DataTable WrGetReportHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetReportHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsReportHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsReportHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportHlpId", OleDbType.Numeric).Value = dr["ReportHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@DefaultHlpMsg", OleDbType.VarWChar).Value = dr["DefaultHlpMsg"].ToString();
			cmd.Parameters.Add("@ReportTitle", OleDbType.VarWChar).Value = dr["ReportTitle"].ToString();
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsReportHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenCriHlp table:
		public DataTable WrGetReportCriHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetReportCriHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsReportCriHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsReportCriHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportCriHlpId", OleDbType.Numeric).Value = dr["ReportCriHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			if (dr["ColumnHeader"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ColumnHeader", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ColumnHeader", OleDbType.VarWChar).Value = dr["ColumnHeader"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsReportCriHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenHlp table:
		public DataTable WrGetScreenHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetScreenHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsScreenHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsScreenHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenHlpId", OleDbType.Numeric).Value = dr["ScreenHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@DefaultHlpMsg", OleDbType.VarWChar).Value = dr["DefaultHlpMsg"].ToString();
			if (dr["FootNote"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@FootNote", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@FootNote", OleDbType.VarWChar).Value = dr["FootNote"].ToString();
			}
			cmd.Parameters.Add("@ScreenTitle", OleDbType.VarWChar).Value = dr["ScreenTitle"].ToString();
			if (dr["AddMsg"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@AddMsg", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@AddMsg", OleDbType.VarWChar).Value = dr["AddMsg"].ToString();
			}
			if (dr["UpdMsg"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@UpdMsg", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@UpdMsg", OleDbType.VarWChar).Value = dr["UpdMsg"].ToString();
			}
			if (dr["DelMsg"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@DelMsg", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@DelMsg", OleDbType.VarWChar).Value = dr["DelMsg"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenCriHlp table:
		public DataTable WrGetScreenCriHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetScreenCriHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsScreenCriHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsScreenCriHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenCriHlpId", OleDbType.Numeric).Value = dr["ScreenCriHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			if (dr["ColumnHeader"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ColumnHeader", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ColumnHeader", OleDbType.VarWChar).Value = dr["ColumnHeader"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenCriHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenFilterHlp table:
		public DataTable WrGetScreenFilterHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetScreenFilterHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsScreenFilterHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsScreenFilterHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenFilterHlpId", OleDbType.Numeric).Value = dr["ScreenFilterHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@FilterName", OleDbType.VarWChar).Value = dr["FilterName"].ToString();
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenFilterHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenObjHlp table:
		public DataTable WrGetScreenObjHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetScreenObjHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsScreenObjHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsScreenObjHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenObjHlpId", OleDbType.Numeric).Value = dr["ScreenObjHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			if (dr["ColumnHeader"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ColumnHeader", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ColumnHeader", OleDbType.VarWChar).Value = dr["ColumnHeader"].ToString();
			}
            if (dr["TbHint"].ToString() == string.Empty)
            {
                cmd.Parameters.Add("@TbHint", OleDbType.VarWChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@TbHint", OleDbType.VarWChar).Value = dr["TbHint"].ToString();
            }
            if (dr["ToolTip"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ToolTip", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ToolTip", OleDbType.VarWChar).Value = dr["ToolTip"].ToString();
			}
			if (dr["ErrMessage"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ErrMessage", OleDbType.VarWChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ErrMessage", OleDbType.VarWChar).Value = dr["ErrMessage"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenObjHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenTabHlp table:
		public DataTable WrGetScreenTabHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetScreenTabHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void WrInsScreenTabHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrInsScreenTabHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenTabHlpId", OleDbType.Numeric).Value = dr["ScreenTabHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@TabFolderName", OleDbType.VarWChar).Value = dr["TabFolderName"].ToString();
			cmd.CommandTimeout = 1800;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenTabHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

        // Return untranslated items from label table:
        public DataTable WrGetLabel(string CultureId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrGetLabel", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void WrInsLabel(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("WrInsLabel", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LabelId", OleDbType.Numeric).Value = dr["LabelId"].ToString();
            cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
            cmd.Parameters.Add("@LabelText", OleDbType.VarWChar).Value = dr["LabelText"].ToString();
            cmd.CommandTimeout = 1800;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsLabel", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

		public string WrRptwizGen(Int32 RptwizId, string SystemId, string AppDatabase, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrRptwizGen", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@RptwizId", OleDbType.Numeric).Value = RptwizId;
			cmd.Parameters.Add("@AppDatabase", OleDbType.VarChar).Value = AppDatabase;
			cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
				if (dt != null && dt.Rows.Count > 0) { rtn = dt.Rows[0][0].ToString(); }
			}
			catch (Exception e)
			{
				rtn = "Error: (WrRptwizGen)" + e.Message;	// Application.Assert does not work in Ajax.
			}
			finally { cn.Close(); }
			return rtn;
		}

		public bool WrRptwizDel(Int32 ReportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("WrRptwizDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			try { cmd.ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrRptwizDel", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return true;
		}

        public bool WrXferRpt(Int32 ReportId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("WrXferRpt", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrXferRpt", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return true;
        }

		public DataTable WrGetDdlPermId(string PermKeyId, Int32 ScreenId, Int32 TableId, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetDdlPermId", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			if (PermKeyId == string.Empty)
			{
				cmd.Parameters.Add("@PermKeyId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@PermKeyId", OleDbType.Numeric).Value = PermKeyId;
			}
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@TableId", OleDbType.Numeric).Value = TableId;
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
			cmd.Parameters.Add("@UsrGroups", OleDbType.VarChar).Value = ui.UsrGroups;
			cmd.Parameters.Add("@Agents", OleDbType.VarChar).Value = ui.Agents;
			cmd.Parameters.Add("@Brokers", OleDbType.VarChar).Value = ui.Brokers;
			cmd.Parameters.Add("@Customers", OleDbType.VarChar).Value = ui.Customers;
			cmd.Parameters.Add("@Investors", OleDbType.VarChar).Value = ui.Investors;
			cmd.Parameters.Add("@Members", OleDbType.VarChar).Value = ui.Members;
			cmd.Parameters.Add("@Vendors", OleDbType.VarChar).Value = ui.Vendors;
			cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@Projects", OleDbType.VarChar).Value = ui.Projects;
			cmd.Parameters.Add("@Cultures", OleDbType.VarChar).Value = ui.Cultures;
			cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		public DataTable WrGetAdmMenuPerm(Int32 screenId, string keyId58, string dbConnectionString, string dbPassword, Int32 screenFilterId, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("WrGetAdmMenuPerm", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			if (keyId58 == string.Empty)
			{
				cmd.Parameters.Add("@keyId58", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId58", OleDbType.Numeric).Value = keyId58;
			}
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@screenFilterId", OleDbType.Numeric).Value = screenFilterId;
			cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

        public Int32 CountEmailsSent()
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn;
            cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("CountEmailsSent", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            int rtn = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
            return rtn;
        }

        // For Report Generator:

        public DataTable GetDdlOriColumnId33S1682(string rptwizCatId, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetDdlOriColumnId33S1682", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetDdlOriColumnId33S1682", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            if (rptwizCatId == string.Empty)
            {
                cmd.Parameters.Add("@rptwizCatId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@rptwizCatId", OleDbType.Numeric).Value = rptwizCatId;
            }
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
            cmd.Parameters.Add("@Customers", OleDbType.VarChar).Value = ui.Customers;
            cmd.Parameters.Add("@Vendors", OleDbType.VarChar).Value = ui.Vendors;
            cmd.Parameters.Add("@Members", OleDbType.VarChar).Value = ui.Members;
            cmd.Parameters.Add("@Investors", OleDbType.VarChar).Value = ui.Investors;
            cmd.Parameters.Add("@Agents", OleDbType.VarChar).Value = ui.Agents;
            cmd.Parameters.Add("@Brokers", OleDbType.VarChar).Value = ui.Brokers;
            cmd.Parameters.Add("@UsrGroups", OleDbType.VarChar).Value = ui.UsrGroups;
            cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = ui.Companys;
            cmd.Parameters.Add("@Projects", OleDbType.VarChar).Value = ui.Projects;
            cmd.Parameters.Add("@Cultures", OleDbType.VarChar).Value = ui.Cultures;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@currCultureId", OleDbType.Numeric).Value = cultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetDdlSelColumnId33S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetDdlSelColumnId33S1682", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetDdlSelColumnId33S1682", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
            if (keyId == string.Empty)
            {
                cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = keyId;
            }
            if (filterId == string.Empty)
            {
                cmd.Parameters.Add("@filterId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@filterId", OleDbType.Numeric).Value = filterId;
            }
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@currCultureId", OleDbType.Numeric).Value = cultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetDdlSelColumnId44S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetDdlSelColumnId44S1682", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetDdlSelColumnId44S1682", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
            if (keyId == string.Empty)
            {
                cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = keyId;
            }
            if (filterId == string.Empty)
            {
                cmd.Parameters.Add("@filterId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@filterId", OleDbType.Numeric).Value = filterId;
            }
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@currCultureId", OleDbType.Numeric).Value = cultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetDdlSelColumnId77S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetDdlSelColumnId77S1682", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetDdlSelColumnId77S1682", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
            if (keyId == string.Empty)
            {
                cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = keyId;
            }
            if (filterId == string.Empty)
            {
                cmd.Parameters.Add("@filterId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@filterId", OleDbType.Numeric).Value = filterId;
            }
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@currCultureId", OleDbType.Numeric).Value = cultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetDdlRptGroupId3S1652(string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetDdlRptGroupId3S1652", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetDdlRptGroupId3S1652", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = "0"; dt.Rows[0][1] = "N/A";
            return dt;
        }

        public DataTable GetDdlRptChart3S1652(string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetDdlRptChart3S1652", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetDdlRptChart3S1652", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = "0"; dt.Rows[0][1] = "N/A";
            return dt;
        }

        public DataTable GetDdlOperator3S1652(string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetDdlOperator3S1652", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetDdlOperator3S1652", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public string AddAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
            + " DECLARE @RptwizId183 numeric(10,0) SELECT @RptwizId183=?"
            + " INSERT dbo.Rptwiz (RptwizName,RptwizDesc,ReportId,AccessCd,UsrId,TemplateName,OrientationCd,UnitCd,TopMargin,BottomMargin,LeftMargin,RightMargin"
            + ",RptwizTypeCd,RptwizCatId,RptChaTypeCd,ThreeD,GMinValue,GLowRange,GMidRange,GMaxValue,GNeedle,GMinValueId,GLowRangeId,GMidRangeId,GMaxValueId,GNeedleId,GPositive,GFormat)"
            + " SELECT ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?"
            + " SELECT @RptwizId183 = @@IDENTITY"
            + " SELECT @RptwizId183", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            row = ds.Tables["AdmRptWiz"].Rows[0];
            if (row["RptwizId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptwizId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizId183", OleDbType.Numeric).Value = row["RptwizId183"].ToString().Trim();
            }
            if (Config.DoubleByteDb) { cmd.Parameters.Add("@RptwizName183", OleDbType.VarWChar).Value = row["RptwizName183"].ToString().Trim(); } else { cmd.Parameters.Add("@RptwizName183", OleDbType.VarChar).Value = row["RptwizName183"].ToString().Trim(); }
            if (Config.DoubleByteDb) { cmd.Parameters.Add("@RptwizDesc183", OleDbType.VarWChar).Value = row["RptwizDesc183"].ToString().Trim(); } else { cmd.Parameters.Add("@RptwizDesc183", OleDbType.VarChar).Value = row["RptwizDesc183"].ToString().Trim(); }
            if (row["ReportId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ReportId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ReportId183", OleDbType.Numeric).Value = row["ReportId183"].ToString().Trim();
            }
            cmd.Parameters.Add("@AccessCd183", OleDbType.Char).Value = row["AccessCd183"].ToString().Trim();
            cmd.Parameters.Add("@UsrId183", OleDbType.Numeric).Value = row["UsrId183"].ToString().Trim();
            cmd.Parameters.Add("@TemplateName183", OleDbType.VarChar).Value = row["TemplateName183"].ToString().Trim();
            cmd.Parameters.Add("@OrientationCd183", OleDbType.Char).Value = row["OrientationCd183"].ToString().Trim();
            cmd.Parameters.Add("@UnitCd183", OleDbType.Char).Value = row["UnitCd183"].ToString().Trim();
            cmd.Parameters.Add("@TopMargin183", OleDbType.Decimal).Value = row["TopMargin183"].ToString().Trim();
            cmd.Parameters.Add("@BottomMargin183", OleDbType.Decimal).Value = row["BottomMargin183"].ToString().Trim();
            cmd.Parameters.Add("@LeftMargin183", OleDbType.Decimal).Value = row["LeftMargin183"].ToString().Trim();
            cmd.Parameters.Add("@RightMargin183", OleDbType.Decimal).Value = row["RightMargin183"].ToString().Trim();
            cmd.Parameters.Add("@RptwizTypeCd183", OleDbType.Char).Value = row["RptwizTypeCd183"].ToString().Trim();
            if (row["RptwizCatId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptwizCatId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizCatId183", OleDbType.Numeric).Value = row["RptwizCatId183"].ToString().Trim();
            }
            if (row["RptChaTypeCd183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptChaTypeCd183", OleDbType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptChaTypeCd183", OleDbType.Char).Value = row["RptChaTypeCd183"].ToString().Trim();
            }
            cmd.Parameters.Add("@ThreeD183", OleDbType.Char).Value = row["ThreeD183"].ToString().Trim();
            if (row["GMinValue183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMinValue183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMinValue183", OleDbType.Numeric).Value = row["GMinValue183"].ToString().Trim();
            }
            if (row["GLowRange183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GLowRange183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GLowRange183", OleDbType.Numeric).Value = row["GLowRange183"].ToString().Trim();
            }
            if (row["GMidRange183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMidRange183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMidRange183", OleDbType.Numeric).Value = row["GMidRange183"].ToString().Trim();
            }
            if (row["GMaxValue183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMaxValue183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMaxValue183", OleDbType.Numeric).Value = row["GMaxValue183"].ToString().Trim();
            }
            if (row["GNeedle183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GNeedle183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GNeedle183", OleDbType.Numeric).Value = row["GNeedle183"].ToString().Trim();
            }
            if (row["GMinValueId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMinValueId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMinValueId183", OleDbType.Numeric).Value = row["GMinValueId183"].ToString().Trim();
            }
            if (row["GLowRangeId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GLowRangeId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GLowRangeId183", OleDbType.Numeric).Value = row["GLowRangeId183"].ToString().Trim();
            }
            if (row["GMidRangeId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMidRangeId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMidRangeId183", OleDbType.Numeric).Value = row["GMidRangeId183"].ToString().Trim();
            }
            if (row["GMaxValueId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMaxValueId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMaxValueId183", OleDbType.Numeric).Value = row["GMaxValueId183"].ToString().Trim();
            }
            if (row["GNeedleId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GNeedleId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GNeedleId183", OleDbType.Numeric).Value = row["GNeedleId183"].ToString().Trim();
            }
            if (row["GPositive183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GPositive183", OleDbType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GPositive183", OleDbType.Char).Value = row["GPositive183"].ToString().Trim();
            }
            if (row["GFormat183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GFormat183", OleDbType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GFormat183", OleDbType.Char).Value = row["GFormat183"].ToString().Trim();
            }
            try
            {
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                row["RptwizId183"] = Int32.Parse(dt.Rows[0][0].ToString());
                rows = ds.Tables["AdmRptWizAdd"].Rows;
                for (int ii = 0; ii < rows.Count; ii++)
                {
                    rows[ii]["RptwizDtlId184"] = AddAdmRptWiz95Dt(row["RptwizId183"].ToString(), rows[ii]["RptwizDtlId184"].ToString(), rows[ii]["ColumnId184"].ToString(), rows[ii]["ColHeader184"].ToString(), rows[ii]["CriOperName184"].ToString(), rows[ii]["CriSelect184"].ToString(), rows[ii]["CriHeader184"].ToString(), rows[ii]["ColSelect184"].ToString(), rows[ii]["ColGroup184"].ToString(), rows[ii]["ColSort184"].ToString(), rows[ii]["AggregateCd184"].ToString(), rows[ii]["RptChartCd184"].ToString(), cn, tr);
                }
                rows = ds.Tables["AdmRptWizDel"].Rows;
                for (int ii = 0; ii < rows.Count; ii++)
                {
                    DelAdmRptWiz95Dt(rows[ii]["RptwizDtlId184"].ToString(), cn, tr);
                }
                Cr_ChkRptwizCri(row["RptwizId183"].ToString(), cn, tr);
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "AddAdmRptWiz95", "", e.Message);
            }
            finally { cn.Close(); }
            if (ds.HasErrors)
            {
                ds.Tables["AdmRptWiz"].GetErrors()[0].ClearErrors();
                ds.Tables["AdmRptWizAdd"].GetErrors()[0].ClearErrors();
                return string.Empty;
            }
            else
            {
                ds.AcceptChanges();
                return row["RptwizId183"].ToString();
            }
        }

        public bool DelAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
            + " DECLARE @RptwizId183 numeric(10,0) SELECT @RptwizId183=?"
            + " DELETE dbo.RptwizDtl"
            + " WHERE RptwizId = @RptwizId183"
            + " DELETE dbo.Rptwiz"
            + " WHERE RptwizId = @RptwizId183", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            row = ds.Tables["AdmRptWiz"].Rows[0];
            cmd.Parameters.Add("@rptwizId183", OleDbType.Numeric).Value = row["RptwizId183"];
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "", "", e.Message);
            }
            finally { cn.Close(); }
            if (ds.HasErrors)
            {
                ds.Tables["AdmRptWiz"].GetErrors()[0].ClearErrors();
                return false;
            }
            else
            {
                ds.AcceptChanges();
                return true;
            }
        }

        public bool UpdAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
            + " DECLARE @RptwizId183 numeric(10,0) SELECT @RptwizId183=?"
            + " UPDATE dbo.Rptwiz SET RptwizName=?,RptwizDesc=?,ReportId=?,AccessCd=?,UsrId=?,TemplateName=?,OrientationCd=?,UnitCd=?,TopMargin=?,BottomMargin=?,LeftMargin=?,RightMargin=?,RptwizTypeCd=?,RptwizCatId=?,RptChaTypeCd=?,ThreeD=?"
            + ",GMinValue=?,GLowRange=?,GMidRange=?,GMaxValue=?,GNeedle=?,GMinValueId=?,GLowRangeId=?,GMidRangeId=?,GMaxValueId=?,GNeedleId=?,GPositive=?,GFormat=?"
            + " WHERE RptwizId = @RptwizId183", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            da.UpdateCommand = cmd;
            da.UpdateCommand.Transaction = tr;
            row = ds.Tables["AdmRptWiz"].Rows[0];
            if (row["RptwizId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptwizId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizId183", OleDbType.Numeric).Value = row["RptwizId183"].ToString().Trim();
            }
            if (Config.DoubleByteDb) { cmd.Parameters.Add("@RptwizName183", OleDbType.VarWChar).Value = row["RptwizName183"].ToString().Trim(); } else { cmd.Parameters.Add("@RptwizName183", OleDbType.VarChar).Value = row["RptwizName183"].ToString().Trim(); }
            if (Config.DoubleByteDb) { cmd.Parameters.Add("@RptwizDesc183", OleDbType.VarWChar).Value = row["RptwizDesc183"].ToString().Trim(); } else { cmd.Parameters.Add("@RptwizDesc183", OleDbType.VarChar).Value = row["RptwizDesc183"].ToString().Trim(); }
            if (row["ReportId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ReportId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ReportId183", OleDbType.Numeric).Value = row["ReportId183"].ToString().Trim();
            }
            cmd.Parameters.Add("@AccessCd183", OleDbType.Char).Value = row["AccessCd183"].ToString().Trim();
            cmd.Parameters.Add("@UsrId183", OleDbType.Numeric).Value = row["UsrId183"].ToString().Trim();
            cmd.Parameters.Add("@TtemplateName183", OleDbType.VarChar).Value = row["TemplateName183"].ToString().Trim();
            cmd.Parameters.Add("@OrientationCd183", OleDbType.Char).Value = row["OrientationCd183"].ToString().Trim();
            cmd.Parameters.Add("@UnitCd183", OleDbType.Char).Value = row["UnitCd183"].ToString().Trim();
            cmd.Parameters.Add("@TopMargin183", OleDbType.Decimal).Value = row["TopMargin183"].ToString().Trim();
            cmd.Parameters.Add("@BottomMargin183", OleDbType.Decimal).Value = row["BottomMargin183"].ToString().Trim();
            cmd.Parameters.Add("@LeftMargin183", OleDbType.Decimal).Value = row["LeftMargin183"].ToString().Trim();
            cmd.Parameters.Add("@RightMargin183", OleDbType.Decimal).Value = row["RightMargin183"].ToString().Trim();
            cmd.Parameters.Add("@RptwizTypeCd183", OleDbType.Char).Value = row["RptwizTypeCd183"].ToString().Trim();
            if (row["RptwizCatId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptwizCatId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizCatId183", OleDbType.Numeric).Value = row["RptwizCatId183"].ToString().Trim();
            }
            if (row["RptChaTypeCd183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptChaTypeCd183", OleDbType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptChaTypeCd183", OleDbType.Char).Value = row["RptChaTypeCd183"].ToString().Trim();
            }
            cmd.Parameters.Add("@ThreeD183", OleDbType.Char).Value = row["ThreeD183"].ToString().Trim();
            if (row["GMinValue183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMinValue183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMinValue183", OleDbType.Numeric).Value = row["GMinValue183"].ToString().Trim();
            }
            if (row["GLowRange183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GLowRange183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GLowRange183", OleDbType.Numeric).Value = row["GLowRange183"].ToString().Trim();
            }
            if (row["GMidRange183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMidRange183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMidRange183", OleDbType.Numeric).Value = row["GMidRange183"].ToString().Trim();
            }
            if (row["GMaxValue183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMaxValue183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMaxValue183", OleDbType.Numeric).Value = row["GMaxValue183"].ToString().Trim();
            }
            if (row["GNeedle183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GNeedle183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GNeedle183", OleDbType.Numeric).Value = row["GNeedle183"].ToString().Trim();
            }
            if (row["GMinValueId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMinValueId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMinValueId183", OleDbType.Numeric).Value = row["GMinValueId183"].ToString().Trim();
            }
            if (row["GLowRangeId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GLowRangeId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GLowRangeId183", OleDbType.Numeric).Value = row["GLowRangeId183"].ToString().Trim();
            }
            if (row["GMidRangeId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMidRangeId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMidRangeId183", OleDbType.Numeric).Value = row["GMidRangeId183"].ToString().Trim();
            }
            if (row["GMaxValueId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMaxValueId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMaxValueId183", OleDbType.Numeric).Value = row["GMaxValueId183"].ToString().Trim();
            }
            if (row["GNeedleId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GNeedleId183", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GNeedleId183", OleDbType.Numeric).Value = row["GNeedleId183"].ToString().Trim();
            }
            if (row["GPositive183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GPositive183", OleDbType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GPositive183", OleDbType.Char).Value = row["GPositive183"].ToString().Trim();
            }
            if (row["GFormat183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GFormat183", OleDbType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GFormat183", OleDbType.Char).Value = row["GFormat183"].ToString().Trim();
            }
            try
            {
                da.UpdateCommand.ExecuteNonQuery();
                rows = ds.Tables["AdmRptWizAdd"].Rows;
                for (int ii = 0; ii < rows.Count; ii++)
                {
                    rows[ii]["RptwizDtlId184"] = AddAdmRptWiz95Dt(row["RptwizId183"].ToString(), rows[ii]["RptwizDtlId184"].ToString(), rows[ii]["ColumnId184"].ToString(), rows[ii]["ColHeader184"].ToString(), rows[ii]["CriOperName184"].ToString(), rows[ii]["CriSelect184"].ToString(), rows[ii]["CriHeader184"].ToString(), rows[ii]["ColSelect184"].ToString(), rows[ii]["ColGroup184"].ToString(), rows[ii]["ColSort184"].ToString(), rows[ii]["AggregateCd184"].ToString(), rows[ii]["RptChartCd184"].ToString(), cn, tr);
                }
                rows = ds.Tables["AdmRptWizDel"].Rows;
                for (int ii = 0; ii < rows.Count; ii++)
                {
                    DelAdmRptWiz95Dt(rows[ii]["RptwizDtlId184"].ToString(), cn, tr);
                }
                Cr_ChkRptwizCri(row["RptwizId183"].ToString(), cn, tr);
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "UpdAdmRptWiz95", "", e.Message);
            }
            finally { cn.Close(); }
            if (ds.HasErrors)
            {
                ds.Tables["AdmRptWiz"].GetErrors()[0].ClearErrors();
                ds.Tables["AdmRptWizAdd"].GetErrors()[0].ClearErrors();
                return false;
            }
            else
            {
                ds.AcceptChanges();
                return true;
            }
        }

        public Int32 AddAdmRptWiz95Dt(string RptwizId183, string RptwizDtlId184, string ColumnId184, string ColHeader184, string CriOperName184, string CriSelect184, string CriHeader184, string ColSelect184, string ColGroup184, string ColSort184, string AggregateCd184, string RptChartCd184, OleDbConnection cn, OleDbTransaction tr)
        {
            OleDbCommand cmd = new OleDbCommand("AddAdmRptWiz95Dt", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RptwizId", OleDbType.Numeric).Value = RptwizId183;
            if (RptwizDtlId184 == string.Empty)
            {
                cmd.Parameters.Add("@RptwizDtlId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizDtlId", OleDbType.Numeric).Value = RptwizDtlId184;
            }
            cmd.Parameters.Add("@ColumnId", OleDbType.Numeric).Value = ColumnId184.Trim();
            if (ColHeader184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ColHeader", OleDbType.VarWChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColHeader", OleDbType.VarWChar).Value = ColHeader184.Trim();
            }
            if (CriOperName184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@CriOperName", OleDbType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@CriOperName", OleDbType.VarChar).Value = CriOperName184.Trim();
            }
            if (CriSelect184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@CriSelect", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@CriSelect", OleDbType.Numeric).Value = CriSelect184.Trim();
            }
            if (CriHeader184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@CriHeader", OleDbType.VarWChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@CriHeader", OleDbType.VarWChar).Value = CriHeader184.Trim();
            }
            if (ColSelect184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ColSelect", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColSelect", OleDbType.Numeric).Value = ColSelect184.Trim();
            }
            if (ColGroup184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ColGroup", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColGroup", OleDbType.Numeric).Value = ColGroup184.Trim();
            }
            if (ColSort184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ColSort", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColSort", OleDbType.Numeric).Value = ColSort184.Trim();
            }
            if (AggregateCd184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@AggregateCd", OleDbType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@AggregateCd", OleDbType.Char).Value = AggregateCd184.Trim();
            }
            if (RptChartCd184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptChartCd", OleDbType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptChartCd", OleDbType.Char).Value = RptChartCd184.Trim();
            }
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            Int32 id = Int32.Parse(dt.Rows[0][0].ToString());
            cmd.Dispose();
            cmd = null;
            return id;
        }

        public void DelAdmRptWiz95Dt(string RptwizDtlId184, OleDbConnection cn, OleDbTransaction tr)
        {
            OleDbCommand cmd = new OleDbCommand("DelAdmRptWiz95Dt", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RptwizDtlId", OleDbType.Numeric).Value = RptwizDtlId184;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            return;
        }

        protected void Cr_ChkRptwizCri(string RptwizId, OleDbConnection cn, OleDbTransaction tr)
        {
            OleDbCommand cmd = new OleDbCommand("Cr_ChkRptwizCri", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (RptwizId == string.Empty)
            {
                cmd.Parameters.Add("@RptwizId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizId", OleDbType.Numeric).Value = RptwizId;
            }
            cmd.Transaction = tr;
            cmd.CommandTimeout = 1800;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            return;
        }

        public void RmTranslatedLbl(string LabelId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
            + " DECLARE @LabelCat varchar(50), @LabelKey varchar(50)"
            + " SELECT @LabelCat = LabelCat, @LabelKey = LabelKey FROM dbo.Label WHERE LabelId = ?"
            + " DELETE FROM dbo.Label WHERE LabelCat = @LabelCat AND LabelKey = @LabelKey AND CultureId <> 1", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.Add("@LabelId", OleDbType.Integer).Value = LabelId;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "RmTranslatedLbl", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public DataTable WrAddMenu(string MenuIndex, string ParentId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrAddMenu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuIndex", OleDbType.SmallInt).Value = MenuIndex;
            cmd.Parameters.Add("@ParentId", OleDbType.Integer).Value = string.IsNullOrEmpty(ParentId.Trim()) ? System.DBNull.Value : (object)ParentId.Trim();
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "WrAddMenu", "", e.Message);
            }
            finally { cn.Close(); }
            return ds.Tables[0];
        }

        public bool WrDelMenu(string MenuId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrDelMenu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuId", OleDbType.Numeric).Value = MenuId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            bool done = false;
            try { cmd.ExecuteNonQuery(); tr.Commit(); done = true; }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrDelMenu", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return done;
        }

        public void WrUpdMenu(string MenuId, string PMenuId, string ParentId, string MenuText, string CultureId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrUpdMenu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuId", OleDbType.Integer).Value = MenuId;
            cmd.Parameters.Add("@PMenuId", OleDbType.Integer).Value = string.IsNullOrEmpty(PMenuId.Trim()) ? System.DBNull.Value : (object)PMenuId.Trim();
            cmd.Parameters.Add("@MenuText", OleDbType.VarWChar).Value = MenuText;
            cmd.Parameters.Add("@ParentId", OleDbType.Integer).Value = string.IsNullOrEmpty(ParentId.Trim()) ? System.DBNull.Value : (object)ParentId.Trim();
            cmd.Parameters.Add("@CultureId", OleDbType.SmallInt).Value = CultureId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            try { cmd.ExecuteNonQuery(); tr.Commit(); }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrUpdMenu", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public DataTable WrAddScreenTab(string TabFolderOrder, string ScreenId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrAddScreenTab", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TabFolderOrder", OleDbType.SmallInt).Value = TabFolderOrder;
            cmd.Parameters.Add("@ScreenId", OleDbType.Integer).Value = string.IsNullOrEmpty(ScreenId.Trim()) ? System.DBNull.Value : (object)ScreenId.Trim(); ;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            cmd.Transaction = tr;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "WrAddScreenTab", "", e.Message);
            }
            finally { cn.Close(); }
            return ds.Tables[0];
        }

        public bool WrDelScreenTab(string ScreenTabId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("WrDelScreenTab", cn);
            OleDbTransaction tr = cn.BeginTransaction();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenTabId", OleDbType.Numeric).Value = ScreenTabId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            bool done = false;
            try { cmd.ExecuteNonQuery(); tr.Commit(); done = true; }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrDelScreenTab", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return done;
        }

        public void WrUpdScreenTab(string ScreenTabId, string TabFolderOrder, string TabFolderName, string CultureId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrUpdScreenTab", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenTabId", OleDbType.Integer).Value = ScreenTabId;
            cmd.Parameters.Add("@TabFolderOrder", OleDbType.SmallInt).Value = TabFolderOrder;
            cmd.Parameters.Add("@TabFolderName", OleDbType.VarWChar).Value = TabFolderName;
            cmd.Parameters.Add("@CultureId", OleDbType.SmallInt).Value = CultureId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            try { cmd.ExecuteNonQuery(); tr.Commit(); }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrUpdScreenTab", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public DataTable WrAddScreenObj(string ScreenId, string PScreenObjId, string TabFolderId, bool IsTab, bool NewRow, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("WrAddScreenObj", cn);
            OleDbTransaction tr = cn.BeginTransaction();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Integer).Value = string.IsNullOrEmpty(ScreenId) ? System.DBNull.Value : (object)ScreenId.Trim();
            cmd.Parameters.Add("@PScreenObjId", OleDbType.Integer).Value = string.IsNullOrEmpty(PScreenObjId.Trim()) ? System.DBNull.Value : (object)PScreenObjId.Trim();
            cmd.Parameters.Add("@TabFolderId", OleDbType.Integer).Value = string.IsNullOrEmpty(TabFolderId.Trim()) ? System.DBNull.Value : (object)TabFolderId.Trim();
            cmd.Parameters.Add("@IsTab", OleDbType.Char).Value = IsTab ? 'Y' : 'N';
            cmd.Parameters.Add("@NewRow", OleDbType.Char).Value = NewRow ? 'Y' : 'N';
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "WrUpdScreenObj", "", e.Message);
            }
            finally { cn.Close(); }
            return ds.Tables[0];
        }

        public bool WrDelScreenObj(string ScreenObjId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrDelScreenObj", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenObjId", OleDbType.Numeric).Value = ScreenObjId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = cmd;
            bool done = false;
            try { cmd.ExecuteNonQuery(); tr.Commit(); done = true; }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrDelScreenObj", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return done;
        }

        public void WrUpdScreenObj(string ScreenObjId, string PScreenObjId, string TabFolderId, string ColumnHeader, string CultureId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrUpdScreenObj", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenObjId", OleDbType.Integer).Value = ScreenObjId;
            cmd.Parameters.Add("@PScreenObjId", OleDbType.Integer).Value = string.IsNullOrEmpty(PScreenObjId.Trim()) ? System.DBNull.Value : (object)PScreenObjId.Trim();
            cmd.Parameters.Add("@ColumnHeader", OleDbType.VarWChar).Value = string.IsNullOrEmpty(ColumnHeader.Trim()) ? System.DBNull.Value : (object)ColumnHeader.Trim(); ;
            cmd.Parameters.Add("@TabFolderId", OleDbType.Integer).Value = string.IsNullOrEmpty(TabFolderId.Trim()) ? System.DBNull.Value : (object)TabFolderId.Trim();
            cmd.Parameters.Add("@CultureId", OleDbType.SmallInt).Value = CultureId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            try { cmd.ExecuteNonQuery(); tr.Commit(); }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrUpdScreenObj", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public string WrGetScreenId(string ProgramName, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrGetScreenId", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProgramName", OleDbType.VarChar).Value = ProgramName;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
        }

        public string WrGetMasterTable(string ScreenId, string ColumnId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrGetMasterTable", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Integer).Value = ScreenId;
            if (ColumnId == string.Empty)
            {
                cmd.Parameters.Add("@ColumnId", OleDbType.Integer).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColumnId", OleDbType.Integer).Value = ColumnId;
            }
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
        }

        public DataTable WrGetScreenObj(string ScreenId, Int16 CultureId, string ScreenObjId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrGetScreenObj", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Integer).Value = !string.IsNullOrEmpty(ScreenId) ? (object)ScreenId : (object)System.DBNull.Value; ;
            cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
            cmd.Parameters.Add("@ScreenObjId", OleDbType.Integer).Value = !string.IsNullOrEmpty(ScreenObjId) ? (object)ScreenObjId : (object)System.DBNull.Value;
            cmd.CommandTimeout = 1800;

            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public string WrCloneScreen(string ScreenId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("WrCloneScreen", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Integer).Value = ScreenId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
        }

        public void PurgeScrAudit(Int16 YearOld, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("PurgeScrAudit", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@YearOld", OleDbType.SmallInt).Value = YearOld;
            cmd.CommandTimeout = 1800;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "PurgeScrAudit", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public void WrUpdScreenReactGen(string ScreenId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrUpdScreenReactGen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Integer).Value = ScreenId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            try { cmd.ExecuteNonQuery(); tr.Commit(); }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrUpdScreenReactGen", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public DataTable WrGetWebRule(string ScreenId, string dbConnectionString, string dbPassword)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbCommand cmd = new OleDbCommand("WrGetWebRule", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.VarChar).Value = ScreenId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}