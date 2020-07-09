namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class GenWizardsAccess : GenWizardsAccessBase, IDisposable
	{
		private OleDbDataAdapter da;
	
		public GenWizardsAccess()
		{
			da = new OleDbDataAdapter();
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
            OleDbConnection cn = new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SetWizNeedRegen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@wizardId", OleDbType.Numeric).Value = wizardId;
            try { cmd.ExecuteNonQuery(); }
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
			OleDbCommand cmd = new OleDbCommand("GetWizardById",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = CPrj.SrcDesDatabase;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = CSrc.SrcDbDatabase;
			cmd.Parameters.Add("@wizardId", OleDbType.Numeric).Value = wizardId;
			da.SelectCommand = cmd;
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
			OleDbCommand cmd = new OleDbCommand("GetWizardColumns",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@wizardId", OleDbType.Numeric).Value = wizardId;
			da.SelectCommand = cmd;
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
			OleDbCommand cmd = new OleDbCommand("GetWizardRule",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@wizardId", OleDbType.Numeric).Value = wizardId;
			da.SelectCommand = cmd;
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
			OleDbConnection cn =  new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("MkWizardW1Upd", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@wizardId", OleDbType.Numeric).Value = wizardId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			try {cmd.ExecuteNonQuery();}
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
			OleDbCommand cmd = new OleDbCommand("GetWizardDel",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = srcDatabase;
			da.SelectCommand = cmd;
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
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DelWizardDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = srcDatabase;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@programName", OleDbType.VarChar).Value = programName;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}
	}
}