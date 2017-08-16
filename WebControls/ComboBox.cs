using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections.Generic;

using RO.SystemFramewk;

namespace RoboCoder.WebControls
{
 	[DefaultProperty("PageNo"), ToolboxData("<{0}:ComboBox runat=server></{0}:ComboBox>")]
	[ValidationProperty("SelectedValue")]
	public class ComboBox : System.Web.UI.WebControls.WebControl, INamingContainer
	{
        
        private Panel comboboxPanel;
        private Panel autocompletePanel;

		private const byte PageSize = 20;
		private TextBox tBox;
		private DropDownList ddList;
		private Label lbPages;
		private Label lbMsg;
		private ImageButton ibWild;
		private ImageButton ibDDFind;
		private ImageButton ibFirst;
		private ImageButton ibPrev;
		private ImageButton ibNext;
		private ImageButton ibLast;

        private TextBox tKeyBox;
        private TextBox tLabelBox;

        public int Count { get { return Items.Count; } }
        public void Select(int idx)
        {
            Items[idx].Selected = true;
            tKeyBox.Text = Items[idx].Value;
            tLabelBox.Text = Items[idx].Text;
        }

        public void DeSelect()
        {
            tKeyBox.Text = "";
            tLabelBox.Text = "";
            ddList.ClearSelection();
            //SelectedItem.Selected = false;
        }

        public bool Selected(int idx)
        {
            return Items[idx].Selected;
        }
        public string Value(int idx)
        {
            return Items[idx].Value;
        }

        public string AutoCompleteUrl
        {
            get { object oo = ViewState["_AutoCompleteUrl"]; return (string)oo; }
            set { ViewState["_AutoCompleteUrl"] = value; }
        }

        public Dictionary<string, string> DataContext
        {
            get { object oo = ViewState["_DataContext"]; return (Dictionary<string, string>)oo; }
            set { ViewState["_DataContext"] = value; }
        }

        public string Mode
        {
            get { object oo = ViewState["_Mode"]; return  oo != null ? oo.ToString() : "C"; }
            set { ViewState["_Mode"] = value; }
        }

		public string FocusID
		{
			get 
            {
                if (Mode == "A") return tLabelBox.ClientID;
                else
                {
                    return tBox.Visible ? tBox.ClientID : ddList.ClientID;
                }
            }
		}

        public string KeyID
        {
            get
            {
                if (Mode == "A") return tKeyBox.ClientID;
                else
                {
                    return tBox.Visible ? tBox.ClientID : ddList.ClientID;
                }
            }
        }

        public string Text
        {
            get { return tKeyBox.Text; }
            set { tKeyBox.Text = value; }
        }

		private int PageNo
		{
			get
			{
				object oo = ViewState["_PageNo"];
				ApplicationAssert.CheckCondition(oo != null, "ComboBox.cs", "PageNo is null", "DataSource may not have been assigned.");
				return (int)oo;
			}
			set {ViewState["_PageNo"] = value;}
		}

		public string FilterText
		{
			get
			{
				object oo = ViewState["_FilterText"];
				if (oo == null) { return string.Empty; } else { return (string)oo; }
			}
			set { ViewState["_FilterText"] = value; }
		}

		private int PageCount
		{
			get
			{
				object oo = ViewState["_PageCount"];
				ApplicationAssert.CheckCondition(oo != null, "ComboBox.cs", "PageCount is null", "DataSource may not have been assigned.");
				return (int)oo;
			}
			set {ViewState["_PageCount"] = value;}
		}

		public void tBox_TextChanged(object sender, System.EventArgs e)
		{
			cbSearch(this, EventArgs.Empty);
		}

