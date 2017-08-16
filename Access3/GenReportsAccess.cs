namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class GenReportsAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;
	
		public GenReportsAccess()
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

        public void SetRptNeedRegen(Int32 reportId, CurrSrc CSrc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SetRptNeedRegen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetRptNeedRegen", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

		public DataTable GetReportById(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetReportById",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = CPrj.SrcDesDatabase;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = CSrc.SrcDbDatabase;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetReportById", "Report Issue", "Information for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetReportColumns(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetReportColumns",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetReportColumns", "Report Columns Issue", "Columns for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetCriReportGrp(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetCriReportGrp",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetCriReportGrp", "Report Issue", "Report Group not available for Report #'" + reportId.ToString() + "'!");
			return dt;
		}

		public DataTable GetReportCriteria(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetReportCriteria",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetReportCriteria", "Report Criteria Issue", "Criteria for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public void MkReportGet(string GenPrefix, Int32 reportId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("MkReportGet", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		// Do not erase stored procedure as this is also called from SqlReportAccess.cs differently:
		public void MkReportGetIn(string GenPrefix, Int32 reportCriId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("MkReportGetIn", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportCriId", OleDbType.Numeric).Value = reportCriId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		public void MkReportUpd(string GenPrefix, Int32 reportId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("MkReportUpd", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		public DataTable GetReportDel(string GenPrefix, string srcDatabase, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetReportDel",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = srcDatabase;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void DelReportDel(string GenPrefix, string srcDatabase, string appDatabase, string desDatabase, string programName, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DelReportDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = srcDatabase;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@programName", OleDbType.VarChar).Value = programName;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}

		public DataTable GetReportCriDel(string GenPrefix, Int32 reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetReportCriDel",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void DelReportCriDel(string GenPrefix, string appDatabase, Int32 reportId, string procedureName, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DelReportCriDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}

		public DataTable GetReportElm(string GenPrefix, Int32 reportId, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OleDbCommand cmd = new OleDbCommand("GetReportElm", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OleDbType.Numeric).Value = reportId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetReportCtr(string GenPrefix, string PRptCtrId, string RptElmId, string RptCelId, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OleDbCommand cmd = new OleDbCommand("GetReportCtr", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			if (PRptCtrId == null || PRptCtrId == string.Empty)
			{
				cmd.Parameters.Add("@PRptCtrId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@PRptCtrId", OleDbType.Numeric).Value = PRptCtrId;
			}
			if (RptElmId == null || RptElmId == string.Empty)
			{
				cmd.Parameters.Add("@RptElmId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptElmId", OleDbType.Numeric).Value = RptElmId;
			}
			if (RptCelId == null || RptCelId == string.Empty)
			{
				cmd.Parameters.Add("@RptCelId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptCelId", OleDbType.Numeric).Value = RptCelId;
			}
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetReportCha(string GenPrefix, string RptCtrId, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OleDbCommand cmd = new OleDbCommand("GetReportCha", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@RptCtrId", OleDbType.Numeric).Value = RptCtrId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetReportTbl(string GenPrefix, string RptCtrId, string ParentId, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OleDbCommand cmd = new OleDbCommand("GetReportTbl", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OleDbType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@RptCtrId", OleDbType.Numeric).Value = RptCtrId;
			if (ParentId == null || ParentId == string.Empty)
			{
				cmd.Parameters.Add("@ParentId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ParentId", OleDbType.Numeric).Value = ParentId;
			}
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}
	}
}