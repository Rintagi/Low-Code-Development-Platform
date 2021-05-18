using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using RO.Facade3;
using RO.Common3;

namespace RO
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
        private static bool pulseStarted = false;
        private static string ROVersion = null;
        private static object o_lock = new object();
        private static string applicationPath = null;
        private static string initRequestUrl = null;
        private static bool started = false;
        public Global()
		{
			InitializeComponent();
		}

        public static List<string> GetExceptionMessage(Exception ex)
        {
            List<string> msg = new List<string>();
            for (var x = ex; x != null; x = x.InnerException)
            {
                if (x is AggregateException && ((AggregateException)x).InnerExceptions.Count > 1)
                {
                    if (((AggregateException)x).InnerExceptions.Count > 1)
                        foreach (var y in ((AggregateException)x).InnerExceptions)
                        {
                            msg.Add(string.Join("\r\n", GetExceptionMessage(y).ToArray()));
                        }
                }
                else
                {
                    msg.Add(x.Message);
                }
            }
            return msg;
        }
		// For the embedded multimedia player only:
		protected void Application_PreSendRequestHeaders(Object source, EventArgs e)
		{
			HttpApplication app = (HttpApplication)source;
			HttpCookieCollection cookies = app.Context.Response.Cookies;
			if (cookies != null)
			{
				foreach (string name in cookies)
				{
                    HttpCookie cookie = cookies[name]; cookie.HttpOnly = (Config.CookieHttpOnly == "false" || name.EndsWith("JS")) ? false : true;
				}
			}
		}


        protected void Application_Start(Object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.ServicePointManager.SecurityProtocol
                | System.Net.SecurityProtocolType.Tls13
                | System.Net.SecurityProtocolType.Tls12
                | System.Net.SecurityProtocolType.Tls11 // some old proxy server needs this, should be something that is controllable via web.config
                ;
            System.Net.ServicePointManager.Expect100Continue = true;

            if (ROVersion == null)
            {
                lock (o_lock)
                {
                    try
                    {
                        ROVersion = (new LoginSystem()).GetRbtVersion();
                    }
                    catch {
                        ROVersion = "unknown";
                    }
                }
            }
        }
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
            if (string.IsNullOrEmpty(applicationPath))
            {
                applicationPath = Request.ApplicationPath;
                initRequestUrl = Request.Url.ToString();
                //Logging(Request, null, initRequestUrl, null, "Application Started", GetRequestInfo());
                try
                {
                    string str = HttpContext.Current.Request.ApplicationPath.ToString();
                    Configuration conf = WebConfigurationManager.OpenWebConfiguration(str);
                    ScriptingJsonSerializationSection section = (ScriptingJsonSerializationSection)conf.GetSection("system.web.extensions/scripting/webServices/jsonSerialization");
                    Application["MaxJsonLength"] = section.MaxJsonLength.ToString();
                }
                catch { }

                started = true;
            }

            if (!pulseStarted 
                //&& !HttpContext.Current.Request.Url.AbsolutePath.Contains("/CronJob.aspx")
                )
            {
                StartBackgroundTask(Config.EnableSsl);
            }
            string extBasePath = Config.ExtBasePath ?? "";
            string appPath = Request.ApplicationPath;
            if (Request.Path == appPath
                && appPath.ToLower() != extBasePath.ToLower()
                && Request.ApplicationPath != "/"
                && Request.Headers["X-Forwarded-For"] != null
                )
            {
                string redirectUrl = Request.Path + "/";
                try
                {
                    Dictionary<string, string> requestHeader = new Dictionary<string, string>();
                    foreach (string x in Request.Headers.Keys)
                    {
                        requestHeader[x] = Request.Headers[x];
                    }
                    requestHeader["Host"] = Request.Url.Host;
                    requestHeader["ApplicationPath"] = appPath;

                    string url = Utils.transformProxyUrl(redirectUrl, requestHeader);
                    Response.Redirect(url);
                }
                catch
                {
                }
            }
            else
            {
                // IIS restriction, modifying cache-control invalidate default document handling in classic pipeline(so must switch to integrated if this is required)
                if (Config.PageCacheControl > 0)
                {
                    // must deliberately set this for time based caching or it becomes public
                    Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
                    // no-cache, max-age and expiry has no effect on forward/backward button(and most likely history)
                    // chrome/firefox default is no-cache if there is no cache-control, except forward/backward button
                    Response.Cache.SetExpires(DateTime.UtcNow.AddSeconds(Config.PageCacheControl.Value));
                }
                else if (Config.PageCacheControl == 0)
                {
                    // deliberately want to retain default asp.net behavior(say to use classic pipeline
                    // won't do anything to the cache control pipeline 
                }
                else  // extreme no-cache
                {
                    //this is absolutely no caching, effectively same as 'form post', even back/forward button in chrome would hit server
                    Response.Cache.SetNoStore();
                }
            }
		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{
            if (Response.Cookies.Count > 0)
            {
                SessionStateSection SessionSettings = ConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;
                string formAuthCookie = System.Web.Security.FormsAuthentication.FormsCookieName;
                string sessionCookie = SessionSettings != null ?  SessionSettings.CookieName : null;
                string extBasePath = Config.ExtBasePath ?? "";
                string appPath = Request.ApplicationPath;
                string xForwardedFor = Request.Headers["X-Forwarded-For"];
                string xForwardedURL = Request.Headers["X-Forwarded-URL"];
                string xForwardedProto = Request.Headers["X-Forwarded-Proto"];
                string xForwardedHttps = Request.Headers["X-Forwarded-Https"];
                string xForwardedHost = Request.Headers["X-Forwarded-Host"];
                foreach (string s in Response.Cookies.AllKeys)
                {
                    if ((formAuthCookie ?? "").Equals(s, StringComparison.InvariantCultureIgnoreCase)
                        || (sessionCookie ?? "").Equals(s, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Response.Cookies[s].HttpOnly = true;
                        bool isSecured = Response.Cookies[s].Secure 
                                                        || (Request.IsSecureConnection 
                                                        || (xForwardedProto ?? "").ToLower() == "https")
                                                        || xForwardedHttps == "on"
                                                        || Request.Cookies["secureChannelTest"] != null
                                                        || (!string.IsNullOrEmpty(xForwardedFor) && Config.EnableSsl && !Request.IsLocal);
                        Response.Cookies[s].Secure = isSecured;
                        string orgPath = Request.Cookies[s].Path;
                        string revisedPath = extBasePath != applicationPath && !string.IsNullOrEmpty(xForwardedFor) ? extBasePath : applicationPath;
                        /* can't do this without lots of coordination at web server level
                         * cookie path is case sensitive but IIS is not
                        Response.Cookies[s].Path = revisedPath;
                         */
                    }
                }
            }
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}


        protected void Application_Error(Object sender, EventArgs e)
        {
            try
            {
                Exception objErr = Server.GetLastError().GetBaseException();
                try
                {
                    if (HttpContext.Current == null
                        || HttpContext.Current.Request == null
                        ||
                        ("GET,PUT,DELETE,POST".IndexOf(HttpContext.Current.Request.HttpMethod) >= 0 
                        /* skip these two form, seems to be coming from crawler
                         * not from crawler but attacker, enable to gauge extend
                        && !Request.Url.GetLeftPart(UriPartial.Path).Contains("WebResource.axd") 
                        && !Request.Url.GetLeftPart(UriPartial.Path).Contains("ScriptResource.axd")
                         */ 
                        /* skip these drive by php/wordpress etc. attack */
                        && !Request.Url.GetLeftPart(UriPartial.Path).Contains(".php")
                        && !Request.Url.GetLeftPart(UriPartial.Path).Contains("popper.js")
                        && !Request.Url.GetLeftPart(UriPartial.Path).Contains("wp-includes")
                        && !(objErr is ThreadAbortException)
                        )
                        )
                    {
                        Logging(Request, objErr, null, null, null, null);
                    }
                }
                catch { }
                if (Request.Url.PathAndQuery.Contains("CronJob.aspx") 
                    &&
                    !string.IsNullOrEmpty(Request.QueryString["hash"])
                    )
                {
                    return;
                }
                if (
                    (Request.IsAuthenticated && HttpContext.Current.User.Identity.Name.ToLower() != "anonymous") || Request.IsLocal || objErr.Message.ToString().Contains("proxy"))
                {
                    Session["ErrMsg"] = objErr.Message.ToString();
                    Session["ErrStackTrace"] = objErr.StackTrace.ToString();
                }
                else
                {
                    Session["ErrMsg"] = "An error has occurred. Please contact your administrator.";
                    Session["ErrStackTrace"] = "";
                }
                Server.ClearError();

                string extBasePath = Config.ExtBasePath ?? "";
                string appPath = Request.ApplicationPath;
                string errorUrl = "Msg.aspx?typ=E";
                if (Request.Url.PathAndQuery.Contains("Msg.aspx"))
                {
                    byte[] err = System.Text.UTF8Encoding.UTF8.GetBytes(objErr.ToString());
                    Response.OutputStream.Flush();
                    Response.OutputStream.Write(err,0,err.Length);
                }
                else
                {
                    if (
                        !string.IsNullOrEmpty(extBasePath)
                        && appPath.ToLower() != extBasePath.ToLower()
                        && Request != null
                        && Request.Headers["X-Forwarded-For"] != null
                        )
                    {
                        try
                        {
                            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
                            foreach (string x in Request.Headers.Keys)
                            {
                                requestHeader[x] = Request.Headers[x];
                            }
                            requestHeader["Host"] = Request.Url.Host;
                            requestHeader["ApplicationPath"] = appPath;

                            string url = Utils.transformProxyUrl(errorUrl, requestHeader);
                            Response.Redirect(url);
                        }
                        catch
                        {
                            Response.Redirect(errorUrl);
                        }

                    }
                    else
                    {
                        Response.Redirect(errorUrl);
                    }
                }

            }
            catch { }
        }

        protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{
            //Logging(null, null, initRequestUrl, null, "Application ended", null);
		}

        #region helpers
        private Dictionary<string, string> GetRequestInfo()
        {
            Dictionary<string, string> info = new Dictionary<string, string>(){
                                                {"UserHostAddress",Request.UserHostAddress},
                                                {"Host",Request.Url.Host},
                                                {"X-Forwarded-For",Request.Headers["X-Forwarded-For"]},
                                                {"X-Forwarded-Host",Request.Headers["X-Forwarded-Host"]},
                                                {"X-Forwarded-Proto",Request.Headers["X-Forwarded-Proto"]},
                                                {"X-Forwarded-Port",Request.Headers["X-Forwarded-Port"]},
                                                {"X-Original-URL",Request.Headers["X-Original-URL"]}
                                            };
            return info;
        }
        private void VisitUrl(string url, Action<string> successAction, Action<Exception> failedAction)
        {
            try
            {
                HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
                wr.Timeout = 10000;
                wr.CookieContainer = new CookieContainer();
//                wr.GetResponse();
                wr.BeginGetResponse(x =>
                {
                    try
                    {
                        using (WebResponse resp = (x.AsyncState as WebRequest).EndGetResponse(x))
                        {
                            using (Stream stream = resp.GetResponseStream())
                            {
                                using (StreamReader sr = new StreamReader(stream))
                                {
                                    string content = sr.ReadToEnd();
                                    sr.Close();
                                    System.Web.HttpRequest request = null;
                                    try
                                    {
                                        // access can fail
                                        request = Request;

                                    }
                                    catch { }
                                    if (successAction != null)
                                    {
                                        successAction(content);
                                    }
                                }
                                stream.Close();
                            }
                            resp.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (failedAction != null) failedAction(ex);
                    }
                }, wr);
            }
            catch (Exception ex)
            {
                if (failedAction != null) failedAction(ex);
            }
        }
        private void StartBackgroundTask(bool ssl)
        {
            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                //always true to work around ssl issue..
                return true;
            };

            lock (o_lock)
            {
               pulseStarted = true;
            }
            string appPath = HttpRuntime.AppDomainAppVirtualPath;
            string http = ((Request.IsSecureConnection) ? "https://" : "http://")
                        + HttpContext.Current.Request.Url.Host
                        + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port.ToString())
                        + (appPath == "/" ? "" : appPath);
            string cronjobBaseUrl =
                    !string.IsNullOrEmpty(Config.CronJobBaseUrl)
                    ? Config.CronJobBaseUrl
                    : (!string.IsNullOrEmpty(Config.IntBaseUrl) ? Config.IntBaseUrl
                    : ""
                    );
            string hash = Convert.ToBase64String(new System.Security.Cryptography.SHA256CryptoServiceProvider().ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(Config.DesPassword)));
            string url = (string.IsNullOrEmpty(cronjobBaseUrl) ? http : cronjobBaseUrl) + "/CronJob.aspx" + "?hash=" + HttpUtility.UrlEncode(hash);

            Dictionary<string, string> info = GetRequestInfo();
            Action kick = () =>
            {
                System.Threading.Thread.Sleep(2000);
                VisitUrl(url, (string content) => {
                    //Logging(null, null, url, null, "cronjob started" + "\r\n", info);
                },
                    (Exception ex) =>
                {
                    lock (o_lock)
                    {
                        pulseStarted = false;
                    }
                    Logging(null, ex, url, null, "fail to kickstart Cronjob", info);
                });
            };
            kick.BeginInvoke(null, null);
        }
        private void LogToFile(HttpRequest request, Exception objErr, string url, string title, string message, Exception ex)
        {
            try
            {
                string fileName = DateTime.UtcNow.ToString("yyyyMMdd") + "_" + "ErrorLog.txt";
                using (var ws = new StreamWriter(Config.PathTmpImport + "/" + fileName, true))
                {
                    string log =
                        (started ? "" : "====================================================")
                        + Environment.NewLine + DateTime.UtcNow.ToString("o") + Environment.NewLine
                        + title + Environment.NewLine
                        + (ex != null ? ex.Message : "") + Environment.NewLine
                        + (ex != null ? "Stack Trace: " + Environment.NewLine + ex.Message : "")
                        + (objErr != null ? "Original Error: " + Environment.NewLine + (objErr != null ? objErr.Message : message) + Environment.NewLine : message + Environment.NewLine)
                        + (objErr != null ? "Original Stack Trace: " + (objErr != null ? objErr.StackTrace : "no stack trace") + Environment.NewLine : "")
                        + "Machine: " + Environment.MachineName + Environment.NewLine
                        + "AppPath: " + applicationPath + Environment.NewLine
                        + (objErr != null ? "url: " + (request != null ? request.Url.AbsolutePath : url) + Environment.NewLine : url + Environment.NewLine)
                        + (objErr != null ? "From: " + (request != null ? request.UserHostAddress : "uknown source") + Environment.NewLine : "")
                        + "Incoming via Host: " + (request != null ? request.Url.Host : "unknown host") + Environment.NewLine;

                    ws.Write(log);
                    ws.Close();
                }
            }
            catch
            {
            }

        }
        private bool Logging(HttpRequest request, Exception objErr, string url, string title, string message, Dictionary<string, string> info)
        {
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
            try
            {
                string webtitle = Config.WebTitle ?? "";
                string to = (Config.TechSuppEmail ?? "cs@robocoder.com").Replace(",",";");
                string from = "cs@robocoder.com";
                string fromTitle = "";
                string replyTo = "";
                string LoginUsrId = null;
                string LoginUserName = null;
                string smtpServer = Config.SmtpServer;
                string[] smtpConfig = smtpServer.Split(new char[] { '|' });
                bool bSsl = smtpConfig[0].Trim() == "true" ? true : false;
                int port = smtpConfig.Length > 1 ? int.Parse(smtpConfig[1].Trim()) : 25;
                string server = smtpConfig.Length > 2 ? smtpConfig[2].Trim() : null;
                string username = smtpConfig.Length > 3 ? smtpConfig[3].Trim() : null;
                string password = smtpConfig.Length > 4 ? smtpConfig[4].Trim() : null;
                string domain = smtpConfig.Length > 5 ? smtpConfig[5].Trim() : null;
                string[] receipients = to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string xForwardedFor = request != null ? request.Headers["X-Forwarded-For"] : (info != null ? info["X-Forwarded-For"] : "");
                string sourceIP = string.Format("From: {0} {1}\r\n\r\n", 
                        request != null ? request.UserHostAddress : (info != null ? info["UserHostAddress"] : "unknown source")
                        ,string.IsNullOrEmpty(xForwardedFor) ? "" : " For "  + xForwardedFor );
                string xForwardedHost = request != null ? request.Headers["X-Forwarded-Host"] : (info != null ? info["X-Forwarded-Host"] : "");
                string xForwardedProto = request != null ? request.Headers["X-Forwarded-Proto"] : (info != null ? info["X-Forwarded-Proto"] : "");
                string xOriginalURL = request != null ? request.Headers["X-Original-URL"] : (info != null ? info["X-Original-URL"] : "");
                string host = string.Format("Host: {0} {1}\r\n\r\n",
                    request != null ? request.Url.Host : (info != null ? info["Host"] : "")
                    , string.IsNullOrEmpty(xForwardedHost) ? "" : " Via " + xForwardedHost);
                string path = string.Format("Path: {0} {1}\r\n\r\n",
                    request != null ? request.Url.PathAndQuery : (info != null ? info["Host"] : "")
                    , string.IsNullOrEmpty(xOriginalURL) ? "" : " Incoming " + xOriginalURL);
                string machine = string.Format("Machine: {0}\r\n\r\n", Environment.MachineName);
                string userAgent = string.Format("UserAgent: {0}\r\n\r\n", request != null ? request.UserAgent : "");
                string roVersion = string.Format("RO Version: {0}\r\n\r\n", ROVersion);
                string currentTime = string.Format("Server Time: {0} \r\n\r\n UTC: {1} \r\n\r\n", DateTime.Now.ToString("O"), DateTime.UtcNow.ToString("O"));

                var exMessages = GetExceptionMessage(objErr);
                try
                {
                    LoginUsrId = ((RO.Common3.Data.LoginUsr)Session["Cache:LUser"]).UsrId.ToString();
                    LoginUserName = ((RO.Common3.Data.LoginUsr)Session["Cache:LUser"]).UsrName.Left(4) + "??????";
                }
                catch { }
                foreach (var t in receipients)
                {
                    mm.To.Add(new System.Net.Mail.MailAddress(t.Trim()));
                }
                mm.Subject = string.IsNullOrEmpty(title)
                    ? webtitle + (objErr != null ? " Application Error " : " Application Message ") + (request != null ? request.Url.GetLeftPart(UriPartial.Path) : url)
                    : webtitle + " " + title;

                mm.Body = message + "\r\n" +
                        (request != null ? request.Url.ToString() : url) + "\r\n\r\n" + (objErr != null ? objErr.Message : "no error") + "\r\n\r\n" + (objErr != null ? objErr.StackTrace : "") + "\r\n"
                            + "\r\n"
                            + sourceIP
                            + "UserId : " + (LoginUsrId ?? "") + " UserName: " + (LoginUserName ?? "") + "\r\n"
                            + userAgent
                            + host 
                            + path
                            + machine
                            + currentTime
                            + roVersion
                            + "";

                if (receipients.Length > 0)
                {
                    mm.IsBodyHtml = false;
                    mm.From = new System.Net.Mail.MailAddress(string.IsNullOrEmpty(username) || !(username ?? "").Contains("@") ? from : username, string.IsNullOrEmpty(fromTitle) ? from : fromTitle);    // Address must be the same as the smtp login user.
                    mm.ReplyToList.Add(new System.Net.Mail.MailAddress(string.IsNullOrEmpty(replyTo) ? from : replyTo)); // supplied from would become reply too for the 'sending on behalf of'
                    (new RO.WebRules.WebRule()).SendEmail(bSsl, port, server, username, password, domain, mm);
                    mm.Dispose();   // Error is trapped and reported from the caller.
                }

                LogToFile(request, objErr, url, mm.Subject, message != null ? message : mm.Body, null);

            }
            catch (Exception ex)
            {
                try
                {
                    LogToFile(request, objErr, url, mm.Subject, message != null ? message : mm.Body, ex);
                }
                catch
                {
                }
            }

            return true;
        }
        #endregion
        #region Web Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}