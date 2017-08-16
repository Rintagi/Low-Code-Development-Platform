namespace RO.Facade3
{
	using System;
	using System.Data;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.Rule3;

	public class GenScreensSystem : MarshalByRefObject
	{
		public bool CreateProgram(Int32 screenId, string screenTitle, string dbAppDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
            return (new GenScreensRules()).CreateProgram(screenId, screenTitle, dbAppDatabase, CPrj, CSrc, CTar, dbConnectionString, dbPassword);
		}

        //public void ProxyProgram(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    (new GenScreensRules()).ProxyProgram(screenId, CPrj, CSrc);
        //}

		public bool DeleteProgram(string programName, Int32 screenId, string appDatabase, string multiDesignDb, string sysProgram, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar)
		{
			return (new GenScreensRules()).DeleteProgram(programName, screenId, appDatabase, multiDesignDb, sysProgram, CPrj, CSrc, CTar);
		}

		public DataTable GetScreenDel(string srcDatabase, string dbConnectionString, string dbPassword)
		{
			using (Access3.GenScreensAccess dac = new Access3.GenScreensAccess())
			{
				return dac.GetScreenDel(srcDatabase, dbConnectionString, dbPassword);
			}
		}
	}
}