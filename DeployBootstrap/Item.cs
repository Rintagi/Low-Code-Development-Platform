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
		private string iKey = "5696923b0a30471b91cc6e5304aec44b";
		private string roENCKey= "8a47d1f5b4554f45b99078fcb6ab9645";
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
			lvVersion.Items.Add("R10.84.90913   Administration");
			lvVersion.Items.Add("V              Common App");
		}
	}
}
