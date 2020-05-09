using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;
using System.Security.Cryptography;
using System.Xml;
using System.Configuration;
#if USE_J_SHARP
using java.io;
using java.util;
using java.util.zip;
#endif
using System.Security.AccessControl;
using System.Linq;
using Microsoft.Win32;
using System.Text.RegularExpressions;

//using Ionic.Utils.Zip;
 
namespace Install
{
    [SerializableAttribute]

    public class Credential
    {
        private string pLoginName;
        private byte[] pPassword;

        public Credential() { }

        public Credential(string LoginName, string Password)
        {
            pLoginName = LoginName;
            SHA1 sha1 = SHA1.Create();
            pPassword = sha1.ComputeHash(Encoding.Unicode.GetBytes(Password));
        }

        public string LoginName
        {
            get { return pLoginName; }
            set { pLoginName = value; }
        }

        public byte[] Password
        {
            get { return pPassword; }
            set { pPassword = value; }
        }
    }
    public class CmdArgExtractor
    {
        /// <summary>
        /// An array of valid prefixes, e.g. "/a:", "/b:" when you expect a user to
        /// pass args in the format "/a:alpha /b:beta".
        /// </summary>
        private string[] _validPrefixes;

        /// <summary>
        /// Args separator, e.g. '/' if you expect a user to pass args
        ///  in the format "/a:alpha /b:beta".
        /// </summary>
        private char _argsSep;

        /// <summary>
        /// Args value separator, e.g. ':' if you expect a user to pass args
        ///  in the format "/a:alpha /b:beta".
        /// </summary>
        private char _argsValSep;

        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        /// <param name="validPrefixes">The valid prefixes, e.g. in "/a:alpha /b:beta"
        /// validPrefixes will be "/a:" and "/b:"</param>
        /// <param name="argsSep">The args separator - "/" in the above example.</param>
        /// <param name="argsValSep">The args value separator, ":" in the above example.</param>
        public CmdArgExtractor(string[] validPrefixes, char argsSep, char argsValSep)
        {
            //TODO: Trhow an error if _validPrefixes not present
            if (validPrefixes.Length == 0)
            {
                throw new Exception("validPrefixes array has no values.");
            }

            if (argsValSep.ToString().Trim().Length == 0)
            {
                throw new Exception("argsValSep cannot be blank.");
            }

            if (argsSep.ToString().Trim().Length == 0)
            {
                throw new Exception("argsSep cannot be blank.");
            }

            this._validPrefixes = (from x in validPrefixes select x.ToLower()).ToArray<string>();
            this._argsSep = argsSep;
            this._argsValSep = argsValSep;
        }


        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        /// <param name="argsSep">The args separator,e.g "/" in "/argA"</param>
        /// <remarks>Can be used it when you expect args in the following format: "/argA /argB"</remarks>
        public CmdArgExtractor(char argsSep)
        {
            if (argsSep.ToString().Trim() == null)
            {
                throw new Exception("argsSep cannot be blank.");
            }
            this._argsSep = argsSep;
        }

        /// <summary>
        /// Validates the command arguments prefixes.
        /// </summary>
        /// <param name="args">An array of arguments passes at Command Prompt.</param>
        /// <returns>invalid argument list</returns>
        public List<string> InvalidArgsPrefixes(string[] args)
        {
            List<string> invalidArgs = new List<string>();
 
            //TODO: Trhow an error if _validPrefixes not present
            if (this._validPrefixes.Length == 0)
            {
                throw new Exception("validPrefixes array has no values.");
            }

            foreach (string x in args)
            {
                string[] arg = x.Split(new char[] { _argsValSep }, 2, StringSplitOptions.RemoveEmptyEntries);
                string p = arg[0] + (arg.Length > 1 ? _argsValSep.ToString() : "");
                if (!_validPrefixes.Contains(p.ToLower())) invalidArgs.Add(x);
            }

            return invalidArgs;
        }


        /// <summary>
        /// Gets dictionary containing arg values and qualifiers.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>dictionary of arg/value pair</returns>
        public Dictionary<string, string> GetArgs(string[] args)
        {
            Dictionary<string, string> kv = new Dictionary<string, string>();

            char[] separators = new char[] { this._argsSep, this._argsValSep };

            foreach (string arg in args) 
            {
                string[] aV = arg.Split(separators, 2, StringSplitOptions.RemoveEmptyEntries);
                if (aV.Length == 2)
                {
                    kv[aV[0].ToLower()] = aV[1].Replace("\"","");
                }
                else
                {
                    kv[aV[0].ToLower()] = "Y";
                }
            }
            return kv;
        }
    }

	public class Utils
	{
        private static string MSQLProvider = "Sqloledb";
		private const string AspExt2Replace = ",rdlc,rdl,config,";

