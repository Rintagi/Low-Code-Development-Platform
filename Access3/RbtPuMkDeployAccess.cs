using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;

namespace RO.Access3
{
	public class AdmPuMkDeployAccess : AdmPuMkDeployAccessBase, IDisposable
	{
		private OleDbDataAdapter da;

		public AdmPuMkDeployAccess()
		{
			da = new OleDbDataAdapter();
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
			OleDbConnection cn = new OleDbConnection(GetDesConnStr());
			cn.Open();
			OleDbTransaction tr = cn.BeginTransaction();
			OleDbCommand cmd = new OleDbCommand("UpdReleaseBuild", cn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Transaction = tr;
			cmd.Parameters.Add("@ReleaseId", OleDbType.Numeric).Value = ReleaseId;
			cmd.Parameters.Add("@ReleaseBuild", OleDbType.VarChar).Value = ReleaseBuild;
			try
			{
				cmd.ExecuteNonQuery();
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