        public void tKeyBox_TextChanged(object sender, System.EventArgs e)
        {
            string keyId = ((TextBox)sender).Text.Trim();
            if (ddList.DataSource == null && System.Web.HttpContext.Current.Session[this.ID] != null)
            {
                try
                {
                    ddList.DataSource = new DataView(((DataView)this.DataSource).Table);
                    ddList.DataBind();
                }
                catch { }
            }
            if (ddList.DataSource != null)
            {
                try
                {
                    ddList.SelectedValue = keyId;
                }
                catch
                {
                    tBox.Text = "**" + keyId;
                    if (Search != null) { Search(this, e); } 
                //    if (!string.IsNullOrEmpty(keyId)) ddList.SelectedValue = keyId;
                //    else if (ddList.Items.Count > 0) ddList.SelectedIndex = 0;
                //    else ddList.SelectedIndex = -1;
                //}
                //catch
                //{
                //    if (ddList.Items.Count > 0) ddList.SelectedIndex = 0;
                //    else ddList.SelectedIndex = -1;
                    //try
                    //{
                    //    ((TextBox)sender).Text = ddList.SelectedItem.Value;
                    //}
                    //catch { }
                }
            }
            //if (ddList.DataSource == null)
            //{
            //    ddList.DataSource = new DataView(((DataView) DataSource).Table);
            //    ddList.DataBind();
            //}
            if (SelectedIndexChanged != null) { SelectedIndexChanged(this, e); };

            //cbSearch(this, EventArgs.Empty);
        }

		public void ClearSearch()
		{
			if (ddList.Items.Count > 0)
			{
				GotoPage(1); DeSelect(); SetTbVisible(); tBox.Text = string.Empty; lbMsg.Text = string.Empty;
			}
		}

		public void SetDdVisible()
		{
			if (!ddList.Visible) {ddList.Visible = true; tBox.Visible = false;}
		}

		public void SetTbVisible()
		{
            if (ddList.Visible) { ddList.Visible = false; tBox.Visible = true; tBox.Text = ddList.SelectedItem == null ? string.Empty : ddList.SelectedItem.Text; }
		}

		public event EventHandler SelectedIndexChanged;
		protected void ddList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (SelectedIndexChanged != null) {SelectedIndexChanged(this, e);}
		}

		public event EventHandler PostBack;
		protected void cbPostBack(object sender, System.EventArgs e)
		{
			if (PostBack != null) {PostBack(this, e);}
		}

		public event EventHandler Search;
		
		public event EventHandler TextChanged;
		
		protected void cbSearch(object sender, System.EventArgs e)
		{
			ddList.Visible = true; tBox.Visible = false;	// Need this to enable search.
			if (Search != null) {Search(this, e);} 
			else {ApplyFilter(string.Empty);}
			if (ddList.Items.Count == 0 || (ddList.Items.Count == 1 && ddList.Items[0].Value == string.Empty))
			{
				ddList.Visible = false; tBox.Visible = true; lbMsg.Text = "Invalid";
				if (TextChanged != null) { TextChanged(this, e); }
			}
		}

		public bool SelectByValue(object ID, string ftr, bool bFireEvent)
		{
			DataView dv = (DataView)DataSource;
			dv.RowFilter = ftr;
			this.FilterText = ftr;
			DataSource = dv;
			if (dv.Count > 0)
			{
				if (ID == DBNull.Value) {SelectItem(0,bFireEvent);}
				else if (ID.ToString() == string.Empty) {SelectItem(0,bFireEvent); if (tBox.Visible) {tBox.Text = string.Empty; lbMsg.Text = string.Empty;}; return true;}
				else
				{
					// This linear search is preferable because sorting is required for binary search.
					for (int row = 0; row < dv.Count; row++)
					{
						if (dv[row][this.DataValueField].ToString().Equals(ID.ToString())) 
						{
							GotoPage(((row + 1) + (PageSize - 1)) / PageSize);
							ListItem li = ddList.Items.FindByValue(ID.ToString());
							if (li != null)
							{
                                DeSelect();
                                Select(row % PageSize);
								if (tBox.Visible) {tBox.Text = dv[row][this.DataTextField].ToString();}
								if (bFireEvent) {ddList_SelectedIndexChanged(this, EventArgs.Empty);}
							}
							return true;
						}
					}
				}
			}
			return false;
		}