        //private const string AspExt2Replace = ",master,rdl,asmx,asax,aspx,ascx,cs,config,";
        public static Action<string> errorMsg { get; set; }
        public static Func<string, string, DialogResult> msgBox { get; set; }
        public static string GetConnString(string DbProvider, string Svr, string Usr, string Pwd, bool bIntegratedSecurity)
        {
            string cs;
            if (DbProvider == "M" || DbProvider != "S")
            {
                // Sqloldeb which doesn't support TLS 1.2
                // MSOLEDBSQL supports TLS 1.2 but requires additional installed package
                cs = "Provider=" + (DbProvider == "M" ? MSQLProvider : DbProvider) + ";Data Source=\"" + Svr + "\";Connect Timeout=50;" + (bIntegratedSecurity ? "Integrated Security=sspi" : "User ID=\"" + Usr + "\";password=\"" + Pwd + "\"");
            }
            else
            {
                cs = "Provider=Sybase.ASEOLEDBProvider;Server Name=\"" + Svr + "\";Connect Timeout=50;OLE DB Services=-4;User ID=\"" + Usr + "\";password=\"" + Pwd + "\"";
            }
            return cs;
        }
        public static bool TestSQLClient(int ver)
        {
            bool has2005 = false;
            bool has2008 = false;
            bool has2012 = false;
            bool has2014 = false;
            bool has2016 = false;
            try
            {
                using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\90"))
                {
                    has2005 = System.IO.File.Exists(sqlServerKey.GetValue("VerSpecificRootDir").ToString() + @"Tools\Binn\bcp.exe");
                }
            }
            catch { }
            try
            {
                using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\100"))
                {
                    has2008 = System.IO.File.Exists(sqlServerKey.GetValue("VerSpecificRootDir").ToString() + @"Tools\Binn\bcp.exe");
                }
            }
            catch { }

            try
            {
                using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\110"))
                {
                    has2012 = System.IO.File.Exists(sqlServerKey.GetValue("VerSpecificRootDir").ToString() + @"Tools\Binn\bcp.exe");
                }
            }
            catch { }

            try
            {
                using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\120"))
                {
                    has2014 = System.IO.File.Exists(sqlServerKey.GetValue("VerSpecificRootDir").ToString().Replace(@"\120\",@"\110\Tools\Binn\bcp.exe"));
                }
            }
            catch { }
            try
            {
                using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\130"))
                {
                    has2016 = System.IO.File.Exists(sqlServerKey.GetValue("VerSpecificRootDir").ToString().Replace(@"\130\", @"\110\Tools\Binn\bcp.exe"));
                }
            }
            catch { }

            if (ver == 9) return has2005;
            else if (ver == 10) return has2008;
            else if (ver == 11) return has2012;
            else if (ver == 12) return has2014;
            else if (ver == 13) return has2016;
            else return false;
        }

        public static KeyValuePair<string, string> GetSQLBcpPath()
        {
            foreach (string ver in new string[] { "90", "100", "110", "120", "130", "140", "150", "160" })
            {
                try
                {
                    using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\" + ver))
                    {
                        string basePath = (sqlServerKey.GetValue("VerSpecificRootDir") ?? "").ToString();
                        string bcpPath = basePath + @"Tools\Binn\bcp.exe";
                        string bcpAltPath = basePath.Replace(@"\" + ver + @"\", @"\Client SDK\ODBC\" + (ver == "120" || ver == "110" ? "110" : "130") + @"\Tools\Binn\bcp.exe");
                        //string bcpAlt2Path = basePath.Replace(@"\" + ver + @"\", @"\Client SDK\ODBC\" + (ver == "120" ? "110" : "130") + @"\Tools\Binn\bcp.exe");
                        if (System.IO.File.Exists(bcpPath)) return new KeyValuePair<string, string>(ver, bcpPath);
                        else if (System.IO.File.Exists(bcpAltPath)) return new KeyValuePair<string, string>(ver, bcpAltPath);
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\" + ver + @"\Tools\ClientSetup"))
                    {
                        string basePath = (sqlServerKey.GetValue("ODBCToolsPath") ?? "").ToString();
                        string altBasePath = (sqlServerKey.GetValue("Path") ?? "").ToString();
                        string bcpPath = basePath + @"\bcp.exe";
                        string bcpAltPath = altBasePath + @"\bcp.exe";
                        if (System.IO.File.Exists(bcpPath)) return new KeyValuePair<string, string>(ver, bcpPath);
                        else if (System.IO.File.Exists(bcpAltPath)) return new KeyValuePair<string, string>(ver, bcpAltPath);
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server\" + ver))
                    {
                        string basePath = (sqlServerKey.GetValue("VerSpecificRootDir") ?? "").ToString();
                        string bcpPath = basePath + @"Tools\Binn\bcp.exe";
                        string bcpAltPath = basePath.Replace(@"\" + ver + @"\", @"\Client SDK\ODBC\" + (ver == "120" || ver == "110" ? "110" : "130") + @"\Tools\Binn\bcp.exe");
                        //string bcpAlt2Path = basePath.Replace(@"\" + ver + @"\", @"\Client SDK\ODBC\" + (ver == "120" ? "110" : "130") + @"\Tools\Binn\bcp.exe");
                        if (System.IO.File.Exists(bcpPath)) return new KeyValuePair<string, string>(ver, bcpPath);
                        else if (System.IO.File.Exists(bcpAltPath)) return new KeyValuePair<string, string>(ver, bcpAltPath);
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    using (RegistryKey sqlServerKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server\" + ver + @"\Tools\ClientSetup"))
                    {
                        string basePath = (sqlServerKey.GetValue("ODBCToolsPath") ?? "").ToString();
                        string altBasePath = (sqlServerKey.GetValue("Path") ?? "").ToString();
                        string bcpPath = basePath + @"\bcp.exe";
                        string bcpAltPath = altBasePath + @"\bcp.exe";
                        if (System.IO.File.Exists(bcpPath)) return new KeyValuePair<string, string>(ver, bcpPath);
                        else if (System.IO.File.Exists(bcpAltPath)) return new KeyValuePair<string, string>(ver, bcpAltPath);
                    }
                }
                catch (Exception)
                {
                }
            }
            return new KeyValuePair<string, string>();
        }

        public static bool TestSQL(string DbProvider, string Svr, string Usr, string Pwd, bool bIntegratedSecurity)
        {
            bool sqlOK = false;
            string cs = GetConnString(DbProvider == "M" ? "MSOLEDBSQL" : DbProvider, Svr, Usr, Pwd, bIntegratedSecurity);
            
            OleDbConnection cn = new OleDbConnection(cs);
            try
            {
                cn.Open();
                sqlOK = true;
                MSQLProvider = "MSOLEDBSQL";
            }
            catch (Exception ex) {
                if (!ex.Message.Contains("MSOLEDBSQL"))
                {
                    ReportError(ex.Message + (ex.Message.Contains("MSOLEDBSQL") ? "\r\n will use Sqloledb as fallback, please install the package if TLS 1.1 or below is disabled" : ""));
                }
            }
			finally { cn.Close(); }

            if (!sqlOK && DbProvider == "M")
            {
                cs = GetConnString("Sqloledb", Svr, Usr, Pwd, bIntegratedSecurity);
                cn = new OleDbConnection(cs);
                try
                {
                    cn.Open();
                    sqlOK = true;
                    MSQLProvider = "Sqloledb";
                }
                catch (Exception ex) { 
                    ReportError(ex.Message + (!ex.Message.Contains("SSL") ? "" : "\r\nyou may need to install MSOLEDBSQL driver if TLS 1.1 or below is disabled and you have general connection problem")); 
                }
                finally { cn.Close(); }
            }

            return sqlOK;
        }
		public static void ExtractBinRsc(string ResourceName, string TargetName)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			int bsize = 16384;
			byte[] buffer = new byte[bsize];
			int cnt = bsize;

			BinaryReader br = new BinaryReader(assembly.GetManifestResourceStream("Install." + ResourceName));
			if (br == null) { throw new Exception("Cannot open source file '" + ResourceName + "'. Please try again."); }
			BinaryWriter bw = new BinaryWriter(new FileInfo(TargetName).Open(FileMode.Create));
			if (bw == null) { br.Close(); throw new Exception("Cannot open target file '" + TargetName + "'. Please try again."); }
			try
			{
				while (bsize == cnt)
				{
					cnt = br.Read(buffer, 0, bsize);
					bw.Write(buffer, 0, cnt);
				}
			}
			catch (Exception ex) { ReportError(ex.Message); }
			finally { br.Close(); bw.Close(); }
		}

		public static void ExtractTxtRsc(string DbProvider, string ResourceName, string oldNS, string newNS, List<string> modules, List<string> moduleDesigns, bool isNew, string serverVer, bool bIntegratedSecurity)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("Install." + ResourceName));
			if (sr == null) { throw new Exception("Cannot open source file '" + ResourceName + "'. Please try again."); }
			StreamWriter sw = new StreamWriter(ResourceName);
			string line = string.Empty;
            KeyValuePair<string, string> bcpPath = GetSQLBcpPath();
            serverVer = bcpPath.Key;
            //string bcpDir = bcpPath.Value.Contains("Client SDK") ? @"\Client SDK\ODBC\" + (serverVer == "120" ? "110" : "130") + @"\Tools\Binn\" : @"\" + serverVer + @"\Tools\Binn\";
            string bcpDir = new System.Text.RegularExpressions.Regex("(.)bcp.exe(.*$)", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Replace(bcpPath.Value, (m) => m.Groups[1].Value);
            System.Text.RegularExpressions.Regex lineBcpRegex = new System.Text.RegularExpressions.Regex("(\"?)(.*)(bcp.*$)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex lineSqmcmdRegex = new System.Text.RegularExpressions.Regex("(\"?)(.*)(sqlcmd.*$)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            try
			{
				while ((line = sr.ReadLine()) != null)
				{
                    if (DbProvider == "M")
                    {
                        /* if the original .bat is already coming from server 2014/2016 development environment, we normalize it back to the old style file first 
                         * basically "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\110\Tools\Binn\" => "C:\Program Files\Microsoft SQL Server\120\Tools\Binn\" 
                         * 2015.7.9 gary
                         * handle 130(SQL 2016) as well
                         * 2016.8.15 gary
                         */
                        line = line.Replace(@"\Client SDK\ODBC\130\Tools\Binn\", @"\" + serverVer + @"\Tools\Binn\").Replace(@"\Client SDK\ODBC\110\Tools\Binn\", @"\" + serverVer + @"\Tools\Binn\");

                        // normalize case to c
                        line = line.Replace(@"\binn\", @"\Binn\");
                        string[] vers = { "80", "90", "100", "110", "120", "130", "140", "150", "160" };

                        // standardize
                        foreach (var v in vers)
                        {
                            line = line.Replace(@"Server\" + v, @"Server\" + serverVer);
                        }
                        /* server 2014 onward changed the whole directory structure for the location of bcp thus the generated .bat file would not work anymore just from the above simple replace
                         * must change the whole path structure
                         * basically "C:\Program Files\Microsoft SQL Server\100\Tools\Binn\" => "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\110\Tools\Binn\"
                         * 2015.7.9 gary
                         * microsoft change the package for the bcp utility again and only up to 110
                         * must install SQL 2012 command line utilities
                         */
                        //line = line.Replace(@"\" + serverVer + @"\Tools\Binn\", bcpDir);
                        line = lineSqmcmdRegex.Replace(lineBcpRegex.Replace(line, (m) => (m.Groups[1].Value + bcpDir + m.Groups[3].Value)), (m) => (m.Groups[1].Value + bcpDir + m.Groups[3].Value));
        
                        //if (serverVer == "120")
                        //{
                        //    line = line.Replace(@"\120\Tools\Binn\", @"\Client SDK\ODBC\110\Tools\Binn\");
                        //}
                        //else if (serverVer == "130")
                        //{
                        //    line = line.Replace(@"\130\Tools\Binn\", @"\Client SDK\ODBC\110\Tools\Binn\");
                        //}
                    }
                    if (bIntegratedSecurity)
                    {
                        if (line.ToLower().Contains("bcp")) line = line.Replace("-U %2 -P %3", "-T");
                        else if (line.ToLower().Contains("sqlcmd")) line = line.Replace("-U %2 -P %3", "");
                    }
                    sw.WriteLine(ReplaceNmSp(line, oldNS, newNS, modules, moduleDesigns, isNew));
				}
			}
			catch (Exception ex) { ReportError(ex.Message); }
			finally { sr.Close(); sw.Close(); }
        }

        public static List<List<string>> GetModuleDbs(string newNS, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
        {
            List<string> modules = new List<string>();
            List<string> modulesDesigns = new List<string>();
            try
            {
                if (!Db.ToLower().EndsWith("design"))
                {
                    DataView dv = new DataView(Utils.GetAppDb("M", Svr, Usr, Pwd, newNS + "Design", bIntegratedSecurity));
                    foreach (DataRowView drv in dv)
                    {
                        modules.Add(drv["dbAppDatabase"].ToString());
                        modulesDesigns.Add(drv["dbDesDatabase"].ToString());
                    }
                }
            }
            catch { }
            return new List<List<string>> { modules, modulesDesigns };

        }
        public static void ExtractSqlRsc(string DbProvider, string ResourceName, string oldNS, string newNS, string Svr, string Usr, string Pwd, string Db, List<string> modules, List<string> modulesDesign, bool isNew, string encryptKey, bool bIntegratedSecurity)
        {
            //if (oldNS != newNS) Db = new System.Text.RegularExpressions.Regex("^" + oldNS).Replace(Db, newNS);
            if (TestConnDb(DbProvider, Svr, Usr, Pwd, Db, bIntegratedSecurity))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("Install." + ResourceName));
                if (ResourceName.EndsWith("Sp.sql"))
                {
                    string contents = DecryptString(sr.ReadToEnd(), encryptKey);
                    sr.Close();
                    MemoryStream ms = new MemoryStream();
                    StreamWriter sw = new StreamWriter(ms);
                    sw.Write(contents);
                    sw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    sr = new StreamReader(ms);
                }
                string line, sql = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "GO")
                    {
                        if (sql != string.Empty)
                        {
                            ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, ReplaceNmSp(sql, oldNS, newNS, modules, modulesDesign, isNew), Db, bIntegratedSecurity, false); sql = string.Empty;
                        }
                    }
                    else
                    {
                        sql = sql + System.Environment.NewLine + line;
                    }
                }
                if (sql != string.Empty)	// in case the manual scripts has no "GO":
                {
                    ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, ReplaceNmSp(sql, oldNS, newNS, modules, modulesDesign, isNew), bIntegratedSecurity);
                }
                sr.Close();
            }
            else
            {
                ReportError(string.Format(@"failed to connect to {0}, not applying {1}", Db, ResourceName));
            }
        }

		public static string ExtractTxt(string ResourceName)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("Install." + ResourceName));
			string txt = sr.ReadToEnd();
			sr.Close();
			return txt;
		}

        public static bool TestConn(string DbProvider, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
		{
			OleDbConnection cn = null;
			try
			{
                string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity);
				cn = new OleDbConnection(cs);
				cn.Open();
			}
			catch (Exception ex) { ReportError(ex.Message); return false; }
			finally { cn.Close(); }
			return true;
		}

        public static bool TestConnDb(string DbProvider, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
        {
            OleDbConnection cn = null;
            try
            {
                string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; 
                cn = new OleDbConnection(cs);
                cn.Open();
            }
            catch (Exception ex) { ReportError(ex.Message); return false; }
            finally { cn.Close(); }
            return true;
        }
        public static bool ExecuteSql(string DbProvider, string Svr, string Usr, string Pwd, string Db, string Sql, bool bIntegratedSecurity)
		{
			OleDbConnection cn = null;
			try
			{
                string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; 
                cn = new OleDbConnection(cs);
                cn.Open();
				// Do not put 'SET NOCOUNT ON' in front of Sql due to 'CREATE .. must be the first statement in a query batch':
				OleDbCommand cmd = new OleDbCommand(Sql, cn);
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 9000;
				cmd.ExecuteNonQuery();
                return true;
			}
			catch (Exception ex)
			{
				// Warning arising from S.Proc. creation depends on another that has not been created yet:
                if (!ex.Message.Contains("Native Error code: 2007"))
                {
                    string err = ex.Message + "\n" + Sql.Substring(0, Math.Min(200, Sql.Length));
                    if (Sql.Length > 200) { err = err + "\n......\n" + Sql.Substring(Math.Max(200, Sql.Length - 100)); }
                    ReportError(err);
                    return false;
                }
                else
                {
                    return true;
                }
			}
			finally { cn.Close(); }

		}
        public static void ExecuteSql(string DbProvider, string Svr, string Usr, string Pwd, string Db, string Sql, string appItem, bool bIntegratedSecurity, bool rollback)
        {
            OleDbConnection cn = null;
            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db;
            try
            {
                cn = new OleDbConnection(cs);
                cn.Open();
            }
            catch (Exception ex)
            {
                ReportError(string.Format(@"failed to open database {0} {1}", Db, ex.Message));
                throw;
            }

            OleDbTransaction tr = rollback ? cn.BeginTransaction() : null;
            try
            {
                // Do not put 'SET NOCOUNT ON' in front of Sql due to 'CREATE .. must be the first statement in a query batch':
                OleDbCommand cmd = new OleDbCommand(Sql, cn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 9000;
                if (rollback)
                {
                    cmd.Transaction = tr;
                }
                cmd.ExecuteNonQuery();
                if (rollback)
                {
                    tr.Commit();
                }
            }
            catch (Exception ex)
            {
                // Warning arising from S.Proc. creation depends on another that has not been created yet:
                if (!ex.Message.Contains("Native Error code: 2007"))
                {
                    if (rollback) tr.Rollback();
                    string err = ex.Message + "\n" + appItem + "\n" + Sql.Substring(0, Math.Min(200, Sql.Length));
                    if (Sql.Length > 200) { err = err + "\n......\n" + Sql.Substring(Math.Max(200, Sql.Length - 100)); }
                    ReportError(err);
                }
                else
                {
                    if (rollback)
                    {
                        tr.Commit();
                    }
                }
            }
            finally { cn.Close(); }
        }
		public static DataTable GetAppDb(string DbProvider, string Svr, string Usr, string Pwd, string Db,bool bIntegratedSecurity)
		{
			DataTable dt = new DataTable();
			OleDbConnection cn = null;
			try
			{
                string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; ;
                OleDbDataAdapter da = new OleDbDataAdapter();
				cn = new OleDbConnection(cs);
				cn.Open();
				OleDbCommand cmd = new OleDbCommand(
                         "SET NOCOUNT ON "
                        +"SELECT m.dbDesDatabase, m.dbAppDatabase, m.dbAppUserId "
                        +"FROM dbo.Systems s "
                        +"INNER JOIN dbo.Systems m on m.dbDesDatabase LIKE REPLACE(s.dbDesDatabase,'Design','') + '%'  " 
                        +"WHERE s.SysProgram = 'Y' AND s.Active = 'Y' "
                        +"AND m.dbAppUserId = s.dbAppUserId AND m.dbAppServer = s.dbAppServer " 
                        +"AND m.SysProgram = 'N' " 
                        +"AND EXISTS (SELECT 1 FROM master.dbo.sysdatabases WHERE name = m.dbAppDatabase) ", cn);
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 900;
				da.SelectCommand = cmd;
				da.Fill(dt);
			}
			catch (Exception ex) { ReportError(ex.Message); }
			finally { cn.Close(); }
			return dt;
		}
        public static DataTable AddSystem(string DbProvider, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity, string dbAppDatabase, string dbDesDatabase, string systemName, string systemAbbr, int systemId, bool moveSystem)
        {
            DataTable dt = new DataTable();
            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; ;
            OleDbConnection cn = null;
            cn = new OleDbConnection(cs);
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter();
                OleDbCommand cmd = new OleDbCommand("WrAddSystem", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;
                cmd.Parameters.Add("@dbAppDatabase", OleDbType.VarChar).Value = dbAppDatabase;
                cmd.Parameters.Add("@dbDesDatabase", OleDbType.VarChar).Value = dbDesDatabase;
                cmd.Parameters.Add("@systemName", OleDbType.VarChar).Value = systemName;
                cmd.Parameters.Add("@systemAbbr", OleDbType.VarChar).Value = systemAbbr;
                cmd.Parameters.Add("@systemId", OleDbType.Numeric).Value = systemId;
                cmd.Parameters.Add("@moveSystem", OleDbType.Char).Value = moveSystem ? "Y" : "N";
                da.SelectCommand = cmd;
                da.Fill(dt);
                tr.Commit();

            }
            catch (Exception ex) {
                tr.Rollback();
                ReportError(ex.Message);
            }
            finally { cn.Close(); }
            return dt;
        }
        public static DataTable GetDeployEntity(string DbProvider, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
        {
            DataTable dt = new DataTable();
            OleDbConnection cn = null;
            try
            {
                string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; ;
                OleDbDataAdapter da = new OleDbDataAdapter();
                cn = new OleDbConnection(cs);
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON SELECT DeployPath FROM dbo.Entity ", cn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 900;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex) { ReportError(ex.Message); }
            finally { cn.Close(); }
            return dt;
        }

        //executes a program, waits for it to exit, might have to do some threading
        public static void ExecuteCommand(string cmd, string arg, bool bRunHidden)
		{
			Process proc;
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.FileName = cmd;
			psi.Arguments = arg;
			if (bRunHidden) { psi.WindowStyle = ProcessWindowStyle.Hidden; }	// Hide window.
			proc = Process.Start(psi);
			while (!proc.HasExited)
			{
				Application.DoEvents();
				System.Threading.Thread.Sleep(150);
			}
            if (proc.ExitCode != 0 && cmd.ToLower().EndsWith("bat"))
            {
                ReportError("Error running " + cmd);
            }

		}

		public static void DeleteFile(string FileName)
		{
			FileInfo fi = new FileInfo(FileName);
            if (fi.Exists && !fi.IsReadOnly) { fi.Delete(); }
		}

        public static void ReplFileNS(string srcPath, string tarPath, string oldNS, string newNS, List<string> modules, List<string> modulesDesigns, bool isNew, DirectoryInfo srcDi, bool isDevRuleTier=false)
		{
            if (!Directory.Exists(srcDi.FullName)) return;
			DirectoryInfo tarDi = new DirectoryInfo(srcDi.FullName.Replace(srcPath, tarPath));
			if (!tarDi.Exists) { tarDi.Create(); }
			FileInfo tarFi;
			FileInfo[] fis = srcDi.GetFiles();
			foreach (FileInfo srcFi in fis)
			{
				tarFi = new FileInfo(srcFi.FullName.Replace(srcPath, tarPath));
				if (!Directory.Exists(tarFi.DirectoryName)) { Directory.CreateDirectory(tarFi.DirectoryName); }
				if (AspExt2Replace.IndexOf("," + srcFi.Extension.Replace(".","") + ",") < 0 
                    && oldNS != "ZZ"
                    && 
                    /* change name space for rule tier .cs(only UsrRules and UsrAccess) */
                    (!isDevRuleTier 
                    || !srcFi.Extension.EndsWith(".cs") 
                    || (srcFi.FullName.Contains("UsrRules") && srcFi.FullName.Contains("UsrAccess"))
                    ))
                {
                    srcFi.CopyTo(tarFi.FullName, true);
                }
                else
                {
                    StreamReader sr = srcFi.OpenText();
                    string ss = sr.ReadToEnd();
                    sr.Close();
                    StreamWriter sw = new StreamWriter(tarFi.FullName);
					sw.Write(ReplaceNmSp(ss, oldNS, newNS,modules,modulesDesigns,isNew));
                    sw.Close();
                }
			}
			foreach (DirectoryInfo di in srcDi.GetDirectories())
			{
                ReplFileNS(srcPath, tarPath, oldNS, newNS, modules, modulesDesigns, isNew, di, isDevRuleTier);
			}
		}

        //public static string ReplaceNmSp(string instr, string oldNS, string newNS)
        //{
        //    if (oldNS != String.Empty && newNS != String.Empty && oldNS != newNS)
        //    {
        //        if (oldNS == "ZZ")
        //        {
        //            return instr.Replace(oldNS, newNS);
        //        }
        //        else
        //        {
        //            return instr.Replace(oldNS + ".", newNS + ".").Replace("namespace " + oldNS, "namespace " + newNS).Replace(oldNS + "Design", newNS + "Design").Replace(oldNS + "Cmon", newNS + "Cmon");
        //        }
        //    }
        //    else { return instr; }
        //}

        public static string ReplaceNmSp(string instr, string oldNS, string newNS, List<string> modules, List<string> modulesDesign, bool isNew)
        {
            if (oldNS != String.Empty && newNS != String.Empty && oldNS != newNS)
            {
                if (oldNS == "ZZ")
                {
                    return instr.Replace(oldNS, newNS);
                }
                else
                {
                    string str = instr.Replace("[[?]]", newNS)
                                    .Replace(oldNS + ".", newNS + ".")
                                    .Replace("namespace " + oldNS + ".", "namespace " + newNS + ".")
                                    .Replace(oldNS + "Design", newNS + "Design")
                                    .Replace(oldNS + "Cmon", newNS + "Cmon");
                    foreach (string module in modules)
                    {
                        str = str.Replace(module, newNS + module.Substring(oldNS.Length));
                    }
                    foreach (string moduleDesign in modulesDesign)
                    {
                        str = str.Replace(moduleDesign, newNS + moduleDesign.Substring(oldNS.Length));
                    }
                    return str;
                }
            }
            else { return instr; }
        }

		public static void UpgradeServer(string DbProvider, string Svr, string Usr, string Pwd, string Db, string FilePath, string newNS, bool bIntegratedSecurity, bool newDb)
		{
			string ver;
			if (Db.IndexOf("Design") >= 0)
			{
                ver = GetCurrVersion(DbProvider, Svr, Usr, Pwd, Db, bIntegratedSecurity);
			}
			else
			{
                ver = GetCurrVersion(DbProvider, Svr, Usr, Pwd, Db + "D", bIntegratedSecurity);
			}
            if (ver.Trim() != string.Empty)
			{
				DataView dvSys;
				DataView dvVer = new DataView(ParseFile(FilePath + @"\VwAppItem.txt", CreateAppItem()));
				dvVer.RowFilter = "(DbProviderCd = '' OR DbProviderCd = '" + DbProvider + "') AND AppInfoDesc > '" + ver + "'";
				dvVer.Sort = "AppInfoDesc,ItemOrder";
				foreach (DataRowView drv in dvVer)
				{
					//ReportError(drv["AppInfoDesc"].ToString() + " :" + drv["ItemOrder"].ToString() + " :" + drv["DbProviderCd"].ToString() + " :" + drv["AppItemName"].ToString() + " :" + drv["MultiDesignDb"].ToString());
                    ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, drv["AppItemCode"].ToString().Replace("[[?]]", newNS), drv["AppInfoDesc"].ToString() + "_" + drv["ItemOrder"].ToString() + "_" + Db, bIntegratedSecurity, true);
					if (Db.IndexOf("Design") >= 0 && drv["MultiDesignDb"].ToString() == "Y")
					{
						dvSys = new DataView(GetAppDb(DbProvider, Svr, Usr, Pwd, Db,bIntegratedSecurity));
						foreach (DataRowView drvs in dvSys)
						{
                            try
                            {
                                ExecuteSql(DbProvider, Svr, Usr, Pwd, drvs["dbDesDatabase"].ToString(), drv["AppItemCode"].ToString().Replace("[[?]]", newNS), drv["AppInfoDesc"].ToString() + "_" + drv["ItemOrder"].ToString() + "_" + drvs["dbDesDatabase"].ToString(), bIntegratedSecurity, true);
                            }
                            catch (Exception ex)
                            {
                                ReportError(string.Format("failed to apply meta data({0}) DB changes to {1}, {2}", Db,  drvs["dbDesDatabase"].ToString(), ex.Message));
                            }
						}
					}
				}
			}
		}
        public static void RemoveListOfFile(string baseDirPath, string fileAndDirList,string appInfoDesc)
        {
            if (baseDirPath.Length > 3 && !string.IsNullOrEmpty(fileAndDirList))
            {
                // we dont want baseDirPath in the form of c:\
                System.Text.RegularExpressions.Regex backSlashRx = new System.Text.RegularExpressions.Regex(@"(\\)+"); // consecutive backslash
                System.Text.RegularExpressions.Regex hasSomethingRx = new System.Text.RegularExpressions.Regex("[a-zA-Z0-9]");
                foreach (string fileSpec in fileAndDirList.Split(new char[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries))
                {
                    string cleanedSpec = backSlashRx.Replace(fileSpec.Replace("\r", "").Replace("\n", "").Replace("..", "").Replace(":", "").Replace("/", "\\").Trim(), "\\");
                    if (cleanedSpec.EndsWith("\\") && hasSomethingRx.IsMatch(cleanedSpec)) // we don't want "\" i.e. root of the base
                    {
                        // whole directory remove
                        try
                        {
                            DirectoryInfo di = new DirectoryInfo(baseDirPath + backSlashRx.Replace("\\" + cleanedSpec, "\\"));
                            DialogResult ret = ConfirmDialog(appInfoDesc.Trim() + "\r\n\r\n" + di.FullName + "\r\n\r\nand its content would be removed", "Directory Removal");
                            if (ret == DialogResult.Yes)
                            {
                                di.Delete(true);
                            }
                        }
                        catch  { }
                    }
                    else if (hasSomethingRx.IsMatch(cleanedSpec)) // again we don't want plain *.*
                    {
                        DialogResult ret =
                            ConfirmDialog(appInfoDesc.Trim() + "\r\n\r\n" + cleanedSpec + "\r\n\r\nwould be removed from\r\n\r\n" + baseDirPath + " (including files in subdirectory)", "Multiple File Removal");
                        if (ret == DialogResult.Yes)
                        {
                            try
                            {
                                foreach (var file in Directory.GetFiles(baseDirPath, cleanedSpec, SearchOption.AllDirectories))
                                {
                                    try
                                    {
                                        FileInfo fi = new FileInfo(file);
                                        //ReportError(fi.FullName);
                                        fi.Delete();
                                    }
                                    catch 
                                    {
                                    }
                                }
                            }
                            catch { }
                        }
                    }

                }
            }
        } 

        public static void UpgradeServerClientTier(string DbProvider, string Svr, string Usr, string Pwd, string Db, string FilePath, string ClientTierPath, string newNS, bool bIntegratedSecurity)
        {
            string ver;
            if (Db.IndexOf("Design") >= 0)
            {
                ver = GetCurrVersion(DbProvider, Svr, Usr, Pwd, Db, bIntegratedSecurity);
            }
            else
            {
                ver = GetCurrVersion(DbProvider, Svr, Usr, Pwd, Db + "D", bIntegratedSecurity);
            }
            if (ver.Trim() != string.Empty && File.Exists(FilePath + @"\VwClnAppItem.txt"))
            {
                DataView dvVer = new DataView(ParseFile(FilePath + @"\VwClnAppItem.txt", CreateAppMiddleTierItem()));
                dvVer.RowFilter = "(ObjectTypeCd = 'C') AND AppInfoDesc > '" + ver + "'";

                dvVer.Sort = "AppInfoDesc,ItemOrder";
                foreach (DataRowView drv in dvVer)
                {
                    RemoveListOfFile(ClientTierPath, drv["AppItemCode"].ToString(), Db.Trim() + " " + drv["AppInfoDesc"].ToString() + " " + drv["AppItemName"].ToString());
                    //ReportError(drv["AppInfoDesc"].ToString() + " :" + drv["ItemOrder"].ToString() + " :" + drv["DbProviderCd"].ToString() + " :" + drv["AppItemName"].ToString() + " :" + drv["MultiDesignDb"].ToString());
                    //ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, drv["AppItemCode"].ToString().Replace("[[?]]", newNS), drv["AppInfoDesc"].ToString() + "_" + drv["ItemOrder"].ToString() + "_" + Db);
                }
            }
        }
        public static void UpgradeServerRuleTier(string DbProvider, string Svr, string Usr, string Pwd, string Db, string FilePath, string RuleTierPath, string newNS, bool bIntegratedSecurity)
        {
            string ver;
            if (Db.IndexOf("Design") >= 0)
            {
                ver = GetCurrVersion(DbProvider, Svr, Usr, Pwd, Db, bIntegratedSecurity);
            }
            else
            {
                ver = GetCurrVersion(DbProvider, Svr, Usr, Pwd, Db + "D", bIntegratedSecurity);
            }
            if (ver.Trim() != string.Empty && File.Exists(FilePath + @"\VwRulAppItem.txt"))
            {
                DataView dvVer = new DataView(ParseFile(FilePath + @"\VwRulAppItem.txt", CreateAppMiddleTierItem()));
                dvVer.RowFilter = "(ObjectTypeCd = 'R') AND AppInfoDesc > '" + ver + "'";
                dvVer.Sort = "AppInfoDesc,ItemOrder";
                foreach (DataRowView drv in dvVer)
                {
                    RemoveListOfFile(RuleTierPath, drv["AppItemCode"].ToString(), Db.Trim() + " " + drv["AppInfoDesc"].ToString() + " " + drv["AppItemName"].ToString());
                    //ReportError(drv["AppInfoDesc"].ToString() + " :" + drv["ItemOrder"].ToString() + " :" + drv["DbProviderCd"].ToString() + " :" + drv["AppItemName"].ToString() + " :" + drv["MultiDesignDb"].ToString());
                    //ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, drv["AppItemCode"].ToString().Replace("[[?]]", newNS), drv["AppInfoDesc"].ToString() + "_" + drv["ItemOrder"].ToString() + "_" + Db);
                }
            }
        }

        private static string GetCurrVersion(string DbProvider, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
		{
			DataTable dt = new DataTable();
			OleDbConnection cn = null;
			try
			{
                string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; ;
				OleDbDataAdapter da = new OleDbDataAdapter();
				cn = new OleDbConnection(cs);
				cn.Open();
				OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
					+ " IF exists (SELECT 1 from dbo.sysobjects where id = object_id('dbo.AppInfo'))"
                    //+ " SELECT isnull(Max(AppInfoDesc),'') FROM dbo.AppInfo WHERE VersionDt IS NOT NULL ELSE SELECT ''", cn);
/* to prevent the case where someone accidentally entering version tracking into the wrong module
 * by using appItem, we can simply ignore wrong entries so the return would be the last version
 * that has been applied for database changes
 * 2012.9.5 gary
 */
                    + " SELECT isnull(Max(AppInfoDesc),'   0.   0.00101') FROM AppItem item INNER JOIN AppInfo info on item.AppInfoId = info.AppInfoId AND info.VersionDt IS NOT NULL ELSE SELECT ''", cn);
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 900;
				da.SelectCommand = cmd;
				da.Fill(dt);
			}
			catch (Exception ex) { ReportError(ex.Message); }
			finally { cn.Close(); }
            try
            {
                return dt.Rows[0][0].ToString();
            }
            catch { return "   0.   0.00101"; }
		}

		private static DataTable ParseFile(string fi, DataTable dt)
		{
			StringBuilder sb = new StringBuilder();
			StreamReader sr = new StreamReader(fi);
			sb.Append(sr.ReadToEnd());
			string[] delim = { "~#~" };
			string[] rows = sb.ToString().Split(delim, StringSplitOptions.RemoveEmptyEntries);
			for (int ii = 0; ii < rows.Length; ii++) { AddRow(dt, rows[ii]); }
			sr.Close();
			return dt;
		}

		private static DataTable CreateAppItem()
		{
			string[] ColumnNames = { "AppInfoDesc", "ItemOrder", "DbProviderCd", "AppItemName", "MultiDesignDb", "AppItemCode" };
			DataTable dt = new DataTable();
			for (int ii = 0; ii < ColumnNames.Length; ii++)
			{
				DataColumn iColumn = new DataColumn();
				iColumn.ColumnName = ColumnNames[ii];
				dt.Columns.Add(iColumn);
			}
			return dt;
		}
        private static DataTable CreateAppMiddleTierItem()
        {
            string[] ColumnNames = { "AppInfoDesc", "ItemOrder", "ObjectTypeCd", "AppItemName", "AppItemCode" };
            DataTable dt = new DataTable();
            for (int ii = 0; ii < ColumnNames.Length; ii++)
            {
                DataColumn iColumn = new DataColumn();
                iColumn.ColumnName = ColumnNames[ii];
                dt.Columns.Add(iColumn);
            }
            return dt;
        }

		private static void AddRow(DataTable dt, string row)
		{
			string[] delim = { "~@~" };
			string[] cols = row.Split(delim, StringSplitOptions.None);
			if (cols.Length != dt.Columns.Count)
			{
				throw new Exception("Columns (" + dt.Columns.Count.ToString() + ") and Data (" + cols.Length.ToString() + ") do not match. Please investigate and try again.");
			}
			DataRow dr = dt.NewRow();
			for (int ii = 0; ii < cols.Length; ii++) { dr[ii] = cols[ii]; }
			dt.Rows.Add(dr);
		}

        public static bool MakeSQLServerDirectory(string DbProvider, string Svr, string Usr, string Pwd, string Db, string bkPath, bool bIntegratedSecurity)
        {
            string MakeBackupPath = "EXEC sys.xp_create_subdir '" + bkPath + "'";
            return ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, MakeBackupPath, bIntegratedSecurity);
        }
        public static bool RunDesPostUpgradeSP(string DbProvider, string oldNS, string newNS, string deployType, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
        {
            string RunPostUpgradeSP = "IF OBJECT_ID('dbo.WrPostUpgrade') IS NOT NULL EXEC "
                        + string.Format("dbo.WrPostUpgrade {0}, {1}, {2}", oldNS ?? "NULL", newNS ?? "NULL", deployType ?? "NULL");
            return ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, RunPostUpgradeSP, bIntegratedSecurity);
        }
        public static bool RunDesPreUpgradeSP(string DbProvider, string oldNS, string newNS, string deployType, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
        {
            string RunPreUpgradeSP = "IF OBJECT_ID('dbo.WrPreUpgrade') IS NOT NULL EXEC "
                        + string.Format("dbo.WrPreUpgrade {0}, {1}, {2}", oldNS ?? "NULL", newNS ?? "NULL", deployType ?? "NULL");
            return ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, RunPreUpgradeSP, bIntegratedSecurity);
        }
        public static bool RunAppPostUpgradeSP(string DbProvider, string oldNS, string newNS, string deployType, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
        {
            string RunPostUpgradeSP = "IF OBJECT_ID('dbo._PostUpgrade') IS NOT NULL EXEC "
                        + string.Format("dbo._PostUpgrade {0}, {1}, {2}", oldNS ?? "NULL", newNS ?? "NULL", deployType ?? "NULL");
            return ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, RunPostUpgradeSP, bIntegratedSecurity);
        }
        public static bool RunAppPreUpgradeSP(string DbProvider, string oldNS, string newNS, string deployType, string Svr, string Usr, string Pwd, string Db, bool bIntegratedSecurity)
        {
            string RunPreUpgradeSP = "IF OBJECT_ID('dbo._PreUpgrade') IS NOT NULL EXEC "
                        + string.Format("dbo._PreUpgrade {0}, {1}, {2}", oldNS ?? "NULL", newNS ?? "NULL", deployType ?? "NULL");
            return ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, RunPreUpgradeSP, bIntegratedSecurity);
        }
        public static void BackupServer(string DbProvider, string Svr, string Usr, string Pwd, string Db, string bkPath, bool bIntegratedSecurity)
		{
			string Sql;
            string MakeBackupPath = "EXEC sys.xp_create_subdir '" + bkPath + "'";
            DataView dvSys = new DataView(GetAppDb(DbProvider, Svr, Usr, Pwd, Db, bIntegratedSecurity));
			// Backup ??Design Database:
			if (DbProvider == "S")
			{
				Sql = "DUMP DATABASE " + Db + " TO '" + bkPath + Db + "'";
			}
			else
			{
                //ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, MakeBackupPath, bIntegratedSecurity);
				Sql = "BACKUP DATABASE " + Db + " TO DISK = '" + bkPath + Db + ".bak'";
			}
            ExecuteSql(DbProvider, Svr, Usr, Pwd, Db, Sql, bIntegratedSecurity);
			// Backup all Design Databases:
			foreach (DataRowView drv in dvSys)
			{
				if (DbProvider == "S")
				{
					Sql = "DUMP DATABASE " + drv["dbDesDatabase"].ToString() + " TO '" + bkPath + drv["dbDesDatabase"].ToString() + "'";
				}
				else
				{
					Sql = "BACKUP DATABASE " + drv["dbDesDatabase"].ToString() + " TO DISK = '" + bkPath + drv["dbDesDatabase"].ToString() + ".bak'";
				}
                ExecuteSql(DbProvider, Svr, Usr, Pwd, drv["dbDesDatabase"].ToString(), Sql, bIntegratedSecurity);
			}
			// Backup all Application Databases:
			foreach (DataRowView drv in dvSys)
			{
				if (DbProvider == "S")
				{
					Sql = "DUMP DATABASE " + drv["dbAppDatabase"].ToString() + " TO '" + bkPath + drv["dbAppDatabase"].ToString() + "'";
				}
				else
				{
					Sql = "BACKUP DATABASE " + drv["dbAppDatabase"].ToString() + " TO DISK = '" + bkPath + drv["dbAppDatabase"].ToString() + ".bak'";
				}
                ExecuteSql(DbProvider, Svr, Usr, Pwd, drv["dbAppDatabase"].ToString(), Sql, bIntegratedSecurity);
			}
		}
        
        public static bool CreateTestDatabase(string DbProvider, string Svr, string Usr, string Pwd, string Db, string crPath, string serverVer, bool bIntegratedSecurity)
        {
            //string DataFileName, DataPathName, DataFileGrowth;
            //string LogFileName, LogPathName, LogFileGrowth;
            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity);
            OleDbConnection cn = new OleDbConnection(cs);
            cn.Open();
            //if (!Directory.Exists(crPath)) { Directory.CreateDirectory(crPath); }
            //string randName = "X" + Guid.NewGuid().ToString().Substring(0, 10).Replace("-","");
            string randName = Db;

            //DataFileName = randName + "_Data";
            //DataPathName = crPath + "\\" + DataFileName + ".MDF";
            //DataFileGrowth = "10%";
            //LogFileName = randName + "_Log";
            //LogPathName = crPath + "\\" + LogFileName + ".LDF";
            //LogFileGrowth = "10%";
            OleDbCommand cmd = new OleDbCommand("IF NOT EXISTS(SELECT 1 FROM sysdatabases WHERE name = '" + randName + "') BEGIN"
                         + " CREATE DATABASE " + randName 
                         /* don't use datapath 
                         + " ON PRIMARY " + " (NAME = " + DataFileName + ", "
                         + " FILENAME = '" + DataPathName + "'," + " FILEGROWTH =" + DataFileGrowth + ") "
                         + " LOG ON (NAME =" + LogFileName + ", " + " FILENAME = '" + LogPathName + "', "
                         + " FILEGROWTH =" + LogFileGrowth + ")"
                          */ 
                         + " DROP DATABASE " + randName
                         + " END"
                //+ (string) (bSql2005 ? " EXEC ('sp_dbcmptlevel  " + Db + " , 80 ')" : " EXEC ('ALTER DATABASE " + Db + " SET COMPATIBILITY_LEVEL = 80 ')")
                         , cn);

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 9000;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) { ReportError(ex.Message); }
            finally { cn.Close(); }

            return false;
        }
        public static bool TestLogin(string DbProvider, string Svr, string Db, string AppUsr, string AppPwd)
        {
            string cs = GetConnString(DbProvider, Svr, AppUsr, AppPwd, false);
            OleDbConnection cn = new OleDbConnection(cs);
            cn.Open();
            cn.Close();
            return true;
        }

        public static bool CreateLogin(string DbProvider, string Svr, string Usr, string Pwd, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
            bool loginOk = false;
            string cs = GetConnString(DbProvider, Svr, AppUsr, AppPwd, false);
            OleDbConnection cn = new OleDbConnection(cs);
            try
            {
                cs = GetConnString(DbProvider, Svr, AppUsr, AppPwd, false);
                cn = new OleDbConnection(cs);
                cn.Open();
                loginOk = true;
                cn.Close();
            }
            catch 
            {
            }

            if (loginOk) return true;

            cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity);
            cn = new OleDbConnection(cs);
            OleDbCommand cmd = new OleDbCommand("", cn);
            cn.Open();
            if (AppUsr.ToLower().Trim() != Usr.ToLower().Trim() || bIntegratedSecurity)
            {
                cmd.CommandText = ""
                + " IF NOT EXISTS (SELECT 1 FROM master.dbo.syslogins WHERE name = '" + @AppUsr.Replace("'", "''") + "')"
                + " BEGIN"
                + "  CREATE LOGIN [" + @AppUsr + "] WITH PASSWORD = '" + @AppPwd.Replace("'", "''") + "', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF "
                + "  DECLARE @userid int "
                + "  use master "
                + "  SELECT @userid = USER_ID('" + @AppUsr.Replace("'", "''") + "')"
                + "  IF @userid IS NULL BEGIN"
                + "     CREATE USER [" + @AppUsr + "] FOR LOGIN [" + @AppUsr + "] "
                + "     GRANT CREATE ANY DATABASE to [" + @AppUsr + "] "
                + "  END"
                + "  use tempdb "
                + "  SELECT @userid = USER_ID('" + @AppUsr.Replace("'", "''") + "')"
                + "  IF @userid IS NULL BEGIN"
                + "     CREATE USER [" + @AppUsr + "] FOR LOGIN [" + @AppUsr + "] "
                + "  END"
                + " END";
                cmd.ExecuteNonQuery();
            }
            cn.Close();
            return TestSQL(DbProvider, Svr, AppUsr, AppPwd, false);
        }

        public static bool CreateDatabase(string DbProvider, string Svr, string Usr, string Pwd, string newNS, string Db, string _crPath, string _serverVer, string EncKey, string AppUsr, string AppPwd, bool bIntegratedSecurity, bool upgrade, bool meta, int systemId, bool moveSystem)
		{
            //string DataFileName, DataPathName, DataFileGrowth;
            //string LogFileName, LogPathName, LogFileGrowth;
            AppUsr = AppUsr.Replace(".", "_");
            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity);
            OleDbConnection cn = new OleDbConnection(cs);
			cn.Open();
			//if (!Directory.Exists(crPath)) { Directory.CreateDirectory(crPath); }
            //DataFileName = Db + "_Data";
            //DataPathName = crPath + "\\" + DataFileName + ".MDF";
            //DataFileGrowth = "10%";
            //LogFileName = Db + "_Log";
            //LogPathName = crPath + "\\" + LogFileName + ".LDF";
            //LogFileGrowth = "10%";
            string currentAppUsr = "";
            bool bModuleExist = false;
            bool bDbExist = false;
            bool bDbCreated = false;
            if (upgrade)
            {
                DataTable dtSystems = GetAppDb(DbProvider, Svr, Usr, Pwd, newNS + "Design", bIntegratedSecurity);
                foreach (DataRow dr in dtSystems.Rows)
                {
                    currentAppUsr = dr["dbAppUserId"].ToString();
                    if (dr["dbAppDatabase"].ToString() == Db || dr["dbDesDatabase"].ToString() == Db)
                    {
                        bModuleExist = true;
                    }
                }
                OleDbCommand dbExist = new OleDbCommand("IF NOT EXISTS(SELECT 1 FROM sysdatabases WHERE name = '" + Db + "') SELECT DbExists = 'N' ELSE SELECT DbExists = 'Y'"
                                , cn);
                OleDbDataAdapter da = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                dbExist.CommandType = CommandType.Text;
                dbExist.CommandTimeout = 900;
                da.SelectCommand = dbExist;
                da.Fill(dt);
                bDbExist = dt.Rows[0][0].ToString() == "Y";
                if (bDbExist) return false;
            }
            else
            {
                OleDbCommand dbExist = new OleDbCommand("IF NOT EXISTS(SELECT 1 FROM sysdatabases WHERE name = '" + Db + "') SELECT DbExists = 'N' ELSE SELECT DbExists = 'Y'"
                                , cn);
                OleDbDataAdapter da = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                dbExist.CommandType = CommandType.Text;
                dbExist.CommandTimeout = 900;
                da.SelectCommand = dbExist;
                da.Fill(dt);
                bDbExist = dt.Rows[0][0].ToString() == "Y";
                if (bDbExist)
                {
                    throw new Exception(string.Format("database {0} exists in the system, remove it or just do upgrade", Db));
                }
            }

            OleDbCommand cmd = new OleDbCommand("IF NOT EXISTS(SELECT 1 FROM sysdatabases WHERE name = '" + Db + "') BEGIN"
                    + " CREATE DATABASE " + Db
                /* should not specify location of data file at all as server may be setup in entirely different manner
                 * 2016.8.17 gary

                    + " ON PRIMARY " + " (NAME = " + DataFileName + ", "
                    + " FILENAME = '" + DataPathName + "'," + " FILEGROWTH =" + DataFileGrowth + ") "
                    + " LOG ON (NAME =" + LogFileName + ", " + " FILENAME = '" + LogPathName + "', "
                    + " FILEGROWTH =" + LogFileGrowth + ")"
                 */
                /* sp_dboption removed in SQL server 2012.7.25 gary */
                    //+ " EXEC ('sp_dboption ''" + Db + "'',''trunc. log on chkpt.'',true')"
                    //+ " EXEC ('sp_dboption ''" + Db + "'',''select into/bulkcopy'',true')"
                /* not needed generally and would not work if the login is not as role
                 * 2015.9.24 gary
                 */
                    //+ " EXEC ('ALTER DATABASE " + Db + " SET RECOVERY SIMPLE')"
                    //+ " EXEC ('declare @serverName nvarchar(max) select @serverName = @@serverName exec sp_serveroption @serverName ,''data access'',''true''')"
                    //+ (string) (bSql2005 ? " EXEC ('sp_dbcmptlevel  " + Db + " , 80 ')" : " EXEC ('ALTER DATABASE " + Db + " SET COMPATIBILITY_LEVEL = 80 ')")
                    + " END"
                    , cn);

            try
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 9000;
				cmd.ExecuteNonQuery();
                bDbCreated = true;
                /* no longer need SQL 2000 compatibility 2012.5.16 gary */
                //cmd.CommandText = "EXEC sp_dbcmptlevel  " + Db + " , 80 ";
                //cmd.ExecuteNonQuery();
                if (AppUsr.ToLower().Trim() != Usr.ToLower().Trim())
                {
                    string dbOwner = string.IsNullOrEmpty(AppUsr.ToLower().Trim()) ? Usr.ToLower().Trim() : AppUsr.ToLower().Trim();

                    if (!string.IsNullOrEmpty(EncKey) && !upgrade)
                    {
                        cmd.CommandText = ""
                        + " IF NOT EXISTS (SELECT 1 FROM master.dbo.syslogins WHERE name = '" + dbOwner.Replace("'", "''") + "')"
                        + " BEGIN"
                        + "  CREATE LOGIN [" + dbOwner + "] WITH PASSWORD = '" + AppPwd.Replace("'", "''") + "'"
                        + "  DECLARE @userid int "
                        + "  use master "
                        + "  SELECT @userid = USER_ID('" + dbOwner.Replace("'", "''") + "')"
                        + "  IF @userid IS NULL BEGIN"
                        + "     CREATE USER [" + dbOwner + "] FOR LOGIN [" + dbOwner + "] "
                        + "     GRANT CREATE ANY DATABASE to [" + dbOwner + "] " 
                        + "  END"
                        + "  use tempdb "
                        + "  SELECT @userid = USER_ID('" + dbOwner.Replace("'", "''") + "')"
                        + "  IF @userid IS NULL BEGIN"
                        + "     CREATE USER [" + dbOwner + "] FOR LOGIN [" + dbOwner + "] "
                        + "  END"
                        + " END";
                        cmd.ExecuteNonQuery();
                    }
                    if (!upgrade)
                    {
                        cmd.CommandText = ""
                            + " DECLARE @userid int "
                            + " USE " + Db + " "
                            + " SELECT @userid = USER_ID('" + dbOwner.Replace("'", "''") + "')"
                            + " IF @userid IS NULL BEGIN"
                            + "     CREATE USER [" + dbOwner + "] FOR LOGIN [" + dbOwner + "] "
                            + "     EXEC('sp_addrolemember ''db_owner'', ''" + dbOwner.Replace("'", "''") + "''')"
                            + " END";
                        cmd.ExecuteNonQuery();
                    }
                }
                // this needs to done seperately as the role login to run the installer may not have proper right 
                // for this, but the error is 'non-critical' so just let user see it 2017.7.28 gary
                try
                {
                    cmd.CommandText = ""
                        + " EXEC ('ALTER DATABASE " + Db + " SET RECOVERY SIMPLE')"
                        ;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { ReportError(ex.Message); }
                if (!bDbExist && upgrade && bDbCreated)
                {
                    // in case of new module on existing installation setup owner proper
                    string dbOwner = string.IsNullOrEmpty(Usr) ? currentAppUsr : Usr;
                    try
                    {
                        cmd.CommandText = ""
                            + " DECLARE @userid int "
                            + " USE " + Db + " "
                            + " SELECT @userid = USER_ID('" + dbOwner.Replace("'", "''") + "')"
                            + " IF @userid IS NULL BEGIN"
                            + "     CREATE USER [" + dbOwner + "] FOR LOGIN [" + dbOwner + "] "
                            + "     EXEC('sp_addrolemember ''db_owner'', ''" + dbOwner.Replace("'", "''") + "''')"
                            + " END";
                        cmd.ExecuteNonQuery();
                        ReportError("New module database " + Db + " has been created with " + dbOwner + " as owner "); 
                    }
                    catch (Exception ex)
                    {
                        ReportError("new DB " + Db + "created but failed to setup owner " + dbOwner + " you would need to manually do that"  + ex.Message);
                    }
                    if (!bModuleExist && systemId > 0 && meta)
                    {
                        string desDb = Db;
                        string appDb = desDb.Substring(0, Db.Length - 1);
                        string abbr = appDb.Substring(newNS.Length);
                        try
                        {
                            AddSystem(DbProvider, Svr, Usr, Pwd, newNS + "Design", bIntegratedSecurity, appDb, desDb, appDb, abbr, systemId, moveSystem);
                            ReportError("new DB " + Db + "has been created and added to the system" + (!moveSystem ? "" : "\r\nplease re-generate all screen/report/wizard"));
                        }
                        catch (Exception ex)
                        {
                            ReportError("new DB " + Db + "has been created but failed to add system " + ex.Message + "\r\n,  please create proper system definition in Systems table" + (!moveSystem ? "" : " and generate all screen/report/wizard"));
                        }
                    }
                }

            }
			catch (Exception ex) { ReportError(ex.Message); }
			finally { cn.Close(); }

            return bDbCreated;

            //cn = new OleDbConnection(cs.Replace("database=master","database="+Db));
            //cn.Open();
            //cmd = new OleDbCommand(""
            //    + " CREATE USER " + @AppUsr + " FOR LOGIN " + @AppUsr + " "
            //    ,cn);
            //try
            //{
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandTimeout = 9000;
            //    cmd.ExecuteNonQuery();
            //    cmd.CommandText = ""
            //        + " EXEC ('sp_addrolemember ''db_owner'', ''" + @AppUsr + "''')";
            //    cmd.ExecuteNonQuery();
            //}  
            //catch (Exception ex) { ReportError(ex.Message); }
            //finally { cn.Close(); }
        }

        private static dynamic LoadAddinClasses(string assemblyFile, string typeName)
        {
            Assembly asm = null;
            Type[] types = null;

            try
            {
                asm = Assembly.LoadFrom(assemblyFile);
                types = asm.GetTypes();
            }
            catch (Exception ex)
            {
                //                var msg = $"Unable to load add-in assembly: {Path.GetFileNameWithoutExtension(assemblyFile)}";
                //                mmApp.Log(msg, ex);
                return ex == null ? "" :  null;
            }

            foreach (var type in types)
            {
                if (type.FullName == typeName)
                {
                    return Activator.CreateInstance(type);
                }
            }
            return null;
        }

        public static string _EncryptString(string inStr, string key)
        {
            if (!File.Exists("License3.dll"))
            {
                ExtractBinRsc(@"Resources.License3.dll", "License3.dll");
            }

            dynamic encryptor = LoadAddinClasses("License3.dll", "RO.Common3.License");
            if (string.IsNullOrEmpty(key))
            {
                var xx = encryptor.GetType().GetMethod("EncryptString", new Type[] { typeof(string)}).Invoke(encryptor, new[] { inStr });
                return xx;
            }
            else
            {
                var xx = encryptor.GetType().GetMethod("EncryptString", new Type[] { typeof(string), typeof(string) }).Invoke(encryptor, new[] { inStr, key });
                return xx;

            }
        }
        private static string EncryptString(string inStr, string key)
		{
            return _EncryptString(inStr, key);
			// The following key must be the same as the system:
            //string pCurrKey = key;
            //string outStr = "";
            //MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            //des.Mode = CipherMode.ECB;
            //try
            //{
            //    des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(pCurrKey));
            //    outStr = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(UTF8Encoding.UTF8.GetBytes(inStr), 0, UTF8Encoding.UTF8.GetBytes(inStr).Length));
            //}
            //catch
            //{
            //    outStr = null;
            //}
            //hashmd5 = null;
            //des = null;
            //return outStr;
		}

		// Creates virtual directories under "Default Web Site":
		public static void SetupIIS(bool Framework2, string site, string Namespace, string ClientTier, string WsTier, string XlsTier, bool bEnable32bit)
		{
			string args;
            if (string.IsNullOrEmpty(site)) site = "Default Web Site";
			try // If IIS7:
			{
				System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(@"c:\Windows\System32\inetsrv\Microsoft.Web.Administration.dll");
                try
                {
                    args = "add APPPOOL -name:\"" + Namespace + "AppPool\" -managedPipelineMode:Classic -processModel.identityType:NetworkService -managedRuntimeVersion:\"" + (Framework2 ? "v2.0" : "v4.0") + "\" " + (bEnable32bit && false ? " /enable32BitAppOnWin64:true " : "");
                    Utils.ExecuteCommand("c:\\windows\\system32\\inetsrv\\appcmd.exe", args, true);
                }
                catch { }
                try
                {
                    args = "add APPPOOL -name:\"WsXlsAppPool\" -managedPipelineMode:Classic -processModel.identityType:NetworkService -managedRuntimeVersion:\"" + (Framework2 ? "v2.0" : "v4.0") + "\" " + (bEnable32bit ? " /enable32BitAppOnWin64:true " : "");
                    Utils.ExecuteCommand("c:\\windows\\system32\\inetsrv\\appcmd.exe", args, true);
                }
                catch { }
                args = "add APP /site.name:\"" + site + "\" /path:/" + Namespace + " /applicationPool:\"" + Namespace + "AppPool\" /physicalPath:\"" + ClientTier + "\"";
				Utils.ExecuteCommand("c:\\windows\\system32\\inetsrv\\appcmd.exe", args, true);
                args = "add APP /site.name:\"" + site + "\" /path:/" + Namespace + "Ws /applicationPool:\"" + Namespace + "AppPool\" /physicalPath:\"" + WsTier + "\"";
				Utils.ExecuteCommand("c:\\windows\\system32\\inetsrv\\appcmd.exe", args, true);
                args = "add APP /site.name:\"" + site + "\" /path:/WsXls /applicationPool:\"WsXlsAppPool\" /physicalPath:\"" + XlsTier + "\"";
				Utils.ExecuteCommand("c:\\windows\\system32\\inetsrv\\appcmd.exe", args, true);
				return;
			}
			catch
			{
				//create IIS6 virtual directories:
				args = "\"c:\\windows\\system32\\iisvdir.vbs\" /create \"Default Web Site\" " + Namespace + " " + ClientTier;
				Utils.ExecuteCommand("c:\\windows\\system32\\cscript.exe", args, true);
				args = "\"c:\\windows\\system32\\iisvdir.vbs\" /create \"Default Web Site\" " + Namespace + "Ws " + WsTier;
				Utils.ExecuteCommand("c:\\windows\\system32\\cscript.exe", args, true);
				args = "\"c:\\windows\\system32\\iisvdir.vbs\" /create \"Default Web Site\" WsXls " + XlsTier;
				Utils.ExecuteCommand("c:\\windows\\system32\\cscript.exe", args, true);

				//changes framework version of virtual directory to .net 2.0
                
				if (Framework2)
				{
					args = "-s W3SVC/1/ROOT/" + Namespace;
					Utils.ExecuteCommand("C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\aspnet_regiis.exe", args, true);
					args = "-s W3SVC/1/ROOT/" + Namespace + "Ws";
					Utils.ExecuteCommand("C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\aspnet_regiis.exe", args, true);
				}
			}
		}

		//grants full control to network service on the client and rule tiers; if path doesnt exist it is created
        public static void SetupFilePermissions(string ClientTierPath, string RuleTierPath, string newNS, string WindowsServiceAccount, Action<int, string> progress, bool isProduction)
		{
			if (!Directory.Exists(ClientTierPath)) { Directory.CreateDirectory(ClientTierPath); }
            progress(0, "Setting up Client Tier ACL ...");
            if (!isProduction)
                Utils.ExecuteCommand("C:\\WINDOWS\\system32\\cacls.exe", " \"" + ClientTierPath + "\" " + "/E /T /C /G \"" + WindowsServiceAccount + "\":F", true);
            else
            {
                /* for production, web layer is read only by default, only data(file upload) is full access */
                Utils.ExecuteCommand("C:\\WINDOWS\\system32\\cacls.exe", " \"" + ClientTierPath + "\" " + "/E /T /C /P \"" + WindowsServiceAccount + "\":R", true);
                /* disable the users special right(authenticated user including NETWORK SERVICE) of create file/directory */
                Utils.ExecuteCommand("C:\\WINDOWS\\system32\\cacls.exe", " \"" + ClientTierPath + "\" " + "/E /T /C /P \"USERS\":R", true);
                Utils.ExecuteCommand("C:\\WINDOWS\\system32\\cacls.exe", " \"" + ClientTierPath + "\\data\" " + "/E /T /C /G \"" + WindowsServiceAccount + "\":F", true);
            }


			string CompDir = ClientTierPath.Replace(@"\Web",@"\PrecompiledWeb\") + newNS;
			if (!Directory.Exists(CompDir)) { Directory.CreateDirectory(CompDir); }
            progress(0, "Setting up PrecompiledWeb ACL ...");
            Utils.ExecuteCommand("C:\\WINDOWS\\system32\\cacls.exe", " \"" + CompDir + "\" " + "/E /T /C /G \"" + WindowsServiceAccount + "\":F", true);

			//only happens if we have a rule tier
			if (RuleTierPath != string.Empty)
			{
				if (!Directory.Exists(RuleTierPath)) { Directory.CreateDirectory(RuleTierPath); }
                progress(0, "Setting up Rule tier ACL ...");
				Utils.ExecuteCommand("C:\\WINDOWS\\system32\\cacls.exe", " \"" + RuleTierPath + "\" " + "/E /T /C /G \"" + WindowsServiceAccount + "\":F", true);
			}
		}

        public static void SetupLicense3Dll(string ruleTierDir, string clientTierDir)
        {
            if (Directory.Exists(ruleTierDir + @"\Lib") && !File.Exists(ruleTierDir + @"\Lib\License3.dll")) 
            {
                try
                {
                    ExtractBinRsc(@"Resources.License3.dll", ruleTierDir + @"\Lib\License3.dll");
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message.ToString());
                }
            }

            if (Directory.Exists(clientTierDir + @"\bin") && !File.Exists(clientTierDir + @"\bin\License3.dll"))
            {
                try
                {
                    ExtractBinRsc(@"Resources.License3.dll", clientTierDir + @"\bin\License3.dll");
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message.ToString());
                }
            }
        }
        public static string SetupEncryptionKey(string ruleTierDir)
        {
            string newEncryptKey = Guid.NewGuid().ToString().Replace("-","");
            if (!Directory.Exists(ruleTierDir + @"\License3")) { Directory.CreateDirectory(ruleTierDir + @"\License3"); }
            using (StreamWriter sw = System.IO.File.CreateText(ruleTierDir + @"\License3\Key.cs"))
            {
                sw.WriteLine("namespace RO.Common3");
                sw.WriteLine("{");
                sw.WriteLine("	public partial class Key ");
                sw.WriteLine("	{");
                sw.WriteLine("		protected string pPrevKey = \"" + newEncryptKey + "\";");
                sw.WriteLine("		protected string pCurrKey = \"" + newEncryptKey + "\";");
                sw.WriteLine("	}");
                sw.WriteLine("}");
                sw.Close();
            }
            //string newEncryptionCS = "";
            //using (StreamReader sr = new StreamReader(ruleTierDir + @"\Common3\Encryption.cs")) 
            //{
            //    string content = sr.ReadToEnd();
            //    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("^\\s*CurrKey\\s*=.*$",System.Text.RegularExpressions.RegexOptions.Multiline);
            //    newEncryptionCS = re.Replace(content, "			CurrKey = \"" + newEncryptKey + "\";" + Environment.NewLine);
            //    sr.Close();
            //}
            //using (StreamWriter sw = new StreamWriter(ruleTierDir + @"\Common3\Encryption.cs", false))
            //{
            //    sw.Write(newEncryptionCS);
            //    sw.Close();
            //}

            //string newDeployCS = "";
            //using (StreamReader sr = new StreamReader(ruleTierDir + @"\Rule3\Deploy.cs"))
            //{
            //    string content = sr.ReadToEnd();
            //    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("^\\s*private string roEncryptionKey\\s*=.*$", System.Text.RegularExpressions.RegexOptions.Multiline);
            //    newDeployCS = re.Replace(content, "        private string roEncryptionKey = \"" + newEncryptKey + "\";" + Environment.NewLine);
            //    sr.Close();
            //}
            //using (StreamWriter sw = new StreamWriter(ruleTierDir + @"\Rule3\Deploy.cs", false))
            //{
            //    sw.Write(newDeployCS);
            //    sw.Close();
            //}
            return newEncryptKey;
        }

		public static void SetupConfigFile(string DbProvider, string Svr, string Usr, string Pwd, string ConfigClientPath, string ConfigWsPath, string ClientTierPath, string RuleTierPath,string oldProjectRoot, string WindowsServiceAccount, string WsRptBaseUrl, string newNS, string oldNS, string WebServer, string DeployType, string encKey, string AppUsr, string AppPwd, string[] localConfig)
		{
			XmlNode xn;
			XmlDocument xd = new XmlDocument();
			xd.Load(ConfigClientPath);
			xn = xd.DocumentElement;

            if (AppUsr != "dummy") Utils.TestSQL("M", Svr, AppUsr, AppPwd, false);

			foreach (XmlNode node in xn.ChildNodes)
			{
				if (node.Name == "appSettings")
				{
                    var regex = new System.Text.RegularExpressions.Regex(@"\\Rule\\?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    var projectRoot = regex.Replace(RuleTierPath, "");
                    oldProjectRoot = new System.Text.RegularExpressions.Regex(@"\\" + oldNS + @"\\?$").Replace(oldProjectRoot, @"\" + newNS);

                    foreach (XmlNode setting in node.ChildNodes)
					{
                        if (setting.Attributes != null && setting.Attributes.Count > 0)
                        {
                            if (setting.Attributes[0].Value == "DesProvider")
                            {
                                if (DbProvider == "M") { setting.Attributes[1].Value = "Sqloledb"; } else { setting.Attributes[1].Value = "Sybase.ASEOLEDBProvider"; }
                            }
                            if (setting.Attributes[0].Value == "DesServer") { setting.Attributes[1].Value = Svr; }
                            if (setting.Attributes[0].Value == "DesUserId") { setting.Attributes[1].Value = AppUsr.Replace(".", "_"); }
                            if (setting.Attributes[0].Value == "DesPassword") { setting.Attributes[1].Value = EncryptString(AppPwd, encKey); }
                            if (setting.Attributes[0].Value == "ClientTierPath") { setting.Attributes[1].Value = ClientTierPath + "\\"; }
                            if (setting.Attributes[0].Value == "RuleTierPath") { setting.Attributes[1].Value = RuleTierPath + "\\"; }
                            //if (setting.Attributes[0].Value == "WsRptBaseUrl") { setting.Attributes[1].Value = WsRptBaseUrl; }
                            if (setting.Attributes[0].Value == "AppNameSpace") { setting.Attributes[1].Value = newNS; }
                            if (setting.Attributes[0].Value == "WsDomain") { setting.Attributes[1].Value = WebServer; }
                            if (setting.Attributes[0].Value == "WsRptDomain") { setting.Attributes[1].Value = WebServer; }
                            if (setting.Attributes[0].Value == "WsBaseUrl") { setting.Attributes[1].Value = (WebServer.StartsWith("http") ? "" : "http://") + WebServer + "/" + newNS + "Ws"; }
                            if (setting.Attributes[0].Value == "WsPassword") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "WsRptPassword") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "OrdUrl" && (setting.Attributes[1].Value.ToLower().Trim().StartsWith("http"))) { setting.Attributes[1].Value = (WebServer.StartsWith("http") ? "" : "http://") + WebServer + "/" + newNS + "/" + string.Join("", new Uri(setting.Attributes[1].Value).Segments.Skip(2).ToArray()); }
                            if (setting.Attributes[0].Value == "SslUrl" && (setting.Attributes[1].Value.ToLower().Trim().StartsWith("http"))) { setting.Attributes[1].Value = (WebServer.StartsWith("http") ? "" : "http://") + WebServer + "/" + newNS + "/" + string.Join("", new Uri(setting.Attributes[1].Value).Segments.Skip(2).ToArray()); }
                            if (setting.Attributes[0].Value == "PmtUrl" && (setting.Attributes[1].Value.ToLower().Trim().StartsWith("http"))) { setting.Attributes[1].Value = (WebServer.StartsWith("http") ? "" : "http://") + WebServer + "/" + newNS + "/" + string.Join("", new Uri(setting.Attributes[1].Value).Segments.Skip(2).ToArray()); }
                            if (setting.Attributes[0].Value == "GoogleAPIKey") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "GoogleClientId") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "FacebookAppId") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "AzureAPIClientId") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "AzureAPISecret") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "AzureAPIRedirectUrl") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "RintagiLicense") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "TechSuppEmail") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "ReactBaseUrl") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "LicenseServer") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "LicenseSignerPath") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "TrustedLoginFederationKey") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "SmtpServer") { setting.Attributes[1].Value = "false|25|localhost"; }
                            if (setting.Attributes[0].Value == "ExtBaseUrl") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "RedirectProjectRoot") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "ExtBasePath") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "SSPI") { setting.Attributes[1].Value = "N"; }
                            if (setting.Attributes[0].Value == "DeployType") { setting.Attributes[1].Value = DeployType; }
                            if (setting.Attributes[0].Value == "PathTxtTemplate")
                            {
                                setting.Attributes[1].Value = setting.Attributes[1].Value.Replace(@"\" + oldNS + @"\", @"\" + newNS + @"\").Replace(oldProjectRoot,projectRoot);
                                if (!setting.Attributes[1].Value.ToLower().Contains(RuleTierPath.ToLower()))
                                {
                                    setting.Attributes[1].Value = new System.Text.RegularExpressions.Regex(@"^.+\\" + newNS + @"\\", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Replace(setting.Attributes[1].Value,projectRoot + "\\");
                                }
                                try
                                {
                                    System.IO.Directory.CreateDirectory(setting.Attributes[1].Value);
                                    if (!setting.Attributes[1].Value.ToLower().Contains(RuleTierPath.ToLower()))
                                    {
                                        SetDirectorySecurity(setting.Attributes[1].Value, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
                                    }
                                }
                                catch { }
                            }
                            if (setting.Attributes[0].Value == "PathXlsImport")
                            {
                                setting.Attributes[1].Value = setting.Attributes[1].Value.Replace(@"\" + oldNS + @"\", @"\" + newNS + @"\").Replace(oldProjectRoot, projectRoot);
                                if (!setting.Attributes[1].Value.ToLower().Contains(RuleTierPath.ToLower()))
                                {
                                    setting.Attributes[1].Value = new System.Text.RegularExpressions.Regex(@"^.+\\" + newNS + @"\\", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Replace(setting.Attributes[1].Value, projectRoot + "\\");
                                }
                                try
                                {
                                    System.IO.Directory.CreateDirectory(setting.Attributes[1].Value);
                                    if (!setting.Attributes[1].Value.ToLower().Contains(RuleTierPath.ToLower())) {
                                        SetDirectorySecurity(setting.Attributes[1].Value, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
                                    }
                                }
                                catch { }
                            }
                            if (setting.Attributes[0].Value == "PathTmpImport")
                            {
                                setting.Attributes[1].Value = setting.Attributes[1].Value.Replace(@"\" + oldNS + @"\", @"\" + newNS + @"\").Replace(oldProjectRoot, projectRoot);
                                if (!setting.Attributes[1].Value.ToLower().Contains(RuleTierPath.ToLower()))
                                {
                                    setting.Attributes[1].Value = new System.Text.RegularExpressions.Regex(@"^.+\\" + newNS + @"\\", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Replace(setting.Attributes[1].Value, projectRoot + "\\");
                                }
                                try
                                {
                                    System.IO.Directory.CreateDirectory(setting.Attributes[1].Value);
                                    if (!setting.Attributes[1].Value.ToLower().Contains(RuleTierPath.ToLower()))
                                    {
                                        SetDirectorySecurity(setting.Attributes[1].Value, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
                                    }
                                }
                                catch { }
                            }
                            if (setting.Attributes[0].Value == "PathRtfTemplate")
                            {
                                setting.Attributes[1].Value = setting.Attributes[1].Value.Replace(@"\" + oldNS + @"\", @"\" + newNS + @"\").Replace(oldProjectRoot, projectRoot);
                                if (!setting.Attributes[1].Value.ToLower().Contains(RuleTierPath.ToLower()))
                                {
                                    setting.Attributes[1].Value = new System.Text.RegularExpressions.Regex(@"^.+\\" + newNS + @"\\", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Replace(setting.Attributes[1].Value, projectRoot + "\\");
                                }
                                try
                                {
                                    System.IO.Directory.CreateDirectory(setting.Attributes[1].Value);
                                    if (!setting.Attributes[1].Value.ToLower().Contains(RuleTierPath.ToLower()))
                                    {
                                        SetDirectorySecurity(setting.Attributes[1].Value, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
                                    }
                                }
                                catch { }
                            }
                            if (setting.Attributes[0].Value == "RintagiLicense") { setting.Attributes[1].Value = ""; }
                            if (setting.Attributes[0].Value == "JWTMasterKey")
                            {
                                var jwtMasterKey = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16)));
                                try
                                {
                                    byte[] randomBits = new byte[32];
                                    using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                                    {
                                        // Fill the array with a random value.
                                        rngCsp.GetBytes(randomBits);
                                    }
                                    jwtMasterKey = Convert.ToBase64String(randomBits);

                                }
                                catch { }
                                setting.Attributes[1].Value = jwtMasterKey;
                            }
                            // empty out local configuration values
                            if ((localConfig ?? new string[] { }).Contains(setting.Attributes[0].Value))
                            {
                                setting.Attributes[1].Value = ""; 
                            }
                        }
					}
				}
				if (node.Name == "connectionStrings")
				{
					foreach (XmlNode setting in node.ChildNodes)
					{
						if (setting.Attributes != null && setting.Attributes.Count > 0)
						{
							if (setting.Attributes[0].Value == "LocalSQLServer") { setting.Attributes[1].Value = "Server=" + Svr + ";Database=aspnetdb;trusted_connection=no;User ID=Persona;password=Persona"; }
						}
					}
				}
                if (node.Name == "system.web")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "authentication")
                        {
                            foreach (XmlNode setting in child.ChildNodes)
                            {
                                if (setting.Name == "forms" && setting.Attributes != null && setting.Attributes.Count > 0)
                                {
                                    string oldName = setting.Attributes["name"].Value;
                                    setting.Attributes["name"].Value = oldName.StartsWith("."+oldNS) ?  oldName.Replace("." + oldNS, "." + newNS) : "." + newNS + "SecuAuth";
                                }
                            }
                        }
                        if (child.Name == "sessionState" && child.Attributes != null && child.Attributes.Count > 0)
                        {
                            string oldName = child.Attributes["cookieName"].Value;
                            child.Attributes["cookieName"].Value = oldName.StartsWith("ASP.NET_" + oldNS) ? oldName.Replace("_" + oldNS, "_" + newNS) : "ASP.NET_" + newNS + "SessionId";
                        }
                    }
                }
            }
			xd.Save(ConfigClientPath);
            // change other settings due to namespace change
            
            //string webConfig = File.ReadAllText(ConfigClientPath);
            //webConfig = new Regex(@"(<forms name="")([^""]+)""(.*)/>").Replace(webConfig, m => { return m.Groups[1] + "." + newNS + "SecuAuth" + m.Groups[3]; });
            //webConfig = new Regex(@"(<sessionState cookieName="")([^""]+)""(.*)/>").Replace(webConfig, m => { return m.Groups[1] + "ASP.NET_" + newNS + "SessionId" + m.Groups[3]; });
            //File.WriteAllText("test.txt", webConfig);

            try
            {
                xd.Load(ConfigWsPath);
                xn = xd.DocumentElement;
                foreach (XmlNode node in xn.ChildNodes)
                {
                    if (node.Name == "appSettings")
                    {
                        foreach (XmlNode setting in node.ChildNodes)
                        {
                            if (setting.Attributes != null && setting.Attributes.Count > 0)
                            {
                                if (setting.Attributes[0].Value == "DesProvider")
                                {
                                    if (DbProvider == "M") { setting.Attributes[1].Value = "Sqloledb"; } else { setting.Attributes[1].Value = "Sybase.ASEOLEDBProvider"; }
                                }
                                if (setting.Attributes[0].Value == "DesServer") { setting.Attributes[1].Value = Svr; }
                                if (setting.Attributes[0].Value == "DesUserId") { setting.Attributes[1].Value = AppUsr.Replace(".", "_"); }
                                if (setting.Attributes[0].Value == "DesPassword") { setting.Attributes[1].Value = EncryptString(AppPwd, encKey); }
                                if (setting.Attributes[0].Value == "ClientTierPath") { setting.Attributes[1].Value = ClientTierPath + "\\"; }
                                if (setting.Attributes[0].Value == "RuleTierPath") { setting.Attributes[1].Value = RuleTierPath + "\\"; }
                                //if (setting.Attributes[0].Value == "WsRptBaseUrl") { setting.Attributes[1].Value = WsRptBaseUrl; }
                                if (setting.Attributes[0].Value == "AppNameSpace") { setting.Attributes[1].Value = newNS; }
                                if (setting.Attributes[0].Value == "WsDomain") { setting.Attributes[1].Value = WebServer; }
                                if (setting.Attributes[0].Value == "WsRptDomain") { setting.Attributes[1].Value = WebServer; }
                                if (setting.Attributes[0].Value == "WsBaseUrl") { setting.Attributes[1].Value = "http://" + WebServer + "/" + newNS + "Ws"; }
                            }
                        }
                    }
                    if (node.Name == "connectionStrings")
                    {
                        foreach (XmlNode setting in node.ChildNodes)
                        {
                            if (setting.Attributes != null && setting.Attributes.Count > 0)
                            {
                                if (setting.Attributes[0].Value == "LocalSQLServer") { setting.Attributes[1].Value = "Server=" + Svr + ";Database=aspnetdb;trusted_connection=no;User ID=Persona;password=Persona"; }
                            }
                        }
                    }
                    /*
                    if (node.Name == "system.web")
                    {
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            if (child.Name == "authentication")
                            {
                                foreach (XmlNode setting in child.ChildNodes)
                                {
                                    if (setting.Name == "forms" && setting.Attributes != null && setting.Attributes.Count > 0)
                                    {
                                        setting.Attributes["name"].Value = setting.Attributes["name"].Value.Replace(".RO", "." + newNS);
                                    }
                                }
                            }
                            if (child.Name == "sessionState" && child.Attributes != null && child.Attributes.Count > 0)
                            {
                                child.Attributes["cookieName"].Value = child.Attributes["cookieName"].Value.Replace("_RO", "_" + newNS);
                            }
                        }
                    }
                    */
                }
                xd.Save(ConfigWsPath);
            }
            catch (Exception ex)
            {
                ReportError(string.Format("Failed to setup {0}({1}), not critical can be ignored", ConfigWsPath, ex.Message));
            }
		}

		public static void SetupSysUsr(string DbProvider, string Svr, string Usr, string Pwd, string Db, string oldNS, string newNS,string encKey, string AppUsr, string AppPwd, bool bIntegratedSecurity)
		{
            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db;
            OleDbConnection cn = new OleDbConnection(cs);
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
			+ " DECLARE @svr varchar(50) SELECT @svr = ?"
			+ " UPDATE dbo.Systems SET ServerName = @svr, dbAppProvider = ?, dbAppServer = @svr, dbAppUserId = ?, dbAppPassword = ?"
			+ ", dbAppDatabase = '" + newNS + "' + right(dbAppDatabase, len(dbAppDatabase) - len('" + oldNS + "'))"
			+ ", dbDesDatabase = '" + newNS + "' + right(dbDesDatabase, len(dbDesDatabase) - len('" + oldNS + "'))"
            + " WHERE dbAppDatabase LIKE '" + oldNS +"%'"
            , cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 900;
			cmd.Transaction = tr;
			cmd.Parameters.Add("@dbAppServer", OleDbType.VarChar).Value = Svr;
			if (DbProvider == "M")
			{
				cmd.Parameters.Add("@dbAppProvider", OleDbType.VarChar).Value = "Sqloledb";
			}
			else
			{
				cmd.Parameters.Add("@dbAppProvider", OleDbType.VarChar).Value = "Sybase.ASEOLEDBProvider";
			}
			cmd.Parameters.Add("@dbAppUserId", OleDbType.VarChar).Value = AppUsr.Replace(".","_");
			cmd.Parameters.Add("@dbAppPassword", OleDbType.VarChar).Value = EncryptString(AppPwd,encKey);
			try
			{
				cmd.ExecuteNonQuery();
				tr.Commit();
			}
			catch (Exception e)
			{
				tr.Rollback();
				ReportError(e.Message.ToString());
			}
			finally { cn.Close(); }
		}
        public static void SetupAdmUsr(string DbProvider, string Svr, string Usr, string Pwd, string Db, string oldNS, string newNS, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; ;
            OleDbConnection cn = new OleDbConnection(cs);
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            /* add login user per passed in */
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
            + " DECLARE @svr varchar(50) "
            + " IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Usr WHERE LoginName = '" + AppUsr + "')"
            + "     INSERT INTO dbo.Usr (LoginName, UsrName, UsrPassword, ConfirmPwd, UsrEmail, CultureId, InternalUsr, TechnicalUsr, UsrGroupLs, DefSystemId, Active, ModifiedOn, PwdDuration, PwdWarn) "
            + "     SELECT ?, ?, ?, ?, ?, ?, 'Y', 'Y', ?, ?, 'Y', GETDATE(), 90, 10 ",cn);
           //+ " SELECT ?, ?, 0x472013A6E5F09E05B63BFE2D642426BC4607733F, 0x472013A6E5F09E05B63BFE2D642426BC4607733F, ?, 'Y', 'Y', ?, ?, 'Y', GETDATE(), 90, 10 ",cn);

            Credential c = new Credential(AppUsr, AppPwd);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@LoginName", OleDbType.VarChar).Value = AppUsr;
            cmd.Parameters.Add("@UsrName", OleDbType.VarChar).Value = AppUsr;
            cmd.Parameters.Add("@UsrPassword", OleDbType.VarBinary).Value = c.Password;
            cmd.Parameters.Add("@ConfirmPassword", OleDbType.VarBinary).Value = c.Password;
            cmd.Parameters.Add("@UsrEmail", OleDbType.VarChar).Value = AppUsr;
            cmd.Parameters.Add("@CultureId", OleDbType.Integer).Value = 1;
            cmd.Parameters.Add("@UsrGroupLs", OleDbType.VarChar).Value = "(1)";
            cmd.Parameters.Add("@DefSystemId", OleDbType.Integer).Value = 3;
            try
            {
                /* no longer need to do this
                if (AppUsr.ToLower().Trim() != Usr.ToLower().Trim()) cmd.ExecuteNonQuery();
                */
                /* add sys user */
                OleDbCommand cmd2 = new OleDbCommand("SET NOCOUNT ON"
                + " DECLARE @svr varchar(50) "
                + " IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Usr WHERE LoginName = '" + "John Doe" + "')"
                + "     INSERT INTO dbo.Usr (LoginName, UsrName, UsrPassword, ConfirmPwd, UsrEmail, CultureId, InternalUsr, TechnicalUsr, UsrGroupLs, DefSystemId, Active, ModifiedOn, PwdDuration, PwdWarn) "
                + "     SELECT ?, ?, 0x472013A6E5F09E05B63BFE2D642426BC4607733F, 0x472013A6E5F09E05B63BFE2D642426BC4607733F, ?, ?, 'Y', 'Y', ?, ?, 'Y', GETDATE(), 90, 10 ", cn);

                cmd2.CommandType = CommandType.Text;
                cmd2.CommandTimeout = 900;
                cmd2.Transaction = tr;
                cmd2.Parameters.Add("@LoginName", OleDbType.VarChar).Value = "John Doe";
                cmd2.Parameters.Add("@UsrName", OleDbType.VarChar).Value = "John Doe";
                cmd2.Parameters.Add("@UsrEmail", OleDbType.VarChar).Value = "";
                cmd2.Parameters.Add("@CultureId", OleDbType.Integer).Value = 1;
                cmd2.Parameters.Add("@UsrGroupLs", OleDbType.VarChar).Value = "(5)";
                cmd2.Parameters.Add("@DefSystemId", OleDbType.Integer).Value = 3;

                cmd2.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ReportError(e.Message.ToString());
            }
            finally { cn.Close(); }
        }
        public static void SetupCompanyProject(string DbProvider, string Svr, string Usr, string Pwd, string Db, string oldNS, string newNS, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; ;
            OleDbConnection cn = new OleDbConnection(cs);
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            /* add firm/company/project to Cmon(db must be Cmon) */
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
            + " IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Firm)"
            + " BEGIN"
            + "     INSERT INTO dbo.Firm (TradeName, LegalName, Active) "
            + "     SELECT 'ACME', 'ACME', 'Y' "
            + " END"
            + " IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Company)"
            + " BEGIN"
            + "     INSERT INTO dbo.Company (FirmId, CompanyDesc, Active) "
            + "     SELECT TOP 1 FirmId, 'ACME', 'Y' FROM dbo.Firm ORDER BY FirmId "
            + " END"
            + " "
            + " IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Project)"
            + " BEGIN"
            + "     INSERT INTO dbo.Project (ProjectDesc, Active) "
            + "     SELECT 'Project 1', 'Y' "
            + " END"
            + " "
            , cn);

            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ReportError(e.Message.ToString());
            }
            finally { cn.Close(); }
        }

        public static void SetupAnonymousUsr(string DbProvider, string Svr, string Usr, string Pwd, string Db, string oldNS, string newNS, bool bIntegratedSecurity)
        {
            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; ;
            OleDbConnection cn = new OleDbConnection(cs);
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
            + " DECLARE @svr varchar(50) "
            + " IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Usr WHERE LoginName = '" + "Anonymous" + "')"
            + "     INSERT INTO dbo.Usr (LoginName, UsrName, UsrPassword, ConfirmPwd, UsrEmail, CultureId, InternalUsr, TechnicalUsr, UsrGroupLs, DefSystemId, Active, ModifiedOn, PwdDuration, PwdWarn) "
            + "     SELECT ?, ?, 0x94117513DF600DE48AB30814DFE0C9F032D9120C, 0x94117513DF600DE48AB30814DFE0C9F032D9120C, ?, ?, 'N', 'N', ?, ?, 'Y', GETDATE(), 90, 10 ", cn);

            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@LoginName", OleDbType.VarChar).Value = "Anonymous";
            cmd.Parameters.Add("@UsrName", OleDbType.VarChar).Value = "Anonymous";
            cmd.Parameters.Add("@UsrEmail", OleDbType.VarChar).Value = "";
            cmd.Parameters.Add("@CultureId", OleDbType.Integer).Value = 1;
            cmd.Parameters.Add("@UsrGroupLs", OleDbType.VarChar).Value = "(1)";
            cmd.Parameters.Add("@DefSystemId", OleDbType.Integer).Value = 5;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ReportError(e.Message.ToString());
            }
            finally { cn.Close(); }
        }

        public static void SetupTiers(string DbProvider, string Svr, string Usr, string Pwd, string Db, string oldNS, string newNS, string oldProjectRoot, string ClientTierPath, string WsTierPath, string XlsTierPath, string RuleTierPath, string encKey, string AppUsr, string AppPwd, string serverVer, bool bIntegratedSecurity)
		{

            KeyValuePair<string, string> bcpPath = GetSQLBcpPath();
            serverVer = bcpPath.Key;
            string bcpDir = bcpPath.Value.Replace(@"\bcp.exe", "\\");
            var regex = new System.Text.RegularExpressions.Regex(@"\\Rule\\?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var projectRoot = regex.Replace(RuleTierPath, "");
            oldProjectRoot = new System.Text.RegularExpressions.Regex(@"\\" + oldNS + @"\\?$").Replace(oldProjectRoot,@"\" + newNS);

            string cs = GetConnString(DbProvider, Svr, Usr, Pwd, bIntegratedSecurity) + ";database=" + Db; ;
            OleDbConnection cn = new OleDbConnection(cs);
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON"
			+ " DECLARE @svr varchar(50) SELECT @svr = ?"
            + " UPDATE dbo.DataTier SET ServerName = @svr, DesServer = @svr, DesUserId = ?, DesPassword = ?"
            + ", DesDatabase = '" + newNS + "' + right(DesDatabase, len(DesDatabase) - len('" + oldNS + "')), DataTierName = replace(DataTierName,'" + oldNS + "','" + newNS + "')"
            + ", PortBinPath = '" + bcpDir + "'"
            + ", InstBinPath = '" + bcpDir + "'"
                // + ", PortBinPath = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PortBinPath,'\\Client SDK\\ODBC\\110\\Tools\\Binn\\','\\" + serverVer + "\\Tools\\Binn\\'),'\\80\\','\\" + serverVer + "\\'),'\\90\\','\\" + serverVer + "\\'),'\\100\\','\\" + serverVer + "\\'),'\\110\\','\\" + serverVer + "\\')" + ",'\\120\\','\\" + serverVer + "\\')" + ",'\\130\\','\\" + serverVer + "\\')" + ",'" + @"\" + serverVer + @"\Tools\Binn\" + "','" + bcpDir + "')" + " "
           // + ", InstBinPath = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PortBinPath,'\\Client SDK\\ODBC\\110\\Tools\\Binn\\','\\" + serverVer + "\\Tools\\Binn\\'),'\\80\\','\\" + serverVer + "\\'),'\\90\\','\\" + serverVer + "\\'),'\\100\\','\\" + serverVer + "\\'),'\\110\\','\\" + serverVer + "\\')" + ",'\\120\\','\\" + serverVer + "\\')" + ",'\\130\\','\\" + serverVer + "\\')" + ",'" + @"\" + serverVer + @"\Tools\Binn\" + "','" + bcpDir + "')" + " "
            + " UPDATE dbo.ClientTier SET DevProgramPath = ?, DevCompilePath = replace(DevCompilePath,'\\" + oldNS + "','\\" + newNS + "'), WsProgramPath = ?, WsCompilePath = replace(WsCompilePath,'\\" + oldNS + "','\\" + newNS + "'), XlsProgramPath = ?, XlsCompilePath = replace(XlsCompilePath,'\\" + oldNS + "','\\" + newNS + "'), ClientTierName = replace(ClientTierName,'" + oldNS + "','" + newNS + "')"
            + " UPDATE dbo.RuleTier SET DevProgramPath = ?, RuleTierName = replace(RuleTierName,'" + oldNS + "','" + newNS + "')"
			+ " UPDATE dbo.Entity SET EntityCode = '" + newNS + "', DeployPath = replace(replace(DeployPath,'" + oldNS + "','" + newNS + "'),'E:\\','C:\\')"
            + " UPDATE dbo.ReleaseDtl SET ObjectName = replace(ObjectName,'" + oldNS + "','" + newNS + "')"
            + " UPDATE dbo.ClientTier SET DevCompilePath = replace(DevCompilePath,'" + oldProjectRoot + "','" + projectRoot + "'), WsCompilePath = replace(WsCompilePath,'" + oldProjectRoot + "','" + projectRoot + "'),XlsCompilePath = replace(XlsCompilePath,'" + oldProjectRoot + "','" + projectRoot + "')"
            + " UPDATE dbo.Entity SET DeployPath = replace(DeployPath,'" + oldProjectRoot + "','" + projectRoot + "')"
            , cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 900;
			cmd.Transaction = tr;
			cmd.Parameters.Add("@Svr", OleDbType.VarChar).Value = Svr;
			cmd.Parameters.Add("@Usr", OleDbType.VarChar).Value = AppUsr.Replace(".","_");
			cmd.Parameters.Add("@Pwd", OleDbType.VarChar).Value = EncryptString(AppPwd,encKey);
			cmd.Parameters.Add("@ClientTierPath", OleDbType.VarChar).Value = ClientTierPath + "\\";
			cmd.Parameters.Add("@WsTierPath", OleDbType.VarChar).Value = WsTierPath + "\\";
            cmd.Parameters.Add("@XlsTierPath", OleDbType.VarChar).Value = XlsTierPath + "\\";
            cmd.Parameters.Add("@RuleTierPath", OleDbType.VarChar).Value = RuleTierPath ; // no trailing
			try
			{
				cmd.ExecuteNonQuery();
				tr.Commit();
			}
			catch (Exception e)
			{
				tr.Rollback();
				ReportError(e.Message.ToString());
			}
			finally { cn.Close(); }
		}

		/*
		public static void DeployRdl(string Svr, string SrcPath, string TarPath, string WsUrl, string newNS)
		{
			bool bFound;
			int i1;
			int i2;
			string dsf;
			string dsn;
			RptServWs.ReportingService2005 rs = new RptServWs.ReportingService2005();
            rs.Url = WsUrl;
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
			DirectoryInfo srcDi = new DirectoryInfo(SrcPath + @"\reports");
			FileInfo[] fis = srcDi.GetFiles();
			foreach (FileInfo srcFi in fis)
			{
				if (srcFi.Extension == ".rdl")
				{
					// Find DataSource name:
					dsn = string.Empty;
					dsf = System.IO.File.ReadAllText(TarPath + @"\reports\" + srcFi.Name);
					if (dsf != string.Empty)
					{
						i1 = dsf.IndexOf("<DataSource Name=\"") + 18;
						i2 = dsf.IndexOf("\"", i1);
						if (i1 > 0 && i2 > i1) { dsn = dsf.Substring(i1, i2 - i1); }
					}
					// Create rdl on server:
                    FileStream fstream = System.IO.File.OpenRead(TarPath + @"\reports\" + srcFi.Name);
					byte[] rdef = new Byte[fstream.Length];
					fstream.Read(rdef, 0, (int)fstream.Length);
					fstream.Close();
					bFound = false;
					RptServWs.CatalogItem[] items = rs.ListChildren("/", false);
					foreach (RptServWs.CatalogItem ci in items)
					{
						if (ci.Type == RptServWs.ItemTypeEnum.Folder && ci.Name == newNS) { bFound = true; break; }
					}
					if (!bFound) { rs.CreateFolder(newNS, "/", null); }
					rs.CreateReport(srcFi.Name.Substring(0,srcFi.Name.Length - 4), "/" + newNS, true, rdef, null);
					// Create Data Source:
					bFound = false;
					items = rs.ListChildren("/" + newNS, false);
					foreach (RptServWs.CatalogItem ci in items)
					{
						if (ci.Type == RptServWs.ItemTypeEnum.DataSource && ci.Name == dsn) { bFound = true; break; }
					}
					if (!bFound)
					{
						RptServWs.DataSourceDefinition dsd = new RptServWs.DataSourceDefinition();
						dsd.ConnectString = "Data Source = " + Svr + "; Initial Catalog = " + dsn + ";";
						dsd.CredentialRetrieval = RptServWs.CredentialRetrievalEnum.Prompt;
						dsd.Enabled = true; dsd.Extension = "SQL";
						rs.CreateDataSource(dsn, "/" + newNS, false, dsd, null);
					}
					// Associate rdl with data source:
					RptServWs.DataSourceReference dsr = new RptServWs.DataSourceReference();
					dsr.Reference = "/" + newNS + "/" + dsn;
					RptServWs.DataSource ds = new RptServWs.DataSource();
					ds.Item = dsr; ds.Name = dsn;
					RptServWs.DataSource[] dss = new RptServWs.DataSource[] { ds };
					rs.SetItemDataSources("/" + newNS + "/" + srcFi.Name.Substring(0, srcFi.Name.Length - 4), dss);
				}
			}
		}
		*/
  
#if USE_J_SHARP
        private static void JFileZip(FileInfo fi, ZipOutputStream zos, string baseName)
        {
            string fn = fi.FullName;
            bool has_slash = baseName.EndsWith("/") || baseName.EndsWith("\\");
            string en = fn.Substring(baseName.Length + (has_slash ? 0 : 1));
            FileInputStream fis = new FileInputStream(fn);
            ZipEntry ze = new ZipEntry(en);
            java.io.File ef = new java.io.File(fn);
            ze.setTime(ef.lastModified());
            zos.putNextEntry(ze);
            sbyte[] buffer = new sbyte[1024];
            int len = 0;
            while ((len = fis.read(buffer)) >= 0)
            {
                zos.write(buffer, 0, len);
            }
            zos.closeEntry();
            fis.close();
        }
        private static void JFileZip(DirectoryInfo di, ZipOutputStream zos, string baseName, string match)
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                if (match == null || fi.Name == match) { JFileZip(fi, zos, baseName); }
            }
            foreach (DirectoryInfo dii in di.GetDirectories())
            {
                /* Currently J#'s zip utility does not support creating directory entries so empty directory would be ignored. */
                JFileZip(dii, zos, baseName, match);
            }
        }

        private static List<ZipEntry> GetZipFiles(ZipFile zipfil)
        {
            List<ZipEntry> lstZip = new List<ZipEntry>();
            Enumeration zipEnum = zipfil.entries();
            while (zipEnum.hasMoreElements())
            {
                ZipEntry zip = (ZipEntry)zipEnum.nextElement();
                lstZip.Add(zip);
            }
            return lstZip;
        }

        public static void JFileZip(string zipFr, string zipTo, bool bRecursive, bool bRmFr)
        {

            FileOutputStream fos = new FileOutputStream(zipTo);
            ZipOutputStream zos = new ZipOutputStream(fos);
            DirectoryInfo di = new DirectoryInfo(zipFr);
            if (di.Exists)
            {
                foreach (FileInfo fi in di.GetFiles())
                {
                    JFileZip(fi, zos, zipFr);
                }
                if (bRecursive)
                {
                    foreach (DirectoryInfo dii in di.GetDirectories())
                    {
                        if (dii.FullName != zipTo) { JFileZip(dii, zos, zipFr, null); }
                    }
                }
            }
            else
            {
                FileInfo fi = new FileInfo(zipFr);
                if (fi.Exists)
                {
                    JFileZip(fi, zos, fi.DirectoryName);
                }
                if (bRecursive)
                {
                    di = new DirectoryInfo(fi.DirectoryName);
                    foreach (DirectoryInfo dii in di.GetDirectories())
                    {
                        if (dii.FullName != zipTo) { JFileZip(dii, zos, fi.DirectoryName + "\\", fi.Name); }
                    }
                }
            }
            zos.close();
            if (bRmFr) { Directory.Delete(zipFr, true); }
        }


        public static void JFileUnzip(string zipFileName, string destinationPath)
        {
            ZipFile zipFile = new ZipFile(zipFileName);
            List<ZipEntry> zipFiles = GetZipFiles(zipFile);
            foreach (ZipEntry zip in zipFiles)
            {
               if (!zip.isDirectory())
               {
                  InputStream s = zipFile.getInputStream(zip);
                  try
                  {
                     Directory.CreateDirectory(destinationPath + "\\" + Path.GetDirectoryName(zip.getName()));
                     FileOutputStream dest = new FileOutputStream(Path.Combine(destinationPath + "\\" + Path.GetDirectoryName(zip.getName()), Path.GetFileName(zip.getName())));
                     try
                     {
                        int len = 0;
                        sbyte[] buffer = new sbyte[7168];
                        while ((len = s.read(buffer)) >= 0) { dest.write(buffer, 0, len); }
                     }
					 catch (Exception ex) { ReportError(ex.Message); }
					 finally { dest.close(); }
                  }
				  catch (Exception ex) { ReportError(ex.Message); }
				  finally { s.close(); }
               }
            }
            zipFile.close();
        }
