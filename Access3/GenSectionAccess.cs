namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class GenSectionAccess : Encryption, IDisposable
	{
		private OleDbDataAdapter da;

        public GenSectionAccess()
		{
			da = new OleDbDataAdapter();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true); // as a service to those who might inherit from us
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
				return;

			if (da != null)
			{
				if(da.SelectCommand != null)
				{
					if( da.SelectCommand.Connection != null  )
					{
						da.SelectCommand.Connection.Dispose();
					}
					da.SelectCommand.Dispose();
				}    
				da.Dispose();
				da = null;
			}
		}

        public void SetSctNeedRegen(string SectionCd)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbConnection cn = new OleDbConnection(GetDesConnStr());
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("SetSctNeedRegen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionCd", OleDbType.Char).Value = SectionCd;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetSctNeedRegen", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

        public DataTable GetPageObj(string SectionCd)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetPageObj", new OleDbConnection(GetDesConnStr()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionCd", OleDbType.Char).Value = SectionCd;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetPageLnk(string PageObjId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OleDbCommand cmd = new OleDbCommand("GetPageLnk", new OleDbConnection(GetDesConnStr()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageObjId", OleDbType.SmallInt).Value = PageObjId;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}