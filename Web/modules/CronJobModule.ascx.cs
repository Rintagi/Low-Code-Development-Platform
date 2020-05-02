using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using RO.Facade3;
using RO.Common3;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Collections.Generic;


public partial class CronJobModule : RO.Web.ModuleBase
{
    private static DataTable SystemList = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);
    private static bool running = false;
    private static bool summNotify = false;

    private void InvokeJob(int jobId, string systemId, string jobLink, string baseUrl, Dictionary<string,string> requestInfo)
    {
        try
        {
            string jobUrl = jobLink.StartsWith("http") ? jobLink : baseUrl + "/" + jobLink + (jobLink.Contains("?") ? "&" : "?");
            string url = jobUrl + "jid=" + jobId.ToString() + "&cron=" + Application.GetHashCode().ToString();
            bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y";

            HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
            wr.CookieContainer = new CookieContainer();
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
                catch (WebException we)
                {
                    if (we.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = we.Response as HttpWebResponse;

                        if (response != null)
                        {
                            try
                            {
                                int status = (int)response.StatusCode;
                                if (status >= 400)
                                {
                                    DataView dv = new DataView(SystemList);
                                    dv.RowFilter = "SystemId = " + systemId;
                                    if (dv.Count > 0)
                                    {
                                        string connStr = Config.GetConnStr(dv[0]["dbAppProvider"].ToString(), dv[0]["ServerName"].ToString()
                                            , dv[0]["dbDesDatabase"].ToString(), ""
                                            , dv[0]["dbAppUserId"].ToString());
                                        (new AdminSystem()).UpdCronJobStatus(jobId, 
                                            DateTime.Now.ToUniversalTime().ToString() + " - " + we.Message + "(" + jobUrl + ")"
                                            , connStr
                                            , singleSQLCredential ? Config.DesPassword : dv[0]["dbAppPassword"].ToString());
                                    }
                                }
                                else if (status >= 300)
                                {
                                    throw new Exception("redirect " + url);
                                }
                            }
                            catch {
                                throw;
                            }
                        }
                        else
                        {
                            throw new Exception("no response code from web request " + url, we);
                            // no http status code available
                        }
                    }
                    else
                    {
                        throw new Exception("other error web request " + url, we);
                        // no http status code available
                    }
                }
                catch (Exception e) {
                    if (e == null) throw;
                }
            }, wr);
        }
        catch (Exception e1) {
            if (e1 == null) throw;
            try
            {
                ErrorTrace(e1, "warning", requestInfo);
            }
            catch { }
        }

    }
    private void RunJobs()
    {
        string xForwardedFor = HttpContext.Current.Request.Headers["X-Forwarded-For"];
        string xForwardedProto = HttpContext.Current.Request.Headers["X-Forwarded-Proto"];
        string xForwardedHost = HttpContext.Current.Request.Headers["X-Forwarded-Host"];
        string appPath =
                !string.IsNullOrEmpty(xForwardedFor)
                && !string.IsNullOrEmpty(Config.ExtBasePath)
                    ? Config.ExtBasePath
                    : HttpContext.Current.Request.ApplicationPath;
        string http = ((Request.IsSecureConnection) ? "https" : "http")
                    + (Request.Url.IsDefaultPort ? "://" : ":" + Request.Url.Port.ToString() + "//")
                    + Request.Url.Host;
        string cronjobBaseUrl =
                !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CronJobBaseUrl"])
                ? System.Configuration.ConfigurationManager.AppSettings["CronJobBaseUrl"]
                : (!string.IsNullOrEmpty(Config.IntBaseUrl) ? Config.IntBaseUrl
                : ""
                );
        string baseUrl = !string.IsNullOrEmpty(cronjobBaseUrl) 
                ? cronjobBaseUrl
                : http + (HttpRuntime.AppDomainAppVirtualPath == "/" ? "" : HttpRuntime.AppDomainAppVirtualPath) 
                ;
        string myUrl = string.IsNullOrEmpty(cronjobBaseUrl) ? http + Request.Url.AbsolutePath : cronjobBaseUrl + "/CronJob.aspx";
        Dictionary<string, string> requestInfo = GetRequestInfo();
        Func<DateTime> currentTime = () => DateTime.Parse(DateTime.Now.ToUniversalTime().ToString("g"));    // strip to minute for comparison.
        Action jobTask = () =>
        {
            try
            {
                while (true)
                {
                    lock (SystemList)
                    {
                        if (running) return;
                        else running = true;
                    }

                    DateTime nextCheck = currentTime().AddMinutes(15);
                    bool perMinute = false;
                    List<string> jobsRun = new List<string>();
                    Dictionary<string, string> links = new Dictionary<string, string>();
                    DateTime now = currentTime();
                    string admEmail = base.SysAdminEmail(3);
                    List<Tuple<Exception, Dictionary<string, string>>> errorList = new List<Tuple<Exception, Dictionary<string, string>>>();
                    bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y";

                    try
                    {
                        foreach (DataRow dr in SystemList.Rows)
                        {
                            if (dr["Active"].ToString() != "Y") continue;
                            string connStr = Config.GetConnStr(dr["dbAppProvider"].ToString(), dr["ServerName"].ToString(), dr["dbDesDatabase"].ToString(), "", dr["dbAppUserId"].ToString());
                            DataTable dtJobs = (new AdminSystem()).GetCronJob(null,string.Empty, connStr
                                , singleSQLCredential ? Config.DesPassword : dr["dbAppPassword"].ToString());
                            dtJobs.DefaultView.Sort = "NextRun, Year, Month, Day, Hour, Minute";
                            foreach (DataRowView drvJob in dtJobs.DefaultView)
                            {
                                now = currentTime();
                                string jobLink = drvJob["JobLink"].ToString();
                                if (drvJob["NextRun"].ToString() == "")
                                {
                                    if (!links.ContainsKey(jobLink))
                                    {
                                        //InvokeJob((int)drvJob["CronJobId"], dr["SystemId"].ToString(), jobLink, baseUrl);
                                        //jobsRun.Add(drvJob["JobLink"].ToString() + " first time on " + now.ToString());
                                        //links.Add(jobLink, jobLink);
                                        try
                                        {
                                            base.UpdCronStatus((int)drvJob["CronJobId"], true, connStr
                                                ,singleSQLCredential ? Config.DesPassword : dr["dbAppPassword"].ToString());
                                        }
                                        catch (Exception ex) {
                                            try
                                            {
                                                string errorMsg = string.Format("Cronjob update status error for {0}({1})", jobLink, drvJob["CronJobId"].ToString());
                                                ErrorTrace(new Exception(errorMsg, ex), "error", requestInfo);
                                            }
                                            catch { }
                                            //var d = new Dictionary<string,string>(){
                                            //    {"CronJobId", drvJob["CronJobId"].ToString()},
                                            //    {"JobLink", jobLink},
                                            //    {"Error",errorMsg},
                                            //};
                                            //errorList.Add(new Tuple<Exception, Dictionary<string, string>>(ex, d));
                                        }
                                    }
                                }
                                else
                                {
                                    DateTime nextRun = (DateTime)drvJob["NextRun"];
                                    DateTime lastRun = (DateTime)drvJob["LastRun"];
                                    if (nextRun <= now && nextRun.AddMinutes(1) > lastRun)
                                    {
                                        if (!links.ContainsKey(jobLink))
                                        {
                                            InvokeJob((int)drvJob["CronJobId"], dr["SystemId"].ToString(), jobLink, baseUrl, requestInfo);
                                            jobsRun.Add(drvJob["JobLink"].ToString() + " intended on " + nextRun.ToString() + " UTC invoked on " + now.ToString() + " UTC");
                                            links.Add(jobLink, jobLink);
                                            if (jobLink.ToLower().StartsWith("http"))
                                            {
                                                try
                                                {
                                                    base.UpdCronStatus((int)drvJob["CronJobId"], connStr
                                                        , singleSQLCredential ? Config.DesPassword : dr["dbAppPassword"].ToString());
                                                }
                                                catch (Exception ex)
                                                {
                                                    try
                                                    {
                                                        string errorMsg = string.Format("Cronjob update status error for {0}({1})", jobLink, drvJob["CronJobId"].ToString());
                                                        ErrorTrace(new Exception(errorMsg, ex), "error", requestInfo);
                                                    }
                                                    catch { }
                                                    //var d = new Dictionary<string, string>(){
                                                    //    {"CronJobId", drvJob["CronJobId"].ToString()},
                                                    //    {"JobLink", jobLink},
                                                    //    {"Error",string.Format("Cronjob update status error for {0}({1})", jobLink, drvJob["CronJobId"].ToString())},
                                                    //};
                                                    //errorList.Add(new Tuple<Exception, Dictionary<string, string>>(ex, d));
                                                }
                                            }
                                        }

                                        //short? year = drvJob.Row.Field<short?>("Year");
                                        //byte? month = drvJob.Row.Field<byte?>("Month");
                                        //byte? day = drvJob.Row.Field<byte?>("Day");
                                        //byte? hour = drvJob.Row.Field<byte?>("Hour");
                                        //byte? min = drvJob.Row.Field<byte?>("Minute");
                                        //byte? dow = drvJob.Row.Field<byte?>("DayOfWeek");
                                        ////DateTime? next = RO.Common3.Utils.GetNextRun(year, month, day, dow, hour, min);
                                        //DateTime? next = GetNextRun(currentTime(), year, month, day, dow, hour, min);
                                        //(new AdminSystem()).UpdCronJob((int)drvJob["CronJobId"], DateTime.Now, next, connStr, dr["dbAppPassword"].ToString());
                                    }
                                    else
                                    {
                                        if (nextRun < nextCheck) { nextCheck = nextRun; }
                                        break;
                                    }
                                }
                            }
                            now = currentTime();
                            dtJobs.DefaultView.RowFilter = string.Format(@" (Year IS NULL OR Year = {0}) 
                                                                            AND (Month IS NULL OR Month = {1})
                                                                            AND (Day IS NULL OR Day = {2})
                                                                            AND (DayOfWeek IS NULL OR DayOfWeek = {3})
                                                                            AND (Hour IS NULL OR Hour = {4})
                                                                            AND Minute IS NULL", now.Year,now.Month,now.Day,(int) now.DayOfWeek,now.Hour);
                            if (dtJobs.DefaultView.Count > 0)
                            {
                                perMinute = true;
                            }
                            else
                            {
                                //dtJobs.DefaultView.RowFilter = "";
                                //DataRow drJob = dtJobs.DefaultView[0].Row;
                                //short? year = drJob.Field<short?>("Year");
                                //byte? month = drJob.Field<byte?>("Month");
                                //byte? day = drJob.Field<byte?>("Day");
                                //byte? hour = drJob.Field<byte?>("Hour");
                                //byte? min = drJob.Field<byte?>("Minute");
                                //byte? dow = drJob.Field<byte?>("DayOfWeek");
                                ////DateTime? next = RO.Common3.Utils.GetNextRun(year, month, day, dow, hour, min);
                                //DateTime? next = GetNextRun(DateTime.Now,year, month, day, dow, hour, min);
                                //(new AdminSystem()).UpdCronJob((int)drJob["CronJobId"], DateTime.Now,next, connStr, dr["dbAppPassword"].ToString());
                            }
                        }
                    }
                    catch (Exception er) 
                    {
                        try
                        {
                            string errorMsg = string.Format("Cronjob invoke error");
                            ErrorTrace(new Exception(errorMsg, er), "error", requestInfo);
                        }
                        catch { }

                        //var d = new Dictionary<string, string>(){
                        //                                {"CronJobId", null},
                        //                                {"JobLink", null},
                        //                                {"Error",string.Format("Cronjob invoke error {0} {1}", er.Message, er.StackTrace)},
                        //                            };
                        //errorList.Add(new Tuple<Exception, Dictionary<string, string>>(er, d));
                    }
                    
                    DateTime nextMin = currentTime().AddMinutes(2);
                    if (jobsRun.Count > 0 && !string.IsNullOrEmpty(admEmail)) 
                    {
                        RO.Common3.Data.Credential cr = new RO.Common3.Data.Credential("Anonymous", "Anonymous");
                        RO.Common3.Data.LoginUsr usr = (new LoginSystem()).GetLoginSecure(cr);
                        string nonAnonymousWarning = usr == null ? "WARNING: This system does not have Anonymous Login" : "";
                        // Asynchronous send email for faster performance:
                        Action a = () =>
                        {
                            if (!summNotify)
                            {
                                summNotify = true;
                                base.SendEmail("background job on " + myUrl.ToString(), "Summary (" + Application.GetHashCode().ToString() + "): Checked on " + DateTime.Now.ToUniversalTime().ToString() + " UTC; Next check on " + (perMinute ? nextMin : nextCheck).ToString() + " UTC <br/><br/>" + string.Join("<br/>", jobsRun.ToArray()) + "<br/><br/>" + nonAnonymousWarning, admEmail, admEmail, admEmail, "Admin", true);
                            }
                        };
                        a.BeginInvoke(null, null);
                    }
                    if (nextCheck > currentTime())
                    {
                        if (perMinute)
                        {
                            if (currentTime() < nextMin) Thread.Sleep(nextMin.Subtract(DateTime.Now.ToUniversalTime()));
                        }
                        else
                        {
                            TimeSpan waitInterval = nextCheck.Subtract(DateTime.Now.ToUniversalTime());
                            Thread.Sleep(waitInterval);
                        }
                    }
                    string myHash = Convert.ToBase64String(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(Config.DesPassword)));
                    WebRequest wr = HttpWebRequest.Create(myUrl + "?hash=" + HttpUtility.UrlEncode(myHash));
                    lock (SystemList)
                    {
                        running = false;
                    }
                    try
                    {
                        HttpWebResponse resp = (HttpWebResponse) wr.GetResponse();
                        if (((int)resp.StatusCode) <= 300)
                        {
                            // trumpline, i.e. we kill ourself and like the next invoke take over
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            string errorMsg = string.Format("Cronjob refresh error {0}", myUrl);
                            ErrorTrace(new Exception(errorMsg, ex), "error", requestInfo);
                        }
                        catch { }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception e)
            {
                try
                {
                    string errorMsg = string.Format("Cronjob daemon error {0}", myUrl);
                    ErrorTrace(new Exception(errorMsg, e), "error", requestInfo);
                }
                catch { }
            }
            finally 
            {
                lock (SystemList)
                {
                    running = false;
                }
            }
        };
        jobTask.BeginInvoke(null,null);
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string myHash = Convert.ToBase64String(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(Config.DesPassword)));
            string hash = HttpUtility.HtmlDecode(Request.QueryString["hash"] ?? "");
            if (hash != myHash)
            {
                throw new Exception("cronjob hash signature not match");
                //return;
            }
        }

        RunJobs();
    }
}