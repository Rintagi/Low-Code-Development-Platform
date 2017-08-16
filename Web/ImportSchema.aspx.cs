namespace RO.Web
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.SessionState;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using System.IO;
    using RO.Facade3;
	using RO.Common3;

	public partial class ImportSchema : PageBase
	{
		public ImportSchema()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(Request.QueryString["key"]))
			{
				try {cSchema.Text = Session["ImportSchema"].ToString();} catch {}
			}
			else if (string.IsNullOrEmpty(Request.QueryString["csy"]))
			{
				PreMsgPopup("Please make sure QueryString has 'csy=' followed by the SystemId and try again.");
			}
			else
			{
                cSchema.Text = Request.QueryString["scm"].ToUpper() == "W" ? ImportSchemaModule.GetWizardSchema() : ImportSchemaModule.GetScrSchema();
			}
		}

        private void ExportToStream(string sFileName, string content)
        {
            Response.Buffer = true;
            Response.ClearHeaders();
            Response.ClearContent();
            if (sFileName.EndsWith(".xls") || sFileName.EndsWith(".xlsx")) Response.ContentType = "application/vnd.ms-excel";
            else if (sFileName.EndsWith(".rtf")) Response.ContentType = "text/rtf";
            else Response.ContentType = "APPLICATION/OCTET-STREAM";
            Response.AppendHeader("Content-Disposition", "Attachment; Filename=" + sFileName);
            byte[] bContent = System.Text.UTF8Encoding.UTF8.GetBytes(content);
            Response.OutputStream.Write(bContent, 0, bContent.Length);
            Response.End();
        }

        protected void cTmplDownload_Clicked(object sender, EventArgs e)
        {
            string tmpl = Request.QueryString["scm"].ToUpper() == "W" ? ImportSchemaModule.GetWizardTmpl() : ImportSchemaModule.GetScrGridTmpl();
            ExportToStream("ImportTemplate.xls", tmpl);

        }

		protected void Page_Init(object sender, EventArgs e)
		{
			InitializeComponent();
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

        void PreMsgPopup(string msg)
        {
            string iconUrl = "images/warning.gif";
            string focusOnCloseId = string.Empty;
            string msgContent = msg;
            string script =
            @"<script type='text/javascript' language='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
            ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
        }
    }
}