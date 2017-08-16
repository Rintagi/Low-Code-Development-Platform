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
//new change
using System.IO;
//end new change

// Used by data\home\demo\Guarded\web.config and BatchRptSetup:
namespace RO.Web
{
    public partial class UpLoadModule : RO.Web.ModuleBase
    {
        private byte sid;

        public UpLoadModule()
        {
            this.Init += new System.EventHandler(Page_Init);
        }

        private string GetMimeTypeFromExtension(string extension)
        {
            //using (DirectoryEntry mimeMap = new DirectoryEntry("IIS://Localhost/MimeMap"))
            //{
            //    PropertyValueCollection propValues = mimeMap.Properties["MimeMap"];
            //    foreach (object value in propValues)
            //    {
            //        IISOle.IISMimeType mimeType = (IISOle.IISMimeType)value;
            //        if (extension == mimeType.Extension)
            //        {
            //            return mimeType.MimeType;
            //        }
            //    }
            //}
            return "application/octet-stream";
        }

        private void SaveUpload(string dbConnectionString, byte sid)
        {
            if (Request.Files.Count > 0 && Request.Files[0].FileName != string.Empty && "image/gif,image/jpeg,image/png,image/tiff,image/pjpeg,image/x-png".IndexOf(Request.Files[0].ContentType) >= 0)
            {
                byte[] dc;
                System.Drawing.Image oBMP = System.Drawing.Image.FromStream(Request.Files[0].InputStream);
                int nHeight = 150;
                if (Request.QueryString["hgt"] != null && !string.IsNullOrEmpty(Request.QueryString["hgt"].ToString()))
                {
                    nHeight = int.Parse(Request.QueryString["hgt"].ToString());
                }
                int nWidth = int.Parse((Math.Round(decimal.Parse(oBMP.Width.ToString()) * (nHeight / decimal.Parse(oBMP.Height.ToString())))).ToString());
               
                Bitmap nBMP = new Bitmap(oBMP, nWidth, nHeight);
                using (System.IO.MemoryStream sm = new System.IO.MemoryStream())
                {
                    nBMP.Save(sm, System.Drawing.Imaging.ImageFormat.Jpeg);
                    sm.Position = 0;
                    dc = new byte[sm.Length + 1];
                    sm.Read(dc, 0, dc.Length); sm.Close();
                }
                oBMP.Dispose(); nBMP.Dispose();
                new AdminSystem().UpdDbImg(Request.QueryString["key"], Request.QueryString["tbl"], Request.QueryString["knm"], Request.QueryString["col"].ToString(), dc, dbConnectionString, base.AppPwd(sid));
                Response.Buffer = true; Response.ClearHeaders(); Response.ClearContent();
                string imgEmbedded = "data:application/base64;base64," + Convert.ToBase64String(dc);
                string json = "{\"imgUrl\":\"" + imgEmbedded + "\"}";
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(json);
                Response.End();
            }
            else if (Request.QueryString["del"] != null)
            { //delete image
                new AdminSystem().UpdDbImg(Request.QueryString["key"], Request.QueryString["tbl"], Request.QueryString["knm"], Request.QueryString["col"].ToString(), null, dbConnectionString, base.AppPwd(sid));
                Response.Buffer = true; Response.ClearHeaders(); Response.ClearContent();
                string json = "{\"imgUrl\":\"" + "images/DefaultImg.png" + "\"}";
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(json);
                Response.End();
            }

        }

