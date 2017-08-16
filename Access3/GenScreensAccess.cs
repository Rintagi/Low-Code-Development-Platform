namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class GenScreensAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;
	
		public GenScreensAccess()
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

        // Make sure there is at least one default row in ScreenTab.
        public void SetScrTab(CurrSrc CSrc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SetScrTab", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetScrTab", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

        public void SetScrNeedRegen(Int32 screenId, CurrSrc CSrc)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SetScrNeedRegen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetScrNeedRegen", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

		public DataTable GetScreenById(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetScreenById", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = CPrj.SrcDesDatabase;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = CSrc.SrcDbDatabase;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenById", "Screen Issue", "Information for Screen #'" + screenId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetScreenColumns(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenColumns",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetScreenColumns", "Screen Columns Issue", "Columns for Screen #'" + screenId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetDistinctScreenTab(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetDistinctScreenTab",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count > 0, "GetDistinctScreenTab", "Screen Issue", "Tab Folder numbers not available for Screen #'" + screenId.ToString() + "'!");
			return dt;
		}

		public DataTable GetScreenCriteria(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenCriteria",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

        public DataTable GetObjGroupCol(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
            OleDbCommand cmd = new OleDbCommand("GetObjGroupCol", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void GetScreenObjDdlById(Int32 screenId, Int32 screenObjId, string procedureName, string createProcedure, string appDatabase, string sysDatabase, string desDatabase, string pKey, string multiDesignDb, CurrSrc CSrc)
		{
			if (da == null) { throw new System.ObjectDisposedException( GetType().FullName ); }
            OleDbConnection cn = new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("GetScreenObjDdlById", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@screenObjId", OleDbType.Numeric).Value = screenObjId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@createProcedure", OleDbType.Char).Value = createProcedure;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@pKey", OleDbType.VarChar).Value = pKey;
			cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
            //da.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenObjDdlById", "Screen DropDown Issue", "Dropdown information for ScreenObjId #'" + screenObjId.ToString() + "' not available!");
            //return dt;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

		public DataTable GetScreenCriDdlById(Int32 screenId, Int32 screenCriId, string procedureName, string createProcedure, string appDatabase, string sysDatabase, string desDatabase, string pKey, string multiDesignDb, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenCriDdlById",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@screenCriId", OleDbType.Numeric).Value = screenCriId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@createProcedure", OleDbType.Char).Value = createProcedure;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@pKey", OleDbType.VarChar).Value = pKey;
			cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenCriDdlById", "Screen DropDown Issue", "Dropdown information for ScreenCriId #'" + screenCriId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetScreenLisI1ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenLisI1ById",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
			cmd.Parameters.Add("@sysProgram", OleDbType.Char).Value = sysProgram;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenLisI1ById", "Screen List Issue", "List Procedures information for ScreenId #'" + screenId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetScreenLisI2ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenLisI2ById",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
			cmd.Parameters.Add("@sysProgram", OleDbType.Char).Value = sysProgram;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenLisI2ById", "Screen List Issue", "List Procedures information for ScreenId #'" + screenId.ToString() + "' not available!");
			return dt;
		}

		public DataTable GetScreenLisI3ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenLisI3ById",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
			cmd.Parameters.Add("@sysProgram", OleDbType.Char).Value = sysProgram;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			ApplicationAssert.CheckCondition(dt.Rows.Count == 1, "GetScreenLisI3ById", "Screen List Issue", "List Procedures information for ScreenId #'" + screenId.ToString() + "' not available!");
			return dt;
		}

        //public DataTable GetAdvRule(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
        //{
        //    if (da == null)
        //    {
        //        throw new System.ObjectDisposedException(GetType().FullName);
        //    }
        //    OleDbCommand cmd = new OleDbCommand("GetAdvRule", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
        //    da.SelectCommand = cmd;
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}

		public DataTable GetWebRule(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetWebRule",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public DataTable GetServerRule(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetServerRule",new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

        public DataTable GetScreenAud(Int32 screenId, string screenTypeName, string desDatabase, string multiDesignDb, CurrSrc CSrc)
        {
            if (da == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            }
            OleDbCommand cmd = new OleDbCommand("GetScreenAud", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
            cmd.Parameters.Add("@screenTypeName", OleDbType.Char).Value = screenTypeName;
            cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
            cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //public DataTable GetScreenCud(Int32 screenId, string screenTypeName, string desDatabase, string multiDesignDb, CurrSrc CSrc)
        //{
        //    if (da == null)
        //    {
        //        throw new System.ObjectDisposedException( GetType().FullName );
        //    }
        //    OleDbCommand cmd = new OleDbCommand("GetScreenCud", new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword)));
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
        //    cmd.Parameters.Add("@screenTypeName", OleDbType.Char).Value = screenTypeName;
        //    cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
        //    cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
        //    da.SelectCommand = cmd;
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}

        //public void MkScreenUpd(Int32 screenId, string screenTypeName, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram)
        //{
        //    if (da == null)
        //    {
        //        throw new System.ObjectDisposedException( GetType().FullName );
        //    }
        //    OleDbConnection cn =  new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
        //    cn.Open();
        //    OleDbCommand cmd = new OleDbCommand("MkScreenUpd", cn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
        //    cmd.Parameters.Add("@screenTypeName", OleDbType.Char).Value = screenTypeName;
        //    cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
        //    cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
        //    cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
        //    cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
        //    cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
        //    cmd.Parameters.Add("@sysProgram", OleDbType.Char).Value = sysProgram;
        //    try {cmd.ExecuteNonQuery();}
        //    catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
        //    finally {cn.Close();}
        //    return;
        //}

		public void MkScreenUpdIn(Int32 screenId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("MkScreenUpdIn", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
			return;
		}

		public DataTable GetScreenDel(string srcDatabase, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenDel",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = srcDatabase;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void DelScreenDel(string srcDatabase, string appDatabase, string desDatabase, string programName, Int32 screenId, string multiDesignDb, string sysProgram, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DelScreenDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@srcDatabase", OleDbType.VarChar).Value = srcDatabase;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@desDatabase", OleDbType.VarChar).Value = desDatabase;
			cmd.Parameters.Add("@programName", OleDbType.VarChar).Value = programName;
			cmd.Parameters.Add("@screenId", OleDbType.Numeric).Value = screenId;
			cmd.Parameters.Add("@multiDesignDb", OleDbType.Char).Value = multiDesignDb;
			cmd.Parameters.Add("@sysProgram", OleDbType.Char).Value = sysProgram;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}

		public DataTable GetScreenCriDel(Int32 ScreenId, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbCommand cmd = new OleDbCommand("GetScreenCriDel",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}

		public void DelScreenCriDel(string appDatabase, string procedureName, string dbConnectionString, string dbPassword)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}            
			OleDbConnection cn =  new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("DelScreenCriDel", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@procedureName", OleDbType.VarChar).Value = procedureName;
			try {cmd.ExecuteNonQuery();}
			catch(Exception e) {ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString());}
			finally {cn.Close();}
		}

		public string GetSByteOle(string DataTypeName, CurrPrj CPrj)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(CPrj.SrcDesConnectionString + DecryptString(CPrj.SrcDesPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetSByteOle", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@DataTypeName", OleDbType.VarChar).Value = DataTypeName;
			string sbole = (string)cmd.ExecuteScalar();
			cmd.Dispose();
			cmd = null;
			cn.Close();
			ApplicationAssert.CheckCondition(sbole != null && sbole != string.Empty, "GetSByteOle", "Invalid Data Type", "Business rule data type '" + DataTypeName + "' not recognized!");
			return sbole;
		}

		public string GetDByteOle(string DataTypeName, CurrPrj CPrj)
		{
			if (da == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName );
			}
			OleDbConnection cn =  new OleDbConnection(CPrj.SrcDesConnectionString + DecryptString(CPrj.SrcDesPassword));
			cn.Open();
			OleDbCommand cmd = new OleDbCommand("GetDByteOle", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@DataTypeName", OleDbType.VarChar).Value = DataTypeName;
			string dbole = (string)cmd.ExecuteScalar();
			cmd.Dispose();
			cmd = null;
			cn.Close();
			ApplicationAssert.CheckCondition(dbole != null && dbole != string.Empty, "GetDByteOle", "Invalid Data Type", "Business rule data type '" + DataTypeName + "' not recognized!");
			return dbole;
		}

        public void MkScrAudit(string CudAction, Int32 ScreenId, string MasterTable, string Gen, string MultiDesignDb, CurrSrc CSrc, string appDatabase, string sysDatabase)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbConnection cn =  new OleDbConnection(CSrc.SrcConnectionString + DecryptString(CSrc.SrcDbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("MkScrAudit", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CudAction", OleDbType.Char).Value = CudAction;
            cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
            cmd.Parameters.Add("@MasterTable", OleDbType.Char).Value = MasterTable;
            cmd.Parameters.Add("@Gen", OleDbType.Char).Value = Gen;
            cmd.Parameters.Add("@MultiDesignDb", OleDbType.Char).Value = MultiDesignDb;
            cmd.Parameters.Add("@appDatabase", OleDbType.VarChar).Value = appDatabase;
			cmd.Parameters.Add("@sysDatabase", OleDbType.VarChar).Value = sysDatabase;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message.ToString()); }
            finally { cn.Close(); }
        }
    }
}