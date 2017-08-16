using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;

namespace RO.Access3
{
	public class WebPartAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;

		public WebPartAccess()
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
					if( da.SelectCommand.Connection != null)
					{
						da.SelectCommand.Connection.Dispose();
					}
					da.SelectCommand.Dispose();
				}
				da.Dispose();
				da = null;
			}
		}

        public DataTable GetDdlDshFldDtl(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetDdlDshFldDtl", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		public DataTable GetDdlDshFld(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OleDbCommand cmd = new OleDbCommand("GetDdlDshFld", new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			if (bAll) { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OleDbType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OleDbType.Numeric).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@currCompanyId", OleDbType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OleDbType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = cmd;
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

        public void UpdDshFldDtl(string PublicAccess, string DshFldDtlId, string DshFldId, string DshFldDtlName, string DshFldDtlDesc, Int32 UsrId, string SystemId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbTransaction tr = cn.BeginTransaction();
            OleDbCommand cmd = new OleDbCommand("UpdDshFldDtl", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@PublicAccess", OleDbType.Char).Value = PublicAccess;
            if (DshFldDtlId == string.Empty)
            {
                cmd.Parameters.Add("@DshFldDtlId", OleDbType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@DshFldDtlId", OleDbType.Numeric).Value = DshFldDtlId;
            }
            if (DshFldId == string.Empty)
            {
                ApplicationAssert.CheckCondition(false, "WebPartAccess", "UpdDshFldDtl", "Please select a folder and try again.");
            }
            else
            {
                cmd.Parameters.Add("@DshFldId", OleDbType.Numeric).Value = DshFldId;
            }
            if (DshFldDtlName == string.Empty)
            {
                ApplicationAssert.CheckCondition(false, "WebPartAccess", "UpdDshFldDtl", "Please enter a name for this memorized criteria and try again.");
            }
            else
            {
                cmd.Parameters.Add("@DshFldDtlName", OleDbType.VarWChar).Value = DshFldDtlName;
            }
            if (DshFldDtlDesc == string.Empty)
            {
                cmd.Parameters.Add("@DshFldDtlDesc", OleDbType.VarWChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@DshFldDtlDesc", OleDbType.VarWChar).Value = DshFldDtlDesc;
            }
            cmd.Parameters.Add("@UsrId", OleDbType.Numeric).Value = UsrId;
            cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
            try
            {
                da.UpdateCommand = cmd;
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                ApplicationAssert.CheckCondition(false, "", "", e.Message);
            }
            finally
            {
                cn.Close();
            }
            return;
        }
    }
}
