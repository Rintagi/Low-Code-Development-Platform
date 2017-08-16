using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
namespace Install
{
	public class Item
	{
		private string oldNS = "RO";
		private string iType = "NPTY";
		private string iKey = "7cb95cef1483489fafb691107aa78b13";
		private string roENCKey= "13467231ea794211aab756008f8500af";
		public string GetOldNS()
		{
			return oldNS;
		}
		public string GetInsType()
		{
			return iType;
		}
		public string GetInsKey()
		{
			return iKey;
		}
		public string GetROKey()
		{
			return roENCKey;
		}
		public void SetROKey(string key)
		{
			roENCKey = key;
		}
		public void SetVersion(ListView lvVersion)
		{
			lvVersion.Items.Add("R10.62.70620   Administration");
			lvVersion.Items.Add("V2.7.61230     Common App");
		}
	}
}
