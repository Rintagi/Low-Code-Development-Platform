namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;

	public abstract class RobotAccessBase : Encryption, IDisposable
	{
		public abstract void Dispose();
        public abstract int ExecSql(string InSql, string dbConnectionString, string dbPassword);
        public abstract DataTable GetClientTier(Int16 EntityId);
        public abstract DataTable GetCustomList(string searchTxt, string dbConnectionString, string dbPassword);
        public abstract DataTable GetDataTier(Int16 EntityId);
        public abstract DataTable GetEntityList();
        public abstract DataTable GetReportList(string searchTxt, string dbConnectionString, string dbPassword);
        public abstract DataTable GetRuleTier(Int16 EntityId);
        public abstract DataTable GetScreenList(string searchTxt, string dbConnectionString, string dbPassword);
        public abstract DataTable GetWizardList(string searchTxt, string dbConnectionString, string dbPassword);
    }
}