        public override string ToolTip
        {
            get
            {
                return base.ToolTip;
            }
            set
            {
                base.ToolTip = value;
                tKeyBox.ToolTip = value;
                tLabelBox.ToolTip = value;
                ddList.ToolTip = value;
                tBox.ToolTip = value;
            }
        }
		public override System.Web.UI.WebControls.Unit Width
		{
			get {EnsureChildControls(); return tBox.Width;}
            set { EnsureChildControls(); ddList.Width = value; tBox.Width = System.Web.UI.WebControls.Unit.Pixel((int)ddList.Width.Value); tLabelBox.Width = tBox.Width;}
		}
        public System.Web.UI.WebControls.Unit MaxWidth
        {
            get { EnsureChildControls(); return  new Unit(tBox.Style["max-width"]) ; }
            set { EnsureChildControls(); ddList.Style["max-width"] = value.ToString(); tBox.Style["max-width"] = value.ToString(); tLabelBox.Style["max-width"] = value.ToString(); }
        }

		public int MaxLength
		{
			get {EnsureChildControls(); return tBox.MaxLength;}
			set {EnsureChildControls(); tBox.MaxLength = value;}
		}

		public string SearchText
		{
			get {EnsureChildControls(); return tBox.Text;}
		}

		public override bool Enabled
		{
			get {EnsureChildControls(); return ddList.Enabled;}
            set { EnsureChildControls(); ddList.Enabled = value; tKeyBox.Enabled = value; tLabelBox.Enabled = value; }
		}

		public override System.Drawing.Color BackColor
		{
            get { EnsureChildControls(); return ddList.BackColor; }
            set { EnsureChildControls(); ddList.BackColor = value; tKeyBox.BackColor = value; tLabelBox.BackColor = value; }
        }

		public bool IsDdlVisible
		{
			get {EnsureChildControls(); return ddList.Visible;}
		}

		public string DataValueField
		{
			get {EnsureChildControls(); return ddList.DataValueField;}
			set {EnsureChildControls(); ddList.DataValueField = value;}
		}

		public string DataTextField
		{
			get {EnsureChildControls(); return ddList.DataTextField;}
			set {EnsureChildControls(); ddList.DataTextField = value;}
		}

		public bool AutoPostBack
		{
			get {EnsureChildControls(); return ddList.AutoPostBack;}
			set {EnsureChildControls(); 
                ddList.AutoPostBack = value; 
                tKeyBox.AutoPostBack = value;
                if (AutoPostBack)
                {
                    tKeyBox.Attributes["onchange"] = "javascript:return CanPostBack(true, this);"; tKeyBox.Attributes["NeedConfirm"] = "Y";
                }
                else
                {
                    tKeyBox.Attributes["onchange"] = null;
                }
            }
		}
		
		public int SelectedIndex
		{
			get {EnsureChildControls(); return ddList.SelectedIndex;}
			set {EnsureChildControls(); ddList.SelectedIndex = value;}
		}

		public int DataSetIndex
		{
			get
			{
				EnsureChildControls(); 
				DataView dv = (DataView)DataSource;
				int iNullRow = 0;
				if (PageNo == 0 && dv[0][this.DataValueField] == DBNull.Value) {iNullRow = 1;}
				int offset = (PageNo - 1) * PageSize + iNullRow;
				return offset + ddList.SelectedIndex;
			}
		}

		public string SelectedValue
		{
			get { EnsureChildControls(); if (Mode == "A") return tKeyBox.Text; else return ddList.SelectedValue; }
		}
        public string SelectedText
        {
            get { EnsureChildControls(); if (Mode == "A") return tLabelBox.Text; else return ddList.SelectedItem.Text; }
        }		
		public ListItem SelectedItem
		{
			get {EnsureChildControls(); return ddList.SelectedItem;}
		}
		
		public ListItemCollection Items
		{
			get {EnsureChildControls(); return ddList.Items;}
		}

		public object DataSource
		{
			get
			{
				ApplicationAssert.CheckCondition(System.Web.HttpContext.Current.Session[this.ID] != null, "ComboBox.cs", "DataSource is null", "DataSource session has not been assigned.");
				DataView dv = ((DataTable)System.Web.HttpContext.Current.Session[this.ID]).DefaultView;
				dv.RowFilter = this.FilterText;
				return dv;
			}
			set
			{
				EnsureChildControls();
				DataView dv = ((DataView)value);
				System.Web.HttpContext.Current.Session[this.ID] = dv.Table;
				PageCount = (dv.Count + PageSize - 1) / PageSize;
				GotoPage(1);
                if (dv.Count > 0)
                {
                    tKeyBox.Text = dv[0][ddList.DataValueField].ToString();
                    tLabelBox.Text = dv[0][ddList.DataTextField].ToString();
                }
            }
		}

