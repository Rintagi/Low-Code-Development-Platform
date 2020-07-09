using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RO.Web
{
    public partial class ViewChartModule : RO.Web.ModuleBase
    {
        public ViewChartModule()
        {
            this.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyId = Request.QueryString["key"];

                if (!string.IsNullOrEmpty(keyId))
                {
                    byte sid = 3;
                    string dbConnectionString = base.SysConnectStr(sid);

                    DataTable dt = null;

                    try
                    {
                        dt = (new AdminSystem()).GetMstById("GetAdmFlowChart1027ById", keyId, dbConnectionString, base.AppPwd(base.LCurr.DbId));
                    }
                    catch (Exception err) 
                    {
                        Common3.Utils.NeverThrow(err);
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var dr = dt.Rows[0];
                        var jsonString = dr["ChartData1325"].ToString();

                        var x = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, object>>>>(jsonString);

                        foreach(var st in x["states"].Keys)
                        {
                            var attr = (JObject)x["states"][st]["attr"];
                            var href = attr["href"];
                            if (href != null)
                            {
                                //change link logic goes here
                                //attr["href"] = "http://www.google.com";
                            }        
                        }

                        hfChartData.Value = JsonConvert.SerializeObject(x);
                    }
                }
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