using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;

namespace RO.Access3
{
	public abstract class WebPartAccessBase : Encryption, IDisposable
	{
        public abstract void Dispose();

        public abstract DataTable GetDdlDshFldDtl(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc);

        public abstract DataTable GetDdlDshFld(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc);

        public abstract void UpdDshFldDtl(string PublicAccess, string DshFldDtlId, string DshFldId, string DshFldDtlName, string DshFldDtlDesc, Int32 UsrId, string SystemId, string dbConnectionString, string dbPassword);
    }
}