		private string GetFilterStr(DataView dv, string ftr)
		{
			string sExpression = ftr;
			if (dv != null)
			{
				string ss = tBox.Text.Trim();
				if (ss != "")
				{
					if (sExpression != string.Empty) {sExpression = sExpression + " AND ";}
					if (dv.Table.Columns.Contains("NoPunc"))
					{
						ss = ss.Replace("^","").Replace("(","").Replace(")","").Replace("_","").Replace("-","").Replace("+","").Replace("=","").Replace("{","").Replace("}","").Replace("[","").Replace("]","").Replace("|","").Replace("\\","").Replace(":","").Replace(";","").Replace("\"","").Replace("'","").Replace("<","").Replace(">","").Replace(",","").Replace(".","").Replace("/","").Replace("$","").Replace("&","").Replace("%","").Replace(" ","").Replace("~","").Replace("`","").Replace("!","").Replace("@","").Replace("#","");
						sExpression = sExpression + "(" + this.DataValueField + " is null OR NoPunc like '" + ss + "*')";
					}
					else
					{
						ss = ss.Replace("'", "''").Replace("[", "[[]");
						sExpression = sExpression + "(" + this.DataValueField + " is null OR " + this.DataTextField + " like '" + ss + "*')";
					}
				}
			}
			return sExpression;
		}

		public void ApplyFilter(string ftr)
		{
			DataView dv = ((DataView)DataSource);
			dv.RowFilter = GetFilterStr(dv,ftr);
			this.FilterText = dv.RowFilter;
			DataSource = dv;
			SelectByText();
		}

		public void SelectByText()
		{
			DataView dv = ((DataView)DataSource);
			if (dv.Count > 0)
			{
				int iNullRow = 0;
				if (dv[0][this.DataValueField] == DBNull.Value) {iNullRow = 1;}
				if (dv.Count - iNullRow == 1) {SelectItem(iNullRow,true);} 
				else {SelectItem(0,true);}
			}
			else {SelectItem(0,true);}
		}

		protected void ibWild_Click(object source, ImageClickEventArgs e)
		{
			cbSearch(this, EventArgs.Empty);
		}

		public event EventHandler DDFindClick;

		protected void ibDDFind_Click(object source, ImageClickEventArgs e)
		{
			ddList.Visible = false; tBox.Visible = true;
			lbMsg.Text = string.Empty;
			cbPostBack(this, EventArgs.Empty);
			if (DDFindClick != null) DDFindClick(this, e);
		}

		private DataView GetCurrPage(DataView dv)
		{
			if (dv == null) {return null;}
			DataTable dt = dv.Table.Clone();
			int iNullRow = 0;
			if (PageNo == 0 && dv[0][this.DataValueField] == DBNull.Value)
			{
				iNullRow = 1;
				dt.Rows.Add(dv[0].Row.ItemArray);
			}
			int offset = (PageNo - 1) * PageSize + iNullRow;
			for (int row = 0; row < PageSize && row + offset < dv.Count; row++)
			{
				dt.Rows.Add(dv[row + offset].Row.ItemArray);
			}
			return new DataView(dt);
		}

		protected void GotoPage(int pg)
		{
			DataView dv = (DataView)DataSource;
			if (PageCount > 0)
			{
				PageNo = pg;
				ddList.DataSource = GetCurrPage(dv);
			}
			else
			{
				PageNo = 0;
				ddList.DataSource = dv;
			}
            try { ddList.DataBind(); }
            catch { }
			lbPages.Text = PageNo.ToString() + " of " + PageCount.ToString();
			cbPostBack(this, EventArgs.Empty);
		}

		protected void SelectItem(int row, bool bFireEvent)
		{
			if (ddList.Items.Count > row)
			{
                DeSelect();
                Select(row);
                //ddList.Items[row].Selected = true;
                //tKeyBox.Text = ddList.Items[row].Text;
                //tLabelBox.Text = ddList.Items[row].Value;
				if (bFireEvent) {ddList_SelectedIndexChanged(this, EventArgs.Empty);}
			}
		}

		protected void ibFirst_Click(object source, ImageClickEventArgs e)
		{
			GotoPage(1); SelectItem(0,true);
		}

