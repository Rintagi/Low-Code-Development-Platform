namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
    using RO.Common3;
	using RO.SystemFramewk;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using System.Linq;

	public abstract class DeployAccessBase : Encryption, IDisposable
	{
        public abstract int AddSystem(string server, string desDBName, string user, string pwd, string nmSpace, string moduleName, string oledbconnectionstring);
        public abstract void BackupDb(string dbProviderCd, string connStr, string pwd, string dbName, string bkFile);
        public abstract void DbCreate(string connStr, string pwd, string dbName);
        public abstract void DbExec(string sql, string connStr, string pwd, string dbName);
        public abstract bool DbExists(string connStr, string pwd);
        public abstract void Dispose();
        public abstract void DropDb(string dbProviderCd, string connStr, string pwd, string dbName);
        public abstract List<string> FixMetaReference(string connStr, string pwd, string desDBName, string srcNmSpace, string nmSpace, string moduleName, List<string> modifiedBy, List<string> systemId, DataTable srcSystemMap, DataTable tarSystemMap);
        public abstract string GenRestoreScript(string dbProviderCd, string connStr, string pwd, string dbName, string iFileAbs, string rsFileRel, string dbPath);
        public abstract DataTable GetReleaseDtl(int releaseId);
        public abstract DataTable GetReleaseInf(int releaseId);
        public abstract DataTable GetReleaseVer(int ReleaseId, string EntityCode);
        public abstract DataTable GetTables(string connStr, string pwd, string dbName);
        public abstract DataTable GetYrReadme(int ReleaseId, string EntityCode);
        public abstract void MkDBOwner(string connStr, string usr, string pwd, string dbName);
        public abstract void OnlineDb(string connStr, string pwd, string dbName);
        public abstract void RestoreWaDb(string dbProviderCd, string connStr, string pwd, string waDb, string waFile, string waPath);
        public abstract void TransferTable(string srcServer, string srcDb, string srcUser, string srcPwd, string tbName, string tarServer, string tarDb, string tarUser, string tarPwd, System.Collections.Generic.Dictionary<string, KeyValuePair<string, List<string[]>>> needTranslate, System.Collections.Generic.Dictionary<System.Text.RegularExpressions.Regex, System.Text.RegularExpressions.MatchEvaluator> reSimple, System.Collections.Generic.Dictionary<System.Text.RegularExpressions.Regex, System.Text.RegularExpressions.MatchEvaluator> reScript);
        public abstract void TruncateTable(string connStr, string pwd, string tableName);
        public abstract void UpdateRelease(string server, string desDBName, string user, string pwd, string nmSpace, string moduleName);
    }
}