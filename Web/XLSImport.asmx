<%@ WebService Language="C#" Class="XLSImport" %>

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

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public partial class XLSImport : System.Web.Services.WebService
{
    public XLSImport()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<string> GetSheetNames(string fileFullName)
    {
        List<string> names = new List<string>();
        OleDbConnection conn = new OleDbConnection();
        System.Collections.ArrayList al = new System.Collections.ArrayList();
        try
        {
            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileFullName + ";Extended Properties=\"Excel 12.0; HDR=NO; IMEX=1;\"";
            //conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileFullName + ";Extended Properties=\"Excel 8.0; HDR=NO; IMEX=1;\"";
            conn.Open();
            // Get original sheet order:
            DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            DataRow[] drs = dt.Select(dt.Columns[2].ColumnName + " not like '*$Print_Area' AND " + dt.Columns[2].ColumnName + " not like '*$''Print_Area'");
            foreach (DataRow dr in drs) { names.Add(dr["TABLE_NAME"].ToString().Replace("'", string.Empty).Replace("$", string.Empty)); }
        }
        catch (Exception e)
        {
            throw (e);
        }
        finally
        {
            conn.Close(); conn = null;
        }

        return names;
    }

    [WebMethod]
    public string ImportFile(string fileName, string workSheet, string startRow, string fileFullName)
    {
        OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileFullName + ";Extended Properties=\"Excel 12.0; HDR=NO; IMEX=1;\"";
            //conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileFullName + ";Extended Properties=\"Excel 8.0; HDR=NO; IMEX=1;\"";
            conn.Open();
            string myQuery = @"SELECT * From [" + workSheet + "$]";
            OleDbCommand myCmd = new OleDbCommand(myQuery, conn);
            da.SelectCommand = myCmd;
            da.Fill(dt);
        }
        catch (Exception e) { throw (e); }
        finally { conn.Close(); conn = null; }
        dt.TableName = workSheet;
        dt = CleanData(dt);
        return dt.DataTableToXml();
    }

    private DataTable CleanData(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == typeof(string))
                {
                    string r = "[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-\u10FFFF]";
                    dr[dc.ColumnName] = System.Text.RegularExpressions.Regex.Replace(dr[dc.ColumnName].ToString(), r, "", System.Text.RegularExpressions.RegexOptions.Compiled);
                }
            }
        }
        return dt;
    }
}
