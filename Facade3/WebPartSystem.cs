using System;
using System.Data;
using RO.Common3;
using RO.Common3.Data;
using RO.Access3;

namespace RO.Facade3
{
	public class WebPartSystem : MarshalByRefObject
	{
		private WebPartAccessBase GetWebPartAccess(int CommandTimeout = 1800)
		{
			if ((Config.DesProvider  ?? "").ToLower() != "odbc")
			{
				return new WebPartAccess();
			}
			else
			{
				return new RO.Access3.Odbc.WebPartAccess();
			}
		}

		public DataTable GetDdlDshFldDtl(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
            using (WebPartAccessBase dac = GetWebPartAccess())
			{
                return dac.GetDdlDshFldDtl(bAll, keyId, dbConnectionString, dbPassword, ui, uc);
			}
		}

		public DataTable GetDdlDshFld(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
            using (WebPartAccessBase dac = GetWebPartAccess())
			{
				return dac.GetDdlDshFld(bAll, keyId, dbConnectionString, dbPassword, ui, uc);
			}
		}

        public void UpdDshFldDtl(string PublicAccess, string DshFldDtlId, string DshFldId, string DshFldDtlName, string DshFldDtlDesc, Int32 UsrId, string SystemId, string dbConnectionString, string dbPassword)
        {
            using (WebPartAccessBase dac = GetWebPartAccess())
            {
                dac.UpdDshFldDtl(PublicAccess, DshFldDtlId, DshFldId, DshFldDtlName, DshFldDtlDesc, UsrId, SystemId, dbConnectionString, dbPassword);
            }
        }
    }
}
