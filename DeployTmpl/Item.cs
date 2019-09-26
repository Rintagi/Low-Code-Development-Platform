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
		private string iType = "DEV";
		private string iKey = "xxxxxxxx";
		private string oldProjectRoot = @"C:\Rintagi\RO";
        private string roENCKey = "xxxxxx";

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
			return oldProjectRoot;
		}
		public string GetOldProjectRoot()
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
			lvVersion.Items.Add("R5.13.10622    Administration");
			lvVersion.Items.Add("V2.3.91123     Common Area");
		}
	}
}
