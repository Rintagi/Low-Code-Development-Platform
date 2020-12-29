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
using RO.Common3;

namespace RoboCoder.WebControls
{
    [ToolboxData("<{0}:OTPTextBox runat=server></{0}:OTPTextBox>")]
    [ValidationProperty("Text")]
    public class OTPTextBox : System.Web.UI.WebControls.TextBox
    {
        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        public int TimeSkew
        {
            get { try { return int.Parse(ViewState["TimeSkew"] as string ?? "2"); } catch { return 2; }; }
            set { ViewState["TimeSkew"] = value.ToString(); }
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
                ViewState.Remove("Validated");
            }
        }
        [Bindable(false)]
        [Category("Properties")]
        [Localizable(true)]
        public bool IsValid
        {
            get 
            {
                bool valid = (ViewState["Validated"] as string) == "Y";
                int skew = TimeSkew;
                int forwardSkew = 2; // allowance clock is slower than client by 2 slot
                for (int i = -1 * skew; i <= (skew + forwardSkew) && !valid; i++)
                {
                    valid = valid || (!string.IsNullOrEmpty(EncryptionKey) 
                                    && 
                                    (RO.Common3.GoogleAuthenticator.CalculateOneTimePassword(System.Text.UTF8Encoding.UTF8.GetBytes(EncryptionKey), i) == Text.Trim()
                                     ||
                                     RO.Common3.GoogleAuthenticator.CalculateOneTimePassword(System.Text.UTF8Encoding.UTF8.GetBytes(EncryptionKey), i, 8) == Text.Trim()
                                     )
                                    );
                }
                return valid;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.TextChanged += OTPTextBox_TextChanged;
        }

        void OTPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IsValid && !string.IsNullOrEmpty(EncryptionKey)) ViewState["Validated"] = "Y"; 
        }
    }
}