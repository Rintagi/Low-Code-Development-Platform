namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.SystemFramewk;

	public abstract class DbPortingAccessBase : Encryption, IDisposable
	{
		public abstract void Dispose();
        public abstract DataTable GetMapTable(Int32 ProjectId, string TableName, string dbConnectionString, string dbPassword);
    }
}