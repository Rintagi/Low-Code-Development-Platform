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
using System.IO;
using System.Linq;

// Used by data\home\demo\Guarded\web.config and BatchRptSetup:
namespace RO.Web
{
    public partial class UpLoadModule : RO.Web.ModuleBase
    {
        private byte sid;

        public class FileUploadObj
        {
            public string fileName;
            public string mimeType;
            public Int64 lastModified;
            public string base64;
        }

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
            if (Request.Files.Count > 0 && Request.Files[0].FileName != string.Empty
                //&& "image/gif,image/jpeg,image/png,image/tiff,image/pjpeg,image/x-png".IndexOf(Request.Files[0].ContentType) >= 0
                )
            {
                byte[] dc;
                using (System.IO.BinaryReader sm = new BinaryReader(Request.Files[0].InputStream))
                {
                    dc = new byte[Request.Files[0].InputStream.Length];
                    sm.Read(dc, 0, dc.Length); sm.Close();
                }

                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                FileUploadObj fileObj = new FileUploadObj()
                {
                    fileName = Request.Files[0].FileName,
                    mimeType = Request.Files[0].ContentType,
                    lastModified = 0,
                    base64 = Convert.ToBase64String(dc),
                };

                string docJson = jss.Serialize(fileObj);
                string docId = Request.QueryString["key"];
                string tableName = Request.QueryString["tbl"];
                string keyColumnName = Request.QueryString["knm"];
                string columnName = Request.QueryString["col"].ToString();

                byte[] savedContent = AddDoc(docJson, docId, tableName, keyColumnName, columnName, dbConnectionString, base.AppPwd(sid), Request.QueryString["hgt"], true);

                //byte[] dc;
                //System.Drawing.Image oBMP = System.Drawing.Image.FromStream(Request.Files[0].InputStream);
                //int nHeight = int.Parse(oBMP.Height.ToString());
                //int nWidth = int.Parse(oBMP.Width.ToString());
                //if (!string.IsNullOrEmpty(Request.QueryString["hgt"].ToString()))
                //{
                //    nHeight = int.Parse(Request.QueryString["hgt"].ToString());
                //    nWidth = int.Parse((Math.Round(decimal.Parse(oBMP.Width.ToString()) * (nHeight / decimal.Parse(oBMP.Height.ToString())))).ToString());
                //}
                //else if (!string.IsNullOrEmpty(Request.QueryString["wth"].ToString()))
                //{
                //    nWidth = int.Parse(Request.QueryString["wth"].ToString());
                //    nHeight = int.Parse((Math.Round(decimal.Parse(oBMP.Height.ToString()) * (nWidth / decimal.Parse(oBMP.Width.ToString())))).ToString());
                //}

                //Bitmap nBMP = new Bitmap(oBMP, nWidth, nHeight);
                //using (System.IO.MemoryStream sm = new System.IO.MemoryStream())
                //{
                //    nBMP.Save(sm, System.Drawing.Imaging.ImageFormat.Jpeg);
                //    sm.Position = 0;
                //    dc = new byte[sm.Length + 1];
                //    sm.Read(dc, 0, dc.Length); sm.Close();
                //}
                //oBMP.Dispose(); nBMP.Dispose();
                //new AdminSystem().UpdDbImg(Request.QueryString["key"], Request.QueryString["tbl"], Request.QueryString["knm"], Request.QueryString["col"].ToString(), dc, dbConnectionString, base.AppPwd(sid));
                Response.Buffer = true; Response.ClearHeaders(); Response.ClearContent();
                //string imgEmbedded = "data:application/base64;base64," + Convert.ToBase64String(dc);

                string imgEmbedded = savedContent != null ? (RO.Common3.Utils.BlobPlaceHolder(savedContent,true) ?? "").Replace("../", "") : "images/DefaultImg.png";
                string json = "{\"imgUrl\":\"" + imgEmbedded + "\"}";
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(json);
                Response.End();
            }
            else if (Request.QueryString["del"] != null) {
                //delete image
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

                    /* To be Deleted:
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
                    */
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


        protected byte[] AddDoc(string docJson, string docId, string tableName, string keyColumnName, string columnName, string dbConnectionString, string dbPwd, string height, bool resizeToIcon = false)
        {
            byte[] storedContent = null;
            bool dummyImage = false;
            int maxHeght = 360;
            int.TryParse(height, out maxHeght);
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                FileUploadObj fileObj = jss.Deserialize<FileUploadObj>(docJson);
                if (!string.IsNullOrEmpty(fileObj.base64))
                {
                    byte[] content = Convert.FromBase64String(fileObj.base64);
                    dummyImage = fileObj.base64 == "iVBORw0KGgoAAAANSUhEUgAAAhwAAAABCAQAAAA/IL+bAAAAFElEQVR42mN89p9hFIyCUTAKSAIABgMB58aXfLgAAAAASUVORK5CYII=";
                    if (resizeToIcon && fileObj.base64.Length > 0 && fileObj.mimeType.StartsWith("image/"))
                    {
                        try
                        {
                            content = ResizeImage(Convert.FromBase64String(fileObj.base64), maxHeght);
                        }
                        catch
                        {
                        }
                    }
                    /* store as 256 byte UTF8 json header + actual binary file content 
                     * if header info > 256 bytes use compact header(256 bytes) + actual header + actual binary file content
                     */
                    string contentHeader = jss.Serialize(new FileInStreamObj() { fileName = fileObj.fileName, lastModified = fileObj.lastModified, mimeType = fileObj.mimeType, ver = "0100", extensionSize = 0 });
                    byte[] streamHeader = Enumerable.Repeat((byte)0x20, 256).ToArray();
                    int headerLength = System.Text.UTF8Encoding.UTF8.GetBytes(contentHeader).Length;
                    string compactHeader = jss.Serialize(new FileInStreamObj() { fileName = "", lastModified = fileObj.lastModified, mimeType = fileObj.mimeType, ver = "0100", extensionSize = headerLength });
                    int compactHeaderLength = System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader).Length;
                    if (headerLength <= 256)
                        Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(contentHeader), streamHeader, headerLength);
                    else
                    {
                        Array.Resize(ref streamHeader, 256 + contentHeader.Length);
                        Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader), streamHeader, compactHeaderLength);
                        Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader), 0, streamHeader, 256, headerLength);
                    }
                    if (content.Length == 0 || dummyImage)
                    {
                        storedContent = null;
                    }
                    else if (fileObj.mimeType.StartsWith("image/") && false)
                    {
                        // backward compatability with asp.net side, only store image and not fileinfo
                        storedContent = content;
                    }
                    else
                    {
                        storedContent = new byte[content.Length + streamHeader.Length];
                        Array.Copy(streamHeader, storedContent, streamHeader.Length);
                        Array.Copy(content, 0, storedContent, streamHeader.Length, content.Length);
                    }
                    byte[] savedContent = content.Length == 0 || dummyImage ? null : storedContent;
                    new AdminSystem().UpdDbImg(docId, tableName, keyColumnName, columnName, savedContent, dbConnectionString, dbPwd);
                    return savedContent;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw new Exception("invalid attachment format: " + Server.HtmlEncode(ex.Message)); }
        }

        protected byte[] ResizeImage(byte[] image, int maxHeight = 360)
        {

            byte[] dc;

            System.Drawing.Image oBMP = null;

            using (var ms = new MemoryStream(image))
            {
                oBMP = System.Drawing.Image.FromStream(ms);
                ms.Close();
            }

            UInt16 orientCode = 1;

            try
            {
                using (var ms2 = new MemoryStream(image))
                {
                    var r = new ExifLib.ExifReader(ms2);
                    r.GetTagValue(ExifLib.ExifTags.Orientation, out orientCode);
                }
            }
            catch { }

            int nHeight = maxHeight; // This is 36x10 line:7700 GenScreen
            int nWidth = int.Parse((Math.Round(decimal.Parse(oBMP.Width.ToString()) * (nHeight / decimal.Parse(oBMP.Height.ToString())))).ToString());

            var nBMP = new System.Drawing.Bitmap(oBMP, nWidth, nHeight);
            using (System.IO.MemoryStream sm = new System.IO.MemoryStream())
            {
                // 1 = do nothing
                if (orientCode == 3)
                {
                    // rotate 180
                    nBMP.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                }
                else if (orientCode == 6)
                {
                    //rotate 90
                    nBMP.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                }
                else if (orientCode == 8)
                {
                    // same as -90
                    nBMP.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                }
                nBMP.Save(sm, System.Drawing.Imaging.ImageFormat.Jpeg);
                sm.Position = 0;
                dc = new byte[sm.Length + 1];
                sm.Read(dc, 0, dc.Length); sm.Close();
            }
            oBMP.Dispose(); nBMP.Dispose();

            return dc;
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
                context.Response.Write(str_image);
            }
            catch {}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}