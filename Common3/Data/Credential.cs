using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace RO.Common3.Data
{
	[SerializableAttribute]
	
	public class Credential
	{
        private string pSelectedLoginName;
        private string pLoginName;
		private byte[] pPassword;
        private string pProvider;

        public Credential() { }
				
		public Credential( string LoginName, string Password )
		{
			pLoginName = LoginName;
			SHA1 sha1 = SHA1.Create();
			pPassword = sha1.ComputeHash(Encoding.Unicode.GetBytes(Password));
		}

        public Credential(string SelectedLoginName, string LoginName, byte[] Password, string provider)
        {
            pLoginName = LoginName;
            pSelectedLoginName = SelectedLoginName;
            SHA1 sha1 = SHA1.Create();
            byte[] h = new byte[32];
            if (Password != null) Password.CopyTo(h, 0);
            pPassword = h;
            pProvider = provider;
        }			
	
		public string LoginName
		{
			get {return pLoginName;}
			set {pLoginName = value;}
		}

        public string SelectedLoginName
        {
            get { return pSelectedLoginName; }
            set { pSelectedLoginName = value; }
        }
			
		public byte[] Password
		{
			get {return pPassword;}
			set {pPassword = value;}
		}

        public string Provider
        {
            get { return pProvider; }
            set { pProvider = value; }
        }
    }
}