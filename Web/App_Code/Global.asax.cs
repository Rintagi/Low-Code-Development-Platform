using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using RO.Common3;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace RO
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
        private static bool pulseStarted = false;
        private static object o_lock = new object();

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
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.ServicePointManager.SecurityProtocol;
        }
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
            if (!pulseStarted)
            {
                StartBackgroundTask(Config.EnableSsl);
            }
		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

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
                        || "GET,PUT,DELETE,POST".IndexOf(HttpContext.Current.Request.HttpMethod) >= 0)
                    {
                        string webtitle = System.Configuration.ConfigurationManager.AppSettings["WebTitle"] ?? "";
                        string to = System.Configuration.ConfigurationManager.AppSettings["TechSuppEmail"] ?? "cs@robocoder.com";
                        string from = "cs@robocoder.com";
                        string fromTitle = "";
                        string replyTo = "";
                        string LoginUsrId = null;
                        string LoginUserName = null;
                        string smtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
                        string[] smtpConfig = smtpServer.Split(new char[] { '|' });
                        bool bSsl = smtpConfig[0].Trim() == "true" ? true : false;
                        int port = smtpConfig.Length > 1 ? int.Parse(smtpConfig[1].Trim()) : 25;
                        string server = smtpConfig.Length > 2 ? smtpConfig[2].Trim() : null;
                        string username = smtpConfig.Length > 3 ? smtpConfig[3].Trim() : null;
                        string password = smtpConfig.Length > 4 ? smtpConfig[4].Trim() : null;
                        string domain = smtpConfig.Length > 5 ? smtpConfig[5].Trim() : null;
                        System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                        string[] receipients = to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string sourceIP = string.Format("From: {0}\r\n\r\n", Request != null ? Request.UserHostAddress : "unknown request url");
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
                        mm.Subject = webtitle + " Application Error " + Request.Url.GetLeftPart(UriPartial.Path);
                        mm.Body = Request.Url.ToString() + "\r\n\r\n" + objErr.Message + "\r\n\r\n" + objErr.StackTrace + "\r\n"
                                + "\r\n" + "UserId : " + (LoginUsrId ?? "") + " UserName: " + (LoginUserName ?? "") + "\r\n"
                                + sourceIP
                                + "\r\n";
                        mm.IsBodyHtml = false;
                        mm.From = new System.Net.Mail.MailAddress(string.IsNullOrEmpty(username) || !(username ?? "").Contains("@") ? from : username, string.IsNullOrEmpty(fromTitle) ? from : fromTitle);    // Address must be the same as the smtp login user.
                        mm.ReplyToList.Add(new System.Net.Mail.MailAddress(string.IsNullOrEmpty(replyTo) ? from : replyTo)); // supplied from would become reply too for the 'sending on behalf of'
                        (new RO.WebRules.WebRule()).SendEmail(bSsl, port, server, username, password, domain, mm);
                        mm.Dispose();   // Error is trapped and reported from the caller.
                    }
                }
                catch { }
                Session["ErrMsg"] = objErr.Message.ToString();
                Session["ErrStackTrace"] = objErr.StackTrace.ToString();
                Server.ClearError();
                Response.Redirect("Msg.aspx?typ=E");
            }
            catch { }
        }

        protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
        private void VisitUrl(string url, Action failedAction)
        {
            try
            {
                HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
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
                                    sr.ReadToEnd();
                                    sr.Close();
                                }
                                stream.Close();
                            }
                            resp.Close();
                        }
                    }
                    catch 
                    {
                        if (failedAction != null) failedAction();
                    }
                }, wr);
            }
            catch
            {
                if (failedAction != null) failedAction();
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
            string http = (Config.EnableSsl ? "https" : "http") 
                        + (HttpContext.Current.Request.Url.IsDefaultPort ? "://" : ":" + HttpContext.Current.Request.Url.Port.ToString() + "//")
                        + HttpContext.Current.Request.Url.Host + HttpRuntime.AppDomainAppVirtualPath;
            string hash = Convert.ToBase64String(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(Config.DesPassword)));
            string url = http + "/CronJob.aspx" + "?hash=" + HttpUtility.UrlEncode(hash);
            Action kick = () =>
            {
                System.Threading.Thread.Sleep(2000);
                VisitUrl(url, () =>
                {
                    lock (o_lock)
                    {
                        pulseStarted = false;
                    }
                });
            };
            kick.BeginInvoke(null, null);
        }
			
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