using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;

namespace RO.Access3
{
	public abstract class AdmPuMkDeployAccessBase : Encryption, IDisposable
	{
		public abstract void Dispose();
        public abstract bool UpdReleaseBuild(Int16 ReleaseId, string ReleaseBuild);
    }
}