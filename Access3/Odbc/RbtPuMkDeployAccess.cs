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
	public class AdmPuMkDeployAccess : AdmPuMkDeployAccessBase, IDisposable
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

		public AdmPuMkDeployAccess()
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

				if(da.InsertCommand != null)
				{
					if( da.InsertCommand.Connection != null)
					{
						da.InsertCommand.Connection.Dispose();
					}
					da.InsertCommand.Dispose();
				}

				if(da.UpdateCommand != null)
				{
					if( da.UpdateCommand.Connection != null)
					{
						da.UpdateCommand.Connection.Dispose();
					}
					da.UpdateCommand.Dispose();
				}

				if(da.DeleteCommand != null)
				{
					if( da.DeleteCommand.Connection != null)
					{
						da.DeleteCommand.Connection.Dispose();
					}
					da.DeleteCommand.Dispose();
				}
				da.Dispose();
				da = null;
			}
		}

		public override bool UpdReleaseBuild(Int16 ReleaseId, string ReleaseBuild)
		{
			if (da == null) {throw new System.ObjectDisposedException( GetType().FullName );}
			OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
			cn.Open();
			OdbcTransaction tr = cn.BeginTransaction();
			OdbcCommand cmd = new OdbcCommand("UpdReleaseBuild", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Transaction = tr;
			cmd.Parameters.Add("@ReleaseId", OdbcType.Numeric).Value = ReleaseId;
			cmd.Parameters.Add("@ReleaseBuild", OdbcType.VarChar).Value = ReleaseBuild;
			try
			{
				TransformCmd(cmd).ExecuteNonQuery();
				tr.Commit();
			}
			catch(Exception e)
			{
				ApplicationAssert.CheckCondition(false, "", "", e.Message);
			}
			finally
			{
				cn.Close();
			}
			return true;
		}
	}
}