using System;
using System.Data;
using RO.Common3;
using RO.Common3.Data;
using RO.Rule3;

namespace RO.Facade3
{
	public class AdmPuMkDeploySystem : MarshalByRefObject
	{
		public bool UpdReleaseBuild(Int16 ReleaseId, string ReleaseBuild)
		{
			using (Access3.AdmPuMkDeployAccess dac = new Access3.AdmPuMkDeployAccess())
			{
				return dac.UpdReleaseBuild(ReleaseId,ReleaseBuild);
			}
		}
	}
}