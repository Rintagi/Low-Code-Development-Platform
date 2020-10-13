using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RO.SystemFramewk;
using System.Linq;

namespace RoboCoder.WebControls
{
    // duplicated logic here (same as in License.cs) as we don't want to pull in depedency for custom control
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

    [ToolboxData("<{0}:EncryptedTextBox runat=server></{0}:EncryptedTextBox>")]
    [ValidationProperty("Text")]
    public class EncryptedTextBox : System.Web.UI.WebControls.TextBox
    {
        [Bindable(false)]
        [Category("Properties")]
        [DefaultValue(4)]
        [Localizable(true)]
        public int VisibleCount
        {
            get { try { return int.Parse(ViewState["VisibleCount"] as string ?? "4"); } catch { return 4; }; }
            set { ViewState["VisibleCount"] = value.ToString();}
        }
        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        public bool Masked
        {
            get { return (ViewState["Masked"] as string ?? "Y") == "Y"; }
            set { ViewState["Masked"] = value ? "Y" : "N"; }
        }
        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        public bool DesMD5
        {
            get { return (ViewState["DesMD5"] as string ?? "N") == "Y"; }
            set { ViewState["DesMD5"] = value ? "Y" : "N"; }
        }
        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        private bool MaskedThis
        {
            get { return (ViewState["MaskedThis"] as string ?? "Y") == "Y" && Masked; }
            set { ViewState["MaskedThis"] = value ? "Y" : "N"; }
        }
        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        public string EncryptionKey
        {
            private get { return ViewState["EncryptKey"] as string; }
            set 
            { 
                ViewState["EncryptKey"] = value;
                if (!string.IsNullOrEmpty(ActualText)) Text = ActualText;
            }
        }
        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        public bool WasEncrypted
        {
            get { return (ViewState["WasEncrypted"] as string ?? "N") == "Y"; }
            set { ViewState["WasEncrypted"] = value ? "Y" : "N"; }
        }
        private bool IsROEncryptedString(string text)
        {
            int visiblePart = text.IndexOf('-');
            string encryptedValue = visiblePart > 0 ? text.Substring(0, visiblePart) : text;
            try
            {
                byte[] x = Convert.FromBase64String(encryptedValue);
                return x[0] > 0 && x[0] <= 1;
            }
            catch { return false; }
        }
        private string ROEncryptString(string inStr, string inKey)
        {
            string outStr = string.Empty;
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            // general format
            // base64(version byte + byte[] of IV + encrypted content) + '-' + visible tail portion
            // version 1 3DES CBC with 8 byte IV
            // version 2 AES256 CBC with 16 byte IV

            byte[] ver = new byte[] { (byte)(DesMD5 ? 1 : 2) };
            byte[] iv = new byte[DesMD5 ? 8 : 16];
            rng.GetBytes(iv);

            var hasher = new ROHasher(DesMD5);
            SymmetricAlgorithm cipher = DesMD5 ? (SymmetricAlgorithm)new TripleDESCryptoServiceProvider() : (SymmetricAlgorithm)new AesCryptoServiceProvider();
            cipher.Mode = CipherMode.CBC;
            cipher.IV = iv;
            cipher.Key = hasher.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey)).Take(DesMD5 ? 16 : 32).ToArray();
            byte[] encryptedBlock = cipher.CreateEncryptor().TransformFinalBlock(UTF8Encoding.UTF8.GetBytes(inStr), 0, UTF8Encoding.UTF8.GetBytes(inStr).Length);
            outStr = Convert.ToBase64String(ver.Concat(iv).Concat(encryptedBlock).ToArray());
            return outStr;
        }

        private string RODecryptString(string inStr, string inKey)
        {
            try
            {
                var hasher = new ROHasher(DesMD5);
                byte[] encryptedData = Convert.FromBase64String(inStr);
                byte ver = encryptedData[0];
                int ivSize = 0;
                if (ver == 1) ivSize = 8;
                else if (ver == 2) ivSize = 16;
                else throw new Exception("unsupported encryption version");

                SymmetricAlgorithm cipher = ver == 1 ? (SymmetricAlgorithm)new TripleDESCryptoServiceProvider() : (SymmetricAlgorithm)new AesCryptoServiceProvider();

                try
                {
                    cipher.IV = encryptedData.Skip(1).Take(ivSize).ToArray();
                    cipher.Mode = CipherMode.CBC;
                    cipher.Key = hasher.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey)).Take(DesMD5 ? 16 : 32).ToArray();
                    string outStr = UTF8Encoding.UTF8.GetString(cipher.CreateDecryptor().TransformFinalBlock(encryptedData.Skip(1 + ivSize).ToArray(), 0, encryptedData.Length - (1 + ivSize)));
                    return outStr;
                }
                catch
                {
                    return null;
                }
            }
            catch {
                return null;
            }
        }

        public override string Text
        {
            get 
            {
                string actualValue = ViewState["ActualValue"] as string ?? string.Empty;
                int len = actualValue.Length;
                //string x = len <= VisibleCount || !MaskedThis ? actualValue : new string('\u0058', len <= VisibleCount ? len : len - VisibleCount) + actualValue.Substring(actualValue.Length - VisibleCount);
                string x = len <= VisibleCount || !MaskedThis ? actualValue : new string('\u002A', len <= VisibleCount ? len : len - VisibleCount) + actualValue.Substring(actualValue.Length - VisibleCount);
                return x;
            }
            set 
            {
                string text = value;
                string actualValue = value;
                bool FromEncryptedSrc = false;
                try
                {
                    int visiblePart = text.IndexOf('-');
                    string encryptedValue = visiblePart > 0 ? text.Substring(0, visiblePart) : text;
                    actualValue = RODecryptString(encryptedValue, EncryptionKey);
                    WasEncrypted = true;
                    FromEncryptedSrc = true;
                }
                catch
                {
                }
                finally
                {

                    if (WasEncrypted ||
                        (text.Length > VisibleCount && !text.Substring(0, text.Length - VisibleCount).Contains("*"))
                        )
                    {
                        if (!FromEncryptedSrc) ViewState["KeyinValue"] = text;
                        else if ((ViewState["KeyinValue"] ?? "").ToString() != actualValue) try { ViewState.Remove("KeyinValue"); }
                            catch { };
                        var oldValue = (ViewState["ActualValue"] ?? "").ToString();
                        MaskedThis = WasEncrypted && FromEncryptedSrc && (ViewState["KeyinValue"] == null || (ViewState["KeyinValue"] ?? "").ToString() != oldValue);
                        //MaskedThis = WasEncrypted;

                        ViewState["ActualValue"] = actualValue;
                    }
                    else if (IsROEncryptedString(text))
                    {
                        ViewState["ActualValue"] = actualValue;
                    }
                }
            }
        }

        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        public string ActualText
        {
            get { return ViewState["ActualValue"] as string ?? string.Empty; }
        }
        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        public string EncryptedText
        {
            get 
            {
                string value = ViewState["ActualValue"] as string ?? string.Empty;
                try
                {
                    return (value.Length == 0 || string.IsNullOrEmpty(EncryptionKey) || value.Length <= VisibleCount) ? value : ROEncryptString(value, EncryptionKey) + "-" + value.Substring(value.Length - VisibleCount, VisibleCount);
                }
                catch
                {
                    return value;
                }
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.TextChanged += EncryptedTextBox_TextChanged;
        }

        void EncryptedTextBox_TextChanged(object sender, EventArgs e)
        {
            string x = ((TextBox)sender).Text;
        }
    }
}