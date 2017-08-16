using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;

using RO.SystemFramewk;

namespace RoboCoder.WebControls
{
    [DefaultProperty("PageNo"), ToolboxData("<{0}:Signature runat=server></{0}:Signature>")]
    [ValidationProperty("Text")]
    public class Signature : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        private Panel signaturePanel;
        private Panel canvasPanel;
        private TextBox data;
        private Button clearSignature;
        private Button saveSignature;
        private LiteralControl canvas;
        private Label topMsg;
        private Label bottomMsg;

        //data_uri = "data:image/png;base64,iVBORw0K..."
        public byte[] SignatureData
        {
            get
            {
                EnsureChildControls();
                if (!string.IsNullOrEmpty(data.Text))
                {
                    string[] split = data.Text.Split(new char[] { ',' });
                    return Convert.FromBase64String(split[1]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                EnsureChildControls();
                data.Text = (value != null) ? "data:image/png;base64," + Convert.ToBase64String(value) : string.Empty;
            }
        }

        /* need this property for validating the signature control */
        public string Text
        {
            get
            {
                EnsureChildControls();
                return data.Text;
            }
        }

        public System.Drawing.Color PenColor
        {
            get
            {
                object oo = ViewState["_PenColor"];
                return (oo == null) ? System.Drawing.Color.Black : (System.Drawing.Color)ViewState["_PenColor"];
            }
            set
            {
                ViewState["_PenColor"] = value;
            }
        }

        public System.Drawing.Color BackgroundColor
        {
            get
            {
                object oo = ViewState["_BackgroundColor"];
                return (oo == null) ? System.Drawing.Color.White : (System.Drawing.Color) ViewState["_BackgroundColor"];
            }
            set
            {
                ViewState["_BackgroundColor"] = value;
            }
        }

        // default = 2.5
        public float MaxLineWidth
        {
            get
            {
                object oo = ViewState["_MaxLineWidth"];
                return (oo == null) ? (float)2.5 : (float)oo;
            }
            set 
            {
                ViewState["_MaxLineWidth"] = value;
            }
        }

        //default = 0.5
        public float MinLineWidth
        {
            get
            {
                object oo = ViewState["_MinLineWidth"];
                return (oo == null) ? (float)0.5 : (float)oo;
            }
            set
            {
                ViewState["_MinLineWidth"] = value;
            }
        }

        public override string CssClass
        {
            get
            {
                return base.CssClass;
            }
            set
            {
                base.CssClass = value;
            }
        }


        public string OnChange
        {
            set
            {
                EnsureChildControls();

                if (data.Attributes["onchange"] != null)
                {
                    data.Attributes["onchange"] = value;
                }
                else
                {
                    data.Attributes.Add("onchange", value);
                }
            }
            get
            {
                EnsureChildControls();
                string result = string.Empty;

                if (data.Attributes["onchange"] != null)
                {
                    result = data.Attributes["onchange"];
                }
                return result;
            }
        }

        public string SaveButtonText
        {
            get
            {
                object oo = ViewState["_SaveButtonText"];
                return (oo == null) ? "Save" : (string)oo;
            }
            set
            {
                ViewState["_SaveButtonText"] = value;
            }
        }

        public string ClearButtonText
        {
            get
            {
                object oo = ViewState["_ClearButtonText"];
                return (oo == null) ? string.Empty : (string)oo;
            }
            set
            {
                ViewState["_ClearButtonText"] = value;
            }
        }

        public string HintMessage
        {
            get
            {
                object oo = ViewState["_HintMessage"];
                return (oo == null) ? "" : (string)oo;
            }
            set
            {
                ViewState["_HintMessage"] = value;
            }
        }

        public bool ShowSaveButton
        {
            get
            {
                object oo = ViewState["_ShowSaveButton"];
                return (oo == null) ? true : (bool)oo;
            }
            set
            {
                ViewState["_ShowSaveButton"] = value;
            }
        }

        public bool ShowClearButton
        {
            get
            {
                object oo = ViewState["_ShowClearButton"];
                return (oo == null) ? true : (bool)oo;
            }
            set
            {
                ViewState["_ShowClearButton"] = value;
            }
        }

        private string ConvertColorToRGB(System.Drawing.Color c)
        {
            return string.Format("rgb({0}, {1}, {1})", c.R, c.G, c.B);
        }

        public void ClearSignature()
        {
            EnsureChildControls();
            this.SignatureData = null;
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            signaturePanel = new Panel();
            canvasPanel = new Panel();
            data = new TextBox();
            clearSignature = new Button();
            saveSignature = new Button();
            topMsg = new Label();
            bottomMsg = new Label();

            data.ID = "SigId";

            signaturePanel.Controls.Add(topMsg);
            signaturePanel.Controls.Add(bottomMsg);

            signaturePanel.Controls.Add(clearSignature);
            signaturePanel.Controls.Add(saveSignature);
            signaturePanel.Controls.Add(data);

            Controls.Add(signaturePanel);

            canvas = new LiteralControl("<canvas class=\"SignCanvas\" id=\"" + data.ClientID + "_Canvas" + "\"></canvas>");
            canvasPanel.Controls.Add(canvas);

            Controls.Add(canvasPanel);

            SetControls();
        }

        private void SetControls()
        {
            clearSignature.Text = this.ClearButtonText;
            clearSignature.OnClientClick = string.Format("ClearSignature('{0}'); return false;", data.ClientID);

            saveSignature.Text = this.SaveButtonText;
            saveSignature.OnClientClick = string.Format("SaveSignature('{0}'); return false;", data.ClientID);

            bottomMsg.Text = this.HintMessage;

            if (base.Attributes["OnChange"] != null && data.Attributes["OnChange"] == null)
            {
                data.Attributes.Add("OnChange", base.Attributes["OnChange"]);
            }

            foreach(string key in base.Style.Keys)
            {
                bottomMsg.Style.Add(key, base.Style[key]);
            }

            canvasPanel.CssClass = "SignCanvasContainer";

            data.Style.Add("display", "none");
        }

        private string SerializeProperties()
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();

            properties.Add("backgroundColor", ConvertColorToRGB(this.BackgroundColor));
            properties.Add("penColor", ConvertColorToRGB(this.PenColor));
            properties.Add("minWidth", this.MinLineWidth.ToString());
            properties.Add("maxWidth", this.MaxLineWidth.ToString());
            //properties.Add("readOnly", this.ReadOnly ? "Y" : "N");

            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            return jss.Serialize(properties);
        }

        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
        }

