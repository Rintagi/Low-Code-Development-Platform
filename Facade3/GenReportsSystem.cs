namespace RO.Facade3
{
	using System;
	using System.Data;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.Rule3;

	public class GenReportsSystem : MarshalByRefObject
	{
		public bool CreateProgram(string GenPrefix, Int32 reportId, string reportTitle, string dbAppDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
			return (new GenReportsRules()).CreateProgram(GenPrefix, reportId, reportTitle, dbAppDatabase, CPrj, CSrc, CTar, dbConnectionString, dbPassword);
		}

        //public void ProxyProgram(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    (new GenReportsRules()).ProxyProgram(GenPrefix, reportId, CPrj, CSrc);
        //}

		public bool DeleteProgram(string GenPrefix, string programName, Int32 reportId, string appDatabase, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar)
		{
			return (new GenReportsRules()).DeleteProgram(GenPrefix, programName, reportId, appDatabase, CPrj, CSrc, CTar);
		}

		public DataTable GetReportDel(string GenPrefix, string srcDatabase, string dbConnectionString, string dbPassword)
		{
			using (Access3.GenReportsAccess dac = new Access3.GenReportsAccess())
			{
				return dac.GetReportDel(GenPrefix, srcDatabase, dbConnectionString, dbPassword);
			}
		}
	}
}