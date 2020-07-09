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

	public class GenReportsAccess : GenReportsAccessBase, IDisposable
	{
		private OdbcDataAdapter da;

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
	
		public GenReportsAccess()
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

        public override void SetRptNeedRegen(Int32 reportId, CurrSrc CSrc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("SetRptNeedRegen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetRptNeedRegen", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

		public override DataTable GetReportById(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetReportById",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = CPrj.SrcDesDatabase;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = CSrc.SrcDbDatabase;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetReportById", "Report Issue", "Information for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetReportColumns(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetReportColumns",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetReportColumns", "Report Columns Issue", "Columns for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetCriReportGrp(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetCriReportGrp",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetCriReportGrp", "Report Issue", "Report Group not available for Report #'" + reportId.ToString() + "'!");
			return dt;
		}

		public override DataTable GetReportCriteria(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetReportCriteria",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetReportCriteria", "Report Criteria Issue", "Criteria for Report #'" + reportId.ToString() + "' not available!");
			return dt;
		}

		public override void MkReportGet(string GenPrefix, Int32 reportId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("MkReportGet", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		// Do not erase stored procedure as this is also called from SqlReportAccess.cs differently:
		public override void MkReportGetIn(string GenPrefix, Int32 reportCriId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("MkReportGetIn", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportCriId", OdbcType.Numeric).Value = reportCriId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		public override void MkReportUpd(string GenPrefix, Int32 reportId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("MkReportUpd", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		public override DataTable GetReportDel(string GenPrefix, string srcDatabase, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetReportDel",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = srcDatabase;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void DelReportDel(string GenPrefix, string srcDatabase, string appDatabase, string desDatabase, string programName, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("DelReportDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = srcDatabase;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@programName", OdbcType.VarChar).Value = programName;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}

		public override DataTable GetReportCriDel(string GenPrefix, Int32 reportId, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetReportCriDel",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void DelReportCriDel(string GenPrefix, string appDatabase, Int32 reportId, string procedureName, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("DelReportCriDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}

		public override DataTable GetReportElm(string GenPrefix, Int32 reportId, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OdbcCommand cmd = new OdbcCommand("GetReportElm", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@reportId", OdbcType.Numeric).Value = reportId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetReportCtr(string GenPrefix, string PRptCtrId, string RptElmId, string RptCelId, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OdbcCommand cmd = new OdbcCommand("GetReportCtr", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			if (PRptCtrId == null || PRptCtrId == string.Empty)
			{
				cmd.Parameters.Add("@PRptCtrId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@PRptCtrId", OdbcType.Numeric).Value = PRptCtrId;
			}
			if (RptElmId == null || RptElmId == string.Empty)
			{
				cmd.Parameters.Add("@RptElmId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptElmId", OdbcType.Numeric).Value = RptElmId;
			}
			if (RptCelId == null || RptCelId == string.Empty)
			{
				cmd.Parameters.Add("@RptCelId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@RptCelId", OdbcType.Numeric).Value = RptCelId;
			}
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetReportCha(string GenPrefix, string RptCtrId, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OdbcCommand cmd = new OdbcCommand("GetReportCha", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@RptCtrId", OdbcType.Numeric).Value = RptCtrId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetReportTbl(string GenPrefix, string RptCtrId, string ParentId, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OdbcCommand cmd = new OdbcCommand("GetReportTbl", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@GenPrefix", OdbcType.VarChar).Value = GenPrefix;
			cmd.Parameters.Add("@RptCtrId", OdbcType.Numeric).Value = RptCtrId;
			if (ParentId == null || ParentId == string.Empty)
			{
				cmd.Parameters.Add("@ParentId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@ParentId", OdbcType.Numeric).Value = ParentId;
			}
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}
	}
}