		protected void ibPrev_Click(object source, ImageClickEventArgs e)
		{
			if (PageNo > 1) {PageNo --; GotoPage(PageNo); SelectItem(0,true);}
		}

		protected void ibNext_Click(object source, ImageClickEventArgs e)
		{
			if (PageNo < PageCount) {PageNo ++;	GotoPage(PageNo); SelectItem(0,true);}
		}

		protected void ibLast_Click(object source, ImageClickEventArgs e)
		{
			GotoPage(PageCount); SelectItem(0,true);
		}

		private void SetControls()
		{
            tBox.Visible = true;
            ddList.Visible = false;
            ddList.CssClass = this.CssClass;
            tBox.CssClass = this.CssClass;
            tBox.Height = Unit.Pixel(15);
            tBox.AutoPostBack = true;

            lbPages.Text = "";
            lbPages.Font.Name = "Arial";
            lbPages.Font.Size = FontUnit.Point(6);
            lbPages.Height = Unit.Pixel(8);

            lbMsg.Text = "";
            lbMsg.Font.Name = "Arial";
            lbMsg.Font.Size = FontUnit.Point(7);
            lbMsg.Height = Unit.Pixel(12);

            ibWild.ToolTip = "Click to search for the text entered (begins with a * for wild search)";
            ibDDFind.ToolTip = "Click to enable search text to be entered";

            ibWild.ImageUrl = "../images/custom-dropdown-button.png";
            ibDDFind.ImageUrl = "../images/custom-dropdown-button-cancel.png";
            ibFirst.ImageUrl = "../Images/First.gif";
            ibPrev.ImageUrl = "../Images/Prev.gif";
            ibNext.ImageUrl = "../Images/Next.gif";
            ibLast.ImageUrl = "../Images/Last.gif";

            ibWild.Width = Unit.Pixel(16);
            ibWild.Height = Unit.Pixel(18);
            ibDDFind.Width = Unit.Pixel(16);
            ibDDFind.Height = Unit.Pixel(18);

            ibFirst.Width = Unit.Pixel(12);
            ibFirst.Height = Unit.Pixel(12);
            ibPrev.Width = Unit.Pixel(12);
            ibPrev.Height = Unit.Pixel(12);
            ibNext.Width = Unit.Pixel(12);
            ibNext.Height = Unit.Pixel(12);
            ibLast.Width = Unit.Pixel(12);
            ibLast.Height = Unit.Pixel(12);

            ibFirst.ToolTip = "Click to go to the first page";
            ibPrev.ToolTip = "Click to go to the previous page";
            ibNext.ToolTip = "Click to go to the next page";
            ibLast.ToolTip = "Click to go to the last page";

            tBox.TextChanged += new EventHandler(tBox_TextChanged);
            ddList.SelectedIndexChanged += new EventHandler(ddList_SelectedIndexChanged);
            ibWild.Click += new ImageClickEventHandler(ibWild_Click);
            ibDDFind.Click += new ImageClickEventHandler(ibDDFind_Click);
            ibFirst.Click += new ImageClickEventHandler(ibFirst_Click);
            ibPrev.Click += new ImageClickEventHandler(ibPrev_Click);
            ibNext.Click += new ImageClickEventHandler(ibNext_Click);
            ibLast.Click += new ImageClickEventHandler(ibLast_Click);

            tKeyBox.Visible = true; tKeyBox.ToolTip = base.ToolTip; tKeyBox.Attributes["style"] = "display:none;";
            tKeyBox.TextChanged += new EventHandler(tKeyBox_TextChanged);
            tLabelBox.Visible = true; tLabelBox.ToolTip = base.ToolTip;
		}

