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

	public class MenuAccess : MenuAccessBase, IDisposable
	{
		private OdbcDataAdapter da;
	
		public MenuAccess()
		{
			da = new OdbcDataAdapter();
		}

		public override void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true); // as a service to those who might inherit from us
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
        public override void NewMenuItem(Int32 ScreenId, Int32 ReportId, Int32 WizardId, string ItemTitle, string dbConnectionString, string dbPassword)
        {
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword)));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("NewMenuItem", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ScreenId", OdbcType.Numeric).Value = ScreenId;
            cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = ReportId;
            cmd.Parameters.Add("@WizardId", OdbcType.Numeric).Value = WizardId;
            cmd.Parameters.Add("@ItemTitle", OdbcType.NVarChar).Value = ItemTitle;
            cmd.CommandTimeout = 1800;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "NewMenuItem", "", e.Message.ToString()); }
            finally { cn.Close(); cmd.Dispose(); cmd = null; }
            return;
        }

        public override DataTable GetMenu(Int16 CultureId, byte SystemId, UsrImpr ui, string dbConnectionString, string dbPassword, int? ScreenId, int? ReportId, int? WizardId)
        {
            //if (!dbConnectionString.Contains("Design")) checkValidLicense();
            if (da == null) { throw new System.ObjectDisposedException( GetType().FullName ); }            
			OdbcCommand cmd = new OdbcCommand("GetMenu",new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(dbConnectionString + DecryptString(dbPassword))));
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@CultureId", OdbcType.Numeric).Value = CultureId;
			cmd.Parameters.Add("@SystemId", OdbcType.Numeric).Value = SystemId;
			cmd.Parameters.Add("@RowAuthoritys", OdbcType.VarChar).Value = ui.RowAuthoritys;
			cmd.Parameters.Add("@Usrs", OdbcType.VarChar).Value = ui.Usrs;
			cmd.Parameters.Add("@UsrGroups", OdbcType.VarChar).Value = ui.UsrGroups;
			cmd.Parameters.Add("@Companys", OdbcType.VarChar).Value = ui.Companys;
			cmd.Parameters.Add("@Projects", OdbcType.VarChar).Value = ui.Projects;
			cmd.Parameters.Add("@Agents", OdbcType.VarChar).Value = ui.Agents;
			cmd.Parameters.Add("@Brokers", OdbcType.VarChar).Value = ui.Brokers;
			cmd.Parameters.Add("@Customers", OdbcType.VarChar).Value = ui.Customers;
			cmd.Parameters.Add("@Investors", OdbcType.VarChar).Value = ui.Investors;
			cmd.Parameters.Add("@Members", OdbcType.VarChar).Value = ui.Members;
			cmd.Parameters.Add("@Vendors", OdbcType.VarChar).Value = ui.Vendors;
            if (ScreenId.HasValue)
                cmd.Parameters.Add("@ScreenId", OdbcType.Numeric).Value = ScreenId.Value;
            else
                cmd.Parameters.Add("@ScreenId", OdbcType.Numeric).Value = DBNull.Value;
            if (ReportId.HasValue)
                cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = ReportId.Value;
            else
                cmd.Parameters.Add("@ReportId", OdbcType.Numeric).Value = DBNull.Value;
            if (WizardId.HasValue)
                cmd.Parameters.Add("@WizardId", OdbcType.Numeric).Value = WizardId.Value;
            else
                cmd.Parameters.Add("@WizardId", OdbcType.Numeric).Value = DBNull.Value;
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
                    if (ii >= licensedCount && SystemId.ToString() != "3" && SystemId.ToString() != "5")
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