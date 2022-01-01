namespace RO.Access3.Odbc
{
	using System;
	using System.Data;
    //using System.Data.OleDb;
    using System.Data.Odbc;
    using System.Linq;
    using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	// Stock rules only. Written over by robot on each deployment.
	public class WebAccess : WebAccessBase, IDisposable
	{
		private OdbcDataAdapter da;
        private DataRowCollection rows;
        private DataRow row;

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
	
		public WebAccess()
		{
			da = new OdbcDataAdapter();
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

        public override bool WrIsUniqueEmail(string UsrEmail)
        {
            DataTable dt = new DataTable();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("SET NOCOUNT ON IF EXISTS (SELECT 1 FROM dbo.Usr WHERE UsrEmail = '" + UsrEmail.Trim().Replace("'", "''") + "') SELECT 'false' ELSE SELECT 'true'", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            try { da.SelectCommand = TransformCmd(cmd); da.Fill(dt); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrIsUniqueEmail", "", e.Message); }
            finally { cn.Close(); }
            bool rtn = false;
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "true") { rtn = true; }
            return rtn;
        }

        // Return TableId to capture uploads of documents:
        public override DataTable WrAddDocTbl(byte SystemId, string TableName, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrAddDocTbl", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SystemId", OdbcType.Numeric).Value = SystemId;
            cmd.Parameters.Add("@TableName", OdbcType.VarChar).Value = TableName;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Return TableId to capture workflow status:
        public override DataTable WrAddWfsTbl(byte SystemId, string TableName, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrAddWfsTbl", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SystemId", OdbcType.Numeric).Value = SystemId;
            cmd.Parameters.Add("@TableName", OdbcType.VarChar).Value = TableName;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Get a list of active user emails.
        public override DataTable WrGetActiveEmails(string MaintMsgId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrGetActiveEmails", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaintMsgId", OdbcType.Numeric).Value = MaintMsgId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Get email and other info for a specific user.
        public override DataTable WrGetUsrEmail(string UsrId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrGetUsrEmail", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

		// Get default CultureId.
		public override string WrGetDefCulture(bool CultureIdOnly)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetDefCulture", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "WrGetDefCulture", "Culture Missing", "Default culture not found. Please contact systems adminstrator.");
			if (CultureIdOnly) {return dt.Rows[0][0].ToString();} else {return dt.Rows[0][1].ToString();}
		}

		// Obtain table-valued function from the physical database for virtual table.
		public override string WrGetVirtualTbl(string TableId, byte DbId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrGetVirtualTbl", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@TableId", OdbcType.Numeric).Value = TableId;
			cmd.Parameters.Add("@DbId", OdbcType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
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
		public override string WrSyncByDb(int UsrId, byte SystemId, byte DbId, string TableId, string TableName, string TableDesc, bool MultiDesignDb, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("WrSyncByDb", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@SystemId", OdbcType.Numeric).Value = SystemId;
			cmd.Parameters.Add("@DbId", OdbcType.Numeric).Value = DbId;
			if (TableId == string.Empty)
			{
				cmd.Parameters.Add("@TableId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@TableId", OdbcType.Numeric).Value = TableId;
			}
			cmd.Parameters.Add("@TableName", OdbcType.VarChar).Value = TableName;
			cmd.Parameters.Add("@TableDesc", OdbcType.VarChar).Value = TableDesc;
			if (MultiDesignDb) {cmd.Parameters.Add("@MultiDesignDb", OdbcType.Char).Value = "Y";} else {cmd.Parameters.Add("@MultiDesignDb", OdbcType.Char).Value = "N";}
			cmd.CommandTimeout = 1800;
			cmd.Transaction = tr;
			try
			{
				da.SelectCommand = TransformCmd(cmd);
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
		public override string WrSyncToDb(byte SystemId, string TableId, string TableName, bool MultiDesignDb, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("WrSyncToDb", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@SystemId", OdbcType.Numeric).Value = SystemId;
			cmd.Parameters.Add("@TableId", OdbcType.Numeric).Value = TableId;
			cmd.Parameters.Add("@TableName", OdbcType.VarChar).Value = TableName;
			if (MultiDesignDb) {cmd.Parameters.Add("@MultiDesignDb", OdbcType.Char).Value = "Y";} else {cmd.Parameters.Add("@MultiDesignDb", OdbcType.Char).Value = "N";}
			cmd.CommandTimeout = 1800;
			cmd.Transaction = tr;
			try
			{
				da.SelectCommand = TransformCmd(cmd);
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
		public override string WrGetCustomSp(string CustomDtlId, byte DbId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrGetCustomSp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CustomDtlId", OdbcType.Numeric).Value = CustomDtlId;
			cmd.Parameters.Add("@DbId", OdbcType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
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
		public override string WrGetSvrRule(string ServerRuleId, byte DbId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrGetSvrRule", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ServerRuleId", OdbcType.Numeric).Value = ServerRuleId;
			cmd.Parameters.Add("@DbId", OdbcType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
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
        public override DataTable WrGetDbTableSys(string TableId, byte DbId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrGetDbTableSys", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TableId", OdbcType.Numeric).Value = TableId;
            cmd.Parameters.Add("@DbId", OdbcType.Numeric).Value = DbId.ToString();
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

		// Return databases affected for synchronization of this stored procedure to physical database.
		public override DataTable WrGetSvrRuleSys(string ScreenId, byte DbId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetSvrRuleSys", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OdbcType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@DbId", OdbcType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		// Return error message if physical s.proc. has not been synchronized to database successfully.
		public override string WrSyncProc(string ProcedureName, string ProcCode, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("EXEC('IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(''"
				+ ProcedureName + "'') AND type=''P'') DROP PROCEDURE " + ProcedureName + "')"
				+ " EXEC(?)", cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 1800;
			da.UpdateCommand = cmd;
			da.UpdateCommand.Transaction = tr;
			cmd.Parameters.Add("@ProcCode", OdbcType.VarChar).Value = ProcCode;
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
		public override string WrSyncFunc(string FunctionName, string ProcCode, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
            FunctionName = FunctionName.Replace("(", string.Empty).Replace(")", string.Empty);  // strip "()" from name;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("EXEC('IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(''"
				+ FunctionName + "'') AND type=''IF'') DROP FUNCTION " + FunctionName + "')"
				+ " EXEC(?)", cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 1800;
			da.UpdateCommand = cmd;
			da.UpdateCommand.Transaction = tr;
			cmd.Parameters.Add("@ProcCode", OdbcType.VarChar).Value = ProcCode;
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
		public override string WrUpdSvrRule(string ServerRuleId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("SET NOCOUNT ON DECLARE @now datetime"
				+ " SELECT @now = GETUTCDATE() UPDATE dbo.ServerRule SET LastGenDt = @now WHERE ServerRuleId = ?"
				+ " SELECT @now", cn);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ServerRuleId", OdbcType.Numeric).Value = ServerRuleId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
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
		public override DataTable WrGetReportApp(byte DbId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetReportApp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@DbId", OdbcType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		// Obtain stored procedure from the physical database for Report Definition.
		public override string WrGetRptProc(string ProcName, byte DbId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrGetRptProc", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ProcName", OdbcType.VarChar).Value = ProcName;
			cmd.Parameters.Add("@DbId", OdbcType.Numeric).Value = DbId.ToString();
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
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
		public override string WrUpdRptProc(string ReportId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("SET NOCOUNT ON DECLARE @now datetime"
				+ " SELECT @now = GETUTCDATE() UPDATE dbo.Report SET LastGenDt = @now WHERE ReportId = ?"
				+ " SELECT @now", cn);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = ReportId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
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
        public override string WrGetMemTranslate(string InStr, string CultureId, string dbConnectionString, string dbPassword)
        {
            string rtn = string.Empty;
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("WrGetMemTranslate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InStr", OdbcType.NVarChar).Value = InStr;
            cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
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
		public override DataTable WrGetCtCrawler(string CrawlerCd)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetCtCrawler", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CrawlerCd", OdbcType.Char).Value = CrawlerCd;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "WrGetCtCrawler", "Info Missing", "Crawler info for \"" + CrawlerCd + "\" not available. Please try again.");
			return dt;
		}

		// Return untranslated items from CtButtonHlp table:
		public override DataTable WrGetCtButtonHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetCtButtonHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsCtButtonHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsCtButtonHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ButtonHlpId", OdbcType.Numeric).Value = dr["ButtonHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@ButtonName", OdbcType.NVarChar).Value = dr["ButtonName"].ToString();
            cmd.Parameters.Add("@ButtonLongNm", OdbcType.NVarChar).Value = dr["ButtonLongNm"].ToString();
            cmd.Parameters.Add("@ButtonToolTip", OdbcType.NVarChar).Value = dr["ButtonToolTip"].ToString();
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsCtButtonHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenHlp table:
		public override DataTable WrGetButtonHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetButtonHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsButtonHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsButtonHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ButtonHlpId", OdbcType.Numeric).Value = dr["ButtonHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			if (dr["ButtonName"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ButtonName", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ButtonName", OdbcType.NVarChar).Value = dr["ButtonName"].ToString();
			}
            if (dr["ButtonLongNm"].ToString() == string.Empty)
            {
                cmd.Parameters.Add("@ButtonLongNm", OdbcType.NVarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ButtonLongNm", OdbcType.NVarChar).Value = dr["ButtonLongNm"].ToString();
            }
            if (dr["ButtonToolTip"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ButtonToolTip", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ButtonToolTip", OdbcType.NVarChar).Value = dr["ButtonToolTip"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsButtonHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from MenuHlp table:
		public override DataTable WrGetMenuHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetMenuHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsMenuHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsMenuHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@MenuHlpId", OdbcType.Numeric).Value = dr["MenuHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@MenuText", OdbcType.NVarChar).Value = dr["MenuText"].ToString();
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsMenuHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from MsgCenter table:
		public override DataTable WrGetMsgCenter(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetMsgCenter", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsMsgCenter(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsMsgCenter", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@MsgCenterId", OdbcType.Numeric).Value = dr["MsgCenterId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@Msg", OdbcType.NVarChar).Value = dr["Msg"].ToString();
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsMsgCenter", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from designated Culture table:
		public override DataTable WrGetCultureLbl(string CultureId)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetCultureLbl", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsCultureLbl(DataRow dr, string CultureId)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsCultureLbl", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureLblId", OdbcType.Numeric).Value = dr["CultureLblId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@Label", OdbcType.NVarChar).Value = dr["Label"].ToString();
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsCultureLbl", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ReportHlp table:
		public override DataTable WrGetReportHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetReportHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsReportHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsReportHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportHlpId", OdbcType.Numeric).Value = dr["ReportHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@DefaultHlpMsg", OdbcType.NVarChar).Value = dr["DefaultHlpMsg"].ToString();
			cmd.Parameters.Add("@ReportTitle", OdbcType.NVarChar).Value = dr["ReportTitle"].ToString();
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsReportHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenCriHlp table:
		public override DataTable WrGetReportCriHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetReportCriHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsReportCriHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsReportCriHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportCriHlpId", OdbcType.Numeric).Value = dr["ReportCriHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			if (dr["ColumnHeader"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ColumnHeader", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ColumnHeader", OdbcType.NVarChar).Value = dr["ColumnHeader"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsReportCriHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenHlp table:
		public override DataTable WrGetScreenHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetScreenHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsScreenHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsScreenHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenHlpId", OdbcType.Numeric).Value = dr["ScreenHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@DefaultHlpMsg", OdbcType.NVarChar).Value = dr["DefaultHlpMsg"].ToString();
			if (dr["FootNote"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@FootNote", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@FootNote", OdbcType.NVarChar).Value = dr["FootNote"].ToString();
			}
			cmd.Parameters.Add("@ScreenTitle", OdbcType.NVarChar).Value = dr["ScreenTitle"].ToString();
			if (dr["AddMsg"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@AddMsg", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@AddMsg", OdbcType.NVarChar).Value = dr["AddMsg"].ToString();
			}
			if (dr["UpdMsg"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@UpdMsg", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@UpdMsg", OdbcType.NVarChar).Value = dr["UpdMsg"].ToString();
			}
			if (dr["DelMsg"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@DelMsg", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@DelMsg", OdbcType.NVarChar).Value = dr["DelMsg"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenCriHlp table:
		public override DataTable WrGetScreenCriHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetScreenCriHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsScreenCriHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsScreenCriHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenCriHlpId", OdbcType.Numeric).Value = dr["ScreenCriHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			if (dr["ColumnHeader"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ColumnHeader", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ColumnHeader", OdbcType.NVarChar).Value = dr["ColumnHeader"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenCriHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenFilterHlp table:
		public override DataTable WrGetScreenFilterHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetScreenFilterHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsScreenFilterHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsScreenFilterHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenFilterHlpId", OdbcType.Numeric).Value = dr["ScreenFilterHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@FilterName", OdbcType.NVarChar).Value = dr["FilterName"].ToString();
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenFilterHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenObjHlp table:
		public override DataTable WrGetScreenObjHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetScreenObjHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsScreenObjHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsScreenObjHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenObjHlpId", OdbcType.Numeric).Value = dr["ScreenObjHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			if (dr["ColumnHeader"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ColumnHeader", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ColumnHeader", OdbcType.NVarChar).Value = dr["ColumnHeader"].ToString();
			}
            if (dr["TbHint"].ToString() == string.Empty)
            {
                cmd.Parameters.Add("@TbHint", OdbcType.NVarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@TbHint", OdbcType.NVarChar).Value = dr["TbHint"].ToString();
            }
            if (dr["ToolTip"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ToolTip", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ToolTip", OdbcType.NVarChar).Value = dr["ToolTip"].ToString();
			}
			if (dr["ErrMessage"].ToString() == string.Empty)
			{
				cmd.Parameters.Add("@ErrMessage", OdbcType.NVarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ErrMessage", OdbcType.NVarChar).Value = dr["ErrMessage"].ToString();
			}
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenObjHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

		// Return untranslated items from ScreenTabHlp table:
		public override DataTable WrGetScreenTabHlp(string CultureId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetScreenTabHlp", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void WrInsScreenTabHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrInsScreenTabHlp", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenTabHlpId", OdbcType.Numeric).Value = dr["ScreenTabHlpId"].ToString();
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@TabFolderName", OdbcType.NVarChar).Value = dr["TabFolderName"].ToString();
			cmd.CommandTimeout = 1800;
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsScreenTabHlp", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return;
		}

        // Return untranslated items from label table:
        public override DataTable WrGetLabel(string CultureId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrGetLabel", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override void WrInsLabel(DataRow dr, string CultureId, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("WrInsLabel", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LabelId", OdbcType.Numeric).Value = dr["LabelId"].ToString();
            cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
            cmd.Parameters.Add("@LabelText", OdbcType.NVarChar).Value = dr["LabelText"].ToString();
            cmd.CommandTimeout = 1800;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrInsLabel", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

		public override string WrRptwizGen(Int32 RptwizId, string SystemId, string AppDatabase, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrRptwizGen", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@RptwizId", OdbcType.Numeric).Value = RptwizId;
			cmd.Parameters.Add("@AppDatabase", OdbcType.VarChar).Value = AppDatabase;
			cmd.Parameters.Add("@SystemId", OdbcType.Numeric).Value = SystemId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
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

		public override bool WrRptwizDel(Int32 ReportId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("WrRptwizDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = ReportId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			try { TransformCmd(cmd).ExecuteNonQuery(); }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrRptwizDel", "", e.Message.ToString()); }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return true;
		}

        public override bool WrXferRpt(Int32 ReportId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("WrXferRpt", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = ReportId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "WrXferRpt", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return true;
        }

		public override DataTable WrGetDdlPermId(string PermKeyId, Int32 ScreenId, Int32 TableId, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetDdlPermId", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			if (PermKeyId == string.Empty)
			{
				cmd.Parameters.Add("@PermKeyId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@PermKeyId", OdbcType.Numeric).Value = PermKeyId;
			}
			cmd.Parameters.Add("@ScreenId", OdbcType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@TableId", OdbcType.Numeric).Value = TableId;
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
			cmd.Parameters.Add("@UsrGroups", OdbcType.VarChar).Value = ui.UsrGroups;
			cmd.Parameters.Add("@Agents", OdbcType.VarChar).Value = ui.Agents;
			cmd.Parameters.Add("@Brokers", OdbcType.VarChar).Value = ui.Brokers;
			cmd.Parameters.Add("@Customers", OdbcType.VarChar).Value = ui.Customers;
			cmd.Parameters.Add("@Investors", OdbcType.VarChar).Value = ui.Investors;
			cmd.Parameters.Add("@Members", OdbcType.VarChar).Value = ui.Members;
			cmd.Parameters.Add("@Vendors", OdbcType.VarChar).Value = ui.Vendors;
			cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@Projects", OdbcType.VarChar).Value = ui.Projects;
			cmd.Parameters.Add("@Cultures", OdbcType.VarChar).Value = ui.Cultures;
			cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		public override DataTable WrGetAdmMenuPerm(Int32 screenId, string keyId58, string dbConnectionString, string dbPassword, Int32 screenFilterId, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("WrGetAdmMenuPerm", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			if (keyId58 == string.Empty)
			{
				cmd.Parameters.Add("@keyId58", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId58", OdbcType.Numeric).Value = keyId58;
			}
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@screenFilterId", OdbcType.Numeric).Value = screenFilterId;
			cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

        public override Int32 CountEmailsSent()
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn;
            cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("CountEmailsSent", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            int rtn = Convert.ToInt32(TransformCmd(cmd).ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
            return rtn;
        }

        // For Report Generator:

        public override DataTable GetDdlOriColumnId33S1682(string rptwizCatId, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OdbcCommand("GetDdlOriColumnId33S1682", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            }
            else
            {
                cmd = new OdbcCommand("GetDdlOriColumnId33S1682", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            if (rptwizCatId == string.Empty)
            {
                cmd.Parameters.Add("@rptwizCatId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@rptwizCatId", OdbcType.Numeric).Value = rptwizCatId;
            }
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
            cmd.Parameters.Add("@Customers", OdbcType.VarChar).Value = ui.Customers;
            cmd.Parameters.Add("@Vendors", OdbcType.VarChar).Value = ui.Vendors;
            cmd.Parameters.Add("@Members", OdbcType.VarChar).Value = ui.Members;
            cmd.Parameters.Add("@Investors", OdbcType.VarChar).Value = ui.Investors;
            cmd.Parameters.Add("@Agents", OdbcType.VarChar).Value = ui.Agents;
            cmd.Parameters.Add("@Brokers", OdbcType.VarChar).Value = ui.Brokers;
            cmd.Parameters.Add("@UsrGroups", OdbcType.VarChar).Value = ui.UsrGroups;
            cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
            cmd.Parameters.Add("@Projects", OdbcType.VarChar).Value = ui.Projects;
            cmd.Parameters.Add("@Cultures", OdbcType.VarChar).Value = ui.Cultures;
            cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@currCultureId", OdbcType.Numeric).Value = cultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override DataTable GetDdlSelColumnId33S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OdbcCommand("GetDdlSelColumnId33S1682", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            }
            else
            {
                cmd = new OdbcCommand("GetDdlSelColumnId33S1682", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
            if (bAll) { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "N"; }
            if (keyId == string.Empty)
            {
                cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = keyId;
            }
            if (filterId == string.Empty)
            {
                cmd.Parameters.Add("@filterId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@filterId", OdbcType.Numeric).Value = filterId;
            }
            cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
            cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@currCultureId", OdbcType.Numeric).Value = cultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override DataTable GetDdlSelColumnId44S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OdbcCommand("GetDdlSelColumnId44S1682", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            }
            else
            {
                cmd = new OdbcCommand("GetDdlSelColumnId44S1682", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
            if (bAll) { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "N"; }
            if (keyId == string.Empty)
            {
                cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = keyId;
            }
            if (filterId == string.Empty)
            {
                cmd.Parameters.Add("@filterId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@filterId", OdbcType.Numeric).Value = filterId;
            }
            cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
            cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@currCultureId", OdbcType.Numeric).Value = cultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override DataTable GetDdlSelColumnId77S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OdbcCommand("GetDdlSelColumnId77S1682", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            }
            else
            {
                cmd = new OdbcCommand("GetDdlSelColumnId77S1682", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
            if (bAll) { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "N"; }
            if (keyId == string.Empty)
            {
                cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = keyId;
            }
            if (filterId == string.Empty)
            {
                cmd.Parameters.Add("@filterId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@filterId", OdbcType.Numeric).Value = filterId;
            }
            cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
            cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@currCultureId", OdbcType.Numeric).Value = cultureId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override DataTable GetDdlRptGroupId3S1652(string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OdbcCommand("GetDdlRptGroupId3S1652", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            }
            else
            {
                cmd = new OdbcCommand("GetDdlRptGroupId3S1652", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = "0"; dt.Rows[0][1] = "N/A";
            return dt;
        }

        public override DataTable GetDdlRptChart3S1652(string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OdbcCommand("GetDdlRptChart3S1652", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            }
            else
            {
                cmd = new OdbcCommand("GetDdlRptChart3S1652", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = "0"; dt.Rows[0][1] = "N/A";
            return dt;
        }

        public override DataTable GetDdlOperator3S1652(string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OdbcCommand("GetDdlOperator3S1652", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            }
            else
            {
                cmd = new OdbcCommand("GetDdlOperator3S1652", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override string AddAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("SET NOCOUNT ON"
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
                cmd.Parameters.Add("@RptwizId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizId183", OdbcType.Numeric).Value = row["RptwizId183"].ToString().Trim();
            }
            if (Config.DoubleByteDb) { cmd.Parameters.Add("@RptwizName183", OdbcType.NVarChar).Value = row["RptwizName183"].ToString().Trim(); } else { cmd.Parameters.Add("@RptwizName183", OdbcType.VarChar).Value = row["RptwizName183"].ToString().Trim(); }
            if (Config.DoubleByteDb) { cmd.Parameters.Add("@RptwizDesc183", OdbcType.NVarChar).Value = row["RptwizDesc183"].ToString().Trim(); } else { cmd.Parameters.Add("@RptwizDesc183", OdbcType.VarChar).Value = row["RptwizDesc183"].ToString().Trim(); }
            if (row["ReportId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ReportId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ReportId183", OdbcType.Numeric).Value = row["ReportId183"].ToString().Trim();
            }
            cmd.Parameters.Add("@AccessCd183", OdbcType.Char).Value = row["AccessCd183"].ToString().Trim();
            cmd.Parameters.Add("@UsrId183", OdbcType.Numeric).Value = row["UsrId183"].ToString().Trim();
            cmd.Parameters.Add("@TemplateName183", OdbcType.VarChar).Value = row["TemplateName183"].ToString().Trim();
            cmd.Parameters.Add("@OrientationCd183", OdbcType.Char).Value = row["OrientationCd183"].ToString().Trim();
            cmd.Parameters.Add("@UnitCd183", OdbcType.Char).Value = row["UnitCd183"].ToString().Trim();
            cmd.Parameters.Add("@TopMargin183", OdbcType.Decimal).Value = row["TopMargin183"].ToString().Trim();
            cmd.Parameters.Add("@BottomMargin183", OdbcType.Decimal).Value = row["BottomMargin183"].ToString().Trim();
            cmd.Parameters.Add("@LeftMargin183", OdbcType.Decimal).Value = row["LeftMargin183"].ToString().Trim();
            cmd.Parameters.Add("@RightMargin183", OdbcType.Decimal).Value = row["RightMargin183"].ToString().Trim();
            cmd.Parameters.Add("@RptwizTypeCd183", OdbcType.Char).Value = row["RptwizTypeCd183"].ToString().Trim();
            if (row["RptwizCatId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptwizCatId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizCatId183", OdbcType.Numeric).Value = row["RptwizCatId183"].ToString().Trim();
            }
            if (row["RptChaTypeCd183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptChaTypeCd183", OdbcType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptChaTypeCd183", OdbcType.Char).Value = row["RptChaTypeCd183"].ToString().Trim();
            }
            cmd.Parameters.Add("@ThreeD183", OdbcType.Char).Value = row["ThreeD183"].ToString().Trim();
            if (row["GMinValue183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMinValue183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMinValue183", OdbcType.Numeric).Value = row["GMinValue183"].ToString().Trim();
            }
            if (row["GLowRange183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GLowRange183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GLowRange183", OdbcType.Numeric).Value = row["GLowRange183"].ToString().Trim();
            }
            if (row["GMidRange183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMidRange183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMidRange183", OdbcType.Numeric).Value = row["GMidRange183"].ToString().Trim();
            }
            if (row["GMaxValue183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMaxValue183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMaxValue183", OdbcType.Numeric).Value = row["GMaxValue183"].ToString().Trim();
            }
            if (row["GNeedle183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GNeedle183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GNeedle183", OdbcType.Numeric).Value = row["GNeedle183"].ToString().Trim();
            }
            if (row["GMinValueId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMinValueId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMinValueId183", OdbcType.Numeric).Value = row["GMinValueId183"].ToString().Trim();
            }
            if (row["GLowRangeId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GLowRangeId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GLowRangeId183", OdbcType.Numeric).Value = row["GLowRangeId183"].ToString().Trim();
            }
            if (row["GMidRangeId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMidRangeId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMidRangeId183", OdbcType.Numeric).Value = row["GMidRangeId183"].ToString().Trim();
            }
            if (row["GMaxValueId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMaxValueId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMaxValueId183", OdbcType.Numeric).Value = row["GMaxValueId183"].ToString().Trim();
            }
            if (row["GNeedleId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GNeedleId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GNeedleId183", OdbcType.Numeric).Value = row["GNeedleId183"].ToString().Trim();
            }
            if (row["GPositive183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GPositive183", OdbcType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GPositive183", OdbcType.Char).Value = row["GPositive183"].ToString().Trim();
            }
            if (row["GFormat183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GFormat183", OdbcType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GFormat183", OdbcType.Char).Value = row["GFormat183"].ToString().Trim();
            }
            try
            {
                da.SelectCommand = TransformCmd(cmd);
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

        public override bool DelAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("SET NOCOUNT ON"
            + " DECLARE @RptwizId183 numeric(10,0) SELECT @RptwizId183=?"
            + " DELETE dbo.RptwizDtl"
            + " WHERE RptwizId = @RptwizId183"
            + " DELETE dbo.Rptwiz"
            + " WHERE RptwizId = @RptwizId183", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            row = ds.Tables["AdmRptWiz"].Rows[0];
            cmd.Parameters.Add("@rptwizId183", OdbcType.Numeric).Value = row["RptwizId183"];
            try
            {
                TransformCmd(cmd).ExecuteNonQuery();
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

        public override bool UpdAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("SET NOCOUNT ON"
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
                cmd.Parameters.Add("@RptwizId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizId183", OdbcType.Numeric).Value = row["RptwizId183"].ToString().Trim();
            }
            if (Config.DoubleByteDb) { cmd.Parameters.Add("@RptwizName183", OdbcType.NVarChar).Value = row["RptwizName183"].ToString().Trim(); } else { cmd.Parameters.Add("@RptwizName183", OdbcType.VarChar).Value = row["RptwizName183"].ToString().Trim(); }
            if (Config.DoubleByteDb) { cmd.Parameters.Add("@RptwizDesc183", OdbcType.NVarChar).Value = row["RptwizDesc183"].ToString().Trim(); } else { cmd.Parameters.Add("@RptwizDesc183", OdbcType.VarChar).Value = row["RptwizDesc183"].ToString().Trim(); }
            if (row["ReportId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ReportId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ReportId183", OdbcType.Numeric).Value = row["ReportId183"].ToString().Trim();
            }
            cmd.Parameters.Add("@AccessCd183", OdbcType.Char).Value = row["AccessCd183"].ToString().Trim();
            if (row["UsrId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@UsrId183", OdbcType.Numeric).Value = LUser.UsrId;
            }
            else
            {
                cmd.Parameters.Add("@UsrId183", OdbcType.Numeric).Value = row["UsrId183"].ToString().Trim();
            }
            cmd.Parameters.Add("@TtemplateName183", OdbcType.VarChar).Value = row["TemplateName183"].ToString().Trim();
            cmd.Parameters.Add("@OrientationCd183", OdbcType.Char).Value = row["OrientationCd183"].ToString().Trim();
            cmd.Parameters.Add("@UnitCd183", OdbcType.Char).Value = row["UnitCd183"].ToString().Trim();
            cmd.Parameters.Add("@TopMargin183", OdbcType.Decimal).Value = row["TopMargin183"].ToString().Trim();
            cmd.Parameters.Add("@BottomMargin183", OdbcType.Decimal).Value = row["BottomMargin183"].ToString().Trim();
            cmd.Parameters.Add("@LeftMargin183", OdbcType.Decimal).Value = row["LeftMargin183"].ToString().Trim();
            cmd.Parameters.Add("@RightMargin183", OdbcType.Decimal).Value = row["RightMargin183"].ToString().Trim();
            cmd.Parameters.Add("@RptwizTypeCd183", OdbcType.Char).Value = row["RptwizTypeCd183"].ToString().Trim();
            if (row["RptwizCatId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptwizCatId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizCatId183", OdbcType.Numeric).Value = row["RptwizCatId183"].ToString().Trim();
            }
            if (row["RptChaTypeCd183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptChaTypeCd183", OdbcType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptChaTypeCd183", OdbcType.Char).Value = row["RptChaTypeCd183"].ToString().Trim();
            }
            cmd.Parameters.Add("@ThreeD183", OdbcType.Char).Value = row["ThreeD183"].ToString().Trim();
            if (row["GMinValue183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMinValue183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMinValue183", OdbcType.Numeric).Value = row["GMinValue183"].ToString().Trim();
            }
            if (row["GLowRange183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GLowRange183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GLowRange183", OdbcType.Numeric).Value = row["GLowRange183"].ToString().Trim();
            }
            if (row["GMidRange183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMidRange183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMidRange183", OdbcType.Numeric).Value = row["GMidRange183"].ToString().Trim();
            }
            if (row["GMaxValue183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMaxValue183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMaxValue183", OdbcType.Numeric).Value = row["GMaxValue183"].ToString().Trim();
            }
            if (row["GNeedle183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GNeedle183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GNeedle183", OdbcType.Numeric).Value = row["GNeedle183"].ToString().Trim();
            }
            if (row["GMinValueId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMinValueId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMinValueId183", OdbcType.Numeric).Value = row["GMinValueId183"].ToString().Trim();
            }
            if (row["GLowRangeId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GLowRangeId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GLowRangeId183", OdbcType.Numeric).Value = row["GLowRangeId183"].ToString().Trim();
            }
            if (row["GMidRangeId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMidRangeId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMidRangeId183", OdbcType.Numeric).Value = row["GMidRangeId183"].ToString().Trim();
            }
            if (row["GMaxValueId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GMaxValueId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GMaxValueId183", OdbcType.Numeric).Value = row["GMaxValueId183"].ToString().Trim();
            }
            if (row["GNeedleId183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GNeedleId183", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GNeedleId183", OdbcType.Numeric).Value = row["GNeedleId183"].ToString().Trim();
            }
            if (row["GPositive183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GPositive183", OdbcType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GPositive183", OdbcType.Char).Value = row["GPositive183"].ToString().Trim();
            }
            if (row["GFormat183"].ToString().Trim() == string.Empty)
            {
                cmd.Parameters.Add("@GFormat183", OdbcType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@GFormat183", OdbcType.Char).Value = row["GFormat183"].ToString().Trim();
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

        private Int32 AddAdmRptWiz95Dt(string RptwizId183, string RptwizDtlId184, string ColumnId184, string ColHeader184, string CriOperName184, string CriSelect184, string CriHeader184, string ColSelect184, string ColGroup184, string ColSort184, string AggregateCd184, string RptChartCd184, OdbcConnection cn, OdbcTransaction tr)
        {
            OdbcCommand cmd = new OdbcCommand("AddAdmRptWiz95Dt", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RptwizId", OdbcType.Numeric).Value = RptwizId183;
            if (RptwizDtlId184 == string.Empty)
            {
                cmd.Parameters.Add("@RptwizDtlId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizDtlId", OdbcType.Numeric).Value = RptwizDtlId184;
            }
            cmd.Parameters.Add("@ColumnId", OdbcType.Numeric).Value = ColumnId184.Trim();
            if (ColHeader184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ColHeader", OdbcType.NVarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColHeader", OdbcType.NVarChar).Value = ColHeader184.Trim();
            }
            if (CriOperName184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@CriOperName", OdbcType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@CriOperName", OdbcType.VarChar).Value = CriOperName184.Trim();
            }
            if (CriSelect184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@CriSelect", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@CriSelect", OdbcType.Numeric).Value = CriSelect184.Trim();
            }
            if (CriHeader184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@CriHeader", OdbcType.NVarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@CriHeader", OdbcType.NVarChar).Value = CriHeader184.Trim();
            }
            if (ColSelect184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ColSelect", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColSelect", OdbcType.Numeric).Value = ColSelect184.Trim();
            }
            if (ColGroup184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ColGroup", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColGroup", OdbcType.Numeric).Value = ColGroup184.Trim();
            }
            if (ColSort184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@ColSort", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColSort", OdbcType.Numeric).Value = ColSort184.Trim();
            }
            if (AggregateCd184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@AggregateCd", OdbcType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@AggregateCd", OdbcType.Char).Value = AggregateCd184.Trim();
            }
            if (RptChartCd184.Trim() == string.Empty)
            {
                cmd.Parameters.Add("@RptChartCd", OdbcType.Char).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptChartCd", OdbcType.Char).Value = RptChartCd184.Trim();
            }
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Int32 id = Int32.Parse(dt.Rows[0][0].ToString());
            cmd.Dispose();
            cmd = null;
            return id;
        }

        private void DelAdmRptWiz95Dt(string RptwizDtlId184, OdbcConnection cn, OdbcTransaction tr)
        {
            OdbcCommand cmd = new OdbcCommand("DelAdmRptWiz95Dt", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RptwizDtlId", OdbcType.Numeric).Value = RptwizDtlId184;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            TransformCmd(cmd).ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            return;
        }

        protected void Cr_ChkRptwizCri(string RptwizId, OdbcConnection cn, OdbcTransaction tr)
        {
            OdbcCommand cmd = new OdbcCommand("Cr_ChkRptwizCri", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (RptwizId == string.Empty)
            {
                cmd.Parameters.Add("@RptwizId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@RptwizId", OdbcType.Numeric).Value = RptwizId;
            }
            cmd.Transaction = tr;
            cmd.CommandTimeout = 1800;
            TransformCmd(cmd).ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            return;
        }

        public override void RmTranslatedLbl(string LabelId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("SET NOCOUNT ON"
            + " DECLARE @LabelCat varchar(50), @LabelKey varchar(50)"
            + " SELECT @LabelCat = LabelCat, @LabelKey = LabelKey FROM dbo.Label WHERE LabelId = ?"
            + " DELETE FROM dbo.Label WHERE LabelCat = @LabelCat AND LabelKey = @LabelKey AND CultureId <> 1", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.Add("@LabelId", OdbcType.Int).Value = LabelId;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "RmTranslatedLbl", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public override DataTable WrAddMenu(string MenuIndex, string ParentId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrAddMenu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuIndex", OdbcType.SmallInt).Value = MenuIndex;
            cmd.Parameters.Add("@ParentId", OdbcType.Int).Value = string.IsNullOrEmpty(ParentId.Trim()) ? System.DBNull.Value : (object)ParentId.Trim();
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = TransformCmd(cmd);
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

        public override bool WrDelMenu(string MenuId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrDelMenu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuId", OdbcType.Numeric).Value = MenuId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            bool done = false;
            try { TransformCmd(cmd).ExecuteNonQuery(); tr.Commit(); done = true; }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrDelMenu", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return done;
        }

        public override void WrUpdMenu(string MenuId, string PMenuId, string ParentId, string MenuText, string CultureId, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrUpdMenu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuId", OdbcType.Int).Value = MenuId;
            cmd.Parameters.Add("@PMenuId", OdbcType.Int).Value = string.IsNullOrEmpty(PMenuId.Trim()) ? System.DBNull.Value : (object)PMenuId.Trim();
            cmd.Parameters.Add("@MenuText", OdbcType.NVarChar).Value = MenuText;
            cmd.Parameters.Add("@ParentId", OdbcType.Int).Value = string.IsNullOrEmpty(ParentId.Trim()) ? System.DBNull.Value : (object)ParentId.Trim();
            cmd.Parameters.Add("@CultureId", OdbcType.SmallInt).Value = CultureId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            try { TransformCmd(cmd).ExecuteNonQuery(); tr.Commit(); }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrUpdMenu", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public override DataTable WrAddScreenTab(string TabFolderOrder, string ScreenId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrAddScreenTab", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TabFolderOrder", OdbcType.SmallInt).Value = TabFolderOrder;
            cmd.Parameters.Add("@ScreenId", OdbcType.Int).Value = string.IsNullOrEmpty(ScreenId.Trim()) ? System.DBNull.Value : (object)ScreenId.Trim(); ;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
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

        public override bool WrDelScreenTab(string ScreenTabId, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("WrDelScreenTab", cn);
            OdbcTransaction tr = cn.BeginTransaction();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenTabId", OdbcType.Numeric).Value = ScreenTabId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            bool done = false;
            try { TransformCmd(cmd).ExecuteNonQuery(); tr.Commit(); done = true; }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrDelScreenTab", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return done;
        }

        public override void WrUpdScreenTab(string ScreenTabId, string TabFolderOrder, string TabFolderName, string CultureId, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrUpdScreenTab", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenTabId", OdbcType.Int).Value = ScreenTabId;
            cmd.Parameters.Add("@TabFolderOrder", OdbcType.SmallInt).Value = TabFolderOrder;
            cmd.Parameters.Add("@TabFolderName", OdbcType.NVarChar).Value = TabFolderName;
            cmd.Parameters.Add("@CultureId", OdbcType.SmallInt).Value = CultureId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            try { TransformCmd(cmd).ExecuteNonQuery(); tr.Commit(); }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrUpdScreenTab", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public override DataTable WrAddScreenObj(string ScreenId, string PScreenObjId, string TabFolderId, bool IsTab, bool NewRow, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("WrAddScreenObj", cn);
            OdbcTransaction tr = cn.BeginTransaction();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OdbcType.Int).Value = string.IsNullOrEmpty(ScreenId) ? System.DBNull.Value : (object)ScreenId.Trim();
            cmd.Parameters.Add("@PScreenObjId", OdbcType.Int).Value = string.IsNullOrEmpty(PScreenObjId.Trim()) ? System.DBNull.Value : (object)PScreenObjId.Trim();
            cmd.Parameters.Add("@TabFolderId", OdbcType.Int).Value = string.IsNullOrEmpty(TabFolderId.Trim()) ? System.DBNull.Value : (object)TabFolderId.Trim();
            cmd.Parameters.Add("@IsTab", OdbcType.Char).Value = IsTab ? 'Y' : 'N';
            cmd.Parameters.Add("@NewRow", OdbcType.Char).Value = NewRow ? 'Y' : 'N';
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = TransformCmd(cmd);
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

        public override bool WrDelScreenObj(string ScreenObjId, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrDelScreenObj", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenObjId", OdbcType.Numeric).Value = ScreenObjId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = TransformCmd(cmd);
            bool done = false;
            try { TransformCmd(cmd).ExecuteNonQuery(); tr.Commit(); done = true; }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrDelScreenObj", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return done;
        }

        public override void WrUpdScreenObj(string ScreenObjId, string PScreenObjId, string TabFolderId, string ColumnHeader, string CultureId, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrUpdScreenObj", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenObjId", OdbcType.Int).Value = ScreenObjId;
            cmd.Parameters.Add("@PScreenObjId", OdbcType.Int).Value = string.IsNullOrEmpty(PScreenObjId.Trim()) ? System.DBNull.Value : (object)PScreenObjId.Trim();
            cmd.Parameters.Add("@ColumnHeader", OdbcType.NVarChar).Value = string.IsNullOrEmpty(ColumnHeader.Trim()) ? System.DBNull.Value : (object)ColumnHeader.Trim(); ;
            cmd.Parameters.Add("@TabFolderId", OdbcType.Int).Value = string.IsNullOrEmpty(TabFolderId.Trim()) ? System.DBNull.Value : (object)TabFolderId.Trim();
            cmd.Parameters.Add("@CultureId", OdbcType.SmallInt).Value = CultureId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            try { TransformCmd(cmd).ExecuteNonQuery(); tr.Commit(); }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrUpdScreenObj", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public override string WrGetScreenId(string ProgramName, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrGetScreenId", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProgramName", OdbcType.VarChar).Value = ProgramName;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
        }

        public override string WrGetMasterTable(string ScreenId, string ColumnId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrGetMasterTable", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OdbcType.Int).Value = ScreenId;
            if (ColumnId == string.Empty)
            {
                cmd.Parameters.Add("@ColumnId", OdbcType.Int).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ColumnId", OdbcType.Int).Value = ColumnId;
            }
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
        }

        public override DataTable WrGetScreenObj(string ScreenId, Int16 CultureId, string ScreenObjId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrGetScreenObj", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OdbcType.Int).Value = !string.IsNullOrEmpty(ScreenId) ? (object)ScreenId : (object)System.DBNull.Value; ;
            cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
            cmd.Parameters.Add("@ScreenObjId", OdbcType.Int).Value = !string.IsNullOrEmpty(ScreenObjId) ? (object)ScreenObjId : (object)System.DBNull.Value;
            cmd.CommandTimeout = 1800;

            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override string WrCloneScreen(string ScreenId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrCloneScreen", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OdbcType.Int).Value = ScreenId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
        }

        public override string WrCloneReport(string ReportId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("WrCloneReport", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReportId", OdbcType.Int).Value = ReportId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1) { return dt.Rows[0][0].ToString(); } else { return string.Empty; }
        }

        public override void PurgeScrAudit(Int16 YearOld, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("PurgeScrAudit", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@YearOld", OdbcType.SmallInt).Value = YearOld;
            cmd.CommandTimeout = 1800;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "PurgeScrAudit", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public override void WrUpdScreenReactGen(string ScreenId, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrUpdScreenReactGen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OdbcType.Int).Value = ScreenId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            try { TransformCmd(cmd).ExecuteNonQuery(); tr.Commit(); }
            catch (Exception e) { tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrUpdScreenReactGen", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public override DataTable WrGetWebRule(string ScreenId, string dbConnectionString, string dbPassword)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcCommand cmd = new OdbcCommand("WrGetWebRule", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OdbcType.VarChar).Value = ScreenId;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}