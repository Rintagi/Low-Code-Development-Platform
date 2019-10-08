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