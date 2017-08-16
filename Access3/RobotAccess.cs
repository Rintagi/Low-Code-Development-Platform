namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;

	public class RobotAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;
	
		public RobotAccess()
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

		public DataTable GetEntityList()
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbCommand cmd = new OleDbCommand("GetEntityList", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetClientTier(Int16 EntityId)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbCommand cmd = new OleDbCommand("GetClientTier", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@EntityId", OleDbType.Numeric).Value = EntityId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetRuleTier(Int16 EntityId)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbCommand cmd = new OleDbCommand("GetRuleTier", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@EntityId", OleDbType.Numeric).Value = EntityId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetDataTier(Int16 EntityId)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbCommand cmd = new OleDbCommand("GetDataTier", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@EntityId", OleDbType.Numeric).Value = EntityId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetCustomList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetCustomList",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@searchTxt", OleDbType.VarChar).Value = searchTxt;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetScreenList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenList",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@searchTxt", OleDbType.VarChar).Value = searchTxt;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetReportList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetReportList",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@searchTxt", OleDbType.VarChar).Value = searchTxt;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetWizardList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetWizardList",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@searchTxt", OleDbType.VarChar).Value = searchTxt;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public int ExecSql(string InSql, string dbConnectionString, string dbPassword)
		{
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand(InSql,cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 6000;
			int rtn = cmd.ExecuteNonQuery();
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;;
		}
	}
}