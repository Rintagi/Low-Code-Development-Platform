namespace RO.Facade3
{
	using System;
	using System.Data;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.Rule3;

	public class MenuSystem : MarshalByRefObject
	{
        public DataTable GetMenu(Int16 CultureId, byte SystemId, UsrImpr ui, string dbConnectionString, string dbPassword, int? ScreenId, int? ReportId, int? WizardId)
        {
            using (Access3.MenuAccess dac = new Access3.MenuAccess())
            {
                return dac.GetMenu(CultureId, SystemId, ui, dbConnectionString, dbPassword, ScreenId, ReportId, WizardId);
            }
		}

        public void NewMenuItem(Int32 ScreenId, Int32 ReportId, Int32 WizardId, string ItemTitle, string dbConnectionString, string dbPassword)
        {
            using (Access3.MenuAccess dac = new Access3.MenuAccess())
            {
                dac.NewMenuItem(ScreenId, ReportId, WizardId, ItemTitle, dbConnectionString, dbPassword);
            }
        }
    }
}