namespace RO.WebRules
{
	using System;
	using System.IO;
	using System.Data;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Net;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.ComponentModel;
	using RO.SystemFramewk;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.Access3;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Diagnostics;

	// Stock rules only. Written over by Rintagi on each deployment.
	public class WebRule : RO.Common3.Encryption
	{
		private WebAccessBase GetWebAccess(int CommandTimeout = 1800)
		{
			if ((Config.DesProvider  ?? "").ToLower() != "odbc")
			{
				return new WebAccess();
			}
			else
			{
				return new RO.Access3.Odbc.WebAccess();
			}
		}

		// Return false if email already exists as login name:
		public bool WrIsUniqueEmail(string UsrEmail)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrIsUniqueEmail(UsrEmail);
            }
        }

        // Add a table to capture uploads of documents:
        public DataTable WrAddDocTbl(byte SystemId, string TableName, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrAddDocTbl(SystemId, TableName, dbConnectionString, dbPassword);
            }
        }

        // Add a table to capture workflow status:
        public DataTable WrAddWfsTbl(byte SystemId, string TableName, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrAddWfsTbl(SystemId, TableName, dbConnectionString, dbPassword);
            }
        }

        // Get a list of active user emails.
        public DataTable WrGetActiveEmails(string MaintMsgId)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrGetActiveEmails(MaintMsgId);
            }
        }

        // Get email and other info for a specific user.
        public DataTable WrGetUsrEmail(string UsrId)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrGetUsrEmail(UsrId);
            }
        }

		// Return default CultureId or CultureTypeName as a string.
		public string WrGetDefCulture(bool bCultureId)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetDefCulture(bCultureId);
			}
		}

		// Obtain table-valued function from the physical database for virtual table.
		public string WrGetVirtualTbl(string TableId, byte DbId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetVirtualTbl(TableId, DbId, dbConnectionString, dbPassword);
			}
		}

		// Return column details from physical database.
		public string WrSyncByDb(int UsrId, byte SystemId, byte DbId, string TableId, string TableName, string TableDesc, bool MultiDesignDb, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrSyncByDb(UsrId, SystemId, DbId, TableId, TableName, TableDesc, MultiDesignDb, dbConnectionString, dbPassword);
			}
		}

		// Return true if physical table has been synchronized successfully.
		public string WrSyncToDb(byte SystemId, string TableId, string TableName, bool MultiDesignDb, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrSyncToDb(SystemId,TableId,TableName,MultiDesignDb,dbConnectionString,dbPassword);
			}
		}

		// Obtain stored procedure from the physical database for Custom Content.
		public string WrGetCustomSp(string CustomDtlId, byte DbId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetCustomSp(CustomDtlId, DbId, dbConnectionString, dbPassword);
			}
		}

		// Obtain stored procedure from the physical database for Server Rule.
		public string WrGetSvrRule(string ServerRuleId, byte DbId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetSvrRule(ServerRuleId, DbId, dbConnectionString, dbPassword);
			}
		}

        // Return databases information for Data Table.
        public DataTable WrGetDbTableSys(string TableId, byte DbId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrGetDbTableSys(TableId, DbId, dbConnectionString, dbPassword);
            }
        }

		// Return databases affected for synchronization of this stored procedure to physical database.
		public DataTable WrGetSvrRuleSys(string ScreenId, byte DbId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetSvrRuleSys(ScreenId, DbId, dbConnectionString, dbPassword);
			}
		}

		// Return true if physical s.proc. has been synchronized successfully.
		public string WrSyncProc(string ProcedureName, string ProcCode, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrSyncProc(ProcedureName, ProcCode, dbConnectionString, dbPassword);
			}
		}

		// Return true if physical function has been synchronized successfully.
		public string WrSyncFunc(string FunctionName, string ProcCode, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrSyncFunc(FunctionName, ProcCode, dbConnectionString, dbPassword);
			}
		}

		// Update last synchronization date to the physical database.
		public string WrUpdSvrRule(string ServerRuleId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrUpdSvrRule(ServerRuleId, dbConnectionString, dbPassword);
			}
		}

		// Return databases affected for synchronization of this stored procedure to physical database.
		public DataTable WrGetReportApp(byte DbId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetReportApp(DbId, dbConnectionString, dbPassword);
			}
		}

		// Obtain stored procedure from the physical database for Server Rule.
		public string WrGetRptProc(string ProcName, byte DbId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetRptProc(ProcName, DbId, dbConnectionString, dbPassword);
			}
		}

		// Update last synchronization date to the physical database.
		public string WrUpdRptProc(string ReportId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrUpdRptProc(ReportId, dbConnectionString, dbPassword);
			}
		}

		// Translate all items from default culture to the specified culture.
        // Every 1 second perform translation on a random number less than 10 as maximum number of items per batch.
		public bool WrTranslateAll(string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int MaxCnt = (new Random()).Next(100) + 1;	// Number of total translations per call to avoid cut off.
			int cnt = 0;
			string DefCulture = WrGetDefCulture(false);
			cnt = cnt + WrTrlCtButtonHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlButtonHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlMenuHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlMsgCenter(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlCultureLbl(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlReportHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlReportCriHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlScreenHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlScreenCriHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlScreenFilterHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlScreenObjHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
			if (cnt >= MaxCnt) { return true; }
			cnt = cnt + WrTrlScreenTabHlp(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
            if (cnt >= MaxCnt) { return true; }
            cnt = cnt + WrTrlLabel(MaxCnt, DefCulture, CultureId, CultureName, dbConnectionString, dbPassword);
            if (cnt > 0) { return true; } else { return false; }
        }

        public string WrGetTranslation(string CultureFr, string CultureId, string CultureTo, string InStr, string dbConnectionString, string dbPassword)
		{
			string OutStr = string.Empty;
            if (InStr != string.Empty)
			{
                using (WebAccessBase dac = GetWebAccess())
                {
                    OutStr = dac.WrGetMemTranslate(InStr, CultureId, dbConnectionString, dbPassword);
                }
                if (OutStr == string.Empty)
                {
                    string TrlCode;
                    // Translation by google ajax:
                    if (CultureTo == "zh-CN") { TrlCode = "en|zh-CN"; }
                    else
                    {
                        TrlCode = CultureFr.Substring(0, 2) + "|" + CultureTo.Substring(0, 2);
                    }
                    OutStr = WrGenericTranslate("GA", TrlCode, InStr, dbConnectionString, dbPassword).Trim();       // Expired Dec 2011.
                    if (OutStr == string.Empty)
                    {
                        OutStr = WrGenericTranslate("GB", TrlCode, InStr, dbConnectionString, dbPassword).Trim();   // Paid translation.
                    }
                    if (OutStr == string.Empty)
                    {
                        // Translation by google ordinary:
                        if (CultureTo == "zh-CN") { TrlCode = "sl=en&tl=zh-CN"; }
                        else
                        {
                            TrlCode = "sl=" + CultureFr.Substring(0, 2) + "&tl=" + CultureTo.Substring(0, 2);
                        }
                        OutStr = WrGenericTranslate("GG", TrlCode, InStr, dbConnectionString, dbPassword).Trim();     // Obsolete.
                    }
                    if (OutStr == string.Empty)
                    {
                        ApplicationAssert.CheckCondition(false, "WrGetTranslation", "", "\"" + InStr + "\" cannot be translated. Please investigate and try again.");
                    }
                    else
                    {
                        OutStr = OutStr.Replace("\\u0026", "&");
                        OutStr = OutStr.Replace("&#39;", "'");
                        OutStr = OutStr.Replace("&quot;", "\"");
                    }
                }
			}
			return OutStr;
		}

		private string WrGenericTranslate(string CrawlerCd, string TrlCode, string InStr, string dbConnectionString, string dbPassword)
		{
			HttpWebRequest request;
			string posttext;
			DataTable dt = null;
			string rtn = string.Empty;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetCtCrawler(CrawlerCd);
			}
			try
			{
				if (CrawlerCd == "GA")
				{
                    // Get:
                    //posttext = dt.Rows[0]["PreText"].ToString() + TrlCode + dt.Rows[0]["PostText"].ToString() + HttpUtility.UrlEncode(InStr.Replace("\\", "/"));
                    //request = (HttpWebRequest)WebRequest.Create(new Uri(dt.Rows[0]["CrawlerURL"].ToString() + posttext));
                    return string.Empty;
				}
                else if (CrawlerCd == "GB")
                {
                    // Get google translation paid api v2:
                    string apiKey = base.DecryptString(Config.GoogleAPIKey);
                    string[] fromTo = TrlCode.Split(new char[] { '|' });
                    posttext = dt.Rows[0]["PreText"].ToString() + apiKey + string.Format("&source={0}&target={1}", fromTo[0], fromTo[1]) + dt.Rows[0]["PostText"].ToString() + HttpUtility.UrlEncode(InStr.Replace("\\", "/"));
                    request = (HttpWebRequest)WebRequest.Create(new Uri(dt.Rows[0]["CrawlerURL"].ToString() + posttext));
                }
                else
				{
					// Post:
					request = (HttpWebRequest)WebRequest.Create(new Uri(dt.Rows[0]["CrawlerURL"].ToString()));
					posttext = dt.Rows[0]["PreText"].ToString() + TrlCode + dt.Rows[0]["PostText"].ToString() + HttpUtility.UrlEncode(InStr.Replace("\\", "/"));
					request.Method = "POST";
					request.ContentType = "application/x-www-form-urlencoded";
					request.ContentLength = posttext.Length;
					request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
					Stream writeStream = request.GetRequestStream();
					UTF8Encoding encoding = new UTF8Encoding();
					byte[] bytes = encoding.GetBytes(posttext);
					writeStream.Write(bytes, 0, bytes.Length);
					writeStream.Close();
				}
				request.Referer = dt.Rows[0]["CrawlerREF"].ToString();
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
				string page = readStream.ReadToEnd();
                Regex reg = new Regex(dt.Rows[0]["ResultRegex"].ToString(), RegexOptions.IgnoreCase | RegexOptions.Multiline);
                MatchCollection matches = reg.Matches(page);
				if (matches.Count == 1 && matches[0].Groups.Count == 2 && matches[0].Groups[1].Value != string.Empty)
				{
					rtn = matches[0].Groups[1].Value;
				}
			}
			catch (Exception err)
			{
				ApplicationAssert.CheckCondition(false, "WrGenericTranslate", dt.Rows[0]["CrawlerURL"].ToString() + ": " + err.Message, "Translation engine cannot handle \"" + InStr + "\". Please investigate and try again.");
			}
            if (CrawlerCd == "GA" || CrawlerCd == "GB")
            {
                // google translate API returns JSONEncode(htmlencoded(string)) and we need to reverse it
                var json_encoded = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<System.Collections.Generic.Dictionary<string, string>>(string.Format("{{\"translatedText\":\"{0}\"}}", rtn));
                rtn = System.Web.HttpUtility.HtmlDecode(json_encoded["translatedText"]);
            }
            return rtn;
		}

		// Translate CtButtonHlp table (??Design only):
		private int WrTrlCtButtonHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			if (dbConnectionString.IndexOf("Design") >= 0)
			{
				DataTable dt;
				using (WebAccessBase dac = GetWebAccess())
				{
					dt = dac.WrGetCtButtonHlp(CultureId, dbConnectionString, dbPassword);
				}
				foreach (DataRow dr in dt.Rows)
				{
                    dr["ButtonName"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ButtonName"].ToString().Trim(), dbConnectionString, dbPassword);
                    dr["ButtonLongNm"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ButtonLongNm"].ToString().Trim(), dbConnectionString, dbPassword);
                    dr["ButtonToolTip"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ButtonToolTip"].ToString().Trim(), dbConnectionString, dbPassword);
					using (WebAccessBase dac = GetWebAccess())
					{
						dac.WrInsCtButtonHlp(dr, CultureId, dbConnectionString, dbPassword);
					}
					cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
				}
			}
			return cnt;
		}

		// Translate ButtonHlp table:
		private int WrTrlButtonHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetButtonHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["ButtonName"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ButtonName"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["ButtonLongNm"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ButtonLongNm"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["ButtonToolTip"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ButtonToolTip"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsButtonHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

		// Translate MenuHlp table:
		private int WrTrlMenuHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetMenuHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["MenuText"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["MenuText"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsMenuHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

		// Translate MsgCenter table:
		private int WrTrlMsgCenter(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetMsgCenter(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["Msg"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["Msg"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsMsgCenter(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

        // Translate ??CultureLbl table:
		private int WrTrlCultureLbl(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			if (dbConnectionString.IndexOf("Design") >= 0)
			{
				DataTable dt;
				using (WebAccessBase dac = GetWebAccess())
				{
					dt = dac.WrGetCultureLbl(CultureId);
				}
				foreach (DataRow dr in dt.Rows)
				{
                    dr["Label"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["Label"].ToString().Trim(), dbConnectionString, dbPassword);
					using (WebAccessBase dac = GetWebAccess())
					{
						dac.WrInsCultureLbl(dr, CultureId);
					}
					cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
				}
			}
			return cnt;
		}

		// Translate ReportHlp table:
		private int WrTrlReportHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetReportHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["DefaultHlpMsg"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["DefaultHlpMsg"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["ReportTitle"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ReportTitle"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsReportHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

		// Translate ReportCriHlp table:
		private int WrTrlReportCriHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetReportCriHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["ColumnHeader"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ColumnHeader"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsReportCriHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

		// Translate ScreenHlp table:
		private int WrTrlScreenHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetScreenHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["DefaultHlpMsg"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["DefaultHlpMsg"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["FootNote"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["FootNote"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["ScreenTitle"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ScreenTitle"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["AddMsg"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["AddMsg"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["UpdMsg"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["UpdMsg"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["DelMsg"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["DelMsg"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsScreenHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

		// Translate ScreenCriHlp table:
		private int WrTrlScreenCriHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetScreenCriHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["ColumnHeader"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ColumnHeader"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsScreenCriHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

		// Translate ScreenFilterHlp table:
		private int WrTrlScreenFilterHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetScreenFilterHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["FilterName"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["FilterName"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsScreenFilterHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

		// Translate ScreenObjHlp table:
		private int WrTrlScreenObjHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetScreenObjHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
				if (",28,36,37,".IndexOf(","+dr["DisplayModeId"].ToString()+",") < 0 )	// Not Image, Placeholder or Html Editor;
				{
                    dr["ColumnHeader"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ColumnHeader"].ToString().Trim(), dbConnectionString, dbPassword);
				}
                dr["TbHint"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["TbHint"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["ToolTip"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ToolTip"].ToString().Trim(), dbConnectionString, dbPassword);
                dr["ErrMessage"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["ErrMessage"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsScreenObjHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

		// Translate ScreenTabHlp table:
		private int WrTrlScreenTabHlp(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
		{
			int cnt = 0;
			DataTable dt;
			using (WebAccessBase dac = GetWebAccess())
			{
				dt = dac.WrGetScreenTabHlp(CultureId, dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
                dr["TabFolderName"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["TabFolderName"].ToString().Trim(), dbConnectionString, dbPassword);
				using (WebAccessBase dac = GetWebAccess())
				{
					dac.WrInsScreenTabHlp(dr, CultureId, dbConnectionString, dbPassword);
				}
				cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
			}
			return cnt;
		}

        // Translate Label table:
        private int WrTrlLabel(int MaxCnt, string DefCulture, string CultureId, string CultureName, string dbConnectionString, string dbPassword)
        {
            int cnt = 0;
            DataTable dt;
            using (WebAccessBase dac = GetWebAccess())
            {
                dt = dac.WrGetLabel(CultureId, dbConnectionString, dbPassword);
            }
            foreach (DataRow dr in dt.Rows)
            {
                dr["LabelText"] = WrGetTranslation(DefCulture, CultureId, CultureName, dr["LabelText"].ToString().Trim(), dbConnectionString, dbPassword);
                using (WebAccessBase dac = GetWebAccess())
                {
                    dac.WrInsLabel(dr, CultureId, dbConnectionString, dbPassword);
                }
                cnt = cnt + 1; if (cnt >= MaxCnt) { break; }
            }
            return cnt;
        }

		// Generate or synchronize report template based on report wizard.
		public string WrRptwizGen(Int32 RptwizId, string SystemId, string AppDatabase, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrRptwizGen(RptwizId, SystemId, AppDatabase, dbConnectionString, dbPassword);
			}
		}

		// Delete report template based on report wizard.
		public bool WrRptwizDel(Int32 ReportId, string dbConnectionString, string dbPassword)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrRptwizDel(ReportId, dbConnectionString, dbPassword);
			}
		}

        // Create a copy of report template based on report wizard in advanced report definition.
        public bool WrXferRpt(Int32 ReportId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrXferRpt(ReportId, dbConnectionString, dbPassword);
            }
        }

		// Replace GetDdlPermId3S1751:
		public DataTable WrGetDdlPermId(string PermKeyId, Int32 ScreenId, Int32 TableId, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetDdlPermId(PermKeyId, ScreenId, TableId, bAll, keyId, dbConnectionString, dbPassword, ui, uc);
			}
		}

		public DataTable WrGetAdmMenuPerm(Int32 screenId, string keyId58, string dbConnectionString, string dbPassword, Int32 screenFilterId, UsrImpr ui, UsrCurr uc)
		{
			using (WebAccessBase dac = GetWebAccess())
			{
				return dac.WrGetAdmMenuPerm(screenId, keyId58, dbConnectionString, dbPassword, screenFilterId, ui, uc);
			}
		}

        public Int32 CountEmailsSent()
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.CountEmailsSent();
            }
        }

        public void SendEmail(bool bSsl, int port, string server, string userName, string encPassword, string domain, System.Net.Mail.MailMessage mm)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(server);
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(encPassword))
            {
                client.Credentials = string.IsNullOrEmpty(domain) ? new NetworkCredential(userName, DecryptString(encPassword)) : new NetworkCredential(userName, DecryptString(encPassword), domain);
            }
            client.Port = port;
            client.EnableSsl = bSsl;
            client.Send(mm);
        }

        public Tuple<int, string, string> sshPush(string pscpPath, string localSource, string remoteTarget, string login, string sshKeyFile, string keyPassword, string hostKey = null)
        {
            string sshPort = "22";
            string password = DecryptString(keyPassword);
            string recursive = Directory.Exists(localSource) ? "-r" : "";
            string withHostKey = string.IsNullOrEmpty(hostKey) ? "" : (" -hostkey " + hostKey);
            string cmdArg = string.Format("-P {0} {1} {5} -q -i \"{2}\" \"{3}\" \"{4}\"", sshPort, recursive, sshKeyFile, localSource, remoteTarget, withHostKey);
            StringBuilder sbStdErr = new StringBuilder();
            StringBuilder sbStdOut = new StringBuilder();
            int wrongPasswordCnt = 0;

            Action<string, Process, StreamWriter, string> puttyPromptHandler = (v, proc, ws, src) =>
            {
                string x = v;
                if (v != null)
                {
                    if (src == "stdout" && !v.StartsWith("Passphrase for key "))
                    {
                        sbStdOut.Append(v);
                        sbStdOut.Append("\n");
                    }
                    else if (!v.Contains("Wrong passphrase"))
                    {
                        sbStdErr.Append(v);
                        sbStdErr.Append("\n");
                    }
                    string prompt = src == "stdout" ? sbStdOut.ToString() : sbStdErr.ToString();
                    if (v.StartsWith("Passphrase for key"))
                    {
                        ws.WriteLine(password);
                        wrongPasswordCnt += 1;
                        if (wrongPasswordCnt > 5)
                        {
                            sbStdErr.Append("Wrong Passphrase\n");
                            if (!proc.HasExited) proc.Kill();
                        }
                    }
                    else if (v.Contains("The server's host key is not"))
                    {
                        ws.WriteLine("n");
                        ws.WriteLine(password);
                        ws.WriteLine("");
                    }
                    else if (v.Contains("Access denied"))
                    {
                        if (!proc.HasExited) proc.Kill();
                    }
                    else if (v.Contains("Wrong passphrase"))
                    {
                        wrongPasswordCnt += 1;
                        if (wrongPasswordCnt > 3)
                        {
                            if (!proc.HasExited) proc.Kill();
                        }
                    }
                }
                //ws.WriteLine("");
            };
            Action<object, EventArgs> exitHandler = (v, ws) =>
            {
                var x = v;
                //ws.WriteLine("");
            };

            var result = RO.Common3.Utils.WinProcEx(pscpPath, localSource, null, cmdArg, puttyPromptHandler, puttyPromptHandler, exitHandler);
            return new Tuple<int, string, string>(result.Item1.ExitCode, sbStdOut.ToString(), sbStdErr.ToString());
        }
        public Tuple<int, string, string> sshRemoteCmd(string plinkPath, string localCmdFile, string remoteTarget, string login, string sshKeyFile, string keyPassword, string hostKey = null)
        {
            string sshPort = "22";
            string password = DecryptString(keyPassword);
            string cmdScript = string.Format("-m {0}", localCmdFile);
            string withHostKey = string.IsNullOrEmpty(hostKey) ? "" : (" -hostkey " + hostKey);
            string cmdArg = string.Format("-P {0} {1} {4} -i \"{2}\" \"{3}\"", sshPort, cmdScript, sshKeyFile, remoteTarget, withHostKey);
            StringBuilder sbStdErr = new StringBuilder();
            StringBuilder sbStdOut = new StringBuilder();
            bool sessionConnected = false;
            int wrongPasswordCnt = 0;

            Action<string, Process, StreamWriter, string> puttyPromptHandler = (v, proc, ws, src) =>
            {
                string x = v;
                if (v != null)
                {
                    if (src == "stdout" && (!v.StartsWith("Passphrase for key ") || sessionConnected))
                    {
                        sbStdOut.Append(v);
                        sbStdOut.Append("\n");
                    }
                    else if (!v.Contains("Wrong passphrase"))
                    {
                        sbStdErr.Append(v);
                        sbStdErr.Append("\n");
                    }
                    string prompt = src == "stdout" ? sbStdOut.ToString() : sbStdErr.ToString();
                    if (v.StartsWith("Passphrase for key"))
                    {
                        ws.WriteLine(password);
                        wrongPasswordCnt += 1;
                        if (wrongPasswordCnt > 3)
                        {
                            sbStdErr.Append("Wrong Passphrase\n");
                            if (!proc.HasExited) proc.Kill();
                        }

                    }
                    else if (v.Contains("The server's host key is not"))
                    {
                        ws.WriteLine("n");
                        ws.WriteLine(password);
                        ws.WriteLine("");
                    }
                    else if (v.Contains("Wrong passphrase"))
                    {
                        wrongPasswordCnt += 1;
                        if (wrongPasswordCnt > 5)
                        {
                            if (!proc.HasExited) proc.Kill();
                        }
                    }
                    else if (v.Contains("Access denied"))
                    {
                        if (!proc.HasExited) proc.Kill();
                    }
                    else if (v.Contains("Press Return to begin session") || v.Contains("Using username"))
                    {
                        ws.WriteLine("");
                        sessionConnected = true;
                    }
                }
                //ws.WriteLine("");
            };

            Action<object, EventArgs> exitHandler = (v, ws) =>
            {
                // if the script run on remote has exit code > 0, nothing are captured and with only an exit code > 0 from WinProc(as failure)
                // consider writing better remote script to deal with situation like that
                // a typical case is 'curl -s http://localhost:someport' where the port is not open
                // even if there are error message to the linux stderr, it is not passed over to plink and the return is just empty
                // with 1 as the exit code
                var x = v;
                //ws.WriteLine("");
            };

            var result = RO.Common3.Utils.WinProcEx(plinkPath, null, null, cmdArg, puttyPromptHandler, puttyPromptHandler, exitHandler);
            return new Tuple<int, string, string>(result.Item1.ExitCode, sbStdOut.ToString(), sbStdErr.ToString());
        }

        // For report generator:

        public DataTable GetDdlOriColumnId33S1682(string rptwizCatId, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.GetDdlOriColumnId33S1682(rptwizCatId, bAll, keyId, dbConnectionString, dbPassword, ui, uc, cultureId);
            }
        }

        public DataTable GetDdlSelColumnId33S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.GetDdlSelColumnId33S1682(screenId, bAll, keyId, filterId, dbConnectionString, dbPassword, ui, uc, cultureId);
            }
        }

        public DataTable GetDdlSelColumnId44S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.GetDdlSelColumnId44S1682(screenId, bAll, keyId, filterId, dbConnectionString, dbPassword, ui, uc, cultureId);
            }
        }

        public DataTable GetDdlSelColumnId77S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.GetDdlSelColumnId77S1682(screenId, bAll, keyId, filterId, dbConnectionString, dbPassword, ui, uc, cultureId);
            }
        }

        public DataTable GetDdlRptGroupId3S1652(string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.GetDdlRptGroupId3S1652(dbConnectionString, dbPassword);
            }
        }

        public DataTable GetDdlRptChart3S1652(string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.GetDdlRptChart3S1652(dbConnectionString, dbPassword);
            }
        }

        public DataTable GetDdlOperator3S1652(string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.GetDdlOperator3S1652(dbConnectionString, dbPassword);
            }
        }

        public string AddAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.AddAdmRptWiz95(LUser, LCurr, ds, dbConnectionString, dbPassword);
            }
        }

        public bool DelAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.DelAdmRptWiz95(LUser, LCurr, ds, dbConnectionString, dbPassword);
            }
        }

        public bool UpdAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.UpdAdmRptWiz95(LUser, LCurr, ds, dbConnectionString, dbPassword);
            }
        }

        public void RmTranslatedLbl(string LabelId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                dac.RmTranslatedLbl(LabelId, dbConnectionString, dbPassword);
            }
        }

        public DataTable WrAddMenu(string PMenuId, string ParentId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrAddMenu(PMenuId, ParentId, dbConnectionString, dbPassword);
            }
        }

        public bool WrDelMenu(string MenuId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrDelMenu(MenuId, dbConnectionString, dbPassword);
            }
        }

        public void WrUpdMenu(string MenuId, string PMenuId, string ParentId, string MenuText, string CultureId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                dac.WrUpdMenu(MenuId, PMenuId, ParentId, MenuText, CultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable WrAddScreenTab(string TabFolderOrder, string ScreenId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrAddScreenTab(TabFolderOrder, ScreenId, dbConnectionString, dbPassword);
            }
        }

        public bool WrDelScreenTab(string ScreenTabId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrDelScreenTab(ScreenTabId, dbConnectionString, dbPassword);
            }
        }

        public void WrUpdScreenTab(string ScreenTabId, string TabFolderOrder, string TabFolderName, string CultureId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                dac.WrUpdScreenTab(ScreenTabId, TabFolderOrder, TabFolderName, CultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable WrAddScreenObj(string ScreenId, string PScreenObjId, string TabFolderId, bool IsTab, bool NewRow, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrAddScreenObj(ScreenId, PScreenObjId, TabFolderId, IsTab, NewRow, dbConnectionString, dbPassword);
            }
        }

        public bool WrDelScreenObj(string ScreenObjId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrDelScreenObj(ScreenObjId, dbConnectionString, dbPassword);
            }
        }

        public void WrUpdScreenObj(string ScreenObjId, string PScreenObjId, string TabFolderId, string ColumnHeader, string CultureId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                dac.WrUpdScreenObj(ScreenObjId, PScreenObjId, TabFolderId, ColumnHeader, CultureId, dbConnectionString, dbPassword);
            }
        }

        // Return ScreenId given program name.
        public string WrGetScreenId(string ProgramName, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrGetScreenId(ProgramName, dbConnectionString, dbPassword);
            }
        }

        // Return MasterTable given Screen ID and DB Column Id.
        public string WrGetMasterTable(string ScreenId, string ColumnId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrGetMasterTable(ScreenId, ColumnId, dbConnectionString, dbPassword);
            }
        }

        public DataTable WrGetScreenObj(string ScreenId, Int16 CultureId, string ScreenObjId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrGetScreenObj(ScreenId, CultureId, ScreenObjId, dbConnectionString, dbPassword);
            }
        }
        public DataTable WrGetWebRule(string ScreenId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrGetWebRule(ScreenId, dbConnectionString, dbPassword);
            }
        }

        // Return SQL script for cloning purpose given Screen ID.
        public string WrCloneScreen(string ScreenId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrCloneScreen(ScreenId, dbConnectionString, dbPassword);
            }
        }

        // Return SQL script for cloning purpose given Report ID.
        public string WrCloneReport(string ReportId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                return dac.WrCloneReport(ReportId, dbConnectionString, dbPassword);
            }
        }

        // Purge audit trails older than YearOld:
        public void PurgeScrAudit(Int16 YearOld, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                dac.PurgeScrAudit(YearOld, dbConnectionString, dbPassword);
            }
        }

        // Set a flag to indicate reactJS screen already generated:
        public void WrUpdScreenReactGen(string ScreenId, string dbConnectionString, string dbPassword)
        {
            using (WebAccessBase dac = GetWebAccess())
            {
                dac.WrUpdScreenReactGen(ScreenId, dbConnectionString, dbPassword);
            }
        }

    }
}