using System;
using System.Text;
using System.Data;
//using System.Data.OleDb;
using System.Data.Odbc;
using System.Linq;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;

namespace RO.Access3.Odbc
{
	public class WebPartAccess : WebPartAccessBase, IDisposable
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

		public WebPartAccess()
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

        public override DataTable GetDdlDshFldDtl(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("GetDdlDshFldDtl", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			if (bAll) { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		public override DataTable GetDdlDshFld(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
			OdbcCommand cmd = new OdbcCommand("GetDdlDshFld", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			if (bAll) { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "Y"; } else { cmd.Parameters.Add("@bAll", OdbcType.Char).Value = "N"; }
			if (keyId == string.Empty)
			{
				cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = System.DBNull.Value;
			}
			else
			{
				cmd.Parameters.Add("@keyId", OdbcType.Numeric).Value = keyId;
			}
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@currCompanyId", OdbcType.Numeric).Value = uc.CompanyId;
			cmd.Parameters.Add("@currProjectId", OdbcType.Numeric).Value = uc.ProjectId;
			cmd.CommandTimeout = 1800;
			da.SelectCommand = TransformCmd(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

        public override void UpdDshFldDtl(string PublicAccess, string DshFldDtlId, string DshFldId, string DshFldDtlName, string DshFldDtlDesc, Int32 UsrId, string SystemId, string dbConnectionString, string dbPassword)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcTransaction tr = cn.BeginTransaction();
            OdbcCommand cmd = new OdbcCommand("UpdDshFldDtl", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Transaction = tr;
            cmd.Parameters.Add("@PublicAccess", OdbcType.Char).Value = PublicAccess;
            if (DshFldDtlId == string.Empty)
            {
                cmd.Parameters.Add("@DshFldDtlId", OdbcType.Numeric).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@DshFldDtlId", OdbcType.Numeric).Value = DshFldDtlId;
            }
            if (DshFldId == string.Empty)
            {
                ApplicationAssert.CheckCondition(false, "WebPartAccess", "UpdDshFldDtl", "Please select a folder and try again.");
            }
            else
            {
                cmd.Parameters.Add("@DshFldId", OdbcType.Numeric).Value = DshFldId;
            }
            if (DshFldDtlName == string.Empty)
            {
                ApplicationAssert.CheckCondition(false, "WebPartAccess", "UpdDshFldDtl", "Please enter a name for this memorized criteria and try again.");
            }
            else
            {
                cmd.Parameters.Add("@DshFldDtlName", OdbcType.NVarChar).Value = DshFldDtlName;
            }
            if (DshFldDtlDesc == string.Empty)
            {
                cmd.Parameters.Add("@DshFldDtlDesc", OdbcType.NVarChar).Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@DshFldDtlDesc", OdbcType.NVarChar).Value = DshFldDtlDesc;
            }
            cmd.Parameters.Add("@UsrId", OdbcType.Numeric).Value = UsrId;
            cmd.Parameters.Add("@SystemId", OdbcType.Numeric).Value = SystemId;
            try
            {
                da.UpdateCommand = cmd;
                TransformCmd(cmd).ExecuteNonQuery();
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
