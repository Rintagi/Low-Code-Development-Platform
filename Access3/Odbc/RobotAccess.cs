namespace RO.Access3.Odbc
{
	using System;
	using System.Data;
	//using System.Data.OleDb;
    using System.Data.Odbc;
    using System.Linq;
	using RO.Common3;

	public class RobotAccess : RobotAccessBase, IDisposable
	{
		private OdbcDataAdapter da;
	
		public RobotAccess()
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

		public override DataTable GetEntityList()
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcCommand cmd = new OdbcCommand("GetEntityList", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetClientTier(Int16 EntityId)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcCommand cmd = new OdbcCommand("GetClientTier", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@EntityId", OdbcType.Numeric).Value = EntityId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetRuleTier(Int16 EntityId)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcCommand cmd = new OdbcCommand("GetRuleTier", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@EntityId", OdbcType.Numeric).Value = EntityId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetDataTier(Int16 EntityId)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcCommand cmd = new OdbcCommand("GetDataTier", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@EntityId", OdbcType.Numeric).Value = EntityId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetCustomList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetCustomList",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@searchTxt", OdbcType.VarChar).Value = searchTxt;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetScreenList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenList",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@searchTxt", OdbcType.VarChar).Value = searchTxt;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetReportList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetReportList",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@searchTxt", OdbcType.VarChar).Value = searchTxt;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetWizardList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetWizardList",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@searchTxt", OdbcType.VarChar).Value = searchTxt;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override int ExecSql(string InSql, string dbConnectionString, string dbPassword)
		{
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand(InSql,cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 6000;
			int rtn = TransformCmd(cmd).ExecuteNonQuery();
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;;
		}
	}
}