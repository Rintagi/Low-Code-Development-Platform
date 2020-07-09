namespace RO.Access3.Odbc
{
	using System;
	using System.Data;
	//using System.Data.OleDb;
    using System.Data.Odbc;
    using System.Linq;
    using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class GenScreensAccess : GenScreensAccessBase, IDisposable
	{
		private OdbcDataAdapter da;
	
		public GenScreensAccess()
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

        // Make sure there is at least one default row in ScreenTab.
        public override void SetScrTab(CurrSrc CSrc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("SetScrTab", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetScrTab", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

        public override void SetScrNeedRegen(Int32 screenId, CurrSrc CSrc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("SetScrNeedRegen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetScrNeedRegen", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

		public override DataTable GetScreenById(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("GetScreenById", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = CPrj.SrcDesDatabase;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = CSrc.SrcDbDatabase;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenById", "Screen Issue", "Information for Screen #'" + screenId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetScreenColumns(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenColumns",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetScreenColumns", "Screen Columns Issue", "Columns for Screen #'" + screenId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetDistinctScreenTab(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetDistinctScreenTab",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetDistinctScreenTab", "Screen Issue", "Tab Folder numbers not available for Screen #'" + screenId.ToString() + "'!");
			return dt;
		}

		public override DataTable GetScreenCriteria(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenCriteria",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

        public override DataTable GetObjGroupCol(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
            OdbcCommand cmd = new OdbcCommand("GetObjGroupCol", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void GetScreenObjDdlById(Int32 screenId, Int32 screenObjId, string procedureName, string createProcedure, string appDatabase, string sysDatabase, string desDatabase, string pKey, string multiDesignDb, CurrSrc CSrc)
		{
			if (da == null) { throw new System.ObjectDisposedException( GetType().FullName ); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("GetScreenObjDdlById", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			cmd.Parameters.Add("@screenObjId", OdbcType.Numeric).Value = screenObjId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@createProcedure", OdbcType.Char).Value = createProcedure;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@pKey", OdbcType.VarChar).Value = pKey;
			cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
            //da.SelectCommand = TransformCmd(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenObjDdlById", "Screen DropDown Issue", "Dropdown information for ScreenObjId #'" + screenObjId.ToString() + "' not available!");
            //return dt;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

		public override DataTable GetScreenCriDdlById(Int32 screenId, Int32 screenCriId, string procedureName, string createProcedure, string appDatabase, string sysDatabase, string desDatabase, string pKey, string multiDesignDb, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenCriDdlById",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			cmd.Parameters.Add("@screenCriId", OdbcType.Numeric).Value = screenCriId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@createProcedure", OdbcType.Char).Value = createProcedure;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@pKey", OdbcType.VarChar).Value = pKey;
			cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenCriDdlById", "Screen DropDown Issue", "Dropdown information for ScreenCriId #'" + screenCriId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetScreenLisI1ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenLisI1ById",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
			cmd.Parameters.Add("@sysProgram", OdbcType.Char).Value = sysProgram;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenLisI1ById", "Screen List Issue", "List Procedures information for ScreenId #'" + screenId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetScreenLisI2ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenLisI2ById",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
			cmd.Parameters.Add("@sysProgram", OdbcType.Char).Value = sysProgram;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenLisI2ById", "Screen List Issue", "List Procedures information for ScreenId #'" + screenId.ToString() + "' not available!");
			return dt;
		}

		public override DataTable GetScreenLisI3ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenLisI3ById",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
			cmd.Parameters.Add("@sysProgram", OdbcType.Char).Value = sysProgram;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenLisI3ById", "Screen List Issue", "List Procedures information for ScreenId #'" + screenId.ToString() + "' not available!");
			return dt;
		}

        //public override DataTable GetAdvRule(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    if (da == null)
        //    {
        //        throw new System.ObjectDisposedException(GetType().FullName);
        //    }
        //    OdbcCommand cmd = new OdbcCommand("GetAdvRule", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
        //    da.SelectCommand = TransformCmd(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}

		public override DataTable GetWebRule(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetWebRule",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
            da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override DataTable GetServerRule(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc, UsrImpr ui, UsrCurr uc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetServerRule",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
            if (ui != null && uc != null)
            {
                cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = ui.Usrs;
                cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
                cmd.Parameters.Add("@Customers", OdbcType.VarChar).Value = ui.Customers;
                cmd.Parameters.Add("@Vendors", OdbcType.VarChar).Value = ui.Vendors;
                cmd.Parameters.Add("@Members", OdbcType.VarChar).Value = ui.Members;
                cmd.Parameters.Add("@Investors", OdbcType.VarChar).Value = ui.Investors;
                cmd.Parameters.Add("@Agents", OdbcType.VarChar).Value = ui.Agents;
                cmd.Parameters.Add("@Brokers", OdbcType.VarChar).Value = ui.Brokers;
                cmd.Parameters.Add("@UsrGroups", OdbcType.VarChar).Value = ui.UsrGroups;
                cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
                cmd.Parameters.Add("@Projects", OdbcType.VarChar).Value = ui.Projects;
                cmd.Parameters.Add("@Cultures", OdbcType.VarChar).Value = ui.Cultures;
                cmd.Parameters.Add("@Borrowers", OdbcType.VarChar).Value = ui.Borrowers;
                cmd.Parameters.Add("@Guarantors", OdbcType.VarChar).Value = ui.Guarantors;
                cmd.Parameters.Add("@Lenders", OdbcType.VarChar).Value = ui.Lenders;
                cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
                cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
            }

			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

        public override DataTable GetScreenAud(Int32 screenId, string screenTypeName, string desDatabase, string multiDesignDb, CurrSrc CSrc)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OdbcCommand cmd = new OdbcCommand("GetScreenAud", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
            cmd.Parameters.Add("@screenTypeName", OdbcType.Char).Value = screenTypeName;
            cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
            cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //public override DataTable GetScreenCud(Int32 screenId, string screenTypeName, string desDatabase, string multiDesignDb, CurrSrc CSrc)
        //{
        //    if (da == null)
        //    {
        //        throw new System.ObjectDisposedException( GetType().FullName );
        //    }
        //    OdbcCommand cmd = new OdbcCommand("GetScreenCud", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword))));
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
        //    cmd.Parameters.Add("@screenTypeName", OdbcType.Char).Value = screenTypeName;
        //    cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
        //    cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
        //    da.SelectCommand = TransformCmd(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}

        //public override void MkScreenUpd(Int32 screenId, string screenTypeName, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram)
        //{
        //    if (da == null)
        //    {
        //        throw new System.ObjectDisposedException( GetType().FullName );
        //    }
        //    OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
        //    cn.Open();
        //    OdbcCommand cmd = new OdbcCommand("MkScreenUpd", cn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
        //    cmd.Parameters.Add("@screenTypeName", OdbcType.Char).Value = screenTypeName;
        //    cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
        //    cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
        //    cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
        //    cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
        //    cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
        //    cmd.Parameters.Add("@sysProgram", OdbcType.Char).Value = sysProgram;
        //    try {TransformCmd(cmd).ExecuteNonQuery();}
        //    catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
        //    finally {cn.Close();}
        //    return;
        //}

		public override void MkScreenUpdIn(Int32 screenId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("MkScreenUpdIn", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		public override DataTable GetScreenDel(string srcDatabase, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenDel",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = srcDatabase;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void DelScreenDel(string srcDatabase, string appDatabase, string desDatabase, string programName, Int32 screenId, string multiDesignDb, string sysProgram, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("DelScreenDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@srcDatabase", OdbcType.VarChar).Value = srcDatabase;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@desDatabase", OdbcType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@programName", OdbcType.VarChar).Value = programName;
			cmd.Parameters.Add("@screenId", OdbcType.Numeric).Value = screenId;
			cmd.Parameters.Add("@multiDesignDb", OdbcType.Char).Value = multiDesignDb;
			cmd.Parameters.Add("@sysProgram", OdbcType.Char).Value = sysProgram;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}

		public override DataTable GetScreenCriDel(Int32 ScreenId, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcCommand cmd = new OdbcCommand("GetScreenCriDel",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OdbcType.Numeric).Value = ScreenId;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public override void DelScreenCriDel(string appDatabase, string procedureName, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("DelScreenCriDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@procedureName", OdbcType.VarChar).Value = procedureName;
			try {TransformCmd(cmd).ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}

		public override string GetSByteOle(string DataTypeName, CurrPrj CPrj)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CPrj.SrcDesConnectionString + DecryptString(CPrj.SrcDesPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("GetSByteOle", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@DataTypeName", OdbcType.VarChar).Value = DataTypeName;
			string sbole = (string)TransformCmd(cmd).ExecuteScalar();
			cmd.Dispose();
			cmd = null;
			cn.Close();
			ApplicationAssert.CheckCondition(sbole != null && sbole != string.Empty, "GetSByteOle", "Invalid Data Type", "Business rule data type '" + DataTypeName + "' not recognized!");
			return sbole;
		}

		public override string GetDByteOle(string DataTypeName, CurrPrj CPrj)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CPrj.SrcDesConnectionString + DecryptString(CPrj.SrcDesPassword)));
			cn.Open();
			OdbcCommand cmd = new OdbcCommand("GetDByteOle", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@DataTypeName", OdbcType.VarChar).Value = DataTypeName;
			string dbole = (string)TransformCmd(cmd).ExecuteScalar();
			cmd.Dispose();
			cmd = null;
			cn.Close();
			ApplicationAssert.CheckCondition(dbole != null && dbole != string.Empty, "GetDByteOle", "Invalid Data Type", "Business rule data type '" + DataTypeName + "' not recognized!");
			return dbole;
		}

        public override void MkScrAudit(string CudAction, Int32 ScreenId, string MasterTable, string Gen, string MultiDesignDb, CurrSrc CSrc, string appDatabase, string sysDatabase)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcConnection cn =  new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("MkScrAudit", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CudAction", OdbcType.Char).Value = CudAction;
            cmd.Parameters.Add("@ScreenId", OdbcType.Numeric).Value = ScreenId;
            cmd.Parameters.Add("@MasterTable", OdbcType.Char).Value = MasterTable;
            cmd.Parameters.Add("@Gen", OdbcType.Char).Value = Gen;
            cmd.Parameters.Add("@MultiDesignDb", OdbcType.Char).Value = MultiDesignDb;
            cmd.Parameters.Add("@appDatabase", OdbcType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OdbcType.VarChar).Value = sysDatabase;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); }
        }
    }
}