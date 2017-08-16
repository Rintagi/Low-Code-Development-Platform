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

            byte[] ver = new byte[]{1}; 
            byte[] iv = new byte[8];                                                                                            
            rng.GetBytes(iv);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Mode = CipherMode.CBC;
            des.IV = iv;
            des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey));
            byte[] encryptedBlock = des.CreateEncryptor().TransformFinalBlock(UTF8Encoding.UTF8.GetBytes(inStr), 0, UTF8Encoding.UTF8.GetBytes(inStr).Length);
            outStr = Convert.ToBase64String(ver.Concat(iv).Concat(encryptedBlock).ToArray());
            return outStr;
        }
        protected string RODecryptString(string inStr, string inKey)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            byte[] encryptedData = Convert.FromBase64String(inStr);
            byte ver = encryptedData[0];
            int ivSize = 0;
            if (ver == 1) ivSize = 8;
            else throw new Exception("unsupported encryption version");

            des.IV = encryptedData.Skip(1).Take(ivSize).ToArray();
            des.Mode = CipherMode.CBC;
            des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey));
            string outStr = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(encryptedData.Skip(1 + ivSize).ToArray(), 0, encryptedData.Length - (1 + ivSize)));
            return outStr;
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