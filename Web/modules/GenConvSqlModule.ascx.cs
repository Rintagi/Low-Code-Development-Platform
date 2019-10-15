namespace RO.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using System.Threading;
	using RO.Facade3;
	using RO.Rule3;
	using RO.Common3;
	using RO.Common3.Data;

	public partial class GenConvSqlModule : RO.Web.ModuleBase
	{
		private const string KEY_dtEntity = "Cache:dtEntity";
		private const string KEY_dtDataTier = "Cache:dtDataTier";
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppPw;
		public GenConvSqlModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				cTitleLabel.Text = "Convert SQL";
				cHelpLabel.Text = "Please paste the Microsoft SQL to be converted into the top section, select the appropriate project (for translation table) and target data tier (for special functions), then press the button to convert to Sybase syntax.";
				GetEntity();
			}
		}

		private void Page_Init(object sender, EventArgs e)
		{
			InitializeComponent();
		}

		#region Web Form Designer generated code
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            CheckAuthentication(true);
            if (LcSysConnString == null)
			{
				SetSystem(3);
			}
		}
		#endregion

        private void CheckAuthentication(bool pageLoad)
        {
            CheckAuthentication(pageLoad, true);
        }

		private void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppConnString = base.AppConnectStr(SystemId);
			LcAppPw = base.AppPwd(SystemId);
		}
		private void GetEntity()
		{
			DataTable dt = (DataTable)Session[KEY_dtEntity];
			if (dt == null) { dt = (new RobotSystem()).GetEntityList(); }
			if (dt != null)
			{
				Session[KEY_dtEntity] = dt;
				cEntityId.DataSource = dt; cEntityId.DataBind();
				if (cEntityId.Items.Count > 0)
				{
					ListItem li = null;
					if (base.CPrj != null)
					{
						li = cEntityId.Items.FindByValue(base.CPrj.EntityId.ToString());
					}
					if (li != null) { cEntityId.ClearSelection(); li.Selected = true; } 
					else
					{
						cEntityId.Items[0].Selected = true;
					}
					cEntityId_SelectedIndexChanged(null, new EventArgs());
				}
			}
		}

		private void GetDataTier(Int16 EntityId)
		{
			DataTable dt = (new RobotSystem()).GetDataTier(EntityId);
			if (dt != null)
			{
				Session[KEY_dtDataTier] = dt;
				cDataTierId.DataSource = dt; cDataTierId.DataBind();
				if (cDataTierId.Items.Count > 0) {cDataTierId.Items[0].Selected = true;}
			}
		}

		protected void cEntityId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GetDataTier(Int16.Parse(cEntityId.SelectedValue));
			cDataTierId.Focus();
		}

		protected void cGenButton_Click(object sender, System.EventArgs e)
		{
			DbPorting pt = new DbPorting();
			DataTable dt = (DataTable)Session[KEY_dtDataTier];
			string TarUdFunctionDb = dt.Rows[cDataTierId.SelectedIndex]["DesDatabase"].ToString();
			cSqlTo.Text = pt.SqlToSybase(Int16.Parse(cEntityId.SelectedValue), TarUdFunctionDb, cSqlFr.Text, LcAppConnString, LcAppPw);
		}
	}
}