using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;

namespace RO.Web
{
	public partial class ExpModule: RO.Web.ModuleBase
	{
        public ExpModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(Session["ExportFnm"].ToString()) && !string.IsNullOrEmpty(Session["ExportStr"].ToString()))
			{
				System.IO.Stream oStream = null;
				StreamWriter sw = null;
				oStream = new MemoryStream();
				sw = new StreamWriter(oStream, System.Text.Encoding.UTF8);
				sw.WriteLine(Session["ExportStr"].ToString());
				sw.Flush();
				oStream.Seek(0, SeekOrigin.Begin);
				Response.Buffer = true;
				Response.ClearHeaders();
				Response.ClearContent();
				Response.ContentType = "APPLICATION/OCTET-STREAM";
				Response.AppendHeader("Content-Disposition", "Attachment; Filename=" + Session["ExportFnm"].ToString());
				byte[] streamByte = new byte[oStream.Length];
				oStream.Read(streamByte, 0, (int)oStream.Length);
				Response.BinaryWrite(streamByte);
				Response.End();
				if (oStream != null) { oStream.Close(); }
				if (sw != null) { sw.Close(); }
			}
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
	}
}