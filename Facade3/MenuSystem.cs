namespace RO.Facade3
{
	using System;
	using System.Data;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.Rule3;
	using RO.Access3;

	public class MenuSystem : MarshalByRefObject
	{
		private MenuAccessBase GetMenuAccess(int CommandTimeout = 1800)
		{
			if ((Config.DesProvider  ?? "").ToLower() != "odbc")
			{
				return new MenuAccess();
			}
			else
			{
				return new RO.Access3.Odbc.MenuAccess();
			}
		}

		public DataTable GetMenu(Int16 CultureId, byte SystemId, UsrImpr ui, string dbConnectionString, string dbPassword, int? ScreenId, int? ReportId, int? WizardId)
        {
            using (MenuAccessBase dac = GetMenuAccess())
            {
                return dac.GetMenu(CultureId, SystemId, ui, dbConnectionString, dbPassword, ScreenId, ReportId, WizardId);
            }
		}

        public void NewMenuItem(Int32 ScreenId, Int32 ReportId, Int32 WizardId, string ItemTitle, string dbConnectionString, string dbPassword)
        {
            using (MenuAccessBase dac = GetMenuAccess())
            {
                dac.NewMenuItem(ScreenId, ReportId, WizardId, ItemTitle, dbConnectionString, dbPassword);
            }
        }
    }
}