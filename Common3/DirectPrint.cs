namespace RO.Common3
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Text;
	using System.Runtime.InteropServices;   
	using System.IO;

	[StructLayout( LayoutKind.Sequential)]
	public struct DOCINFO 
	{
		[MarshalAs(UnmanagedType.LPWStr)]public string pDocName;
		[MarshalAs(UnmanagedType.LPWStr)]public string pOutputFile; 
		[MarshalAs(UnmanagedType.LPWStr)]public string pDataType;
	} 

	// Should only execute this on the client tier:
	public class DirectPrint
	{
		[DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=false, CallingConvention=CallingConvention.StdCall )]
		public static extern long OpenPrinter(string pPrinterName,ref IntPtr phPrinter, int pDefault);
		[DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=false, CallingConvention=CallingConvention.StdCall )]
		public static extern long StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);
		[DllImport("winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern long StartPagePrinter(IntPtr hPrinter);
		[DllImport( "winspool.drv",CharSet=CharSet.Ansi,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern long WritePrinter(IntPtr hPrinter,string data, int buf,ref int pcWritten);            
		[DllImport( "winspool.drv" ,CharSet=CharSet.Unicode,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern long EndPagePrinter(IntPtr hPrinter);
		[DllImport( "winspool.drv", CharSet=CharSet.Unicode,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern long EndDocPrinter(IntPtr hPrinter);
		[DllImport("winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=true, CallingConvention=CallingConvention.StdCall )]
		public static extern long ClosePrinter(IntPtr hPrinter);
		
		public DirectPrint()
		{
		}

		public void PrintString(string printerPath, string docName, string sb, ref bool bPrintOK)
		{
			int pcWritten = 0;
			DOCINFO di = new DOCINFO();
			di.pDocName = docName;
			di.pDataType = "RAW";                        
			System.IntPtr lhPrinter = new System.IntPtr();

			OpenPrinter(printerPath, ref lhPrinter, 0);
			StartDocPrinter(lhPrinter, 1, ref di);
			StartPagePrinter(lhPrinter);  
			WritePrinter(lhPrinter, sb, sb.Length, ref pcWritten);
			EndPagePrinter(lhPrinter);
			EndDocPrinter(lhPrinter);
			ClosePrinter(lhPrinter);
			if (pcWritten > 0 && pcWritten == sb.Length)
			{
				bPrintOK = true;
			}
			else
			{
				bPrintOK = false;
			}
		}
	}
}