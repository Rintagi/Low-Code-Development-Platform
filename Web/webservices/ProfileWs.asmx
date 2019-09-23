<%@ WebService Language="C#" Class="ProfileWs" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.Rule3;
using RO.Web;
using System.Xml;
using System.Collections;
using System.IO;
using System.Text;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Linq;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Net;

// Need to run the following 3 lines to generate AdminWs.cs for C# calls at Windows SDK v6.1: CMD manually if AdminWs.asmx is changed:
// C:\
// CD\Rintagi\RO\Service3
// "C:\Program Files\Microsoft SDKs\Windows\v6.1\Bin\wsdl.exe" /nologo /namespace:RO.Service3 /out:"AdminWs.cs" "http://RND08/ROWs/AuthWs.asmx"

[ScriptService()]
[WebService(Namespace = "http://Rintagi.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class ProfileWs : AsmxBase
{
    const byte systemId = 3;
    const int screenId = 0;
    const string programName = "Profile";

    protected override byte GetSystemId() { return systemId; }
    protected override int GetScreenId() { return screenId; }
    protected override string GetProgramName() { return programName; }
    protected override string GetValidateMstIdSPName() { throw new NotImplementedException(); }
    protected override string GetMstTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetDtlTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetMstKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
    protected override string GetDtlKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }   
    protected override DataTable _GetMstById(string pid)
    {
        throw new NotImplementedException();
    }
    protected override DataTable _GetDtlById(string pid, int screenFilterId)
    {
        throw new NotImplementedException();
    }
    protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
    {
        throw new NotImplementedException();
    }
    protected override SerializableDictionary<string, string> InitDtl()
    {
        throw new NotImplementedException();
    }
    protected override SerializableDictionary<string, string> InitMaster()
    {
        throw new NotImplementedException();
    }        
    public ProfileWs()
        : base()
    {
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, object> GetProfileInfo()
    {
        Func<ApiResponse<SerializableDictionary<string, string>, object>> fn = () =>
        {
            SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId, false, false, false);
            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();
            SerializableDictionary<string, string> profileInfo = new SerializableDictionary<string, string>();

            if (LUser != null)
            {
                DataTable dt = (new AdminSystem()).GetMstById("GetAdmUsr1ById", LUser.UsrId.ToString(), null, null);
                DataRow dr = dt.Rows[0];

                profileInfo["LoginName"] = dr["LoginName1"].ToString();
                profileInfo["UsrName"] = dr["UsrName1"].ToString();
                profileInfo["UsrEmail"] = dr["UsrEmail1"].ToString();
            }

            mr.status = "success";
            mr.errorMsg = "";
            mr.data = profileInfo;
            return mr;
        };
        var result = ProtectedCall(fn);
        return result;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<bool, object> UpdateProfile(string NewLoginName, string NewUsrName, string NewUsrEmail)
    {
        Func<ApiResponse<bool, object>> fn = () =>
        {
            SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId, false, false, false);
            ApiResponse<bool, object> mr = new ApiResponse<bool, object>();

            if (LUser != null/* && LUser.LoginName.ToLower() != "anonymous"*/)
            {
                DataTable dt = (new AdminSystem()).GetMstById("GetAdmUsr1ById", LUser.UsrId.ToString(), null, null);

                if (dt.Rows.Count == 1 && dt.Rows[0]["LoginName1"].ToString() != "anonymous")
                {
                    string oldEmail = LUser.UsrEmail;
                    (new LoginSystem()).UpdUserLoginInfo(LUser.UsrId, NewLoginName, NewUsrName, NewUsrEmail);
                    //cMsg.Text = TranslateItem(GetLabels().Rows, "LoginInfoChangedMsg");
                    LUser.LoginName = NewLoginName;
                    LUser.UsrEmail = NewUsrEmail;
                    LUser.UsrName = NewUsrName;

                    mr.status = "success";
                    mr.data = true;

                    try
                    {
                        string from = base.SysCustServEmail(3);
                        string subject = TranslateItem(GetLabels().Rows, "ProfileChangedEmailSubject");
                        string body = TranslateItem(GetLabels().Rows, "ProfileChangedEmailBody");
                        //string site = Request.Url.Scheme + "://" + Request.Url.Host + "/" + Request.ApplicationPath + " (" + Config.WebTitle + ")";
                        string site = GetSiteUrl();

                        if (!string.IsNullOrEmpty(oldEmail))
                        {
                            NotifyUser(subject, body, oldEmail, from);
                        }
                        else if (LUser.TwoFactorAuth)
                        {
                            string sysadminEmail = base.SysAdminEmail(3);
                            string admBody = string.Format(TranslateItem(GetLabels().Rows, "ProfileChangedEmailBodyForAdmin"), LUser.LoginName);
                            NotifyUser(subject, body, sysadminEmail, from);
                        }
                    }
                    catch { }
                }
                else
                {
                    mr.status = "failed";
                    mr.errorMsg = "Error updating profile";
                    mr.data = false;
                }
            }
            else
            {
                mr.status = "failed";
                mr.errorMsg = "Error updating profile";
                mr.data = false;
            }

            return mr;
        };
        var result = ProtectedCall(fn);
        return result;
    }

    protected string ResetRequestLoginName(string j, string p)
    {
        try
        {
            if (!string.IsNullOrEmpty(j))
            {
                string usrId = j;
                var usr = ValidateResetUrl(j, p);
                if (usr.Value)
                {
                    return usr.Key;
                }
                else
                {
                    return null;
                }
            }
        }
        catch { return null; }
        return null;
    }

    protected KeyValuePair<string, bool> ValidateResetUrl(string UsrId, string p)
    {
        DataTable dt = (new LoginSystem()).GetSaltedUserInfo(int.Parse(UsrId), "", "");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            string loginName = dr["LoginName"].ToString();
            string userEmail = dr["UsrEmail"].ToString();
            byte[] x = Decrypt(Convert.FromBase64String(p), dr["UsrPassword"] as byte[], dr["UsrPassword"] as byte[]);
            string y = new string(System.Text.Encoding.ASCII.GetChars(x));
            long resetTime = long.Parse(y);
            if (resetTime < DateTime.Now.AddMinutes(-60).ToFileTimeUtc())
            {
                return new KeyValuePair<string, bool>(loginName, false);
            }
            return new KeyValuePair<string, bool>(loginName, true);
        }
        return new KeyValuePair<string, bool>("", false);
    }


    [WebMethod(EnableSession = false)]
    public ApiResponse<bool, object> UpdUsrPwd(string j, string p, string NewUsrPassword, string ConfirmPwd)
    {
        Func<ApiResponse<bool, object>> fn = () =>
        {
            ApiResponse<bool, object> mr = new ApiResponse<bool, object>();

            if (ConfirmPwd.Trim() != NewUsrPassword.Trim())
            {
                mr.status = "failed";
                mr.errorMsg = "Please match your passwords and try again!";
                mr.data = false;
                return mr;
            }

            foreach (var re in Config.PasswordComplexity.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!new System.Text.RegularExpressions.Regex("^.*" + re.Trim() + ".*$").IsMatch(NewUsrPassword.Trim()))
                {
                    mr.status = "failed";
                    mr.data = false;
                    if (string.IsNullOrEmpty(Config.PasswordHelpMsg))
                    {
                        mr.errorMsg = "Password chosen is too simple, try again";
                    }
                    else
                    {
                        mr.errorMsg = Config.PasswordHelpMsg;
                    }

                    return mr;
                }
            }

            string resetLoginName = string.Empty;

            if (LUser != null)
            {
                DataTable dt = (new AdminSystem()).GetMstById("GetAdmUsr1ById", LUser.UsrId.ToString(), null, null);

                resetLoginName = dt.Rows[0]["LoginName1"].ToString();
                LUser.LoginName = dt.Rows[0]["LoginName1"].ToString();
            }
            else
            {
                resetLoginName = ResetRequestLoginName(j, p);
            }

            if (!string.IsNullOrEmpty(resetLoginName))
            {
                Credential cr = new Credential(resetLoginName, NewUsrPassword.Trim());

                if ((new LoginSystem()).UpdUsrPassword(cr, LUser, false))
                {
                    mr.status = "success";
                    mr.data = true;

                    try
                    {
                        string from = base.SysCustServEmail(3);
                        string subject = TranslateItem(GetLabels().Rows, "PasswordChangedEmailSubject");
                        string body = TranslateItem(GetLabels().Rows, "PasswordChangedEmailBody");
                        if (!string.IsNullOrEmpty(LUser.UsrEmail))
                        {
                            NotifyUser(subject, body, LUser.UsrEmail, from);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                mr.status = "failed";
                mr.errorMsg = "Invalid Reset Request";
                mr.data = false;
                return mr;
            }

            return mr;
        };

        bool resetRequest = !string.IsNullOrEmpty(j) && !string.IsNullOrEmpty(p);

        if (resetRequest)
        {
            return ManagedApiCall(fn);
        }
        else
        {
            return ProtectedCall(fn, resetRequest);
        }
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<bool, string> ResetPwd(string ResetLoginName, string ResetUsrEmail)
    {
        //SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId, false, false, false);
        ApiResponse<bool, string> mr = new ApiResponse<bool, string>();

        DataTable dt = (new LoginSystem()).GetSaltedUserInfo(0, ResetLoginName, ResetUsrEmail);
        DataTable dtLabel = GetLabels();
        /* it is possible that there is multiple user login returned
        * 2013.12.24 gary
         */
        List<string> popMsg = new List<string>();
        foreach (DataRow dr in dt.Rows)
        {
            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["UsrEmail"].ToString()))
            {
                string loginName = dr["LoginName"].ToString();
                string userEmail = dr["UsrEmail"].ToString();
                var reset_url = GetResetLoginUrl(dr["UsrId"].ToString(), "", "", "", "", null, null);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(TranslateItem(dtLabel.Rows, "ResetPwdEmailMsg1"));
                sb.Append("<br /><strong><a href=\"").Append(reset_url.Value).Append("\">").Append(reset_url.Value).Append("</a></strong><br /><br />");
                sb.Append("<strong>").Append(loginName).Append("</strong>&nbsp;");
                sb.Append(TranslateItem(dtLabel.Rows, "ResetPwdEmailMsg2"));
                //string host = Request.Url.Host;
                string host = "http://fintruxdev/rc";
                try
                {
                    string from = base.SysCustServEmail(3);
                    base.SendEmail(string.Format(TranslateItem(dtLabel.Rows, "ResetPwdEmailSubject"), host, Config.WebTitle), sb.ToString(), userEmail, from, from, Config.WebTitle + " Customer Care", true);
                    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("^(.)([^\\@]*)(@)(.*)$");
                    var shieldedEmail = re.Replace(userEmail, (m) => m.Groups[1].Captures[0] + "xxxxxxx" + "@" + m.Groups[4]);
                    popMsg.Add(string.Format(TranslateItem(dtLabel.Rows, "ResetEmailSentMsg"), shieldedEmail));

                }
                catch (Exception err)
                {
                    mr.status = "failed";
                    mr.data = false;
                    mr.errorMsg = err.Message.Replace(Environment.NewLine, "<br/>");
                    return mr;
                }
            }
            else
            {
                // we are here because login name is used for reset request
                mr.status = "failed";
                mr.data = false;
                mr.errorMsg = TranslateItem(dtLabel.Rows, "NoEmailOnFileMsg");
                return mr;
            }
        }
        if (popMsg.Count > 0)
        {
            mr.status = "success";
            mr.data = true;
            mr.supportingData = string.Join("\n", popMsg.ToArray());
            return mr;
        }

        return mr;
    }

    protected KeyValuePair<string, string> GetResetLoginUrl(string UsrId, string LoginName, string Email, string keyAs, string userState, string signUpURL, string returnUrl)
    {
        DataRow dr = ((new LoginSystem()).GetSaltedUserInfo(int.Parse(UsrId), LoginName, Email)).Rows[0];
        string emailOnFile = dr["UsrEmail"].ToString();
        byte[] resetTime = System.Text.Encoding.ASCII.GetBytes(DateTime.Now.ToFileTimeUtc().ToString());
        string resetTimeEnc = Convert.ToBase64String(Encrypt(resetTime, dr["UsrPassword"] as byte[], dr["UsrPassword"] as byte[]));
        //string loginUrl = (System.Web.Security.FormsAuthentication.LoginUrl ?? "").Replace("/" + Config.AppNameSpace + "/", "");
        //if (string.IsNullOrEmpty(loginUrl)) loginUrl = "MyAccount.aspx";
        //string reset_url = string.Format("{0}" + (Request.Url.ToString().Contains("?") && !string.IsNullOrEmpty(signUpURL) ? "&" : "?") + "{3}={1}&p={2}{4}",
        //    ((Config.EnableSsl ? Config.SslUrl : Config.OrdUrl).StartsWith("http") ? (new Uri(Config.EnableSsl ? Config.SslUrl : Config.OrdUrl)) : Request.Url).GetLeftPart(UriPartial.Scheme) + Request.Url.Host + Request.Url.AbsolutePath.ToLower().Replace((signUpURL ?? loginUrl).ToLower(), loginUrl),
        //    HttpUtility.UrlEncode(dr["UsrId"].ToString()),
        //    HttpUtility.UrlEncode(resetTimeEnc),
        //    string.IsNullOrEmpty(keyAs) ? "j" : keyAs,
        //    userState
        //    );

        // for now
        string reset_url = string.Format("http://localhost:3000/#/newpassword?j={0}&p={1}", HttpUtility.UrlEncode(dr["UsrId"].ToString()), HttpUtility.UrlEncode(resetTimeEnc));
        return new KeyValuePair<string, string>(emailOnFile, reset_url + (string.IsNullOrEmpty(returnUrl) ? "" : "&ReturnUrl=" + Server.UrlEncode(returnUrl)));
    }

    protected void NotifyUser(string subject, string body, string email, string from)
    {
        string site = GetSiteUrl(true);
        SendEmail(subject + " " + site, body, email, from, "", Config.WebTitle + " Customer Care", false);
    }

    protected string GetSiteUrl(bool bIncludeTitle = false)
    {
#if false
        string site = Request.Url.Scheme + "://" + Request.Url.Host + Request.ApplicationPath + (bIncludeTitle ? " (" + Config.WebTitle + ")" : "");
        return site;
#endif
        string serverDomain = System.Configuration.ConfigurationManager.AppSettings["ERPMainSite"] ?? (Config.EnableSsl ? @"https://" : @"http://") + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
        return serverDomain;
    }

    private DataTable GetLabels()
    {
        DataTable dtLabel = (new AdminSystem()).GetLabels(LUser != null && LUser.LoginName.ToLower() != "anonymous" ? LUser.CultureId : (short)1, "MyAccountModule", null, null, null);
        DataColumn[] pkey = new DataColumn[1];
        pkey[0] = dtLabel.Columns[0];
        dtLabel.PrimaryKey = pkey;
        return dtLabel;
    }
}