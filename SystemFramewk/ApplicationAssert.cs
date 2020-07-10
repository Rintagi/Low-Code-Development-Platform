namespace RO.SystemFramewk
{
	using System;
	using System.Diagnostics;
    using System.Text.RegularExpressions;

	public class ApplicationAssert
	{
        private static Regex formatter = new Regex(@"ERROR\s*\[\d+\].*\[Microsoft\]\[ODBC.+\]\[SQL Server\]");
		public static void CheckCondition(bool condition, string sourceText, string causeText, string errorText)
		{
			if (!condition)
			{
                // remove driver related info from mesage
                errorText = formatter.Replace(errorText, "");

				if (causeText != string.Empty) {errorText = causeText + ": " + errorText;}
				if (sourceText != string.Empty) {errorText = sourceText + ": " + errorText;}
				throw new ApplicationException(errorText);
			}
		}
	}
}