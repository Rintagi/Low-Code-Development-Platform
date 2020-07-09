using System;
using System.Data;
using RO.Common3;
using RO.Common3.Data;
using RO.Rule3;
using RO.Access3;

namespace RO.Facade3
{
	public class AdmPuMkDeploySystem : MarshalByRefObject
	{
		private AdmPuMkDeployAccessBase GetAdmPuMkDeployAccess(int CommandTimeout = 1800)
		{
			if ((Config.DesProvider  ?? "").ToLower() != "odbc")
			{
				return new AdmPuMkDeployAccess();
			}
			else
			{
				return new RO.Access3.Odbc.AdmPuMkDeployAccess();
			}
		}

		public bool UpdReleaseBuild(Int16 ReleaseId, string ReleaseBuild)
		{
			using (AdmPuMkDeployAccessBase dac = GetAdmPuMkDeployAccess())
			{
				return dac.UpdReleaseBuild(ReleaseId,ReleaseBuild);
			}
		}
	}
}