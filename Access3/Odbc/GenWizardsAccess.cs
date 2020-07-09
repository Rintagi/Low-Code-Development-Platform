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

	public class GenWizardsAccess : GenWizardsAccessBase, IDisposable
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
	
		public GenWizardsAccess()
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

        public override void SetWizNeedRegen(Int32 wizardId, CurrSrc CSrc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("SetWizNeedRegen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@wizardId", OdbcType.Numeric).Value = wizardId;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetWizNeedRegen", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

		public override DataTable GetWizardById(Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetWizardById",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = CPrj.SrcDesDatabase;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = CSrc.SrcDbDatabase;
			cmd.Parameters.Add("@wizardId", OdbcType.Numeric).Value = wizardId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetWizardById", "Wizard Issue", "Information for Wizard #'" + wizardId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetWizardColumns(Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetWizardColumns",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@wizardId", OdbcType.Numeric).Value = wizardId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetWizardColumns", "Wizard Columns Issue", "Columns for Wizard #'" + wizardId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetWizardRule(Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetWizardRule",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@wizardId", OdbcType.Numeric).Value = wizardId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void MkWizardW1Upd(Int32 wizardId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("MkWizardW1Upd", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@wizardId", OdbcType.Numeric).Value = wizardId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		public override DataTable GetWizardDel(string srcDatabase, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetWizardDel",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = srcDatabase;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void DelWizardDel(string srcDatabase, string appDatabase, string desDatabase, string programName, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("DelWizardDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = srcDatabase;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@programName", OdbcType.VarChar).Value = programName;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}
	}
}