namespace RO.Rule3
{
	using System;
	using System.Data;
	using System.IO;
	using System.Text;
	using System.Text.RegularExpressions;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.SystemFramewk;
	using RO.Access3;

	public class GenCustomsRules
	{
		public string CreateProgC(bool Overwrite, DataView dv, CurrSrc CSrc, string ProgramPath, string AppNameSpace, string ObjectTypeCd, string FrameworkCd)
		{
			StreamWriter sw;
			BinaryWriter bw;
			FileInfo fi;
			bool ErrFound;
			StringBuilder sb = new StringBuilder();
			string Framewk;
			foreach (DataRowView drv in dv)
			{
				if (drv["FrameworkCd"].ToString() == string.Empty) { Framewk = string.Empty; }
				else if (drv["FrameworkCd"].ToString() == "1") { Framewk = "Dotnet 1.1 "; }
				else { Framewk = "Dotnet 2.0 "; }
				ErrFound = false;
				fi = new FileInfo(ProgramPath + drv["RelativePath"].ToString() + drv["ProgramName"].ToString());
				if (!Overwrite && fi.LastWriteTime > Convert.ToDateTime(drv["ModifiedOn"].ToString()))
				{
					ErrFound = true;
					sb.Append("<br />" + Framewk + drv["RelativePath"].ToString() + drv["ProgramName"].ToString() + " is more recent and not over-written;");
				}
				if (!ErrFound)
				{
					if (drv["CustomCode"].ToString().Trim() != string.Empty)
					{
						sw = new StreamWriter(ProgramPath + drv["RelativePath"].ToString() + drv["ProgramName"].ToString());
						try {sw.Write(drv["CustomCode"].ToString().Replace("[["+"?]]",AppNameSpace));}	//avoid being replaced by iteself.
						catch (Exception err) {ApplicationAssert.CheckCondition(false, "", "", err.Message);}
						finally {sw.Close();}
					}
					else if (drv["CustomBin"] != null && drv["CustomBin"].ToString() != string.Empty)
					{
						bw = new BinaryWriter(File.Open(ProgramPath + drv["RelativePath"].ToString() + drv["ProgramName"].ToString(), FileMode.Create, FileAccess.ReadWrite));
						try {bw.Write((Byte[])drv["CustomBin"]);}
						catch (Exception err) {ApplicationAssert.CheckCondition(false, "", "", err.Message);}
						finally {bw.Close();}
					}
					else
					{
						ErrFound = true;
						sb.Append("<br />" + drv["RelativePath"].ToString() + drv["ProgramName"].ToString() + " is ignored;");
					}
				}
				if (!ErrFound)
				{
					if (ObjectTypeCd == "C")	// Client tier
					{
						if (FrameworkCd == "1")
						{
							Robot.ModifyCsproj(false, ProgramPath + "Web.csproj", drv["RelativePath"].ToString() + drv["ProgramName"].ToString(), FrameworkCd, string.Empty);
						}
					}
					else if (ObjectTypeCd == "R")	// Rule tier
					{
						if (drv["RelativePath"].ToString() == "Common" + CSrc.SrcSystemId.ToString() + "\\Data\\")
						{
							Robot.ModifyCsproj(false, ProgramPath + "Common" + CSrc.SrcSystemId.ToString() + "\\Common" + CSrc.SrcSystemId.ToString() + ".csproj", @"Data\" + drv["ProgramName"].ToString(), string.Empty, FrameworkCd);
						}
						else if (drv["RelativePath"].ToString().Length > 1)
						{
							Robot.ModifyCsproj(false, ProgramPath + drv["RelativePath"].ToString() + drv["RelativePath"].ToString().Substring(0,drv["RelativePath"].ToString().Length-1) + ".csproj", drv["ProgramName"].ToString(), string.Empty, FrameworkCd);
						}
					}
					using (Access3.GenCustomsAccess dac = new Access3.GenCustomsAccess())
					{
						dac.SetLastGenDt(drv["CustomDtlId"].ToString(), CSrc);
					}
				}
			}
			return sb.ToString();
		}

		public string CreateProgD(bool Overwrite, DataTable SystemDt, DataView dv, string AppNameSpace, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar, string dbConnectionString, string dbPassword)
		{
			string SrcDb, TarDb;
			DbPorting pt = new DbPorting();
			string ss;
			DateTime LmSrc, LmTar;
			StringBuilder sb = new StringBuilder();
			foreach (DataRowView drv in dv)
			{
				using (Access3.GenCustomsAccess dac = new Access3.GenCustomsAccess())
				{
					LmSrc = dac.GetLastModDt(drv["ProgramName"].ToString(), CSrc.SrcConnectionString, CSrc.SrcDbPassword);
				}
				using (Access3.GenCustomsAccess dac = new Access3.GenCustomsAccess())
				{
					LmTar = dac.GetLastModDt(drv["ProgramName"].ToString(), CTar.TarConnectionString, CTar.TarDbPassword);
				}
				if (!Overwrite && (LmSrc > Convert.ToDateTime(drv["ModifiedOn"].ToString()) || LmTar > Convert.ToDateTime(drv["ModifiedOn"].ToString())))
				{
					sb.Append("<br />" + drv["ProgramName"].ToString() + " is more recent and not over-written;");
				}
				else
				{
					ss = drv["CustomCode"].ToString().Trim().Replace("[["+"?]]",AppNameSpace);	//avoid being replaced by iteself.
					if (ss != string.Empty)
					{
						if (CPrj.TarDesProviderCd == "S") { ss = pt.SqlToSybase(CPrj.EntityId, CPrj.TarDesDatabase, ss, dbConnectionString, dbPassword); }
						SrcDb = CSrc.SrcDbDatabase; TarDb = CTar.TarDbDatabase;	// Save for later restore.
						foreach(DataRow rr in SystemDt.Rows)
						{
							if (drv["MultiDesignDb"].ToString() == "Y")
							{
								CSrc.SrcDbDatabase = rr["dbDesDatabase"].ToString();
								CTar.TarDbDatabase = rr["dbDesDatabase"].ToString();
								ExecProgD(drv["ProgramName"].ToString(), ss, CPrj, CSrc, CTar);
							}
							else if (rr["dbDesDatabase"].ToString() == SrcDb)
							{
								CSrc.SrcDbDatabase = rr["dbAppDatabase"].ToString();
								CTar.TarDbDatabase = rr["dbAppDatabase"].ToString();
								ExecProgD(drv["ProgramName"].ToString(), ss, CPrj, CSrc, CTar);
							}
						}
						CSrc.SrcDbDatabase = SrcDb; CTar.TarDbDatabase = TarDb;
						using (Access3.GenCustomsAccess dac = new Access3.GenCustomsAccess())
						{
							dac.SetLastGenDt(drv["CustomDtlId"].ToString(), CSrc);
						}
					}
				}
			}
			return sb.ToString();
		}

		private void ExecProgD(string ProcedureName, string ss, CurrPrj CPrj, CurrSrc CSrc, CurrTar CTar)
		{
			using (Access3.RobotAccess dac = new Access3.RobotAccess())
			{
				if (CPrj.SrcDesProviderCd == "S")
				{
					dac.ExecSql("SET QUOTED_IDENTIFIER ON SET ANSINULL ON IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id('"
					+ ProcedureName + "') AND type='P') DROP PROCEDURE " + ProcedureName
					+ " EXEC('" + ss.Replace("'","''") + "') SET QUOTED_IDENTIFIER OFF", CSrc.SrcConnectionString, CSrc.SrcDbPassword);
				}
				else
				{
					dac.ExecSql("SET QUOTED_IDENTIFIER ON SET ANSI_NULLS ON IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id('"
					+ ProcedureName + "') AND type='P') DROP PROCEDURE " + ProcedureName
					+ " EXEC('" + ss.Replace("'", "''") + "') SET QUOTED_IDENTIFIER OFF", CSrc.SrcConnectionString, CSrc.SrcDbPassword);
				}
			}
			if (CTar.TarDbServer != CSrc.SrcDbServer && CPrj.SrcDesProviderCd == "M")
			{
				using (Access3.RobotAccess dac = new Access3.RobotAccess())
				{
					if (CPrj.TarDesProviderCd == "S")
					{
						dac.ExecSql("SET QUOTED_IDENTIFIER ON SET ANSINULL ON IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id('"
						+ ProcedureName + "') AND type='P') DROP PROCEDURE " + ProcedureName
						+ " EXEC('" + ss.Replace("'", "''") + "') SET QUOTED_IDENTIFIER OFF", CTar.TarConnectionString, CTar.TarDbPassword);
					}
					else
					{
						dac.ExecSql("SET QUOTED_IDENTIFIER ON SET ANSI_NULLS ON IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id('"
						+ ProcedureName + "') AND type='P') DROP PROCEDURE " + ProcedureName
						+ " EXEC('" + ss.Replace("'", "''") + "') SET QUOTED_IDENTIFIER OFF", CTar.TarConnectionString, CTar.TarDbPassword);
					}
				}
			}
		}
	}
}