        protected override void Render(HtmlTextWriter output)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                output.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            }
            output.AddAttribute(HtmlTextWriterAttribute.Id, data.ClientID + "ctl");
            output.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
            output.RenderBeginTag(HtmlTextWriterTag.Div); // container wrapper

            if (!this.Enabled)
            {
                // just assign data.text to the source..
                data.Visible = false;

                output.AddAttribute(HtmlTextWriterAttribute.Src, data.Text);
                output.AddAttribute(HtmlTextWriterAttribute.Class, "SignImage");
                output.RenderBeginTag(HtmlTextWriterTag.Img);
                output.RenderEndTag(); //img
            }
            else
            {
                data.Visible = true;

                //canvas
                output.AddAttribute(HtmlTextWriterAttribute.Class, "SignControlContainer");
                output.RenderBeginTag(HtmlTextWriterTag.Div);
                canvasPanel.RenderControl(output);
                data.RenderControl(output);
                output.RenderEndTag();

                if (!string.IsNullOrEmpty(bottomMsg.Text))
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "SignBottomMsg");
                    output.RenderBeginTag(HtmlTextWriterTag.Div);
                    bottomMsg.RenderControl(output);
                    output.RenderEndTag();
                }

                output.AddAttribute(HtmlTextWriterAttribute.Class, "SignButtonContainer");
                output.RenderBeginTag(HtmlTextWriterTag.Div);


                if (this.ShowClearButton)
                {
                    //output.AddAttribute(HtmlTextWriterAttribute.Class, "SignClearButton");
                    //output.RenderBeginTag(HtmlTextWriterTag.Div);
                    clearSignature.RenderControl(output);
                    //output.RenderEndTag();
                }

                if (this.ShowSaveButton)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "SignSaveButton");
                    output.RenderBeginTag(HtmlTextWriterTag.Div);
                    saveSignature.RenderControl(output);
                    output.RenderEndTag();
                }

                output.RenderEndTag();

                ClientScriptManager cs = this.Page.ClientScript;
                ScriptManager.RegisterStartupScript(data, data.GetType(), data.ClientID + "canvas", @"ActivateSignature('" + data.ClientID + "','" + data.ClientID + "_Canvas" + "','" + SerializeProperties() + "');", true);
            }

            output.RenderEndTag(); // div
        }
    }
}