		protected override void CreateChildControls()
		{
			Controls.Clear();
            comboboxPanel = new Panel();
            autocompletePanel = new Panel();
            Controls.Add(comboboxPanel);
            Controls.Add(autocompletePanel);

            tBox = new TextBox();
            ddList = new DropDownList();
            lbPages = new Label();
            lbMsg = new Label();
            ibWild = new ImageButton();
            ibDDFind = new ImageButton();
            ibFirst = new ImageButton();
            ibPrev = new ImageButton();
            ibNext = new ImageButton();
            ibLast = new ImageButton();
            comboboxPanel.Controls.Clear();
            comboboxPanel.Controls.Add(tBox);
            comboboxPanel.Controls.Add(ddList);
            comboboxPanel.Controls.Add(lbPages);
            comboboxPanel.Controls.Add(lbMsg);
            comboboxPanel.Controls.Add(ibWild);
            comboboxPanel.Controls.Add(ibDDFind);
            comboboxPanel.Controls.Add(ibFirst);
            comboboxPanel.Controls.Add(ibPrev);
            comboboxPanel.Controls.Add(ibNext);
            comboboxPanel.Controls.Add(ibLast);

            tKeyBox = new TextBox(); tKeyBox.AutoPostBack = AutoPostBack; tKeyBox.ID = "KeyId";
            tLabelBox = new TextBox(); tLabelBox.ID = "KeyIdText";
            autocompletePanel.Controls.Add(tKeyBox);
            autocompletePanel.Controls.Add(tLabelBox);

			SetControls();
		}

		protected override void Render(HtmlTextWriter output)
		{
			if (this.Visible)
			{
                autocompletePanel.Visible = false;
                comboboxPanel.Visible = false;
                if (Mode != "A")
                {
                    comboboxPanel.Visible = true;
                    System.Collections.IEnumerator keys = base.Attributes.Keys.GetEnumerator();
                    String key;
                    while (keys.MoveNext())
                    {
                        key = (string)keys.Current;
                        if ("onchange".IndexOf(key.ToLower()) >= 0)
                        {
                            ibWild.Attributes.Add("OnClick", base.Attributes[key]);
                            ibDDFind.Attributes.Add("OnClick", base.Attributes[key]);
                            ibFirst.Attributes.Add("OnClick", base.Attributes[key]);
                            ibPrev.Attributes.Add("OnClick", base.Attributes[key]);
                            ibNext.Attributes.Add("OnClick", base.Attributes[key]);
                            ibLast.Attributes.Add("OnClick", base.Attributes[key]);
                        }
                        tBox.Attributes.Add(key, base.Attributes[key]);
                        ddList.Attributes.Add(key, base.Attributes[key]);
                    }
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "combobox");
                    output.RenderBeginTag(HtmlTextWriterTag.Div);

                    tBox.Attributes.Add("OnFocus", "this.select();");

                    if (!ddList.Enabled) 
                    { 
                        ddList.Visible = true; tBox.Visible = false; 
                    }

                    if (ddList.Visible) { 
                        output.AddAttribute(HtmlTextWriterAttribute.Id, ddList.ClientID);
                        output.AddAttribute(HtmlTextWriterAttribute.Class, "combobox-dropdownlist");
                    }
                    ddList.RenderControl(output);
                    if (tBox.Visible) {
                        output.AddAttribute(HtmlTextWriterAttribute.Id, tBox.ClientID);
                        output.AddAttribute(HtmlTextWriterAttribute.Class, "combobox-textbox");
                    }
                    tBox.RenderControl(output);

                    
                    if (!ddList.Visible)
                    {
                        output.AddStyleAttribute(HtmlTextWriterStyle.Left, (tBox.Width.Value + 4).ToString() + "px");
                        output.AddAttribute(HtmlTextWriterAttribute.Class, "combobox-controls combobox-control-textbox");
                        output.RenderBeginTag(HtmlTextWriterTag.Div);

                        ibWild.CausesValidation = false;
                        output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
                        ibWild.RenderControl(output);
                        lbMsg.RenderControl(output);
                        output.RenderEndTag(); //</div> - combobox-controls
                    }
                    if (ddList.Enabled && ddList.Visible)
                    {
                        output.AddStyleAttribute(HtmlTextWriterStyle.Left, (ddList.Width.Value).ToString() + "px");
                        output.AddAttribute(HtmlTextWriterAttribute.Class, "combobox-controls combobox-control-dropdownlist");
                        output.RenderBeginTag(HtmlTextWriterTag.Div);
                        ibDDFind.CausesValidation = false;
                        output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
                        ibDDFind.RenderControl(output);

                        lbPages.RenderControl(output);

                        ibFirst.CausesValidation = false;
                        output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
                        ibFirst.RenderControl(output);

                        ibPrev.CausesValidation = false;
                        output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
                        ibPrev.RenderControl(output);

                        ibNext.CausesValidation = false;
                        output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
                        ibNext.RenderControl(output);

                        ibLast.CausesValidation = false;
                        output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
                        ibLast.RenderControl(output);
                        output.RenderEndTag(); //</div> - combobox-controls
                    }
                    
                    output.RenderEndTag(); //</div> - combobox
                }
                else
                {
                    autocompletePanel.Visible = true;
                    System.Collections.IEnumerator keys = base.Attributes.Keys.GetEnumerator();
                    String key;
                    while (keys.MoveNext())
                    {
                        key = (string)keys.Current;
                        string attr = base.Attributes[key] ?? "";
                        if (key.ToLower() == "onchange" && AutoPostBack && !attr.Contains("CanPostBack"))
                        {
                            attr = attr + " if (!CanPostBack(true,this)) return false;";
                        }
                        tKeyBox.Attributes.Add(key, attr);
                        if (key.ToLower() != "onchange") tLabelBox.Attributes.Add(key, attr);
                    }
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string url = AutoCompleteUrl;
                    Dictionary<string, string> context = DataContext;

                    output.AddAttribute(HtmlTextWriterAttribute.Class, "autocomplete-container");
                    output.RenderBeginTag(HtmlTextWriterTag.Div); // container wrapper

                    output.AddAttribute(HtmlTextWriterAttribute.Id, tKeyBox.ClientID);
                    output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
                    tKeyBox.Style["display"] = "none";
                    
                    tKeyBox.RenderControl(output);

                    output.AddAttribute(HtmlTextWriterAttribute.Id, tLabelBox.ClientID);

                    output.AddAttribute(HtmlTextWriterAttribute.Class, "ui-autocomplete-input ui-widget ui-widget-content ui-corner-left autocomplete-box-input");
                    output.AddAttribute("readonly", "true");
                    output.AddAttribute("onfocus", "ActivateAutocomplete" + tKeyBox.ClientID + "(this ,event);return false;");
                    tLabelBox.RenderControl(output);
                    // simulate button layer
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "ui-button ui-widget ui-state-default ui-button-icon-only ui-corner-right ui-button-icon autocomplete-box-button");
                    output.AddAttribute(HtmlTextWriterAttribute.Style, "box-sizing:border-box;");
                    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ActivateAutocomplete" + tKeyBox.ClientID + "(this ,event);return false;");
                    output.RenderBeginTag(HtmlTextWriterTag.Span);
                    Label searchBox = new Label();
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "ui-button-icon-primary ui-icon ui-icon-search");
                    output.AddAttribute(HtmlTextWriterAttribute.Style, "display:inline-block;");
                    searchBox.RenderControl(output);

