namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using System.Drawing;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public abstract class MenuAccessBase : Encryption, IDisposable
	{
		public abstract void Dispose();
        public abstract DataTable GetMenu(Int16 CultureId, byte SystemId, UsrImpr ui, string dbConnectionString, string dbPassword, int? ScreenId, int? ReportId, int? WizardId);
        public abstract void NewMenuItem(Int32 ScreenId, Int32 ReportId, Int32 WizardId, string ItemTitle, string dbConnectionString, string dbPassword);
    }
}