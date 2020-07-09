namespace RO.Access3.Odbc
{
	using System;
	using System.Data;
    //using System.Data.OleDb;
    using System.Data.Odbc;
    using System.Linq;
    using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public class GenSectionAccess : GenSectionAccessBase, IDisposable
	{
		private OdbcDataAdapter da;

        private static OdbcCommand TransformCmd(OdbcCommand cmd)
        {
            if (cmd.Parameters != null
                && cmd.Parameters.Count > 0
                && cmd.CommandType == CommandType.StoredProcedure
                && !string.IsNullOrEmpty(cmd.CommandText)
                && !cmd.CommandText.StartsWith("{CALL")
                )
            {
                cmd.CommandText = string.Format("{{CALL {0}({1})}}"
                    , cmd.CommandText
                    , string.Join(",", Enumerable.Repeat("?", cmd.Parameters.Count).ToArray())
                    );
            }
            return cmd;
        }

        public GenSectionAccess()
		{
			da = new OdbcDataAdapter();
		}

		public override void Dispose()
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

        public override void SetSctNeedRegen(string SectionCd)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcConnection cn = new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr()));
            cn.Open();
            OdbcCommand cmd = new OdbcCommand("SetSctNeedRegen", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionCd", OdbcType.Char).Value = SectionCd;
            try { TransformCmd(cmd).ExecuteNonQuery(); }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "SetSctNeedRegen", "", e.Message.ToString()); }
            finally { cn.Close(); }
            return;
        }

        public override DataTable GetPageObj(string SectionCd)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("GetPageObj", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionCd", OdbcType.Char).Value = SectionCd;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public override DataTable GetPageLnk(string PageObjId)
        {
            if (da == null) { throw new System.ObjectDisposedException(GetType().FullName); }
            OdbcCommand cmd = new OdbcCommand("GetPageLnk", new OdbcConnection(Config.ConvertOleDbConnStrToOdbcConnStr(GetDesConnStr())));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageObjId", OdbcType.SmallInt).Value = PageObjId;
            da.SelectCommand = TransformCmd(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}