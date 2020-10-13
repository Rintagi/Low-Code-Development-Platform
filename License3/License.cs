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

    public class ROHasher
    {
        private object _hasher;
        private bool _md5;
        public ROHasher(bool md5)
        {
            _md5 = md5;
            _hasher = md5 ? (object)new MD5CryptoServiceProvider() : (object)new SHA256CryptoServiceProvider();
        }
        public byte[] ComputeHash(byte[] val)
        {
            return _md5
                ? ((MD5CryptoServiceProvider)_hasher).ComputeHash(val)
                : ((SHA256CryptoServiceProvider)_hasher).ComputeHash(val);
        }
    }

    public class ROPwdCrypto
    {
        // legacy version using md5+3des
        // new version with sha256 + aes256
        private SymmetricAlgorithm _encryptor;
        private bool _md5;
        public ROPwdCrypto(bool md5)
        {
            _md5 = md5;
            _encryptor = md5 ? (SymmetricAlgorithm)new TripleDESCryptoServiceProvider() : (SymmetricAlgorithm)new AesCryptoServiceProvider();
            
        }
        public byte[] Encrypt(byte[] key, byte[] val)
        {
            _encryptor.Mode = CipherMode.ECB;
            _encryptor.Key = key.Take(_md5 ? 16 : 32).ToArray();
            return _encryptor.CreateEncryptor().TransformFinalBlock(val, 0, val.Length);
        }
        public byte[] Decrypt(byte[] key, byte[] val)
        {
            _encryptor.Mode = CipherMode.ECB;
            _encryptor.Key = key.Take(_md5 ? 16 : 32).ToArray();
            return _encryptor.CreateDecryptor().TransformFinalBlock(val, 0, val.Length);
        }
    }

    public partial class License : Key
    {
        private bool _md5 = false;

        public License()
        {
            _md5 = true;
        }
        public License(bool md5)
        {
            _md5 = md5;
        }
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

        public string EncryptString(string inStr, bool? md5=null)
        {
            return EncryptString(inStr, CurrKey, md5);
        }

        public string EncryptString(string inStr, string inKey, bool? md5 = null)
        {
            string outStr = string.Empty;
            var hasher = new ROHasher(md5 != null ? md5.Value : _md5);
            var encryptor = new ROPwdCrypto(md5 != null ? md5.Value : _md5);
            try
            {
                outStr = Convert.ToBase64String(
                    encryptor.Encrypt(
                        hasher.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey)), 
                        UTF8Encoding.UTF8.GetBytes(inStr))
                    );
            }
            catch (Exception ex)
            {
                outStr = null;
                if (ex == null) throw;
            }
            hasher = null;
            encryptor = null;
            return outStr;
        }

        protected string DecryptString(string inStr, string inKey, bool? md5 = null)
        {
            if (string.IsNullOrEmpty(inStr)) return null;

            string outStr = "";
            var hasher = new ROHasher(md5 != null ? md5.Value : _md5);
            var encryptor = new ROPwdCrypto(md5 != null ? md5.Value : _md5);
            try
            {
                outStr = UTF8Encoding.UTF8.GetString(
                    encryptor.Decrypt(
                        hasher.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey)), 
                        Convert.FromBase64String(inStr))
                    );
            }
            catch
            {
                outStr = null;
            }
            hasher = null;
            encryptor = null;
            return outStr;
        }

        protected string DecryptString(string inStr, bool? md5 = null)
        {
            if (string.IsNullOrEmpty(inStr)) return null;

            try
            {
                return DecryptString(inStr, CurrKey, md5);
            }
            catch
            {
                try
                {
                    return DecryptString(inStr, PrevKey);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
