namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class DbScriptAccess : DbScriptAccessBase, IDisposable
	{
		private OleDbDataAdapter da;
	
		public DbScriptAccess()
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

		public override DataTable GetData(string InSql, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			OleDbCommand cmd = null;
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			if (IsFrSource)
			{
				cmd = new OleDbCommand(InSql,new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			}
			else
			{
				cmd = new OleDbCommand(InSql,new OleDbConnection(CTar.TarConnectionString + DecryptString(CTar.TarDbPassword)));
			}
			cmd.CommandType = CommandType.Text;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataSet GetDataSet(string InSql, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			OleDbCommand cmd = null;
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			if (IsFrSource)
			{
				cmd = new OleDbCommand(InSql,new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			}
			else
			{
				cmd = new OleDbCommand(InSql,new OleDbConnection(CTar.TarConnectionString + DecryptString(CTar.TarDbPassword)));
			}
			cmd.CommandType = CommandType.Text;
			da.SelectCommand = cmd;
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}

		public override DataTable GetColumnInfo(string tbName, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("sp_MShelpcolumns",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@tablename", OleDbType.VarChar).Value = "[dbo].[" + tbName + "]";
			cmd.Parameters.Add("@flags", OleDbType.Numeric).Value = 0;
			cmd.Parameters.Add("@orderby", OleDbType.VarChar).Value = "id";
			cmd.Parameters.Add("@flags2", OleDbType.Numeric).Value = 0;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetColumnInfo", "sp_MShelpcolumns", "Columns not available for table '" + tbName + "'!");
			return dt;
		}

		public override DataTable GetPKInfo(string tbName, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("sp_MStablekeys",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@tablename", OleDbType.VarChar).Value = "[dbo].[" + tbName + "]";
			cmd.Parameters.Add("@colname", OleDbType.VarChar).Value = System.DBNull.Value;
			cmd.Parameters.Add("@type", OleDbType.Numeric).Value = 6;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetFKInfo(string tbName, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			OleDbCommand cmd = null;
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			if (IsFrSource)
			{
				cmd = new OleDbCommand("sp_MStablekeys",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			}
			else
			{
				cmd = new OleDbCommand("sp_MStablekeys",new OleDbConnection(CTar.TarConnectionString + DecryptString(CTar.TarDbPassword)));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@tablename", OleDbType.VarChar).Value = "[dbo].[" + tbName + "]";
			cmd.Parameters.Add("@colname", OleDbType.VarChar).Value = System.DBNull.Value;
			cmd.Parameters.Add("@type", OleDbType.Numeric).Value = 8;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override object ExecScript(string DataTier, string CmdName, string IsqlFile, bool IsFrSource, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword, Func<object, string, bool> processResult)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("ExecScript", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CmdName", OleDbType.VarChar).Value = CmdName;
			if (IsFrSource)
			{
				cmd.Parameters.Add("@DbServer", OleDbType.VarChar).Value = CSrc.SrcDbServer;
				cmd.Parameters.Add("@DbUserId", OleDbType.VarChar).Value = CSrc.SrcDbUserId;
				cmd.Parameters.Add("@DbPassword", OleDbType.VarChar).Value = DecryptString(CSrc.SrcDbPassword);
			}
			else
			{
				cmd.Parameters.Add("@DbServer", OleDbType.VarChar).Value = CTar.TarDbServer;
				cmd.Parameters.Add("@DbUserId", OleDbType.VarChar).Value = CTar.TarDbUserId;
				cmd.Parameters.Add("@DbPassword", OleDbType.VarChar).Value = DecryptString(CTar.TarDbPassword);
			}
			if (IsqlFile != string.Empty)
			{
				cmd.Parameters.Add("@DbDatabase", OleDbType.VarChar).Value = CTar.TarDbDatabase;
				cmd.Parameters.Add("@IsqlFile", OleDbType.VarChar).Value = IsqlFile;
				cmd.Parameters.Add("@DataTier", OleDbType.Char).Value = DataTier;
			}
			else	// BCP
			{
				cmd.Parameters.Add("@DbDatabase", OleDbType.VarChar).Value = System.DBNull.Value;
				cmd.Parameters.Add("@IsqlFile", OleDbType.VarChar).Value = System.DBNull.Value;
				cmd.Parameters.Add("@DataTier", OleDbType.Char).Value = System.DBNull.Value;
			}
			cmd.CommandTimeout = 6000;
			OleDbDataReader odr = null;
			try { 
                odr = cmd.ExecuteReader();
                bool stop = false;
                if (processResult != null)
                {
                    while (!stop && odr.Read())
                    {
                        stop = processResult(odr.GetValue(0), odr.GetString(0));
                    }
                }
            }
			catch (Exception e) { ApplicationAssert.CheckCondition(false, "ExecScript", "ExecuteReader", e.Message.ToString()); }
			return odr;
			// cn.Close(); To be handled by garbage collection.
		}
	}
}