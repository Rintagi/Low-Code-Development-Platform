using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.WebRules;


namespace RO.Web
{
    public partial class RestfulModule : RO.Web.ModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var x = Page.RouteData;
            // this only works for aspx target
            //Server.Transfer("~/AdmUsr.aspx", true);
            //this only works if incomin is a POST
            //TransferRequest only works for integrated pipeline
            //Server.TransferRequest("~/webservices/AdmusrWs.asmx/Restful", true);
            var postHeader = new System.Collections.Specialized.NameValueCollection(Request.Headers);
            var screen = x.Values["screen"];
            var action = x.Values["action"] as string;
            postHeader.Add("X-METHOD", Request.HttpMethod);
            Server.TransferRequest(string.Format("~/webservices/{0}Ws.asmx/" + (string.IsNullOrEmpty(action) ? "restful" : action),screen)
                , true
                , "POST"
                , postHeader
                , true);
        }
    }
}