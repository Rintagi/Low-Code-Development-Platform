namespace RO.Facade3
{
	using System;
	using System.Data;
    using RO.Access3;
    using RO.Common3;
	using RO.Common3.Data;

	public class LoginSystem : MarshalByRefObject
	{
		private LoginAccessBase GetLoginAccess(int CommandTimeout = 1800)
		{
			if ((Config.DesProvider  ?? "").ToLower() != "odbc")
			{
				return new LoginAccess();
			}
			else
			{
				return new RO.Access3.Odbc.LoginAccess();
			}
		}

		public bool IsUsrSafeIP(int UsrId, string IpAddress)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                return dac.IsUsrSafeIP(UsrId, IpAddress);
            }
        }

        public void SetUsrSafeIP(int UsrId, string IpAddress)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                dac.SetUsrSafeIP(UsrId, IpAddress);
            }
        }

        public bool IsNullLegacyPwd(string LoginName)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.IsNullLegacyPwd(LoginName);
			}
		}

		public bool ChkAdminLogin(string RowAuths)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.ChkAdminLogin(RowAuths);
			}
		}

		public bool ChkLoginStatus(string LoginName)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.ChkLoginStatus(LoginName);
			}
		}

        public void SetLoginStatus(string LoginName, bool bLoginSuccess, string IpAddress, string Provider, string ProviderLoginName)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                dac.SetLoginStatus(LoginName, bLoginSuccess, IpAddress, Provider, ProviderLoginName);
            }
        }

        public DataTable GetLogins(string LoginName, string Provider)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                return dac.GetLogins(LoginName, Provider);
            }
        }

		public LoginUsr GetLoginSecure(Credential cr)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetLoginSecure(cr);
			}
		}

		public LoginUsr GetLoginLegacy(string LoginName, string Password)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetLoginLegacy(LoginName, Password);
			}
		}

        public void UpdUserLoginInfo(int UsrId, string LoginName, string UsrName, string UsrEmail)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                dac.UpdUserLoginInfo(UsrId, LoginName, UsrName, UsrEmail);
            }
        }

        public void CancelUserAccount(int UsrId)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                dac.CancelUserAccount(UsrId);
            }
        }

        public DataTable GetSaltedUserInfo(int UsrId, string LoginName, string UsrEmail)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                return dac.GetSaltedUserInfo(UsrId, LoginName, UsrEmail);
            }
        }

		public UsrPref GetUsrPref(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetUsrPref(UsrId, CompanyId, ProjectId, SystemId);
			}
		}

		public UsrImpr GetUsrImpr(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetUsrImpr(UsrId, CompanyId, ProjectId, SystemId);
			}
		}

		public DataTable GetUsrImprNext(Int32 usrId)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetUsrImprNext(usrId);
			}
		}

		public DataTable GetCompanyList(string Usrs, string RowAuthoritys, string Companys)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetCompanyList(Usrs, RowAuthoritys, Companys);
			}
		}

		public DataTable GetProjectList(string Usrs, string RowAuthoritys, string Projects, string currCompanyId)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetProjectList(Usrs, RowAuthoritys, Projects, currCompanyId);
			}
		}

		public DataTable GetSystemsList(string dbConnectionString, string dbPassword)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetSystemsList(dbConnectionString, dbPassword);
			}
		}

        public bool UpdUsrPassword(Credential cr, LoginUsr LUser, bool RemoveLink)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                return dac.UpdUsrPassword(cr, LUser, RemoveLink);
            }
        }

		public string GetPwdExpMsg(string UsrId, String CultureId, string PwdExpDays)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetPwdExpMsg(UsrId, CultureId, PwdExpDays);
			}
		}

		public string GetHintAnswer(string UsrId)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetHintAnswer(UsrId);
			}
		}

		public string GetHintQuestionId(string UsrId)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetHintQuestionId(UsrId);
			}
		}

		public DataTable GetHintQuestion()
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetHintQuestion();
			}
		}

		public bool UpdHintQuestion(string UsrId, string HintQuestionId, string HintAnswer)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.UpdHintQuestion(UsrId, HintQuestionId, HintAnswer);
			}
		}

		public string GetRbtVersion()
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetRbtVersion();
			}
		}

		public string GetAppVersion(string dbConnectionString, string dbPassword)
		{
			using (LoginAccessBase dac = GetLoginAccess())
			{
				return dac.GetAppVersion(dbConnectionString, dbPassword);
			}
		}

        public void LinkUserLogin(int UsrId, string ProviderCd, string LoginName)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                dac.LinkUserLogin(UsrId, ProviderCd, LoginName);
            }
        }
        public void UnlinkUserLogin(int UsrId, string ProviderCd, string LoginName)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                dac.UnlinkUserLogin(UsrId, ProviderCd, LoginName);
            }
        }
        public DataTable GetLinkedUserLogin(int UsrId)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                return dac.GetLinkedUserLogin(UsrId);
            }
        }

        public DataTable WrAddUsr(string LoginName, string UsrName, string UsrPassword, int CultureId, int DefSystemId, string UsrEmail, string UsrGroups, bool ForcePwdChg, int? CustomerId, int? BrokerId, int? VendorId, bool Active, string SSOProviderCd, string SSOLoginName)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                return dac.WrAddUsr(LoginName, UsrName, UsrPassword, CultureId, DefSystemId, UsrEmail, UsrGroups, ForcePwdChg, CustomerId, BrokerId, VendorId, Active, SSOProviderCd, SSOLoginName);
            }
        }

        public void WrDelUsr(int UsrId)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                dac.WrDelUsr(UsrId);
            }
        }

        public string WrGetUsrOTPSecret(int UsrId, string hostSecret = null)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                return dac.WrGetUsrOTPSecret(UsrId, hostSecret);
            }
        }

        public string WrSetUsrOTPSecret(int UsrId, bool bEnable, string hostSecret = null)
        {
            using (LoginAccessBase dac = GetLoginAccess())
            {
                return dac.WrSetUsrOTPSecret(UsrId, bEnable, hostSecret);
            }
        }
    }
}