namespace RO.SystemFramewk
{
	using System;
	using System.Diagnostics;

	public class ApplicationAssert
	{
		public static void CheckCondition(bool condition, string sourceText, string causeText, string errorText)
		{
			if (!condition)
			{
				if (causeText != string.Empty) {errorText = causeText + ": " + errorText;}
				if (sourceText != string.Empty) {errorText = sourceText + ": " + errorText;}
				throw new ApplicationException(errorText);
			}
		}
	}
}