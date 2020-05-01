namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
    using RO.Common3;
	using RO.SystemFramewk;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using System.Linq;

	public class DeployAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;
	
		public DeployAccess()
		{
			da = new OleDbDataAdapter();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true); // as a service to those who might inherit from us
		}

        public static SqlDbType ToSqlDbType(Type type)
        {
            SqlParameter p1 = new SqlParameter();
            System.ComponentModel.TypeConverter tc;
            tc = System.ComponentModel.TypeDescriptor.GetConverter(p1.DbType);

            if (tc.CanConvertFrom(type))
                p1.DbType = (DbType)tc.ConvertFrom(type.Name);
            else
            {
                try
                {
                    p1.DbType = (DbType)tc.ConvertFrom(type.Name);
                }
                catch { }
            }

            return p1.SqlDbType;
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

		// Make sure the target file does not exist as this backup is not overwriting:
		public void BackupDb(string dbProviderCd, string connStr, string pwd, string dbName, string bkFile)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			string ss;
			if (dbProviderCd == "S")
			{
				ss = "DUMP DATABASE " + dbName + " TO '" + bkFile +"'";
			}
			else
			{
				ss = "BACKUP DATABASE " + dbName + " TO DISK = '" + bkFile +"'";
			}
			OleDbConnection cn =  new OleDbConnection(connStr + DecryptString(pwd));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand(ss, cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 10000;
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				if (dbProviderCd == "S")
				{
					if (e.Message.IndexOf("No error information available") < 0)	// Dump&Load always throw exception regardless.
					{
						ApplicationAssert.CheckCondition(false, "DeployAccess.BackupDb", "", e.Message);
					}
				}
				else
				{
					ApplicationAssert.CheckCondition(false, "DeployAccess.BackupDb", "", e.Message);
				}
			}
			finally
			{
				System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));	//Temporary to avoid restore error due to db in use.
				cn.Close();
				cn = null;
			}
			return;
		}

		public void OnlineDb(string connStr, string pwd, string dbName)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbConnection cn =  new OleDbConnection(connStr + DecryptString(pwd));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("ONLINE DATABASE " + dbName, cn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 4000;
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				ApplicationAssert.CheckCondition(false, "DeployAccess.OnlineDb", "", e.Message);
			}
			finally
			{
				cn.Close();
				cn = null;
			}
			return;
		}

		public void RestoreWaDb(string dbProviderCd, string connStr, string pwd, string waDb, string waFile, string waPath)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}         
			OleDbConnection cn =  new OleDbConnection(connStr + DecryptString(pwd));
			cn.Open();
			OleDbCommand cmd;
			if (dbProviderCd == "S")
			{
				cmd = new OleDbCommand("LOAD DATABASE " + waDb + " FROM '" + waFile + "'", cn);
			}
			else	//Ms Windows only for now, not Linux
			{
				cmd = new OleDbCommand("RESTORE FILELISTONLY FROM DISK = '" + waFile +"'", cn);
				cmd.CommandType = CommandType.Text;
				da.SelectCommand = cmd;
				DataTable dt = new DataTable();
				da.Fill(dt);
				ApplicationAssert.CheckCondition(dt.Rows.Count != 0 , "DeployAccess.RestoreWaDb", "", "Resotre file does not have any logical file name:" + waFile);
				string restoreCmd = "RESTORE DATABASE " + waDb + " FROM DISK = '" + waFile +"' WITH ";
				foreach(DataRow dr in dt.Rows)
				{
					if (dr["Type"].ToString() == "L")
						restoreCmd += "MOVE '" + dr["LogicalName"].ToString() + "' TO '" + waPath + waDb + ".ldf',";
					else 
						restoreCmd += "MOVE '" + dr["LogicalName"].ToString() + "' TO '" + waPath + waDb + ".mdf',";
				}
				if (restoreCmd.EndsWith(",")) restoreCmd = restoreCmd.Remove(restoreCmd.Length-1, 1);
				cn.Close();
				cn.Open();
				cmd = new OleDbCommand(restoreCmd, cn);
			}
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 10000;
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				if (e.Message.IndexOf("No error information available") < 0)	// Dump&Load always throw exception regardless.
				{
					ApplicationAssert.CheckCondition(false, "DeployAccess.RestoreWaDb", "", e.Message);
				}
			}
			finally
			{
				cn.Close();
				cn = null;
			}
			return;
		}

		public string GenRestoreScript(string dbProviderCd, string connStr, string pwd, string dbName, string iFileAbs, string rsFileRel, string dbPath)
		{
			string rsScript;
			if (dbProviderCd == "S")
			{
				rsScript = "LOAD DATABASE " + dbName + " FROM '" + iFileAbs +"'\r\nGO\r\nONLINE DATABASE " + dbName + "\r\nGO\r\n";
			}
			else	//Ms Windows only for now, not Linux
			{
				if (da == null)
				{
					throw new System.ObjectDisposedException( GetType().FullName );
				}         
				OleDbConnection cn =  new OleDbConnection(connStr + DecryptString(pwd));
				cn.Open();
				OleDbCommand cmd = new OleDbCommand("RESTORE FILELISTONLY FROM DISK = '" + iFileAbs +"'", cn);
				cmd.CommandType = CommandType.Text;
				da.SelectCommand = cmd;
				DataTable dt = new DataTable();
				da.Fill(dt);
				ApplicationAssert.CheckCondition(dt.Rows.Count != 0 , "DeployAccess.GenRestoreScript", "", "Cannot resotre file: " + iFileAbs + "!");
				rsScript = "\"RESTORE DATABASE " + dbName + " FROM DISK = '%CD%\\" + rsFileRel +"' WITH REPLACE, ";
				foreach(DataRow dr in dt.Rows)
				{
					if (dr["Type"].ToString() == "L")
						rsScript += "MOVE '" + dr["LogicalName"].ToString() + "' TO '" + dbPath + dbName + "_Log.LDF',";
					else 
						rsScript += "MOVE '" + dr["LogicalName"].ToString() + "' TO '" + dbPath + dbName + "_Data.MDF',";
				}
				if (rsScript.EndsWith(",")) {rsScript = rsScript.Remove(rsScript.Length-1, 1);}
				rsScript = rsScript + "\" >> Install.log";
				cn.Close();
			}
			return rsScript;
		}
		
		public void DropDb(string dbProviderCd, string connStr, string pwd, string dbName)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}         
			if (dbProviderCd == "S")
			{
				System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));	//Temporary to avoid database still in use by previous call.
			}
			OleDbConnection cn =  new OleDbConnection(connStr + DecryptString(pwd));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DROP DATABASE " + dbName, cn);
			cmd.CommandType = CommandType.Text;
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				ApplicationAssert.CheckCondition(false, "DeployAccess.DropDb", "", e.Message);
			}
			finally
			{
				cn.Close();
				cn = null;
			}
			return;
		}

		public void TruncateTable(string connStr, string pwd, string tableName)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}         
			OleDbConnection cn =  new OleDbConnection(connStr + DecryptString(pwd));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DELETE FROM " + tableName, cn);
			cmd.CommandType = CommandType.Text;
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				ApplicationAssert.CheckCondition(false, "DeployAccess.TruncateTable", "", e.Message);
			}
			finally
			{
				cn.Close();
				cn = null;
			}
			return;
		}

		public bool DbExists(string connStr, string pwd)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}      
			OleDbConnection cn =  new OleDbConnection(connStr + DecryptString(pwd));
			bool ret = true;
			try
			{
				cn.Open();
			}
			catch(Exception e)
			{
				ApplicationAssert.CheckCondition((e.Message.IndexOf("no entry found") >= 0) || (e.Message.IndexOf("Cannot open database") >= 0), "DeployAccess.DbExists", "Check database existence", e.Message);
				ret = false;
			}
			finally
			{
				cn.Close();
				cn = null;
			}
			return ret;
		}
        public void MkDBOwner(string connStr, string usr, string pwd, string dbName)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbConnection cn = new OleDbConnection(connStr + DecryptString(pwd));
            cn.Open();
            string ss = ""
                        + " DECLARE @userid int "
                        + " USE " + dbName + " "
                        + " select @userid=principal_id from sys.database_principals where sid = SUSER_SID(N'" + usr + "')"
                        + " IF @userid IS NULL BEGIN"
                        + "     CREATE USER " + usr + " FOR LOGIN " + usr + " "
                        + "     EXEC('sp_addrolemember ''db_owner'', ''" + usr + "''')"
                        + " END"; OleDbCommand cmd = new OleDbCommand(ss, cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 4000;
            try
            {
                if (usr.ToLower() != "sa")
                {
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                ApplicationAssert.CheckCondition(false, "DeployAccess.DbCreate", "", e.Message);
            }
            finally
            {
                cn.Close();
                cn = null;
            }
            return;
        }
		// Makes sure on Sybase all design databases including ??Design are the same size as below.
        public void DbCreate(string connStr, string pwd, string dbName)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbConnection cn = new OleDbConnection(connStr + DecryptString(pwd));
            cn.Open();

            string ss = "EXEC('CREATE DATABASE " + dbName
                //                + " ON WorkArea_dat = 60 LOG ON WorkArea_log = 60 "
                + "')"
                /* these are not necessary and also doesn't work if the database login doesn't have
                 * full sa right
                 * also the dblogin must have CREATE ANY DATABASE permission to run this which is granted by
                 *
                 * USE master
                 * GRANT CREATE ANY DATABASE TO <whatever login>
                 * 
                 * 2015.9.24 gary
                 * */
                /* sp_dboption removed in SQL server 2012.7.25 gary */
                //+ " EXEC ('ALTER DATABASE " + dbName + " SET RECOVERY SIMPLE')"
                ////+ " EXEC('master.dbo.sp_dboption " + dbName + ", ''select into/bulkcopy'', true')"
                ////+ " EXEC('master.dbo.sp_dboption " + dbName + ", ''trunc log on chkpt'', true')";
                //+ " EXEC ('declare @serverName nvarchar(max) select @serverName = @@serverName exec sp_serveroption @serverName ,''data access'',''true''')";
                ;
            OleDbCommand cmd = new OleDbCommand(ss, cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 4000;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ApplicationAssert.CheckCondition(false, "DeployAccess.DbCreate", "", e.Message);
            }
            finally
            {
                cn.Close();
                cn = null;
            }
            return;
        }

		public DataTable GetReleaseInf(int releaseId)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetReleaseInf", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReleaseId", OleDbType.Numeric).Value = releaseId == -1 ? (object) DBNull.Value : (object) releaseId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1 || releaseId == -1, "GetReleaseInf", "", "Release information not avaliable for Id# " + releaseId.ToString() + "!");
			return dt;
		}

		public DataTable GetReleaseDtl(int releaseId)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetReleaseDtl", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReleaseId", OleDbType.Numeric).Value = releaseId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetReleaseVer(int ReleaseId, string EntityCode)
		{
			if (da == null) {throw new System.ObjectDisposedException(GetType().FullName);}
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetReleaseVer", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReleaseId", OleDbType.Numeric).Value = ReleaseId;
			cmd.Parameters.Add("@EntityCode", OleDbType.VarChar).Value = EntityCode;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetYrReadme(int ReleaseId, string EntityCode)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetYrReadme", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReleaseId", OleDbType.Numeric).Value = ReleaseId;
			cmd.Parameters.Add("@EntityCode", OleDbType.VarChar).Value = EntityCode;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}
        private DataTable GetData(string sql, string connStr, string pwd)
        {
            using (OleDbCommand cmd = new OleDbCommand(sql, new OleDbConnection(connStr + DecryptString(pwd))))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable GetTables(string connStr, string pwd, string dbName)
        {
            return GetData("USE " + dbName + " SELECT so.name AS tbName,(SELECT 1 FROM dbo.syscolumns WHERE id=so.id AND (status & 128) = 128) AS hasIdentity FROM dbo.sysobjects so WHERE type = 'U' AND name <> 'dtproperties' ORDER BY so.name", connStr, pwd);
        }

        public void DbExec(string sql, string connStr, string pwd, string dbName)
        {
            using (OleDbConnection cn = new OleDbConnection(connStr + DecryptString(pwd)))
            {
                cn.Open();
                OleDbTransaction tr = cn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1800;
                cmd.Transaction = tr;
                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }
                catch (Exception e) { tr.Rollback(); throw e; }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void UpdateRelease(string server, string desDBName, string user, string pwd, string nmSpace, string moduleName)
        {
            SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder();
            connStr.UserID = user;
            connStr.Password = DecryptString(pwd);
            connStr.InitialCatalog = desDBName;
            connStr.DataSource = server;
            connStr.IntegratedSecurity = false;
            //connStr.Pooling = false;
            connStr.ConnectTimeout = 30;
            using (SqlConnection conn = new SqlConnection(connStr.ConnectionString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT ReleaseDtlId, ObjectName FROM ReleaseDtl WHERE ObjectType = 'D' ", conn, tr);
                da.UpdateCommand = new SqlCommand("UPDATE ReleaseDtl Set ObjectName = @ObjectName WHERE ReleaseDtlId = @ReleaseDtlId ", conn, tr);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string module = nmSpace + moduleName;
                    string cmon = nmSpace + "Cmon";
                    foreach (DataRow dr in dt.Rows)
                    {
                        string[] dbs = (from db in dr["ObjectName"].ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries) select db.Trim().ToLower()).ToArray();
                        if (dbs.Contains(cmon.ToLower().Trim() + "d")) 
                        {
                            if (!dbs.Contains(module.ToLower().Trim() + "d")) dr["ObjectName"] = dr["ObjectName"].ToString() + " |" + module + "D";
                        }
                        else if (dbs.Contains(cmon.ToLower().Trim()))
                        {
                            if (!dbs.Contains(module.ToLower().Trim())) dr["ObjectName"] = dr["ObjectName"].ToString() + " |" + module;
                        }
                    }
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string name = dc.ColumnName.ToLower().Trim();
                        da.UpdateCommand.Parameters.Add("@" + name, ToSqlDbType(dc.DataType), -1, name);
                    }
                    da.UpdateBatchSize = 0;
                    da.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                    da.AcceptChangesDuringUpdate = false;
                    da.ContinueUpdateOnError = false;
                    try
                    {
                        da.Update(dt);
                        tr.Commit();
                    }
                    catch
                    {
                        tr.Rollback();
                        throw;
                    }
                }
                else
                {
                    tr.Commit();
                }
            }
        }
        public List<string> FixMetaReference(string connStr, string pwd, string desDBName, string srcNmSpace, string nmSpace, string moduleName, List<string> modifiedBy, List<string> systemId, DataTable srcSystemMap, DataTable tarSystemMap)
        {
            List<string> failedEntries = new List<string>();

            using (OleDbConnection cn = new OleDbConnection(connStr + DecryptString(pwd)))
            {
                cn.Open();
                foreach (string f in modifiedBy)
                {
                    string[] col = f.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    string updateCmd = string.Format(@"
update t
set {0} = ISNULL(u.UsrId,t.{0})
from {1} t,( select top 1 * from {2}.dbo.Usr order by UsrId) u 
where not exists(select top 1 1 from {2}.dbo.Usr u where u.UsrId = t.{0})
", col[1].Trim(), col[0].Trim(), desDBName);
                    OleDbTransaction tr = cn.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand(updateCmd, cn,tr);
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        tr.Commit();
                    }
                    catch  { tr.Rollback(); }
                }
                Dictionary<byte, string> oldSystem = new Dictionary<byte, string>();
                Dictionary<string, byte> newSystem = new Dictionary<string, byte>();
                Regex nmSpaceRe = new Regex("^" + srcNmSpace, RegexOptions.IgnoreCase);
                foreach (DataRow dr in srcSystemMap.Rows)
                {
                    oldSystem[(byte) dr["SystemId"]] = dr["dbAppDataBase"].ToString();
                }
                foreach (DataRow dr in tarSystemMap.Rows)
                {
                    newSystem[dr["dbAppDataBase"].ToString()] = (byte)dr["SystemId"];
                }
                foreach (string f in systemId)
                {
                    string[] col = f.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da.SelectCommand = new OleDbCommand("SELECT * FROM " + col[0], cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        string key = string.Empty;
                        try
                        {
                            byte oldSystemId = (byte)dr["SystemId"];
                            key = dr[col[1]].ToString();
                            string oldDbName = oldSystem[oldSystemId];
                            string newDbName = nmSpaceRe.Replace(oldDbName, nmSpace);
                            byte newSystemId = newSystem[newDbName];
                            OleDbTransaction tr = cn.BeginTransaction();
                            OleDbCommand updCmd = new OleDbCommand(string.Format(@"update {0} set {1}={2} WHERE {3} = {4} ", col[0], col[2], newSystemId.ToString(), col[1], key.ToString()), cn, tr);
                            try
                            {
                                updCmd.ExecuteNonQuery();
                                tr.Commit();
                            }
                            catch  { tr.Rollback(); }
                        }
                        catch { failedEntries.Add(f + ": key(" + col[1] + ")->" + key); }

                    }
                }
                return failedEntries;
            }

        }
        public int AddSystem(string server, string desDBName, string user, string pwd, string nmSpace, string moduleName, string oledbconnectionstring)
        {
            SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder();
            connStr.UserID = user;
            connStr.Password = DecryptString(pwd);
            connStr.InitialCatalog = desDBName;
            connStr.DataSource = server;
            connStr.IntegratedSecurity = false;
            //connStr.Pooling = false;
            connStr.ConnectTimeout = 30;
            using (SqlConnection conn = new SqlConnection(connStr.ConnectionString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT * FROM Systems ", conn, tr);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.InsertCommand = builder.GetInsertCommand(true);
                int newSystemId = 1;
                bool alreadyExists = false;
                foreach (DataRow dr in dt.Rows)
                {
                    byte systemId = (byte) dr["SystemId"];
                    if (systemId >= newSystemId) newSystemId = systemId + 1;
                    if (dr["dbAppDataBase"].ToString().Trim() == nmSpace + moduleName)
                    {
                        alreadyExists = true;
                        newSystemId = systemId;
                        break;
                    }
                }
                DataRow nDr = dt.NewRow();
                nDr.ItemArray = dt.Rows[0].ItemArray;
                nDr["SystemId"] = byte.Parse(newSystemId.ToString());
                nDr["dbAppDataBase"] = nmSpace + moduleName;
                nDr["dbDesDataBase"] = nmSpace + moduleName + "D";
                nDr["SystemName"] = nmSpace + moduleName;
                nDr["SysProgram"] = "N";
                try
                {
                    if (!alreadyExists)
                    {
                        dt.Rows.Add(nDr);
                        da.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        da.Update(dt);

                        string appUser = nDr["dbAppUserId"].ToString();
                        MkDBOwner(oledbconnectionstring, appUser, pwd, nmSpace + moduleName);
                        MkDBOwner(oledbconnectionstring, appUser, pwd, nmSpace + moduleName + "D");

                        tr.Commit();
                    }
                }
                catch
                {
                    tr.Rollback();
                    throw;
                }
                return newSystemId;
            }
        }
        public void TransferTable(string srcServer, string srcDb, string srcUser, string srcPwd, string tbName, string tarServer, string tarDb, string tarUser, string tarPwd, System.Collections.Generic.Dictionary<string, KeyValuePair<string, List<string[]>>> needTranslate, System.Collections.Generic.Dictionary<System.Text.RegularExpressions.Regex, System.Text.RegularExpressions.MatchEvaluator> reSimple, System.Collections.Generic.Dictionary<System.Text.RegularExpressions.Regex, System.Text.RegularExpressions.MatchEvaluator> reScript)
        {
            SqlConnectionStringBuilder srcConnStr = new SqlConnectionStringBuilder();
            srcConnStr.UserID = srcUser;
            srcConnStr.Password = DecryptString(srcPwd);
            srcConnStr.InitialCatalog = srcDb;
            srcConnStr.DataSource = srcServer;
            srcConnStr.IntegratedSecurity = false;
            //srcConnStr.Pooling = false;
            srcConnStr.ConnectTimeout = 30;
            using (SqlConnection srcConn = new SqlConnection(srcConnStr.ConnectionString))
            {
                srcConn.Open();
                SqlConnectionStringBuilder tarConnStr = new SqlConnectionStringBuilder();
                tarConnStr.UserID = tarUser;
                tarConnStr.Password = DecryptString(tarPwd);
                tarConnStr.InitialCatalog = tarDb;
                tarConnStr.DataSource = tarServer;
                tarConnStr.IntegratedSecurity = false;
                //tarConnStr.Pooling = false;
                tarConnStr.ConnectTimeout = 30;

                SqlCommand commandSourceData = new SqlCommand(
                "SELECT * " +
                "FROM " + tbName, srcConn);
                SqlDataReader reader = commandSourceData.ExecuteReader();
                using (SqlConnection tarConn = new SqlConnection(tarConnStr.ConnectionString))
                {
                    tarConn.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(tarConnStr.ConnectionString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = tbName;

                        try
                        {
                            bulkCopy.WriteToServer(reader);
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                    if (needTranslate.ContainsKey(tbName.ToLower().Trim()))
                    {
                        KeyValuePair<string, List<string[]>> colDef = needTranslate[tbName.ToLower().Trim()];
                        string kcol = colDef.Key;
                        string cols = string.Join(",", (from c in colDef.Value select c[0]).ToArray<string>());
                        string updCols = string.Join(",", (from c in colDef.Value select c[0] + " = @" + c[0]).ToArray<string>());
                        SqlTransaction tr = tarConn.BeginTransaction();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand("SELECT " + kcol + ", " + cols + " FROM " + tbName, tarConn, tr);
                        da.UpdateCommand = new SqlCommand("UPDATE " + tbName + " SET " + updCols + " WHERE " + kcol + "= @" + kcol, tarConn, tr);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                foreach (string[] cdf in colDef.Value)
                                {
                                    string val = dr[cdf[0]].ToString();
                                    Dictionary<Regex, MatchEvaluator> translateRE = cdf.Length > 1 ? reScript : reSimple;
                                    foreach (KeyValuePair<Regex, MatchEvaluator> kv in translateRE)
                                    {
                                        val = kv.Key.Replace(val, kv.Value);
                                    }
                                    dr[cdf[0]] = val;
                                }
                            }
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string name = dc.ColumnName.ToLower().Trim();
                                da.UpdateCommand.Parameters.Add("@" + name, ToSqlDbType(dc.DataType), name == kcol ? 4 : -1, name);
                            }
                            da.UpdateBatchSize = 0;
                            da.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                            da.AcceptChangesDuringUpdate = false;
                            da.ContinueUpdateOnError = false;
                            try
                            {
                                da.Update(dt);
                                tr.Commit();
                            }
                            catch
                            {
                                tr.Rollback();
                                throw;
                            }
                            finally
                            {
                                tarConn.Close();
                            }
                        }
                        else
                        {
                            tr.Commit();
                            tarConn.Close();
                        }
                    }
                    else
                    {
                        tarConn.Close();
                    }

                }
            }
        }
	}
}