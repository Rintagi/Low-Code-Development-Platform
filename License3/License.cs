namespace RO.Common3
{
    using System;
    using System.Text;
    using System.Configuration;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Net;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    public partial class License : Key
    {
        private string PrevKey
        {
            get { return pPrevKey; }
            set { pPrevKey = value; }
        }

        private string CurrKey
        {
            get { return pCurrKey; }
            set { pCurrKey = value; }
        }

        public string EncryptString(string inStr)
        {
            return EncryptString(inStr, CurrKey);
        }

        public string EncryptString(string inStr, string inKey)
        {
            string outStr = string.Empty;
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            try
            {
                des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey));
                outStr = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(UTF8Encoding.UTF8.GetBytes(inStr), 0, UTF8Encoding.UTF8.GetBytes(inStr).Length));
            }
            catch
            {
                outStr = null;
            }
            hashmd5 = null;
            des = null;
            return outStr;
        }

        protected string DecryptString(string inStr, string inKey)
        {
            if (string.IsNullOrEmpty(inStr)) return null;

            string outStr = "";
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            try
            {
                des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey));
                outStr = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(inStr), 0, Convert.FromBase64String(inStr).Length));
            }
            catch
            {
                outStr = null;
            }
            hashmd5 = null;
            des = null;
            return outStr;
        }

        protected string DecryptString(string inStr)
        {
            if (string.IsNullOrEmpty(inStr)) return null;

            string outStr = "";
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            try
            {
                des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(CurrKey));
                outStr = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(inStr), 0, Convert.FromBase64String(inStr).Length));
            }
            catch
            {
                try
                {
                    des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(PrevKey));
                    outStr = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(inStr), 0, Convert.FromBase64String(inStr).Length));
                }
                catch
                {
                    outStr = null;
                }
            }
            hashmd5 = null;
            des = null;
            return outStr;
        }
    }
}
