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

	public class DbScriptAccess : DbScriptAccessBase, IDisposable
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
	
		public DbScriptAccess()
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

		public override DataTable GetData(string InSql, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			OdbcCommand cmd = null;
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			if (IsFrSource)
			{
				cmd = new OdbcCommand(InSql,new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			}
			else
			{
				cmd = new OdbcCommand(InSql,new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CTar.TarConnectionString + DecryptString(CTar.TarDbPassword))));
			}
			cmd.CommandType = CommandType.Text;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataSet GetDataSet(string InSql, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			OdbcCommand cmd = null;
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			if (IsFrSource)
			{
				cmd = new OdbcCommand(InSql,new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			}
			else
			{
				cmd = new OdbcCommand(InSql,new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CTar.TarConnectionString + DecryptString(CTar.TarDbPassword))));
			}
			cmd.CommandType = CommandType.Text;
			da.SelectCommand = TransformCmd(cmd);
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
			OdbcCommand cmd = new OdbcCommand("sp_MShelpcolumns",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@tablename", OdbcType.VarChar).Value = "[dbo].[" + tbName + "]";
			cmd.Parameters.Add("@flags", OdbcType.Numeric).Value = 0;
			cmd.Parameters.Add("@orderby", OdbcType.VarChar).Value = "id";
			cmd.Parameters.Add("@flags2", OdbcType.Numeric).Value = 0;
			da.SelectCommand = TransformCmd(cmd);
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
			OdbcCommand cmd = new OdbcCommand("sp_MStablekeys",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@tablename", OdbcType.VarChar).Value = "[dbo].[" + tbName + "]";
			cmd.Parameters.Add("@colname", OdbcType.VarChar).Value = System.DBNull.Value;
			cmd.Parameters.Add("@type", OdbcType.Numeric).Value = 6;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetFKInfo(string tbName, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			OdbcCommand cmd = null;
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			if (IsFrSource)
			{
				cmd = new OdbcCommand("sp_MStablekeys",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			}
			else
			{
				cmd = new OdbcCommand("sp_MStablekeys",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CTar.TarConnectionString + DecryptString(CTar.TarDbPassword))));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@tablename", OdbcType.VarChar).Value = "[dbo].[" + tbName + "]";
			cmd.Parameters.Add("@colname", OdbcType.VarChar).Value = System.DBNull.Value;
			cmd.Parameters.Add("@type", OdbcType.Numeric).Value = 8;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override object ExecScript(string DataTier, string CmdName, string IsqlFile, bool IsFrSource, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword, Func<object, string, bool> processResult = null)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("ExecScript", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CmdName", OdbcType.VarChar).Value = CmdName;
			if (IsFrSource)
			{
				cmd.Parameters.Add("@DbServer", OdbcType.VarChar).Value = CSrc.SrcDbServer;
				cmd.Parameters.Add("@DbUserId", OdbcType.VarChar).Value = CSrc.SrcDbUserId;
				cmd.Parameters.Add("@DbPassword", OdbcType.VarChar).Value = DecryptString(CSrc.SrcDbPassword);
			}
			else
			{
				cmd.Parameters.Add("@DbServer", OdbcType.VarChar).Value = CTar.TarDbServer;
				cmd.Parameters.Add("@DbUserId", OdbcType.VarChar).Value = CTar.TarDbUserId;
				cmd.Parameters.Add("@DbPassword", OdbcType.VarChar).Value = DecryptString(CTar.TarDbPassword);
			}
			if (IsqlFile != string.Empty)
			{
				cmd.Parameters.Add("@DbDatabase", OdbcType.VarChar).Value = CTar.TarDbDatabase;
				cmd.Parameters.Add("@IsqlFile", OdbcType.VarChar).Value = IsqlFile;
				cmd.Parameters.Add("@DataTier", OdbcType.Char).Value = DataTier;
			}
			else	// BCP
			{
				cmd.Parameters.Add("@DbDatabase", OdbcType.VarChar).Value = System.DBNull.Value;
				cmd.Parameters.Add("@IsqlFile", OdbcType.VarChar).Value = System.DBNull.Value;
				cmd.Parameters.Add("@DataTier", OdbcType.Char).Value = System.DBNull.Value;
			}
			cmd.CommandTimeout = 6000;
			OdbcDataReader odr = null;
			try { 
                odr = TransformCmd(cmd).ExecuteReader();
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