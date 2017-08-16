using System;
using System.Data;
using RO.Common3;
using RO.Common3.Data;

namespace RO.Facade3
{
	public class WebPartSystem : MarshalByRefObject
	{

        public DataTable GetDdlDshFldDtl(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
            using (Access3.WebPartAccess dac = new Access3.WebPartAccess())
			{
                return dac.GetDdlDshFldDtl(bAll, keyId, dbConnectionString, dbPassword, ui, uc);
			}
		}

		public DataTable GetDdlDshFld(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
            using (Access3.WebPartAccess dac = new Access3.WebPartAccess())
			{
				return dac.GetDdlDshFld(bAll, keyId, dbConnectionString, dbPassword, ui, uc);
			}
		}

        public void UpdDshFldDtl(string PublicAccess, string DshFldDtlId, string DshFldId, string DshFldDtlName, string DshFldDtlDesc, Int32 UsrId, string SystemId, string dbConnectionString, string dbPassword)
        {
            using (Access3.WebPartAccess dac = new Access3.WebPartAccess())
            {
                dac.UpdDshFldDtl(PublicAccess, DshFldDtlId, DshFldId, DshFldDtlName, DshFldDtlDesc, UsrId, SystemId, dbConnectionString, dbPassword);
            }
        }
    }
}
