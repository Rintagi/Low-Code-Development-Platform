using System;
using System.Web;
using System.ComponentModel;
using System.Text;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Security.Principal;
using RO.Common3;

[Serializable]
public class WsCredential : IReportServerCredentials
{
	bool IReportServerCredentials.GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
	{
		userName = string.Empty; password = string.Empty; authority = string.Empty;	authCookie = new Cookie();
		return false;
	}

	WindowsIdentity IReportServerCredentials.ImpersonationUser
	{
		get { return null; }
	}

    // Needed by SqlReportModule.ascx.cs:
	System.Net.ICredentials IReportServerCredentials.NetworkCredentials
	{
		get { return new NetworkCredential(Config.WsRptUserName, Config.WsRptPassword, Config.WsRptDomain); }
	}
}
