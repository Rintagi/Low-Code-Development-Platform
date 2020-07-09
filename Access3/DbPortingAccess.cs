namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.SystemFramewk;

	public class DbPortingAccess : DbPortingAccessBase, IDisposable
	{
		private OleDbDataAdapter da;
	
		public DbPortingAccess()
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

		public override DataTable GetMapTable(Int32 ProjectId, string TableName, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			da = new OleDbDataAdapter("SELECT * FROM " + TableName + " WHERE ProjectId = " + ProjectId.ToString(), dbConnectionString + DecryptString(dbPassword));
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}
	}
}