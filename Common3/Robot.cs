namespace RO.Common3
{
	using System;
	using System.Text;
	using System.Data;
	using System.IO;
	using System.Configuration;
	using RO.SystemFramewk;
	using RO.Common3.Data;

	public class Robot
	{
        public static string SmallCapToStart(string inStr)
		{
			if (inStr != null && inStr != "")
			{
				return inStr.Substring(0,1).ToLower() + inStr.Remove(0,1);
			}
			else
			{
				return "";
			}
		}

		public static string HashRequired(string displayMode, string ColName, string bTrim)
		{
			string str; if (bTrim == "Y") { str = ".Trim()"; } else { str = string.Empty; }
			switch (displayMode)
			{
				case "Currency":
					return "Decimal.Parse(row[\"" + SmallCapToStart(ColName) + "\"].ToString().Trim(),System.Globalization.NumberStyles.Currency)";
				case "Password":
					return "new Credential(string.Empty,row[\"" + SmallCapToStart(ColName) + "\"].ToString()).Password";
				default:
					return "row[\"" + SmallCapToStart(ColName) + "\"].ToString()" + str;
			}
		}

		public static string ChkHash(string displayMode, string ColName, string bTrim)
		{
			string str; if (bTrim == "Y") { str = ".Trim()"; } else { str = string.Empty; }
			switch (displayMode)
			{
				case "Currency":
					return "Decimal.Parse(" + ColName + ".ToString().Trim(),System.Globalization.NumberStyles.Currency)";
				case "Password":
					return "new Credential(string.Empty," + ColName + ".ToString()).Password";
				default:
					return ColName + str;
			}
		}

		public static string ParseRequired(string varTypeName, bool bPrefix)
		{
			if (bPrefix)
			{
				if ("string".IndexOf(varTypeName.ToLower()) >= 0) {return "";} 
				else {return varTypeName + ".Parse(";}
			}
			else
			{
				if ("string".IndexOf(varTypeName.ToLower()) >= 0) {return "";} 
				else {return ")";}
			}
		}

		public static string QuoteRequired(bool bSingle, string dataTypeSysName, string DefaultValue)
		{
            if ("Char,NChar,VarChar,NVarChar,String".IndexOf(dataTypeSysName) >= 0 && DefaultValue.IndexOf("LUser.") < 0 && DefaultValue.IndexOf("LImpr.") < 0 && DefaultValue.IndexOf("LCurr.") < 0 && DefaultValue.IndexOf("Config.") < 0 && DefaultValue.IndexOf(".ToString") < 0)
			{
				if (bSingle)
                {
                    if (DefaultValue.StartsWith("'") && DefaultValue.EndsWith("'")) { return string.Empty; } else { return "'"; }
                }
                else
                {
                    if (DefaultValue.StartsWith("\"") && DefaultValue.EndsWith("\"")) { return string.Empty; } else { return "\""; }
                }
			}
			else if ("DateTime".IndexOf(dataTypeSysName) >= 0)
			{
				if (DefaultValue == string.Empty) { if (bSingle) { return "'"; } else { return "\""; } }
				else try { DateTime.Parse(DefaultValue); if (bSingle) { return "'"; } else { return "\""; } }
				catch { return ""; }
			}
			else
			{
				return string.Empty;
			}
		}

        public static string DataTypeConvert(string ZeroFilled, string DisplayName, string DisplayMode, string NumericData, bool bPrefix)   // Backward compatibility.
        {
            return DataTypeConvert(ZeroFilled, DisplayName, DisplayMode, NumericData, string.Empty, bPrefix);
        }

		public static string DataTypeConvert(string ZeroFilled, string DisplayName, string DisplayMode, string NumericData, string ColumnScale, bool bPrefix)
		{
			switch (DisplayMode)
			{
                case "ShortDateUTC":
                    if (bPrefix) { return "RO.Common3.Utils.fm" + DisplayMode + ZeroFilled + "("; } else { return ",base.LUser.Culture,CurrTimeZoneInfo())"; }
                case "DateUTC":
                case "LongDateUTC":
                case "DateTimeUTC":
                case "LongDateTimeUTC":
                case "ShortDateTimeUTC":
                    if (bPrefix) { return "RO.Common3.Utils.fm" + DisplayMode + "("; } else { return ",base.LUser.Culture,CurrTimeZoneInfo())"; }
                case "ShortDate":
                    if (bPrefix) { return "RO.Common3.Utils.fm" + DisplayMode + ZeroFilled + "("; } else { return ",base.LUser.Culture)"; }
                case "DateTime":
				case "Date":
				case "LongDateTime":
				case "LongDate":
				case "ShortDateTime":
				case "Currency":
				case "Money":
                    if (bPrefix) { return "RO.Common3.Utils.fm" + DisplayMode + "("; } else { return ",base.LUser.Culture)"; }
				default:
                    if (NumericData == "Y" && "ComboBox,DropDownList,ListBox,RadioButtonList".IndexOf(DisplayName) < 0)
					{
                        if (bPrefix) { return "RO.Common3.Utils.fmNumeric(\"" + ColumnScale + "\","; } else { return ",base.LUser.Culture)"; }
					} 
					else {return string.Empty;}
			}
		}

		public static string DataTypeFormat(string columnFormat, string paddSize, string paddChar, string dataTypeSysName, bool bPrefix)
		{
			if ("string".IndexOf(dataTypeSysName.ToLower()) < 0)
			{
				if (bPrefix) {return dataTypeSysName + ".Parse(";}
				else
				{
					if (columnFormat == null) {columnFormat = "";}
					if (paddSize == null || paddSize == "")
					{
						return ").ToString(\"" + columnFormat + "\")";
					}
					else
					{
						return ").ToString(\"" + columnFormat + "\").PadLeft(" + paddSize + ",'" + paddChar+ "')";
					}
				}
			}
			return "";
		}

        // Calling sequence for connection string.
        public static string GetCnStr(string multiDesignDb, string sysProgram)
        {
            if (multiDesignDb == "Y")
            {
                return ",(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId)";
            }
            else if (sysProgram != "Y")
            {
                return ",LcAppConnString,LcAppPw";
            }
            else
            {
                return ",null,null";
            }
        }

		// Calling sequence for connection string.  This should be deleted when not in use anymore.
		public static string GetCnCall(string multiDesignDb, string sysProgram)
		{
			if (multiDesignDb == "Y")
			{
				return ",(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId)";
			}
			else if (sysProgram != "Y")
			{
				return ",LcAppConnString,LcAppPw";
			}
			else
			{
				return "";
			}
		}

        //public static string GetCnDclr(string multiDesignDb, string sysProgram)
        //{
        //    if (multiDesignDb == "Y" || sysProgram != "Y")
        //    {
        //        return ", string dbConnectionString, string dbPassword";
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        //public static string GetCnParm(string multiDesignDb, string sysProgram)
        //{
        //    if (multiDesignDb == "Y" || sysProgram != "Y")
        //    {
        //        return ",dbConnectionString,dbPassword";
        //    }
        //    else
        //    {
        //        return ",null,null";
        //    }
        //}

        // Connection string for Access3.  This should be deleted when not in use anymore.
		public static string GetCnPass(string multiDesignDb, string sysProgram)
		{
			if (multiDesignDb == "Y" || sysProgram != "Y")
			{
				return "dbConnectionString + DecryptString(dbPassword)";
			}
			else
			{
				return "GetDesConnStr()";
			}
		}

        public static string GetIsSys(string multiDesignDb, string sysProgram)
        {
            if (multiDesignDb == "Y" || sysProgram != "Y") return "N";
            else return "Y";
        }

		public static void WriteToFile(string os, string fullFilePath, string strToBeWritten)
		{
            Action WriteToFile = () =>
            {
                StreamWriter sw = new StreamWriter(fullFilePath);
                if (os == "L")	// Linux
                {
                    sw.Write(strToBeWritten.Replace("\r\n", "\n"));
                }
                else
                {
                    sw.Write(strToBeWritten);
                }
                sw.Close();
            };

            try
            {
                WriteToFile();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    System.Threading.Thread.Sleep(1000);
                    WriteToFile();
                }
                else throw;
            }
		}

		public static string GetControlName(DataRowView drv)
		{
			if (drv["ColumnName"].ToString() != string.Empty)
			{
                return drv["ColumnName"].ToString() + (string.IsNullOrEmpty(drv["TableId"].ToString()) ? string.Empty : drv["TableId"].ToString());
			}
			else
			{
				return drv["ScreenObjId"].ToString();
			}
		}

		public static void ModifyCsproj(bool bDelete, string ProjFile, string RelPath, string ClientFrwork, string RuleFrwork)
		{
			const string ASPX = ".aspx";
			const string ASPXCS = ".aspx.cs";
			const string CS = ".cs";
			const string ASCX = ".ascx";
			const string ASCXCS = ".ascx.cs";
			const string GIF = ".gif";
			const string JPG = ".jpg";
			const string JS = ".js";
			const string CSS = ".css";
			const string SPACE5 = "    ";
			const string SPACE15 = "               ";
			const string SPACE19 = "                   ";
			StringBuilder sb = new StringBuilder();
			StreamReader sr = new StreamReader(ProjFile);
			sb.Append(sr.ReadToEnd());
			sr.Close();
			string sbStr = sb.ToString();
			if (ClientFrwork == "1")
			{
				if (bDelete)
				{
					if (sbStr.IndexOf("RelPath = \"" + RelPath + "\"") >= 0)
					{
						//find the last </ItemGroup> tag
						int tagMidIndex = sbStr.IndexOf("RelPath = \"" + RelPath + "\"");
						//keep indentation
						int tagStartIndex = sbStr.Substring(0, tagMidIndex).LastIndexOf("<File");
						tagStartIndex = sbStr.Substring(0, tagStartIndex).LastIndexOf("\n");
						//no close tag
						int closeTagIndex = tagMidIndex + sbStr.Substring(tagMidIndex, sbStr.Length - tagMidIndex).IndexOf("/>");
						int tagEndIndex = sbStr.Substring(closeTagIndex).IndexOf("\n") >= 0 ? closeTagIndex + sbStr.Substring(closeTagIndex).IndexOf("\n") : closeTagIndex;
						sb.Remove(tagStartIndex, tagEndIndex - tagStartIndex);
					}
				}
				else
				{
					if (sbStr.IndexOf("RelPath = \"" + RelPath + "\"") < 0)
					{
						if (sbStr.IndexOf("</Include>") >= 0)
						{
							//find the last </ItemGroup> tag
							int insertIndex = sbStr.LastIndexOf("</Include>");
							//keep indentation
							int indentStartIndex = sbStr.Substring(0, insertIndex).LastIndexOf("\n");
							string indentStr = sbStr.Substring(indentStartIndex, insertIndex - indentStartIndex);

							string tagString = "";

							indentStr = indentStr.Replace("\r", "");
							indentStr = indentStr.Replace("\n", "");
							int ii = RelPath.LastIndexOf("\\");
							if(RelPath.IndexOf(ASPXCS) == RelPath.Length - ASPXCS.Length)
							{
								tagString = SPACE5 + "<File\r\n " +	SPACE19 + "RelPath = \"" + RelPath + "\"\r\n "
								+ SPACE19 + "DependentUpon = \"" + RelPath.Substring(0, RelPath.Length - ASPXCS.Length).Substring(ii + 1, RelPath.Length - ASPXCS.Length - ii - 1) + ".aspx\"\r\n "
								+ SPACE19 + "SubType = \"ASPXCodeBehind\"\r\n "
								+ SPACE19 + "BuildAction = \"Compile\"\r\n "
								+ SPACE15 + "/>";
							}
							else if(RelPath.IndexOf(ASCXCS) == RelPath.Length - ASCXCS.Length)
							{
								tagString = SPACE5 + "<File\r\n "
								+ SPACE19 + "RelPath = \"" + RelPath + "\"\r\n "
								+ SPACE19 + "DependentUpon = \"" + RelPath.Substring(0, RelPath.Length - ASCXCS.Length).Substring(ii + 1, RelPath.Length - ASPXCS.Length - ii - 1) + ".ascx\"\r\n "
								+ SPACE19 + "SubType = \"ASPXCodeBehind\"\r\n "
								+ SPACE19 + "BuildAction = \"Compile\"\r\n "
								+ SPACE15 + "/>";
							}
							else if(RelPath.IndexOf(ASPX) == RelPath.Length - ASPX.Length ||
								RelPath.IndexOf(ASCX) == RelPath.Length - ASCX.Length ||
								RelPath.IndexOf(JPG) == RelPath.Length - JPG.Length || 
								RelPath.IndexOf(GIF) == RelPath.Length - GIF.Length || 
								RelPath.IndexOf(JS) == RelPath.Length - JS.Length || 
								RelPath.IndexOf(CSS) == RelPath.Length - CSS.Length)
							{
								tagString = SPACE5 + "<File\r\n " + 
									SPACE19 + "RelPath = \"" + RelPath + "\"\r\n " +
									SPACE19 + "BuildAction = \"Content\"\r\n " +
									SPACE15 + "/>";
							}
							else if(RelPath.IndexOf(CS) == RelPath.Length - CS.Length)
							{
								tagString = SPACE5 +  "<File\r\n " +
									SPACE19 + "RelPath = \"" + RelPath + "\"\r\n " +
									SPACE19 + "SubType = \"ASPXCodeBehind\"\r\n " +
									SPACE19 + "BuildAction = \"Compile\"\r\n " +
									SPACE15 + "/>";
							}
							sb.Insert(insertIndex, tagString + "\r\n" + indentStr);
						}
					}
				}
			}
			else	//ClientFrwork could be Empty or anything else.
			{
				if (RuleFrwork == "1")
				{
					if (bDelete)
					{
						if (sbStr.IndexOf("RelPath = \"" + RelPath + "\"") >= 0)
						{
							//find the last </ItemGroup> tag
							int tagMidIndex = sbStr.IndexOf("RelPath = \"" + RelPath + "\"");
							//keep indentation
							int tagStartIndex = sbStr.Substring(0, tagMidIndex).LastIndexOf("<File");
							tagStartIndex = sbStr.Substring(0, tagStartIndex).LastIndexOf("\n");
							//no close tag
							int closeTagIndex = tagMidIndex + sbStr.Substring(tagMidIndex, sbStr.Length - tagMidIndex).IndexOf("/>");
							int tagEndIndex = sbStr.Substring(closeTagIndex).IndexOf("\n") >= 0 ? closeTagIndex + sbStr.Substring(closeTagIndex).IndexOf("\n") : closeTagIndex;
							sb.Remove(tagStartIndex, tagEndIndex - tagStartIndex);
						}
					}
					else
					{
						if (sbStr.IndexOf("RelPath = \"" + RelPath + "\"") < 0)
						{
							if (sbStr.IndexOf("</Include>") >= 0)
							{
								//find the last </ItemGroup> tag
								int insertIndex = sbStr.LastIndexOf("</Include>");
								//keep indentation
								int indentStartIndex = sbStr.Substring(0, insertIndex).LastIndexOf("\n");
								string indentStr = sbStr.Substring(indentStartIndex, insertIndex - indentStartIndex);
								sb.Insert(insertIndex, "\t<File RelPath = \"" + RelPath + "\" BuildAction = \"Compile\" />\r" + indentStr);
							}
						}
					}            
				}
				else	// DotNet 2.0 for now.
				{
					if (bDelete)
					{
						if (sbStr.IndexOf("Include=\"" + RelPath + "\"") >= 0)
						{
							//find the last </ItemGroup> tag
							int tagMidIndex = sbStr.IndexOf("Include=\"" + RelPath + "\"");
							//keep indentation
							int tagStartIndex = sbStr.Substring(0, tagMidIndex).LastIndexOf("<Compile");
							tagStartIndex = sbStr.Substring(0, tagStartIndex).LastIndexOf("\n");
							//find close tag
							if (sbStr.Substring(tagMidIndex, sbStr.Length - tagMidIndex).IndexOf("</Compile>") >= 0)
							{
								int closeTagIndex = tagMidIndex + sbStr.Substring(tagMidIndex, sbStr.Length - tagMidIndex).IndexOf("</Compile>");
								if (sbStr.Substring(tagMidIndex, closeTagIndex - tagMidIndex).IndexOf("<Compile ") < 0)
								{
									//with close tag
									int tagEndIndex = sbStr.Substring(closeTagIndex).IndexOf("\n") >= 0 ? closeTagIndex + sbStr.Substring(closeTagIndex).IndexOf("\n") : closeTagIndex;
									sb.Remove(tagStartIndex, tagEndIndex - tagStartIndex);
								}
								else
								{
									//no close tag
									closeTagIndex = tagMidIndex + sbStr.Substring(tagMidIndex, sbStr.Length - tagMidIndex).IndexOf("/>");
									int tagEndIndex = sbStr.Substring(closeTagIndex).IndexOf("\n") >= 0 ? closeTagIndex + sbStr.Substring(closeTagIndex).IndexOf("\n") : closeTagIndex;
									sb.Remove(tagStartIndex, tagEndIndex - tagStartIndex);
								}
							}
							else
							{
								//no close tag
								int closeTagIndex = tagMidIndex + sbStr.Substring(tagMidIndex, sbStr.Length - tagMidIndex).IndexOf("/>");
								int tagEndIndex = sbStr.Substring(closeTagIndex).IndexOf("\n") >= 0 ? closeTagIndex + sbStr.Substring(closeTagIndex).IndexOf("\n") : closeTagIndex;
								sb.Remove(tagStartIndex, tagEndIndex - tagStartIndex);
							}
						}
					}
					else
					{
						if (sbStr.IndexOf("Include=\"" + RelPath + "\"") < 0)
						{
							if (sbStr.IndexOf("</ItemGroup>") >= 0)
							{
								//find the last </ItemGroup> tag
								int insertIndex = sbStr.LastIndexOf("</ItemGroup>");
								//keep indentation
								int indentStartIndex = sbStr.Substring(0, insertIndex).LastIndexOf("\n");
								string indentStr = sbStr.Substring(indentStartIndex, insertIndex - indentStartIndex);
								sb.Insert(insertIndex, "\t<Compile Include=\"" + RelPath + "\" />\r" + indentStr);
							}
						}
					}            
				}
			}
			StreamWriter sw = new StreamWriter(ProjFile);
			try {sw.Write(sb.ToString());} 
			finally {sw.Close();}
		}

		public static string CompilePrj(string CsPrj)
		{
			// Only DotNet 2.0 or later can be compiled this way:
			string cmd_path = "\"" + Config.BuildExe + "\"";
			string cmd_arg = " \"" + Config.RuleTierPath + CsPrj + "\" /p:Configuration=Debug /t:Rebuild /v:minimal /nologo";
			string ss = Utils.WinProc(cmd_path, cmd_arg, true);
			if (ss.IndexOf("failed") >= 0 || ss.Replace("errorreport", string.Empty).Replace("warnaserror", string.Empty).IndexOf("error") >= 0)
			{
				throw new Exception(ss);
			}
            return ss;
        }

        public static void CompileProxy(CurrPrj CPrj, CurrSrc CSrc)
        {
            // Need to recompile Facade layer to make MkWsProxy works:
            CompilePrj("Facade" + CSrc.SrcSystemId.ToString() + "\\Facade" + CSrc.SrcSystemId.ToString() + ".csproj");
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Facade" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Facade" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcWsProgramPath + "\\Bin\\Facade" + CSrc.SrcSystemId.ToString() + ".dll", true);
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Facade" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Rule" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcWsProgramPath + "\\Bin\\Rule" + CSrc.SrcSystemId.ToString() + ".dll", true);
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Facade" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Access" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcWsProgramPath + "\\Bin\\Access" + CSrc.SrcSystemId.ToString() + ".dll", true);
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Facade" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Common" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcWsProgramPath + "\\Bin\\Common" + CSrc.SrcSystemId.ToString() + ".dll", true);
        }

        public static string CompileAuxProj(CurrPrj CPrj)
        {
            string[] auxProj = new string[] { "SystemFramewk", "WebRules", "WebControls", "UsrAccess", "UsrRules" };
            string result = "";
            foreach (string proj in auxProj)
            {
                if (Directory.Exists(Config.RuleTierPath + "\\" + proj))
                {
                    result = result + CompilePrj(proj + "\\" + proj + ".csproj");
                    File.Copy(CPrj.SrcRuleProgramPath + "\\" + proj + "\\Bin\\Debug\\" + proj + ".dll", CPrj.SrcClientProgramPath + "\\Bin\\" + proj + ".dll", true);
                    if (proj == "SystemFramewk")
                    {
                        File.Copy(CPrj.SrcRuleProgramPath + "\\" + proj + "\\Bin\\Debug\\" + proj + ".dll", CPrj.SrcClientProgramPath + "\\Bin\\" + proj + ".dll", true);
                    }
                }
            }
            return result;
        }

        public static void RefreshClientTier(CurrPrj CPrj, CurrSrc CSrc)
        {
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Service" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Service" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcClientProgramPath + "\\Bin\\Service" + CSrc.SrcSystemId.ToString() + ".dll", true);
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Service" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Facade" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcClientProgramPath + "\\Bin\\Facade" + CSrc.SrcSystemId.ToString() + ".dll", true);
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Service" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Rule" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcClientProgramPath + "\\Bin\\Rule" + CSrc.SrcSystemId.ToString() + ".dll", true);
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Service" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Access" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcClientProgramPath + "\\Bin\\Access" + CSrc.SrcSystemId.ToString() + ".dll", true);
            File.Copy(CPrj.SrcRuleProgramPath + "\\" + "Service" + CSrc.SrcSystemId.ToString() + "\\Bin\\Debug\\Common" + CSrc.SrcSystemId.ToString() + ".dll", CPrj.SrcClientProgramPath + "\\Bin\\Common" + CSrc.SrcSystemId.ToString() + ".dll", true);
        }

		public static StringBuilder MkWsProxy(string ProgramName, CurrPrj CPrj, CurrSrc CSrc)
		{
			if (!File.Exists(Config.WsdlExe)) { throw new Exception("Expected program wsdl.exe not found at location " + Config.WsdlExe + "!");}
			StringBuilder sb = new StringBuilder();
			string ss;
			string url = Config.WsBaseUrl + "/" + ProgramName + "Ws.asmx";
			string cs_file = Path.GetTempFileName();
			string cmd_arg = " /nologo /namespace:" + CPrj.EntityCode + ".Service" + CSrc.SrcSystemId.ToString() + " /out:\"" + cs_file + "\" " + "\"" + url + "\"";
			ss = Utils.WinProc(Config.WsdlExe, cmd_arg, true);
			if (ss.IndexOf("Error:") >= 0) { throw new Exception(ss); }
			using (StreamReader sr = new StreamReader(cs_file))
			{
				sb.Append(sr.ReadToEnd());
			}
			File.Delete(cs_file);
			return sb;
		}
	}
}