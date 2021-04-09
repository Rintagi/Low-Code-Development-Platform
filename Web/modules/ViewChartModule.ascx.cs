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
                string compId = GetCompany();

                DataTable dtChartList = null;
                int filterId = 0;
                string key = string.Empty;
                try
                {
                    try { 
                        dtChartList = (new AdminSystem()).GetLis(1027, "GetLisAdmFlowchart1027", true, "Y", 0, null, null, filterId, key, string.Empty, GetScrCriteria(), base.LImpr, base.LCurr, UpdCriteria(false)); 
                    }
                    catch { 
                        dtChartList = (new AdminSystem()).GetLis(1027, "GetLisAdmFlowchart1027", true, "N", 0, null, null, filterId, key, string.Empty, GetScrCriteria(), base.LImpr, base.LCurr, UpdCriteria(false)); 
                    }
                }
                catch (Exception err) { 
                    Common3.Utils.NeverThrow(err); 
                }

                if (dtChartList != null && dtChartList.Rows.Count > 0)
                {
                    string companyId = compId;
                    //string keyId = dtChartList.Rows[1][0].ToString(); // by default using the first one if no key passed in from querysting and no default
                    string keyId = "";

                    foreach (DataRow dr in dtChartList.Rows)
                    {
                        if (string.IsNullOrEmpty(companyId) || companyId == "0")
                        {
                            if (dr[2].ToString() == "Y" && string.IsNullOrEmpty(dr[3].ToString()))
                            {
                                keyId = dr[0].ToString();
                                break;
                            }
                        }
                        else
                        {
                            if (dr[2].ToString() == "Y")
                            {
                                keyId = dr[0].ToString();
                                break;
                            }
                        }
                        
                    }

                    if (!string.IsNullOrEmpty(Request.QueryString["key"]))
                    {
                        keyId = Request.QueryString["key"];
                    }

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

                        foreach (var st in x["states"].Keys)
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
                else
                {
                    return;
                }

            }
        }

        private DataView GetScrCriteria()
        {
            DataTable dtScrCri = new DataTable();
            try
            {
                byte sid = 3;
                string dbConnectionString = base.SysConnectStr(sid);
                dtScrCri = (new AdminSystem()).GetScrCriteria("1027", dbConnectionString, base.AppPwd(base.LCurr.DbId));
            }
            catch{}

            return dtScrCri.DefaultView;
        }

        private DataSet UpdCriteria(bool bUpdate)
        {
            string companyId = LCurr.CompanyId.ToString();

            DataSet ds = new DataSet();
            ds.Tables.Add(MakeColumns(new DataTable("DtScreenIn")));
            DataRow dr = ds.Tables["DtScreenIn"].NewRow();
            DataView dvCri = GetScrCriteria();
            foreach (DataRowView drv in dvCri)
            {
                if (drv["DisplayName"].ToString() == "ComboBox")
                {
                    if (companyId != null && companyId != string.Empty && companyId != "0")
                    {
                        dr[drv["ColumnName"].ToString()] = companyId;
                    }
                }
            }
            ds.Tables["DtScreenIn"].Rows.Add(dr);
            
            return ds;
        }

        private string GetCompany()
        {
            
            string curCompanyId = LCurr.CompanyId.ToString();
            string companyId = curCompanyId;
            
            DataTable dt = (DataTable)Session["CompanyList"];
            if (dt == null)
            {
                dt = (new LoginSystem()).GetCompanyList(base.LImpr.Usrs, base.LImpr.RowAuthoritys, base.LImpr.Companys);
            }

            foreach (DataRow dr in dt.Rows)
            {
                string xCompanyId = dr["CompanyId"].ToString();

                if ((string.IsNullOrEmpty(curCompanyId) || curCompanyId == "0") && xCompanyId != "")
                {
                    companyId = xCompanyId;
                    break;
                }
                else if ((string.IsNullOrEmpty(xCompanyId) && curCompanyId == "0"))
                {
                    companyId = "";
                    break;
                }
            }

            return companyId;
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            DataView dvCri = GetScrCriteria();
            foreach (DataRowView drv in dvCri)
            {
                if (drv["DataTypeSysName"].ToString() == "DateTime") { columns.Add(drv["ColumnName"].ToString(), typeof(DateTime)); }
                else if (drv["DataTypeSysName"].ToString() == "Byte") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte)); }
                else if (drv["DataTypeSysName"].ToString() == "Int16") { columns.Add(drv["ColumnName"].ToString(), typeof(Int16)); }
                else if (drv["DataTypeSysName"].ToString() == "Int32") { columns.Add(drv["ColumnName"].ToString(), typeof(Int32)); }
                else if (drv["DataTypeSysName"].ToString() == "Int64") { columns.Add(drv["ColumnName"].ToString(), typeof(Int64)); }
                else if (drv["DataTypeSysName"].ToString() == "Single") { columns.Add(drv["ColumnName"].ToString(), typeof(Single)); }
                else if (drv["DataTypeSysName"].ToString() == "Double") { columns.Add(drv["ColumnName"].ToString(), typeof(Double)); }
                else if (drv["DataTypeSysName"].ToString() == "Byte[]") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte[])); }
                else if (drv["DataTypeSysName"].ToString() == "Object") { columns.Add(drv["ColumnName"].ToString(), typeof(Object)); }
                else { columns.Add(drv["ColumnName"].ToString(), typeof(String)); }
            }
            return dt;
        }


        private void CheckAuthentication(bool pageLoad)
        {
            CheckAuthentication(pageLoad, true);
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
            CheckAuthentication(true);
        }
        #endregion
    }
}