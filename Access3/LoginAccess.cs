namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;
    using System.Linq;

	public class LoginAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;
	
		public LoginAccess()
		{
			da = new OleDbDataAdapter();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true); // as a service to those who might inherit from us
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
				return;

			if (da != null)
			{
				if(da.SelectCommand != null)
				{
					if( da.SelectCommand.Connection != null  )
					{
						da.SelectCommand.Connection.Dispose();
					}
					da.SelectCommand.Dispose();
				}    
				da.Dispose();
				da = null;
			}
		}

        public bool IsUsrSafeIP(int UsrId, string IpAddress)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("IsUsrSafeIP", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
            cmd.Parameters.Add("@IpAddress", OleDbType.VarChar).Value = IpAddress;
            int rtn = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
            if (rtn == 0) { return false; }
            else { return true; }
        }

        public void SetUsrSafeIP(int UsrId, string IpAddress)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SetUsrSafeIP", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
            cmd.Parameters.Add("@IpAddress", OleDbType.VarChar).Value = IpAddress;
            cmd.ExecuteScalar();
            cmd.Dispose();
            cmd = null;
            cn.Close();
        }

		public bool IsNullLegacyPwd(string LoginName)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("IsNullLegacyPwd", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarWChar).Value = LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarChar).Value = LoginName;
			}
			int rtn = Convert.ToInt32(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} 
			else {return true;}
		}

		public bool ChkAdminLogin(string RowAuths)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("ChkAdminLogin", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@RowAuths", OleDbType.VarChar).Value = RowAuths;
			int rtn = Convert.ToInt32(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) { return false; }
			else { return true; }
		}

		public bool ChkLoginStatus(string LoginName)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("ChkLoginStatus", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarWChar).Value = LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarChar).Value = LoginName;
			}
			int rtn = Convert.ToInt32(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} 
			else {return true;}
		}

        public void SetLoginStatus(string LoginName, bool bLoginSuccess, string IpAddress, string Provider, string ProviderLoginName)
        {
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("SetLoginStatus", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarWChar).Value = LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarChar).Value = LoginName;
			}
			if (bLoginSuccess)
			{
				cmd.Parameters.Add("@LoginSuccess", OleDbType.Char).Value = "Y";
			}
			else
			{
				cmd.Parameters.Add("@LoginSuccess", OleDbType.Char).Value = "N";
			}
            cmd.Parameters.Add("@IpAddress", OleDbType.VarChar).Value = IpAddress;
            cmd.Parameters.Add("@Provider", OleDbType.VarChar).Value = Provider;
            cmd.Parameters.Add("@ProviderLogiName", OleDbType.VarChar).Value = ProviderLoginName;
            cmd.ExecuteScalar();
			cmd.Dispose();
			cmd = null;
			cn.Close();
		}

        public DataTable GetLogins(string LoginName, string Provider)
        {
            DataTable dt = new DataTable();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SET NOCOUNT ON SELECT DISTINCT u.UsrId, u.LoginName FROM dbo.Usr u INNER JOIN UsrProvider p on u.UsrId = p.UsrId AND p.LoginName = ? AND p.ProviderCd = ? WHERE u.Active = 'Y' ", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.Add("@LoginName", OleDbType.VarWChar).Value = LoginName;
            cmd.Parameters.Add("@ProviderCd", OleDbType.VarWChar).Value = Provider;
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch { throw; }
            finally { cn.Close(); }
            return dt;
        }

		public LoginUsr GetLoginSecure(Credential cr)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetLoginSecure",new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarWChar).Value = cr.LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarChar).Value = cr.LoginName;
			}
			cmd.Parameters.Add("@UsrPassword", OleDbType.VarBinary).Value = cr.Password;
            cmd.Parameters.Add("@Provider", OleDbType.VarChar).Value = cr.Provider;
            cmd.Parameters.Add("@SelectedLoginName", OleDbType.VarChar).Value = cr.SelectedLoginName;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
			da.Fill(dt);
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			else
			{
                DataRow dr = dt.Rows[0];
                int usrId = Int32.Parse(dr[0].ToString());

                int licensedCount = GetLicensedUserCount();
                if (licensedCount > 0)
                {
                    OleDbConnection cn = new OleDbConnection(GetDesConnStr());
                    DataTable dtUsr = new DataTable();
                    cn.Open();
                    cmd = new OleDbCommand("SET NOCOUNT ON SELECT TOP " + licensedCount.ToString() + " u.UsrId FROM dbo.Usr u WHERE u.Active = 'Y' ORDER BY u.UsrId ", cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 1800;
                    try
                    {
                        da.SelectCommand = cmd;
                        da.Fill(dtUsr);
                    }
                    catch { throw; }
                    finally { cn.Close(); }
                    if (dt.AsEnumerable().Where(drUsr=> drUsr["UsrId"].ToString() == usrId.ToString()).Count() == 0)
                    {
                        throw new Exception(string.Format("Please get more user login licenses(current purchased license {0}) or decactivate some inactive users", licensedCount));
                    }
                }


                LoginUsr usr = new LoginUsr(dr[14].ToString()
                    ,Int32.Parse(dr[0].ToString())
					,dr[1].ToString()
					,dr[2].ToString()
					,dr[3].ToString()
					,dr[4].ToString()
					,Int16.Parse(dr[5].ToString())
					,dr[6].ToString()
					,byte.Parse(dr[7].ToString())
					,Int16.Parse(dr[8].ToString())
					,Int32.Parse(dr[9].ToString())
                    ,Int16.Parse(dr[10].ToString())
                    ,Int16.Parse(dr[11].ToString())
                    ,dr[12].ToString() == "Y" ? true : false
                    ,dr[13] as DateTime?
                    , dr[15].ToString()
                    , dr[17].ToString() == "Y"
                    );
                return usr;
			}
		}

		public LoginUsr GetLoginLegacy(string LoginName, string Password)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetLoginLegacy",new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarWChar).Value = LoginName;
				cmd.Parameters.Add("@Password", OleDbType.VarWChar).Value = Password;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarChar).Value = LoginName;
				cmd.Parameters.Add("@Password", OleDbType.VarChar).Value = Password;
			}
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			else
			{
				DataRow dr = dt.Rows[0];
				LoginUsr usr = new LoginUsr(LoginName
					,Int32.Parse(dr[0].ToString())
					,dr[1].ToString()
					,dr[2].ToString()
					,dr[3].ToString()
					,dr[4].ToString()
					,byte.Parse(dr[5].ToString())
					,dr[6].ToString()
					,byte.Parse(dr[7].ToString())
					,Int16.Parse(dr[8].ToString())
					,Int16.Parse(dr[9].ToString())
					,Int16.Parse(dr[10].ToString())
					,Int16.Parse(dr[11].ToString())
                    ,dr[12].ToString() == "Y" ? true : false
                    ,dr[13] as DateTime?
                    , null
                    , false
                    );
                return usr;
			}
		}

        public void CancelUserAccount(int UsrId)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("CancelUserAccount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, "CancelUserAccount", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public void UpdUserLoginInfo(int UsrId, string LoginName, string UsrName, string UsrEmail)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("UpdUsrLoginInfo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@LoginName", Config.DoubleByteDb ? OleDbType.VarWChar : OleDbType.VarChar).Value = LoginName;
                cmd.Parameters.Add("@UsrName", Config.DoubleByteDb ? OleDbType.VarWChar : OleDbType.VarChar).Value = UsrName;
                cmd.Parameters.Add("@UsrEmail", Config.DoubleByteDb ? OleDbType.VarWChar : OleDbType.VarChar).Value = string.IsNullOrEmpty(UsrEmail) ? (object)System.DBNull.Value : UsrEmail;
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, "UpdUsrLoginInfo", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public DataTable GetSaltedUserInfo(int UsrId, string LoginName, string UsrEmail)
        {
            DataTable dt = new DataTable();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("DECLARE @UsrId int, @LoginName nvarchar(32), @UsrEmail nvarchar(1024) SELECT @UsrId=?, @LoginName=?, @UsrEmail=? SET NOCOUNT ON SELECT DISTINCT UsrId, LoginName, UsrEmail, UsrPassword, LastPwdChgDt FROM dbo.Usr WHERE Active = 'Y' AND (UsrId = @UsrId OR (LoginName IS NOT NULL AND LoginName <> '' AND LoginName = @LoginName) OR (UsrEmail IS NOT NULL AND UsrEmail <> '' AND UsrEmail = @UsrEmail))", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.Add("@UsrId", OleDbType.Integer).Value = UsrId;
            cmd.Parameters.Add("@LoginName", OleDbType.VarWChar).Value = LoginName;
            cmd.Parameters.Add("@UsrEmail", OleDbType.VarWChar).Value = UsrEmail;
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch { throw; }
            finally { cn.Close(); }

            foreach (DataRow dr in dt.Rows)
            {
                // we don't want to leak the user password hash verbatim
                dr["UsrPassword"] = (new System.Security.Cryptography.HMACMD5(dr["UsrPassword"] == null || dr["UsrPassword"].ToString() == "" ? (System.Security.Cryptography.SHA1.Create()).ComputeHash(System.Text.Encoding.Unicode.GetBytes("NoPassword")) : dr["UsrPassword"] as byte[])).ComputeHash(System.Text.Encoding.ASCII.GetBytes(dr["UsrId"].ToString() + (dr["LastPwdChgDt"] == null || dr["LastPwdChgDt"].ToString() == "" ? new DateTime() : (DateTime)dr["LastPwdChgDt"]).ToString(System.Globalization.CultureInfo.InvariantCulture)));
            }
            return dt;
        }

		public UsrPref GetUsrPref(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetUsrPref",new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@CompanyId", OleDbType.Numeric).Value = CompanyId;
			cmd.Parameters.Add("@ProjectId", OleDbType.Numeric).Value = ProjectId;
			cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count <= 1, "GetUsrPref", "Integrity Failure", "Non-unique user Preference!");
			if (dt.Rows.Count != 1)
			{
				return null;
			}
			else
			{
				DataRow dr = dt.Rows[0];
                UsrPref pref = new UsrPref(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString());
				return pref;
			}
		}

		public UsrImpr GetUsrImpr(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetUsrImpr",new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@companyId", OleDbType.Numeric).Value = CompanyId;
			cmd.Parameters.Add("@projectId", OleDbType.Numeric).Value = ProjectId;
			cmd.Parameters.Add("@systemId", OleDbType.Numeric).Value = SystemId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count <= 1, "GetUsrImpr", "Integrity Failure", "Non-unique user Impersonation!");
			if (dt.Rows.Count != 1)
			{
				return null;
			}
			else
			{
				DataRow dr = dt.Rows[0];
				UsrImpr impr = new UsrImpr(dr[0].ToString(),dr[1].ToString(),dr[2].ToString(),dr[3].ToString(),dr[4].ToString()
					,dr[5].ToString(),dr[6].ToString(),dr[7].ToString(),dr[8].ToString(),dr[9].ToString(),dr[10].ToString(),dr[11].ToString()
                    ,dr[12].ToString(),dr[13].ToString(),dr[14].ToString());
				return impr;
			}
		}

		public DataTable GetUsrImprNext(Int32 usrId)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetUsrImprNext",new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = usrId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetCompanyList(string Usrs, string RowAuthoritys, string Companys)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetCompanyList",new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = Usrs;
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = RowAuthoritys;
			cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = Companys;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
            int licensedCount = GetLicensedCompanyCount();
            if (licensedCount > 0)
            {
                int ii = 0;
                bool rowsRemoved = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (ii >= licensedCount)
                    {
                        dr.Delete();
                        rowsRemoved = true;
                    }
                    ii = ii + 1;
                }
                if (rowsRemoved) dt.AcceptChanges();
            }
            return dt;
		}

		public DataTable GetProjectList(string Usrs, string RowAuthoritys, string Projects, string currCompanyId)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OleDbCommand cmd = new OleDbCommand("GetProjectList", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = Usrs;
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = RowAuthoritys;
			cmd.Parameters.Add("@Projects", OleDbType.VarChar).Value = Projects;
			if (currCompanyId == string.Empty)
			{
				cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = currCompanyId;
			}
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);

            int licensedCount = GetLicensedProjectCount();
            if (licensedCount > 0)
            {
                int ii = 0;
                bool rowsRemoved = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (ii >= licensedCount)
                    {
                        dr.Delete();
                        rowsRemoved = true;
                    }
                    ii = ii + 1;
                }
                if (rowsRemoved) dt.AcceptChanges();
            }

            return dt;
		}

		public DataTable GetSystemsList(string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = null;
			if (string.IsNullOrEmpty(dbConnectionString) || string.IsNullOrEmpty(dbPassword))
			{
				cmd = new OleDbCommand("GetSystemsList", new OleDbConnection(GetDesConnStr()));
			}
			else
			{
				cmd = new OleDbCommand("GetSystemsList", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);

            int licensedCount = GetLicensedModuleCount();
            if (licensedCount > 0)
            {
                int ii = 0;
                bool rowsRemoved = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (ii >= licensedCount && dr["SystemId"].ToString() != "3" && dr["SystemId"].ToString() != "5")
                    {
                        dr.Delete();
                        rowsRemoved = true;
                    }
                    ii = ii + 1;
                }
                if (rowsRemoved) dt.AcceptChanges();
            }

            return dt;
		}

        public bool UpdUsrPassword(Credential cr, LoginUsr LUser, bool RemoveLink)
        {
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("UpdUsrPassword", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarWChar).Value = cr.LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OleDbType.VarChar).Value = cr.LoginName;
			}
			cmd.Parameters.Add("@UsrPassword", OleDbType.VarBinary).Value = cr.Password;
            cmd.Parameters.Add("@CurrUsrId", OleDbType.Integer).Value = LUser != null && (LUser.LoginName ?? "").ToLower() != "anonymous" ? LUser.UsrId : -1;
            cmd.Parameters.Add("@RemoveLink", OleDbType.Char).Value = RemoveLink ? "Y" : "N";
            int rtn = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} 
			else {return true;}
		}

		public string GetPwdExpMsg(string UsrId, string CultureId, string PwdExpDays)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetPwdExpMsg", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@PwdExpDays", OleDbType.Numeric).Value = PwdExpDays;
			string rtn = Convert.ToString(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

		public string GetHintAnswer(string UsrId)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetHintAnswer", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
			string rtn = Convert.ToString(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

		public string GetHintQuestionId(string UsrId)
		{
			if ( da == null ) { throw new System.ObjectDisposedException( GetType().FullName ); }
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetHintQuestionId", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
			string rtn = Convert.ToString(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

		public DataTable GetHintQuestion()
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetHintQuestion", new OleDbConnection(GetDesConnStr()));
			cmd.CommandType = CommandType.StoredProcedure;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public bool UpdHintQuestion(string UsrId, string HintQuestionId, string HintAnswer)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("UpdHintQuestion", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
			if (HintQuestionId == string.Empty)
			{
				cmd.Parameters.Add("@HintQuestionId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@HintQuestionId", OleDbType.Numeric).Value = HintQuestionId;
			}
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@HintAnswer", OleDbType.VarWChar).Value = HintAnswer;
			}
			else
			{
				cmd.Parameters.Add("@HintAnswer", OleDbType.VarChar).Value = HintAnswer;
			}
			int rtn = Convert.ToInt32(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} 
			else {return true;}
		}

		public string GetRbtVersion()
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbConnection cn =  new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetAppVersion", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			string rtn = Convert.ToString(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

		public string GetAppVersion(string dbConnectionString, string dbPassword)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetAppVersion", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			string rtn = Convert.ToString(cmd.ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

        public void LinkUserLogin(int UsrId, string ProviderCd, string LoginName)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("LinkUsrLogin", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@ProviderCd", OleDbType.Char).Value = ProviderCd;
                cmd.Parameters.Add("@LoginName", Config.DoubleByteDb ? OleDbType.VarWChar : OleDbType.VarChar).Value = LoginName;
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, "LinkUserLogin", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public void UnlinkUserLogin(int UsrId, string ProviderCd, string LoginName)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("UnlinkUsrLogin", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@ProviderCd", OleDbType.Char).Value = ProviderCd;
                cmd.Parameters.Add("@LoginName", Config.DoubleByteDb ? OleDbType.VarWChar : OleDbType.VarChar).Value = LoginName;
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, "UnlinkUserLogin", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public DataTable GetLinkedUserLogin(int UsrId)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbCommand cmd = new OleDbCommand("GetLinkedUserLogin", new OleDbConnection(GetDesConnStr()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@usrId", OleDbType.Numeric).Value = UsrId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable WrAddUsr(string LoginName, string UsrName, string UsrPassword, int CultureId, int DefSystemId, string UsrEmail, string UsrGroups, bool ForcePwdChg, int? CustomerId, int? BrokerId, int? VendorId, bool Active, string SSOProviderCd, string SSOLoginName)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrAddUsr", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LogiName", OleDbType.VarWChar).Value = LoginName;
            cmd.Parameters.Add("@UsrName", OleDbType.VarWChar).Value = UsrName;
            cmd.Parameters.Add("@UsrPassword", OleDbType.VarBinary).Value = new Credential(LoginName, UsrPassword).Password;
            cmd.Parameters.Add("@CultureId", OleDbType.Integer).Value = CultureId;
            cmd.Parameters.Add("@DefSystemId", OleDbType.Integer).Value = DefSystemId;
            cmd.Parameters.Add("@UsrEmail", OleDbType.VarWChar).Value = UsrEmail;
            cmd.Parameters.Add("@UsrGroups", OleDbType.VarChar).Value = UsrGroups;
            cmd.Parameters.Add("@ForcePwdChg", OleDbType.Char).Value = ForcePwdChg ? "Y" : "N";
            cmd.Parameters.Add("@CustomerId", OleDbType.Integer).Value = string.IsNullOrEmpty(CustomerId.ToString()) ? DBNull.Value : (object)CustomerId;
            cmd.Parameters.Add("@BrokerId", OleDbType.Integer).Value = string.IsNullOrEmpty(BrokerId.ToString()) ? DBNull.Value : (object)BrokerId;
            cmd.Parameters.Add("@VendorId", OleDbType.Integer).Value = string.IsNullOrEmpty(VendorId.ToString()) ? DBNull.Value : (object)VendorId;
            cmd.Parameters.Add("@Active", OleDbType.Char).Value = Active ? "Y" : "N";
            cmd.Parameters.Add("@SSOProviderCd", OleDbType.Char).Value = SSOProviderCd;
            cmd.Parameters.Add("@SSOLoginName", OleDbType.VarWChar).Value = SSOLoginName;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = cmd;
            cmd.Transaction = tr;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "WrAddUsr", "", e.Message);
            }
            finally { cn.Close(); }
            return ds.Tables[0];
        }

        public void WrDelUsr(int UsrId)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("WrDelUsr", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrDelUsr", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public string WrGetUsrOTPSecret(int UsrId, string hostSecret = null)
        {

            DataTable dt = new DataTable();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("DECLARE @UsrId int SELECT @UsrId=? SET NOCOUNT ON SELECT DISTINCT UsrId, OTPSecret, TwoFactorAuth FROM dbo.Usr WHERE (UsrId = @UsrId)", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 90;
            cmd.Parameters.Add("@UsrId", OleDbType.Integer).Value = UsrId;
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return string.IsNullOrEmpty(hostSecret) ? DecryptString(dt.Rows[0]["OTPSecret"].ToString()) : DecryptString(dt.Rows[0]["OTPSecret"].ToString(), hostSecret);
            }
            catch { return string.Empty; }
            finally { cn.Close(); }

        }

        public string WrSetUsrOTPSecret(int UsrId, bool bEnable, string hostSecret = null)
        {
            string Secret = Guid.NewGuid().ToString().Replace("-", "");
            string EncSecret = string.IsNullOrEmpty(hostSecret) ? EncryptString(Secret) : EncryptString(Secret, hostSecret);
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("DECLARE @UsrId int, @TwoFactorAuth CHAR(1), @OTPSecret VARCHAR(64) SELECT @UsrId=?, @TwoFactorAuth=?, @OTPSecret=? SET NOCOUNT ON UPDATE Usr SET OTPSecret = @OTPSecret, TwoFactorAuth = @TwoFactorAuth WHERE UsrId = @UsrId", cn);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@TwoFactorAuth", OleDbType.Char).Value = bEnable ? "Y" : "N";
                cmd.Parameters.Add("@OTPSecret", OleDbType.VarChar).Value = bEnable ? (object)(EncSecret) : System.DBNull.Value;
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, "WrSetUsrOTPSecret", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
            return Secret;
        }
    }
}