        private void SaveMultiDocUpload(string dbConnectionString, string dbPwd, byte sid, bool overwrite)
        {
            if (Request.Files.Count > 0 && Request.Files[0].FileName != string.Empty)
            {
                byte[] dc;
                using (System.IO.BinaryReader sm = new BinaryReader(Request.Files[0].InputStream))
                {
                    dc = new byte[Request.Files[0].InputStream.Length];
                    sm.Read(dc, 0, dc.Length); sm.Close();
                }
                // In case DocId has not been saved properly, always find the most recent to replace as long as it has the same file name:
                string DocId = string.Empty;
                DocId = new AdminSystem().GetDocId(Request.QueryString["key"], Request.QueryString["tbl"], Path.GetFileName(Request.Files[0].FileName), base.LUser.UsrId.ToString(), dbConnectionString, dbPwd);
                if (DocId == string.Empty || !overwrite)
                {
                    DocId = new AdminSystem().AddDbDoc(Request.QueryString["key"], Request.QueryString["tbl"], Path.GetFileName(Request.Files[0].FileName), Request.Files[0].ContentType, dc.Length, dc, dbConnectionString, dbPwd, base.LUser);
                }
                else
                {
                    new AdminSystem().UpdDbDoc(DocId, Request.QueryString["tbl"], Path.GetFileName(Request.Files[0].FileName), Request.Files[0].ContentType, dc.Length, dc, dbConnectionString, dbPwd, base.LUser);
                }

                Response.Buffer = true; Response.ClearHeaders(); Response.ClearContent();
                string json = "{\"newDocId\":\"" + DocId + "\"}";
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(json);
                Response.End();
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {

                StringBuilder sb = new StringBuilder();
                if (Request.QueryString["tbl"] != null && Request.QueryString["key"] != null)
                {
                    /* prevent manually constructed request that would lead to information leakage via hashing of 
                     * query string and session secret, only apply for database related retrieval which are all generated by the system
                     */
                    ValidatedQS();
                    string dbConnectionString;
                    if (Request.QueryString["sys"] != null)
                    {
                        sid = byte.Parse(Request.QueryString["sys"].ToString());
                    }
                    else
                    {
                        throw new Exception("Please make sure '&sys=' is present and try again.");
                    }
                    if (new AdminSystem().IsMDesignDb(Request.QueryString["tbl"].ToString()))
                    {
                        dbConnectionString = base.SysConnectStr(sid);
                    }
                    else
                    {
                        dbConnectionString = base.AppConnectStr(sid);
                    }

                    if (Request.QueryString["multi"] != null)
                        SaveMultiDocUpload(dbConnectionString, base.AppPwd(sid), sid, true);
                    else
                        SaveUpload(dbConnectionString, sid);
                    return;

                    DataTable dt = null;
                    try
                    {
                        if (Request.QueryString["knm"] != null && Request.QueryString["col"] != null)     // ImageButton
                        {
                            dt = (new AdminSystem()).GetDbImg(Request.QueryString["key"].ToString(), Request.QueryString["tbl"].ToString(), Request.QueryString["knm"].ToString(), Request.QueryString["col"].ToString(), dbConnectionString, base.AppPwd(sid));
                            Response.Buffer = true; Response.ClearHeaders(); Response.ClearContent();
                            Response.ContentType = "image/jpeg";
                            Response.AppendHeader("Content-Disposition", "Attachment; Filename=");
                            Response.BinaryWrite((byte[])dt.Rows[0][0]);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                        else // Document.
                        {
                            dt = (new AdminSystem()).GetDbDoc(Request.QueryString["key"].ToString(), Request.QueryString["tbl"].ToString(), dbConnectionString, base.AppPwd(sid));
                            Response.Buffer = true; Response.ClearHeaders(); Response.ClearContent();
                            Response.ContentType = dt.Rows[0]["MimeType"].ToString();
                            Response.AppendHeader("Content-Disposition", "Attachment; Filename=" + dt.Rows[0]["DocName"].ToString());
                            Response.BinaryWrite((byte[])dt.Rows[0]["DocImage"]);
                            Response.End();
                        }
                    }
                    catch (Exception err) { ApplicationAssert.CheckCondition(false, "DnLoadModule", "", err.Message); }
                }
                else if (Request.QueryString["file"] != null)
                {
                    /* file based download needs to be catered manually by webrule for protected contents
                     * via access control in the IIS directory level(no access) and gated by dnloadmodule via server side transfer
                     */
                    try
                    {
                        bool pub = true;
                        if (LImpr != null)
                        {
                            string UsrGroup = (char)191 + base.LImpr.UsrGroups + (char)191;
                            if (UsrGroup.IndexOf((char)191 + "25" + (char)191) < 0 && UsrGroup.IndexOf((char)191 + "5" + (char)191) < 0)
                                pub = true;
                            else
                                pub = false;
                        }
                        string fileName = Request.QueryString["file"].ToString();
                        string key = Request.QueryString["key"].ToString();
                        string DownloadLinkCode = Session.SessionID;
                        byte[] Download_code = System.Text.Encoding.ASCII.GetBytes(DownloadLinkCode);
                        System.Security.Cryptography.HMACMD5 bkup_hmac = new System.Security.Cryptography.HMACMD5(Download_code);
                        byte[] Download_hash = bkup_hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(fileName));
                        string Download_hashString = BitConverter.ToString(Download_hash);
                        bool allowDownload = Download_hashString == key;
                        fileName = fileName.ToLower().Replace("/guarded/", "/source/");
                        string url = fileName;
                        string fullfileName = Server.MapPath(fileName);   // we enforce everything file for download is under ../files
                        System.IO.FileInfo file = new System.IO.FileInfo(fullfileName);
                        string oname = file.Name;
                        if (!allowDownload && pub && !(file.Name.StartsWith("Pub") || file.Name.StartsWith("pub")))
                        {
                            if (file.Name.EndsWith(".wmv"))
                            {
                                file = new FileInfo(file.DirectoryName + "/PubMsg.wmv");
                                url = fileName.Replace(oname, "PubMsg.wmv");
                            }
                            else
                            {
                                if (LUser == null || LUser.LoginName == "Anonymous")
                                {
                                    string loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;
                                    if (string.IsNullOrEmpty(loginUrl)) { loginUrl = "MyAccount.aspx"; }
                                    Response.Redirect(loginUrl + (loginUrl.IndexOf('?') > 0 ? "&" : "?") + "wrn=1&ReturnUrl=" + Server.UrlEncode(Request.Url.PathAndQuery));
                                }
                                else
                                {
                                    throw new Exception("Access Denied");
                                }
                            }
                        }
                        Response.Buffer = true; Response.ClearHeaders(); Response.ClearContent();
                        Response.ContentType = GetMimeTypeFromExtension(file.Extension);
                        Response.AddHeader("Content-Disposition", "Attachment; Filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Server.Transfer(url);
                    }
                    catch (Exception err) { ApplicationAssert.CheckCondition(false, "DnLoadModule", "", err.Message); }
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

    //new change
    public class fileUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
                        string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                        file.SaveAs(pathToSave_100);
                    }
                }
                //  database record update logic here  ()

                context.Response.Write(str_image);
            }
            catch (Exception ac)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
    //end new change
}