namespace RO.Common3
{
    using System;
    using System.IO;
    using System.Text;
    using System.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Runtime.CompilerServices;

    public static class XmlUtils
    {
		// Convert to xml from objects:
		public static string ObjectToXml(this object o)
        {
            XmlSerializer s = new XmlSerializer(o.GetType());
            System.IO.StringWriter sw = new System.IO.StringWriter();
            XmlWriter xw = XmlWriter.Create(sw);
            s.Serialize(xw, o);
            string x = sw.ToString();
            return x;
        }

        public static string DataSetToXml(this DataSet ds)
        {
            //return ds.GetXml();	// Do not use this construct because null columns are not handled.
			System.IO.StringWriter sw = new System.IO.StringWriter();
			XmlWriter xw = XmlWriter.Create(sw);
			ds.WriteXml(xw, XmlWriteMode.WriteSchema);
			return sw.ToString();
		}

		public static string DataTableToXml(this DataTable dt)
		{
			DataSet ds = new DataSet();
			ds.Tables.Add(dt);
			return DataSetToXml(ds);
		}

		public static string DataViewToXml(this DataView dv)
		{
			return DataTableToXml(dv.ToTable());
		}

		// Convert to objects from xml:
		public static TT XmlToObject<TT>(this string xml)
		{
			XmlSerializer s = new XmlSerializer(typeof(TT));
			XmlReader xs = XmlReader.Create(new System.IO.StringReader(xml));

			TT o = (TT)s.Deserialize(xs);
			return o;
		}

        public static DataSet XmlToDataSet(this string xml)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(new System.IO.StringReader(xml));
            if (ds.Tables.Count == 0)
            {
                // create an empty table of it is an empty set
                DataTable dt = new DataTable();
                ds.Tables.Add(dt);
            }
			ds.AcceptChanges();
            return ds;
        }

		public static DataTable XmlToDataTable(this string xml)
		{
			return XmlToDataSet(xml).Tables[0];
		}

		public static DataView XmlToDataView(this string xml)
		{
			return new DataView(XmlToDataTable(xml));
		}

		// The following is only applicable to subclass of DataSet:
		public static TT XmlToDataSet<TT>(this string xml) where TT: DataSet, new()
		{
            TT ds = new TT();
			ds.ReadXml(new System.IO.StringReader(xml));
			if (ds.Tables.Count == 0)
			{
				// create an empty table if it is an empty set
				DataTable dt = new DataTable();
				ds.Tables.Add(dt);
			}
			ds.AcceptChanges();
			return ds;
		}
	}
}
