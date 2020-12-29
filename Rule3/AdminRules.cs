namespace RO.Rule3
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Text.RegularExpressions;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.SystemFramewk;
	using RO.Access3;

	public class AdminRules
	{
		public AdminAccessBase GetAdminAccess(int CommandTimeout = 1800)
		{
			if ((Config.DesProvider  ?? "").ToLower() != "odbc")
			{
				return new AdminAccess(CommandTimeout);
			}
			else
			{
				return new RO.Access3.Odbc.AdminAccess(CommandTimeout);
			}
		}

		public DataTable GetLastCriteria(Int32 rowsExpected, Int32 screenId, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			DataTable dt = null;
			using (AdminAccessBase dac = GetAdminAccess())
			{
				dt = dac.GetLastCriteria(screenId, reportId, usrId, dbConnectionString, dbPassword);
			}
			if (dt.Rows.Count != rowsExpected)
			{
				using (AdminAccessBase dac = GetAdminAccess())
				{
					dac.DelLastCriteria(screenId, reportId, usrId, dbConnectionString, dbPassword);
					dac.IniLastCriteria(screenId, reportId, usrId, dbConnectionString, dbPassword);
					dt = dac.GetLastCriteria(screenId, reportId, usrId, dbConnectionString, dbPassword);
				}
			}
			return dt;
		}

		public string GetMsg(string Msg, Int16 CultureId, string TechnicalUsr, string dbConnectionString, string dbPassword)
		{
			string mm = Msg;
			using (AdminAccessBase dac = GetAdminAccess())
			{
				try 
				{
					int ic = mm.IndexOf("{");
					if (ic >= 0)
					{
						mm = mm.Substring(ic, mm.Length - ic);
						MatchCollection sa = Regex.Matches(mm,@"{[^}]*}");
						if (sa[0].Value != string.Empty)	// Found MsgId and process only the first one.
						{
							DataTable dt = dac.GetMsg(Int32.Parse(sa[0].Value.Substring(1,sa[0].Value.Length-2)), CultureId, dbConnectionString, dbPassword);
							if (dt != null && dt.Rows.Count > 0)
							{
								string ss = dt.Rows[0]["Msg"].ToString();
								for (int ii=1; ii < sa.Count; ii++)
								{
									ss = SubPar(ss, ii, sa[ii].Value.Substring(1,sa[ii].Value.Length-2), dt.Rows[0]["CultureTypeName"].ToString());
								}
								if (TechnicalUsr == "Y")
								{
									return dt.Rows[0]["MsgSource"].ToString() + ": " + ss;
								}
								else {return ss;}
							} 
							else return Msg;
						} 
						else return Msg;
					}
					else // Remove all messages in front including Sybase extra messages between "[" and "]".
					{
						int i0 = mm.IndexOf("[");
						if (i0 >= 0)
						{
							int i1 = mm.IndexOf("]");
							if (i1 >= 0)
							{
								int i2 = mm.Substring(i1 + 1).IndexOf("]");
								if (i2 >= 0) {mm = mm.Substring(0,i0) + mm.Substring(i1 + i2 + 2).Trim();}
							}
						}
						return mm;
					}
				}
				catch {return Msg;}
			}
		}
        public DataTable GetCronJob(int? jobId, string jobLink, string dbConnectionString, string dbPassword)
        {
            using (AdminAccessBase dac = GetAdminAccess())
            {
                return dac.GetCronJob(jobId, jobLink, dbConnectionString, dbPassword);
            }
        }
        public void UpdCronJob(int jobId, DateTime? lastRun, DateTime? nextRun, string dbConnectionString, string dbPassword)
        {
            using (AdminAccessBase dac = GetAdminAccess())
            {
                dac.UpdCronJob(jobId, lastRun, nextRun, dbConnectionString, dbPassword);
            }
        }
        public void UpdCronJobStatus(int jobId, string err, string dbConnectionString, string dbPassword)
        {
            using (AdminAccessBase dac = GetAdminAccess())
            {
                dac.UpdCronJobStatus(jobId, err, dbConnectionString, dbPassword);
            }
        }
        public Tuple<string, bool, string> UpdateLicense(string licenseString, string hash)
        {
            using (AdminAccessBase dac = GetAdminAccess())
            {
                return dac.UpdateLicense(licenseString, hash);
            }
        }

		public string SubPar(string msg, int parNum, string parVal, string parCulture)
		{
			string ss = msg;
			string par = Regex.Match(ss, "%" + (parNum).ToString() + "[sncgdD]").Value;
			if (par != string.Empty)
			{
				switch (par.Substring(par.Length-1))
				{
					case "n":
						ss = ss.Replace(par, Utils.fmMoney(parVal, parCulture));
						break;
					case "c":
						ss = ss.Replace(par, Utils.fmCurrency(parVal, parCulture));
						break;
					case "g":
						ss = ss.Replace(par, Utils.fmNumeric(parVal, parCulture));
						break;
					case "d":
						ss = ss.Replace(par, Utils.fmShortDate(parVal, parCulture));
						break;
					case "D":
						ss = ss.Replace(par, Utils.fmLongDate(parVal, parCulture));
						break;
					default:
						ss = ss.Replace(par, parVal);
						break;
				}
			}
			return ss;
		}
	}
}