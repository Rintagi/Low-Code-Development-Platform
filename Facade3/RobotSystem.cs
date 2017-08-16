namespace RO.Facade3
{
	using System;
	using System.Data;

	public class RobotSystem : MarshalByRefObject
	{
		public DataTable GetEntityList()
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				return dac.GetEntityList();
			}
		}

		public DataTable GetClientTier(Int16 EntityId)
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				return dac.GetClientTier(EntityId);
			}
		}

		public DataTable GetRuleTier(Int16 EntityId)
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				return dac.GetRuleTier(EntityId);
			}
		}

		public DataTable GetDataTier(Int16 EntityId)
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				return dac.GetDataTier(EntityId);
			}
		}

		public DataTable GetCustomList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				return dac.GetCustomList(searchTxt, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetScreenList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				return dac.GetScreenList(searchTxt, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetReportList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				return dac.GetReportList(searchTxt, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetWizardList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				return dac.GetWizardList(searchTxt, dbConnectionString, dbPassword);
			}
		}
	}
}