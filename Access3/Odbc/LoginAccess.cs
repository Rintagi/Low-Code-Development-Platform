namespace RO.Access3.Odbc
{
	using System;
	using System.Data;
	//using System.Data.OleDb;
    using System.Data.Odbc;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;
    using System.Linq;

	public class LoginAccess : LoginAccessBase, IDisposable
	{
		private OdbcDataAdapter da;

        private static OdbcCommand TransformCmd(OdbcCommand cmd)
        {
            if (cmd.Parameters != null 
                && cmd.Parameters.Count > 0
                && cmd.CommandType == CommandType.StoredProcedure
                && !string.IsNullOrEmpty(cmd.CommandText)
                && !cmd.CommandText.StartsWith("{CALL")
                )
            {
                cmd.CommandText = string.Format("{{CALL {0}({1})}}"
                    , cmd.CommandText
                    , string.Join(",", Enumerable.Repeat("?", cmd.Parameters.Count).ToArray())
                    );
            } 
            return cmd;
        }
		public LoginAccess()
		{
			da = new OdbcDataAdapter();
		}

		public override void Dispose()
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

        public override bool IsUsrSafeIP(int UsrId, string IpAddress)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("IsUsrSafeIP", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
            cmd.Parameters.Add("@IpAddress", OdbcType.VarChar).Value = IpAddress;
            int rtn = Convert.ToInt32(TransformCmd(cmd).ExecuteScalar());
            cmd.Dispose();
            cmd = null;
            cn.Close();
            if (rtn == 0) { return false; }
            else { return true; }
        }

        public override void SetUsrSafeIP(int UsrId, string IpAddress)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("SetUsrSafeIP", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
            cmd.Parameters.Add("@IpAddress", OdbcType.VarChar).Value = IpAddress;
            TransformCmd(cmd).ExecuteScalar();
            cmd.Dispose();
            cmd = null;
            cn.Close();
        }

		public override bool IsNullLegacyPwd(string LoginName)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("IsNullLegacyPwd", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OdbcType.NVarChar).Value = LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OdbcType.VarChar).Value = LoginName;
			}
			int rtn = Convert.ToInt32(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} 
			else {return true;}
		}

		public override bool ChkAdminLogin(string RowAuths)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("ChkAdminLogin", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@RowAuths", OdbcType.VarChar).Value = RowAuths;
			int rtn = Convert.ToInt32(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) { return false; }
			else { return true; }
		}

		public override bool ChkLoginStatus(string LoginName)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("ChkLoginStatus", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OdbcType.NVarChar).Value = LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OdbcType.VarChar).Value = LoginName;
			}
			int rtn = Convert.ToInt32(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} 
			else {return true;}
		}

        public override void SetLoginStatus(string LoginName, bool bLoginSuccess, string IpAddress, string Provider, string ProviderLoginName)
        {
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("SetLoginStatus", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OdbcType.NVarChar).Value = LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OdbcType.VarChar).Value = LoginName;
			}
			if (bLoginSuccess)
			{
				cmd.Parameters.Add("@LoginSuccess", OdbcType.Char).Value = "Y";
			}
			else
			{
				cmd.Parameters.Add("@LoginSuccess", OdbcType.Char).Value = "N";
			}
            cmd.Parameters.Add("@IpAddress", OdbcType.VarChar).Value = IpAddress;
            cmd.Parameters.Add("@Provider", OdbcType.VarChar).Value = string.IsNullOrEmpty(Provider) ? (object) System.DBNull.Value : Provider;
            cmd.Parameters.Add("@ProviderLogiName", OdbcType.VarChar).Value = string.IsNullOrEmpty(ProviderLoginName) ? (object)System.DBNull.Value : ProviderLoginName;
            TransformCmd(cmd).ExecuteScalar();
			cmd.Dispose();
			cmd = null;
			cn.Close();
		}

        public override DataTable GetLogins(string SSOLoginName, string Provider, string UsrId = null)
        {
            DataTable dt = new DataTable();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("GetLoginSSO", cn);
            cmd.CommandType = CommandType.StoredProcedure;

//            OdbcCommand cmd = new OdbcCommand("SET NOCOUNT ON SELECT DISTINCT u.UsrId, u.LoginName FROM dbo.Usr u INNER JOIN UsrProvider p on u.UsrId = p.UsrId AND p.LoginName = ? AND p.ProviderCd = ? WHERE u.Active = 'Y' ", cn);
//            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.Add("@SSOLoginName", OdbcType.NVarChar).Value = string.IsNullOrEmpty(SSOLoginName) ? (object)System.DBNull.Value : SSOLoginName;
            cmd.Parameters.Add("@ProviderCd", OdbcType.NVarChar).Value = Provider;
            cmd.Parameters.Add("@UsrId", OdbcType.Int).Value = string.IsNullOrEmpty(UsrId) ? (object)System.DBNull.Value : UsrId;

            try
            {
                da.SelectCommand = TransformCmd(cmd);
                da.Fill(dt);
            }
            catch { throw; }
            finally { cn.Close(); }
            return dt;
        }

		public override LoginUsr GetLoginSecure(Credential cr)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetLoginSecure",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OdbcType.NVarChar).Value = cr.LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OdbcType.VarChar).Value = cr.LoginName;
			}
			cmd.Parameters.Add("@UsrPassword", OdbcType.VarBinary).Value = cr.Password;
            cmd.Parameters.Add("@Provider", OdbcType.VarChar).Value = cr.Provider;
            cmd.Parameters.Add("@SelectedLoginName", OdbcType.VarChar).Value = cr.SelectedLoginName;
            da.SelectCommand = TransformCmd(cmd);
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
                    OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
                    DataTable dtUsr = new DataTable();
                    cn.Open();
                    cmd = new OdbcCommand("SET NOCOUNT ON SELECT TOP " + licensedCount.ToString() + " u.UsrId FROM dbo.Usr u WHERE u.Active = 'Y' ORDER BY u.UsrId ", cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 1800;
                    try
                    {
                        da.SelectCommand = TransformCmd(cmd);
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

		public override LoginUsr GetLoginLegacy(string LoginName, string Password)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetLoginLegacy",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OdbcType.NVarChar).Value = LoginName;
				cmd.Parameters.Add("@Password", OdbcType.NVarChar).Value = Password;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OdbcType.VarChar).Value = LoginName;
				cmd.Parameters.Add("@Password", OdbcType.VarChar).Value = Password;
			}
            da.SelectCommand = TransformCmd(cmd);
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

        public override void CancelUserAccount(int UsrId)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("CancelUserAccount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
                TransformCmd(cmd).ExecuteNonQuery();
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

        public override void UpdUserLoginInfo(int UsrId, string LoginName, string UsrName, string UsrEmail)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("UpdUsrLoginInfo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@LoginName", Config.DoubleByteDb ? OdbcType.NVarChar : OdbcType.VarChar).Value = LoginName;
                cmd.Parameters.Add("@UsrName", Config.DoubleByteDb ? OdbcType.NVarChar : OdbcType.VarChar).Value = UsrName;
                cmd.Parameters.Add("@UsrEmail", Config.DoubleByteDb ? OdbcType.NVarChar : OdbcType.VarChar).Value = string.IsNullOrEmpty(UsrEmail) ? (object)System.DBNull.Value : UsrEmail;
                TransformCmd(cmd).ExecuteNonQuery();
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

        public override DataTable GetSaltedUserInfo(int UsrId, string LoginName, string UsrEmail)
        {
            DataTable dt = new DataTable();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("DECLARE @UsrId int, @LoginName nvarchar(32), @UsrEmail nvarchar(1024) SELECT @UsrId=?, @LoginName=?, @UsrEmail=? SET NOCOUNT ON SELECT DISTINCT UsrId, LoginName, UsrEmail, UsrPassword, LastPwdChgDt FROM dbo.Usr WHERE Active = 'Y' AND (UsrId = @UsrId OR (LoginName IS NOT NULL AND LoginName <> '' AND LoginName = @LoginName) OR (UsrEmail IS NOT NULL AND UsrEmail <> '' AND UsrEmail = @UsrEmail))", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.Add("@UsrId", OdbcType.Int).Value = UsrId;
            cmd.Parameters.Add("@LoginName", OdbcType.NVarChar).Value = LoginName;
            cmd.Parameters.Add("@UsrEmail", OdbcType.NVarChar).Value = UsrEmail;
            try
            {
                da.SelectCommand = TransformCmd(cmd);
                da.Fill(dt);
            }
            catch { throw; }
            finally { cn.Close(); }

            foreach (DataRow dr in dt.Rows)
            {
                // we don't want to leak the user password hash verbatim
                dr["UsrPassword"] = (new System.Security.Cryptography.HMACSHA256(dr["UsrPassword"] == null || dr["UsrPassword"].ToString() == "" ? (System.Security.Cryptography.SHA1.Create()).ComputeHash(System.Text.Encoding.Unicode.GetBytes("NoPassword")) : dr["UsrPassword"] as byte[])).ComputeHash(System.Text.Encoding.ASCII.GetBytes(dr["UsrId"].ToString() + (dr["LastPwdChgDt"] == null || dr["LastPwdChgDt"].ToString() == "" ? new DateTime() : (DateTime)dr["LastPwdChgDt"]).ToString(System.Globalization.CultureInfo.InvariantCulture)));
            }
            return dt;
        }

		public override UsrPref GetUsrPref(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetUsrPref",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@CompanyId", OdbcType.Numeric).Value = CompanyId;
			cmd.Parameters.Add("@ProjectId", OdbcType.Numeric).Value = ProjectId;
			cmd.Parameters.Add("@SystemId", OdbcType.Numeric).Value = SystemId;
			da.SelectCommand = TransformCmd(cmd);
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

		public override UsrImpr GetUsrImpr(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetUsrImpr",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@usrId", OdbcType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@companyId", OdbcType.Numeric).Value = CompanyId;
			cmd.Parameters.Add("@projectId", OdbcType.Numeric).Value = ProjectId;
			cmd.Parameters.Add("@systemId", OdbcType.Numeric).Value = SystemId;
			da.SelectCommand = TransformCmd(cmd);
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

		public override DataTable GetUsrImprNext(Int32 usrId)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetUsrImprNext",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@usrId", OdbcType.Numeric).Value = usrId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetCompanyList(string Usrs, string RowAuthoritys, string Companys)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetCompanyList",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = Usrs;
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = RowAuthoritys;
			cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = Companys;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
            int licensedCount = GetLicensedCompanyCount();
            if (licensedCount > 0)
            {
                int ii = 0;
                bool rowsRemoved = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["CompanyId"].ToString())) continue;

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

		public override DataTable GetProjectList(string Usrs, string RowAuthoritys, string Projects, string currCompanyId)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			OdbcCommand cmd = new OdbcCommand("GetProjectList", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = Usrs;
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = RowAuthoritys;
			cmd.Parameters.Add("@Projects", OdbcType.VarChar).Value = Projects;
			if (currCompanyId == string.Empty)
			{
				cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = currCompanyId;
			}
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);

            int licensedCount = GetLicensedProjectCount();
            if (licensedCount > 0)
            {
                int ii = 0;
                bool rowsRemoved = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["ProjectId"].ToString())) continue;

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

		public override DataTable GetSystemsList(string dbConnectionString, string dbPassword)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = null;
			if (string.IsNullOrEmpty(dbConnectionString) || string.IsNullOrEmpty(dbPassword))
			{
				cmd = new OdbcCommand("GetSystemsList", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			}
			else
			{
				cmd = new OdbcCommand("GetSystemsList", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			}
			cmd.CommandType = CommandType.StoredProcedure;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);

            int licensedCount = GetLicensedModuleCount();
            if (licensedCount >= 0)
            {
                int ii = 0;
                bool rowsRemoved = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["SystemId"].ToString())) continue;

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

        public override bool UpdUsrPassword(Credential cr, LoginUsr LUser, bool RemoveLink)
        {
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("UpdUsrPassword", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@LoginName", OdbcType.NVarChar).Value = cr.LoginName;
			}
			else
			{
				cmd.Parameters.Add("@LoginName", OdbcType.VarChar).Value = cr.LoginName;
			}
			cmd.Parameters.Add("@UsrPassword", OdbcType.VarBinary).Value = cr.Password;
            cmd.Parameters.Add("@CurrUsrId", OdbcType.Int).Value = LUser != null && (LUser.LoginName ?? "").ToLower() != "anonymous" ? LUser.UsrId : -1;
            cmd.Parameters.Add("@RemoveLink", OdbcType.Char).Value = RemoveLink ? "Y" : "N";
            int rtn = Convert.ToInt32(TransformCmd(cmd).ExecuteScalar());
            cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} 
			else {return true;}
		}

		public override string GetPwdExpMsg(string UsrId, string CultureId, string PwdExpDays)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("GetPwdExpMsg", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@PwdExpDays", OdbcType.Numeric).Value = PwdExpDays;
			string rtn = Convert.ToString(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

		public override string GetHintAnswer(string UsrId)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("GetHintAnswer", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
			string rtn = Convert.ToString(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

		public override string GetHintQuestionId(string UsrId)
		{
			if ( da == null ) { throw new System.ObjectDisposedException( GetType().FullName ); }
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("GetHintQuestionId", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
			string rtn = Convert.ToString(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

		public override DataTable GetHintQuestion()
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetHintQuestion", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
			cmd.CommandType = CommandType.StoredProcedure;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override bool UpdHintQuestion(string UsrId, string HintQuestionId, string HintAnswer)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("UpdHintQuestion", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
			if (HintQuestionId == string.Empty)
			{
				cmd.Parameters.Add("@HintQuestionId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@HintQuestionId", OdbcType.Numeric).Value = HintQuestionId;
			}
			if (Config.DoubleByteDb)
			{
				cmd.Parameters.Add("@HintAnswer", OdbcType.NVarChar).Value = HintAnswer;
			}
			else
			{
				cmd.Parameters.Add("@HintAnswer", OdbcType.VarChar).Value = HintAnswer;
			}
			int rtn = Convert.ToInt32(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			if (rtn == 0) {return false;} 
			else {return true;}
		}

		public override string GetRbtVersion()
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("GetAppVersion", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			string rtn = Convert.ToString(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

		public override string GetAppVersion(string dbConnectionString, string dbPassword)
		{
			if ( da == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("GetAppVersion", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			string rtn = Convert.ToString(TransformCmd(cmd).ExecuteScalar());
			cmd.Dispose();
			cmd = null;
			cn.Close();
			return rtn;
		}

        public override void LinkUserLogin(int UsrId, string ProviderCd, string LoginName, string LoginMeta = null)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("LinkUsrLogin", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@ProviderCd", OdbcType.Char).Value = ProviderCd;
                cmd.Parameters.Add("@LoginName", Config.DoubleByteDb ? OdbcType.NVarChar : OdbcType.VarChar).Value = LoginName;
                cmd.Parameters.Add("@LoginMeta", Config.DoubleByteDb ? OdbcType.NVarChar : OdbcType.VarChar).Value = LoginMeta;
                TransformCmd(cmd).ExecuteNonQuery();
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

        public override void UnlinkUserLogin(int UsrId, string ProviderCd, string LoginName, string LoginMeta = null)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("UnlinkUsrLogin", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@ProviderCd", OdbcType.Char).Value = ProviderCd;
                cmd.Parameters.Add("@LoginName", Config.DoubleByteDb ? OdbcType.NVarChar : OdbcType.VarChar).Value = LoginName;
                cmd.Parameters.Add("@LoginMeta", Config.DoubleByteDb ? OdbcType.NVarChar : OdbcType.VarChar).Value = LoginMeta;
                TransformCmd(cmd).ExecuteNonQuery();
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

        public override DataTable GetLinkedUserLogin(int UsrId)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcCommand cmd = new OdbcCommand("GetLinkedUserLogin", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@usrId", OdbcType.Numeric).Value = UsrId;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override DataTable WrAddUsr(string LoginName, string UsrName, string UsrPassword, int CultureId, int DefSystemId, string UsrEmail, string UsrGroups, bool ForcePwdChg, int? CustomerId, int? BrokerId, int? VendorId, bool Active, string SSOProviderCd, string SSOLoginName)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrAddUsr", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LogiName", OdbcType.NVarChar).Value = LoginName;
            cmd.Parameters.Add("@UsrName", OdbcType.NVarChar).Value = UsrName;
            cmd.Parameters.Add("@UsrPassword", OdbcType.VarBinary).Value = new Credential(LoginName, UsrPassword).Password;
            cmd.Parameters.Add("@CultureId", OdbcType.Int).Value = CultureId;
            cmd.Parameters.Add("@DefSystemId", OdbcType.Int).Value = DefSystemId;
            cmd.Parameters.Add("@UsrEmail", OdbcType.NVarChar).Value = UsrEmail;
            cmd.Parameters.Add("@UsrGroups", OdbcType.VarChar).Value = UsrGroups;
            cmd.Parameters.Add("@ForcePwdChg", OdbcType.Char).Value = ForcePwdChg ? "Y" : "N";
            cmd.Parameters.Add("@CustomerId", OdbcType.Int).Value = string.IsNullOrEmpty(CustomerId.ToString()) ? DBNull.Value : (object)CustomerId;
            cmd.Parameters.Add("@BrokerId", OdbcType.Int).Value = string.IsNullOrEmpty(BrokerId.ToString()) ? DBNull.Value : (object)BrokerId;
            cmd.Parameters.Add("@VendorId", OdbcType.Int).Value = string.IsNullOrEmpty(VendorId.ToString()) ? DBNull.Value : (object)VendorId;
            cmd.Parameters.Add("@Active", OdbcType.Char).Value = Active ? "Y" : "N";
            cmd.Parameters.Add("@SSOProviderCd", OdbcType.Char).Value = SSOProviderCd;
            cmd.Parameters.Add("@SSOLoginName", OdbcType.NVarChar).Value = SSOLoginName;
            cmd.CommandTimeout = 1800;
            da.SelectCommand = TransformCmd(cmd);
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

        public override void WrDelUsr(int UsrId)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("WrDelUsr", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
                TransformCmd(cmd).ExecuteNonQuery();
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

        public override string WrGetUsrOTPSecret(int UsrId, string hostSecret = null)
        {

            DataTable dt = new DataTable();
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("DECLARE @UsrId int SELECT @UsrId=? SET NOCOUNT ON SELECT DISTINCT UsrId, OTPSecret, TwoFactorAuth FROM dbo.Usr WHERE (UsrId = @UsrId)", cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 90;
            cmd.Parameters.Add("@UsrId", OdbcType.Int).Value = UsrId;
            try
            {
                da.SelectCommand = TransformCmd(cmd);
                da.Fill(dt);
                return string.IsNullOrEmpty(hostSecret) ? DecryptString(dt.Rows[0]["OTPSecret"].ToString()) : DecryptString(dt.Rows[0]["OTPSecret"].ToString(), hostSecret);
            }
            catch { return string.Empty; }
            finally { cn.Close(); }

        }

        public override string WrSetUsrOTPSecret(int UsrId, bool bEnable, string hostSecret = null)
        {
//            string Secret = Guid.NewGuid().ToString().Replace("-", "");
            string Secret = randomByteString(32); 
            string EncSecret = string.IsNullOrEmpty(hostSecret) ? EncryptString(Secret) : EncryptString(Secret, hostSecret);
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("DECLARE @UsrId int, @TwoFactorAuth CHAR(1), @OTPSecret VARCHAR(64) SELECT @UsrId=?, @TwoFactorAuth=?, @OTPSecret=? SET NOCOUNT ON UPDATE Usr SET OTPSecret = @OTPSecret, TwoFactorAuth = @TwoFactorAuth WHERE UsrId = @UsrId", cn);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@TwoFactorAuth", OdbcType.Char).Value = bEnable ? "Y" : "N";
                cmd.Parameters.Add("@OTPSecret", OdbcType.VarChar).Value = bEnable ? (object)(EncSecret) : System.DBNull.Value;
                TransformCmd(cmd).ExecuteNonQuery();
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

        public override DataTable GetUsrNotificationChannel(int UsrId, string FilterXml)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcCommand cmd = new OdbcCommand("GetUsrNotificationChannel", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@usrId", OdbcType.Numeric).Value = UsrId;
            cmd.Parameters.Add("@filterXml", OdbcType.VarChar).Value = string.IsNullOrEmpty(FilterXml) ? (object)System.DBNull.Value : FilterXml;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override void UpdUsrNotificationChannel(int UsrId, string DeviceId, string UserAgent, string ClientIP, string Fingerprint, string AppSig, string NotificationType)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("UpdUsrNotificationChannel", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            try
            {
                cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
                cmd.Parameters.Add("@DeviceId", OdbcType.VarChar).Value = DeviceId;
                cmd.Parameters.Add("@NotificationType", OdbcType.VarChar).Value = NotificationType;
                cmd.Parameters.Add("@UserAgent", OdbcType.VarChar).Value = UserAgent;
                cmd.Parameters.Add("@ClientIP", OdbcType.VarChar).Value = ClientIP;
                cmd.Parameters.Add("@Fingerprint", OdbcType.VarChar).Value = string.IsNullOrEmpty(Fingerprint) ? (object)DBNull.Value : (object)Fingerprint;
                cmd.Parameters.Add("@AppSig", OdbcType.VarChar).Value = string.IsNullOrEmpty(AppSig) ? (object)DBNull.Value : (object)AppSig; ;
                TransformCmd(cmd).ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback(); ApplicationAssert.CheckCondition(false, "UpdUsrNotificationChannel", "", e.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }
    }
}