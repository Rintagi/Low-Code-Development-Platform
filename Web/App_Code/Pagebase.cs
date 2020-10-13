namespace RO.Web
{
	using System;
	using System.Data;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.ComponentModel;
    using System.IO;
    using RO.Common3;

    public class PageBase : System.Web.UI.Page
    {
        private static string PageExpandNode;	//Temporary bug get-around on treeview auto-collapse.

        public PageBase()
        {
        }

        public static string ExpandNode
        {
            get
            {
                return PageExpandNode;
            }
            set
            {
                PageExpandNode = value;
            }
        }

        protected void SetMasterPage()
        {
            string MasterPgFile = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["m"]))
            {
                MasterPgFile = Request.QueryString["m"].ToString();
            }
            else
            {
                try { MasterPgFile = ((RO.Common3.Data.UsrPref)Session["Cache:LPref"]).MasterPgFile; }
                catch { }
                finally { if (string.IsNullOrEmpty(MasterPgFile)) try { MasterPgFile = RO.Common3.Config.MasterPgFile; } catch { MasterPgFile = "Default.master"; } }
            }
            if (string.IsNullOrEmpty(MasterPgFile) || !System.IO.File.Exists(Server.MapPath(MasterPgFile))) { Page.MasterPageFile = "Default.Master"; } else { Page.MasterPageFile = MasterPgFile; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string appPath = Request.ApplicationPath;
            string extBasePath = Config.ExtBasePath;
            string extDomain = Config.ExtDomain;
            string extBaseUrl = Config.ExtBaseUrl;
            string xForwardedFor = Request.Headers["X-Forwarded-For"];
            string xForwardedURL = Request.Headers["X-Forwarded-URL"];
            if (!string.IsNullOrEmpty(extBasePath)
                && Config.TranslateExtUrl
                && !string.IsNullOrEmpty(xForwardedFor)
                && (appPath.ToLower() != extBasePath.ToLower()
                )
                )
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                base.Render(htw);
                string s = sw.ToString();

                /* only work if application path IS THE SAME LENGTH as external BasePath. i.e. /XX/ => /YY/ but not /ABC/ => /EFGH/ 
                 * due to partial postback is length sensitive in content */
                /* only do this for partial postback not html content or it would be doubled */
                if (Response.ContentType == "text/plain" && (appPath.Length == extBasePath.Length))
                {
                    // only do this 
                    s = s.ReplaceInsensitive((Request.ApplicationPath + "/WebResource.axd").Replace("//", "/"), (extBasePath + "/WebResource.axd").Replace("//", "/"))
                         .ReplaceInsensitive((Request.ApplicationPath + "/ScriptResource.axd").Replace("//", "/"), (extBasePath + "/ScriptResource.axd").Replace("//", "/"));
                }
                var rx = new System.Text.RegularExpressions.Regex("((src|href)=[\"'])(/" + appPath.Substring(1) + ")");
                s = rx.Replace(s, (m) => { return m.Groups[1].Value + extBasePath + (appPath == "/" ? "/" : ""); });
                writer.Write(s);
            }
            else base.Render(writer);

        }

    }

}