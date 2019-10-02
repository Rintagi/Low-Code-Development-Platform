namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using System.Drawing;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class MenuAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;
	
		public MenuAccess()
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

        // Make a dummy new menu item for launching a new screen, report or wizard.
        public void NewMenuItem(Int32 ScreenId, Int32 ReportId, Int32 WizardId, string ItemTitle, string dbConnectionString, string dbPassword)
        {
            OleDbConnection cn = new OleDbConnection(dbConnectionString + DecryptString(dbPassword));
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("NewMenuItem", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId;
            cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId;
            cmd.Parameters.Add("@WizardId", OleDbType.Numeric).Value = WizardId;
            cmd.Parameters.Add("@ItemTitle", OleDbType.VarWChar).Value = ItemTitle;
            cmd.CommandTimeout = 1800;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "NewMenuItem", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public DataTable GetMenu(Int16 CultureId, byte SystemId, UsrImpr ui, string dbConnectionString, string dbPassword, int? ScreenId, int? ReportId, int? WizardId)
        {
            //if (!dbConnectionString.Contains("Design")) checkValidLicense();
            if (da == null) { throw new System.ObjectDisposedException( GetType().FullName ); }            
			OleDbCommand cmd = new OleDbCommand("GetMenu",new OleDbConnection(dbConnectionString + DecryptString(dbPassword)));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OleDbType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@SystemId", OleDbType.Numeric).Value = SystemId;
			cmd.Parameters.Add("@RowAuthoritys", OleDbType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OleDbType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@UsrGroups", OleDbType.VarChar).Value = ui.UsrGroups;
			cmd.Parameters.Add("@Companys", OleDbType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@Projects", OleDbType.VarChar).Value = ui.Projects;
			cmd.Parameters.Add("@Agents", OleDbType.VarChar).Value = ui.Agents;
			cmd.Parameters.Add("@Brokers", OleDbType.VarChar).Value = ui.Brokers;
			cmd.Parameters.Add("@Customers", OleDbType.VarChar).Value = ui.Customers;
			cmd.Parameters.Add("@Investors", OleDbType.VarChar).Value = ui.Investors;
			cmd.Parameters.Add("@Members", OleDbType.VarChar).Value = ui.Members;
			cmd.Parameters.Add("@Vendors", OleDbType.VarChar).Value = ui.Vendors;
            if (ScreenId.HasValue)
                cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = ScreenId.Value;
            else
                cmd.Parameters.Add("@ScreenId", OleDbType.Numeric).Value = DBNull.Value;
            if (ReportId.HasValue)
                cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = ReportId.Value;
            else
                cmd.Parameters.Add("@ReportId", OleDbType.Numeric).Value = DBNull.Value;
            if (WizardId.HasValue)
                cmd.Parameters.Add("@WizardId", OleDbType.Numeric).Value = WizardId.Value;
            else
                cmd.Parameters.Add("@WizardId", OleDbType.Numeric).Value = DBNull.Value;
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
	}
}