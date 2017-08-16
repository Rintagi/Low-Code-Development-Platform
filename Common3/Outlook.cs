namespace RO.Common3
{
	using System;
	using System.Text;
	using System.Configuration;

	public class Outlook
	{
		private static string FileFormat = @"BEGIN:VCALENDAR" + System.Environment.NewLine
		+ "PRODID:-//Microsoft Corporation//Outlook 11.0 MIMEDIR//EN" + System.Environment.NewLine
		+ "METHOD:PUBLISH" + System.Environment.NewLine
		+ "BEGIN:VEVENT" + System.Environment.NewLine
		+ "DTSTART:{DTSTART}" + System.Environment.NewLine
		+ "DTEND:{DTEND}" + System.Environment.NewLine
		+ "LOCATION:{LOCATION}" + System.Environment.NewLine
		+ "DTSTAMP:{DTSTAMP}" + System.Environment.NewLine
		+ "SUMMARY:{SUMMARY}" + System.Environment.NewLine
		+ "DESCRIPTION;ENCODING=QUOTED-PRINTABLE:{DESCRIPTION}" + System.Environment.NewLine
		+ "PRIORITY:{PRIORITY}" + System.Environment.NewLine
		+ "BEGIN:VALARM" + System.Environment.NewLine
		+ "TRIGGER:-PT15M" + System.Environment.NewLine
		+ "ACTION:DISPLAY" + System.Environment.NewLine
		+ "DESCRIPTION:Reminder" + System.Environment.NewLine
		+ "END:VALARM" + System.Environment.NewLine
		+ "END:VEVENT" + System.Environment.NewLine
		+ "END:VCALENDAR" + System.Environment.NewLine;

		//DTSTART:20070914T070000
		//DTEND:20070914T073000
		//LOCATION:1075 W Georgia
		//DTSTAMP:20070914T073000
		//SUMMARY:Test Event
		//PRIORITY:5
		//TRIGGER:-PT15M

		private static string ConvertTime(DateTime dt)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(dt.Year.ToString("##"));
			sb.Append(dt.Month.ToString("0#"));
			sb.Append(dt.Day.ToString("0#"));
			sb.Append("T");
			sb.Append(dt.Hour.ToString("0#"));
			sb.Append(dt.Minute.ToString("0#"));
			sb.Append("00"); //for seconds
			return sb.ToString();
		}

		public static string MkCalendar(string Subject, string Location, int Priority, string Description, DateTime dtStart, DateTime dtEnd)
		{
			string Temp = FileFormat;

			Temp = Temp.Replace("{DTSTART}", ConvertTime(dtStart));
			if (dtEnd.Kind != DateTimeKind.Unspecified)
			{
				Temp = Temp.Replace("{DTEND}", ConvertTime(dtEnd));
			}
			else
			{
				Temp = Temp.Replace("DTEND:{DTEND}" + System.Environment.NewLine, string.Empty);
			}
			return Temp.Replace("{LOCATION}", Location).Replace("{DTSTAMP}", ConvertTime(DateTime.Today)).Replace("{PRIORITY}", Priority.ToString()).Replace("{SUMMARY}", Subject).Replace("{DESCRIPTION}", Description.Replace("\r\n","=0D=0A").Replace("\n","=0D=0A"));
		}
	}
}
