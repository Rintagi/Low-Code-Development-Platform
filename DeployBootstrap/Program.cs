using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Install
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();


        [STAThread]
        static void Main(string[] args)
        {
            Item item = new Item();			// Version info.
            ItemPDT iPDT = new ItemPDT();		// Existing Rbt:Rintagi/App:Production.
            ItemDEV iDEV = new ItemDEV();		// Existing Rbt:Developer/App:Extranet.
            ItemPTY iPTY = new ItemPTY();		// Existing Rbt:Application/App:Prototype.
            ItemNPDT nPDT = new ItemNPDT();		// New Rbt:Rintagi/App:Production.
            ItemNDEV nDEV = new ItemNDEV();	// New Rbt:Developer/App:Extranet.
            ItemNPTY nPTY = new ItemNPTY();		// New Rbt:Application/App:Prototype.

            if (args.Length == 0)
            {
                FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Default());
            }
            else
            {
                string[] prefixes = new string[] { "/m:", "/n:", "/v:", "/dat:", "/u:", "/p:", "/c:", "/d:", "/r", "/?", "/noauto", "/cln:", "/rul:", "/xls:", "/wsv:", "/au:", "/ap:", "/debug", "/s:", "/nouser", "/url:", "/nodata", "/32bit", "/sspi" };
                CmdArgExtractor cae = new CmdArgExtractor(prefixes, '/', ':');
                List<string> invalidArgs = cae.InvalidArgsPrefixes(args);
                if (invalidArgs.Count > 0)
                {
                    Usage("");
                    foreach (string x in invalidArgs)
                    {
                        Console.WriteLine(string.Format("invalid parameter -> {0}", x));
                    }
                    Environment.Exit(99);
                }
                Dictionary<string, string> kv = cae.GetArgs(Environment.GetCommandLineArgs());
                if (kv.ContainsKey("?"))
                {
                    Usage(""); Environment.Exit(99);
                }

                if (kv.ContainsKey("debug"))
                {
                    foreach (KeyValuePair<string, string> x in kv)
                    {
                        if (x.Key != "p" && x.Key != "ap" ) Console.WriteLine("{0} => {1}", x.Key, x.Value);
                        else Console.WriteLine("{0} => {1}", x.Key, x.Value.Substring(0, 1));

                    }
                }
                if (!kv.ContainsKey("n") || string.IsNullOrEmpty(kv["n"])) { Usage("must provide name space"); Environment.Exit(99); }
                if (!kv.ContainsKey("v") || string.IsNullOrEmpty(kv["v"])) { Usage("must provide sql server version"); Environment.Exit(99); }
                if (!kv.ContainsKey("dat") || string.IsNullOrEmpty(kv["dat"])) { Usage("must provide sql server name"); Environment.Exit(99); }
                if (!kv.ContainsKey("u") || string.IsNullOrEmpty(kv["u"])) { Usage("must provide sql server sys login name"); Environment.Exit(99); }
                if (!kv.ContainsKey("p") || string.IsNullOrEmpty(kv["p"])) { Usage("must provide sql server sys login password"); Environment.Exit(99); }
                if (!kv.ContainsKey("au") || string.IsNullOrEmpty(kv["au"])) { Usage("must provide sql server app user login name"); Environment.Exit(99); }
                if (!kv.ContainsKey("ap") || string.IsNullOrEmpty(kv["ap"])) { Usage("must provide sql server app user login password"); Environment.Exit(99); }
                if (!kv.ContainsKey("c") || string.IsNullOrEmpty(kv["c"])) { Usage("must provide target backup directory for client tier files"); Environment.Exit(99); }
                if (!kv.ContainsKey("d") || string.IsNullOrEmpty(kv["d"])) { Usage("must provide target backup directory for data tier files"); Environment.Exit(99); }
                if (kv.ContainsKey("cln") && string.IsNullOrEmpty(kv["cln"])) { Usage("must provide valid client tier root location"); Environment.Exit(99); }
                if (kv.ContainsKey("wsv") && string.IsNullOrEmpty(kv["wsv"])) { Usage("must provide valid web server tier root location"); Environment.Exit(99); }
                if (kv.ContainsKey("xls") && string.IsNullOrEmpty(kv["xls"])) { Usage("must provide valid xls tier location"); Environment.Exit(99); }
                if (kv.ContainsKey("rul") && string.IsNullOrEmpty(kv["rul"])) { Usage("must provide valid rintagi root location"); Environment.Exit(99); }

                string wwwroot = kv.ContainsKey("cln") ? kv["cln"] : @"c:\inetpub\wwwroot" + @"\" + kv["n"] + @"\Web\";
                string wsvroot = kv.ContainsKey("wsv") ? kv["wsv"] : @"c:\inetpub\wwwroot" + @"\" + kv["n"] + @"Ws\";
                string xlsroot = kv.ContainsKey("xls") ? kv["xls"] : @"c:\inetpub\wwwroot" + @"\" + @"wsxls\";
                string rintagiroot = kv.ContainsKey("rul") ? kv["rul"] : @"c:\rintagi" + @"\" + kv["n"] + @"\";
                string site = kv.ContainsKey("s") ? kv["s"] : "Default Web Site";
                Dictionary<string, string> tiers = new Dictionary<string, string>();
                Dictionary<string, string> dataServer = new Dictionary<string, string>();
                tiers["client"] = wwwroot;
                tiers["ws"] = wsvroot;
                tiers["xls"] = xlsroot;
                if (kv.ContainsKey("r")) tiers["rule"] = rintagiroot;
                if (kv.ContainsKey("s")) tiers["site"] = site; else tiers["site"] = "Default Web Site";
                dataServer["serverType"] = "M";
                dataServer["server"] = kv["dat"];
                dataServer["user"] = kv["u"];
                dataServer["password"] = kv["p"];
                dataServer["design"] = kv["n"] + "Design";
                Action<int, string> progress = (int p, string msg) =>
                {
                    if (!string.IsNullOrEmpty(msg))
                        Console.WriteLine(string.Format("{0} : {1}", DateTime.Now, msg));
                };

                string uniqueStr = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");

                try
                {
                    if (!kv.ContainsKey("m") || string.IsNullOrEmpty(kv["m"]) || kv["m"] == "bak")
                    {
                        // make sure error comes back to us instead of messagebox
                        Utils.errorMsg = (msg) =>
                        {
                            Console.WriteLine(msg); Environment.Exit(99);
                        };
                        if (kv.ContainsKey("nodata")) dataServer["serverType"] = "";
                        Utils.Backup(tiers, dataServer, kv["c"] + (kv.ContainsKey("noauto") ? "" : "\\" + kv["n"] + uniqueStr), kv["d"] + (kv.ContainsKey("noauto") ? "" : "\\" + kv["n"] + uniqueStr), progress, kv.ContainsKey("sspi"));
                    } 
                    else {
                        tiers["newNS"] = kv["n"];
                        try {
                            tiers["webServer"] = !kv.ContainsKey("url") || string.IsNullOrEmpty(kv["url"]) ? 
                                (tiers["site"] == "Default Web Site" ? "localhost" : tiers["site"]) : 
                                kv["url"]; 
                        }
                        catch 
                        { 
                            tiers["webServer"] = "localhost"; 
                        };
                        tiers["wsUrl"] = "http://" + tiers["webServer"] + "/ReportServer/ReportService2005.asmx";
                        tiers["isNet2"] = "N";
                        tiers["enable32Bit"] = kv.ContainsKey("32bit") ? "Y" : "N";

                        dataServer["appUser"] = kv["au"];
                        dataServer["appPwd"] = kv["ap"];
                        dataServer["IntegratedSecurity"] = kv.ContainsKey("sspi") ? "Y" : "N";

                        try { dataServer["dbpath"] = kv["dbp"];} catch { dataServer["dbpath"] = @"c:\sqldata";};
                        try { dataServer["serverVer"] = kv["v"] == "2005" ? "90" : (kv["v"] == "2008" ? "100" : (kv["v"] == "2012" ? "110" : "100"));} catch { dataServer["serverVer"] = "100";};
                        if (kv.ContainsKey("nodata")) dataServer["dbpath"] = "";
                        // make sure error comes back to us instead of messagebox
                        bool hasError = false;
                        Utils.msgBox = (msg, caption) => { Console.WriteLine(msg); return DialogResult.Yes; };
                        Utils.errorMsg = (msg) =>
                        {
                            Console.WriteLine(msg); hasError = true;
                        };
                        if (kv["m"] == "n") {
                            if (System.IO.Directory.Exists(tiers["client"]))
                            {
                                Console.WriteLine(string.Format("Client tier {0} already exists, delete and try again", tiers["client"])); Environment.Exit(99);
                            }
                            Console.WriteLine(string.Format("Creating new App -> {0} from {1}", tiers["newNS"], item.GetOldNS()));
                            Utils.NewApp(tiers, dataServer, "", "", progress, item, iPDT, iDEV, iPTY, nPDT, nDEV, nPTY, kv.ContainsKey("nouser"));
                        } else {
                            Console.WriteLine(string.Format("Upgrading App -> {0} from {1}", tiers["newNS"], item.GetOldNS()));
                            Utils.UpgradeApp(tiers, dataServer, "", "", progress, item, iPDT, iDEV, iPTY, nPDT, nDEV, nPTY);
                        }
                        if (!hasError)
                        {
                            Console.WriteLine("Installation completed successfully"); Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Installation FAILED"); Environment.Exit(99);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Back Failed");
                    Environment.Exit(99);
                }

            }
        }
        static void Usage(string msg)
        {
            Console.Write("Usage: " +
                "Install [/n:GSX /v:2008 /dat:sqlserver /u:user /p:password /ap:app_user /au:appPassword /c:clientTargetDir /d:dataTargetDir [/r] [/cln:clienttier] [/rul:ruletier] [/wsv:wstier] [/xls:xlstier]] [/?] \r\n\r\n" +
                "       /n: application name space like RO\r\n" +
                "       /m: mode. b means backup, n means new application, u means upgrade. default(i.e. not specified) is b \r\n" +
                "       /v: SQL server client version installed 2005 or 2008\r\n" +
                "       /dat: SQL server name\r\n" +
                "       /u: SQL server sys login name such as sa\r\n" +
                "       /p: SQL server sys login password\r\n" +
                "       /au: SQL server user login name(the app will run under)\r\n" +
                "       /ap: SQL server user login password\r\n" +
                "       /c: client tier backup target location(directory)\r\n" +
                "       /d: data tier backup target location(directory), must be accessible from the SQL server running machine\r\n" +
                "       /s: IIS site name the app will be installed under, default to 'Default Web Site'\r\n" +
                "       /r include rule tier\r\n" +
                "       /url: base URL for the application, default to 'localhost' \r\n" +
                "       /dbp: SQL server data location, default to c:\\sqldata \r\n" +
                "       /cln: root of client tier, default to c:\\inetpub\\wwwroot\\<namespace>\\Web \r\n" +
                "       /wsv: root of web server tier, default to c:\\inetpub\\wwwroot\\<namespace>Ws \r\n" +
                "       /xls: root of wsxls tier, default to c:\\inetpub\\wwwroot\\wsxls \r\n" +
                "       /rul: root of rintagi rule tier, default to c:\\rintagi\\<namespace> \r\n" +
                "       /noauto do not auto generate sub-directory(based on current time) under the specified target directory\r\n" +
                "       /nouser do not create login users for the new system\r\n" +
                "       /nodata do not touch data tier\r\n" +
                "       /? This help message\r\n\r\n"
                );

            Console.WriteLine(msg);
        }
    }
}