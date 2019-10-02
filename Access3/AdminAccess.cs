namespace RO.Access3
{
	using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data;
	using System.Data.OleDb;
	using System.Drawing;
	using System.Text;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class AdminAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;
	
		public AdminAccess()
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

        // For screens:

        public string GetMaintMsg()
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetMaintMsg", new OleDbConnection(GetDesConnStr()));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            ApplicationAssert.CheckCondition(dt.Rows.Count <= 1, "GetMaintMsg", "Maintenance Message", "Cannot obtain maintenance message. Please contact systems adminstrator ASAP.");
            return dt.Rows[0][0].ToString();
        }

        public DataTable GetHomeTabs(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetHomeTabs", new OleDbConnection(GetDesConnStr()));
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
            cmd.Parameters.Add("@CompanyId", OleDbType.Numeric).Value = CompanyId;
            cmd.Parameters.Add("@ProjectId", OleDbType.Numeric).Value = ProjectId;
            cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public string SetCult(int UsrId, Int16 CultureId)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("SetCult", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			if (dt != null && dt.Rows.Count > 0) { rtn = dt.Rows[0][0].ToString(); }
			return rtn;
		}

		public byte GetCult(string lang)
		{
			byte rtn = 1;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetCult", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@lang", OleDbType.VarChar).Value = lang;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			if (dt != null && dt.Rows.Count > 0) { rtn = byte.Parse(dt.Rows[0][0].ToString()); }
			return rtn;
		}

		public DataTable GetLang(Int16 CultureId)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetLang", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetLastPageInfo(Int32 screenId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetLastPageInfo", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void UpdLastPageInfo(Int32 screenId, Int32 usrId, string lastPageInfo, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("UpdLastPageInfo", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
			if (Config.DoubleByteDb) {cmd.Parameters.Add("@LastPageInfo", OleDbType.VarWChar).Value = lastPageInfo;} else {cmd.Parameters.Add("@LastPageInfo", OleDbType.VarChar).Value = lastPageInfo;}
			cmd.CommandTimeout = 1800;
			try {cmd.ExecuteNonQuery();}
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "UpdLastPageInfo", "", e.Message.ToString()); }
			finally {cn.Close(); cmd.Dispose(); cmd = null;}
			return;
		}

        public DataTable GetLastCriteria(Int32 screenId, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetLastCriteria", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void DelLastCriteria(Int32 screenId, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("DelLastCriteria", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = screenId;
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

        public void IniLastCriteria(Int32 screenId, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("IniLastCriteria", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = screenId;
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

        public void DelDshFldDtl(string DshFldDtlId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("DelDshFldDtl", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DshFldDtlId", OleDbType.Numeric).Value = DshFldDtlId;
            cmd.CommandTimeout = 1800;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); }
            cmd.Dispose();
            cmd = null;
            return;
        }

        public void DelDshFld(string DshFldId, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("DelDshFld", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DshFldId", OleDbType.Numeric).Value = DshFldId;
            cmd.CommandTimeout = 1800;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); }
            cmd.Dispose();
            cmd = null;
            return;
        }

        public string UpdDshFld(string PublicAccess, string DshFldId, string DshFldName, Int32 usrId, string dbConnectionString, string dbPassword)
        {
            string rtn = string.Empty;
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("UpdDshFld", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@PublicAccess", OleDbType.Char).Value = PublicAccess;
            if (DshFldId == string.Empty)
            {
                cmd.Parameters.Add("@DshFldId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@DshFldId", OleDbType.Numeric).Value = DshFldId;
            }
            cmd.Parameters.Add("@DshFldName", OleDbType.VarWChar).Value = DshFldName;
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

		public string GetSchemaScrImp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetSchemaScrImp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			StringBuilder sb = new StringBuilder();
			foreach(DataRow dr in dt.Rows){ sb.Append(dr[0].ToString()); }
			return sb.ToString();
		}

        public string GetScrImpTmpl(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetScrImpTmpl", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows) { sb.Append(dr[0].ToString()); }
            return sb.ToString();
        }

		public DataTable GetButtonHlp(Int32 screenId, Int32 reportId, Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetButtonHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@WizardId", OleDbType.Numeric).Value = wizardId;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetClientRule(Int32 screenId, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetClientRule", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetScreenHlp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = null;
			if (string.IsNullOrEmpty(dbConnectionString))
			{
				cmd = new OleDbCommand("GetScreenHlp", new OleDbConnection(GetDesConnStr()));
			}
			else
			{
				cmd = new OleDbCommand("GetScreenHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenHlp", "Screen Issue", "Default help message not available for Screen #'" + screenId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
			return dt;
		}

		public DataTable GetGlobalFilter(Int32 usrId, Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetGlobalFilter", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = usrId;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetScreenFilter(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetScreenFilter", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetScreenTab(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetScreenTab", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetScreenTab", "Screen Issue", "Tab Folder Names not available for Screen #'" + screenId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
			return dt;
		}

		public DataTable GetScreenCriHlp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			if ( da == null ) { throw new System.ObjectDisposedException( GetType().FullName ); }            
			OleDbCommand cmd = new OleDbCommand("GetScreenCriHlp",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
            try { da.Fill(dt); }
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cmd.Dispose(); cmd = null;}
//			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetScreenCriHlp", "Screen Issue", "Criteria Column Headers not available for Screen #'" + screenId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
			return dt;
		}

		public void LogUsage(Int32 UsrId, string UsageNote, string EntityTitle, Int32 ScreenId, Int32 ReportId, Int32 WizardId, string Miscellaneous, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("LogUsage", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
			if (string.IsNullOrEmpty(UsageNote))
			{
				cmd.Parameters.Add("@UsageNote", OleDbType.VarChar).Value = System.DBNull.Value;
			}
			else
			{
				if (Config.DoubleByteDb) {cmd.Parameters.Add("@UsageNote", OleDbType.VarWChar).Value = UsageNote;} 
				else {cmd.Parameters.Add("@UsageNote", OleDbType.VarChar).Value = UsageNote;}
			}
			if (Config.DoubleByteDb) {cmd.Parameters.Add("@EntityTitle", OleDbType.VarWChar).Value = EntityTitle;} 
			else {cmd.Parameters.Add("@EntityTitle", OleDbType.VarChar).Value = EntityTitle;}
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
			cmd.Parameters.Add("@WizardId", OleDbType.Numeric).Value = WizardId;
			if (string.IsNullOrEmpty(Miscellaneous))
			{
				cmd.Parameters.Add("@Miscellaneous", OleDbType.VarChar).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@Miscellaneous", OleDbType.VarChar).Value = Miscellaneous;
			}
			cmd.CommandTimeout = 1800;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close(); cmd.Dispose(); cmd = null;}
			return;
		}

		public DataTable GetInfoByCol(Int32 ScreenId, string ColumnName, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetInfoByCol", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@ColumnName", OleDbType.VarChar).Value = ColumnName;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetInfoByCol", "Column not available", "Column '" + ColumnName + "' is not defined for Screen #'" + ScreenId.ToString() + "!");
			return dt;
		}

		public bool IsValidOvride(Credential cr, Int32 usrId)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("IsValidOvride", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@OvrideId", OleDbType.Numeric).Value = Int32.Parse(cr.LoginName);
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
			cmd.Parameters.Add("@UsrPassword", OleDbType.VarBinary).Value = cr.Password;
			int rtn = Convert.ToInt32(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} else {return true;}
		}

		public DataTable GetMsg(int MsgId, Int16 CultureId, string dbConnectionString, string dbPassword)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = null;
			if (string.IsNullOrEmpty(dbConnectionString))
			{
				cmd = new OleDbCommand("GetMsg", new OleDbConnection(GetDesConnStr()));
			}
			else
			{
				cmd = new OleDbCommand("GetMsg", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@MsgId", OleDbType.Numeric).Value = MsgId;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}
        public DataTable GetCronJob(int? jobId, string jobLink, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = null;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetCronJob", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetCronJob", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            if (jobId == null)
                cmd.Parameters.Add("@jobId", OleDbType.VarChar).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@jobId", OleDbType.VarChar).Value = jobId;
            if (string.IsNullOrEmpty(jobLink))
                cmd.Parameters.Add("@jobLink", OleDbType.VarChar).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@jobLink", OleDbType.VarChar).Value = jobLink;

            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void UpdCronJob(int jobId, DateTime? lastRun, DateTime? nextRun, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("UpdCronJob", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@jobId", OleDbType.Numeric).Value = jobId;
            cmd.Parameters.Add("@lastRun", GetOleDbType("datetime")).Value = lastRun;
            cmd.Parameters.Add("@nextRun", GetOleDbType("datetime")).Value = nextRun;
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
        }
        public void UpdCronJobStatus(int jobId, string Error, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("UpdCronJobStatus", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@jobId", OleDbType.Numeric).Value = jobId;
            cmd.Parameters.Add("@err", OleDbType.VarWChar).Value = Error.ToString();
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
        }

		// Obtain translated label one at a time from the table "Label" on system dependent database.
        public string GetLabel(Int16 CultureId, string LabelCat, string LabelKey, string CompanyId, string dbConnectionString, string dbPassword)
		{
			string rtn = string.Empty;
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn;
			if (string.IsNullOrEmpty(dbConnectionString))
			{
				cn = new OleDbConnection(GetDesConnStr());
			}
			else
			{
				cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			}
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetLabel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId.ToString();
            cmd.Parameters.Add("@LabelCat", OleDbType.VarChar).Value = LabelCat;
            cmd.Parameters.Add("@LabelKey", OleDbType.VarChar).Value = LabelKey;
			if (string.IsNullOrEmpty(CompanyId))
			{
				cmd.Parameters.Add("@CompanyId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@CompanyId", OleDbType.Numeric).Value = CompanyId;
			}
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			try { da.Fill(dt); rtn = dt.Rows[0][0].ToString(); }
			catch { }
			finally { cn.Close(); cmd.Dispose(); cmd = null; }
			return rtn;
		}

		// Obtain translated labels as one category from the table "Label" on system dependent database.
		public DataTable GetLabels(Int16 CultureId, string LabelCat, string CompanyId, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd;
			if (string.IsNullOrEmpty(dbConnectionString))
			{
				cmd = new OleDbCommand("GetLabels", new OleDbConnection(GetDesConnStr()));
			}
			else
			{
				cmd = new OleDbCommand("GetLabels", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId.ToString();
			cmd.Parameters.Add("@LabelCat", OleDbType.VarChar).Value = LabelCat;
			if (string.IsNullOrEmpty(CompanyId))
			{
				cmd.Parameters.Add("@CompanyId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@CompanyId", OleDbType.Numeric).Value = CompanyId;
			}
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

        public DataTable GetScrCriteria(string screenId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetScreenCriteria", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try { da.Fill(dt); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cmd.Dispose(); cmd = null; }
            //ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetScrCriteria", "Screen Criteria Issue", "Criteria for Screen #'" + screenId.ToString() + "' not available!");
            return dt;
        }

        public void MkGetScreenIn(string screenId, string screenCriId, string procedureName, string appDatabase, string sysDatabase, string multiDesignDb, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("MkGetScreenIn", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@screenCriId", OleDbType.Numeric).Value = screenCriId;
            cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
            cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
            cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
            cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public DataTable GetScreenIn(string screenId, string procedureName, int TotalCnt, string RequiredValid, int topN, string FilterTxt, bool bAll, string keyId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            if (string.IsNullOrEmpty(FilterTxt))
            {
                cmd.Parameters.Add("@filterTxt", OleDbType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                if (Config.DoubleByteDb) { cmd.Parameters.Add("@filterTxt", OleDbType.VarWChar).Value = FilterTxt; } else { cmd.Parameters.Add("@filterTxt", OleDbType.VarChar).Value = FilterTxt; }
            }
            cmd.Parameters.Add("@topN", OleDbType.Numeric).Value = topN;
            cmd.Parameters.Add("@bAll", OleDbType.Char).Value = bAll ? "Y" : "N";
            cmd.Parameters.Add("@keyId", OleDbType.VarChar).Value = string.IsNullOrEmpty(keyId) ? System.DBNull.Value : (object)keyId;
            da.SelectCommand = cmd;
            cmd.CommandTimeout = 1800;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (RequiredValid != "Y" && dt.Rows.Count >= TotalCnt) { dt.Rows.InsertAt(dt.NewRow(), 0); }
            return dt;
        }

        public int CountScrCri(string ScreenCriId, string MultiDesignDb, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("CountScrCri", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenCriId", OleDbType.Numeric).Value = ScreenCriId;
            cmd.Parameters.Add("@MultiDesignDb", OleDbType.Char).Value = MultiDesignDb;
            int rtn = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
           return rtn;
        }

        public void UpdScrCriteria(string screenId, string programName, DataView dvCri, Int32 usrId, bool isCriVisible, DataSet ds, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("Upd" + programName + "In", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = usrId;
            if (isCriVisible) { cmd.Parameters.Add("@isCriVisible", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@isCriVisible", OleDbType.Char).Value = "N"; }
            if (dvCri != null && ds != null)
            {
                DataRow dr = ds.Tables["DtScreenIn"].Rows[0];
                foreach (DataRowView drv in dvCri)
                {
                    if (drv["RequiredValid"].ToString() == "N" && string.IsNullOrEmpty(dr[drv["ColumnName"].ToString()].ToString().Trim()))
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
            }
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
            if (ds.HasErrors)
            {
                ds.Tables["DtScreenIn"].GetErrors()[0].ClearErrors();
            }
            else
            {
                ds.AcceptChanges();
            }
            return;
        }

		public DataTable GetAuthRow(Int32 ScreenId, string RowAuthoritys, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetAuthRow", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = RowAuthoritys;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetAuthRow", "Authorization Issue", "Authority levels have not been defined for Screen #'" + ScreenId.ToString() + "!");
            return dt;
		}

		public DataTable GetAuthCol(Int32 ScreenId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetAuthCol", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
            da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetAuthCol", "Authorization Issue", "Authority levels have not been defined for Screen #'" + ScreenId.ToString() + "!");
			return dt;
		}

		public DataTable GetAuthExp(Int32 ScreenId, Int16 CultureId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetAuthExp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetAuthExp", "Authorization Issue", "Authority levels have not been defined for Screen #'" + ScreenId.ToString() + "!");
			return dt;
		}

		public DataTable GetScreenLabel(Int32 ScreenId, Int16 CultureId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
            //if (!dbConnectionString.Contains("Design")) checkValidLicense();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetScreenLabel", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetScreenLabel", "Screen Issue", "Screen Column Headers not available for Screen #'" + ScreenId.ToString() + "' and Culture #'" + CultureId.ToString() + "'!");
			return dt;
		}

        public DataTable GetDdl(Int32 screenId, string procedureName, bool bAddNew, bool bAll, int topN, string keyId, string dbConnectionString, string dbPassword, string filterTxt, UsrImpr ui, UsrCurr uc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
            if (keyId == string.Empty)
            {
                cmd.Parameters.Add("@keyId", OleDbType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@keyId", OleDbType.VarChar).Value = keyId;
            }
            cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
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
            cmd.Parameters.Add("@topN", OleDbType.Numeric).Value = topN;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (bAddNew) { dt.Rows.InsertAt(dt.NewRow(), 0); }
            return dt;
        }

        public DataTable RunWrRule(int screenId, string procedureName, string dbConnectionString, string dbPassword, string parameterXML, UsrImpr ui, UsrCurr uc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }

            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            OleDbCommand cmd = new OleDbCommand(procedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            if (parameterXML == string.Empty)
            {
                cmd.Parameters.Add("@parameterXML", OleDbType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                if (Config.DoubleByteDb) { cmd.Parameters.Add("@parameterXML", OleDbType.VarWChar).Value = parameterXML; } else { cmd.Parameters.Add("@parameterXML", OleDbType.VarChar).Value = parameterXML; }
            }
            cmd.CommandTimeout = 3600;
            da.SelectCommand = cmd;

            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            try
            {
                cmd.Transaction = tr;
                DataSet ds = new DataSet();
                //dt.Load(cmd.ExecuteReader());
                da.Fill(ds);

                /*
                 * DO NOT USE DataAdapter Fill(DataTable) as error raised is not captured when the SP already return something(and the raiserror is done after that)
                 * , i.e. not behave as one expect, Fill(DataSet) which would correctly capture the error thus we can rollback
                da.Fill(dt); 
                */
                tr.Commit();
                if (ds.Tables.Count > 0) return ds.Tables[0]; else return new DataTable();
            }
            catch (Exception e)
            {
                tr.Rollback();
                throw new Exception(procedureName + ":" + e.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetExp(Int32 screenId, string procedureName, string useGlobalFilter, string dbConnectionString, string dbPassword, Int32 screenFilterId, DataView dvCri, UsrImpr ui, UsrCurr uc, DataSet ds)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@useGlobalFilter", OleDbType.VarChar).Value = useGlobalFilter;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@key", OleDbType.VarWChar).Value = System.DBNull.Value;
            cmd.Parameters.Add("@FilterTxt", OleDbType.VarWChar).Value = System.DBNull.Value;
            if (dvCri != null && ds != null)
            {
                DataRow dr = ds.Tables["DtScreenIn"].Rows[0];
                foreach (DataRowView drv in dvCri)
                {
                    if (drv["RequiredValid"].ToString() == "N" && string.IsNullOrEmpty(dr[drv["ColumnName"].ToString()].ToString().Trim()))
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
            }
            cmd.Parameters.Add("@screenFilterId", OleDbType.Numeric).Value = screenFilterId;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetLis(Int32 screenId, string procedureName, bool bAddNew, string useGlobalFilter, int topN, string dbConnectionString, string dbPassword, Int32 screenFilterId, string key, string filterTxt, DataView dvCri, UsrImpr ui, UsrCurr uc, DataSet ds)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@useGlobalFilter", OleDbType.VarChar).Value = useGlobalFilter;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            if (string.IsNullOrEmpty(key)) { cmd.Parameters.Add("@key", OleDbType.VarWChar).Value = System.DBNull.Value; } else { cmd.Parameters.Add("@key", OleDbType.VarWChar).Value = key; }
            if (string.IsNullOrEmpty(filterTxt)) { cmd.Parameters.Add("@filterTxt", OleDbType.VarWChar).Value = System.DBNull.Value; } else { cmd.Parameters.Add("@filterTxt", OleDbType.VarWChar).Value = filterTxt; }
            if (dvCri != null && ds != null)
            {
                DataRow dr = ds.Tables["DtScreenIn"].Rows[0];
                foreach (DataRowView drv in dvCri)
                {
                    if (drv["RequiredValid"].ToString() == "N" && string.IsNullOrEmpty(dr[drv["ColumnName"].ToString()].ToString().Trim()) || (drv["DisplayName"].ToString() == "Rating" && dr[drv["ColumnName"].ToString()].ToString() == "0"))
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
            }
            cmd.Parameters.Add("@screenFilterId", OleDbType.Numeric).Value = screenFilterId;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.Parameters.Add("@topN", OleDbType.Numeric).Value = topN;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (bAddNew) { dt.Rows.InsertAt(dt.NewRow(), 0); }
            return dt;
        }

        public DataTable GetMstById(string procedureName, string keyId1, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            if (string.IsNullOrEmpty(keyId1)) { cmd.Parameters.Add("@keyId1", OleDbType.VarWChar).Value = System.DBNull.Value; } else { cmd.Parameters.Add("@keyId1", OleDbType.VarWChar).Value = keyId1; }
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        /* Albeit rare, this overload shall take care of more than one column as primary key */
        public DataTable GetMstById(string procedureName, string keyId1, string keyId2, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            if (string.IsNullOrEmpty(keyId1)) { cmd.Parameters.Add("@keyId1", OleDbType.VarWChar).Value = System.DBNull.Value; } else { cmd.Parameters.Add("@keyId1", OleDbType.VarWChar).Value = keyId1; }
            if (string.IsNullOrEmpty(keyId2)) { cmd.Parameters.Add("@keyId2", OleDbType.VarWChar).Value = System.DBNull.Value; } else { cmd.Parameters.Add("@keyId2", OleDbType.VarWChar).Value = keyId2; }
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetDtlById(Int32 screenId, string procedureName, string keyId, string dbConnectionString, string dbPassword, Int32 screenFilterId, UsrImpr ui, UsrCurr uc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            if (string.IsNullOrEmpty(keyId))
            {
                cmd.Parameters.Add("@keyId", OleDbType.VarWChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@keyId", OleDbType.VarWChar).Value = keyId;
            }
            cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
            cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
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
            cmd.Parameters.Add("@Borrowers", OleDbType.VarChar).Value = ui.Borrowers;
            cmd.Parameters.Add("@Guarantors", OleDbType.VarChar).Value = ui.Guarantors;
            cmd.Parameters.Add("@Lenders", OleDbType.VarChar).Value = ui.Lenders;
            cmd.Parameters.Add("@screenFilterId", OleDbType.Numeric).Value = screenFilterId;
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private OleDbType GetOleDbType(string ColType)
        {
            OleDbType otp;
            switch (ColType.ToLower())
            {
                case "numeric": case "tinyint": case "smallint": case "int": case "bigint":
                    otp = OleDbType.Numeric; break;
                case "single": case "real":
                    otp = OleDbType.Single; break;
                case "double": case "float":
                    otp = OleDbType.Double; break;
                case "currency": case "money":
                    otp = OleDbType.Currency; break;
                case "binary": case "image":
                    otp = OleDbType.Binary; break;
                case "varbinary":
                    otp = OleDbType.VarBinary; break;
                case "wchar": case "nchar":
                    otp = OleDbType.WChar; break;
                case "varwchar": case "nvarchar": case "ntext":
                    otp = OleDbType.VarWChar; break;
                case "dbtimestamp": case "datetime": case "smalldatetime":
                    otp = OleDbType.DBTimeStamp; break;
                case "char":
                    otp = OleDbType.Char; break;
                case "varchar": case "text":
                    otp = OleDbType.VarChar; break;
                case "decimal":
                    otp = OleDbType.Decimal; break;
                case "dbdate": case "date":
                    otp = OleDbType.DBDate; break;
                default: throw new Exception("Non-anticipated OleDbType: " + ColType + ".  Please contact admnistrator ASAP.");
            }
            return otp;
        }

        private string GetCallParam(string callp, LoginUsr LUser, UsrImpr LImpr, UsrCurr LCurr, DataRow row)
        {
            string rtn = string.Empty;
            switch (callp.ToLower())
            {
                case "luser.loginname": rtn = LUser.LoginName.ToString(); break;
                case "luser.usrid": rtn = LUser.UsrId.ToString(); break;
                case "luser.usrname": rtn = LUser.UsrName.ToString(); break;
                case "luser.usremail": rtn = LUser.UsrEmail.ToString(); break;
                case "luser.internalusr": rtn = LUser.InternalUsr.ToString(); break;
                case "luser.technicalusr": rtn = LUser.TechnicalUsr.ToString(); break;
                case "luser.cultureid": rtn = LUser.CultureId.ToString(); break;
                case "luser.culture": rtn = LUser.Culture.ToString(); break;
                case "luser.defsystemid": rtn = LUser.DefSystemId.ToString(); break;
                case "luser.defprojectid": rtn = LUser.DefProjectId.ToString(); break;
                case "luser.defcompanyid": rtn = LUser.DefCompanyId.ToString(); break;
                case "luser.pwdchgdt": rtn = LUser.PwdChgDt.ToString(); break;
                case "luser.pwdduration": rtn = LUser.PwdDuration.ToString(); break;
                case "luser.pwdwarn": rtn = LUser.PwdWarn.ToString(); break;

                case "lcurr.companyid": rtn = LCurr.CompanyId.ToString(); break;
                case "lcurr.projectid": rtn = LCurr.ProjectId.ToString(); break;
                case "lcurr.systemid": rtn = LCurr.SystemId.ToString(); break;
                case "lcurr.dbid": rtn = LCurr.DbId.ToString(); break;

                case "limpr.usrs": rtn = LImpr.Usrs.ToString(); break;
                case "limpr.usrgroups": rtn = LImpr.UsrGroups.ToString(); break;
                case "limpr.cultures": rtn = LImpr.Cultures.ToString(); break;
                case "limpr.rowauthoritys": rtn = LImpr.RowAuthoritys.ToString(); break;
                case "limpr.companys": rtn = LImpr.Companys.ToString(); break;
                case "limpr.projects": rtn = LImpr.Projects.ToString(); break;
                case "limpr.investors": rtn = LImpr.Investors.ToString(); break;
                case "limpr.customers": rtn = LImpr.Customers.ToString(); break;
                case "limpr.vendors": rtn = LImpr.Vendors.ToString(); break;
                case "limpr.agents": rtn = LImpr.Agents.ToString(); break;
                case "limpr.brokers": rtn = LImpr.Brokers.ToString(); break;
                case "limpr.members": rtn = LImpr.Members.ToString(); break;
                case "limpr.borrowers": rtn = LImpr.Borrowers.ToString(); break;
                case "limpr.guarantors": rtn = LImpr.Guarantors.ToString(); break;
                case "limpr.lenders": rtn = LImpr.Lenders.ToString(); break;

                case "config.architect": rtn = Config.Architect; break;
                case "config.cookiehttponly": rtn = Config.CookieHttpOnly; break;
                case "config.pwdexpdays": rtn = Config.PwdExpDays; break;
                case "config.adminloginonly": rtn = Config.AdminLoginOnly; break;
                case "config.wsdlexe": rtn = Config.WsdlExe; break;
                case "config.smtpserver": rtn = Config.SmtpServer; break;
                case "config.pmturl": rtn = Config.PmtUrl; break;
                case "config.ordurl": rtn = Config.OrdUrl; break;
                case "config.sslurl": rtn = Config.SslUrl; break;
                case "config.buildexe": rtn = Config.BuildExe; break;
                case "config.backuppath": rtn = Config.BackupPath; break;
                case "config.appnamespace": rtn = Config.AppNameSpace; break;
                case "config.deploytype": rtn = Config.DeployType; break;
                case "config.clienttierpath": rtn = Config.ClientTierPath; break;
                case "config.clanguagecd": rtn = Config.CLanguageCd; break;
                case "config.cframeworkcd": rtn = Config.CFrameworkCd; break;
                case "config.ruletierpath": rtn = Config.RuleTierPath; break;
                case "config.rlanguagecd": rtn = Config.RLanguageCd; break;
                case "config.rframeworkcd": rtn = Config.RFrameworkCd; break;
                case "config.dprovidercd": rtn = Config.DProviderCd; break;
                case "config.webtitle": rtn = Config.WebTitle; break;
                case "config.readonlybcolor": rtn = Config.ReadOnlyBColor; break;
                case "config.mandatorychar": rtn = Config.MandatoryChar; break;
                case "config.pathrtftemplate": rtn = Config.PathRtfTemplate; break;
                case "config.pathtxttemplate": rtn = Config.PathTxtTemplate; break;
                case "config.pathxlsimport": rtn = Config.PathXlsImport; break;
                case "config.pathtmpimport": rtn = Config.PathTmpImport; break;
                case "config.loginimage": rtn = Config.LoginImage; break;
                default: rtn = row[callp].ToString(); break;
            }
            return rtn;
        }

        private bool ExecSRule(string sRowFilter, DataView dvSRule, string firingEvent, string beforeCRUD, LoginUsr LUser, UsrImpr LImpr, UsrCurr LCurr, DataRow row, bool bDeferError, bool bHasErr, System.Collections.Generic.Dictionary<string, string> ErrLst, OleDbConnection cn, OleDbTransaction tr, ref string keyAdded)
        {
            string callp = string.Empty;
            string param = string.Empty;
            StringBuilder callingParams = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            StringBuilder parameterTypes = new StringBuilder();
            dvSRule.RowFilter = sRowFilter;
            foreach (DataRowView drv in dvSRule)
            {
                if (drv[firingEvent].ToString() == "Y" && drv["BeforeCRUD"].ToString() == beforeCRUD && (bDeferError || !bHasErr))
                {
                    try
                    {
                        OleDbCommand cmd = new OleDbCommand(drv["ProcedureName"].ToString(), cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        callingParams.Remove(0, callingParams.Length);
                        parameterNames.Remove(0, parameterNames.Length);
                        parameterTypes.Remove(0, parameterTypes.Length);
                        callingParams.Append(drv["CallingParams"].ToString());
                        parameterNames.Append(drv["ParameterNames"].ToString());
                        parameterTypes.Append(drv["ParameterTypes"].ToString());
                        while (parameterNames.Length > 0)
                        {
                            callp = Utils.PopFirstWord(callingParams, (char)44).Trim();
                            param = Utils.PopFirstWord(parameterNames, (char)44).Trim();
                            if (string.IsNullOrEmpty(callp) || callp.ToString().ToLower() == "null" || string.IsNullOrEmpty(GetCallParam(callp, LUser, LImpr, LCurr, row)) || GetCallParam(callp, LUser, LImpr, LCurr, row) == Convert.ToDateTime("0001.01.01").ToString())
                            {
                                cmd.Parameters.Add("@" + param, GetOleDbType(Utils.PopFirstWord(parameterTypes, (char)44).Trim())).Value = System.DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + param, GetOleDbType(Utils.PopFirstWord(parameterTypes, (char)44).Trim())).Value = GetCallParam(callp, LUser, LImpr, LCurr, row);
                            }
                        }
                        cmd.Transaction = tr; cmd.CommandTimeout = 1800;
                        if (beforeCRUD == "S" && firingEvent == "OnAdd")
                        {
                            da.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0][0].ToString())) { keyAdded = dt.Rows[0][0].ToString(); }
                        }
                        else
                        {
                            cmd.ExecuteNonQuery();
                        }
                        cmd.Dispose();
                        cmd = null;
                    }
                    catch (Exception e) { bHasErr = true; string suffix = ErrLst.ContainsKey(drv["ProcedureName"].ToString()) ? "_" + Guid.NewGuid().ToString().Substring(0, 4) : ""; ErrLst[drv["ProcedureName"].ToString() + suffix] = e.Message; }
                }
            }
            return bHasErr;
        }

        private string AddDataDt(string pMKeyCol, string pMKeyOle, string pMKeyVal, DataRow row, DataRow typDt, DataRow disDt, DataColumnCollection cols, string sql, string pKeyCol, string pKeyOle, OleDbConnection cn, OleDbTransaction tr)
        {
            string rtn = string.Empty;
            OleDbCommand cmd;
            cmd = new OleDbCommand("SET NOCOUNT ON " + sql.Replace("RODesign.", Config.AppNameSpace + "Design."), cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 3600;
            cmd.Transaction = tr;
            if (!string.IsNullOrEmpty(pMKeyOle))    // I2
            {
                if (string.IsNullOrEmpty(pMKeyVal) || pMKeyVal == Convert.ToDateTime("0001.01.01").ToString())
                {
                    cmd.Parameters.Add("@" + pMKeyCol, GetOleDbType(pMKeyOle)).Value = System.DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@" + pMKeyCol, GetOleDbType(pMKeyOle)).Value = pMKeyVal;
                }
            }
            if (string.IsNullOrEmpty(row[pKeyCol].ToString().Trim()) || row[pKeyCol].ToString().Trim() == Convert.ToDateTime("0001.01.01").ToString())
            {
                cmd.Parameters.Add("@" + pKeyCol, GetOleDbType(pKeyOle)).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@" + pKeyCol, GetOleDbType(pKeyOle)).Value = row[pKeyCol].ToString().Trim();
            }
            foreach (DataColumn dc in cols)
            {
                if (dc.ColumnName != pKeyCol && dc.ColumnName != pMKeyCol)
                {
                    if ("hyperlink,imagelink,hyperpopup,imagepopup,datagridlink,label".IndexOf(disDt[dc.ColumnName].ToString().ToLower()) < 0 && !string.IsNullOrEmpty(typDt[dc.ColumnName].ToString()) && !(disDt[dc.ColumnName].ToString().ToLower() == "imagebutton" && typDt[dc.ColumnName].ToString().ToLower() == "varbinary"))
                    {
                        if (string.IsNullOrEmpty(row[dc.ColumnName].ToString().Trim()) || row[dc.ColumnName].ToString().Trim() == Convert.ToDateTime("0001.01.01").ToString())
                        {
                            cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typDt[dc.ColumnName].ToString())).Value = System.DBNull.Value;
                        }
                        //else if (disDt[dc.ColumnName].ToString().ToLower() == "currency")
                        //{
                        //    cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typDt[dc.ColumnName].ToString())).Value = Decimal.Parse(row[dc.ColumnName].ToString().Trim(), System.Globalization.NumberStyles.Currency);
                        //}
                        else if (disDt[dc.ColumnName].ToString().ToLower() == "password")
                        {
                            cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typDt[dc.ColumnName].ToString())).Value = new Credential(string.Empty, row[dc.ColumnName].ToString().Trim()).Password;
                        }
                        else
                        {
                            cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typDt[dc.ColumnName].ToString())).Value = row[dc.ColumnName].ToString();
                        }
                    }
                }
            }
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			rtn = dt.Rows[0][0].ToString();
            cmd.Dispose();
            cmd = null;
            return rtn;
        }

        private void UpdDataDt(string pMKeyCol, DataRow row, DataRow typDt, DataRow disDt, DataColumnCollection cols, string sql, string pKeyCol, string pKeyOle, OleDbConnection cn, OleDbTransaction tr)
        {
            OleDbCommand cmd;
            cmd = new OleDbCommand("SET NOCOUNT ON " + sql.Replace("RODesign.", Config.AppNameSpace + "Design."), cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 3600;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@" + pKeyCol, GetOleDbType(pKeyOle)).Value = row[pKeyCol].ToString().Trim();
            foreach (DataColumn dc in cols)
            {
                if (dc.ColumnName != pKeyCol && dc.ColumnName != pMKeyCol && "hyperlink,imagelink,hyperpopup,imagepopup,datagridlink,label".IndexOf(disDt[dc.ColumnName].ToString().ToLower()) < 0 && !string.IsNullOrEmpty(typDt[dc.ColumnName].ToString()) && !(disDt[dc.ColumnName].ToString().ToLower() == "imagebutton" && typDt[dc.ColumnName].ToString().ToLower() == "varbinary"))
                {
                    if (string.IsNullOrEmpty(row[dc.ColumnName].ToString().Trim()) || row[dc.ColumnName].ToString().Trim() == Convert.ToDateTime("0001.01.01").ToString())
                    {
                        cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typDt[dc.ColumnName].ToString())).Value = System.DBNull.Value;
                    }
                    //else if (disDt[dc.ColumnName].ToString().ToLower() == "currency")
                    //{
                    //    cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typDt[dc.ColumnName].ToString())).Value = Decimal.Parse(row[dc.ColumnName].ToString().Trim(), System.Globalization.NumberStyles.Currency);
                    //}
                    else if (disDt[dc.ColumnName].ToString().ToLower() == "password")
                    {
                        cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typDt[dc.ColumnName].ToString())).Value = new Credential(string.Empty, row[dc.ColumnName].ToString().Trim()).Password;
                    }
                    else
                    {
                        cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typDt[dc.ColumnName].ToString())).Value = row[dc.ColumnName].ToString();
                    }
                }
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            return;
        }

        private void DelDataDt(LoginUsr LUser, DataRow row, DataRow typDt, DataRow disDt, DataColumnCollection cols, string sql, string pKeyCol, string pKeyOle, OleDbConnection cn, OleDbTransaction tr)
        {
            OleDbCommand cmd;
            cmd = new OleDbCommand("SET NOCOUNT ON " + sql.Replace("RODesign.", Config.AppNameSpace + "Design."), cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 3600;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@" + pKeyCol, GetOleDbType(pKeyOle)).Value = row[pKeyCol].ToString().Trim();
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = LUser.UsrId.ToString();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            return;
        }

        // Only I1 or I2 would call this:
        public string AddData(Int32 ScreenId, bool bDeferError, LoginUsr LUser, UsrImpr LImpr, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword, CurrPrj CPrj, CurrSrc CSrc)
        {
            bool bHasErr = false;
            string sAddDataDt = string.Empty;
            System.Collections.Generic.Dictionary<string, string> ErrLst = new System.Collections.Generic.Dictionary<string, string>();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            DataTable dtScr = null;
            DataTable dtAud = null;
            DataView dvSRule = null;
            DataView dvCol = null;
            using (Access3.GenScreensAccess dac = new Access3.GenScreensAccess())
            {
                dtScr = dac.GetScreenById(ScreenId, CPrj, CSrc);
                dtAud = dac.GetScreenAud(ScreenId, dtScr.Rows[0]["ScreenTypeName"].ToString(), CPrj.SrcDesDatabase, dtScr.Rows[0]["MultiDesignDb"].ToString(), CSrc);
                dvSRule = new DataView(dac.GetServerRule(ScreenId, CPrj, CSrc));
                dvCol = new DataView(dac.GetScreenColumns(ScreenId, CPrj, CSrc));
            }
            string appDbName = dtScr.Rows[0]["dbAppDatabase"].ToString();
            string screenName = dtScr.Rows[0]["ProgramName"].ToString();
            bool licensedScreen = IsLicensedFeature(appDbName, screenName);
            if (!licensedScreen)
            {
                throw new Exception("please acquire proper license to unlock this feature");
            }
            string pMKeyCol = string.Empty; string pDKeyCol = string.Empty;
            string pMKeyOle = string.Empty; string pDKeyOle = string.Empty;
            dvCol.RowFilter = "PrimaryKey = 'Y'";
            foreach (DataRowView drv in dvCol)
            {
                if (drv["MasterTable"].ToString() == "Y")
                {
                    pMKeyCol = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                    pMKeyOle = drv["DataTypeDByteOle"].ToString();
                }
                if (drv["MasterTable"].ToString() != "Y")
                {
                    pDKeyCol = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                    pDKeyOle = drv["DataTypeDByteOle"].ToString();
                }
            }
            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            DataRow row = ds.Tables[0].Rows[0];
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON " + dtAud.Rows[1][0].ToString().Replace("RODesign.", Config.AppNameSpace + "Design."), cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 3600;
            cmd.Transaction = tr;
            DataRow typ = ds.Tables[0].Rows[1]; DataRow dis = ds.Tables[0].Rows[2];
            if (string.IsNullOrEmpty(row[pMKeyCol].ToString().Trim()) || row[pMKeyCol].ToString().Trim() == Convert.ToDateTime("0001.01.01").ToString())
            {
                cmd.Parameters.Add("@" + pMKeyCol, GetOleDbType(pMKeyOle)).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@" + pMKeyCol, GetOleDbType(pMKeyOle)).Value = row[pMKeyCol].ToString().Trim(); ;
            }
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ColumnName != pMKeyCol && "hyperlink,imagelink,hyperpopup,imagepopup,datagridlink,imagebutton,label".IndexOf(dis[dc.ColumnName].ToString().ToLower()) < 0 && !string.IsNullOrEmpty(typ[dc.ColumnName].ToString()))
                {
                    if (string.IsNullOrEmpty(row[dc.ColumnName].ToString().Trim()) || row[dc.ColumnName].ToString().Trim() == Convert.ToDateTime("0001.01.01").ToString())
                    {
                        cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = System.DBNull.Value;
                    }
                    //else if (dis[dc.ColumnName].ToString().ToLower() == "currency")
                    //{
                    //    cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = Decimal.Parse(row[dc.ColumnName].ToString().Trim(), System.Globalization.NumberStyles.Currency);
                    //}
                    else if (dis[dc.ColumnName].ToString().ToLower() == "password")
                    {
                        cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = new Credential(string.Empty, row[dc.ColumnName].ToString().Trim()).Password;
                    }
                    else if (typ[dc.ColumnName].ToString().ToLower() == "varbinary")    // Not for password
                    {
                        cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = Convert.FromBase64String((string)row[dc.ColumnName]);
                    }
                    else
                    {
                        cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = row[dc.ColumnName].ToString();
                    }
                }
            }
            try
            {
                /* create the temp table that can be used between SPs */
                OleDbCommand tempTableCmd = new OleDbCommand("SET NOCOUNT ON CREATE TABLE #CRUDTemp (rid int identity, KeyVal varchar(max), ColumnName varchar(50), Val nvarchar(max), mode char(1), MasterTable char(1))", cn, tr);
                tempTableCmd.ExecuteNonQuery(); tempTableCmd.Dispose();
                /* Before CRUD rules */
                bool SkipAdd = false;
                bool SkipGridAdd = false;
                string pKeyAdded = null, dKeyAdded = null, _dummy = null;
                foreach (DataRowView drv in dvSRule)
                {
                    SkipAdd = SkipAdd || (drv["MasterTable"].ToString() == "Y" && drv["OnAdd"].ToString() == "Y" && drv["BeforeCRUD"].ToString() == "S");
                    SkipGridAdd = SkipGridAdd || (drv["MasterTable"].ToString() == "N" && drv["OnAdd"].ToString() == "Y" && drv["BeforeCRUD"].ToString() == "S");
                }
                pKeyAdded = null;
                bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnAdd", "Y", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref pKeyAdded);
                /* Skip CRUD */
                bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnAdd", "S", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref pKeyAdded);
                if (SkipAdd && !string.IsNullOrEmpty(pKeyAdded)) { row[pMKeyCol] = pKeyAdded; }
                if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                {
                    for (int jj = 0; jj < ds.Tables[2].Rows.Count; jj++)
                    {
                        if (SkipAdd && !string.IsNullOrEmpty(pKeyAdded) && SkipGridAdd) ds.Tables[2].Rows[jj][pMKeyCol] = pKeyAdded;
                        dKeyAdded = null;
                        bHasErr = ExecSRule("MasterTable <> 'Y'", dvSRule, "OnAdd", "Y", LUser, LImpr, LCurr, ds.Tables[2].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref dKeyAdded);
                        /* Skip CRUD */
                        bHasErr = ExecSRule("MasterTable <> 'Y'", dvSRule, "OnAdd", "S", LUser, LImpr, LCurr, ds.Tables[2].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref dKeyAdded);
                        if (!string.IsNullOrEmpty(dKeyAdded)) { ds.Tables[2].Rows[jj][pDKeyCol] = dKeyAdded; };
                    }
                }
                if ((bDeferError || !bHasErr) && !SkipAdd)
                {
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0][0].ToString())) { row[pMKeyCol] = dt.Rows[0][0].ToString(); }
                }
                if (!bHasErr)
                {
                    if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                    {
                        DataRow typDt = ds.Tables[1].Rows[0]; DataRow disDt = ds.Tables[1].Rows[1]; DataColumnCollection cols = ds.Tables[1].Columns;
                        for (int jj = 0; jj < ds.Tables[2].Rows.Count && !SkipGridAdd; jj++)
                        {
                            sAddDataDt = AddDataDt(pMKeyCol, pMKeyOle, row[pMKeyCol].ToString().Trim(), ds.Tables[2].Rows[jj], typDt, disDt, cols, dtAud.Rows[4][0].ToString(), pDKeyCol, pDKeyOle, cn, tr);
                            if (!string.IsNullOrEmpty(sAddDataDt)) { ds.Tables[2].Rows[jj][pDKeyCol] = sAddDataDt; };
                        }
                    }
                    /* After CRUD rules */
                    bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnAdd", "N", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                    {
                        for (int jj = 0; jj < ds.Tables[2].Rows.Count; jj++)
                        {
                            bHasErr = ExecSRule("MasterTable <> 'Y'", dvSRule, "OnAdd", "N", LUser, LImpr, LCurr, ds.Tables[2].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                        }
                    }
                    /* before commit */
                    bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnAdd", "C", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                }
                /* Only if both master and detail adds succeed */
                if (!bHasErr) { tr.Commit(); }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string err in ErrLst.Keys) { sb.Append(Environment.NewLine + err + "|" + ErrLst[err]); }
                    throw new Exception(sb.ToString());
                }
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "AddData", "", e.Message);
            }
            finally { cn.Close(); }
            if (ds.HasErrors)
            {
                ds.Tables[0].GetErrors()[0].ClearErrors();
                if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                {
                    ds.Tables[2].GetErrors()[0].ClearErrors();
                }
            }
            else
            {
                ds.AcceptChanges();
            }
            return row[pMKeyCol].ToString();
        }

        public bool UpdData(Int32 ScreenId, bool bDeferError, LoginUsr LUser, UsrImpr LImpr, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword, CurrPrj CPrj, CurrSrc CSrc)
        {
            bool bHasErr = false;
            string sAddDataDt = string.Empty;
            string sRowFilter = string.Empty;
            System.Collections.Generic.Dictionary<string, string> ErrLst = new System.Collections.Generic.Dictionary<string, string>();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            DataTable dtScr = null;
            DataTable dtAud = null;
            DataView dvSRule = null;
            DataView dvCol = null;
            using (Access3.GenScreensAccess dac = new Access3.GenScreensAccess())
            {
                dtScr = dac.GetScreenById(ScreenId, CPrj, CSrc);
                dtAud = dac.GetScreenAud(ScreenId, dtScr.Rows[0]["ScreenTypeName"].ToString(), CPrj.SrcDesDatabase, dtScr.Rows[0]["MultiDesignDb"].ToString(), CSrc);
                dvSRule = new DataView(dac.GetServerRule(ScreenId, CPrj, CSrc));
                dvCol = new DataView(dac.GetScreenColumns(ScreenId, CPrj, CSrc));
            }
            string appDbName = dtScr.Rows[0]["dbAppDatabase"].ToString();
            string screenName = dtScr.Rows[0]["ProgramName"].ToString();
            bool licensedScreen = IsLicensedFeature(appDbName, screenName);
            if (!licensedScreen)
            {
                throw new Exception("please acquire proper license to unlock this feature");
            }

            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            int ii = 0;
            DataRow row = ds.Tables[0].Rows[0];
            if ("I1,I2".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
            {
                OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON " + dtAud.Rows[2][0].ToString().Replace("RODesign.", Config.AppNameSpace + "Design."), cn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 3600;
                da.UpdateCommand = cmd;
                da.UpdateCommand.Transaction = tr;
                DataRow typ = ds.Tables[0].Rows[1]; DataRow dis = ds.Tables[0].Rows[2];
                string pMKeyCol = string.Empty;
                string pMKeyOle = string.Empty;
                string pMKeyVal = string.Empty;
                dvCol.RowFilter = "PrimaryKey = 'Y'";
                foreach (DataRowView drv in dvCol)
                {
                    if (drv["MasterTable"].ToString() == "Y")
                    {
                        pMKeyCol = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                        pMKeyOle = drv["DataTypeDByteOle"].ToString();
                    }
                }
                dvCol.RowFilter = "";
                pMKeyVal = row[pMKeyCol].ToString().Trim();
                cmd.Parameters.Add("@" + pMKeyCol, GetOleDbType(pMKeyOle)).Value = row[pMKeyCol].ToString().Trim();
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    if (dc.ColumnName != pMKeyCol && "hyperlink,imagelink,hyperpopup,imagepopup,datagridlink,imagebutton,label".IndexOf(dis[dc.ColumnName].ToString().ToLower()) < 0 && !string.IsNullOrEmpty(typ[dc.ColumnName].ToString()))
                    {
                        if (string.IsNullOrEmpty(row[dc.ColumnName].ToString().Trim()) || row[dc.ColumnName].ToString().Trim() == Convert.ToDateTime("0001.01.01").ToString())
                        {
                            cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = System.DBNull.Value;
                        }
                        //else if (dis[dc.ColumnName].ToString().ToLower() == "currency")
                        //{
                        //    cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = Decimal.Parse(row[dc.ColumnName].ToString().Trim(), System.Globalization.NumberStyles.Currency);
                        //}
                        else if (dis[dc.ColumnName].ToString().ToLower() == "password")
                        {
                            cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = new Credential(string.Empty, row[dc.ColumnName].ToString().Trim()).Password;
                        }
                        else if (typ[dc.ColumnName].ToString().ToLower() == "varbinary")    // Not for password
                        {
                            cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = Convert.FromBase64String((string)row[dc.ColumnName]);
                        }
                        else
                        {
                            cmd.Parameters.Add("@" + dc.ColumnName, GetOleDbType(typ[dc.ColumnName].ToString())).Value = row[dc.ColumnName].ToString();
                        }
                    }
                }
            }
            try
            {
                /* create the temp table that can be used between SPs */
                OleDbCommand tempTableCmd = new OleDbCommand("SET NOCOUNT ON CREATE TABLE #CRUDTemp (rid int identity, KeyVal varchar(max), mode char(1), MasterTable char(1))", cn, tr);
                tempTableCmd.ExecuteNonQuery(); tempTableCmd.Dispose();

                /* Before CRUD rules */
                bool SkipUpd = false;
                bool SkipGridAdd = false;
                bool SkipGridUpd = false;
                bool SkipGridDel = false;
                string _dummy = null;

                if ("I1,I2".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
                {
                    foreach (DataRowView drv in dvSRule)
                    {
                        SkipUpd = SkipUpd || (drv["MasterTable"].ToString() == "Y" && drv["OnUpd"].ToString() == "Y" && drv["BeforeCRUD"].ToString() == "S");
                    }
                    bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnUpd", "Y", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    /* Skip CRUD */
                    bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnUpd", "S", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                }
                if ("I2,I3".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
                {
                    foreach (DataRowView drv in dvSRule)
                    {
                        SkipGridAdd = SkipGridAdd || (drv["MasterTable"].ToString() == (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I3" ? "Y" : "N") && drv["OnAdd"].ToString() == "Y" && drv["BeforeCRUD"].ToString() == "S");
                        SkipGridUpd = SkipGridUpd || (drv["MasterTable"].ToString() == (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I3" ? "Y" : "N") && drv["OnUpd"].ToString() == "Y" && drv["BeforeCRUD"].ToString() == "S");
                        SkipGridDel = SkipGridDel || (drv["MasterTable"].ToString() == (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I3" ? "Y" : "N") && drv["OnDel"].ToString() == "Y" && drv["BeforeCRUD"].ToString() == "S");
                    }

                    string pMKeyCol = string.Empty; string pDKeyCol = string.Empty;
                    string pMKeyOle = string.Empty; string pDKeyOle = string.Empty;
                    dvCol.RowFilter = "PrimaryKey = 'Y'";
                    foreach (DataRowView drv in dvCol)
                    {
                        if (drv["MasterTable"].ToString() == "Y")
                        {
                            pMKeyCol = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                            pMKeyOle = drv["DataTypeDByteOle"].ToString();
                        }
                        if (drv["MasterTable"].ToString() != "Y")
                        {
                            pDKeyCol = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                            pDKeyOle = drv["DataTypeDByteOle"].ToString();
                        }
                    }

                    if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I3") { ii = 0; sRowFilter = "MasterTable = 'Y'"; } else { ii = 1; sRowFilter = "MasterTable <> 'Y'"; }
                    ii = ii + 1;
                    for (int jj = 0; jj < ds.Tables[ii].Rows.Count; jj++)
                    {
                        string KeyAdded = null;
                        if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2" && SkipGridAdd) ds.Tables[ii].Rows[jj][pMKeyCol] = row[pMKeyCol].ToString().Trim();
                        bHasErr = ExecSRule(sRowFilter, dvSRule, "OnAdd", "Y", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref KeyAdded);
                        /* Skip CRUD */
                        bHasErr = ExecSRule(sRowFilter, dvSRule, "OnAdd", "S", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref KeyAdded);
                        if (SkipGridAdd && !string.IsNullOrEmpty(KeyAdded)) { ds.Tables[ii].Rows[jj][dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2" ? pDKeyCol : pMKeyCol] = KeyAdded; };
                    }
                    ii = ii + 1;
                    for (int jj = 0; jj < ds.Tables[ii].Rows.Count; jj++)
                    {
                        bHasErr = ExecSRule(sRowFilter, dvSRule, "OnUpd", "Y", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                        /* Skip CRUD */
                        bHasErr = ExecSRule(sRowFilter, dvSRule, "OnUpd", "S", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    }
                    ii = ii + 1;
                    for (int jj = 0; jj < ds.Tables[ii].Rows.Count; jj++)
                    {
                        bHasErr = ExecSRule(sRowFilter, dvSRule, "OnDel", "Y", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                        /* Skip CRUD */
                        bHasErr = ExecSRule(sRowFilter, dvSRule, "OnDel", "S", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    }
                }

                if ("I1,I2".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0 && !SkipUpd)
                {
                    if (bDeferError || !bHasErr) { da.UpdateCommand.ExecuteNonQuery(); }
                }
                if (bDeferError || !bHasErr)
                {
                    if ("I2,I3".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
                    {
                        string pMKeyCol = string.Empty; string pDKeyCol = string.Empty;
                        string pMKeyOle = string.Empty; string pDKeyOle = string.Empty;
                        dvCol.RowFilter = "PrimaryKey = 'Y'";
                        foreach (DataRowView drv in dvCol)
                        {
                            if (drv["MasterTable"].ToString() == "Y")
                            {
                                pMKeyCol = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                                pMKeyOle = drv["DataTypeDByteOle"].ToString();
                            }
                            if (drv["MasterTable"].ToString() != "Y")
                            {
                                pDKeyCol = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                                pDKeyOle = drv["DataTypeDByteOle"].ToString();
                            }
                        }
                        if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I3") { ii = 0; } else { ii = 1; }
                        DataRow typDt = ds.Tables[ii].Rows[0]; DataRow disDt = ds.Tables[ii].Rows[1]; DataColumnCollection cols = ds.Tables[ii].Columns;
                        ii = ii + 1;

                        for (int jj = 0; jj < ds.Tables[ii].Rows.Count && !SkipGridAdd; jj++)
                        {
                            // this is technically not allowed to be skipped as subsequent server rules depend on the created key and there is 
                            // no easy way to return that via ExecSRule(assuming it was added there instead)

                            if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                            {
                                sAddDataDt = AddDataDt(pMKeyCol, pMKeyOle, row[pMKeyCol].ToString().Trim(), ds.Tables[ii].Rows[jj], typDt, disDt, cols, dtAud.Rows[4][0].ToString(), pDKeyCol, pDKeyOle, cn, tr);
                                if (!string.IsNullOrEmpty(sAddDataDt)) { ds.Tables[ii].Rows[jj][pDKeyCol] = sAddDataDt; };
                            }
                            else
                            {
                                sAddDataDt = AddDataDt(pMKeyCol, string.Empty, string.Empty, ds.Tables[ii].Rows[jj], typDt, disDt, cols, dtAud.Rows[1][0].ToString(), pMKeyCol, pMKeyOle, cn, tr);
                                if (!string.IsNullOrEmpty(sAddDataDt)) { ds.Tables[ii].Rows[jj][pMKeyCol] = sAddDataDt; };
                            }
                        }
                        ii = ii + 1;
                        for (int jj = 0; jj < ds.Tables[ii].Rows.Count && !SkipGridUpd; jj++)
                        {
                            if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                            {
                                UpdDataDt(pMKeyCol, ds.Tables[ii].Rows[jj], typDt, disDt, cols, dtAud.Rows[5][0].ToString(), pDKeyCol, pDKeyOle, cn, tr);
                            }
                            else
                            {
                                UpdDataDt(pMKeyCol, ds.Tables[ii].Rows[jj], typDt, disDt, cols, dtAud.Rows[2][0].ToString(), pMKeyCol, pMKeyOle, cn, tr);
                            }
                        }
                        ii = ii + 1;
                        for (int jj = 0; jj < ds.Tables[ii].Rows.Count && !SkipGridDel; jj++)
                        {
                            if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                            {
                                DelDataDt(LUser, ds.Tables[ii].Rows[jj], typDt, disDt, cols, dtAud.Rows[3][0].ToString(), pDKeyCol, pDKeyOle, cn, tr);
                            }
                            else
                            {
                                DelDataDt(LUser, ds.Tables[ii].Rows[jj], typDt, disDt, cols, dtAud.Rows[0][0].ToString(), pMKeyCol, pMKeyOle, cn, tr);
                            }
                        }
                    }
                    /* After CRUD rules */
                    DataRow GridRow = null; string GridRowType = null;
                    if ("I1,I2".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
                    {
                        bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnUpd", "N", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    }
                    if ("I2,I3".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
                    {
                        if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I3") { ii = 0; sRowFilter = "MasterTable = 'Y'"; } else { ii = 1; sRowFilter = "MasterTable <> 'Y'"; }
                        ii = ii + 1;
                        for (int jj = 0; jj < ds.Tables[ii].Rows.Count; jj++)
                        {
                            GridRow = ds.Tables[ii].Rows[jj]; GridRowType = "OnAdd";
                            bHasErr = ExecSRule(sRowFilter, dvSRule, "OnAdd", "N", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                        }
                        ii = ii + 1;
                        for (int jj = 0; jj < ds.Tables[ii].Rows.Count; jj++)
                        {
                            GridRow = ds.Tables[ii].Rows[jj]; ; GridRowType = "OnUpd";
                            bHasErr = ExecSRule(sRowFilter, dvSRule, "OnUpd", "N", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                        }
                        ii = ii + 1;
                        for (int jj = 0; jj < ds.Tables[ii].Rows.Count; jj++)
                        {
                            GridRow = ds.Tables[ii].Rows[jj]; ; GridRowType = "OnDel";
                            bHasErr = ExecSRule(sRowFilter, dvSRule, "OnDel", "N", LUser, LImpr, LCurr, ds.Tables[ii].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                        }
                    }
                    /* before commit */
                    if ("I1,I2".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
                    {
                        bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnUpd", "C", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    }
                    else if (GridRow != null) // I3 with at least one row changed
                    {
                        // would only run ONCE using the last row info (delete or update or add, in that order)
                        bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, GridRowType, "C", LUser, LImpr, LCurr, GridRow, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    }
                }
                /* Only if both master and detail succeed */
                if (!bHasErr) { tr.Commit(); }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string err in ErrLst.Keys) { sb.Append(Environment.NewLine + err + "|" + ErrLst[err]); }
                    throw new Exception(sb.ToString());
                }
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "UpdData", "", e.Message);
            }
            finally { cn.Close(); }
            if (ds.HasErrors)
            {
                ii = 0;
                if ("I1,I2".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
                {
                    ds.Tables[ii].GetErrors()[0].ClearErrors(); ii = ii + 1;
                }
                if ("I2,I3".IndexOf(dtScr.Rows[0]["ScreenTypeName"].ToString()) >= 0)
                {
                    ii = ii + 1; ds.Tables[ii].GetErrors()[0].ClearErrors();
                    ii = ii + 1; ds.Tables[ii].GetErrors()[0].ClearErrors();
                    ii = ii + 1; ds.Tables[ii].GetErrors()[0].ClearErrors();
                }
                return false;
            }
            else
            {
                ds.AcceptChanges(); return true;
            }
        }

        // Only I1 or I2 would call this.
        public bool DelData(Int32 ScreenId, bool bDeferError, LoginUsr LUser, UsrImpr LImpr, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword, CurrPrj CPrj, CurrSrc CSrc)
        {
            bool bHasErr = false;
            System.Collections.Generic.Dictionary<string, string> ErrLst = new System.Collections.Generic.Dictionary<string, string>();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            DataTable dtScr = null;
            DataTable dtAud = null;
            DataView dvSRule = null;
            DataView dvCol = null;
            using (Access3.GenScreensAccess dac = new Access3.GenScreensAccess())
            {
                dtScr = dac.GetScreenById(ScreenId, CPrj, CSrc);
                dtAud = dac.GetScreenAud(ScreenId, dtScr.Rows[0]["ScreenTypeName"].ToString(), CPrj.SrcDesDatabase, dtScr.Rows[0]["MultiDesignDb"].ToString(), CSrc);
                dvSRule = new DataView(dac.GetServerRule(ScreenId, CPrj, CSrc));
                dvCol = new DataView(dac.GetScreenColumns(ScreenId, CPrj, CSrc));
            }
            string appDbName = dtScr.Rows[0]["dbAppDatabase"].ToString();
            string screenName = dtScr.Rows[0]["ProgramName"].ToString();
            bool licensedScreen = IsLicensedFeature(appDbName, screenName);
            if (!licensedScreen)
            {
                throw new Exception("please acquire proper license to unlock this feature");
            }

            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            DataRow row = ds.Tables[0].Rows[0];
            // SQL in dtAud delete the detail rows for I2 as well:
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON " + dtAud.Rows[0][0].ToString().Replace("RODesign.", Config.AppNameSpace + "Design."), cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 3600;
            cmd.Transaction = tr;
            dvCol.RowFilter = "PrimaryKey = 'Y'";
            foreach (DataRowView drv in dvCol)
            {
                if (drv["MasterTable"].ToString() == "Y")
                {
                     cmd.Parameters.Add("@" + drv["ColumnName"].ToString() + drv["TableId"].ToString(), GetOleDbType(drv["DataTypeDByteOle"].ToString())).Value = row[drv["ColumnName"].ToString() + drv["TableId"].ToString()].ToString().Trim();
                     cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = LUser.UsrId.ToString();
                }
            }
            try
            {
                /* create the temp table that can be used between SPs */
                OleDbCommand tempTableCmd = new OleDbCommand("SET NOCOUNT ON CREATE TABLE #CRUDTemp (rid int identity, KeyVal varchar(max), mode char(1), MasterTable char(1))", cn, tr);
                tempTableCmd.ExecuteNonQuery(); tempTableCmd.Dispose();

                bool SkipDel = false;
                string _dummy = null;
                foreach (DataRowView drv in dvSRule)
                {
                    SkipDel = SkipDel || (drv["MasterTable"].ToString() == "Y" && drv["OnDel"].ToString() == "Y" && drv["BeforeCRUD"].ToString() == "S");
                }

                /* Before CRUD rules */
                bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnDel", "Y", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                /* Skip CRUD */
                bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnDel", "S", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                {
                    for (int jj = 0; jj < ds.Tables[4].Rows.Count; jj++)
                    {
                        bHasErr = ExecSRule("MasterTable <> 'Y'", dvSRule, "OnDel", "Y", LUser, LImpr, LCurr, ds.Tables[4].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                        /* Skip CRUD */
                        bHasErr = ExecSRule("MasterTable <> 'Y'", dvSRule, "OnDel", "S", LUser, LImpr, LCurr, ds.Tables[4].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    }
                }
                if ((bDeferError || !bHasErr) && !SkipDel) { cmd.ExecuteNonQuery(); }
                bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnDel", "N", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                {
                    for (int jj = 0; jj < ds.Tables[4].Rows.Count; jj++)
                    {
                        bHasErr = ExecSRule("MasterTable <> 'Y'", dvSRule, "OnDel", "N", LUser, LImpr, LCurr, ds.Tables[4].Rows[jj], bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                    }
                }
                /* before commit */
                bHasErr = ExecSRule("MasterTable = 'Y'", dvSRule, "OnDel", "C", LUser, LImpr, LCurr, row, bDeferError, bHasErr, ErrLst, cn, tr, ref _dummy);
                if (!bHasErr) { tr.Commit(); }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string err in ErrLst.Keys) { sb.Append(Environment.NewLine + err + "|" + ErrLst[err]); }
                    throw new Exception(sb.ToString());
                }
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "DelData", "", e.Message);
            }
            finally { cn.Close(); }
            if (ds.HasErrors)
            {
                ds.Tables[0].GetErrors()[0].ClearErrors();
                if (dtScr.Rows[0]["ScreenTypeName"].ToString() == "I2")
                {
                    ds.Tables[4].GetErrors()[0].ClearErrors();
                }
                return false;
            }
            else
            {
                ds.AcceptChanges(); return true;
            }
        }

        public string DelDoc(string MasterId, string DocId, string UsrId, string DdlKeyTableName, string TableName, string ColumnName, string pMKey, string dbConnectionString, string dbPassword)
        {
            string rtn = string.Empty;
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
            + " DECLARE @MasterId numeric(10,0), @DocId numeric(10,0), @UsrId numeric(10,0) SELECT @MasterId=?, @DocId=?, @UsrId=?"
            + " IF EXISTS (SELECT 1 FROM dbo." + DdlKeyTableName + " WHERE DocId = @DocId AND InputBy = @UsrId)"
            + " BEGIN"
                + " DELETE FROM dbo." + DdlKeyTableName + " WHERE DocId = @DocId"
                + " SELECT @DocId = MAX(DocId) FROM dbo." + DdlKeyTableName + " WHERE MasterId = @MasterId"
                + " UPDATE dbo." + TableName + " SET " + ColumnName + " = @DocId WHERE " + pMKey + " = @MasterId"
            + " END"
            + " SELECT @DocId", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@MasterId", OleDbType.Numeric).Value = MasterId;
            cmd.Parameters.Add("@DocId", OleDbType.Numeric).Value = DocId;
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0][0].ToString())) { rtn = dt.Rows[0][0].ToString(); }
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, string.Empty, string.Empty, e.Message);
            }
            finally { cn.Close(); }
            return rtn;
        }

        // For reports:

        public DataTable GetIn(Int32 reportId, string procedureName, int TotalCnt, string RequiredValid, bool bAll, string keyId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
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
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (RequiredValid != "Y" && dt.Rows.Count >= TotalCnt) { dt.Rows.InsertAt(dt.NewRow(), 0); }
            return dt;
        }

        // To be deleted: for backward compatibility only.
        public DataTable GetIn(Int32 reportId, string procedureName, bool bAddNew, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
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
            cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
            cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (bAddNew) { dt.Rows.InsertAt(dt.NewRow(), 0); }
            return dt;
        }

        public DataTable GetRptDt(Int32 reportId, string procedureName, UsrImpr ui, UsrCurr uc, DataSet ds, DataView dvCri, string dbConnectionString, string dbPassword, bool bUpd, bool bXls, bool bVal)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand(procedureName, new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
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
            if (dvCri != null && ds != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                foreach (DataRowView drv in dvCri)
                {
                    if (drv["RequiredValid"].ToString() == "N" && string.IsNullOrEmpty(dr[drv["ColumnName"].ToString()].ToString().Trim()))
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
            }
            if (bUpd) { cmd.Parameters.Add("@bUpd", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bUpd", OleDbType.Char).Value = "N"; }
            if (bXls) { cmd.Parameters.Add("@bXls", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bXls", OleDbType.Char).Value = "N"; }
            if (bVal) { cmd.Parameters.Add("@bVal", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bVal", OleDbType.Char).Value = "N"; }
            da.SelectCommand = cmd;
            cmd.CommandTimeout = 1800;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool UpdRptDt(Int32 reportId, string procedureName, Int32 usrId, DataSet ds, DataView dvCri, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand(procedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            da.UpdateCommand = cmd;
            da.UpdateCommand.Transaction = tr;
            cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
            cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = usrId;
            if (dvCri != null && ds != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                foreach (DataRowView drv in dvCri)
                {
                    if (drv["RequiredValid"].ToString() == "N" && string.IsNullOrEmpty(dr[drv["ColumnName"].ToString()].ToString().Trim()))
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
            }
            try
            {
                da.UpdateCommand.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, "UpdRptDt", "", e.Message);
            }
            finally { cn.Close(); }
            if (ds.HasErrors)
            {
                ds.Tables[0].GetErrors()[0].ClearErrors(); return false;
            }
            else
            {
                ds.AcceptChanges(); return true;
            }
        }

        public DataTable GetPrinterList(string UsrGroups, string Members)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetPrinterList", new OleDbConnection(GetDesConnStr()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UsrGroups", OleDbType.VarChar).Value = UsrGroups;
            cmd.Parameters.Add("@Members", OleDbType.VarChar).Value = Members;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.InsertAt(dt.NewRow(), 0);
                dt.Rows[0]["PrinterPath"] = "*";
                dt.Rows[0]["PrinterName"] = "<Printer Unavailable>";
            }
            return dt;
        }

        // For legacy batch reporting only:
        public void UpdLastCriteria(Int32 screenId, Int32 reportId, Int32 usrId, Int32 criId, string lastCriteria, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("UpdLastCriteria", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = reportId;
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
            cmd.Parameters.Add("@CriId", OleDbType.Numeric).Value = criId;
            if (string.IsNullOrEmpty(lastCriteria))
            {
                cmd.Parameters.Add("@LastCriteria", OleDbType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                if (Config.DoubleByteDb) { cmd.Parameters.Add("@LastCriteria", OleDbType.VarWChar).Value = lastCriteria; } else { cmd.Parameters.Add("@LastCriteria", OleDbType.VarChar).Value = lastCriteria; }
            }
            cmd.CommandTimeout = 1800;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public DataTable GetReportHlp(Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = null;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cmd = new OleDbCommand("GetReportHlp", new OleDbConnection(GetDesConnStr()));
            }
            else
            {
                cmd = new OleDbCommand("GetReportHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
            cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetReportHlp", "Report Issue", "Default help message not available for Report #'" + reportId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
            return dt;
        }

        public DataTable GetReportCriHlp(Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetReportCriHlp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
            cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetReportCriHlp", "Report Issue", "Report Criteria Column Headers not available for Report #'" + reportId.ToString() + "' and Culture #'" + cultureId.ToString() + "'!");
            return dt;
        }

        public DataTable GetReportSct()
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetReportSct", new OleDbConnection(GetDesConnStr()));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetReportItem(Int32 ReportId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetReportItem", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Need to do this because SQLRS Report Viewer is not accessible here:
        public string GetRptPwd(string pwd)
        {
            return DecryptString(pwd);
        }

        // For Wizards:

        public string GetSchemaWizImp(Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetSchemaWizImp", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@wizardId", OleDbType.Numeric).Value = wizardId;
            cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows) { sb.Append(dr[0].ToString()); }
            return sb.ToString();
        }

        public string GetWizImpTmpl(Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetWizImpTmpl", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@wizardId", OleDbType.Numeric).Value = wizardId;
            cmd.Parameters.Add("@cultureId", OleDbType.Numeric).Value = cultureId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows) { sb.Append(dr[0].ToString()); }
            return sb.ToString();
        }

        private void ImportRow(DataView dvCol, string procedureName, string ImportFileName, DataRow row, OleDbConnection cn, OleDbTransaction tr)
        {
            OleDbCommand cmd = new OleDbCommand(procedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ImportFileName", OleDbType.VarChar).Value = ImportFileName;
            int ii = 0;
            foreach (DataRowView drv in dvCol)
            {
                try
                {
                    if (string.IsNullOrEmpty(row[ii].ToString().Trim()) || row[ii].ToString().Trim() == Convert.ToDateTime("0001.01.01").ToString() || row[ii].ToString().Trim() == Convert.ToDateTime("1899.12.30").ToString()
                        || (",numeric,decimal,currency,double,".IndexOf(drv["DataTypeSByteOle"].ToString().ToLower()) >= 0 && row[ii].ToString().Trim() == "-") // "-" is a form of empty/null in excel for certain numeric formatting
                        || row[ii].ToString().Trim() == "#N/A") // excel formula lookup result, should be another form of null
                    {
                        cmd.Parameters.Add("@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString(), GetOleDbType(drv["DataTypeDByteOle"].ToString())).Value = System.DBNull.Value;
                    }
                    else if (",numeric,decimal,double,".IndexOf(drv["DataTypeSByteOle"].ToString().ToLower()) >= 0 && row[ii].ToString().Trim().EndsWith("%"))
                    {
                        cmd.Parameters.Add("@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString(), GetOleDbType(drv["DataTypeDByteOle"].ToString())).Value = Decimal.Parse(row[ii].ToString().Trim().Left(row[ii].ToString().Trim().Length - 1));
                    }
                    else if (drv["DataTypeSByteOle"].ToString().ToLower() == "currency")
                    {
                        cmd.Parameters.Add("@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString(), GetOleDbType(drv["DataTypeDByteOle"].ToString())).Value = Decimal.Parse(row[ii].ToString().Trim(), System.Globalization.NumberStyles.Currency);
                    }
                    else if (drv["DataTypeSysName"].ToString().Contains("Date") && !string.IsNullOrEmpty(row[ii].ToString().Trim()))
                    {
                        cmd.Parameters.Add("@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString(), GetOleDbType(drv["DataTypeDByteOle"].ToString())).Value = DateTime.Parse(row[ii].ToString().Trim(), System.Threading.Thread.CurrentThread.CurrentCulture);
                    }
                    else
                    {
                        cmd.Parameters.Add("@" + Robot.SmallCapToStart(drv["ColumnName"].ToString()) + drv["TableId"].ToString(), GetOleDbType(drv["DataTypeDByteOle"].ToString())).Value = row[ii].ToString().Trim();
                    }
                    ii = ii + 1;
                }
                catch (Exception er) { throw new Exception("Col " + Utils.Num2ExcelCol(ii + 1) + " " + er.Message, er); };
            }
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            return;
        }

        public int ImportRows(Int32 wizardId, string procedureName, bool bOverwrite, Int32 usrId, DataSet ds, int iStart, string fileName, string dbConnectionString, string dbPassword, CurrPrj CPrj, CurrSrc CSrc)
        {
            bool bDeferError = true;       // This can be false if error is to be trapped one at a time.
            bool bHasErr = false;
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            System.Collections.Generic.Dictionary<string, string> ErrLst = new System.Collections.Generic.Dictionary<string, string>();
            DataView dvRul = null;
            DataView dvCol = null;
            using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
            {
                dvRul = new DataView(dac.GetWizardRule(wizardId, CPrj, CSrc));
                dvCol = new DataView(dac.GetWizardColumns(wizardId, CPrj, CSrc));
            }
            int ii = 1;
            DataRowCollection rows = ds.Tables[0].Rows;
            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                cn = new OleDbConnection(GetDesConnStr());
            }
            else
            {
                cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            }
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            try
            {
                bHasErr = ExecWRule(dvRul, "Y", bOverwrite, usrId, fileName, bDeferError, bHasErr, ErrLst, cn, tr);
                for (ii = iStart; ii < rows.Count; ii++)
                {
                    if (rows[ii].RowState != System.Data.DataRowState.Deleted)
                    {
                        try { ImportRow(dvCol, procedureName, fileName, rows[ii], cn, tr); }
                        catch (Exception e)
                        {
                            ApplicationAssert.CheckCondition(false, "ImportRows", "", "Row " + (ii + 1).ToString() + ", " + e.Message);
                        }
                    }
                }
                bHasErr = ExecWRule(dvRul, "N", bOverwrite, usrId, fileName, bDeferError, bHasErr, ErrLst, cn, tr);
                if (!bHasErr) { tr.Commit(); }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string err in ErrLst.Keys) { sb.Append(Environment.NewLine + err + "|" + ErrLst[err]); }
                    throw new Exception(sb.ToString());
                }
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "ImportRows", "", e.Message);
            }
            finally { cn.Close(); }
            if (ds.HasErrors)
            {
                ds.Tables[0].GetErrors()[0].ClearErrors(); return 0;
            }
            else
            {
                ds.AcceptChanges(); return (rows.Count - iStart);
            }
        }

        private bool ExecWRule(DataView dvRul, string beforeCRUD, bool bOverwrite, Int32 usrId, string ImportFileName, bool bDeferError, bool bHasErr, System.Collections.Generic.Dictionary<string, string> ErrLst, OleDbConnection cn, OleDbTransaction tr)
        {
            foreach (DataRowView drv in dvRul)
            {
                if (drv["BeforeCRUD"].ToString() == beforeCRUD && (bDeferError || !bHasErr))
                {
                    try
                    {
                        OleDbCommand cmd = new OleDbCommand(drv["ProcedureName"].ToString(), cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (bOverwrite) { cmd.Parameters.Add("@Overwrite", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@Overwrite", OleDbType.Char).Value = "N"; }
                        cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = usrId;
                        cmd.Parameters.Add("@ImportFileName", OleDbType.VarChar).Value = ImportFileName;
                        cmd.Transaction = tr; cmd.CommandTimeout = 1800; cmd.ExecuteNonQuery(); cmd.Dispose(); cmd = null;
                    }
                    catch (Exception e) { bHasErr = true; ErrLst[drv["ProcedureName"].ToString()] = e.Message; }
                }
            }
            return bHasErr;
        }

        // For general:

        public bool IsRegenNeeded(string ProgramName, Int32 ScreenId, Int32 ReportId, Int32 WizardId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn;
            if (string.IsNullOrEmpty(dbConnectionString)) { cn = new OleDbConnection(GetDesConnStr()); } else { cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword)); }
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("IsRegenNeeded", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (string.IsNullOrEmpty(ProgramName))
            {
                cmd.Parameters.Add("@ProgramName", OleDbType.VarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@ProgramName", OleDbType.VarChar).Value = ProgramName;
            }
            cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
            cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
            cmd.Parameters.Add("@WizardId", OleDbType.Numeric).Value = WizardId;
            int rtn = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
            if (rtn == 0) { return false; } else { return true; }
        }

        public string AddDbDoc(string MasterId, string TblName, string DocName, string MimeType, long DocSize, byte[] dc, string dbConnectionString, string dbPassword, LoginUsr lu)
        {
            string rtn = string.Empty;
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
                + " DECLARE @DocId numeric(10,0)"
                + " INSERT " + TblName + " (MasterId, DocName, MimeType, DocSize, DocImage, InputBy, InputOn, Active)"
                + " SELECT ?, ?, ?, ?, ?, ?, getdate(), 'Y'"
                + " SELECT @DocId = @@IDENTITY"
                + " SELECT @DocId", cn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MasterId", OleDbType.Numeric).Value = MasterId;
            cmd.Parameters.Add("@DocName", OleDbType.VarWChar).Value = DocName;
            cmd.Parameters.Add("@MimeType", OleDbType.VarChar).Value = MimeType;
            cmd.Parameters.Add("@DocSize", OleDbType.Numeric).Value = DocSize;
            cmd.Parameters.Add("@DocImage", OleDbType.Binary).Value = dc;
            cmd.Parameters.Add("@InputBy", OleDbType.Numeric).Value = lu.UsrId;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                rtn = dt.Rows[0][0].ToString();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                cn.Close();
            }
            return rtn;
        }

        // Get the most recent to replace as long as it has the same file name and owned by this user:
        public string GetDocId(string MasterId, string TblName, string DocName, string UsrId, string dbConnectionString, string dbPassword)
        {
            string rtn = string.Empty;
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
                + " SELECT DocId FROM " + TblName + " WHERE InputOn = (SELECT MAX(InputOn) FROM " + TblName + " WHERE MasterId = ? AND DocName = ? AND InputBy = ?)", cn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MasterId", OleDbType.Numeric).Value = MasterId;
            cmd.Parameters.Add("@DocName", OleDbType.VarWChar).Value = DocName;
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0) { rtn = dt.Rows[0][0].ToString(); }
            }
            catch (Exception e)
            {
                ApplicationAssert.CheckCondition(false, "", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                cn.Close();
            }
            return rtn;
        }

        public void UpdDbDoc(string DocId, string TblName, string DocName, string MimeType, long DocSize, byte[] dc, string dbConnectionString, string dbPassword, LoginUsr lu)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
                + " UPDATE " + TblName + " SET DocName = ?, MimeType = ?, DocSize = ?, DocImage = ?, InputBy = ?, InputOn = getdate(), Active = 'Y'"
                + " WHERE DocId = ?", cn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@DocName", OleDbType.VarWChar).Value = DocName;
            cmd.Parameters.Add("@MimeType", OleDbType.VarChar).Value = MimeType;
            cmd.Parameters.Add("@DocSize", OleDbType.Numeric).Value = DocSize;
            cmd.Parameters.Add("@DocImage", OleDbType.Binary).Value = dc;
            cmd.Parameters.Add("@InputBy", OleDbType.Numeric).Value = lu.UsrId;
            cmd.Parameters.Add("@DocId", OleDbType.Numeric).Value = DocId;
            cmd.CommandTimeout = 1800;
            da.UpdateCommand = cmd;
            da.UpdateCommand.Transaction = tr;
            try
            {
                da.UpdateCommand.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                cn.Close();
            }
        }

        public void UpdDbImg(string DocId, string TblName, string KeyName, string ColName, byte[] dc, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON UPDATE " + TblName + " SET " + ColName + " = ? WHERE " + KeyName + " = ?", cn);
            cmd.CommandType = CommandType.Text;
            if (dc == null)
            {
                cmd.Parameters.Add("@DocImage", OleDbType.Binary).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@DocImage", OleDbType.Binary).Value = dc;
            }
            cmd.Parameters.Add("@DocId", OleDbType.Numeric).Value = DocId;
            cmd.CommandTimeout = 1800;
            da.UpdateCommand = cmd;
            da.UpdateCommand.Transaction = tr;
            try
            {
                da.UpdateCommand.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                cn.Close();
            }
        }

        public bool IsMDesignDb(string TblName)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("IsMDesignDb", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TblName", OleDbType.VarChar).Value = TblName;
            int rtn = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
            if (rtn == 0) { return false; }
            else { return true; }
        }

        public DataTable GetDbDoc(string DocId, string TblName, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
                + " SELECT DocName, MimeType, DocImage FROM " + TblName + " WHERE DocId = ?", cn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@DocId", OleDbType.Numeric).Value = DocId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try { da.Fill(dt); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message); }
            finally { cmd.Dispose(); cmd = null; cn.Close(); }
            return dt;
        }

        public DataTable GetDbImg(string DocId, string TblName, string KeyName, string ColName, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON SELECT " + ColName + " FROM " + TblName + " WHERE " + KeyName + " = ?", cn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@DocId", OleDbType.Numeric).Value = DocId;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try { da.Fill(dt); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message); }
            finally { cmd.Dispose(); cmd = null; cn.Close(); }
            return dt;
        }

        public string GetDesignVersion(string ns, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            ns = ns.Trim();
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
                     + " SELECT TOP 1 1 a.AppInfoDesc FROM " + ns + "Design.dbo.AppInfo a"
                     + " INNER JOIN " + ns + "Design.dbo.AppItem ai on a.AppInfoId = ai.AppInfoId and a.VersionDt is not null"
                     + " ORDER BY a.Version Dt DESC"
                     , cn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1800;
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0) return dt.Rows[0][0].ToString();
                else return "";
            }
            finally
            {
                cn.Close();
            }
        }

        public List<string> HasOutstandReleaseContent(string ns, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            ns = ns.Trim();
            List<string> outstandingReleaseContentSys = new List<string>();
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
                     + " SELECT * FROM " + ns + "Design.dbo.Systems"
                     , cn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1800;
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string dbName = dr["dbDesDatabase"].ToString();
                    DataTable dtX = new DataTable();
                    cmd.CommandText = "SET NOCOUNT ON"
                        + " SELECT TOP 1 1 "
                        + " FROM " + dbName + ".dbo.AppInfo a "
                        + " INNER JOIN " + dbName + ".dbo.AppItem ai on a.AppInfoId = ai.AppInfoId and a.VersionDt is null"
                        + "";
                    da.SelectCommand = cmd;
                    da.Fill(dtX);
                    if (dtX.Rows.Count > 0)
                    {
                        outstandingReleaseContentSys.Add(dr["SystemName"].ToString());
                    }
                }
            }
            finally { cn.Close(); }
            return outstandingReleaseContentSys;
        }

        public Dictionary<string,List<string>> HasOutstandRegen(string ns, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            ns = ns.Trim();
            Dictionary<string, List<string>> regenList = new Dictionary<string, List<string>>();
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
                     + " SELECT * FROM " + ns + "Design.dbo.Systems"
                     , cn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1800;
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string dbName = dr["dbDesDatabase"].ToString();
                    if (dbName.ToLower().EndsWith("design") && ns.ToLower() != "ro") continue;
                    DataTable dtX = new DataTable();
                    cmd.CommandText = "SET NOCOUNT ON"
                        + " SELECT Typ='Sreen', ProgramName = ProgramName, Description = ScreenDesc "
                        + " FROM " + dbName + ".dbo.Screen WHERE NeedRegen = 'Y' and (GenerateSc = 'Y' or GenerateSr = 'Y') "
                        + " UNION "
                        + " SELECT Typ='Report', ProgramName = ProgramName, Description = ReportDesc "
                        + " FROM " + dbName + ".dbo.Report where NeedRegen = 'Y' and (Generaterp = 'Y') "
                        + " UNION "
                        + " SELECT Typ='Wizard', ProgramName = ProgramName, Description = WizardTitle "
                        + " FROM " + dbName + ".dbo.Wizard where NeedRegen = 'Y' "
                        + "";
                    da.SelectCommand = cmd;
                    da.Fill(dtX);
                    if (dtX.Rows.Count > 0)
                    {
                        Dictionary<string, string> l = dtX.AsEnumerable().GroupBy(x => x["Typ"].ToString(), x => x.Field<string>("Description")).ToDictionary(x => x.Key, x => string.Join(",", x.ToArray<string>()));
                        regenList[dr["SystemName"].ToString()] = l.Select(x => x.Key + " : " + x.Value).ToList();
                    }
                }
            }
            finally { cn.Close(); }
            return regenList;
        }

        public void UpdFxRate(string FrCurrency, string ToCurrency, string ToFxRate)
        {
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("UpdFxRate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FrCurrency", OleDbType.VarChar).Value = FrCurrency;
            cmd.Parameters.Add("@ToCurrency", OleDbType.VarChar).Value = ToCurrency;
            cmd.Parameters.Add("@ToFxRate", OleDbType.VarChar).Value = ToFxRate; // Must use .VarChar to bypass the unable to convert error.
            cmd.CommandTimeout = 1800;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "UpdFxRate", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public DataTable GetFxRate(string FrCurrency, string ToCurrency)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("GetFxRate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FrCurrency", OleDbType.VarChar).Value = FrCurrency;
            cmd.Parameters.Add("@ToCurrency", OleDbType.VarChar).Value = ToCurrency;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try { da.Fill(dt); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "GetFxRate", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return dt;
        }

        public void MkWfStatus(string ScreenObjId, string MasterTable, string appDatabase, string sysDatabase, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("MkWfStatus", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenObjId", OleDbType.Numeric).Value = ScreenObjId;
            cmd.Parameters.Add("@MasterTable", OleDbType.Char).Value = MasterTable;
            cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
            cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); }
        }
    }
}