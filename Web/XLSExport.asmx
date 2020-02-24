<%@ WebService Language="C#" Class="XLSExport" %>

using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using RO.Common3;
using Microsoft.Win32;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public partial class XLSExport : System.Web.Services.WebService
{
    public XLSExport()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    private void UpdateCell(string range, string value, OleDbConnection conn)
    {
        string[] r = range.Split('!');
        string c = r[1].Replace("$", "");
        string s = r[0];
        string x = s.Replace("\"", "") + "$" + c + ":" + c;
        try
        {
            OleDbCommand cmd = new OleDbCommand(string.Format("UPDATE [{0}] SET F1 = '{1}'", x, value), conn);
            cmd.ExecuteNonQuery();
        }
        catch
        {
            try
            {
                double.Parse(value);
            }
            catch
            {
                value = value == "Y" ? "2" : "1";
            }
            OleDbCommand cmd = new OleDbCommand(string.Format("UPDATE [{0}] SET F1 = {1}", x, value), conn);
            cmd.ExecuteNonQuery();
        }
    }
    [WebMethod]
    public int[] ExportToFile(string src, string[] keyCols, string[] valCols, string workSheet, string fileFullName)
    {
        //RegistryKey regKeyAceWoW = (RegistryKey) Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Office\\12.0\\Access Connectivity Engine\\Engines\\Excel");
        //RegistryKey regKeyJet4WoW = (RegistryKey)Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Jet\\4.0\\Engines\\Excel");
        //RegistryKey regKeyAce = (RegistryKey)Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Office\\12.0\\Access Connectivity Engine\\Engines\\Excel");
        //RegistryKey regKeyJet4 = (RegistryKey)Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Jet\\4.0\\Engines\\Excel");

        //RegistryKey regKey = regKeyJet4;

        //if (regKey != null)
        //{
        //    int guessRow = (int)regKey.GetValue("TypeGuessRows");
        //    if (guessRow != 1)
        //    {
        //        if (regKeyJet4WoW != null) 
        //        {
        //            //throw new Exception("make sure registry SOFTWARE\\Wow6432Node\\Microsoft\\Office\\12.0\\Access Connectivity Engine\\Engines\\Excel\\TypeGuessRows is set to 1 for proper all string columns exporting");
        //            throw new Exception("make sure registry SOFTWARE\\Wow6432Node\\Microsoft\\Jet\\4.0\\Engines\\Excel\\TypeGuessRows is set to 1 for proper all string columns exporting");
        //        } 
        //        else 
        //        {
        //            //throw new Exception("make sure registry SOFTWARE\\Microsoft\\Office\\12.0\\Access Connectivity Engine\\Engines\\Excel\\TypeGuessRows is set to 1 for proper all string columns exporting");
        //            throw new Exception("make sure registry SOFTWARE\\Microsoft\\Jet\\4.0\\Engines\\Excel\\Access Connectivity Engine\\Engines\\Excel\\TypeGuessRows is set to 1 for proper all string columns exporting");
        //        }
        //    }
        //}
        DataSet dsSrc = src.XmlToDataSet();
        OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataSet dsXls = new DataSet();
        Dictionary<string, string> colMap = new Dictionary<string, string>();
        Dictionary<string, string> revColMap = new Dictionary<string, string>();
        int aCnt = 0, uCnt = 0;
        try
        {
            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileFullName + ";Extended Properties=\"Excel 12.0; HDR=NO; \"";
            //conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileFullName + ";Extended Properties=\"Excel 8.0; HDR=Yes; IMEX=0;\"";
            conn.Open();
            string myQuery = @"SELECT * From [" + workSheet + "$]";
            OleDbCommand myCmd = new OleDbCommand(myQuery, conn);
            da.SelectCommand = myCmd;
            da.Fill(dsXls, workSheet);
            foreach (DataColumn dc in dsXls.Tables[0].Columns)
            {
                colMap[dsXls.Tables[0].Rows[0][dc.ColumnName].ToString()] = dc.ColumnName;
                revColMap[dc.ColumnName] = dsXls.Tables[0].Rows[0][dc.ColumnName].ToString();
                //colMap[dc.ColumnName] = dc.ColumnName;
                //revColMap[dc.ColumnName] = dc.ColumnName;
            }

            List<string> fldList = new List<string>();
            List<string> updList = new List<string>();
            List<string> addList = new List<string>();
            List<string> holderList = new List<string>();
            List<string> whereList = new List<string>();
            List<string> sortSrcList = new List<string>();
            List<string> sortList = new List<string>();
            da.UpdateCommand = new OleDbCommand();
            da.UpdateCommand.Connection = conn;
            da.InsertCommand = new OleDbCommand();
            da.InsertCommand.Connection = conn;
            foreach (string c in valCols)
            {
                if (dsXls.Tables[0].Columns.Contains(colMap[c]) && dsSrc.Tables[0].Columns.Contains(c))
                {
                    updList.Add(string.Format("{0}=?", colMap[c])); fldList.Add(colMap[c]);
                    addList.Add(colMap[c]); holderList.Add("?");
                    da.UpdateCommand.Parameters.Add("@" + colMap[c], OleDbType.VarWChar).SourceColumn = colMap[c];
                    da.InsertCommand.Parameters.Add("@" + colMap[c], OleDbType.VarWChar).SourceColumn = colMap[c];

                }
            }
            foreach (string c in keyCols)
            {

                if (dsXls.Tables[0].Columns.Contains(colMap[c]) && dsSrc.Tables[0].Columns.Contains(c))
                {
                    whereList.Add(string.Format("{0}=?", colMap[c])); sortSrcList.Add(c); sortList.Add(colMap[c]);
                    addList.Add(colMap[c]); holderList.Add("?");
                    da.UpdateCommand.Parameters.Add("@" + colMap[c], OleDbType.VarWChar).SourceColumn = colMap[c];
                    da.InsertCommand.Parameters.Add("@" + colMap[c], OleDbType.VarWChar).SourceColumn = colMap[c];

                }
            }

            if (whereList.Count == 0)
            {
                throw new Exception("there needs to be at least one column that is in common between the target worksheet and the source");
            }
            da.UpdateCommand.CommandText = string.Format("UPDATE [{0}$] SET {1} WHERE {2}", workSheet, string.Join(",", updList.ToArray<string>()), string.Join(" AND ", whereList.ToArray<string>()));
            da.InsertCommand.CommandText = string.Format("INSERT INTO [{0}$] ({1}) VALUES ({2})", workSheet, string.Join(",", addList.ToArray<string>()), string.Join(",", holderList.ToArray<string>()));
            DataView dv = dsXls.Tables[0].DefaultView;
            dv.Sort = string.Join(",", sortList.ToArray<string>());

            DataView dvSrc = dsSrc.Tables[0].DefaultView;
            dvSrc.Sort = string.Join(",", sortSrcList.ToArray<string>());

            foreach (DataRowView dr in dvSrc)
            {
                if (keyCols[0] == "TagName" && valCols[0] == "TagValue" && !dr["TagName"].ToString().StartsWith("[["))
                {
                    UpdateCell(dr["TagName"].ToString(), dr["TagValue"].ToString(), conn);
                }
                else
                {
                    List<string> kList = new List<string>();
                    foreach (string k in sortSrcList)
                    {
                        kList.Add(dr[k].ToString());
                    }
                    DataRowView[] adrv = dv.FindRows(kList.ToArray<string>());
                    if (adrv.Length > 0)
                    {
                        foreach (DataRowView drv in adrv)
                        {
                            foreach (string s in fldList)
                            {
                                drv[s] = dr[revColMap[s]].ToString();
                            }
                            uCnt = uCnt + 1;
                        }
                    }
                    else
                    {
                        DataRow drT = dsXls.Tables[0].NewRow();
                        foreach (string s in addList)
                        {
                            drT[s] = dr[revColMap[s]].ToString();
                        }
                        dsXls.Tables[0].Rows.Add(drT);
                        aCnt = aCnt + 1;
                    }
                }
            }

            da.Update(dsXls, workSheet);

        }
        catch (Exception e) { throw (e); }
        finally { conn.Close(); conn = null; }
        return new int[] { aCnt, uCnt, dsSrc.Tables[0].Rows.Count };
    }

}