#else
        public static void JFileZip(string zipFr, string zipTo, bool bRecursive, bool bRmFr)
        {
            using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile(zipTo, System.Text.Encoding.UTF8))
            //using (Ionic.Utils.Zip.ZipFile zipFile = new Ionic.Utils.Zip.ZipFile(zipTo))
            {
                if (!zipTo.StartsWith("\\\\")) zipFile.TempFileFolder = Path.GetDirectoryName(zipTo);
                zipFile.AddDirectory(zipFr, "");
                zipFile.UseZip64WhenSaving = Ionic.Zip.Zip64Option.Always;
                //zipFile.ParallelDeflateThreshold = -1;
                zipFile.Save();
            }

        }

        public static void JFileZip(string zipFr, string zipTo, bool bRecursive, bool bRmFr, List<string> excludeDir)
        {
            using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile(zipTo, System.Text.Encoding.UTF8))
            //using (Ionic.Utils.Zip.ZipFile zipFile = new Ionic.Utils.Zip.ZipFile(zipTo))
            {
                if (!zipTo.StartsWith("\\\\")) zipFile.TempFileFolder = Path.GetDirectoryName(zipTo);
                zipFile.ExcludeDir = excludeDir;
                zipFile.AddDirectory(zipFr, "");
                zipFile.UseZip64WhenSaving = Ionic.Zip.Zip64Option.Always;
                //zipFile.ParallelDeflateThreshold = -1;
                zipFile.Save();
            }

        }
        public static void JFileUnzip(string zipFileName, string destinationPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(destinationPath);
                if (di.Parent == null) throw new Exception("Do not run the installer from the root directory, copy it to a temp directory and run it");
                else
                {
                    di.Delete(true);
                }
            }
            catch { }

            using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile(zipFileName, System.Text.Encoding.UTF8))
            {
                zipFile.ExtractAll(destinationPath);
            }
            // for this particular case we change the attribute to normal
            if (Directory.Exists(destinationPath))
            {
                try { RemoveROAttribute(destinationPath); }
                catch (Exception e) { ReportError(e.Message.ToString()); }
            }
        }
        public static void RemoveROAttribute(string directoryPath) 
        { 
            var rootInfo = new DirectoryInfo(directoryPath) { Attributes = FileAttributes.Normal }; 
            foreach (var fileInfo in rootInfo.GetFileSystemInfos()) fileInfo.Attributes = FileAttributes.Normal; 
            foreach (var subDirectory in Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories)) 
            { 
                var subInfo = new DirectoryInfo(subDirectory) { Attributes = FileAttributes.Normal }; 
                foreach (var fileInfo in subInfo.GetFileSystemInfos()) fileInfo.Attributes = FileAttributes.Normal; 
            } 
        } 
