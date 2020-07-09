namespace RO.Rule3
{
	using System;
	using System.Text;
	using System.Data;
	//using System.Data.OleDb;
	using System.Text.RegularExpressions;
	using RO.Access3;
	using RO.SystemFramewk;
	using RO.Common3;

	public class DbPorting
	{
		public StringBuilder sbWarning = new StringBuilder("");

		private DbPortingAccessBase GetDbPortingAccess(int CommandTimeout = 1800)
		{
			if ((Config.DesProvider  ?? "").ToLower() != "odbc")
			{
				return new DbPortingAccess();
			}
			else
			{
				return new RO.Access3.Odbc.DbPortingAccess();
			}
		}

		public DbPorting()
		{
		}
		
		// Make sure the table dbo.SqlToSybMap exists in the SQL RORobot database.
		public string SqlToSybase (Int16 EntityId, string UdFunctionDb, string InStr, string dbConnectionString, string dbPassword)
		{
			string temp = "";
			temp = mapDatabases(EntityId, InStr, dbConnectionString, dbPassword);
			StringBuilder sbError = checkErrWarning(temp);
			ApplicationAssert.CheckCondition(sbError.ToString() == string.Empty,"DbPorting.SqlToSybase()","Converting Script",sbError.ToString());
			if (sbError.ToString() == string.Empty) {return convertFunctions(convertCode(temp),UdFunctionDb);} else {return InStr;}
		}

		//Rules for T-SQL.
		private string convertCode(string InStr)
		{
			string tInStr = InStr;
			//Remove ( left parenthesis
			//Remove right parenthesis + WITH SETERROR
			//remove ,severity,warning
			tInStr= Regex.Replace(tInStr,@"(?i)RAISERROR(\s*[(])","RAISERROR 20001 ");
			tInStr= Regex.Replace(tInStr,@"(?i)\s*[)]\s*WITH\s*SETERROR","");
			tInStr= Regex.Replace(tInStr,@"(?i)(RAISERROR 20001\s*'[^']*')(\s*,\s*\d+){2}","$1");
			//erase encryption option from CREATE PROCEDURE name WITH ENCRYPTION
			tInStr=Regex.Replace(tInStr,@"(?i)(/[*]){0,1}\s*WITH\s*ENCRYPTION\s*([*]/){0,1}","  ");
			//Change Ansi_nulls to AnsiNull option.
			tInStr=Regex.Replace(tInStr,@"(?i)(SET)\s*(ANSI_NULLS)\s*(ON|OFF)","$1 ANSINULL $3");
			//Comment out ARITHABORT and ANSI_WARNINGS option.
			//Eliminate SET XACT_ABORT and ANSI_WARNINGS not supported in Sybase
			tInStr=Regex.Replace(tInStr,@"(?i)(SET)\s*(XACT_ABORT)\s*(ON|OFF)","");
			tInStr=Regex.Replace(tInStr,@"(?i)(SET)\s*(ANSI_WARNINGS)\s*(ON|OFF)","");
			//Eliminate LOCAL from cursor
			tInStr=Regex.Replace(tInStr,@"(?i)(DECLARE)\s*(.*)(CURSOR)\s*LOCAL\s*(FOR)","$1 $2 $3 $4");
			//Eliminate FAST_FORWARD from cursor
			tInStr = Regex.Replace(tInStr, @"(?i)(DECLARE)\s*(.*)(CURSOR)\s*FAST_FORWARD\s*(FOR)", "$1 $2 $3 $4");
			//Eliminate FAST_FORWARD LOCAL from cursor
			tInStr = Regex.Replace(tInStr, @"(?i)(DECLARE)\s*(.*)(CURSOR)\s*FAST_FORWARD LOCAL\s*(FOR)", "$1 $2 $3 $4");
			//Eliminate LOCAL FAST_FORWARD from cursor
			tInStr = Regex.Replace(tInStr, @"(?i)(DECLARE)\s*(.*)(CURSOR)\s*LOCAL FAST_FORWARD\s*(FOR)", "$1 $2 $3 $4");
			//Correct the fecth statement
			tInStr=Regex.Replace(tInStr,@"(?i)FETCH(\s*NEXT\s*FROM\s*)(\w+)","FETCH $2");
			//Correct deallocate cursor syntax
			tInStr=Regex.Replace(tInStr,@"(?i)DEALLOCATE\s*(\w+)","DEALLOCATE CURSOR $1");
			//Correct the @@fetch_status
			tInStr=Regex.Replace(tInStr,@"(?i)@@FETCH_STATUS","@@sqlStatus");
			//Correct the constant of @@fetch_status -1 to 2 End of dataset
			tInStr=Regex.Replace(tInStr,@"(?i)(@@sqlStatus)\s*(=)\s*(-1)","$1 $2 2");
			//Change the PwdEncrypt to internal_encrypt (Sybase only)
			tInStr=Regex.Replace(tInStr,@"(?i)pwdencrypt\s*[(]\s*([@a-z.0-9_]+)\s*[)]","internal_encrypt($1)");
			//Change the PwdCompare(a,b,n)=1 PwdCompare(a,b,n)<>0 PwdCompare(a,b,n)>1 function to internal_encrypt(a)=b
			tInStr=Regex.Replace(tInStr,@"(?i)pwdcompare[(]\s*([@a-z0-9_.]+)\s*,\s*([@a-z0-9_.]+)\s*(,\s*0){0,1}\s*[)]\s*(=\s*1|<>\s*0|>\s*0)","internal_encrypt($1)=$2");
			//Change the PwdCompare(a,b,n)=0 PwdCompare(a,b,n)<>1 PwdCompare(a,b,n)>0 function to internal_encrypt(a)<>b
			tInStr=Regex.Replace(tInStr,@"(?i)pwdcompare[(]\s*([@a-z0-9_.]+)\s*,\s*([@a-z0-9_.]+)\s*(,\s*0){0,1}\s*[)]\s*(=\s*0|<>\s*1|<\s*1)","internal_encrypt($1)<>$2");

			//Rules for DDL from MSSQL
			//Change default syntax
			tInStr=Regex.Replace(tInStr, @"(?i)(NOT NULL|NULL)(\s*)CONSTRAINT\s*\w*\s*(DEFAULT)\s*[(]N{0,1}(['].*[']|\d*[.]{0,1}\d*)[)]"," $3 $4 $1 ");
			//Replace ??int to numeric (?,0) in all declarations
			tInStr=Regex.Replace(tInStr, @"(?i)([(\s])(bigint)([),'\s])","$1numeric (19,0)$3");
			tInStr=Regex.Replace(tInStr, @"(?i)([(\s])(int)([),'\s])","$1numeric (10,0)$3");
			tInStr=Regex.Replace(tInStr, @"(?i)([(\s])(smallint)([),'\s])","$1numeric (5,0)$3");
			tInStr=Regex.Replace(tInStr, @"(?i)([(\s])(tinyint)([),'\s])","$1numeric (3,0)$3");
			tInStr=Regex.Replace(tInStr, @"(?i)IDENTITY\s*[(]\d+,\d+[)]"," IDENTITY ");
			//Replace nText field to TEXT field 
			tInStr=Regex.Replace(tInStr, @"(?i)(\[|\s)(\s*)(ntext)(\s*)(\]|[,]|\s)","$1$2text$4$5");
			//Remove the COLLATE syntax
			tInStr=Regex.Replace(tInStr, @"(?i)(collate\s*)(\w*)\s*(NULL|NOT NULL)","$3");
			//Remove on Primary syntax
			tInStr=Regex.Replace(tInStr, @"(?i)ON\s*\[\s*Primary\s*]","");
			//Remove TEXTIMAGE_ syntax
			tInStr=Regex.Replace(tInStr, @"(?i)TEXTIMAGE_","");
			//Remove WITH NOCHECK when create primary keys
			//Remove WITH STATISTICS_NORECOMPUTE when creating an index
			tInStr=Regex.Replace(tInStr, @"(?i)(WITH\s*STATISTICS_NORECOMPUTE|WITH\s*NOCHECK)","");
			//Remove the function ObjectProperty from DDL generated by MSSQL
			tInStr=Regex.Replace(tInStr, @"(?i)and OBJECTPROPERTY[(]id, N{0,1}'IsUserTable'[)] = 1[)]","and type='U')");
			tInStr=Regex.Replace(tInStr, @"(?i)and OBJECTPROPERTY[(]id, N{0,1}'IsForeignKey'[)] = 1[)]","and type='RI')");
			tInStr=Regex.Replace(tInStr, @"(?i)and OBJECTPROPERTY[(]id, N{0,1}'IsView'[)] = 1[)]","and type='V')");
			tInStr=Regex.Replace(tInStr, @"(?i)and OBJECTPROPERTY[(]id, N{0,1}'IsProcedure'[)] = 1[)]","and type='P')");
			tInStr=Regex.Replace(tInStr, @"(?i)and xtype='P'[)]","and type='P')");
			//Fix System function SUSER_SID() to Sybase SUSER_ID()
			tInStr=Regex.Replace(tInStr, @"(?i)SUSER_SID","SUSER_ID");
			//Remove Locking Hints
			tInStr=Regex.Replace(tInStr, @"(?i)([(]\s*)(UPDLOCK|TABLOCK|PAGLOCK|TABLOCKX|XLOCK|NOLOCK)\s*[)]","");
			int phNum;
			Regex rx = new Regex("RAISERROR 20001 '(.*(%s|%d).*)'");
			Match mh = rx.Match(tInStr);
			while (mh.Success) 
			{
				string tt = mh.Value;
				string ss = tt;
				Regex rr = new Regex("(%d|%s)");
				phNum=0;
				while (rr.Match(ss).Success)
				{
					phNum++;
					ss=rr.Replace(ss,"%" + phNum.ToString() + "!",1);
				}
				tInStr = tInStr.Replace(tt,ss);
				mh = mh.NextMatch();
			}
			//Remove all square brackets for temp table and dbo only. LAST STEP
			tInStr=Regex.Replace(tInStr, @"\[(#\w*|dbo)\]","$1");
			return tInStr;
		}

		private StringBuilder checkErrWarning (string strInput)
		{
			StringBuilder sbError = new StringBuilder("");

			//Change default syntax in DDL (this is the only warning, the rest are errors)
			//sbWarning.Append(checkRule(strInput, @"(?i)(NOT NULL|NULL)(\s*)CONSTRAINT\s*\w*\s*(DEFAULT)\s*[(]N{0,1}(['].*[']|\d*[.]{0,1}\d*)[)]", "Default values are used in table"));

			//Check for Select Top n statements
			sbError.Append(checkRule(strInput, @"(?i)select top \d+.*\n","SELECT TOP n statements are not allowed in ASE"));

			//check for table data types (memory tables)
			sbError.Append(checkRule(strInput, @"(?i)@\w* table.*\n","ASE does not recognize TABLE data type"));

			//check for sql_variant data type
			sbError.Append(checkRule(strInput, @"(?i)@\w* sql_variant.*\n","ASE does not recognize SQL_VARIANT data type"));

			//check for uniqueidentifier data type
			sbError.Append(checkRule(strInput, @"(?i)@\w* uniqueidentifier.*\n","ASE does not recognize UNIQUEIDENTIFIER data type"));

			//check for sql_variant data type in DDL
			sbError.Append(checkRule(strInput, @"(?i)\w*\[sql_variant].*\n","ASE does not recognize SQL_VARIANT data type in DDL"));

			//check for uniqueidentifier data type in DDL
			sbError.Append(checkRule(strInput, @"(?i)\w*\[uniqueidentifier].*\n","ASE does not recognize UNIQUEIDENTIFIER data type in DDL"));

			//check for T-SQL sybase reserved words used them as identifiers in MSSQL
			sbError.Append(checkRule(strInput, @"(?i)\[(add|all|alter|and|any|arith_overflow|as|asc|at|authorization|avg)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(begin|between|break|browse|bulk|by)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(cascade|case|char_convert|check|checkpoint|close|clustered|coalesce|commit|compute|confirm,connect|constraint|continue|controlrow|convert|count|create|current|cursor)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(database|dbcc|deallocate|declare|default|delete|desc|deterministic|disk distinct|double|drop,dummy|dump)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(else|end|endtran|errlvl|errordata|errorexit|escape|except|exclusive|exec|execute|exists|exit,exp_row_size|external)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(fetch|fillfactor|for|foreign|from|func)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(goto|grant|group)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(having|holdlock)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(identity|identity_gap|identity_insert|identity_start|if|in|index|inout|insert|install|intersect|into|is,isolation)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(jar|join)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(key|kill)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(level|like|lineno|load|lock)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(max|max_rows_per_page|min|mirror|mirrorexit|modify)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(national|new|noholdlock|nonclustered|not|null|nullif|numeric_truncation)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(of|off|offsets|on|once|online|only|open|option|or|order|out|output|over)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(partition|perm|permanent|plan|precision|prepare|print|privileges|proc|procedure,processexit|proxy_table|public)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(quiesce)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(raiserror|read|readpast|readtext|reconfigure|references remove|reorg|replace|replication,reservepagegap|return|returns|revoke|role|rollback|rowcount|rows|rule)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(save|schema|select|set|setuser|shared|shutdown|some|statistics|stringsize|stripe|sum|syb_identity,syb_restree|syb_terminate)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(table|temp|temporary|textsize|to|tran|transaction|trigger|truncate|tsequal)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(union|unique|unpartition|update|use|user|user_option|using)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(values|varying|view)].*\n","Invalid use of reserved word"));
			sbError.Append(checkRule(strInput, @"(?i)\[(waitfor|when|where|while|with|work|writetext)].*\n","Invalid use of reserved word"));
			return sbError;
		}

		private string checkRule(string strInput, string rule, string desc)
		{
			StringBuilder errFound = new StringBuilder("");
			Regex rx = new Regex(rule);
			Match mh = rx.Match(strInput);
			while(mh.Success)
			{
				errFound.Append("{" + desc + "} ~ " + mh.Value + "\r\n");
				mh = mh.NextMatch();
			}
			return errFound.ToString();
		}

		private string mapDatabases(Int32 ProjectId, string InStr, string dbConnectionString, string dbPassword)
		{
			string strMapped = InStr;
			DataTable dt;
			using (DbPortingAccessBase dac = GetDbPortingAccess())
			{
				dt = dac.GetMapTable(ProjectId, "dbo.SqlToSybMap", dbConnectionString, dbPassword);
			}
			foreach (DataRow dr in dt.Rows)
			{
				strMapped = Regex.Replace(strMapped,"(?i)" + dr["OrigWord"].ToString() + @"(\s|,|\))", dr["DestWord"].ToString() + "$1");
			}
			return strMapped;
		}

		private string convertFunctions(string InStr, string UdFunctionDb)
		{
			string tInStr=InStr;
			tInStr=Regex.Replace(tInStr,"(?i)REPLACE\\s*[(]","str_replace(");
			tInStr=Regex.Replace(tInStr,"(?i)LTRIM\\s*[(]",UdFunctionDb + ".dbo.fLTrim(");
			tInStr=Regex.Replace(tInStr,"(?i)RTRIM\\s*[(]",UdFunctionDb + ".dbo.fRTrim(");
			//Do not unmask the following as it may affect the pSendMail stored procedure:
			//tInStr=Regex.Replace(tInStr,"(?i)(master\\.dbo\\.xp_sendmail|master\\.\\.xp_sendmail|dbo\\.xp_sendmail|xp_sendmail)",UdFunctionDb + ".dbo.pSendMail");
			return tInStr;
		}
	}
}