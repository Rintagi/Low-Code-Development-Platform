namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public abstract class GenWizardsAccessBase : Encryption, IDisposable
	{
        public abstract void DelWizardDel(string srcDatabase, string appDatabase, string desDatabase, string programName, string dbConnectionString, string dbPassword);
        public abstract void Dispose();
        public abstract DataTable GetWizardById(Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetWizardColumns(Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetWizardDel(string srcDatabase, string dbConnectionString, string dbPassword);
        public abstract DataTable GetWizardRule(Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract void MkWizardW1Upd(Int32 wizardId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase);
        public abstract void SetWizNeedRegen(Int32 wizardId, CurrSrc CSrc);
    }
}