                    output.RenderEndTag(); // Span

                    output.RenderEndTag(); // div
                    ClientScriptManager cs = this.Page.ClientScript;
                    ScriptManager.RegisterStartupScript(tKeyBox, tKeyBox.GetType(), tKeyBox.ClientID + "combobox",
                        @"function ActivateAutocomplete" + tKeyBox.ClientID + "(ele ,event) { ApplyAutoComplete(ele,'" + tKeyBox.ClientID + "','" + url + "'," + jss.Serialize(context) + ", event);}"
                        , true);
//                    if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
//                    {
//                        ScriptManager.RegisterStartupScript(tKeyBox, tKeyBox.GetType(), tKeyBox.ClientID + "combobox",
//                        @"
//                        try {
//                           Sys.Application.add_load(
//                            function() {
//                                CreateComboAuto('" + tKeyBox.ClientID + @"','" + url + @"'," + jss.Serialize(context) + @", true);                       
//                             });
//                        } catch (e) {}
//                        ",
//                                true);
//                    }
//                    else
//                    {
//                                ScriptManager.RegisterStartupScript(tKeyBox, tKeyBox.GetType(), tKeyBox.ClientID + "combobox",
//                                @"
//                        try {
//                                CreateComboAuto('" + tKeyBox.ClientID + @"','" + url + @"'," + jss.Serialize(context) + @", true);                       
//                        } catch (e) {}
//                        ",
//                        true);

//                    }
                }
            }
		}
	}
}