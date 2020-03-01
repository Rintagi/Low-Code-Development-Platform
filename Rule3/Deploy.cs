namespace RO.Rule3
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.IO;
    using System.Xml;
    using RO.SystemFramewk;
    using RO.Access3;
    using RO.Common3;
    using RO.Common3.Data;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.Win32;

    public class Deploy : Encryption
    {
        public new const string ROVersion = "20200228";
        private StringBuilder sbd;	// version control design meta data
        private StringBuilder sba;	// version control application meta data
        private StringBuilder si;	// InstallItem.cs for installation

        public Deploy()
        {
            sbd = new StringBuilder("");
            sba = new StringBuilder("");
            si = new StringBuilder("");
        }
        public string PrepInstall(int releaseId, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
        {
            throw new NotImplementedException("This feature is not available in community version, please acquire proper Rintagi license for this feature");
        }

        private void DbCreate(string connStr, string pwd, string dbName)
        {
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                dac.DbCreate(connStr, pwd, dbName);
            }
        }


        private CurrSrc GetSrc(string providerOle, string serverName, string dbServer, string database, string userId, string password)
        {
            CurrSrc CSrc = new CurrSrc(false, null);
            CSrc.SrcServerName = serverName;
            CSrc.SrcDbProvider = providerOle;
            CSrc.SrcDbServer = dbServer;
            CSrc.SrcDbDatabase = database;
            CSrc.SrcDbUserId = userId;
            CSrc.SrcDbPassword = password;
            return CSrc;
        }

        private CurrTar GetTar(string providerOle, string serverName, string dbServer, string database, string userId, string password)
        {
            CurrTar CTar = new CurrTar(false, null);
            CTar.TarServerName = serverName;
            CTar.TarDbProvider = providerOle;
            CTar.TarDbServer = dbServer;
            CTar.TarDbDatabase = database;
            CTar.TarDbUserId = userId;
            CTar.TarDbPassword = password;
            return CTar;
        }

        public List<string> TransferModule(string srcServerName, string srcUser, string srcPwd, string srcDbName, string srcNmSpace, string srcModule, string transferTables, string[] translateColumns, string tarServerName, string tarUser, string tarPwd, string tarNmSpace, string tarModule, bool frameOnly, bool updateRel, bool updSystem)
        {
            string metaContent = ""; // table content to be transfer in the 'D' database for new module

            throw new NotImplementedException("feature not implemented");
        }
    }
}