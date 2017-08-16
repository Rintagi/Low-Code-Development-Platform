namespace RO.Facade3
{
	using System;
	using System.Data;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.Rule3;

	public class GenWizardsSystem : MarshalByRefObject
	{
		public bool CreateProgram(Int32 wizardId, string wizardTitle, string dbAppDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
            return (new GenWizardsRules()).CreateProgram(wizardId, wizardTitle, dbAppDatabase, CPrj, CSrc, CTar, dbConnectionString, dbPassword);
		}

        //public void ProxyProgram(Int32 wizardId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    (new GenWizardsRules()).ProxyProgram(wizardId, CPrj, CSrc);
        //}

		public bool DeleteProgram(string programName, Int32 wizardId, string appDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar)
		{
			return (new GenWizardsRules()).DeleteProgram(programName, wizardId, appDatabase, CPrj, CSrc, CTar);
		}

		public DataTable GetWizardDel(string srcDatabase, string dbConnectionString, string dbPassword)
		{
			using (Access3.GenWizardsAccess dac = new Access3.GenWizardsAccess())
			{
				return dac.GetWizardDel(srcDatabase, dbConnectionString, dbPassword);
			}
		}
	}
}