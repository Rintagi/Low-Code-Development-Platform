namespace RO.Rule3
{
	using System;
	using System.Text;
	using System.Data;
	using System.Data.OleDb;
	using System.Text.RegularExpressions;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.Access3;
	using RO.SystemFramewk;

	public class DbScript
	{
		public StringBuilder sbWarning = new StringBuilder("");
		private string sTablesExempt = "";
		private bool bNewApp;

		public DbScript(string TablesExempt, bool NewApp)
		{
			sTablesExempt = TablesExempt;
			bNewApp = NewApp;
		}

		public DataTable GetFKeys(string dbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
			{
				switch (dbProviderCd)
				{
					case "M":
						return dac.GetData("select so1.name as fkName, so2.name as tbName from sysreferences sr inner join sysobjects so1 on sr.constid = so1.id  inner join sysobjects so2 on sr.fkeyid = so2.id ORDER BY so1.name", IsFrSource, CSrc, CTar);
					case "S":
						return dac.GetData("select so1.name as fkName, so2.name as tbName from sysreferences sr inner join sysobjects so1 on sr.constrid = so1.id  inner join sysobjects so2 on sr.tableid = so2.id ORDER BY so1.name", IsFrSource, CSrc, CTar);
					default:
						ApplicationAssert.CheckCondition(false,"DbScript.GetFKeys()","Data Tier","Data Provider Code \"" + dbProviderCd + "\" not recognized. Please rectify and try again.");
						return null;
				}
			}
		}

		public DataTable GetTables(string dbProviderCd, bool IsFrSource, bool IsInExempt, bool IsDataExempt, CurrSrc CSrc, CurrTar CTar)
		{
			string sInClause = "";
			string sTyClause = "type = 'U'";
			string sExempt = sTablesExempt;
			if (bNewApp && !IsDataExempt) { sExempt = string.Empty; }	// bcp for new robot, app, etc.
			if (sExempt != string.Empty)
			{
				if (IsInExempt)	//Both tables and views for bcp only:
				{
					sInClause = " AND so.name in " + sExempt; sTyClause = "type in ('U','V')";
				}
				else
				{
					sInClause = " AND so.name not in " + sExempt;
				}
			}
			using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
			{
				return dac.GetData("SELECT so.name AS tbName,(SELECT 1 FROM dbo.syscolumns WHERE id=so.id AND (status & 128) = 128) AS hasIdentity FROM dbo.sysobjects so WHERE " + sTyClause + " AND name <> 'dtproperties'" + sInClause + " ORDER BY so.name", IsFrSource, CSrc, CTar);
			}
		}

		public string ScriptTruncData(string dbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			StringBuilder sb = new StringBuilder("");
			DataTable dt;
			dt = GetFKeys(dbProviderCd, IsFrSource, CSrc, CTar); //Drop FKs
			string FKType;
			if (dbProviderCd.Equals("M")) {FKType = "F";} else {FKType = "RI";};
			foreach (DataRow dr in dt.Rows)
			{
				sb.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + dr["fkName"].ToString() + "') and type='" + FKType + "')\r\n");
				sb.Append("ALTER TABLE dbo." + dr["tbName"].ToString() + " DROP CONSTRAINT " + dr["fkName"].ToString() + " \r\nGO\r\n");
			}
			dt = GetTables(dbProviderCd, IsFrSource, false, false, CSrc, CTar);
			foreach (DataRow dr in dt.Rows)
			{
				sb.Append("DELETE FROM dbo." + dr["tbName"].ToString() + "\r\nGO\r\n");
			}
			return sb.ToString();
		}

		public string ScriptCreateTables(string SrcDbProviderCd, string TarDbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			StringBuilder sb = new StringBuilder("");
			DataTable dt;
			DataTable dtIx;
			dt = GetViews(SrcDbProviderCd, IsFrSource, CSrc, CTar); //Drop Views
			foreach (DataRow dr in dt.Rows)
			{
				sb.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + dr[0].ToString() + "') AND type='V')\r\n");
				sb.Append("DROP VIEW dbo." + dr[0].ToString() + "\r\nGO\r\n");
			}
			dt = GetFKeys(SrcDbProviderCd, IsFrSource, CSrc, CTar); //Drop FKs
			string FKType = "F"; if (TarDbProviderCd.Equals("S")) { FKType = "RI"; };
			foreach (DataRow dr in dt.Rows)
			{
				sb.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + dr["fkName"].ToString() + "') and type='" + FKType + "')\r\n");
				sb.Append("ALTER TABLE dbo." + dr["tbName"].ToString() + " DROP CONSTRAINT " + dr["fkName"].ToString() + " \r\nGO\r\n");
			}
			dt = GetTables(SrcDbProviderCd, IsFrSource, false, false, CSrc, CTar);
			foreach (DataRow dr in dt.Rows)
			{
				using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
				{
					dtIx = dac.GetData("EXEC sp_helpindex " + dr["tbName"].ToString(), IsFrSource, CSrc, CTar);
				}
				foreach (DataRow drIx in dtIx.Rows)
				{
					if (drIx[0].ToString().Substring(0,3) != "PK_")	// No primary key.
					{
						sb.Append("IF EXISTS (SELECT name FROM sysindexes WHERE name = '" + drIx[0].ToString() + "')\r\n");
						sb.Append("DROP INDEX " + dr["tbName"].ToString() + "." + drIx[0].ToString() + " \r\nGO\r\n");
					}
				}
				sb.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + dr[0].ToString() + "') AND type='U')\r\n");
				sb.Append("DROP TABLE dbo." + dr[0].ToString() + "\r\nGO\r\n");
				sb.Append(ScriptCreateTable(dr["tbName"].ToString(),CSrc));
			}
			return sb.ToString();
		}

		public string ScriptCreateTable(string tbName, CurrSrc CSrc)
		{
			string TypesWithPar = "#binary#varbinary#nchar#nvarchar#char#varchar#";
			StringBuilder sb = new StringBuilder("");
			StringBuilder sbPk = new StringBuilder("");
			DataTable dtCol;
			DataTable dtPk;
			using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
			{
				dtCol = dac.GetColumnInfo(tbName,CSrc);
				dtPk = dac.GetPKInfo(tbName,CSrc);
			}
			if (dtPk.Rows.Count > 0)
			{
				sbPk.Append("CONSTRAINT " + dtPk.Rows[0]["cName"].ToString() + " PRIMARY KEY CLUSTERED (");
				for (int i=1; i<=(int)dtPk.Rows[0]["cColCount"]; i++)
				{
					sbPk.Append("\r\n"+dtPk.Rows[0]["cKeyCol"+i.ToString().Trim()].ToString()+",");
				}
				sbPk = sbPk.Replace(",","",sbPk.Length-1,1);
				sbPk.Append("\r\n)\r\n");
			}
			sb.Append("CREATE TABLE " + tbName + " ( \r\n");
			foreach(DataRow dr in dtCol.Rows)
			{
				sb.Append(dr["col_name"].ToString() + " " + dr["col_typename"].ToString() + " ");
				if (TypesWithPar.IndexOf("#" + dr["col_typename"].ToString() + "#") > 0)
				{
					// sp_MShelpcolumns returns col_len -1 for varbinary(max) and 0 for varchar(max):
					if (dr["col_len"].ToString() == "-1" || dr["col_len"].ToString() == "0") { sb.Append("(max) "); } else { sb.Append("(" + dr["col_len"].ToString() + ") "); }
				}
				if (dr["col_typename"].ToString().Equals("decimal") || dr["col_typename"].ToString().Equals("numeric")) 
					sb.Append("(" + dr["col_prec"].ToString() + "," + dr["col_scale"].ToString() + ") ");
				if ((bool)dr["col_identity"]) sb.Append("IDENTITY(" + dr["col_seed"].ToString() + "," + dr["col_increment"].ToString() + ") ");
				if ((bool)dr["col_null"]) sb.Append("NULL ");
				else sb.Append("NOT NULL ");
				//Add default constraint
				if (dr["col_dridefname"] != System.DBNull.Value)
				{
					sb.Append("CONSTRAINT " + dr["col_dridefname"].ToString() + " DEFAULT " + dr[15].ToString());
				}
				sb.Append(",\r\n");
			}
			if (sbPk.Length < 1) sb = sb.Replace(",","",sb.Length-",\r\n".Length,1);
			sb.Append(sbPk.ToString() + ")\r\nGO\r\n");
			return sb.ToString();
		}

		public DataTable GetViews(string dbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
			{
				switch (dbProviderCd)
				{
					case "M":
						return dac.GetData("SELECT name FROM sysobjects WHERE type='V' AND OBJECTPROPERTY(id,'IsMSShipped') = 0 ORDER BY name", IsFrSource, CSrc, CTar);
					case "S":
						return dac.GetData("SELECT name FROM sysobjects WHERE type='V' ORDER BY name", IsFrSource, CSrc, CTar);
					default:
						ApplicationAssert.CheckCondition(false,"DbScript.GetViews()","Data Tier","Data Provider Code \"" + dbProviderCd + "\" not recognized. Please rectify and try again.");
						return null;
				}
			}
		}
		
		public DataTable GetSps(string SrcDbProviderCd, string TarDbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
			{
				if (SrcDbProviderCd == "M" && TarDbProviderCd == "M")	// FN: Functions returning scalar; TF: Functions returning result set.; IF inline table function
				{
                    return dac.GetData("select name, type from sysobjects where type IN ('P','FN','TF','IF') and substring(name,1,3) <> 'dt_' order by case when type = 'P' then type else 'F' end, name", IsFrSource, CSrc, CTar);
				}
				else
				{
					return dac.GetData("select name, type from sysobjects where type='P' and substring(name,1,3) <> 'dt_'", IsFrSource, CSrc, CTar);
				}
			}
		}

		public string ScriptClearDb(string SrcDbProviderCd, string TarDbProviderCd, bool bTable, bool bData, bool bIndex, bool bView, bool bSp, CurrSrc CSrc, CurrTar CTar)
		{
			StringBuilder sb = new StringBuilder("");
			DataTable dt;
			if (bSp)
			{
				dt = GetSps(SrcDbProviderCd, TarDbProviderCd, false, CSrc, CTar); //Drop  Target Stored Procedures
				foreach (DataRow dr in dt.Rows)
				{
					sb.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + dr[0].ToString() + "') AND type='" + dr[1].ToString().Trim() + "')\r\n");
					if (dr[1].ToString() == "FN")
					{
						sb.Append("DROP FUNCTION dbo." + dr[0].ToString() + "\r\n");
					}
					else
					{
						sb.Append("DROP PROCEDURE dbo." + dr[0].ToString() + "\r\n");
					}
					sb.Append("GO\r\n");
				}
			}
			if (bTable || bView)
			{
				dt = GetViews(TarDbProviderCd, false, CSrc, CTar); //Drop Target Views
				foreach (DataRow dr in dt.Rows)
				{
					sb.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + dr[0].ToString() + "') AND type='V')\r\n");
					sb.Append("DROP VIEW dbo." + dr[0].ToString() + "\r\nGO\r\n");
				}
			}
			if (bTable || bData || bIndex)
			{
				dt = GetFKeys(TarDbProviderCd, false, CSrc, CTar); //Drop Target FKs
				string FKType;
				if (TarDbProviderCd.Equals("M")) {FKType = "F";} else {FKType = "RI";};
				foreach (DataRow dr in dt.Rows)
				{
					sb.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + dr["fkName"].ToString() + "') and type='" + FKType + "')\r\n");
					sb.Append("ALTER TABLE dbo." + dr["tbName"].ToString() + " DROP CONSTRAINT " + dr["fkName"].ToString() + " \r\nGO\r\n");
				}
			}
			if (bTable || bIndex)
			{
				dt = GetTables(TarDbProviderCd, false, false, false, CSrc, CTar);	//Drop Target Index
				DataTable dtIx;
				foreach (DataRow dr in dt.Rows)
				{
					using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
					{
						dtIx = dac.GetData("EXEC sp_helpindex " + dr["tbName"].ToString(), false, CSrc, CTar);
					}
					foreach (DataRow drIx in dtIx.Rows)
					{
						if (drIx[0].ToString().Substring(0,3) != "PK_")	// No primary key.
						{
							sb.Append("IF EXISTS (SELECT name FROM sysindexes WHERE name = '" + drIx[0].ToString() + "')\r\n");
							sb.Append("DROP INDEX " + dr["tbName"].ToString() + "." + drIx[0].ToString() + " \r\nGO\r\n");
						}
					}
				}
			}
			if (bData)
			{
				dt = GetTables(TarDbProviderCd, false, false, false, CSrc, CTar);
				foreach (DataRow dr in dt.Rows)
				{
					sb.Append("DELETE FROM dbo." + dr["tbName"].ToString() + "\r\nGO\r\n");
				}
			}
			if (bTable)
			{
				dt = GetTables(TarDbProviderCd, false, false, false, CSrc, CTar); //Drop target Tables
				foreach (DataRow dr in dt.Rows)
				{
					sb.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + dr[0].ToString() + "') AND type='U')\r\n");
					sb.Append("DROP TABLE dbo." + dr[0].ToString() + "\r\nGO\r\n");
				}
			}
			return sb.ToString();
		}

		public string GenerateBCPFiles(string ReleaseOs, string TarProviderCd, string dbProviderCd, string bcpPath, bool bOut, string outputPath, string separator, bool IsInExempt, CurrSrc CSrc, CurrTar CTar)
		{
			string unicd = "-c"; if (TarProviderCd == "M") { unicd = "-w"; }
			string direction = " in ";
			string logpath = outputPath;
			string str = "";
			string db = "";
			string hasIdentity = "";
			DataTable dt = GetTables(dbProviderCd, true, IsInExempt, true, CSrc, CTar);
            bool bIntegratedSecurity = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y" && (System.Configuration.ConfigurationManager.AppSettings["SSPI"] ?? "N") == "Y";
            if (ReleaseOs == "L")
			{
				str += "# !/bin/bash\n";
			}
			if (bOut)
			{
				direction = " out ";
				if (ReleaseOs == "L")
				{
					str += "rm " + outputPath + "*.txt -f\n";
				}
				else
				{
					str += "IF EXIST " + outputPath + "*.txt DEL /Q /S " + outputPath + "*.txt\r\n";
				}
			}
			foreach (DataRow dr2 in dt.Rows)
			{
				if (bOut) {db = CSrc.SrcDbDatabase;}
				else
				{
					db = CTar.TarDbDatabase;
					if (dr2["hasIdentity"].ToString().Equals("1")) {hasIdentity=" -E ";} else {hasIdentity="";}
				}
				if (ReleaseOs == "L")
				{
					if (dbProviderCd == "S")	//Sybase
					{
						str += bcpPath + "bcp " + db + ".dbo." + dr2["tbName"].ToString() + direction + outputPath + dr2["tbName"].ToString() + ".txt " + hasIdentity + " -e " + logpath + "..\\Error.txt -S$1 -U$2 -P$3 -T1048576 " + unicd + " -t\"" + separator + "\" -r\"~#~\" -b10000 >> " + logpath + "..\\Install.log\n";
					}
					else
					{
						str += bcpPath + "bcp " + db + ".dbo." + dr2["tbName"].ToString() + direction + outputPath + dr2["tbName"].ToString() + ".txt " + hasIdentity + " -e " + logpath + "..\\Error.txt -S $1 -U $2 -P $3 -q " + unicd + " -CRAW -t\"" + separator + "\" -r\"~#~\" >> " + logpath + "..\\Install.log\n";
					}
					str += "if [ $? -ne 0 ]\nthen\nexit\nfi\n";
				}
				else
				{
					if (dbProviderCd == "S")	//Sybase
					{
						str += "\"" + bcpPath + "bcp\" \"" + db + ".dbo." + dr2["tbName"].ToString() + "\"" + direction + "\"" + outputPath + dr2["tbName"].ToString() + ".txt\" " + hasIdentity + " -e \"" + logpath + "..\\Error.txt\" -S%1 -U%2 -P%3 -T1048576 " + unicd + " -t\"" + separator + "\" -r\"~#~\" -b10000 >> " + logpath + "..\\Install.log\r\n";
					}
					else
					{
                        if (direction == " in ") str += "\"" + bcpPath + "sqlcmd\" -Q \"TRUNCATE TABLE " + db + ".dbo." + dr2["tbName"].ToString() + "\"" + " " + " -S %1 -U %2 -P %3 " + " >> " + logpath + "..\\Install.log\r\n";
                        str += "\"" + bcpPath + "bcp\" \"" + db + ".dbo." + dr2["tbName"].ToString() + "\"" + direction + "\"" + outputPath + dr2["tbName"].ToString() + ".txt\" " + hasIdentity + " -e \"" + logpath + "..\\Error.txt\" -S %1 -U %2 -P %3 -q " + unicd + " -CRAW -t\"" + separator + "\" -r\"~#~\" >> " + logpath + "..\\Install.log\r\n";
                        if (bIntegratedSecurity && bOut) str = str.Replace("-U %2 -P %3", " -T ");
                    }
					str += "IF ERRORLEVEL 1 GOTO ThereIsError\r\n";
				}
			}
			if (ReleaseOs == "M") { str += "exit /b 0\r\n:ThereIsError\r\nexit /b 99\r\n"; }
			return str;
		}

		public string ScriptIndexFK(string SrcDbProviderCd, string TarDbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			StringBuilder sbDrop = new StringBuilder("");
			StringBuilder sbCrea = new StringBuilder("");
			string strIx;
			string strFK;
			DataTable dtIx;
			DataTable dtFK;
			DataTable dt;
			dt = GetTables(SrcDbProviderCd, IsFrSource, false, false, CSrc, CTar);
			foreach (DataRow dr2 in dt.Rows)
			{
				using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
				{
					dtIx = dac.GetData("EXEC sp_helpindex " + dr2["tbName"].ToString(), IsFrSource, CSrc, CTar);
				}
				foreach (DataRow drIx in dtIx.Rows)
				{
					if (drIx[0].ToString().Substring(0,3) != "PK_")	// No primary key.
					{
						sbDrop.Append("IF EXISTS (SELECT name FROM sysindexes WHERE name = '" + drIx[0].ToString() + "')\r\n");
						sbDrop.Append("DROP INDEX " + dr2["tbName"].ToString() + "." + drIx[0].ToString() + " \r\nGO\r\n");
						strIx = "CREATE ";
						if (drIx[1].ToString().LastIndexOf("unique") > 0) {strIx += " UNIQUE ";}
						strIx += "INDEX " + drIx[0].ToString() + " ON " + dr2["tbName"].ToString() + "(";
						strIx += drIx[2].ToString() + ")\r\nGO\r\n\r\n";
						//replace (-) 
						sbCrea.Append(Regex.Replace(sbDrop.Append(strIx).ToString(),"[(]-[)]"," DESC"));
						sbDrop.Remove(0,sbDrop.Length); //clear the drop statement
					}
				}
				using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
				{
					dtFK = dac.GetFKInfo(dr2["tbName"].ToString(), IsFrSource, CSrc, CTar);
				}
				foreach (DataRow drFK in dtFK.Rows)
				{
					sbDrop.Append("if exists (select * from dbo.sysobjects where id = object_id(N'dbo." + drFK["cName"].ToString() + "') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)\r\n");
					sbDrop.Append("ALTER TABLE dbo." + dr2["tbName"].ToString() + " DROP CONSTRAINT " + drFK["cName"].ToString() + " \r\nGO\r\n");
					strFK = "\r\nALTER TABLE " + dr2["tbName"].ToString() + " ADD\nCONSTRAINT " + drFK["cName"].ToString() + " FOREIGN KEY\r\n(";
					for (int i = 1; i <= (int)drFK["cColCount"]; i++) {strFK += "\r\n"+drFK["cKeyCol"+i.ToString().Trim()].ToString()+",";}
					strFK = strFK.Substring(0,strFK.Length-1);
					strFK += ")\r\n REFERENCES " + drFK["cRefTable"].ToString() + "\r\n(";
					for (int i = 1; i <= (int)drFK["cColCount"]; i++) {strFK += "\r\n"+drFK["cRefCol"+i.ToString().Trim()].ToString()+",";}
					strFK = strFK.Substring(0,strFK.Length-1);
					strFK = sbDrop.Append(strFK).ToString();
					sbDrop.Remove(0, sbDrop.Length); //clear the drop statement
					strFK += ")\r\nGO\r\n";
					sbCrea.Append(strFK);
				}				
			}
			return sbCrea.ToString();
		}

		public string ScriptView(string SrcDbProviderCd, string TarDbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			StringBuilder sbCrea = new StringBuilder("");
			StringBuilder sbView = new StringBuilder("");
			DataTable dtVw;
			DataTable dt;
			dt = GetViews(SrcDbProviderCd, IsFrSource, CSrc, CTar);
			foreach (DataRow dr in dt.Rows)
			{
				using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
				{
					dtVw = dac.GetData("EXEC sp_helptext " + dr[0].ToString(), IsFrSource, CSrc, CTar);
				}
				foreach (DataRow dr2 in dtVw.Rows) {sbView.Append(dr2[0].ToString());}
				if (!sbView.ToString().Equals(""))
				{
					sbCrea.Append("if exists (select * from dbo.sysobjects where id = object_id(N'dbo." + dr[0].ToString() + "') and OBJECTPROPERTY(id, N'IsView') = 1)\r\n");
					sbCrea.Append("drop view dbo." + dr[0].ToString() + "\r\n");
					sbCrea.Append("GO\r\n");
					sbCrea.Append(sbView.ToString());
					sbView.Remove(0, sbView.Length);
					sbCrea.Append("\r\nGO\r\n");
				}
			}
			return sbCrea.ToString();
		}

		public string ScriptSProcedures(string SrcDbProviderCd, string TarDbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			StringBuilder sb = new StringBuilder("");
			string ss;
			DataTable dt = GetSps(SrcDbProviderCd, TarDbProviderCd, IsFrSource, CSrc, CTar);
			foreach (DataRow dr in dt.Rows)
			{
				ss = ScriptCreaSp(dr[0].ToString(), dr[1].ToString().Trim(), SrcDbProviderCd, IsFrSource, CSrc, CTar);
				if (ss != string.Empty)
				{
					sb.Append(ScriptDropSp(dr[0].ToString(), dr[1].ToString().Trim()));
					sb.Append("GO\r\n");
					sb.Append("SET QUOTED_IDENTIFIER ON\r\n");
					sb.Append("GO\r\n");
					if (SrcDbProviderCd == "S") {sb.Append("SET ANSINULL ON\r\n");} else {sb.Append("SET ANSI_NULLS ON\r\n");}
					sb.Append("GO\r\n");
					sb.Append(ss);
					sb.Append(" \r\nGO\r\n");
					sb.Append("SET QUOTED_IDENTIFIER OFF\r\n");
					sb.Append("GO\r\n");
				}
			}
			return sb.ToString();
		}

		public string ScriptDropSp(string SpName, string SpType)
		{
			StringBuilder sbDrop = new StringBuilder("");
			sbDrop.Append("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo." + SpName + "') AND type='" + SpType + "')\r\n");
            if ("FN,TF,IF".IndexOf(SpType) >= 0)
            {
				sbDrop.Append("DROP FUNCTION dbo." + SpName + "\r\n");
			}
			else
			{
				sbDrop.Append("DROP PROCEDURE dbo." + SpName + "\r\n");
			}
			return sbDrop.ToString();
		}

		public string ScriptCreaSp(string SpName, string SpType, string SrcDbProviderCd, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			StringBuilder sbCrea = new StringBuilder("");
			DataTable dtSp;
			using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
			{
				try
				{
					if (SrcDbProviderCd == "S")
					{
						dtSp = dac.GetDataSet("EXEC sp_helptext " + SpName, IsFrSource, CSrc, CTar).Tables[1];
					}
					else
					{
						dtSp = dac.GetData("EXEC sp_helptext " + SpName, IsFrSource, CSrc, CTar);
					}
				}
				catch (Exception e)
				{
					if (e.Message.IndexOf("hidden") < 0) {throw new Exception(e.Message);} else {dtSp = null;}
				}
			}
			if (dtSp != null) {foreach (DataRow dr2 in dtSp.Rows) {sbCrea.Append(dr2[0].ToString());}}
			return Regex.Replace(sbCrea.ToString(),@"(?i)([a-zA-Z0-9])(\.{2})([a-zA-Z])","$1.dbo.$3");
		}

		public string EncryptSProcedures(string SrcDbProviderCd, string TarDbProviderCd, string ss, bool IsFrSource, CurrSrc CSrc, CurrTar CTar)
		{
			if (TarDbProviderCd == "S")
			{
				StringBuilder sbCrea = new StringBuilder(ss);
				StringBuilder sbProc = new StringBuilder("");
				DataTable dtSp;
				DataTable dt = GetSps(SrcDbProviderCd, TarDbProviderCd, IsFrSource, CSrc, CTar);
				foreach (DataRow dr in dt.Rows)
				{
					using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
					{
						dtSp = dac.GetData("EXEC sp_helptext " + dr[0].ToString(), IsFrSource, CSrc, CTar);
					}
					foreach (DataRow dr2 in dtSp.Rows) {sbProc.Append(dr2[0].ToString());}
					if (!sbProc.ToString().Equals(""))
					{
						sbProc.Remove(0, sbProc.Length);
						sbCrea.Append("sp_hidetext " + dr[0].ToString() + " \r\nGO\r\n");
					}
				}
				return sbCrea.ToString();
			}
			else	// TarDbProviderCd == "M"
			{
				return Regex.Replace(ss,@"(?i)([^'])(/[*])\s*WITH\s*ENCRYPTION\s*([*]/)","$1WITH ENCRYPTION");
			}
		}

		public void ExecScript(string dbProviderCd, string Source, string CmdName, string IsqlFile, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
			StringBuilder sbError = new StringBuilder("");
			sbWarning.Remove(0, sbWarning.Length);
			OleDbDataReader drd;
			using (Access3.DbScriptAccess dac = new Access3.DbScriptAccess())
			{
				drd = dac.ExecScript(dbProviderCd, CmdName, IsqlFile, dbProviderCd == string.Empty, CSrc, CTar, dbConnectionString, dbPassword);
			}
			Regex isqlWarningRule = new Regex(@"Level\s+([1-9],|1[0-1],)");
			Regex isqlErrorRule = new Regex(@"(?i)(Level\s+(1[2-9]|2[0-9]))|(Library\serror:)|(not\srecognized\sas\san\sinternal)|(isql:\sunknown\soption)|(Syntax\sError\sin)");
			Regex batErrorRule = new Regex(@"(?i)(\s+denied\.|\s+msg\s+|\s+error\s+|\s+failed)");
			bool addToError = false;
			bool addToWarning = false;
			while (drd.Read())
			{
				if (!drd.GetValue(0).Equals(System.DBNull.Value))
				{
					if (IsqlFile == string.Empty)	// batch bcp
					{
						if (batErrorRule.IsMatch(drd.GetString(0))) { addToError = true; }
						if (addToError) { sbError.Append(Regex.Replace(drd.GetString(0), @"(-P\s*\w*\s|-U\s*\w*\s)", "") + "\r\n"); }
					}
					else
					{
						if (isqlErrorRule.IsMatch(drd.GetString(0)))
						{
							addToError = true;
							addToWarning = false;
						}
						else if (isqlWarningRule.IsMatch(drd.GetString(0)))
						{
							addToError = false;
							addToWarning = true;
						}
						if (addToError)
						{
							sbError.Append(Regex.Replace(drd.GetString(0), @"(-P\s*\w*\s|-U\s*\w*\s)", "") + "\r\n");
						}
						else if (addToWarning)
						{
							sbWarning.Append(Regex.Replace(drd.GetString(0), @"(-P\s*\w*\s|-U\s*\w*\s)", "") + "\r\n");
						}
					}
				}
			}
			if (IsqlFile == string.Empty)	// batch bcp
			{
				ApplicationAssert.CheckCondition(!batErrorRule.IsMatch(sbError.ToString()), Source, "DbScript.ExecScript()", sbError.ToString());
			}
			else
			{
				ApplicationAssert.CheckCondition(!isqlErrorRule.IsMatch(sbError.ToString()), Source, "DbScript.ExecScript()", sbError.ToString());
			}
			if (sbWarning.ToString() != "") { sbWarning.Insert(0, Source + ": DbScript.ExecScript(): "); }
		}
	}
}