#endif
        /// <summary>
        /// method for setting permissions for a specified directory
        /// </summary>
        /// <param name="dir">directory we're working with</param>
        /// <param name="user">user account</param>
        /// <param name="rights">FileSystemRights enum value (http://msdn.microsoft.com/en-us/library/system.security.accesscontrol.filesystemrights.aspx)</param>
        /// <param name="inheritance">InheritanceFlags value (http://msdn.microsoft.com/en-us/library/system.security.accesscontrol.inheritanceflags.aspx)</param>
        /// <param name="propagation">PropagationFlags value (http://msdn.microsoft.com/en-us/library/system.security.accesscontrol.propagationflags.aspx)</param>
        /// <param name="control">AccessControlType value (http://msdn.microsoft.com/en-us/library/w4ds5h86.aspx)</param>
        /// <returns></returns>
        /// 
        public static bool SetDirectorySecurity(string dir, string user, FileSystemRights rights, AccessControlType control, bool silent = false)
        {
            try
            {
                //create a new DirectoryInfo object for the directory we're working with
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                //get the current security settings for the specified directory
                DirectorySecurity security = dirInfo.GetAccessControl();
 
                //add the new access rule
                security.AddAccessRule(new FileSystemAccessRule(user, rights,System.Security.AccessControl.InheritanceFlags.ContainerInherit, System.Security.AccessControl.PropagationFlags.None, control));
                security.AddAccessRule(new FileSystemAccessRule(user, rights, System.Security.AccessControl.InheritanceFlags.ObjectInherit, System.Security.AccessControl.PropagationFlags.None, control));

                //apply the new settings
                dirInfo.SetAccessControl(security);

                //all went well
                return true;
            }
            catch (Exception ex)
            {
                if (!silent) ReportError(ex.Message);
                return false;
            }
        }
        
		protected static string DecryptString(string inStr, string key)
		{
			string outStr = string.Empty;
			string encrypt_key = key;
            if (!string.IsNullOrEmpty(key))
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                try
                {
                    des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(encrypt_key));
                    outStr = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(inStr), 0, Convert.FromBase64String(inStr).Length));
                }
                catch (Exception ex) { ReportError(ex.Message); }
                hashmd5 = null;
                des = null;
                return outStr;
            }
            else return inStr;
		}
        public static void ReportError(string msg)
        {
            if (errorMsg != null)
            {
                errorMsg(msg);
            }
            else
            {
                MessageBox.Show(msg);
            }
        }
        public static DialogResult ConfirmDialog(string msg, string caption)
        {
            if (msgBox != null)
            {
                return msgBox(msg, caption);
            }
            else
            {
                return MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }
        public static bool Backup(Dictionary<string,string> tiers, Dictionary<string,string> dataServer, string clientTargetDir, string dataTargetDir, Action<int,string> progress, bool bIntegratedSecurity)
        {
            string serverName = dataServer["server"] ?? "";
            bool isLocalServer = serverName.ToUpper().StartsWith(Environment.MachineName) || serverName.ToLower().StartsWith("localhost") || serverName.StartsWith(".");
            bool dataTargetAccessible = false;
            bool createdViaServer = false;
            try
            {
                if (!string.IsNullOrEmpty(clientTargetDir) && !Directory.Exists(clientTargetDir)) { Directory.CreateDirectory(clientTargetDir); }
            }
            catch (Exception ex)
            {
                ReportError(string.Format("Fail to access client tier backup directory {0} due to {1}", clientTargetDir, ex.Message));
                return false;
            }
            try
            {
                if (!string.IsNullOrEmpty(dataTargetDir))
                {
                    try
                    {
                        if (!isLocalServer && !dataTargetDir.StartsWith(@"\\"))
                        {
                            createdViaServer = Utils.MakeSQLServerDirectory(dataServer["serverType"], dataServer["server"], dataServer["user"], dataServer["password"], dataServer["design"], dataTargetDir + "\\Data\\", bIntegratedSecurity);
                            if (Directory.Exists(dataTargetDir + "\\Data"))
                            {
                                dataTargetAccessible = true;
                            }
                            else
                            {
                                try
                                {
                                    Directory.CreateDirectory(dataTargetDir + "\\Data");
                                }
                                catch (Exception ex1)
                                {
                                    ReportError(string.Format("Fail to create data tier backup directory {0} due to {1}", dataTargetDir, ex1.Message));
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                Directory.CreateDirectory(dataTargetDir + "\\Data");
                                createdViaServer = true;
                                dataTargetAccessible = true;
                            }
                            catch (Exception ex1)
                            {
                                ReportError(string.Format("Fail to create data tier backup directory {0} due to {1}", dataTargetDir, ex1.Message));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ReportError(string.Format("Fail to create data tier backup directory {0} via server due to {1}", dataTargetDir, ex.Message));
                    }

                }
            }
            catch (Exception ex)
            {
                ReportError(string.Format("Fail to access  data tier backup directory {0} due to {1}", dataTargetDir, ex.Message));
                //if (!string.IsNullOrWhiteSpace(clientTargetDir)) return false;
            }
            if (!string.IsNullOrEmpty(clientTargetDir))
            {
                progress(20, tiers.ContainsKey("client") && !string.IsNullOrEmpty(tiers["client"]) ? string.Format("Backing up client tier files {0} ...", tiers["client"]) : "Skip client tier files ...");
                if (tiers.ContainsKey("client") && !string.IsNullOrEmpty(tiers["client"]))
                {
                    if (!Directory.Exists(tiers["client"]))
                    {
                        ReportError(string.Format("client tier {0} not exist will skip", tiers["client"]));
                        return false;
                    }
                    else
                    {
                        Utils.JFileZip(tiers["client"], clientTargetDir + "\\Cln.zip", true, false);
                    }
                }
                progress(10, tiers.ContainsKey("ws") && !string.IsNullOrEmpty(tiers["ws"]) ? string.Format("Backing up web service tier files {0} ...", tiers["ws"]) : "Skip web service tier files ...");
                if (tiers.ContainsKey("ws") && !string.IsNullOrEmpty(tiers["ws"]))
                {
                    if (!Directory.Exists(tiers["ws"]))
                    {
                        ReportError(string.Format("web service tier {0} not exist will skip", tiers["ws"]));
                        //return false;
                    }
                    else
                    {
                        Utils.JFileZip(tiers["ws"], clientTargetDir + "\\Wsv.zip", true, false);
                    }
                }
                progress(10, tiers.ContainsKey("xls") && !string.IsNullOrEmpty(tiers["xls"]) ? string.Format("Backing up xls tier files {0} ...", tiers["xls"]) : "Skip xls tier files ...");
                if (tiers.ContainsKey("xls") && !string.IsNullOrEmpty(tiers["xls"]))
                {
                    if (!Directory.Exists(tiers["xls"]))
                    {
                        ReportError(string.Format("xls tier {0} not exist, will skip", tiers["xls"]));
                        //return false;
                    }
                    else
                    {
                        Utils.JFileZip(tiers["xls"], clientTargetDir + "\\Xls.zip", true, false);

                    }
                }
                progress(20, tiers.ContainsKey("rule") && !string.IsNullOrEmpty(tiers["rule"]) ? string.Format("Backing up rule tier files {0} ...", tiers["rule"]) : "Skip rule tier files ...");
                if (tiers.ContainsKey("rule") && !string.IsNullOrEmpty(tiers["rule"]))
                {
                    if (!Directory.Exists(tiers["rule"]))
                    {
                        ReportError(string.Format("rule tier {0} not exist will skip", tiers["rule"]));
                        return false;
                    }
                    else
                    {
                        Utils.JFileZip(tiers["rule"], clientTargetDir + "\\Rul.zip", true, false, new List<string> { "\\Deploy*\\", ".git", "\\node_modules\\", "\\npm\\", "\\npm_cache\\", ".vs", "Import", "PrecompiledWeb", "Web", "WsXls", "\\*Ws\\" });
                    }
                }
            }
            else
            {
                progress(60, "Skip client tier files ...");
            }
            progress(20, !string.IsNullOrEmpty(dataTargetDir) && !string.IsNullOrEmpty(dataServer["serverType"]) ? string.Format("Backing up data tier files {0} to {1} ...", dataServer["server"], dataTargetDir) : "Skip data tier files ...");
            if (!string.IsNullOrEmpty(dataTargetDir) && !string.IsNullOrEmpty(dataServer["serverType"]))
            {
                string backupBaseName = dataTargetDir + (createdViaServer ? "\\Data\\" : "_");
                Utils.BackupServer(dataServer["serverType"], dataServer["server"], dataServer["user"], dataServer["password"], dataServer["design"], backupBaseName, bIntegratedSecurity);
                if (dataTargetDir.IndexOf("\\") >= 0 && clientTargetDir == dataTargetDir)	// Windows path:
                {
                    try
                    {
                        if (dataTargetAccessible || new DirectoryInfo(dataTargetDir + "\\Data").GetFiles().Length > 0)
                        {
                            Utils.JFileZip(dataTargetDir + "\\Data", dataTargetDir + "\\Dat.zip", true, false);
                            Directory.Delete(dataTargetDir + "\\Data", true);
                        }
                        else
                        {
                            ReportError(string.Format("data backup not accessible, will skip zipping data tier file backups located on server, {0}*", backupBaseName));
                        }
                    }
                    catch (Exception ex)
                    {
                        ReportError(string.Format("failed to zip data data tier file backups, {0}. {1}", dataTargetDir + "\\Data", ex.Message));
                    }
                }
            }
            else
            {
                progress(20, "Skip data tier files ...");
            }
            progress(20, "Backup completed.");
            return true;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = true)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        private static void InstallLib(string ruleTierPath, string clientTierPath)
        {
            if (Directory.Exists(clientTierPath + "/bin") && Directory.Exists(ruleTierPath))
            {
                try
                {
                    string libPath = ruleTierPath + "/Lib";
                    if (!Directory.Exists(libPath)) Directory.CreateDirectory(libPath);
                    string[] Dlls = new string[]{
                            "Access3.dll"
                            ,"AjaxControlToolkit.dll"
                            ,"Common3.dll"
                            ,"ExifLib.dll"
                            ,"Facade3.dll"
                            ,"License3.dll"
                            ,"Newtonsoft.Json.dll"
                            ,"Rule3.dll"
                            ,"Service3.dll"
                            ,"SystemFramewk.dll"
                            ,"WebControls.dll"
                            ,"WebRules.dll"
                    };
                    foreach (string n in Dlls)
                    {
                        string from = clientTierPath + @"\bin\" + n;
                        string to = libPath + @"\" + n;
                        try
                        {
                            File.Copy(from, to , true);
                        }
                        catch (Exception ex)
                        {
                            ReportError(string.Format("failed to copy {0} to Lib, {1}", from, ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    ReportError(string.Format("failed to popular Lib, {0}", ex.Message));
                }
            }
        }
        private static void InstallShadow(string ruleTierPath, string clientTierPath, string shadowRootName)
        {
            string shadowRoot = ruleTierPath + @"\Shadow\";
            string shadowDirectory = shadowRoot + shadowRootName;
            string targetRuleDirecty = ruleTierPath;
            if (Directory.Exists(shadowDirectory) && !string.IsNullOrEmpty(shadowRootName)) {
                DirectoryCopy(shadowDirectory, targetRuleDirecty, true);
                try
                {
                    if (shadowRootName.StartsWith("Com")) Directory.Delete(shadowRoot, true);
                } catch (Exception ex)
                {
                    ReportError("Failed to remove shadow directory " + ex);
                }
            }
        }
        public static void NewApp(Dictionary<string, string> tiers, Dictionary<string, string> dataServer, string clientTargetDir, string dataTargetDir, Action<int, string> progress,
            Item item,			// Version info.
            ItemPDT iPDT,		// Existing Rbt:Rintagi/App:Production.
            ItemDEV iDEV,		// Existing Rbt:Developer/App:Extranet.
            ItemPTY iPTY,		// Existing Rbt:Application/App:Prototype.
            ItemNPDT nPDT,		// New Rbt:Rintagi/App:Production.
            ItemNDEV nDEV,	// New Rbt:Developer/App:Extranet.
            ItemNPTY nPTY,		// New Rbt:Application/App:Prototype.        
            bool noUser,         // do not create users
            bool cli             // interactive or console
            )
        {
            if (Application.StartupPath.EndsWith(":\\") || Application.StartupPath.EndsWith(":") || Application.StartupPath.StartsWith("\\\\")) 
            {
                ReportError(@"Please do not run the installer from a root direcotory(like C:\) or across network(like \\server\share\install.exe)");
                return;
            }
            bool isSQLServer = dataServer["serverType"] == "M";
            string serverVer = dataServer["serverVer"];
            bool isNPDT = item.GetInsType() == "PDT" || item.GetInsType() == "NPDT";
            bool isNet2 = tiers["isNet2"] == "Y";
            bool hasRule = tiers.ContainsKey("rule") && !string.IsNullOrEmpty(tiers["rule"]);
            bool isDev = item.GetInsType() == "DEV" || item.GetInsType() == "NDEV";
            bool isPty = item.GetInsType() == "PTY" || item.GetInsType() == "NPTY";
            bool bEnable32bit = tiers["enable32Bit"] == "Y";

            string dbServer = dataServer["server"];
            string dbPath = dataServer["dbpath"];
            string SysUserName = dataServer["user"];
            string SysPwd = dataServer["password"];
            string appUserName = dataServer["appUser"];
            string appPwd = dataServer["appPwd"];
            string dbServerType = dataServer["serverType"];
            string webServer = tiers["webServer"];
            string newNS = tiers["newNS"];
            string oldNS = item.GetOldNS();
            string clientTier = tiers["client"];
            string wsTier = tiers["ws"];
            string xlsTier = tiers["xls"];
            string ruleTier = tiers["rule"];
            string wsUrl = tiers["wsUrl"];
            string DeployType = "DEV";
            string site = tiers.ContainsKey("site") ? tiers["site"] : "";
            string windowsServiceAccount = tiers.ContainsKey("WindowsServiceAccount") ? tiers["WindowsServiceAccount"] : "Network Service";
            string oldProjectRoot = item.GetOldProjectRoot();
            bool isDataTier = !string.IsNullOrEmpty(dbPath);
            bool hasDeploy = item.GetHasDeploy();
            bool bIntegratedSecurity = dataServer["IntegratedSecurity"] == "Y";
            if (isDataTier)
            {
                bool ok = Utils.CreateTestDatabase(isSQLServer ? "M" : "S",
                                    dbServer, SysUserName, SysPwd, newNS + "Design", dbPath, serverVer, bIntegratedSecurity);
                if (!ok) return;
            }
            else
            {
                if (!string.IsNullOrEmpty(appUserName) && appUserName != "dummy" && !Utils.TestSQL("M", dbServer, appUserName, appPwd, false))
                {
                    return;
                }
            }

            if (isDataTier && isSQLServer && dbServer.EndsWith(@"\") || dbServer.ToUpper().EndsWith("MSSQLSERVER"))
            {
                ReportError(@"Use '.' or just the server name WITHOUT trailing '\' if you want to specify the default MSSQLSERVER instance");
                return;
            }

            if (appUserName != "dummy" && !Utils.CreateLogin(isSQLServer ? "M" : "S", dbServer, SysUserName, SysPwd, appUserName, appPwd, bIntegratedSecurity))
            {
                ReportError(@"AppUsr/Pwd cannot be created or have no access to the server");
                return;
            }

            if (!string.IsNullOrEmpty(ruleTier)) SetProjectRoot(ruleTier, windowsServiceAccount, cli);
            if (isNPDT) // App:Production
            {
                if (isNPDT) { DeployType = "PRD"; }
                Utils.SetupFilePermissions(clientTier, string.Empty, windowsServiceAccount, newNS, progress, true);
                nPDT.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                iPDT.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                Utils.SetupIIS(isNet2, site, newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                if (site != "Default Web Site")
                {
                    try
                    {
                        Utils.SetupIIS(isNet2, "Default Web Site", newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                    }
                    catch { }
                    try
                    {
                        Utils.SetupIIS(isNet2, "localhost", newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                    }
                    catch { }
                }
                if (isSQLServer)
                {
                    Utils.SetupConfigFile("M", dbServer, SysUserName, SysPwd, clientTier + "\\Web.config", wsTier + "\\Web.config", clientTier, ruleTier, oldProjectRoot, windowsServiceAccount, wsUrl, newNS, oldNS, webServer, DeployType, item.GetROKey(), appUserName, appPwd, item.localWebConfig);
                }
                else
                {
                    Utils.SetupConfigFile("S", dbServer, SysUserName, SysPwd, clientTier + "\\Web.config", wsTier + "\\Web.config", clientTier, ruleTier, oldProjectRoot, windowsServiceAccount, wsUrl, newNS, oldNS, webServer, DeployType, item.GetROKey(), appUserName, appPwd, item.localWebConfig);
                }
                if (isDataTier)
                {
                    if (isSQLServer)
                    {
                        nPDT.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupSysUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, item.GetROKey(), appUserName, appPwd, bIntegratedSecurity);
                        nPDT.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        nPDT.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (!noUser)
                        {
                            Utils.SetupAnonymousUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, bIntegratedSecurity);
                            Utils.SetupAdmUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, appUserName, appPwd, bIntegratedSecurity);
                        }
                        /* assuming table have all been created and View/Function would only appear in upgrade but not new, run the upgrade */
                        iPDT.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iPDT.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iPDT.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupTiers("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, oldProjectRoot, clientTier, wsTier, xlsTier, ruleTier, item.GetROKey(), appUserName, appPwd, serverVer, bIntegratedSecurity);
                    }
                    else
                    {
                        nPDT.InstSysS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupSysUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, item.GetROKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupTiers("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, oldProjectRoot, clientTier, wsTier, xlsTier, ruleTier, item.GetROKey(), appUserName, appPwd, serverVer, bIntegratedSecurity);
                        nPDT.InstDesS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        nPDT.InstAppS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (!noUser)
                        {
                            Utils.SetupAnonymousUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, bIntegratedSecurity);
                            Utils.SetupAdmUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, appUserName, appPwd, bIntegratedSecurity);
                        }
                        nPDT.InstDesS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                    }
                }
            }
            else if (oldNS == "RO" && isDev)
            {
                Utils.SetupFilePermissions(clientTier, ruleTier, newNS, windowsServiceAccount, progress, false);
                nDEV.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                iDEV.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                if (hasRule) {
                    nDEV.InstRul(ruleTier, newNS, progress);
                    iDEV.InstRul(ruleTier, newNS, progress);
                }
                InstallShadow(ruleTier, clientTier, item.GetShadowRootName());
                Utils.SetupIIS(isNet2, site, newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                if (site != "Default Web Site")
                {
                    try
                    {
                        Utils.SetupIIS(isNet2, "Default Web Site", newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                    }
                    catch { }
                    try
                    {
                        Utils.SetupIIS(isNet2, "localhost", newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                    }
                    catch { }
                }
                if (isSQLServer)
                {
                    Utils.SetupConfigFile("M", dbServer, SysUserName, SysPwd, clientTier + "\\Web.config", wsTier + "\\Web.config", clientTier, ruleTier, oldProjectRoot, windowsServiceAccount, wsUrl, newNS, oldNS, webServer, DeployType, item.GetROKey(), appUserName, appPwd, item.localWebConfig);
                }
                else
                {
                    Utils.SetupConfigFile("S", dbServer, SysUserName, SysPwd, clientTier + "\\Web.config", wsTier + "\\Web.config", clientTier, ruleTier, oldProjectRoot, windowsServiceAccount, wsUrl, newNS, oldNS, webServer, DeployType, item.GetROKey(), appUserName, appPwd, item.localWebConfig);
                }
                if (isDataTier)
                {
                    if (isSQLServer)
                    {
                        nDEV.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupSysUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, item.GetROKey(), appUserName, appPwd, bIntegratedSecurity);
                        nDEV.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        nDEV.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (!noUser)
                        {
                            Utils.SetupAnonymousUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, bIntegratedSecurity);
                            Utils.SetupAdmUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, appUserName, appPwd, bIntegratedSecurity);
                            Utils.SetupCompanyProject("M", dbServer, SysUserName, SysPwd, newNS + "Cmon", oldNS, newNS, appUserName, appPwd, bIntegratedSecurity);
                        }
                        /* assuming table have all been created and View/Function would only appear in upgrade but not new, run the upgrade */
                        iDEV.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iDEV.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iDEV.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        /* 
                        nDEV.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd);
                        -- run one more time as the *D SP upgrade is done before TmplD is created 
                        nDEV.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd);
                        */
                        Utils.SetupTiers("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, oldProjectRoot, clientTier, wsTier, xlsTier, ruleTier, item.GetROKey(), appUserName, appPwd, serverVer, bIntegratedSecurity);
                    }
                    else
                    {
                        nDEV.InstSysS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupSysUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, item.GetROKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupTiers("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, oldProjectRoot, clientTier, wsTier, xlsTier, ruleTier, item.GetROKey(), appUserName, appPwd, serverVer, bIntegratedSecurity);
                        nDEV.InstAppS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (!noUser)
                        {
                            Utils.SetupAnonymousUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, bIntegratedSecurity);
                            Utils.SetupAdmUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, appUserName, appPwd, bIntegratedSecurity);
                        }
                        nDEV.InstDesS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                    }
                }
                MakeNewApp(oldNS, newNS,ruleTier); MakeNewDev(false,oldNS,newNS,clientTier,ruleTier, hasDeploy, null); SetFilePermission(clientTier,ruleTier,wsTier,xlsTier, windowsServiceAccount, newNS);

                if (oldNS == "RO")
                {
                    /* recompile as the dll is now invalid after the key change */
                    if (!System.IO.File.Exists(ruleTier + "\\" + "UsrRules\\UsrRule.cs"))
                    {
                        try
                        {
                            if (!System.IO.Directory.Exists(ruleTier + "\\" + "UsrRules")) System.IO.Directory.CreateDirectory(ruleTier + "\\" + "UsrRules");
                            using (StreamWriter sw = System.IO.File.CreateText(ruleTier + "\\" + "UsrRules\\UsrRule.cs"))
                            {
                                sw.WriteLine("namespace " + newNS + ".UsrRules");
                                sw.WriteLine("{");
                                sw.WriteLine("	public class UsrRule {} ");
                                sw.WriteLine("}");
                            }

                        }
                        catch (Exception ex) { ReportError("Failed to create stub UsrRule.cs: " + ex.Message); }
                    }
                    if (!System.IO.File.Exists(ruleTier + "\\" + "UsrAccess\\UsrAccess.cs"))
                    {
                        try
                        {
                            if (!System.IO.Directory.Exists(ruleTier + "\\" + "UsrAccess")) System.IO.Directory.CreateDirectory(ruleTier + "\\" + "UsrAccess");
                            using (StreamWriter sw = System.IO.File.CreateText(ruleTier + "\\" + "UsrAccess\\UsrAccess.cs"))
                            {
                                sw.WriteLine("namespace " + newNS + ".UsrAccess");
                                sw.WriteLine("{");
                                sw.WriteLine("	public class UsrAccess {} ");
                                sw.WriteLine("}");
                                sw.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            ReportError("Failed to create stub UsrAccess.cs: " + ex.Message);
                        }

                    }
                }
                // we must build the app to copy the usraccess.dll and usrrule.dll to the proper bin
                if (oldNS == "RO")
                {
                    /* recompile as the dll containing the encryption key may not be the same */
                    string cmd_arg = "\"" + clientTier.Substring(0, clientTier.Length - 4) + "\\" + newNS + ".sln\" /p:Configuration=Release /t:Rebuild /v:minimal /nologo";
                    progress(95, "Rebuilding new " + newNS + " installation ...");
                    Utils.ExecuteCommand("C:\\WINDOWS\\Microsoft.NET\\" + (isNet2 ? "Framework\\v3.5" : "Framework64\\v4.0.30319") + "\\msbuild.exe", cmd_arg, true);
                }
            }
            else if ((oldNS != "RO" || item.GetInsType().Contains("PTY")) && isPty) // App:Prototype
            {

                Utils.SetupFilePermissions(clientTier, ruleTier, newNS, windowsServiceAccount, progress, false);
                nPTY.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                iPTY.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                if (hasRule) {
                    nPTY.InstRul(ruleTier, newNS, progress);
                    iPTY.InstRul(ruleTier, newNS, progress);
                }
                InstallShadow(ruleTier, clientTier, item.GetShadowRootName());

                if (newNS == "RO")
                {
                    /* change secret key */
                    item.SetROKey(Utils.SetupEncryptionKey(ruleTier));
                }
                Utils.SetupIIS(isNet2, site, newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                if (site != "Default Web Site")
                {
                    try
                    {
                        Utils.SetupIIS(isNet2, "Default Web Site", newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                    }
                    catch { }
                    try
                    {
                        Utils.SetupIIS(isNet2, "localhost", newNS, clientTier, wsTier, xlsTier, bEnable32bit);
                    }
                    catch { }
                }
                if (isSQLServer)
                {
                    Utils.SetupConfigFile("M", dbServer, SysUserName, SysPwd, clientTier + "\\Web.config", wsTier + "\\Web.config", clientTier, ruleTier, oldProjectRoot, windowsServiceAccount, wsUrl, newNS, oldNS, webServer, DeployType, item.GetROKey(), appUserName, appPwd, item.localWebConfig);
                }
                else
                {
                    Utils.SetupConfigFile("S", dbServer, SysUserName, SysPwd, clientTier + "\\Web.config", wsTier + "\\Web.config", clientTier, ruleTier, oldProjectRoot, windowsServiceAccount, wsUrl, newNS, oldNS, webServer, DeployType, item.GetROKey(), appUserName, appPwd, item.localWebConfig);
                }
                if (isDataTier)
                {
                    if (isSQLServer)
                    {
                        nPTY.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupSysUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, item.GetROKey(), appUserName, appPwd, bIntegratedSecurity);
                        nPTY.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        nPTY.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (!noUser)
                        {
                            Utils.SetupAnonymousUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, bIntegratedSecurity);
                            Utils.SetupAdmUsr("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, appUserName, appPwd, bIntegratedSecurity);
                            Utils.SetupCompanyProject("M", dbServer, SysUserName, SysPwd, newNS + "Cmon", oldNS, newNS, appUserName, appPwd, bIntegratedSecurity);
                        }
                        iPTY.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iPTY.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iPTY.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupTiers("M", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, oldProjectRoot, clientTier, wsTier, xlsTier, ruleTier, item.GetROKey(), appUserName, appPwd, serverVer, bIntegratedSecurity);
                    }
                    else
                    {
                        nPTY.InstSysS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupSysUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, item.GetROKey(), appUserName, appPwd, bIntegratedSecurity);
                        Utils.SetupTiers("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, oldProjectRoot, clientTier, wsTier, xlsTier, ruleTier, item.GetROKey(), appUserName, appPwd, serverVer, bIntegratedSecurity);
                        nPTY.InstDesS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        nPTY.InstAppS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (!noUser)
                        {
                            Utils.SetupAnonymousUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, bIntegratedSecurity);
                            Utils.SetupAdmUsr("S", dbServer, SysUserName, SysPwd, newNS + "Design", oldNS, newNS, appUserName, appPwd, bIntegratedSecurity);
                        }
                        nPTY.InstDesS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                    }
                }
                MakeNewDev(false,oldNS,newNS,clientTier,ruleTier, hasDeploy,null); SetFilePermission(clientTier,ruleTier,wsTier,xlsTier, windowsServiceAccount, newNS);

                if (newNS == "RO")
                {
                    /* recompile as the dll containing the encryption key may not be the same */
                    string cmd_arg = "\"" + clientTier.Substring(0, clientTier.Length - 4) + "\\" + newNS + ".sln\" /p:Configuration=Release /t:Rebuild /v:minimal /nologo";
                    progress(95, "Rebuilding new RO installation ...");
                    Utils.ExecuteCommand("C:\\WINDOWS\\Microsoft.NET\\" + (isNet2 ? "Framework\\v3.5" : "Framework64\\v4.0.30319") + "\\msbuild.exe", cmd_arg, true);
                }
            }
        }
        public static void UpgradeApp(Dictionary<string, string> tiers, Dictionary<string, string> dataServer, string clientTargetDir, string dataTargetDir, Action<int, string> progress,
          Item item,			// Version info.
            ItemPDT iPDT,		// Existing Rbt:Rintagi/App:Production.
            ItemDEV iDEV,		// Existing Rbt:Developer/App:Extranet.
            ItemPTY iPTY,		// Existing Rbt:Application/App:Prototype.
            ItemNPDT nPDT,		// New Rbt:Rintagi/App:Production.
            ItemNDEV nDEV,	// New Rbt:Developer/App:Extranet.
            ItemNPTY nPTY		// New Rbt:Application/App:Prototype.            
            )
        {
            if (Application.StartupPath.EndsWith(":\\") || Application.StartupPath.EndsWith(":") || Application.StartupPath.StartsWith("\\\\"))
            {
                ReportError(@"Please do not run the installer from a root direcotory(like C:\) or across network(like \\server\share\install.exe)");
                return;
            }
            bool isSQLServer = dataServer["serverType"] == "M";
            string serverVer = dataServer["serverVer"];
            bool isNPDT = item.GetInsType() == "PDT" || item.GetInsType() == "NPDT";
            bool isNet2 = tiers["isNet2"] == "Y";
            bool hasRule = tiers.ContainsKey("rule") && !string.IsNullOrEmpty(tiers["rule"]);
            bool isDev = item.GetInsType() == "DEV" || item.GetInsType() == "NDEV";
            bool isPty = item.GetInsType() == "PTY" || item.GetInsType() == "NPTY";
            
            string dbServer = dataServer["server"];
            string dbPath = dataServer["dbpath"];
            string SysUserName = dataServer["user"];
            string SysPwd = dataServer["password"];
            string appUserName = dataServer["appUser"];
            string appPwd = dataServer["appPwd"];
            string dbServerType = dataServer["serverType"];
            string webServer = tiers["webServer"];
            string newNS = tiers["newNS"];
            string oldNS = item.GetOldNS();
            string clientTier = tiers["client"];
            string wsTier = tiers["ws"];
            string xlsTier = tiers["xls"];
            string ruleTier = tiers["rule"];
            string wsUrl = tiers["wsUrl"];
            bool isDataTier = !string.IsNullOrEmpty(dbPath); ;
            bool bIntegratedSecurity = dataServer["IntegratedSecurity"] == "Y";
            bool hasDeploy = item.GetHasDeploy();


            if (isDataTier && isSQLServer && dbServer.EndsWith(@"\") || dbServer.ToUpper().EndsWith("MSSQLSERVER"))
            {
                ReportError(@"Use '.' or just the server name WITHOUT trailing '\' if you want to specify the default MSSQLSERVER instance");
                return;
            }

            if (isDataTier && !TestConnDb("M", dbServer, SysUserName, SysPwd, newNS + "Design", bIntegratedSecurity))
            {
                ReportError(string.Format(@"Fail to open {0}, upgrade aborted", newNS + "Design"));
                return;
            }
            DataTable dtDeployEntity = !isDataTier ? null : GetDeployEntity("M", dbServer, SysUserName, SysPwd, newNS + "Design", bIntegratedSecurity);

            if (item.GetInsType().Contains("PDT")) // Rbt:Rintagi production or App:Production
            {
                if (isDataTier)
                {
                    if (isSQLServer)
                    {
                        iPDT.ApplyDesChg("M", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iPDT.ApplyAppChg("M", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iPDT.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iPDT.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (oldNS != "RO")
                        {
                            iPDT.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        }
                    }
                    else
                    {
                        iPDT.ApplyDesChg("S", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iPDT.ApplyAppChg("S", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iPDT.InstSysS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iPDT.InstDesS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (oldNS != "RO")
                        {
                            iPDT.InstAppS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        }
                    }
                }
                iPDT.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                SetupLicense3Dll(ruleTier, clientTier);
            }
            else if (oldNS == "RO" && isDev) // Rbt:App Developer
            {
                if (isDataTier)
                {
                    if (isSQLServer)
                    {
                        iDEV.ApplyDesChg("M", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iDEV.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iDEV.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                    }
                    else
                    {
                        iDEV.ApplyDesChg("S", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iDEV.InstSysS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iDEV.InstDesS(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                    }
                }

                iDEV.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                iDEV.InstRul(ruleTier, newNS, progress);
                InstallShadow(ruleTier, clientTier, item.GetShadowRootName());
                SetupLicense3Dll(ruleTier, clientTier);
                InstallLib(ruleTier, clientTier);
                /* recompile as the dll containing the encryption key may not be the same */
                //string cmd_arg = "\"" + clientTier.Substring(0, clientTier.Length - 4) + "\\" + newNS + ".sln\" /p:Configuration=Release /t:Rebuild /v:minimal /nologo";
                //Utils.ExecuteCommand("C:\\WINDOWS\\Microsoft.NET\\" + (isNet2 ? "Framework\\v3.5" : "Framework64\\v4.0.30319") + "\\msbuild.exe", cmd_arg, true);
                MakeNewDev(true,oldNS,newNS,clientTier,ruleTier, hasDeploy, dtDeployEntity);
            }
            else // App:Prototype
            {
                if (isDataTier)
                {
                    if (isSQLServer)
                    {
                        iPTY.ApplyDesChg("M", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iPTY.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iPTY.ApplyAppChg("M", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iPTY.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (oldNS != "RO" || isPty)
                        {
                            iPTY.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        }
                    }
                    else
                    {
                        iPTY.ApplyDesChg("S", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iPTY.InstSysM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        iPTY.ApplyAppChg("S", dbServer, SysUserName, SysPwd, newNS, clientTier, ruleTier, wsTier, progress, bIntegratedSecurity);
                        iPTY.InstDesM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        if (oldNS != "RO" || isPty)
                        {
                            iPTY.InstAppM(dbServer, SysUserName, SysPwd, newNS, progress, serverVer, dbPath, item.GetInsKey(), appUserName, appPwd, bIntegratedSecurity);
                        }
                    }
                }
                iPTY.InstCln(dbServer, clientTier, wsTier, xlsTier, wsUrl, newNS, progress);
                iPTY.InstRul(ruleTier, newNS, progress);
                InstallShadow(ruleTier, clientTier, item.GetShadowRootName());
                if (oldNS != "RO")
                {
                    InstallLib(ruleTier, clientTier);
                }
                SetupLicense3Dll(ruleTier, clientTier);

                /* recompile as the dll containing the encryption key may not be the same */
                if (oldNS == "RO" && newNS == "RO")
                {
                    // only do this for RO, non no longer need recompile as License3.dll is not supposed to be touched for upgrade
                    string cmd_arg = "\"" + clientTier.Substring(0, clientTier.Length - 4) + "\\" + newNS + ".sln\" /p:Configuration=Release /t:Rebuild /v:minimal /nologo";
                    progress(95, "Rebuilding " + newNS + " after PTY upgrade ...");
                    Utils.ExecuteCommand("C:\\WINDOWS\\Microsoft.NET\\" + (isNet2 ? "Framework\\v3.5" : "Framework64\\v4.0.30319") + "\\msbuild.exe", cmd_arg, true);
                }
                MakeNewDev(true, oldNS, newNS, clientTier, ruleTier, hasDeploy, dtDeployEntity);
            }
            // post upgrade db/client
        }
        private static void SetProjectRoot(string ruleTier, string WindowsServiceAccount, bool cli)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"\\Rule\\?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var projectRoot = regex.Replace(ruleTier, "");
            bool directoryExists = Directory.Exists(projectRoot);
            if (!directoryExists)
            {
                Directory.CreateDirectory(projectRoot);
            }
            Utils.SetDirectorySecurity(projectRoot, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow, !cli);

        }
        private static void SetFilePermission(string clientTier, string ruleTier, string wsTier, string xlsTier, string WindowsServiceAccount, string newNS)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"\\Rule\\?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var projectRoot = regex.Replace(ruleTier, "");
            if (!clientTier.ToLower().Contains(projectRoot.ToLower()))
                Utils.SetDirectorySecurity(clientTier, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            if (!clientTier.ToLower().Contains(projectRoot.ToLower())) Utils.SetDirectorySecurity(clientTier.Replace(@"\Web", @"\PrecompiledWeb\"), WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            if (!ruleTier.ToLower().Contains(projectRoot.ToLower())) Utils.SetDirectorySecurity(ruleTier, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            if (!wsTier.ToLower().Contains(projectRoot.ToLower())) Utils.SetDirectorySecurity(wsTier, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            if (!xlsTier.ToLower().Contains(projectRoot.ToLower())) Utils.SetDirectorySecurity(xlsTier, WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);

            if (!projectRoot.ToLower().Contains(projectRoot.ToLower())) Utils.SetDirectorySecurity(projectRoot + @"\Deploy" + newNS + "PDT", WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            if (!projectRoot.ToLower().Contains(projectRoot.ToLower())) Utils.SetDirectorySecurity(projectRoot + @"\Deploy" + newNS + "PTY", WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            if (newNS == "RO")
            {
                if (!projectRoot.ToLower().Contains(projectRoot.ToLower())) Utils.SetDirectorySecurity(projectRoot + @"\Deploy" + newNS + "DEV", WindowsServiceAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            }
        }

        private static void MakeNewApp(string oldNS, string newNS, string ruleTier)
        {
            if (oldNS == "RO")
            {
                string zip = @"Resources.dev.zip";
                Utils.ExtractBinRsc(zip, zip);
                Utils.JFileUnzip(zip, Application.StartupPath + @"\Temp");
                DirectoryInfo srcDi = new DirectoryInfo(Application.StartupPath + @"\Temp");
                Utils.ReplFileNS(Application.StartupPath + @"\Temp" + (Directory.Exists(Application.StartupPath + @"\Temp" + @"\dev") ? @"\dev" : ""), ruleTier, "ZZ", newNS, new List<string>(), new List<string>(), true, srcDi);
                srcDi.Delete(true); Utils.DeleteFile(zip);
            }
            else
            {
                // MUST deploy the existing sln in configuration
                string slnFileName = ruleTier + "/" + oldNS + ".sln";
                string newSlnFileName = ruleTier + "/" + newNS + ".sln";
                string sln = File.ReadAllText(ruleTier + "/" + oldNS + ".sln");
                sln = sln.Replace(oldNS, newNS);
                //sln = new Regex(@"(<sessionState cookieName="")([^""]+)""(.*)/>").Replace(sln, m => { return m.Groups[1] + "ASP.NET_" + newNS + "SessionId" + m.Groups[3]; });
                File.WriteAllText(newSlnFileName, sln);
                try
                {
                    File.Delete(slnFileName);
                }
                catch { }
            }

        }

        private static string DeployDir(string ruleTier, string ns, string deployType, bool upgrade)
        {
            string deployPath = ruleTier + @"\Deploy" + ns + deployType;
            deployPath = @"C:\Deploy" + ns + deployType;
            if (Directory.Exists(deployPath)) return deployPath;
            deployPath = @"D:\Deploy" + ns + deployType;
            if (Directory.Exists(deployPath)) return deployPath;
            deployPath = @"E:\Deploy" + ns + deployType;
            if (Directory.Exists(deployPath)) return deployPath;
            //if (upgrade) return null;
            var regex = new System.Text.RegularExpressions.Regex(@"\\Rule\\?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var projectRoot = regex.Replace(ruleTier, "");
            return projectRoot + @"\Deploy" + ns + deployType;
        }

        private static void MakeNewDev(bool upgrade,string oldNS, string newNS, string clientTier, string ruleTier, bool hasDeploy, DataTable dtDeployEntity)
        {
            if (hasDeploy)
            {

                string zip = @"Resources.dep.zip";
                Utils.ExtractBinRsc(zip, zip);
                Utils.JFileUnzip(zip, Application.StartupPath + @"\Temp");
                DirectoryInfo srcDi = new DirectoryInfo(Application.StartupPath + @"\Temp");
                if (dtDeployEntity != null)
                {
                    foreach (DataRow dr in dtDeployEntity.Rows)
                    {
                        string deployPath = dr["DeployPath"].ToString();
                        if (!upgrade)
                        {
                            deployPath = new System.Text.RegularExpressions.Regex(@"(^.+(\\|/))(Deploy.*$)").Replace(
                                deployPath, (m) => ruleTier + (ruleTier.EndsWith(@"\") || ruleTier.EndsWith(@"/") ? "" : @"\") + m.Groups[3].Value
                                );
                        }
                        Utils.ReplFileNS(Application.StartupPath + @"\Temp", deployPath, "ZZZ", "ZZZ", new List<string>(), new List<string>(), true, srcDi);
                        Utils.ExtractBinRsc(zip, deployPath + @"\Resources\dep.zip");
                    }
                }
                else
                {
                    /* be very careful about ReplFileNS as it do very brute force replacement which
                     * would screw up binary files */
                    string deployPath = DeployDir(ruleTier, newNS, "PDT", upgrade);
                    if (!string.IsNullOrEmpty(deployPath))
                    {
                        Utils.ReplFileNS(Application.StartupPath + @"\Temp", deployPath, "ZZZ", "ZZZ", new List<string>(), new List<string>(), true, srcDi);
                        Utils.ExtractBinRsc(zip, deployPath + @"\Resources\dep.zip");
                    }
                    deployPath = DeployDir(ruleTier, newNS, "PTY", upgrade);
                    if (!string.IsNullOrEmpty(deployPath))
                    {
                        Utils.ReplFileNS(Application.StartupPath + @"\Temp", deployPath, "ZZZ", "ZZZ", new List<string>(), new List<string>(), true, srcDi);
                        Utils.ExtractBinRsc(zip, deployPath + @"\Resources\dep.zip");
                    }
                    if (newNS == "RO")
                    {
                        deployPath = DeployDir(ruleTier, newNS, "DEV", upgrade);
                        if (!string.IsNullOrEmpty(deployPath))
                        {
                            Utils.ReplFileNS(Application.StartupPath + @"\Temp", deployPath, "ZZZ", "ZZZ", new List<string>(), new List<string>(), true, srcDi);
                            Utils.ExtractBinRsc(zip, deployPath + @"\Resources\dep.zip");
                        }
                    }
                }
                srcDi.Delete(true); Utils.DeleteFile(zip);

            }

            if (!upgrade)
            {
                if (oldNS == "RO")
                {
                    string zip = newNS == "RO" ? @"Resources.rosln.zip" : @"Resources.sln.zip";
                    string sln = newNS == "RO" ? @"\rosln" : @"\sln";
                    Utils.ExtractBinRsc(zip, zip);
                    Utils.JFileUnzip(zip, Application.StartupPath + @"\Temp");
                    DirectoryInfo srcDi = new DirectoryInfo(Application.StartupPath + @"\Temp");
                    Utils.ReplFileNS(Application.StartupPath + @"\Temp" + (Directory.Exists(Application.StartupPath + @"\Temp" + sln) ? sln : ""), clientTier.Substring(0, clientTier.Length - 4), "ZZ", newNS, new List<string>(), new List<string>(), true, srcDi);
                    try
                    {
                        //rename generic dev.sln to say RO.SLN
                        string c = clientTier.Substring(0, clientTier.Length - 4);
                        string oldpath = c + (newNS == "RO" ? @"\ro.sln" : @"\ZZ.sln");
                        string newpath = c + @"\" + newNS + ".sln";
                        System.IO.File.Move(oldpath, newpath);

                    }
                    catch { }
                    srcDi.Delete(true); Utils.DeleteFile(zip);
                }
                else
                {
                    // MUST deploy the existing sln in configuration, too many project GUID dependency in sln
                    string slnFileName = ruleTier + "/" + oldNS + ".sln";
                    string newSlnFileName = ruleTier + "/" + newNS + ".sln";
                    string sln = File.ReadAllText(ruleTier + "/" + oldNS + ".sln");
                    sln = sln.Replace(oldNS, newNS);
                    //sln = new Regex(@"(<sessionState cookieName="")([^""]+)""(.*)/>").Replace(sln, m => { return m.Groups[1] + "ASP.NET_" + newNS + "SessionId" + m.Groups[3]; });
                    File.WriteAllText(newSlnFileName, sln);
                    try
                    {
                        File.Delete(slnFileName);
                    }
                    catch { }

                }
            }

        }

    }
}