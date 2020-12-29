namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
	public abstract class LoginAccessBase : Encryption, IDisposable
	{
        protected static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        protected static string randomByteString(int count)
        {
            byte[] r = new byte[count <= 0 ? 1 : count];
            rngCsp.GetBytes(r);
            return Convert.ToBase64String(r);
        }
        public abstract void CancelUserAccount(int UsrId);
        public abstract bool ChkAdminLogin(string RowAuths);
        public abstract bool ChkLoginStatus(string LoginName);
        public abstract void Dispose();
        public abstract string GetAppVersion(string dbConnectionString, string dbPassword);
        public abstract DataTable GetCompanyList(string Usrs, string RowAuthoritys, string Companys);
        public abstract string GetHintAnswer(string UsrId);
        public abstract DataTable GetHintQuestion();
        public abstract string GetHintQuestionId(string UsrId);
        public abstract DataTable GetLinkedUserLogin(int UsrId);
        public abstract LoginUsr GetLoginLegacy(string LoginName, string Password);
        public abstract DataTable GetLogins(string LoginName, string Provider, string UsrId = null);
        public abstract LoginUsr GetLoginSecure(Credential cr);
        public abstract DataTable GetProjectList(string Usrs, string RowAuthoritys, string Projects, string currCompanyId);
        public abstract string GetPwdExpMsg(string UsrId, string CultureId, string PwdExpDays);
        public abstract string GetRbtVersion();
        public abstract DataTable GetSaltedUserInfo(int UsrId, string LoginName, string UsrEmail);
        public abstract DataTable GetSystemsList(string dbConnectionString, string dbPassword);
        public abstract UsrImpr GetUsrImpr(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId);
        public abstract DataTable GetUsrImprNext(Int32 usrId);
        public abstract UsrPref GetUsrPref(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId);
        public abstract bool IsNullLegacyPwd(string LoginName);
        public abstract bool IsUsrSafeIP(int UsrId, string IpAddress);
        public abstract void LinkUserLogin(int UsrId, string ProviderCd, string LoginName, string LoginMeta = null);
        public abstract void SetLoginStatus(string LoginName, bool bLoginSuccess, string IpAddress, string Provider, string ProviderLoginName);
        public abstract void SetUsrSafeIP(int UsrId, string IpAddress);
        public abstract void UnlinkUserLogin(int UsrId, string ProviderCd, string LoginName, string LoginMeta = null);
        public abstract bool UpdHintQuestion(string UsrId, string HintQuestionId, string HintAnswer);
        public abstract void UpdUserLoginInfo(int UsrId, string LoginName, string UsrName, string UsrEmail);
        public abstract bool UpdUsrPassword(Credential cr, LoginUsr LUser, bool RemoveLink);
        public abstract DataTable WrAddUsr(string LoginName, string UsrName, string UsrPassword, int CultureId, int DefSystemId, string UsrEmail, string UsrGroups, bool ForcePwdChg, int? CustomerId, int? BrokerId, int? VendorId, bool Active, string SSOProviderCd, string SSOLoginName);
        public abstract void WrDelUsr(int UsrId);
        public abstract string WrGetUsrOTPSecret(int UsrId, string hostSecret = null);
        public abstract string WrSetUsrOTPSecret(int UsrId, bool bEnable, string hostSecret = null);
        public abstract DataTable GetUsrNotificationChannel(int UsrId, string FilterXml = null);
        public abstract void UpdUsrNotificationChannel(int UsrId, string DeviceId, string UserAgent, string ClientIP, string fingerprint, string appSig, string NotificationType);
    }
}