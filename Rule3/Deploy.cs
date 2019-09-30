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
        private const string AspExt2Replace = ",master,asmx,asax,aspx,ascx,cs,config,";
        private StringBuilder sbd;	// version control design meta data
        private StringBuilder sba;	// version control application meta data
        private StringBuilder si;	// InstallItem.cs for installation
        private StringBuilder MsgWarning = new StringBuilder("");
        private static string installerEncKey = Guid.NewGuid().ToString().Replace("-", "");

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

        public Deploy()
        {
            sbd = new StringBuilder("");
            sba = new StringBuilder("");
            si = new StringBuilder("");
        }
        public bool CanDeploy()
        {
            return CheckValidLicense();
        }
        public string PrepInstall(int releaseId, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
        {
            if (!CheckValidLicense())
            {
                throw new Exception("Please acquire proper Rintagi license for this feature");
            }
            string waDb = "WA" + releaseId.ToString();
            DataTable rInf;
            DataTable rDtl;
            DataView rVer;
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                rInf = dac.GetReleaseInf(releaseId);
            }
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                rDtl = dac.GetReleaseDtl(releaseId);
            }
            string ReleaseTypeAbbr = rInf.Rows[0]["ReleaseTypeAbbr"].ToString();
            string PrepDir = rInf.Rows[0]["PrepDir"].ToString();
            string PrepPath = rInf.Rows[0]["PrepPath"].ToString();
            string InstPath = rInf.Rows[0]["InstPath"].ToString();			// For Bakcup restore only.
            string PrepDeplPath = rInf.Rows[0]["PrepDeplPath"].ToString();
            if (!PrepDeplPath.EndsWith("\\")) PrepDeplPath = PrepDeplPath + "\\";
            string InstDeplPath = rInf.Rows[0]["InstDeplPath"].ToString();	// For Bakcup restore only.
            string ReleaseOs = rInf.Rows[0]["ReleaseOs"].ToString();
            string EntityCode = rInf.Rows[0]["EntityCode"].ToString();
            string ProjectRoot = new Regex(@"^.+\\" + EntityCode + @"\\").Match(PrepPath).Value;
            Int16 EntityId = Int16.Parse(rInf.Rows[0]["EntityId"].ToString());
            string shadowRoot = PrepDeplPath.Replace(new Regex(@"^.+\\" + "Deploy(" +  EntityCode + ")?", RegexOptions.IgnoreCase).Match(PrepDeplPath).Value,"");

            bool skipDeploy = false;
            // The following is necessary because somehow the create directory would fail from time to time after the deletion;
            if (Directory.Exists(PrepPath))
            {
                DirectoryInfo di = new DirectoryInfo(PrepPath);
                foreach (DirectoryInfo dii in di.GetDirectories())
                {
                    Directory.Delete(dii.FullName, true);
                }
                foreach (FileInfo fii in di.GetFiles())
                {
                    fii.Delete();
                }
            }
            else { Directory.CreateDirectory(PrepPath); }
            if (!Directory.Exists(PrepDeplPath + "Version")) { Directory.CreateDirectory(PrepDeplPath + "Version"); }
            si.Append("using System;" + Environment.NewLine);
            si.Append("using System.Collections.Generic;" + Environment.NewLine);
            si.Append("using System.Text;" + Environment.NewLine);
            si.Append("using System.Windows.Forms;" + Environment.NewLine);
            si.Append("using System.Data;" + Environment.NewLine);
            si.Append("using System.IO;" + Environment.NewLine);
            si.Append((char)13);
            si.Append("namespace Install" + Environment.NewLine);
            si.Append("{" + Environment.NewLine);
            si.Append("	public class Item" + ReleaseTypeAbbr + Environment.NewLine);
            si.Append("	{" + Environment.NewLine);
            si.Append("		private string sqf, txt, zip;" + Environment.NewLine);
            si.Append("		private string oldNS = \"" + EntityCode + "\";" + Environment.NewLine);
            si.Append("		private bool isNew = " + (ReleaseTypeAbbr.StartsWith("N") ? "true" : "false") + ";" + Environment.NewLine);

            List<string> modules = new List<string>();
            List<string> modulesDesign = new List<string>();
            foreach (DataRow dr in rDtl.Rows)
            {
                if (dr["ObjectType"].ToString() == "D")
                {
                    string[] ObjectName = dr["ObjectName"].ToString().Split('|');
                    bool isDesign = ObjectName.Any(v=>v.Trim().ToLower().EndsWith("cmond"));
                    foreach (var o in ObjectName)
                    {
                        if (isDesign)
                        {
                            if (!modulesDesign.Contains(o.Trim()) && !string.IsNullOrEmpty(o.Trim())) modulesDesign.Add(o.Trim());
                        }
                        else
                        {
                            if (!modules.Contains(o.Trim()) && !string.IsNullOrEmpty(o.Trim())) modules.Add(o.Trim());
                        }
                    }
                }
            }

            si.Append("		private List<List<string>> moduleDBs = new List<List<string>> { "
                + " new List<string>{" + string.Join(", ", modules.Select(s => "\"" + s + "\"").ToArray()) + "}"
                + ",new List<string>{" + string.Join(", ", modulesDesign.Select(s => "\"" + s + "\"").ToArray()) + "}"
                + " };" + Environment.NewLine);

            // ProgressBar has a maximum value of 100:
            int ProgressVal = 0;
            foreach (DataRow dr in rDtl.Rows)
            {
                if (dr["ObjectType"].ToString() == "D")
                {
                    string[] ObjectName = dr["ObjectName"].ToString().Split('|');
                    ProgressVal = ProgressVal + ObjectName.Length;
                }
                else { ProgressVal = ProgressVal + 1; }
            }
            ProgressVal = 100 / ProgressVal;
            // Prepare contents:
            foreach (DataRow dr in rDtl.Rows)
            {
                switch (dr["ObjectType"].ToString())
                {
                    case "D":
                        bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y";
                        if (singleSQLCredential)
                        {
                            dr["SrcDbServer"] = Config.DesServer;
                            dr["SrcDbUserId"] = Config.DesUserId;
                            dr["SrcDbPassword"] = Config.DesPassword;
                        }

                        Directory.CreateDirectory(PrepPath + "Server" + dr["ReleaseDtlId"].ToString() + "\\");
                        si.Append((char)13);
                        si.Append("		public void Inst");
                        if (dr["ObjectName"].ToString().IndexOf("Design") >= 0) { si.Append("Sys"); }
                        else if (dr["ObjectName"].ToString().IndexOf("CmonD") >= 0) { si.Append("Des"); } else { si.Append("App"); }
                        si.Append(dr["TarDbProviderCd"].ToString() + "(string Svr, string Usr, string Pwd, string newNS, Action<int,string> progress, string serverVer, string dbPath, string encKey, string AppUsr, string AppPwd, bool bIntegratedSecurity)" + Environment.NewLine);
                        si.Append("		{" + Environment.NewLine);
                        //si.Append("			DataView dv = null;" + Environment.NewLine);
                        if (ReleaseOs == "L")
                        {
                            SetupDbs(dr, ProgressVal, ReleaseOs, EntityId, EntityCode, ReleaseTypeAbbr, PrepDeplPath, PrepDir, PrepPath + "Server" + dr["ReleaseDtlId"].ToString() + "\\", InstPath + "Server" + dr["ReleaseDtlId"].ToString() + "/", InstDeplPath, waDb, dbConnectionString, dbPassword);
                        }
                        else
                        {
                            SetupDbs(dr, ProgressVal, ReleaseOs, EntityId, EntityCode, ReleaseTypeAbbr, PrepDeplPath, PrepDir, PrepPath + "Server" + dr["ReleaseDtlId"].ToString() + "\\", InstPath + "Server" + dr["ReleaseDtlId"].ToString() + "\\", InstDeplPath, waDb, dbConnectionString, dbPassword);
                        }
                        si.Append("		}" + Environment.NewLine);
                        break;
                    case "C":	//Ms Windows for now:
                        Directory.CreateDirectory(PrepPath + "Client" + dr["ReleaseDtlId"].ToString() + "\\");
                        if (dr["ObjectName"].ToString() == "n/a")
                        {
                            si.Append((char)13);
                            si.Append("		public void InstCln" + "(string Svr, string TarPath, string WsPath, string XlsPath, string WsUrl, string newNS, Action<int,string> progress)" + Environment.NewLine);
                            si.Append("		{" + Environment.NewLine);
                            si.Append("		}" + Environment.NewLine);
                        }
                        else
                        {
                            string fiConfig = PrepPath + "Client" + dr["ReleaseDtlId"].ToString() + "\\Web.config";
                            if (File.Exists(fiConfig))
                            {
                                XmlDocument xd = new XmlDocument();
                                xd.Load(fiConfig);
                                XmlNode xn;
                                xn = xd.DocumentElement;
                                foreach (XmlNode node in xn.ChildNodes)
                                {
                                    if (node.Name == "appSettings")
                                    {
                                        foreach (XmlNode setting in node.ChildNodes)
                                        {
                                            if (setting.Attributes != null && setting.Attributes[0].Value == "DesServer")
                                            {
                                                setting.Attributes[1].Value = string.Empty;
                                            }
                                            if (setting.Attributes != null && setting.Attributes[0].Value == "DesUserId")
                                            {
                                                setting.Attributes[1].Value = string.Empty;
                                            }
                                            if (setting.Attributes != null && setting.Attributes[0].Value == "DesPassword")
                                            {
                                                setting.Attributes[1].Value = string.Empty;
                                            }
                                        }
                                    }
                                }
                                xd.Save(fiConfig);
                            }
                            if ("DEV,NDEV,PTY,NPTY".IndexOf(ReleaseTypeAbbr) >= 0)
                            {
                                if (dr["ObjectName"].ToString().Trim() == "*.*")
                                {
                                    Utils.JFileZip(dr["SrcClnPath"].ToString() + "\\", PrepPath + "Client" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectExempt"].ToString() + " |PrecompiledWeb", AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                    Utils.JFileZip(dr["SrcWsPath"].ToString() + "\\", PrepPath + "Ws" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectExempt"].ToString() + " |PrecompiledWeb", AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                    Utils.JFileZip(dr["SrcXlsPath"].ToString() + "\\", PrepPath + "Xls" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectExempt"].ToString() + " |PrecompiledWeb", AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                }
                                else
                                {
                                    Utils.JFileZip(dr["SrcClnPath"].ToString() + "\\", PrepPath + "Client" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectName"].ToString(), dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                    Utils.JFileZip(dr["SrcWsPath"].ToString() + "\\", PrepPath + "Ws" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectName"].ToString(), dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                    Utils.JFileZip(dr["SrcXlsPath"].ToString() + "\\", PrepPath + "Xls" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectName"].ToString(), dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                }
                            }
                            else // Encrypted pre-compiled path:
                            {
                                PrecompileWeb(dr["SrcClnPath"].ToString(), dr["ComClnPath"].ToString(), dr["CombineAsm"].ToString(), Config.AppNameSpace);
                                if (dr["ObjectName"].ToString().Trim() == "*.*")
                                {
                                    Utils.JFileZip(dr["ComClnPath"].ToString() + "\\", PrepPath + "Client" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                    PrecompileWeb(dr["SrcWsPath"].ToString(), dr["ComWsPath"].ToString(), "N", Config.AppNameSpace + "Ws");
                                    Utils.JFileZip(dr["ComWsPath"].ToString() + "\\", PrepPath + "Ws" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                    PrecompileWeb(dr["SrcXlsPath"].ToString(), dr["ComXlsPath"].ToString(), "N", "WsXls");
                                    Utils.JFileZip(dr["ComXlsPath"].ToString() + "\\", PrepPath + "Xls" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                }
                                else
                                {
                                    Utils.JFileZip(dr["ComClnPath"].ToString() + "\\", PrepPath + "Client" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectName"].ToString(), dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                    PrecompileWeb(dr["SrcWsPath"].ToString(), dr["ComWsPath"].ToString(), "N", Config.AppNameSpace + "Ws");
                                    Utils.JFileZip(dr["ComWsPath"].ToString() + "\\", PrepPath + "Ws" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectName"].ToString(), dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                    PrecompileWeb(dr["SrcXlsPath"].ToString(), dr["ComXlsPath"].ToString(), "N", "WsXls");
                                    Utils.JFileZip(dr["ComXlsPath"].ToString() + "\\", PrepPath + "Xls" + dr["ReleaseDtlId"].ToString() + ".zip", true, dr["ObjectName"].ToString(), dr["ObjectExempt"].ToString(), AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                                }
                            }
                            si.Append((char)13);
                            si.Append("		public void InstCln" + "(string Svr, string TarPath, string WsPath, string XlsPath, string WsUrl, string newNS, Action<int,string> progress)" + Environment.NewLine);
                            si.Append("		{" + Environment.NewLine);
                            si.Append("			progress(0, \"Copying client tier files ...\");" + Environment.NewLine);
                            si.Append("			zip = \"_" + PrepDir + ".Client" + dr["ReleaseDtlId"].ToString() + ".zip\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractBinRsc(zip, zip);" + Environment.NewLine);
                            si.Append("			Utils.JFileUnzip(zip, Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                            si.Append("			DirectoryInfo srcDi = new DirectoryInfo(Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                            si.Append("			Utils.ReplFileNS(Application.StartupPath + @\"\\Temp\", TarPath, oldNS, newNS, moduleDBs[0], moduleDBs[1], isNew, srcDi);" + Environment.NewLine);
                            //si.Append("			Utils.DeployRdl(Svr, Application.StartupPath + @\"\\Temp\", TarPath, WsUrl, newNS);" + Environment.NewLine);
                            si.Append("			srcDi.Delete(true); Utils.DeleteFile(zip); progress(" + ProgressVal.ToString() + ",string.Empty);" + Environment.NewLine);
                            si.Append("			progress(0, \"Copying web service tier files ...\");" + Environment.NewLine);
                            si.Append("			zip = \"_" + PrepDir + ".Ws" + dr["ReleaseDtlId"].ToString() + ".zip\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractBinRsc(zip, zip);" + Environment.NewLine);
                            si.Append("			Utils.JFileUnzip(zip, Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                            si.Append("			srcDi = new DirectoryInfo(Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                            si.Append("			Utils.ReplFileNS(Application.StartupPath + @\"\\Temp\", WsPath, oldNS, newNS, moduleDBs[0], moduleDBs[1], isNew, srcDi);" + Environment.NewLine);
                            si.Append("			srcDi.Delete(true); Utils.DeleteFile(zip); progress(" + ProgressVal.ToString() + ",string.Empty);" + Environment.NewLine);
                            si.Append("			zip = \"_" + PrepDir + ".Xls" + dr["ReleaseDtlId"].ToString() + ".zip\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractBinRsc(zip, zip);" + Environment.NewLine);
                            si.Append("			Utils.JFileUnzip(zip, Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                            si.Append("			srcDi = new DirectoryInfo(Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                            si.Append("			Utils.ReplFileNS(Application.StartupPath + @\"\\Temp\", XlsPath, oldNS, newNS, moduleDBs[0], moduleDBs[1], isNew, srcDi);" + Environment.NewLine);
                            si.Append("			srcDi.Delete(true); Utils.DeleteFile(zip); progress(" + ProgressVal.ToString() + ",string.Empty);" + Environment.NewLine);
                            si.Append("		}" + Environment.NewLine);
                        }
                        break;
                    case "R":	//Ms Windows for now:
                        Directory.CreateDirectory(PrepPath + "Rule" + dr["ReleaseDtlId"].ToString() + "\\");
                        if (dr["ObjectName"].ToString() == "n/a")
                        {
                            si.Append("		public void InstRul" + "(string TarPath, string newNS, Action<int,string> progress)" + Environment.NewLine);
                            si.Append("		{" + Environment.NewLine);
                            si.Append("		}" + Environment.NewLine);
                        }
                        else
                        {
                            string objectExempt = dr["ObjectExempt"].ToString() 
                                + " |Deploy" + Config.AppNameSpace + "*\\*.*"
                                + " |" + Config.AppNameSpace + "Ws\\*.*"
                                + " |DeployBootstrap\\*.*"
                                + " |SQL\\*.*"
                                + " |*\\bin\\*.*"
                                + " |*\\obj\\*.*"
                                + " |*\\node_modules\\*.*"
                                + " |*\\npm-cache\\*.*"
                                + " |.npmrc"
                                + " |*.csproj.user"
                                + " |.vs"
                                + " |.git\\*.*"
                                + " |PrecompiledWeb\\*.*"
                                + " |Web\\*.*"
                                + " |WsXLs\\*.*"
                                + " |*\\npm\\*.*"
                                + " |package-lock.json";
                            if (objectExempt.Contains("Deploy.cs")) skipDeploy = true;

                            if (dr["ObjectName"].ToString().Trim() == "*.*" || true)
                            {
                                string objectIncluded = dr["ObjectName"].ToString().Trim();
                                Utils.JFileZip(dr["SrcRulePath"].ToString() + "\\", PrepPath + "Rule" + dr["ReleaseDtlId"].ToString() + ".zip", true, objectIncluded, objectExempt, AspExt2Replace, dr["SrcClientNS"].ToString(), dr["SrcClientNS"].ToString());
                            }
                            else
                            {
                                FileCopy(dr, dr["SrcRulePath"].ToString(), PrepPath + "Rule" + dr["ReleaseDtlId"].ToString() + "\\", dr["SrcRuleNS"].ToString(), dr["SrcRuleNS"].ToString(), new DirectoryInfo(dr["SrcRulePath"].ToString()), true);
//                                var reactPath = new Regex(@"\\[^\\]+\\?$").Replace(dr["SrcRulePath"].ToString(), @"\React"); 
//                                FileCopy(dr, reactPath, PrepPath + "Rule" + dr["ReleaseDtlId"].ToString() + "\\" + @"React\", dr["SrcRuleNS"].ToString(), dr["SrcRuleNS"].ToString(), new DirectoryInfo(reactPath), true);

                                DeleteExemptFiles(dr["SrcRulePath"].ToString(), PrepPath + "Rule" + dr["ReleaseDtlId"].ToString() + "\\", new DirectoryInfo(dr["SrcRulePath"].ToString()), objectExempt.ToString());
                                Utils.JFileZip(PrepPath + "Rule" + dr["ReleaseDtlId"].ToString() + "\\", PrepPath + "Rule" + dr["ReleaseDtlId"].ToString() + ".zip", true, true);
                            }
                            si.Append((char)13);
                            si.Append("		public void InstRul" + "(string TarPath, string newNS, Action<int,string> progress)" + Environment.NewLine);
                            si.Append("		{" + Environment.NewLine);
                            si.Append("			progress(0, \"Copying rule tier files ...\");" + Environment.NewLine);
                            si.Append("			zip = \"_" + PrepDir + ".Rule" + dr["ReleaseDtlId"].ToString() + ".zip\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractBinRsc(zip, zip);" + Environment.NewLine);
                            si.Append("			Utils.JFileUnzip(zip, Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                            //si.Append("			Utils.ExecuteCommand(Winzip,\" -min -e -o \" + zip + \" \" + Application.StartupPath + @\"\\Temp\", true);" + Environment.NewLine);
                            si.Append("			DirectoryInfo srcDi = new DirectoryInfo(Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                            si.Append("			Utils.ReplFileNS(Application.StartupPath + @\"\\Temp\", TarPath, oldNS, newNS, moduleDBs[0], moduleDBs[1], isNew, srcDi);" + Environment.NewLine);
                            si.Append("			srcDi.Delete(true); Utils.DeleteFile(zip); progress(" + ProgressVal.ToString() + ",string.Empty);" + Environment.NewLine);
                            si.Append("		}" + Environment.NewLine);
                        }
                        break;
                    default:
                        ApplicationAssert.CheckCondition(false, "Deploy.PrepInstall()", "ObjectType", "Invalid Object Type '" + dr["ObjectType"].ToString() + "'; Use 'C-Client,R-Rule,D-Data' only.");
                        break;
                }
            }
            // Version controls:
            si.Append((char)13);
            si.Append("		public void ApplyDesChg(string DbProvider, string Svr, string Usr, string Pwd, string newNS, string clientTierPath, string ruleTierPath, string WsTierPath, Action<int,string> progress, bool bIntegratedSecurity)" + Environment.NewLine);
            si.Append("		{" + (char)13 + sbd.ToString() + "		}" + Environment.NewLine);
            si.Append((char)13);
            si.Append("		public void ApplyAppChg(string DbProvider, string Svr, string Usr, string Pwd, string newNS, string clientTierPath, string ruleTierPath, string WsTierPath, Action<int,string> progress, bool bIntegratedSecurity)" + Environment.NewLine);
            si.Append("		{" + (char)13 + sba.ToString() + "		}" + Environment.NewLine);
            si.Append("	}" + Environment.NewLine);
            si.Append("}" + Environment.NewLine);
            Robot.WriteToFile("M", PrepDeplPath + "Item" + ReleaseTypeAbbr + ".cs", si.ToString());

            // Write to Item.cs.
            StringBuilder sm = new StringBuilder("");
            sm.Append("using System;" + Environment.NewLine);
            sm.Append("using System.Collections.Generic;" + Environment.NewLine);
            sm.Append("using System.Text;" + Environment.NewLine);
            sm.Append("using System.Windows.Forms;" + Environment.NewLine);
            sm.Append("using System.Data;" + Environment.NewLine);
            sm.Append("using System.IO;" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("namespace Install" + Environment.NewLine);
            sm.Append("{" + Environment.NewLine);
            sm.Append("	public class Item" + Environment.NewLine);
            sm.Append("	{" + Environment.NewLine);
            sm.Append("		private string oldNS = \"" + EntityCode + "\";" + Environment.NewLine);
            sm.Append("		private string iType = \"" + ReleaseTypeAbbr + "\";" + Environment.NewLine);
            sm.Append("		private string iKey = \"" + installerEncKey + "\";" + Environment.NewLine);
            sm.Append("		private string oldProjectRoot = @\"" + ProjectRoot + "\";" + Environment.NewLine);
            sm.Append("		private string shadowRoot = @\"" + shadowRoot + "\";" + Environment.NewLine);
            sm.Append("		private bool hasDeploy = " + (skipDeploy ? "false" : "true") + ";" + Environment.NewLine);
            sm.Append("		private string roENCKey= \"" + base.pCurrKey + "\";" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public string GetOldNS()" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            sm.Append("			return oldNS;" + Environment.NewLine);
            sm.Append("		}" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public string GetInsType()" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            sm.Append("			return iType;" + Environment.NewLine);
            sm.Append("		}" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public string GetInsKey()" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            sm.Append("			return iKey;" + Environment.NewLine);
            sm.Append("		}" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public string GetOldProjectRoot()" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            sm.Append("			return oldProjectRoot;" + Environment.NewLine);
            sm.Append("		}" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public string GetShadowRootName()" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            sm.Append("			return shadowRoot;" + Environment.NewLine);
            sm.Append("		}" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public bool GetHasDeploy()" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            sm.Append("			return hasDeploy;" + Environment.NewLine);
            sm.Append("		}" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public string GetROKey()" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            sm.Append("			return roENCKey;" + Environment.NewLine);
            sm.Append("		}" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public void SetROKey(string key)" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            sm.Append("			roENCKey = key;" + Environment.NewLine);
            sm.Append("		}" + Environment.NewLine);
            sm.Append((char)13);
            sm.Append("		public void SetVersion(ListView lvVersion)" + Environment.NewLine);
            sm.Append("		{" + Environment.NewLine);
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                rVer = new DataView(dac.GetReleaseVer(releaseId, EntityCode));
            }
            foreach (DataRowView drv in rVer)
            {
                sm.Append("			lvVersion.Items.Add(\"" + drv["VersionName"].ToString() + "\");" + Environment.NewLine);
            }
            sm.Append("		}" + Environment.NewLine);
            sm.Append("	}" + Environment.NewLine);
            sm.Append("}" + Environment.NewLine);
            Robot.WriteToFile("M", PrepDeplPath + "Item.cs", sm.ToString());

            // Write to ReleaseNote.txt.
            StringBuilder sd = new StringBuilder("Prerequisite and release information for all versions within 36 months:\r\n");
            DataView dv;
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                dv = new DataView(dac.GetYrReadme(releaseId, EntityCode));
            }
            foreach (DataRowView drv in dv)
            {
                sd.Append("\r\n");
                sd.Append("System: " + drv["SystemName"].ToString() + "\r\n");
                sd.Append("Version: " + drv["AppInfoDesc"].ToString() + "\r\n");
                sd.Append("Prerequisite: \r\n" + drv["Prerequisite"].ToString() + "\r\n");
                sd.Append("Readme: \r\n" + drv["Readme"].ToString() + "\r\n");
            }
            Robot.WriteToFile("M", PrepDeplPath + "ReleaseNote.txt", sd.ToString());

            // Setup Alternative License file
            try
            {
                using (StreamReader sr = new StreamReader(shadowRoot + @"\License.txt",true))
                {
                    string licenseContent = sr.ReadToEnd();
                    Robot.WriteToFile("M", PrepDeplPath + "License.txt", sd.ToString());
                    sr.Close();
                }
            }
            catch
            {
            }
            return MsgWarning.ToString();
        }

        private void SetupDbs(DataRow dr, int ProgressVal, string ReleaseOs, Int16 EntityId, string EntityCode, string ReleaseTypeAbbr, string PrepDeplPath, string PrepDir, string bkPath, string itPath, string iPath, string waDb, string dbConnectionString, string dbPassword)
        {
            bkPath = bkPath.Replace("/", "\\");
            string sBcpIn = string.Empty;
            string sBcpOut = string.Empty;
            string ss = string.Empty;
            DbPorting pt = new DbPorting();
            DbScript ds = null;
            string[] ObjectName = dr["ObjectName"].ToString().Split('|');
            string srcDb, tarDb;
            //string sqlCmd;
            //if (dr["TarDbProviderCd"].ToString() == "S") {sqlCmd = "isql";} else {sqlCmd = "osql";}
            foreach (string odb in ObjectName)
            {
                tarDb = odb.Trim();
                var logFile = PrepDeplPath + "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\..\\Install.log";
                try { File.Delete(logFile); }
                catch { }
                if (ReleaseTypeAbbr.StartsWith("N") && dr["SProcOnly"].ToString() != "A")
                {
                    si.Append("			Utils.CreateDatabase(\"" + dr["TarDbProviderCd"].ToString() + "\", Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", dbPath, serverVer,encKey,AppUsr,AppPwd, bIntegratedSecurity);" + Environment.NewLine);
                }
                //if (dr["TarScriptBef"].ToString().Trim() != string.Empty)
                //{
                //    ss = dr["TarScriptBef"].ToString();
                //    if (dr["TarDbProviderCd"].ToString() == "S") { ss = pt.SqlToSybase(EntityId, dr["TarDesDatabase"].ToString(), ss, dbConnectionString, dbPassword); }
                //    Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Bef.sql", ss);
                //    si.Append((char)13);
                //    si.Append("			progress(0, \"Upgrading \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " script before ...\");" + Environment.NewLine);
                //    si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Bef.sql\";" + Environment.NewLine);
                //    si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\");" + Environment.NewLine);
                //}
                ds = new DbScript("('" + dr["ObjectExempt"].ToString().Replace(" ", "").Replace("|", "','") + "')", ReleaseTypeAbbr.StartsWith("N"));
                if (!dr["SrcObject"].Equals(System.DBNull.Value) && !dr["SrcObject"].ToString().Trim().Equals(string.Empty))
                {
                    srcDb = dr["SrcObject"].ToString().Trim();
                }
                else
                {
                    srcDb = tarDb;
                }
                //if (dr["SProcOnly"].ToString() == "N") //Backup and Restore with Working Area.
                //{
                //    if (dr["SrcDbProviderCd"].ToString() == "S")
                //    {
                //        if (!DbExists(Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), waDb, "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString()))
                //        {
                //            DbCreate(Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), waDb);
                //        }
                //    }
                //    if (ReleaseOs == "L")
                //    {
                //        BackupDb(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), srcDb, iPath + srcDb + ".bak");
                //        RestoreWaDb(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), waDb, iPath + srcDb + ".bak", iPath);
                //    }
                //    else
                //    {
                //        BackupDb(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), srcDb, bkPath + srcDb + ".bak");
                //        RestoreWaDb(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), waDb, bkPath + srcDb + ".bak", bkPath);
                //        File.Delete(bkPath + srcDb + ".bak");
                //    }
                //    si.Append((char)13);
                //    si.Append("			progress(0, \"Upgrading \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " database ...\");" + Environment.NewLine);
                //    si.Append("			zip = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + ".bak\";" + Environment.NewLine);
                //    si.Append("			Utils.ExtractBinRsc(zip, \"" + tarDb + ".bak\");" + Environment.NewLine);
                //    //Truncate ObjectExempt
                //    if (!dr["ObjectExempt"].Equals(System.DBNull.Value) && !dr["ObjectExempt"].ToString().Trim().Equals(string.Empty))
                //    {
                //        sBcpOut = GetScript("TAROUT", "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", pt, ds, dr, srcDb, tarDb, ReleaseOs, EntityId, dbConnectionString, dbPassword);
                //        if (sBcpOut != string.Empty)
                //        {
                //            sBcpOut = "echo ... >> Install.log\r\necho ... Executing " + tarDb + "BcpO.bat: >> Install.log\r\n" + sBcpOut;
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "BcpO.bat", sBcpOut);
                //            si.Append((char)13);
                //            si.Append("			txt = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "BcpO.bat\";" + Environment.NewLine);
                //            si.Append("			Utils.ExtractTxtRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", txt, oldNS, newNS, bSql2005);" + Environment.NewLine);
                //            si.Append("			Directory.CreateDirectory(\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\");" + Environment.NewLine);
                //            si.Append("			Utils.ExecuteCommand(txt, \"\\\"\" + Svr + \"\\\" \\\"\" + Usr + \"\\\" \\\"\" + Pwd + \"\\\"\", true); Utils.DeleteFile(txt);" + Environment.NewLine);
                //            sBcpIn = GetScript("TARIN", "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", pt, ds, dr, srcDb, tarDb, ReleaseOs, EntityId, dbConnectionString, dbPassword);
                //            sBcpIn = "echo ... >> Install.log\r\necho ... Executing " + tarDb + "BcpI.bat: >> Install.log\r\n" + sBcpIn;
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "BcpI.bat", sBcpIn);
                //        }
                //        string[] ObjectExempt = dr["ObjectExempt"].ToString().Replace(" ", "").Split('|');
                //        foreach (string xTbl in ObjectExempt)
                //        {
                //            TruncateTable(Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), waDb, "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), xTbl);
                //        }
                //    }
                //    // Encrypt or change namespace:
                //    if (dr["DoSpEncrypt"].ToString() == "Y" || (!dr["SrcObject"].Equals(System.DBNull.Value) && !dr["SrcObject"].ToString().Trim().Equals(string.Empty)))
                //    {
                //        ss = GetScript("SP", string.Empty, pt, ds, dr, srcDb, tarDb, ReleaseOs, EntityId, dbConnectionString, dbPassword);
                //        if (ss != string.Empty)
                //        {
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Ecr.sql", ss);
                //            // ExecScript can handle unlimited file size:
                //            ds.ExecScript(dr["TarDbProviderCd"].ToString(), "Script Stored Procedures", "\"" + dr["TarPortBinPath"].ToString() + sqlCmd + "\"", bkPath + tarDb + "Ecr.sql", null
                //                , GetTar(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), dr["SrcDbServer"].ToString(), waDb, dr["SrcDbUserId"].ToString(), dr["SrcDbPassword"].ToString()), dbConnectionString, dbPassword);
                //            File.Delete(bkPath + tarDb + "Ecr.sql");
                //        }
                //    }
                //    //Backup again
                //    if (ReleaseOs == "L")
                //    {
                //        BackupDb(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), waDb, iPath + tarDb + ".bak");
                //        ss = GenRestoreScript(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), tarDb, iPath + tarDb + ".bak", tarDb + ".bak", dr["TarDbDataPath"].ToString());
                //        if (dr["SrcDbProviderCd"].ToString() == "S")
                //        {
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Rst.bat", "# !/bin/bash\n" + dr["TarInstBinPath"].ToString() + sqlCmd + " -S$1 -U$2 -P$3 -J iso_1 -i " + itPath + tarDb + "Rst.sql");
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Rst.sql", ss);
                //        }
                //        else
                //        {
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Rst.bat", "# !/bin/bash\n" + dr["TarInstBinPath"].ToString() + sqlCmd + " -S$1 -U$2 -P$3 -J iso_1 -Q" + ss);
                //        }
                //    }
                //    else
                //    {
                //        BackupDb(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), waDb, bkPath + tarDb + ".bak");
                //        ss = GenRestoreScript(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), tarDb, bkPath + tarDb + ".bak", tarDb + ".bak", dr["TarDbDataPath"].ToString());
                //        if (dr["SrcDbProviderCd"].ToString() == "S")
                //        {
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Rst.bat", "\"" + dr["TarInstBinPath"].ToString() + sqlCmd + ".exe\" -S%1 -U%2 -P%3 -i " + itPath + tarDb + "Rst.sql");
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Rst.sql", ss);
                //        }
                //        else
                //        {
                //            Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Rst.bat", "\"" + dr["TarInstBinPath"].ToString() + sqlCmd + ".exe\" -S%1 -U%2 -P%3 -Q" + ss + "\r\n");
                //        }
                //    }
                //    if (ss.Trim() != string.Empty)
                //    {
                //        si.Append((char)13);
                //        si.Append("			txt = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Rst.bat\";" + Environment.NewLine);
                //        si.Append("			Utils.ExtractTxtRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", txt, oldNS, newNS, bSql2005);" + Environment.NewLine);
                //        si.Append("			Utils.ExecuteCommand(txt, \"\\\"\" + Svr + \"\\\" \\\"\" + Usr + \"\\\" \\\"\" + Pwd + \"\\\"\", true); Utils.DeleteFile(txt);" + Environment.NewLine);
                //    }
                //    if (sBcpIn.Trim() != string.Empty)
                //    {
                //        si.Append((char)13);
                //        si.Append("			txt = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "BcpI.bat\";" + Environment.NewLine);
                //        si.Append("			Utils.ExtractTxtRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", txt, oldNS, newNS, bSql2005);" + Environment.NewLine);
                //        si.Append("			Utils.ExecuteCommand(txt, \"\\\"\" + Svr + \"\\\" \\\"\" + Usr + \"\\\" \\\"\" + Pwd+ \"\\\"\", true); Utils.DeleteFile(txt);" + Environment.NewLine);
                //        si.Append("			Directory.Delete(\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\",true);" + Environment.NewLine);
                //    }
                //    //Drop Working Area database
                //    DropDb(dr["SrcDbProviderCd"].ToString(), Config.GetConnStr(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), "master", "OLE DB Services=-4;", dr["SrcDbUserId"].ToString()), dr["SrcDbPassword"].ToString(), waDb);
                //    si.Append("			Utils.DeleteFile(\"" + tarDb + ".bak\");" + Environment.NewLine);
                //}
                //else	//Scripting:
                //{
                if (dr["SProcOnly"].ToString() == "P" || dr["SProcOnly"].ToString() == "X" )	//Porting:
                {
                    ss = GetScript("TABLE", string.Empty, pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                    if (!string.IsNullOrEmpty(ss) && dr["DoSpEncrypt"].ToString() == "N" && !string.IsNullOrEmpty(dr["SrcRulePath"].ToString()))
                    {
                        string srcPath = dr["SrcRulePath"].ToString();
                        if (!Directory.Exists(srcPath + "/SQL/")) Directory.CreateDirectory(srcPath + "/SQL/");
                        Robot.WriteToFile(ReleaseOs, srcPath + "/SQL/" + tarDb + "Table.sql", ss);
                    }

                    if (ss != string.Empty)
                    {
                        Robot.WriteToFile("M", bkPath + tarDb + "Table.sql", ss);
                        si.Append("			progress(0, \"Creating \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " tables ...\");" + Environment.NewLine);
                        si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Table.sql\";" + Environment.NewLine);
                        si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                    }

                    ss = GetScript("SRCOUT", PrepDeplPath + "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                    if (ss != string.Empty)
                    {
                        Robot.WriteToFile("M", bkPath + tarDb + "SrcO.bat", ss);
                        // Script Source tables:
                        Utils.WinProc("\"" + bkPath + tarDb + "SrcO.bat\"", " \"" + dr["SrcDbServer"].ToString() + "\" \"" + dr["SrcDbUserId"].ToString() + "\" \"" + DecryptString(dr["SrcDbPassword"].ToString()) + "\"", false);
                        // ExecScript can handle unlimited file size:
                        //							ds.ExecScript(string.Empty, "Script Source tables", bkPath + tarDb + "SrcO.bat", string.Empty, GetSrc(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), dr["SrcDbServer"].ToString(), srcDb, dr["SrcDbUserId"].ToString(), dr["SrcDbPassword"].ToString()), null, dbConnectionString, dbPassword);
                        try
                        {
                            using (var sr = new StreamReader(logFile))
                            {
                                var content = sr.ReadToEnd();
                                Regex errLine = new Regex("^Error =.+(Warning:.+)?$", RegexOptions.Multiline);
                                foreach (Match m in errLine.Matches(content))
                                {
                                    if (!m.Value.Contains("Warning"))
                                    {
                                        throw new ApplicationException(string.Format("{0} running SRCOUT batch file {1}, check log {2}", m.Value, bkPath + tarDb + "SrcO.bat", logFile));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex is ApplicationException)
                            {
                                throw;
                            }
                        }

                        Utils.JFileZip(PrepDeplPath + "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", bkPath + "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip", true, true);
                        File.Delete(bkPath + tarDb + "SrcO.bat");
                    }
                    ss = GetScript("SRCIN", "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                    if (ss != string.Empty)
                    {
                        ss = "echo ... >> Install.log\r\necho ... Executing " + tarDb + "SrcI.bat: >> Install.log\r\n" + ss;
                        Robot.WriteToFile("M", bkPath + tarDb + "SrcI.bat", ss);
                        si.Append((char)13);
                        si.Append("			progress(0, \"Upgrading \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " data ...\");" + Environment.NewLine);
                        si.Append("			zip = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + ".Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\";" + Environment.NewLine);
                        si.Append("			Utils.ExtractBinRsc(zip, \"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\");" + Environment.NewLine);
                        si.Append("			Utils.JFileUnzip(@\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\", @\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\");" + Environment.NewLine);
                        //si.Append("			Utils.ExecuteCommand(Winzip, @\" -min -e -o Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", true);" + Environment.NewLine);
                        si.Append("			txt = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "SrcI.bat\";" + Environment.NewLine);
                        si.Append("			Utils.ExtractTxtRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", txt, oldNS, newNS, moduleDBs[0],moduleDBs[1], isNew, serverVer, bIntegratedSecurity);" + Environment.NewLine);
                        si.Append("			Utils.ExecuteCommand(txt, \"\\\"\" + Svr + \"\\\" \\\"\" + Usr + \"\\\" \\\"\" + Pwd + \"\\\"\", true);" + Environment.NewLine);
                        si.Append("			try { Directory.Delete(\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\",true);" + Environment.NewLine);
                        si.Append("			Utils.DeleteFile(\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\"); } catch {}; Utils.DeleteFile(txt);" + Environment.NewLine);
                    }
                    ss = GetScript("INDEX", string.Empty, pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                    if (!string.IsNullOrEmpty(ss) && dr["DoSpEncrypt"].ToString() == "N" && !string.IsNullOrEmpty(dr["SrcRulePath"].ToString()))
                    {
                        string srcPath = dr["SrcRulePath"].ToString();
                        if (!Directory.Exists(srcPath + "/SQL/")) Directory.CreateDirectory(srcPath + "/SQL/");
                        Robot.WriteToFile(ReleaseOs, srcPath + "/SQL/" + tarDb + "Index.sql", ss);
                    }
                    if (ss != string.Empty)
                    {
                        Robot.WriteToFile("M", bkPath + tarDb + "Index.sql", ss);
                        si.Append((char)13);
                        si.Append("			progress(0, \"Creating \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " indexes ...\");" + Environment.NewLine);
                        si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Index.sql\";" + Environment.NewLine);
                        si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                    }

                    //done further below
                    //if (!ReleaseTypeAbbr.StartsWith("N"))
                    //{
                    //    // only output VIEW for upgrade due to running sequence 2016.8.8 gary
                    //    ss = GetScript("VIEW", string.Empty, pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                    //    if (ss != string.Empty)
                    //    {
                    //        Robot.WriteToFile("M", bkPath + tarDb + "View.sql", ss);
                    //        si.Append((char)13);
                    //        si.Append("			progress(0, \"Upgrading \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " views ...\");" + Environment.NewLine);
                    //        si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "View.sql\";" + Environment.NewLine);
                    //        si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                    //    }
                    //}
                }

                if (dr["SProcOnly"].ToString() == "Y")
                {
                    // SP only('Y') now is showing TABLE and View(instead of 'S Proc and View') giving people the impression that table structure would be created, the logic has to reflect that. 
                    // we have to output ALL TABLE and INDEX definition in the case of 'N'
                    if (ReleaseTypeAbbr.StartsWith("N"))
                    {
                        ss = GetScript("TABLE", string.Empty, pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                        if (!string.IsNullOrEmpty(ss) && dr["DoSpEncrypt"].ToString() == "N" && !string.IsNullOrEmpty(dr["SrcRulePath"].ToString()))
                        {
                            string srcPath = dr["SrcRulePath"].ToString();
                            if (!Directory.Exists(srcPath + "/SQL/")) Directory.CreateDirectory(srcPath + "/SQL/");
                            Robot.WriteToFile(ReleaseOs, srcPath + "/SQL/" + tarDb + "Table.sql", ss);
                        }

                        if (ss != string.Empty)
                        {
                            Robot.WriteToFile("M", bkPath + tarDb + "Table.sql", ss);
                            si.Append("			progress(0, \"Creating \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " tables ...\");" + Environment.NewLine);
                            si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Table.sql\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                        }

                        ss = GetScript("INDEX", string.Empty, pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                        if (!string.IsNullOrEmpty(ss) && dr["DoSpEncrypt"].ToString() == "N" && !string.IsNullOrEmpty(dr["SrcRulePath"].ToString()))
                        {
                            string srcPath = dr["SrcRulePath"].ToString();
                            if (!Directory.Exists(srcPath + "/SQL/")) Directory.CreateDirectory(srcPath + "/SQL/");
                            Robot.WriteToFile(ReleaseOs, srcPath + "/SQL/" + tarDb + "Index.sql", ss);
                        }
                        if (ss != string.Empty)
                        {
                            Robot.WriteToFile("M", bkPath + tarDb + "Index.sql", ss);
                            si.Append((char)13);
                            si.Append("			progress(0, \"Creating \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " indexes ...\");" + Environment.NewLine);
                            si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Index.sql\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["ObjectExempt"].ToString()))
                    {
                        // for SP only deployment use the exception list as a way to push 'system tables' in app(supporting code tables etc.)
                        // i.e. it means SP only EXCEPT these objects(tables) that would be pushed with contents
                        ss = GetScript("XMTOUT", PrepDeplPath + "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                        if (ss != string.Empty)
                        {
                            Robot.WriteToFile("M", bkPath + tarDb + "SrcO.bat", ss);
                            // Script Source tables:
                            Utils.WinProc("\"" + bkPath + tarDb + "SrcO.bat\"", " \"" + dr["SrcDbServer"].ToString() + "\" \"" + dr["SrcDbUserId"].ToString() + "\" \"" + DecryptString(dr["SrcDbPassword"].ToString()) + "\"", false);
                            // ExecScript can handle unlimited file size:
                            //							ds.ExecScript(string.Empty, "Script Source tables", bkPath + tarDb + "SrcO.bat", string.Empty, GetSrc(dr["SrcDbProviderOle"].ToString(), dr["SrcServerName"].ToString(), dr["SrcDbServer"].ToString(), srcDb, dr["SrcDbUserId"].ToString(), dr["SrcDbPassword"].ToString()), null, dbConnectionString, dbPassword);
                            Utils.JFileZip(PrepDeplPath + "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", bkPath + "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip", true, true);
                            File.Delete(bkPath + tarDb + "SrcO.bat");
                        }
                        ss = GetScript("XMTIN", "Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                        if (ss != string.Empty)
                        {
                            ss = "echo ... >> Install.log\r\necho ... Executing " + tarDb + "SrcI.bat: >> Install.log\r\n" + ss;
                            Robot.WriteToFile("M", bkPath + tarDb + "SrcI.bat", ss);
                            si.Append((char)13);
                            si.Append("			progress(0, \"Upgrading \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " data ...\");" + Environment.NewLine);
                            si.Append("			zip = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + ".Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractBinRsc(zip, \"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\");" + Environment.NewLine);
                            si.Append("			Utils.JFileUnzip(@\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\", @\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\");" + Environment.NewLine);
                            //si.Append("			Utils.ExecuteCommand(Winzip, @\" -min -e -o Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", true);" + Environment.NewLine);
                            si.Append("			txt = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "SrcI.bat\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractTxtRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", txt, oldNS, newNS, moduleDBs[0],moduleDBs[1], isNew, serverVer, bIntegratedSecurity);" + Environment.NewLine);
                            si.Append("			Utils.ExecuteCommand(txt, \"\\\"\" + Svr + \"\\\" \\\"\" + Usr + \"\\\" \\\"\" + Pwd + \"\\\"\", true);" + Environment.NewLine);
                            si.Append("			try { Directory.Delete(\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\",true);" + Environment.NewLine);
                            si.Append("			Utils.DeleteFile(\"Data" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\"); } catch {}; Utils.DeleteFile(txt);" + Environment.NewLine);
                        }
                    }
                }

                if ((dr["SProcOnly"].ToString() == "P" || dr["SProcOnly"].ToString() == "Y")  &&
                    (tarDb.EndsWith("CmonD") || !dr["ObjectName"].ToString().Contains("CmonD")))
                {
                    // view should be treated as SP as in a production release
                    // it would usually be SP only which unfortunately would skip the view
                    // 2012.6.11 gary

                    ss = GetScript("VIEW", string.Empty, pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                    if (!string.IsNullOrEmpty(ss) && dr["DoSpEncrypt"].ToString() == "N" && !string.IsNullOrEmpty(dr["SrcRulePath"].ToString()))
                    {
                        string srcPath = dr["SrcRulePath"].ToString();
                        if (!Directory.Exists(srcPath + "/SQL/")) Directory.CreateDirectory(srcPath + "/SQL/");
                        Robot.WriteToFile(ReleaseOs, srcPath + "/SQL/" + tarDb + "View.sql", ss);
                    }
                    if (!ReleaseTypeAbbr.StartsWith("N"))
                    {
                        if (ss != string.Empty)
                        {
                            // only output VIEW for upgrade due to running sequence 2016.8.8 gary
                            Robot.WriteToFile("M", bkPath + tarDb + "View.sql", ss);
                            if (dr["ObjectName"].ToString().IndexOf("CmonD") >= 0)	// Run the same View to all application design databases:
                            {
                                if (tarDb.EndsWith("CmonD"))
                                {
                                    si.Append("			progress(0, \"Upgrading all design database Views ...\");" + Environment.NewLine);
                                    si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "View.sql\";" + Environment.NewLine);
                                    si.Append("			DataView dvV = new DataView(Utils.GetAppDb(\"" + dr["TarDbProviderCd"].ToString() + "\", Svr, Usr, Pwd, newNS + \"Design\", bIntegratedSecurity));" + Environment.NewLine);
                                    si.Append("			foreach (DataRowView drv in dvV)" + Environment.NewLine);
                                    si.Append("			{" + Environment.NewLine);
                                    si.Append("				Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, drv[\"dbDesDatabase\"].ToString(), moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                                    si.Append("			}" + Environment.NewLine);
                                }
                            }
                            else
                            {
                                si.Append((char)13);
                                si.Append("			progress(0, \"Upgrading \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " views ...\");" + Environment.NewLine);
                                si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "View.sql\";" + Environment.NewLine);
                                si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                            }
                        }
                    }
                    ss = GetScript("SP", string.Empty, pt, ds, dr, srcDb, tarDb, ReleaseOs, EntityId, dbConnectionString, dbPassword);
                    if (!ReleaseTypeAbbr.StartsWith("N") || (dr["ObjectName"].ToString().Contains("ROCmon")))
                    {
                        // only output SP(including functions) for upgrade due to running sequence(cross db dependency of tables). installer will always run 'upgrade' as part of new installation when the tables are all created 2016.8.8 gary
                        Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Sp.sql", ss);
                    }
                    else
                    {
                        /* dummy file for new */
                        Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Sp.sql", (new Encryption()).EncryptString("", installerEncKey));
                    }
                    if (!string.IsNullOrEmpty(ss) && dr["DoSpEncrypt"].ToString() == "N" && !string.IsNullOrEmpty(dr["SrcRulePath"].ToString()))
                    {

                        string srcPath = dr["SrcRulePath"].ToString();
                        string uss = DecryptString(ss, installerEncKey);
                        if (!Directory.Exists(srcPath + "/SQL/")) Directory.CreateDirectory(srcPath + "/SQL/");
                        Robot.WriteToFile(ReleaseOs, srcPath + "/SQL/" + tarDb + "Sp.sql", uss);
                    }
                    if (ss != string.Empty)
                    {
                        if (dr["ObjectName"].ToString().IndexOf("CmonD") >= 0)	// Run the same S.Proc. to all application design databases:
                        {
                            if (tarDb.EndsWith("CmonD"))
                            {
                                si.Append("			progress(0, \"Upgrading all design database S.Procs ...\");" + Environment.NewLine);
                                si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Sp.sql\";" + Environment.NewLine);
                                si.Append("			DataView dv = new DataView(Utils.GetAppDb(\"" + dr["TarDbProviderCd"].ToString() + "\", Svr, Usr, Pwd, newNS + \"Design\", bIntegratedSecurity));" + Environment.NewLine);
                                si.Append("			foreach (DataRowView drv in dv)" + Environment.NewLine);
                                si.Append("			{" + Environment.NewLine);
                                si.Append("				Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, drv[\"dbDesDatabase\"].ToString(), moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                                si.Append("			}" + Environment.NewLine);
                            }
                        }
                        else
                        {
                            si.Append((char)13);
                            si.Append("			progress(0, \"Upgrading \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " Stored Procedures ...\");" + Environment.NewLine);
                            si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Sp.sql\";" + Environment.NewLine);
                            si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                        }
                    }
                }
                MsgWarning.Append(pt.sbWarning.ToString()); MsgWarning.Append(ds.sbWarning.ToString());
                //}
                if (dr["TarScriptAft"].ToString().Trim() != string.Empty)
                {
                    ss = dr["TarScriptAft"].ToString();
                    if (dr["TarDbProviderCd"].ToString() == "S") { ss = pt.SqlToSybase(EntityId, dr["TarDesDatabase"].ToString(), ss, dbConnectionString, dbPassword); }
                    Robot.WriteToFile(ReleaseOs, bkPath + tarDb + "Aft.sql", ss);
                    si.Append((char)13);
                    si.Append("			progress(0, \"Upgrading \" + newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " script after ...\");" + Environment.NewLine);
                    si.Append("			sqf = \"_" + PrepDir + ".Server" + dr["ReleaseDtlId"].ToString() + "." + tarDb + "Aft.sql\";" + Environment.NewLine);
                    si.Append("			Utils.ExtractSqlRsc(\"" + dr["TarDbProviderCd"].ToString() + "\", sqf, oldNS, newNS, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", moduleDBs[0], moduleDBs[1], isNew, encKey, bIntegratedSecurity);" + Environment.NewLine);
                }
                // BCP Out version table changes history:
                //if (dr["TarDbProviderCd"].ToString() == "M" && ",DEV,PDT,PTY,".IndexOf(ReleaseTypeAbbr) >= 0 && (dr["SProcOnly"].ToString() == "N" || dr["SProcOnly"].ToString() == "P" || dr["SProcOnly"].ToString() == "X")
                //    && (dr["ObjectName"].ToString().IndexOf("Design") >= 0 || dr["ObjectName"].ToString().IndexOf("CmonD") >= 0))
                if (dr["TarDbProviderCd"].ToString() == "M" && (dr["ObjectName"].ToString().IndexOf("Design") >= 0 || dr["ObjectName"].ToString().IndexOf("CmonD") >= 0))
                {
                    if (File.Exists(PrepDeplPath + "Version\\" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip"))
                    {
                        File.Delete(PrepDeplPath + "Version\\" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip");
                    }
                    /* output client and rule tier app item as well */
                    ds = new DbScript("('VwAppItem','VwClnAppItem','VwRulAppItem')", false);
                    ss = GetScript("XMTOUT", PrepDeplPath + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", pt, ds, dr, srcDb, tarDb, "M", EntityId, dbConnectionString, dbPassword);
                    if (ss != string.Empty)
                    {
                        Robot.WriteToFile("M", bkPath + tarDb + "XmtO.bat", ss);
                        // Script Version History:
                        Utils.WinProc("\"" + bkPath + tarDb + "XmtO.bat\"", " \"" + dr["SrcDbServer"].ToString() + "\" \"" + dr["SrcDbUserId"].ToString() + "\" \"" + DecryptString(dr["SrcDbPassword"].ToString()) + "\"", false);
                        Utils.JFileZip(PrepDeplPath + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\\", PrepDeplPath + "Version\\" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip", true, true);
                        File.Delete(bkPath + tarDb + "XmtO.bat");
                    }
                    if (dr["ObjectName"].ToString().IndexOf("Design") >= 0)
                    {
                        sbd.Append((char)13);
                        sbd.Append("			progress(0, \"Applying " + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + " changes ...\");" + Environment.NewLine);
                        sbd.Append("			zip = \"Version." + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\";" + Environment.NewLine);
                        sbd.Append("			Utils.ExtractBinRsc(zip, zip);" + Environment.NewLine);
                        sbd.Append("			Utils.JFileUnzip(zip, Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                        //sbd.Append("			Utils.ExecuteCommand(Winzip,\" -min -e -o \" + zip + \" \" + Application.StartupPath + @\"\\Temp\", true);" + Environment.NewLine);
                        sbd.Append("			Utils.UpgradeServer(DbProvider, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", Application.StartupPath + @\"\\Temp\", newNS, bIntegratedSecurity);" + Environment.NewLine);
                        if (!ReleaseTypeAbbr.StartsWith("N")) sbd.Append("			Utils.UpgradeServerClientTier(DbProvider, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", Application.StartupPath + @\"\\Temp\",clientTierPath, newNS, bIntegratedSecurity);" + Environment.NewLine);
                        if (!ReleaseTypeAbbr.StartsWith("N")) sbd.Append("			Utils.UpgradeServerRuleTier(DbProvider, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + "\", Application.StartupPath + @\"\\Temp\",ruleTierPath, newNS, bIntegratedSecurity);" + Environment.NewLine);
                        sbd.Append("			(new DirectoryInfo(Application.StartupPath + @\"\\Temp\")).Delete(true); Utils.DeleteFile(zip);" + Environment.NewLine);
                    }
                    else
                    {
                        sba.Append((char)13);
                        // UpgradeServer apply AppItem to eg.Cmon extracted from eg.CmonD:
                        sba.Append("			progress(0, \"Applying " + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length - 1) + " changes ...\");" + Environment.NewLine);
                        sba.Append("			zip = \"Version." + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length) + ".zip\";" + Environment.NewLine);
                        sba.Append("			Utils.ExtractBinRsc(zip, zip);" + Environment.NewLine);
                        sba.Append("			Utils.JFileUnzip(zip, Application.StartupPath + @\"\\Temp\");" + Environment.NewLine);
                        //sba.Append("			Utils.ExecuteCommand(Winzip,\" -min -e -o \" + zip + \" \" + Application.StartupPath + @\"\\Temp\", true);" + Environment.NewLine);
                        sba.Append("			Utils.UpgradeServer(DbProvider, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length - 1) + "\", Application.StartupPath + @\"\\Temp\", newNS, bIntegratedSecurity);" + Environment.NewLine);
                        if (!ReleaseTypeAbbr.StartsWith("N")) sba.Append("			Utils.UpgradeServerClientTier(DbProvider, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length - 1) + "\", Application.StartupPath + @\"\\Temp\",clientTierPath, newNS, bIntegratedSecurity);" + Environment.NewLine);
                        if (!ReleaseTypeAbbr.StartsWith("N")) sba.Append("			Utils.UpgradeServerRuleTier(DbProvider, Svr, Usr, Pwd, newNS + \"" + tarDb.Substring(EntityCode.Length, tarDb.Length - EntityCode.Length - 1) + "\", Application.StartupPath + @\"\\Temp\",ruleTierPath, newNS, bIntegratedSecurity);" + Environment.NewLine);
                        sba.Append("			(new DirectoryInfo(Application.StartupPath + @\"\\Temp\")).Delete(true); Utils.DeleteFile(zip);" + Environment.NewLine);
                    }
                }
                si.Append("			progress(" + ProgressVal.ToString() + ",string.Empty);" + Environment.NewLine);
            }
        }

        private void FileCopy(DataRow dr, string srcPath, string tarPath, string oldNS, string newNS, DirectoryInfo srcDi, bool bFirstCall)
        {
            DirectoryInfo tarDi = new DirectoryInfo(srcDi.FullName.Replace(srcPath, tarPath));
            //if (!tarDi.Exists) { tarDi.Create(); }
            string[] ObjectName = dr["ObjectName"].ToString().Split('|');
            string objectExempt = dr["ObjectExempt"].ToString()
                                + " |Deploy" + Config.AppNameSpace + "*"
                                + " |" + Config.AppNameSpace + "Ws"
                                + " |DeployBootstrap"
                                + " |SQL"
                                + " |bin"
                                + " |obj"
                                + " |node_modules"
                                + " |npm-cache"
                                + " |.npmrc"
                                + " |.vs"
                                + " |.git"
                                + " |PrecompiledWeb"
                                + " |Web"
                                + " |WsXLs"
                                + " |npm"
                                + " |package-lock.json";
            string[] ObjectExempts = objectExempt.Split('|');
            string currDir = srcDi.FullName;
            List<System.Text.RegularExpressions.Regex> exemptRules =
                (from o in objectExempt.Split('|').ToList<string>()
                 where !string.IsNullOrEmpty(o.Trim())
                 select new System.Text.RegularExpressions.Regex("^" + currDir.Replace("\\", "\\\\").Replace(".", "\\.") + (o.Contains(".") && !o.Contains("\\") ? ".*" : "") + (o.StartsWith("\\") ? @"(\\)?" : @"\\") + o.Trim().ToLower().Replace("\\", "\\\\").Replace("*.*", "*").Replace(".", "\\.").Replace("*", o.Contains("\\*.*") ? ".*" : "[^\\\\]*") + "$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)).ToList();
            Func<string, string, bool> fIsExempted = (f, p) => { foreach (var re in exemptRules) { if (re.IsMatch(p)) return true; } return false; };
            Func<string, string, bool> fIsIncluded = (f, p) => true;

            string[] reactLocal = new string[] { "node_modules", "npm-cache",".npmrc","npm", "package-lock.json"};
            foreach (string oo in ObjectName)
            {
                if (bFirstCall || oo.IndexOf("\\") < 0)
                {
                    try
                    {
                        FileInfo tarFi;
                        FileInfo[] fis = srcDi.GetFiles(oo.IndexOf("\\") < 0 ? (srcDi.FullName.ToLower().Contains(oo.Trim().ToLower()) ? "*.*" : "")  : oo.Trim());
                        foreach (FileInfo srcFi in fis)
                        {
                            tarFi = new FileInfo(srcFi.FullName.Replace(srcPath, tarPath));
                            if (!Directory.Exists(tarFi.DirectoryName) && !reactLocal.Contains(srcFi.Name.ToLower().Trim())) { Directory.CreateDirectory(tarFi.DirectoryName); }
                            if (AspExt2Replace.IndexOf("," + srcFi.Extension.Substring(1, srcFi.Extension.Length - 1) + ",") < 0)
                            {
                                if (!tarFi.Exists && !reactLocal.Contains(srcFi.Name.ToLower().Trim())) { srcFi.CopyTo(tarFi.FullName); }
                            }
                            else if (!reactLocal.Contains(srcFi.Name.ToLower().Trim()))
                            {
                                ReplaceAndCopy(oldNS, newNS, srcFi, tarFi);
                            }
                        }
                    }
                    catch { }	// in case ROWs does not have the directory specified in RO.
                }
            }
            foreach (DirectoryInfo di in srcDi.GetDirectories())
            {
                if (di.Name.ToLower().Trim() != ".git"
                    && !reactLocal.Contains(di.Name.ToLower().Trim())
                    )
                {
                    FileCopy(dr, srcPath, tarPath, oldNS, newNS, di, false);
                }
            }
        }

        // System.Diagnostics.Process.Start does not work in Framwork 3.5:
        //private void FileZip(string zipFr, string zipTo)
        //{
        //    string cmd = "\"C:\\Program Files\\WinZip\\winzip32.exe\"";
        //    string arg = " -min -a -r \"" + zipTo + "\" \"" + zipFr + "\"";
        //    System.Diagnostics.Process proc;
        //    if (!(new FileInfo(cmd.Replace("\"", ""))).Exists)
        //    {
        //        ApplicationAssert.CheckCondition(false, "Deploy.FileZip()", "Command Program", "Winzip program '" + cmd + "' does not exist! Please rectify and try again."); return;
        //    }
        //    if ((new DirectoryInfo(zipFr)).Exists)
        //    {
        //        proc = System.Diagnostics.Process.Start(cmd,arg);
        //        proc.WaitForExit();
        //        Directory.Delete(zipFr, true);
        //    }
        //    else
        //    {
        //        ApplicationAssert.CheckCondition(false, "Deploy.FileZip()", "Source Files", "Source '" + zipFr + "' does not exist! Please rectify and try again."); return;
        //    }
        //}

        private void DeleteExemptFiles(string srcPath, string tarPath, DirectoryInfo srcDi, string objectExempt)
        {
            try
            {
                DirectoryInfo tarDi = new DirectoryInfo(srcDi.FullName.Replace(srcPath, tarPath));
                string[] ObjectExempt = objectExempt.Split('|');
                foreach (string oe in ObjectExempt)
                {
                    if (oe.IndexOf("\\") < 0)
                    {
                        DirectoryInfo[] dis = tarDi.GetDirectories(oe.Trim());
                        foreach (DirectoryInfo di in dis) { if (di.Exists) { di.Delete(true); } }
                        FileInfo[] fis = tarDi.GetFiles(oe.Trim());
                        foreach (FileInfo fi in fis) { if (fi.Exists) { fi.Delete(); } }
                    }
                }
                foreach (DirectoryInfo di in srcDi.GetDirectories())
                {
                    DeleteExemptFiles(srcPath, tarPath, di, objectExempt);
                }
            }
            catch { }
        }

        private void ReplaceAndCopy(string oldNS, string newNS, FileInfo srcFi, FileInfo tarFi)
        {
            StreamWriter sw = null;
            StreamReader sr = null;
            if (oldNS != String.Empty && newNS != String.Empty)
            {
                try
                {
                    sr = srcFi.OpenText();
                    string content = sr.ReadToEnd().Replace(oldNS + ".", newNS + ".").Replace("namespace " + oldNS, "namespace " + newNS).Replace(oldNS + "Design", newNS + "Design").Replace(oldNS + "Cmon", newNS + "Cmon");
                    sr.Close();
                    sw = new StreamWriter(tarFi.FullName);
                    sw.Write(content);
                    sw.Close();
                }
                catch (Exception ex)
                {
                    ApplicationAssert.CheckCondition(false, "Deploy.ReplaceAndCopy", "", ex.Message);
                }
                finally
                {
                    if (sr != null) sr.Close();
                    if (sw != null) sw.Close();
                }
            }
        }

        private void BackupDb(string dbProviderCd, string connStr, string pwd, string dbName, string bkFile)
        {
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                dac.BackupDb(dbProviderCd, connStr, pwd, dbName, bkFile);
            }
        }

        private void RestoreWaDb(string dbProviderCd, string connStr, string pwd, string waDb, string waFile, string waPath)
        {
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                dac.RestoreWaDb(dbProviderCd, connStr, pwd, waDb, waFile, waPath);
            }
            if (dbProviderCd == "S")
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(60));	//Temporary to avoid database still in use by previous call.
                using (Access3.DeployAccess dac = new Access3.DeployAccess())
                {
                    dac.OnlineDb(connStr, pwd, waDb);
                }
            }
        }

        private string GenRestoreScript(string dbProviderCd, string connStr, string pwd, string dbName, string iFileAbs, string rsFileRel, string dbPath)
        {
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                return dac.GenRestoreScript(dbProviderCd, connStr, pwd, dbName, iFileAbs, rsFileRel, dbPath);
            }
        }

        private void DropDb(string dbProviderCd, string connStr, string pwd, string dbName)
        {
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                dac.DropDb(dbProviderCd, connStr, pwd, dbName);
            }
        }

        private void TruncateTable(string connStr, string pwd, string tblName)
        {
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                dac.TruncateTable(connStr, pwd, tblName);
            }
        }

        private void ExecSql(string InSql, string connStr, string pwd)
        {
            using (Access3.RobotAccess dac = new Access3.RobotAccess())
            {
                dac.ExecSql(InSql, connStr, pwd);
            }
        }

        private bool DbExists(string connStr, string pwd)
        {
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                return dac.DbExists(connStr, pwd);
            }
        }

        private void DbCreate(string connStr, string pwd, string dbName)
        {
            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                dac.DbCreate(connStr, pwd, dbName);
            }
        }

        private string GetDbOption(string dbProviderCd)
        {
            if (dbProviderCd == "M") { return "-d"; } else { return "-D"; }
        }

        private string GetScript(string ScriptType, string bcpPath, DbPorting pt, DbScript ds, DataRow dr, string srcDb, string tarDb, string ReleaseOs, Int16 EntityId, string dbConnectionString, string dbPassword)
        {
            string ss = string.Empty;
            bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y";
            CurrSrc CSrc = GetSrc(dr["SrcDbProviderOle"].ToString(), singleSQLCredential ? Config.DesServer : dr["SrcServerName"].ToString(), dr["SrcDbServer"].ToString(), srcDb, singleSQLCredential ? Config.DesUserId : dr["SrcDbUserId"].ToString(), singleSQLCredential ? Config.DesPassword : dr["SrcDbPassword"].ToString());
            CurrTar CTar = GetTar(dr["SrcDbProviderOle"].ToString(), singleSQLCredential ? Config.DesServer : dr["SrcServerName"].ToString(), dr["SrcDbServer"].ToString(), tarDb, singleSQLCredential ? Config.DesUserId : dr["SrcDbUserId"].ToString(), singleSQLCredential ? Config.DesPassword : dr["SrcDbPassword"].ToString());

            if (!File.Exists(dr["SrcPortBinPath"].ToString() + @"\bcp.exe") && ",XMTOUT,SRCOUT,".IndexOf(ScriptType) >= 0)
            {
                var foundBCPPath = GetSQLBcpPath();
                throw new Exception(string.Format("Check DataTier definition for Source Port Bin Path({0}), file not exists - one found {1}", dr["SrcPortBinPath"].ToString(), foundBCPPath.Value ?? "None"));
            }
            if (!File.Exists(dr["TarPortBinPath"].ToString() + @"\bcp.exe") && ",XMTIN,SRCIN,".IndexOf(ScriptType) >= 0)
            {
                var foundBCPPath = GetSQLBcpPath();
                throw new Exception(string.Format("Check DataTier definition for Target Port Bin Path({0}), file not exists - one found {1}", dr["TarPortBinPath"].ToString(), foundBCPPath.Value ?? "None"));
            }

            if (ScriptType == "TAROUT")	// Target BCP OUT
            {
                // Use PortBinPath instead of InstBinPath because of Deploy wizards:
                ss = ds.GenerateBCPFiles(ReleaseOs, dr["TarDbProviderCd"].ToString(), dr["TarDbProviderCd"].ToString(), dr["TarPortBinPath"].ToString(), true, bcpPath, "~@~", true, CSrc, CTar);
                if (!dr["SrcObject"].Equals(System.DBNull.Value) && !dr["SrcObject"].ToString().Trim().Equals(string.Empty))
                {
                    ss = ss.Replace(srcDb + ".dbo.", tarDb + ".dbo.");
                }
            }
            else if (ScriptType == "TARIN")	// Target BCP IN
            {
                // Use PortBinPath instead of InstBinPath because of Deploy wizards:
                ss = ds.GenerateBCPFiles(ReleaseOs, dr["TarDbProviderCd"].ToString(), dr["TarDbProviderCd"].ToString(), dr["TarPortBinPath"].ToString(), false, bcpPath, "~@~", true, CSrc, CTar);
            }
            else if (ScriptType == "TABLE")	// Tables
            {
                ss = ds.ScriptCreateTables(dr["SrcDbProviderCd"].ToString(), dr["TarDbProviderCd"].ToString(), true, CSrc, CTar);
                if (dr["SrcDbProviderCd"].ToString() == "M" && dr["TarDbProviderCd"].ToString() == "S") { ss = pt.SqlToSybase(EntityId, dr["TarDesDatabase"].ToString(), ss, dbConnectionString, dbPassword); }
            }
            else if (ScriptType == "XMTOUT")	// Exempt BCP Out
            {
                Directory.CreateDirectory(bcpPath);
                // Use PortBinPath instead of InstBinPath because of Deploy wizards:
                ss = ds.GenerateBCPFiles(ReleaseOs, dr["TarDbProviderCd"].ToString(), dr["SrcDbProviderCd"].ToString(), dr["SrcPortBinPath"].ToString(), true, bcpPath, "~@~", true, CSrc, CTar);
            }
            else if (ScriptType == "XMTIN")	// Exempt BCP In
            {
                // Use PortBinPath instead of InstBinPath because of Deploy wizards:
                ss = ds.GenerateBCPFiles(ReleaseOs, dr["TarDbProviderCd"].ToString(), dr["SrcDbProviderCd"].ToString(), dr["SrcPortBinPath"].ToString(), false, bcpPath, "~@~", true, CSrc, CTar);
            }
            else if (ScriptType == "SRCOUT")	// Source BCP Out
            {
                Directory.CreateDirectory(bcpPath);
                // Use PortBinPath instead of InstBinPath because of Deploy wizards:
                ss = ds.GenerateBCPFiles(ReleaseOs, dr["TarDbProviderCd"].ToString(), dr["SrcDbProviderCd"].ToString(), dr["SrcPortBinPath"].ToString(), true, bcpPath, "~@~", false, CSrc, CTar);
            }
            else if (ScriptType == "SRCIN")	// Source BCP In
            {
                // Use PortBinPath instead of InstBinPath because of Deploy wizards:
                ss = ds.GenerateBCPFiles(ReleaseOs, dr["TarDbProviderCd"].ToString(), dr["TarDbProviderCd"].ToString(), dr["TarPortBinPath"].ToString(), false, bcpPath, "~@~", false, CSrc, CTar);
            }
            else if (ScriptType == "INDEX")	// Indexes
            {
                ss = ds.ScriptIndexFK(dr["SrcDbProviderCd"].ToString(), dr["TarDbProviderCd"].ToString(), true, CSrc, CTar);
                if (dr["SrcDbProviderCd"].ToString() == "M" && dr["TarDbProviderCd"].ToString() == "S") { ss = pt.SqlToSybase(EntityId, dr["TarDesDatabase"].ToString(), ss, dbConnectionString, dbPassword); }
            }
            else if (ScriptType == "VIEW")	// Views
            {
                ss = ds.ScriptView(dr["SrcDbProviderCd"].ToString(), dr["TarDbProviderCd"].ToString(), true, CSrc, CTar);
                if (dr["SrcDbProviderCd"].ToString() == "M" && dr["TarDbProviderCd"].ToString() == "S") { ss = pt.SqlToSybase(EntityId, dr["TarDesDatabase"].ToString(), ss, dbConnectionString, dbPassword); }
            }
            else	// Stored Procedures.
            {
                ss = ds.ScriptSProcedures(dr["SrcDbProviderCd"].ToString(), dr["TarDbProviderCd"].ToString(), true, CSrc, CTar);
                if (dr["SrcDbProviderCd"].ToString() == "M" && dr["TarDbProviderCd"].ToString() == "S") { ss = pt.SqlToSybase(EntityId, dr["TarDesDatabase"].ToString(), ss, dbConnectionString, dbPassword); }
                if (dr["DoSpEncrypt"].ToString() == "Y") { ss = ds.EncryptSProcedures(dr["SrcDbProviderCd"].ToString(), dr["TarDbProviderCd"].ToString(), ss, true, CSrc, CTar); }
                ss = (new Encryption()).EncryptString(ss, installerEncKey);	// This key must be used in the installer to decrypt.
            }
            return ss.Trim();
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

        private void PrecompileWeb(string srcDir, string targetDir, string CombineAsm, string ns)
        {
            string[] v = System.Environment.Version.ToString().Split(new char[] { '.' });
            string cmd_path = @"C:\Windows\Microsoft.Net\Framework\v" + string.Join(".", v, 0, 3) + @"\aspnet_compiler.exe";
            string cmd_arg = string.Format("-v /{0} -p {1} -f {2} -c -fixednames" + (CombineAsm == "Y" ? "" : " -u"), ns, srcDir, targetDir);
            string ss = Utils.WinProc(cmd_path, cmd_arg, true);
            if (ss.IndexOf("error") >= 0) { throw new Exception(ss); }

            //The following will merge all dlls into one Web.dll.
            if (CombineAsm == "Y")
            {
                cmd_path = Config.WsdlExe.ToLower().Replace("wsdl.exe", "aspnet_merge.exe");
                //cmd_arg = string.Format("-o {0} {1}", "Web", targetDir);
                //cmd_arg = string.Format("-w {0} {1}", "Web", targetDir);
                /* .NET 4 is too large for say SC to combine all into one single DLL, cannot use -w */
                cmd_arg = string.Format("{0}", targetDir);
                ss = Utils.WinProc(cmd_path, cmd_arg, true);
                if (ss.IndexOf("error") >= 0) { throw new Exception(ss); }
            }
        }
        public List<string> TransferModule(string srcServerName, string srcUser, string srcPwd, string srcDbName, string srcNmSpace, string srcModule, string transferTables, string[] translateColumns, string tarServerName, string tarUser, string tarPwd, string tarNmSpace, string tarModule, bool frameOnly, bool updateRel, bool updSystem)
        {
            string metaContent = ""; // table content to be transfer in the 'D' database for new module

            List<string> metaTranslate = new List<string> { 
                "AppItem.AppItemId.AppItemCode.Script",
                "DbColumn.ColumnId.ExternalTable",
                "DbTable.TableId.VirtualSql.Script",
                "Release.ReleaseId.TarScriptAft.Script",
                "ReleaseDtl.ReleaseDtlId.ObjectName",
                "Report.ReportId.RegCode.Script",
                "Report.ReportId.ValCode.Script",
                "Report.ReportId.UpdCode.Script",
                "Report.ReportId.XlsCode.Script",
                "ServerRule.ServerRuleId.RuleCode.Script",
            };
            List<string> metaModifiedBy = new List<string> { 
                "UtReport.ModifiedBy",
                "ServerRule.ModifiedBy",
                "Report.ModifiedBy",
                "DbTable.ModifiedBy",
                "CustomDtl.ModifiedBy"
            };
            List<string> metaSystemId = new List<string> { 
                "DbTable.TableId.SystemId",
            };
            string tarDbName = tarNmSpace + tarModule;
            CurrTar tar = GetTar("Sqloledb", tarServerName, tarServerName, tarDbName, tarUser, tarPwd);
            CurrTar tarMaster = GetTar("Sqloledb", tarServerName, tarServerName, "master", tarUser, tarPwd);
            DbCreate(tarMaster.TarConnectionString, tarPwd, tarDbName + (srcDbName != srcNmSpace + "Design" ? "D" : ""));
            List<string> errListMeta = TransferDB(srcServerName, srcUser, srcPwd, srcDbName + (srcDbName != srcNmSpace + "Design" ? "D" : ""), srcNmSpace, tarDbName + (srcDbName != srcNmSpace + "Design" ? "D" : ""), tarNmSpace, metaContent, metaTranslate.ToArray(), tarServerName, tarUser, tarPwd, srcDbName != srcNmSpace + "Design" ? frameOnly : false, true);
            List<string> errList = new List<string>();
            List<string> metaFix = new List<string>();
            if (srcDbName != srcNmSpace + "Design")
            {
                DbCreate(tarMaster.TarConnectionString, tarPwd, tarDbName);
                errList = TransferDB(srcServerName, srcUser, srcPwd, srcDbName, srcNmSpace, tarDbName, tarNmSpace, transferTables, translateColumns, tarServerName, tarUser, tarPwd, frameOnly, false);
            }
            if (tarDbName != tarNmSpace + "Design" && tarDbName != tarNmSpace + "Cmon" && updateRel)
            {
                using (Access3.DeployAccess dac = new Access3.DeployAccess())
                {
                    dac.UpdateRelease(tarServerName, tarNmSpace + "Design", tarUser, tarPwd, tarNmSpace, tarModule);
                }
            }
            if (tarDbName != tarNmSpace + "Design" && tarDbName != tarNmSpace + "Cmon")
            {
                using (Access3.DeployAccess dac = new Access3.DeployAccess())
                {
                    dac.AddSystem(tarServerName, tarNmSpace + "Design", tarUser, tarPwd, tarNmSpace, tarModule, tar.TarConnectionString);
                }
            }
            if (tarDbName != tarNmSpace + "Design") 
            {
                using (Access3.DeployAccess dac = new Access3.DeployAccess())
                {
                    CurrSrc srcDesign = GetSrc("Sqloledb", srcServerName, srcServerName, srcNmSpace + "Design", srcUser, srcPwd);
                    CurrTar tarDesign = GetTar("Sqloledb", tarServerName, tarServerName, tarNmSpace + "Design", tarUser, tarPwd);
                    DataTable dtSrcSystem = (new LoginAccess()).GetSystemsList(srcDesign.SrcConnectionString, srcPwd);
                    DataTable dtTarSystem = (new LoginAccess()).GetSystemsList(tarDesign.TarConnectionString, tarPwd);
                    CurrTar tarD = GetTar("Sqloledb", tarServerName, tarServerName, tarDbName+"D", tarUser, tarPwd);
                    metaFix = dac.FixMetaReference(tarD.TarConnectionString, tarPwd, tarNmSpace + "Design", srcNmSpace, tarNmSpace, tarModule, metaModifiedBy, metaSystemId, dtSrcSystem, dtTarSystem);
                }
            }
            if (errListMeta.Count > 0) errListMeta.Insert(0, "For Database: " + tarDbName + (srcDbName != srcNmSpace + "Design" ? "D" : ""));
            if (errList.Count > 0) errList.Insert(0, "For Database: " + tarDbName);
            errListMeta.AddRange(errList);
            if (metaFix.Count > 0) { errListMeta.Add("Failed Meta Data fix:"); errListMeta.AddRange(metaFix); }
            return errListMeta;
        }
        private List<string> TransferDB(string srcServerName, string srcUser, string srcPwd, string srcDbName, string srcNmSpace, string tarDbName, string tarNmSpace, string transferTables, string[] translateColumns, string tarServerName, string tarUser, string tarPwd, bool frameOnly, bool isMeta)
        {
            List<string> errList = new List<string>();
            List<string> permError = new List<string>();
            CurrSrc src = GetSrc("Sqloledb", srcServerName, srcServerName, srcDbName, srcUser, srcPwd);
            CurrTar tar = GetTar("Sqloledb", tarServerName, tarServerName, tarDbName, tarUser, tarPwd);
            HashSet<string> transferContent = new HashSet<string>(from t in (transferTables ?? "").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries) select t.Trim().ToLower());
            MatchEvaluator meRef = m =>
            {
                return m.Groups[1].Value + tarNmSpace + m.Groups[4].Value;
            };
            MatchEvaluator meQuoted = m =>
            {
                return m.Groups[1].Value + tarNmSpace + m.Groups[2].Value;
            };
            MatchEvaluator meLine = m =>
            {
                return m.Groups[1].Value + tarNmSpace + m.Groups[2].Value;
            };
            Regex reLine = new Regex(@"(\s*|\|)" + srcNmSpace + @"([^.\|]+?.*)",RegexOptions.IgnoreCase);
            Regex reQuoted = new Regex(@"(')" + srcNmSpace + @"([^\r\n']+?')", RegexOptions.IgnoreCase);
            Regex reRef = new Regex(@"((\[|\s|'|=))(" + srcNmSpace + @")([^]\.\r\n]+?\]?\.\[?dbo\]?\.\[?[^]\s']+?(\]|\s|'|\r|$))",RegexOptions.IgnoreCase);
            Dictionary<Regex, MatchEvaluator> simpleRE = new Dictionary<Regex, MatchEvaluator> { {reLine,meLine}};
            Dictionary<Regex, MatchEvaluator> scriptRE = new Dictionary<Regex, MatchEvaluator> { { reRef, meRef },{reQuoted,meQuoted} };

            using (Access3.DeployAccess dac = new Access3.DeployAccess())
            {
                DbScript ds = new DbScript("", true);
                if (!frameOnly || isMeta)
                {
                    string ss = ds.ScriptCreateTables("M", "M", true, src, tar).Replace("\r\nGO\r\n", " \r\n\r\n ");

                    if (!string.IsNullOrEmpty(ss)) dac.DbExec(ss, tar.TarConnectionString, tarPwd, tarDbName);
                    ss = ds.ScriptIndexFK("M", "M", true, src, tar).Replace("\r\nGO\r\n", " \r\n\r\n ");
                    if (!string.IsNullOrEmpty(ss)) dac.DbExec(ss, tar.TarConnectionString, tarPwd, tarDbName);

                    // transfer content 
                    DataTable dtTables = ds.GetTables("M", true, false, false, src, tar);
                    Dictionary<string, KeyValuePair<string, List<string[]>>> needTranslate = new Dictionary<string, KeyValuePair<string, List<string[]>>>();
                    if (translateColumns != null)
                    {
                        foreach (string c in translateColumns)
                        {
                            string[] tc = c.Split(new char[] { '.' });
                            string tbl = tc[0].Trim().ToLower();
                            string kcol = tc[1].Trim();
                            KeyValuePair<string, List<string[]>> cols = new KeyValuePair<string, List<string[]>>(kcol, new List<string[]>());
                            if (needTranslate.ContainsKey(tbl)) cols = needTranslate[tbl];
                            else needTranslate[tbl] = cols;
                            cols.Value.Add(tc.Skip(2).ToArray());
                        }
                    }
                    foreach (DataRow dr in dtTables.Rows)
                    {
                        string tbName = dr["tbName"].ToString();
                        if ((isMeta && !frameOnly) || (transferContent.Count > 0 && transferContent.Contains(tbName.ToLower().Trim())))
                        {
                            dac.TransferTable(srcServerName, srcDbName, srcUser, srcPwd, tbName, tarServerName, tarDbName, tarUser, tarPwd, needTranslate, simpleRE, scriptRE);
                        }
                    }
                }

                bool hasError = false;
                int tries = 3;
                string[] views = ds.ScriptView("M", "M", true, src, tar).Split(new string[] { "\r\nGO" }, StringSplitOptions.RemoveEmptyEntries);
                string[] sps = ds.ScriptSProcedures("M", "M", true, src, tar).Split(new string[] { "\r\nGO" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tries; i++) 
                {
                    List<string> failedView = new List<string>();
                    List<string> failedSP = new List<string>();
                    errList.RemoveAll(_=>true);
                    foreach (string cmd in views) 
                    {
                        string tarCmd = reRef.Replace(cmd, meRef);
                        tarCmd = reQuoted.Replace(tarCmd, meQuoted);
                        try
                        {
                            dac.DbExec(tarCmd, tar.TarConnectionString, tarPwd, tarDbName);
                        }
                        catch (Exception e)
                        {
                            //if (i + 1 == tries) throw;
                            errList.Add(string.Format("{0}\r\n{1}", tarCmd, e.Message));
                            if (!cmd.ToUpper().Contains("DROP VIEW ")) failedView.Add(cmd);
                            else permError.Add(string.Format("{0}\r\n{1}", tarCmd, e.Message));
                            hasError = true;
                        }
                    }

                    foreach (string cmd in sps) 
                    {
                        if (!frameOnly || isMeta || cmd.Contains("MkStoredProcedure"))
                        {
                            string tarCmd = reRef.Replace(cmd, meRef);
                            tarCmd = reQuoted.Replace(tarCmd, meQuoted);
                            try
                            {
                                dac.DbExec(tarCmd, tar.TarConnectionString, tarPwd, tarDbName);
                            }
                            catch (Exception e)
                            {
                                //if (i + 1 == tries) throw;
                                errList.Add(string.Format("{0}\r\n{1}", tarCmd, e.Message));
                                if (!cmd.ToUpper().Contains("DROP PROCEDURE ")) failedSP.Add(cmd);
                                else permError.Add(string.Format("{0}\r\n{1}", tarCmd, e.Message));
                                hasError = true;
                            }
                        }
                    }
                    if (!hasError) break;

                    views = failedView.ToArray();
                    sps = failedSP.ToArray();
                }

                if (isMeta && frameOnly)
                {
                    string initCmd = "EXEC SetScrTab EXEC SetRptGrp";
                    try
                    {
                        dac.DbExec(initCmd, tar.TarConnectionString, tarPwd, tarDbName);
                    }
                    catch (Exception e)
                    {
                        errList.Add(string.Format("{0}\r\n{1}", initCmd, e.Message));
                    }
                }
           }
           permError.AddRange(errList);
           return permError;
        }
    }
}