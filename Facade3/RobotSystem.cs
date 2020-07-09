namespace RO.Facade3
{
	using System;
	using System.Data;
	using RO.Access3;
	using RO.Common3;

	public class RobotSystem : MarshalByRefObject
	{
		private RobotAccessBase GetRobotAccess(int CommandTimeout = 1800)
		{
			if ((Config.DesProvider  ?? "").ToLower() != "odbc")
			{
				return new RobotAccess();
			}
			else
			{
				return new RO.Access3.Odbc.RobotAccess();
			}
		}

		public DataTable GetEntityList()
		{
			using (RobotAccessBase dac = GetRobotAccess())
			{
				return dac.GetEntityList();
			}
		}

		public DataTable GetClientTier(Int16 EntityId)
		{
			using (RobotAccessBase dac = GetRobotAccess())
			{
				return dac.GetClientTier(EntityId);
			}
		}

		public DataTable GetRuleTier(Int16 EntityId)
		{
			using (RobotAccessBase dac = GetRobotAccess())
			{
				return dac.GetRuleTier(EntityId);
			}
		}

		public DataTable GetDataTier(Int16 EntityId)
		{
			using (RobotAccessBase dac = GetRobotAccess())
			{
				return dac.GetDataTier(EntityId);
			}
		}

		public DataTable GetCustomList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			using (RobotAccessBase dac = GetRobotAccess())
			{
				return dac.GetCustomList(searchTxt, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetScreenList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			using (RobotAccessBase dac = GetRobotAccess())
			{
				return dac.GetScreenList(searchTxt, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetReportList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			using (RobotAccessBase dac = GetRobotAccess())
			{
				return dac.GetReportList(searchTxt, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetWizardList(string searchTxt, string dbConnectionString, string dbPassword)
		{
			using (RobotAccessBase dac = GetRobotAccess())
			{
				return dac.GetWizardList(searchTxt, dbConnectionString, dbPassword);
			}
		}
	}
}