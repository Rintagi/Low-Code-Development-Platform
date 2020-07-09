namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public abstract class DbScriptAccessBase : Encryption, IDisposable
	{
        public abstract void Dispose();
        public abstract DataTable GetData(string InSql, bool IsFrSource, CurrSrc CSrc, CurrTar CTar);
        public abstract DataSet GetDataSet(string InSql, bool IsFrSource, CurrSrc CSrc, CurrTar CTar);
        public abstract DataTable GetColumnInfo(string tbName, CurrSrc CSrc);
        public abstract DataTable GetPKInfo(string tbName, CurrSrc CSrc);
        public abstract DataTable GetFKInfo(string tbName, bool IsFrSource, CurrSrc CSrc, CurrTar CTar);
        public abstract object ExecScript(string DataTier, string CmdName, string IsqlFile, bool IsFrSource, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword, Func<object, string, bool> processResult);
    }
}