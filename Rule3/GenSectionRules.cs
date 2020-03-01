namespace RO.Rule3
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Linq;
    using RO.Common3;
    using RO.Common3.Data;
    using RO.SystemFramewk;
    using RO.Access3;

    public class GenSectionRules
    {
        public bool CreateProgram(string SectionCd, CurrPrj CPrj, CurrSrc CSrc)
        {
            using (Access3.GenSectionAccess dac = new Access3.GenSectionAccess())
            {
                StreamWriter sw;
                StringBuilder Ascx;
                StringBuilder AscxCs;
                StringBuilder Css = new StringBuilder();
                DataTable dtObj;

                /* Take care of Default page */
                if (SectionCd == "D")
                {
                    dtObj = dac.GetPageObj(SectionCd);    // Get Default info.
                    Ascx = MakeHAscx(dtObj, "Default", CPrj, CSrc);
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"modules\DefaultModule.ascx"); try { sw.Write(Ascx); }
                    finally { sw.Close(); }
                    AscxCs = MakeHAscxCs(dtObj, SectionCd, "Default", CPrj, CSrc);
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"modules\DefaultModule.ascx.cs"); try { sw.Write(AscxCs); }
                    finally { sw.Close(); }
                    Css.Append(MakeCss(dtObj, "Default", CPrj, CSrc));
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"css\sectionD.css"); try { sw.Write(Css); } finally { sw.Close(); }
                }
                
                /* Take care of Header */
                if (SectionCd == "H")
                {
                    dtObj = dac.GetPageObj(SectionCd);    // Get Header info.
                    Ascx = MakeHAscx(dtObj, "Header", CPrj, CSrc);
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"modules\HeaderModule.ascx"); try { sw.Write(Ascx); }
                    finally { sw.Close(); }
                    AscxCs = MakeHAscxCs(dtObj, SectionCd, "Header", CPrj, CSrc);
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"modules\HeaderModule.ascx.cs"); try { sw.Write(AscxCs); }
                    finally { sw.Close(); }
                    Css.Append(MakeCss(dtObj, "Header", CPrj, CSrc));
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"css\sectionH.css"); try { sw.Write(Css); } finally { sw.Close(); }
                }

                /* Take care of Footer */
                if (SectionCd == "F")
                {
                    dtObj = dac.GetPageObj(SectionCd);    // Get Footer info.
                    Ascx = MakeHAscx(dtObj, "Footer", CPrj, CSrc);
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"modules\FooterModule.ascx"); try { sw.Write(Ascx); }
                    finally { sw.Close(); }
                    AscxCs = MakeHAscxCs(dtObj, SectionCd, "Footer", CPrj, CSrc);
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"modules\FooterModule.ascx.cs"); try { sw.Write(AscxCs); }
                    finally { sw.Close(); }
                    Css.Append(MakeCss(dtObj, "Footer", CPrj, CSrc));
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"css\sectionF.css"); try { sw.Write(Css); } finally { sw.Close(); }
                }

                /* Take care of Sidebar */
                if (SectionCd == "S")
                {
                    dtObj = dac.GetPageObj(SectionCd);    // Get Sidebar info.
                    Ascx = MakeHAscx(dtObj, "Sidebar", CPrj, CSrc);
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"modules\SidebarModule.ascx"); try { sw.Write(Ascx); }
                    finally { sw.Close(); }
                    AscxCs = MakeHAscxCs(dtObj, SectionCd, "Sidebar", CPrj, CSrc);
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"modules\SidebarModule.ascx.cs"); try { sw.Write(AscxCs); }
                    finally { sw.Close(); }
                    Css.Append(MakeCss(dtObj, "Sidebar", CPrj, CSrc));
                    sw = new StreamWriter(CPrj.SrcClientProgramPath + @"css\sectionS.css"); try { sw.Write(Css); } finally { sw.Close(); }
                }
            }
            // Reset regen flag to NO:
            using (Access3.GenSectionAccess dac = new Access3.GenSectionAccess())
            {
                dac.SetSctNeedRegen(SectionCd);
            }
            return true;
        }

        private StringBuilder MakeHAscx(DataTable dtObj, string SectionNm, CurrPrj CPrj, CurrSrc CSrc)
        {
            DataView dvLnk = null;
            DataRow drObj = null;
            StringBuilder sb = new StringBuilder();
            StringBuilder tm = new StringBuilder();
            StringBuilder rp = new StringBuilder();
            sb.Append("<%@ Control Language=\"c#\" Inherits=\"RO.Web." + SectionNm + "Module\" CodeFile=\"" + SectionNm + "Module.ascx.cs\" CodeFileBaseClass=\"RO.Web.ModuleBase\" %>" + Environment.NewLine);
            if (dtObj.Select("LinkTypeCd = 'CUL'").Count() > 0)
            {
                sb.Append("<%@ Register TagPrefix=\"rcasp\" Namespace=\"RoboCoder.WebControls\" Assembly=\"WebControls, Culture=neutral\" %>" + Environment.NewLine);
            }
            if (dtObj.Select("LinkTypeCd = 'MNT'").Count() > 0)
            {
                sb.Append("<%@ Register TagPrefix=\"Module\" TagName=\"MenuTop\" Src=\"MenuTopModule.ascx\" %>" + Environment.NewLine);
            }
            if (dtObj.Select("LinkTypeCd = 'MNS'").Count() > 0)
            {
                sb.Append("<%@ Register TagPrefix=\"Module\" TagName=\"MenuSid\" Src=\"MenuSidModule.ascx\" %>" + Environment.NewLine);
            }
            if (dtObj.Select("LinkTypeCd = 'PRF'").Count() > 0)
            {
                sb.Append("<%@ Register TagPrefix=\"Module\" TagName=\"Profile\" Src=\"MyProfileModule.ascx\" %>" + Environment.NewLine);
            }

            // Prepare body tm:
            if (SectionNm == "Header")
            {
                tm.Append("<div class=\"mobileHeader\">" + Environment.NewLine);
                if (dtObj.Select("LinkTypeCd = 'LGO'").Count() > 0)
                {
                    drObj = (dtObj.Select("LinkTypeCd = 'LGO'"))[0];
                    using (Access3.GenSectionAccess dac = new Access3.GenSectionAccess())
                    {
                        dvLnk = dac.GetPageLnk(drObj["PageObjId"].ToString()).DefaultView;
                    }
                    tm.Append("    <div class=\"mobileLogo\"><asp:HyperLink NavigateUrl=\"" + Utils.AddTilde(dvLnk[0]["PageLnkRef"].ToString()) + "\" runat=\"server\"><asp:Image ImageUrl=\"" + Utils.AddTilde(dvLnk[0]["PageLnkAlt"].ToString()) + "\" runat=\"server\" /></asp:HyperLink></div>" + Environment.NewLine);
                }
                if (dtObj.Select("LinkTypeCd = 'BRC'").Count() > 0)
                {
                    tm.Append("    <div class=\"crumbSec\"><asp:HyperLink ID=\"cMobileCrumb\" CssClass=\"crumbMobile\" runat=\"server\" /></div>" + Environment.NewLine);
                }
                if (dtObj.Select("LinkTypeCd = 'MNT'").Count() > 0)
                {
                    tm.Append("    <div><section id=\"mobileMenu\"><div class=\"sb-navbar sb-slide\"><div class=\"sb-toggle-right\">" + Environment.NewLine);
                    tm.Append("        <asp:Label ID=\"mMenuTitle\" CssClass=\"mMenuTitle\" Text=\"MENU\" runat=\"server\"></asp:Label>" + Environment.NewLine);
                    tm.Append("        <div class=\"navicon-line\"></div><div class=\"navicon-line\"></div><div class=\"navicon-line\"></div>" + Environment.NewLine);
                    tm.Append("    </div></div></section></div>" + Environment.NewLine);
                }
                if (dtObj.Select("LinkTypeCd = 'PRF'").Count() > 0)
                {
                    tm.Append("    <div><asp:HyperLink ID=\"cSignIn\" CssClass=\"SignInMobile\" Text=\"Sign In\" runat=\"server\" /></div>" + Environment.NewLine);
                    tm.Append("    <div><asp:Button ID=\"cProfileButton\" CssClass=\"ProfBtnSec\" OnClientClick=\"slide(this); return false\" Text=\"Profile\" runat=\"server\" /></div>" + Environment.NewLine);
                }
                if (dtObj.Select("LinkTypeCd = 'HDR'").Count() > 0)
                {
                    tm.Append("    <div><asp:Button ID=\"cLinkButton\" CssClass=\"LinkBtnSec\" OnClientClick=\"openLinkSec(); return false;\" Text=\"Link\" runat=\"server\" /></div>" + Environment.NewLine);
                }
                if (dtObj.Select("LinkTypeCd = 'SSO'").Count() > 0)
                {
                    tm.Append("    <div><asp:Button ID=\"cSociButton\" CssClass=\"SociBtnSec\" OnClientClick=\"openSociSec(); return false;\" Text=\"Social\" runat=\"server\" /></div>" + Environment.NewLine);
                }
                tm.Append("</div>" + Environment.NewLine);
            }
            if (dtObj.Select("LinkTypeCd = 'BKI'").Count() > 0)
            {
                drObj = (dtObj.Select("LinkTypeCd = 'BKI'"))[0];
                tm.Append("<div class=\"HideBgImgOnMobile PageObj" + drObj["PageObjId"].ToString() + "\">" + Environment.NewLine);
            }
            string PreviousRow = string.Empty;
            string PreviousCol = string.Empty;
            foreach (DataRowView drv in dtObj.DefaultView)
            {
                if (PreviousRow != string.Empty && drv["RowCssClass"].ToString() != PreviousRow)
                {
                    PreviousRow = "NewRow"; PreviousCol = "NewCol";     // For PreviousCol below.
                }
                if (PreviousCol != string.Empty && drv["ColCssClass"].ToString() != PreviousCol)
                {
                    tm.Append("    </div>" + Environment.NewLine);
                    if (PreviousCol.LastIndexOf("-12") == PreviousCol.Length - 3) { PreviousRow = "NewRow"; PreviousCol = "NewCol"; }
                }
                if (PreviousRow != string.Empty && drv["RowCssClass"].ToString() != PreviousRow)
                {
                    tm.Append("</div></div>" + Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(drv["SctGrpRowId"].ToString()) && !string.IsNullOrEmpty(drv["SctGrpColId"].ToString()))
                {
                    if (drv["RowCssClass"].ToString() != PreviousRow)
                    {
                        tm.Append("<div class=\"r-table " + drv["RowCssClass"].ToString() + " SctGrpRow" + drv["SctGrpRowId"].ToString() + "\"><div class=\"r-tr\">" + Environment.NewLine);
                        PreviousRow = drv["RowCssClass"].ToString(); PreviousCol = "NewCol";
                    }
                    if (drv["ColCssClass"].ToString() != PreviousCol)
                    {
                        tm.Append("    <div class=\"r-td " + drv["ColCssClass"].ToString() + " SctGrpCol" + drv["SctGrpColId"].ToString());
                        if (",LGO,CDD,CDT,BRC,".IndexOf("," + drv["LinkTypeCd"].ToString() + ",") >= 0) { tm.Append(" HideOnMobile"); }
                        tm.Append("\">" + Environment.NewLine);
                        PreviousCol = drv["ColCssClass"].ToString();
                    }
                    if (drv["LinkTypeCd"].ToString() == "LGO")
                    {
                        tm.Append("        <div ID=\"cLogoHolder\" class=\"SctGrpDiv" + drv["SctGrpColId"].ToString() + "\" runat=\"server\">" + Environment.NewLine);
                    }
                    else if (drv["LinkTypeCd"].ToString() == "HDR")
                    {
                        tm.Append("        <div ID=\"cLinkHolder\" class=\"link-content" + (SectionNm == "Header" ? " hideMoreButtonSec" : "") + " SctGrpDiv" + drv["SctGrpColId"].ToString() + "\" runat=\"server\">" + Environment.NewLine);
                    }
                    else if (drv["LinkTypeCd"].ToString() == "SSO")
                    {
                        tm.Append("        <div ID=\"cSociHolder\" class=\"link-content" + (SectionNm == "Header" ? " hideMoreButtonSec" : "") + " SctGrpDiv" + drv["SctGrpColId"].ToString() + "\" runat=\"server\">" + Environment.NewLine);
                    }
                    else if (drv["LinkTypeCd"].ToString() == "CUL")
                    {
                        tm.Append("        <div class=\"reset-lang SctGrpDiv" + drv["SctGrpColId"].ToString() + "\">" + Environment.NewLine);
                    }
                    else if (drv["LinkTypeCd"].ToString() == "CRS")
                    {
                        tm.Append("        <div class=\"flexslider SctGrpDiv" + drv["SctGrpColId"].ToString() + "\">" + Environment.NewLine);
                    }
                    else
                    {
                        tm.Append("        <div class=\"SctGrpDiv" + drv["SctGrpColId"].ToString() + "\">" + Environment.NewLine);
                    }
                    using (Access3.GenSectionAccess dac = new Access3.GenSectionAccess())
                    {
                        dvLnk = dac.GetPageLnk(drv["PageObjId"].ToString()).DefaultView;
                    }
                    if (",HDR,SSO,LNK,CRS,".IndexOf("," + drv["LinkTypeCd"].ToString() + ",") >= 0)
                    {
                        if (drv["LinkTypeCd"].ToString() == "CRS")
                        {
                            tm.Append("        <ul class=\"slides PageObj" + drv["PageObjId"].ToString() + "\">" + Environment.NewLine);
                        }
                        else
                        {
                            tm.Append("        <div class=\"r-table PageObj" + drv["PageObjId"].ToString() + "\"><div class=\"r-tr\"><div class=\"r-td\">" + Environment.NewLine);
                        }
                        bool bFirstLnk = true;
                        foreach (DataRowView drvn in dvLnk)
                        {
                            tm.Append("            ");
                            if (drv["LinkTypeCd"].ToString() == "CRS")
                            {
                                if (bFirstLnk) { tm.Append("<li class=\"flex-active-slide\">"); bFirstLnk = false; } else { tm.Append("<li style=\"display:none;\">"); }
                            }
                            /* Stack image on top of text */
                            if (!string.IsNullOrEmpty(drvn["PageLnkImg"].ToString()))
                            {
                                // Cannot use asp:HyperLink because of mouseover script:
                                tm.Append("<asp:ImageButton ID=\"PageLnk" + drvn["PageLnkId"].ToString() + "Img\"");
                                tm.Append(" ImageUrl=\"" + Utils.AddTilde(drvn["PageLnkImg"].ToString()) + "\"");
                                // CSS applies to text if both image and text exist:
                                if (string.IsNullOrEmpty(drvn["PageLnkTxt"].ToString())) { tm.Append(" CssClass=\"PageLnk" + drvn["PageLnkId"].ToString() + "\""); }
                                if (!string.IsNullOrEmpty(drvn["PageLnkRef"].ToString()))
                                {
                                    if (drvn["PageLnkRef"].ToString().ToLower().IndexOf("javascript:") >= 0)
                                    {
                                        tm.Append(" OnClientClick=\"" + drvn["PageLnkRef"].ToString().Substring(drvn["PageLnkRef"].ToString().ToLower().IndexOf("javascript:") + 11) + "\"");
                                    }
                                    else if (drvn["Popup"].ToString() == "Y" && !drvn["PageLnkRef"].ToString().ToLower().StartsWith("mailto"))
                                    {
                                        tm.Append(" OnClientClick=\"SearchLink('" + drvn["PageLnkRef"].ToString() + "','','',''); return stopEvent(this,event);\" NavigateUrl=\"#\"");
                                    }
                                    else
                                    {
                                        tm.Append(" NavigateUrl=\"" + Utils.AddTilde(drvn["PageLnkRef"].ToString()) + "\"");
                                    }
                                }
                                tm.Append(" runat=\"server\" />");
                                // Prepare toggle script rp:
                                if (drv["LinkTypeCd"].ToString() != "LGO" && !string.IsNullOrEmpty(drvn["PageLnkAlt"].ToString()))
                                {
                                    rp.Append("    Sys.Application.add_load(function () { var pid = $('#<%=PageLnk" + drvn["PageLnkId"].ToString() + "Img.ClientID%>');" + Environment.NewLine);
                                    rp.Append("        pid.mouseover(function () { pid.attr('src','" + Utils.StripTilde(drvn["PageLnkAlt"].ToString(), false) + "'); });" + Environment.NewLine);
                                    rp.Append("        pid.mouseout(function () { pid.attr('src','" + Utils.StripTilde(drvn["PageLnkImg"].ToString(), false) + "'); });" + Environment.NewLine);
                                    rp.Append("    });" + Environment.NewLine);
                                }
                            }
                            if (!string.IsNullOrEmpty(drvn["PageLnkTxt"].ToString()))
                            {
                                tm.Append("<asp:HyperLink Text=\"" + drvn["PageLnkTxt"].ToString() + "\"");
                                tm.Append(" CssClass=\"PageLnk" + drvn["PageLnkId"].ToString() + "\"");
                                if (!string.IsNullOrEmpty(drvn["PageLnkRef"].ToString()))
                                {
                                    if (drvn["PageLnkRef"].ToString().ToLower().IndexOf("javascript:") >= 0)
                                    {
                                        tm.Append(" OnClick=\"" + drvn["PageLnkRef"].ToString().Substring(drvn["PageLnkRef"].ToString().ToLower().IndexOf("javascript:") + 11) + "\"");
                                    }
                                    else if (drvn["Popup"].ToString() == "Y" && !drvn["PageLnkRef"].ToString().ToLower().StartsWith("mailto"))
                                    {
                                        tm.Append(" OnClick=\"SearchLink('" + drvn["PageLnkRef"].ToString() + "','','',''); return stopEvent(this,event);\" NavigateUrl=\"#\"");
                                    }
                                    else
                                    {
                                        tm.Append(" NavigateUrl=\"" + Utils.AddTilde(drvn["PageLnkRef"].ToString()) + "\"");
                                    }
                                }
                                tm.Append(" runat=\"server\" />");
                            }
                            if (drv["LinkTypeCd"].ToString() == "CRS") { tm.Append("</li>" + Environment.NewLine); } else { tm.Append(Environment.NewLine); }
                        }
                        if (drv["LinkTypeCd"].ToString() == "CRS")
                        {
                            tm.Append("        </ul>" + Environment.NewLine);
                        }
                        else
                        {
                            tm.Append("        </div></div></div>" + Environment.NewLine);
                        }
                    }
                    else /* Singular Object: do not allow button as PostBackUrl is causing validation issue. One can always style an image button or hyperlink as a button via css. */
                    {
                        tm.Append("        <div class=\"PageObj" + drv["PageObjId"].ToString() + "\">" + Environment.NewLine);
                        if (drv["LinkTypeCd"].ToString() == "LGO" || drv["LinkTypeCd"].ToString() == "IMG")
                        {
                            tm.Append("            <asp:HyperLink NavigateUrl=\"" + Utils.AddTilde(dvLnk[0]["PageLnkRef"].ToString()) + "\" runat=\"server\"><asp:Image CssClass=\"PageLnk" + dvLnk[0]["PageLnkId"].ToString() + "\" ImageUrl=\"" + Utils.AddTilde(dvLnk[0]["PageLnkImg"].ToString()) + "\" runat=\"server\" /></asp:HyperLink>" + Environment.NewLine);
                        }
                        else if (drv["LinkTypeCd"].ToString() == "PRF")
                        {
                            tm.Append("            <Module:Profile ID=\"ModuleProfile\" runat=\"server\" />" + Environment.NewLine);
                        }
                        else if (drv["LinkTypeCd"].ToString() == "MNT")
                        {
                            tm.Append("            <Module:MenuTop ID=\"ModuleMenuTop\" runat=\"server\" />" + Environment.NewLine);
                        }
                        else if (drv["LinkTypeCd"].ToString() == "MNS")
                        {
                            tm.Append("            <Module:MenuSid ID=\"ModuleMenuSid\" runat=\"server\" />" + Environment.NewLine);
                        }
                        else if (drv["LinkTypeCd"].ToString() == "BRC")
                        {
                            tm.Append("            <asp:Literal ID=\"cBreadCrumb\" EnableViewState=\"false\" runat=\"server\" />" + Environment.NewLine);
                        }
                        else if (drv["LinkTypeCd"].ToString() == "CDD" || drv["LinkTypeCd"].ToString() == "CDT")
                        {
                            tm.Append("            <asp:Label ID=\"cWelcomeTime\" runat=\"server\" />" + Environment.NewLine);
                        }
                        else if (drv["LinkTypeCd"].ToString() == "CUL")
                        {
                            tm.Append("            <rcasp:ComboBox ID=\"cLang\" CssClass=\"inp-ddl\" Mode=\"A\" AutoPostBack=\"true\" OnPostBack=\"cbPostBack\" OnSearch=\"cbCultureId\" DataValueField=\"CultureTypeId\" DataTextField=\"CultureTypeLabel\" runat=\"server\" OnSelectedIndexChanged=\"cLang_SelectedIndexChanged\" />" + Environment.NewLine);
                            tm.Append("            <asp:ImageButton ID=\"lanResetBtn\" CssClass=\"lanResetBtn\" runat=\"server\" ImageUrl=\"~/images/reset.jpg\" OnClick=\"lanResetBtn_Click\" ToolTip=\"Reset Language\" CausesValidation=\"false\" />" + Environment.NewLine);
                        }
                        else if (drv["LinkTypeCd"].ToString() == "VER")
                        {
                            tm.Append("            <asp:Label id=\"cVersionTxt\" runat=\"server\" />" + Environment.NewLine);
                        }
                        else if (drv["LinkTypeCd"].ToString() == "LAB")
                        {
                            tm.Append("            <asp:Label CssClass=\"PageLnk" + dvLnk[0]["PageLnkId"].ToString() + "\" Text=\"" + dvLnk[0]["PageLnkTxt"].ToString() + "\" runat=\"server\" />" + Environment.NewLine);
                        }
                        else /* Just in case */
                        {
                            tm.Append("" + Environment.NewLine);
                        }
                        tm.Append("        </div>" + Environment.NewLine);
                    }
                    tm.Append("        </div>" + Environment.NewLine);
                }
            }
            /* Take care of the last */
            if (PreviousCol != string.Empty) { tm.Append("    </div>" + Environment.NewLine); }
            if (PreviousRow != string.Empty) { tm.Append("</div></div>" + Environment.NewLine); }
            if (dtObj.Select("LinkTypeCd = 'BKI'").Count() > 0) { tm.Append("</div>" + Environment.NewLine); }

            // Complete the page:
            sb.Append("<script>" + Environment.NewLine);
            if (dtObj.Select("LinkTypeCd = 'HDR'").Count() > 0)
            {
                sb.Append("    function openLinkSec() { var linkContainer = $('#<%=cLinkHolder.ClientID%>'); if (linkContainer.hasClass('hideMoreButtonSec')) { linkContainer.removeClass('hideMoreButtonSec'); } else { linkContainer.addClass('hideMoreButtonSec'); }; }" + Environment.NewLine);
                sb.Append("    $(document).mouseup(function (e) { var linkContainer = $('#<%=cLinkHolder.ClientID%>'); if ($(window).width() <= 1024) { if (!linkContainer.is(e.target) && linkContainer.has(e.target).length === 0 && !linkContainer.hasClass('hideMoreButtonSec')) { openLinkSec(); } } });" + Environment.NewLine);
            }
            if (dtObj.Select("LinkTypeCd = 'SSO'").Count() > 0)
            {
                sb.Append("    function openSociSec() { var socialContainer = $('#<%=cSociHolder.ClientID%>'); if (socialContainer.hasClass('hideMoreButtonSec')) { socialContainer.removeClass('hideMoreButtonSec'); } else { socialContainer.addClass('hideMoreButtonSec'); }; }" + Environment.NewLine);
                sb.Append("    $(document).mouseup(function (e) { var socialContainer = $('#<%=cSociHolder.ClientID%>'); if ($(window).width() <= 1024) { if (!socialContainer.is(e.target) && socialContainer.has(e.target).length === 0 && !socialContainer.hasClass('hideMoreButtonSec')) { openSociSec(); } } });" + Environment.NewLine);
            }
            if (dtObj.Select("PageObjSrp is not null AND PageObjSrp <> ''").Count() > 0)
            {
                foreach (DataRowView drv in dtObj.DefaultView)
                {
                    if (!string.IsNullOrEmpty(drv["PageObjSrp"].ToString()) && sb.ToString().IndexOf(drv["PageObjSrp"].ToString()) < 0) { sb.Append(drv["PageObjSrp"].ToString() + Environment.NewLine); }
                }
            }
            sb.Append(rp.ToString());
            sb.Append("</script>" + Environment.NewLine);
            sb.Append(tm.ToString());
            return sb;
        }

        private StringBuilder MakeHAscxCs(DataTable dtObj, string SectionCd, string SectionNm, CurrPrj CPrj, CurrSrc CSrc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("namespace RO.Web" + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    using System;" + Environment.NewLine);
            sb.Append("    using System.Data;" + Environment.NewLine);
            sb.Append("    using System.Drawing;" + Environment.NewLine);
            sb.Append("    using System.Web;" + Environment.NewLine);
            sb.Append("    using System.Web.UI.WebControls;" + Environment.NewLine);
            sb.Append("    using System.Web.UI.HtmlControls;" + Environment.NewLine);
            sb.Append("    using RO.Facade3;" + Environment.NewLine);
            sb.Append("    using RO.Common3;" + Environment.NewLine);
            sb.Append("    using RO.Common3.Data;" + Environment.NewLine + Environment.NewLine);
            sb.Append("    public partial class " + SectionNm + "Module : RO.Web.ModuleBase" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("        private const string KEY_" + SectionNm + "Generated = \"Cache:" + SectionNm + "Generated\";" + Environment.NewLine + Environment.NewLine);
            sb.Append("        public " + SectionNm + "Module()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            this.Init += new System.EventHandler(Page_Init);" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine + Environment.NewLine);
            sb.Append("        protected void Page_Load(object sender, System.EventArgs e)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (!IsPostBack)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (Session[KEY_" + SectionNm + "Generated] == null) try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    if (base.CPrj != null && base.CSrc != null && Config.DeployType == \"DEV\" && (new AdminSystem()).IsRegenNeeded(\"" + SectionNm + "\", 0, 0, 0, string.Empty, string.Empty))" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        (new GenSectionSystem()).CreateProgram(\"" + SectionCd + "\", base.CPrj, base.CSrc);" + Environment.NewLine);
            sb.Append("                        Session[KEY_" + SectionNm + "Generated] = true; this.Redirect(Request.RawUrl);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception err) { throw new ApplicationException(err.Message); }" + Environment.NewLine);
            if (dtObj.Select("LinkTypeCd = 'PRF'").Count() > 0)
            {
                sb.Append("                if (!Request.IsAuthenticated || LUser == null || LUser.LoginName.ToLower() == \"anonymous\")" + Environment.NewLine);
                sb.Append("                {" + Environment.NewLine);
                sb.Append("                    string loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;" + Environment.NewLine);
                sb.Append("                    if (string.IsNullOrEmpty(loginUrl)) loginUrl = \"MyAccount.aspx\";" + Environment.NewLine);
                sb.Append("                    cSignIn.Visible = true; cSignIn.NavigateUrl = \"~/\" + loginUrl + (loginUrl.Contains(\"?\") ? \"&\" : \"?\") + \"logo=N\"; cProfileButton.Visible = false;" + Environment.NewLine);
                sb.Append("                }" + Environment.NewLine);
                sb.Append("                else" + Environment.NewLine);
                sb.Append("                {" + Environment.NewLine);
                sb.Append("                    cSignIn.Visible = false; cProfileButton.Visible = true;" + Environment.NewLine);
                sb.Append("                }" + Environment.NewLine);
            }
            sb.Append("                if (Request.IsAuthenticated && base.LUser != null)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            if (dtObj.Select("LinkTypeCd = 'VER'").Count() > 0)
            {
                sb.Append("                    try {" + Environment.NewLine);
                sb.Append("                        byte sid = byte.Parse(((DropDownList)Page.Master.FindControl(\"ModuleHeader\").FindControl(\"ModuleProfile\").FindControl(\"SystemsList\")).SelectedValue);" + Environment.NewLine);
                sb.Append("                        cVersionTxt.Text = \"&#169;1999-\" + DateTime.Now.Year.ToString() + \" Robocoder Corporation. All rights reserved (V\" + (new LoginSystem()).GetAppVersion(base.SysConnectStr(sid), base.AppPwd(sid)) + \" by R\" + (new LoginSystem()).GetRbtVersion() + \"). Protected by U.S. Patent 6,876,314.\";" + Environment.NewLine);
                sb.Append("                    } catch { cVersionTxt.Text = \"&#169;1999-\" + DateTime.Now.Year.ToString() + \" Robocoder Corporation. All rights reserved.\"; }" + Environment.NewLine);
            }
            if (dtObj.Select("LinkTypeCd = 'CUL'").Count() > 0)
            {
                sb.Append("                    SetCultureId(cLang, LUser.CultureId.ToString());" + Environment.NewLine);
            }
            if (dtObj.Select("LinkTypeCd = 'CDD'").Count() > 0)
            {
                sb.Append("                    cWelcomeTime.Text = Utils.fmLongDate(DateTime.Now.ToString(), LUser.Culture);" + Environment.NewLine);
            }
            else if (dtObj.Select("LinkTypeCd = 'CDT'").Count() > 0)
            {
                sb.Append("                    cWelcomeTime.Text = Utils.fmLongDateTime(DateTime.Now.ToString(), LUser.Culture);" + Environment.NewLine);
            }
            sb.Append("                }" + Environment.NewLine);
            if (dtObj.Select("LinkTypeCd = 'LGO'").Count() > 0 || dtObj.Select("LinkTypeCd = 'HDR'").Count() > 0 || dtObj.Select("LinkTypeCd = 'SSO'").Count() > 0)
            {
                sb.Append("                if (base.LUser != null && base.LPref != null && Request.QueryString[\"typ\"] != null && Request.QueryString[\"typ\"].ToString() == \"N\")" + Environment.NewLine);
                sb.Append("                {" + Environment.NewLine);
                if (dtObj.Select("LinkTypeCd = 'LGO'").Count() > 0) { sb.Append("                    cLogoHolder.Visible = false;" + Environment.NewLine); }
                if (dtObj.Select("LinkTypeCd = 'HDR'").Count() > 0) { sb.Append("                    cLinkHolder.Visible = false;" + Environment.NewLine); }
                if (dtObj.Select("LinkTypeCd = 'SSO'").Count() > 0) { sb.Append("                    cSociHolder.Visible = false;" + Environment.NewLine); }
                sb.Append("                }" + Environment.NewLine);
            }
            if (SectionNm == "Header")
            {
                if (dtObj.Select("LinkTypeCd = 'HDR'").Count() > 0) { sb.Append("                if (cLinkHolder.Controls.Count <= 0) { cLinkButton.Visible = false; }" + Environment.NewLine); }
                if (dtObj.Select("LinkTypeCd = 'SSO'").Count() > 0) { sb.Append("                if (cSociHolder.Controls.Count <= 0) { cSociButton.Visible = false; }" + Environment.NewLine); }
            }
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine + Environment.NewLine);
            sb.Append("        protected void Page_PreRender(object sender, System.EventArgs e)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            if (dtObj.Select("LinkTypeCd = 'MNT'").Count() > 0)
            {
                sb.Append("            if (this.Visible && Request.IsAuthenticated && base.LUser != null && base.VMenu != null)" + Environment.NewLine);
                sb.Append("            {" + Environment.NewLine);
                sb.Append("                base.VMenu.RowFilter = \"QId='\" + PageBase.ExpandNode + \"'\";" + Environment.NewLine);
                sb.Append("            }" + Environment.NewLine);
                if (dtObj.Select("LinkTypeCd = 'BRC'").Count() > 0)
                {
                    sb.Append("            if (base.VMenu != null && base.VMenu.Count > 0)" + Environment.NewLine);
                    sb.Append("            {" + Environment.NewLine);
                    sb.Append("                string ftr = base.VMenu.RowFilter;" + Environment.NewLine);
                    sb.Append("                cBreadCrumb.Text = \"<strong>\" + base.VMenu[0][\"MenuText\"].ToString() + \"</strong>\";" + Environment.NewLine);
                    sb.Append("                while (base.VMenu[0][\"ParentQId\"].ToString() != string.Empty)" + Environment.NewLine);
                    sb.Append("                {" + Environment.NewLine);
                    sb.Append("                    base.VMenu.RowFilter = \"QId='\" + base.VMenu[0][\"ParentQId\"].ToString() + \"'\";" + Environment.NewLine);
                    sb.Append("                    cBreadCrumb.Text = base.VMenu[0][\"MenuText\"].ToString() + \"&gt; \" + cBreadCrumb.Text;" + Environment.NewLine);
                    sb.Append("                }" + Environment.NewLine);
                    sb.Append("                base.VMenu.RowFilter = ftr;" + Environment.NewLine);
                    sb.Append("                if (HttpContext.Current.Request.Url.AbsolutePath.Contains(Config.SslUrl)) { cMobileCrumb.Text = Config.WebTitle; } else { cMobileCrumb.Text = base.VMenu[0][\"MenuText\"].ToString(); }" + Environment.NewLine);
                    sb.Append("            }" + Environment.NewLine);
                }
            }
            sb.Append("        }" + Environment.NewLine + Environment.NewLine);
            sb.Append("        protected void Page_Init(object sender, EventArgs e)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            InitializeComponent();" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine + Environment.NewLine);
            sb.Append("        #region Web Form Designer generated code" + Environment.NewLine);
            sb.Append("        ///		Required method for Designer support - do not modify" + Environment.NewLine);
            sb.Append("        ///		the contents of this method with the code editor." + Environment.NewLine);
            sb.Append("        /// </summary>" + Environment.NewLine);
            sb.Append("        private void InitializeComponent()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            if (SectionNm.ToLower() == "default") { sb.Append("            base.CheckAuthentication(true, true);  // Authorized users only;" + Environment.NewLine); } else { sb.Append("" + Environment.NewLine); }
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        #endregion" + Environment.NewLine + Environment.NewLine);
            if (dtObj.Select("LinkTypeCd = 'CUL'").Count() > 0)
            {
                sb.Append("        protected void cbPostBack(object sender, System.EventArgs e)" + Environment.NewLine);
                sb.Append("        {" + Environment.NewLine);
                sb.Append("        }" + Environment.NewLine + Environment.NewLine);
                sb.Append("        protected void cbCultureId(object sender, System.EventArgs e)" + Environment.NewLine);
                sb.Append("        {" + Environment.NewLine);
                sb.Append("            SetCultureId((RoboCoder.WebControls.ComboBox)sender, string.Empty);" + Environment.NewLine);
                sb.Append("        }" + Environment.NewLine + Environment.NewLine);
                sb.Append("        private void SetCultureId(RoboCoder.WebControls.ComboBox ddl, string keyId)" + Environment.NewLine);
                sb.Append("        {" + Environment.NewLine);
                sb.Append("            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();" + Environment.NewLine);
                sb.Append("            context[\"method\"] = \"GetDdlCultureId\";" + Environment.NewLine);
                sb.Append("            context[\"addnew\"] = \"Y\";" + Environment.NewLine);
                sb.Append("            context[\"mKey\"] = \"CultureTypeId\";" + Environment.NewLine);
                sb.Append("            context[\"mVal\"] = \"CultureTypeLabel\";" + Environment.NewLine);
                sb.Append("            context[\"mTip\"] = \"CultureTypeLabel\";" + Environment.NewLine);
                sb.Append("            context[\"mImg\"] = \"CultureTypeLabel\";" + Environment.NewLine);
                sb.Append("            context[\"ssd\"] = Request.QueryString[\"ssd\"];" + Environment.NewLine);
                sb.Append("            context[\"scr\"] = \"1\";" + Environment.NewLine);
                sb.Append("            context[\"csy\"] = \"3\";" + Environment.NewLine);
                sb.Append("            context[\"filter\"] = \"0\";" + Environment.NewLine);
                sb.Append("            context[\"isSys\"] = \"N\";" + Environment.NewLine);
                sb.Append("            context[\"conn\"] = string.Empty;" + Environment.NewLine);
                sb.Append("            ddl.AutoCompleteUrl = \"AutoComplete.aspx/DdlSuggests\";" + Environment.NewLine);
                sb.Append("            ddl.DataContext = context;" + Environment.NewLine);
                sb.Append("            if (ddl != null)" + Environment.NewLine);
                sb.Append("            {" + Environment.NewLine);
                sb.Append("                DataView dv = null;" + Environment.NewLine);
                sb.Append("                if (keyId == string.Empty && ddl.SearchText.StartsWith(\"**\")) { keyId = ddl.SearchText.Substring(2); }" + Environment.NewLine);
                sb.Append("                try { dv = new DataView((new AdminSystem()).GetDdl(1, \"GetDdlCultureId\", true, false, 0, keyId, null, null, string.Empty, base.LImpr, base.LCurr)); } catch { return; }" + Environment.NewLine);
                sb.Append("                ddl.DataSource = dv;" + Environment.NewLine);
                sb.Append("                try { ddl.SelectByValue(keyId, string.Empty, false); } catch { try { ddl.SelectedIndex = 0; } catch { } }" + Environment.NewLine);
                sb.Append("            }" + Environment.NewLine);
                sb.Append("        }" + Environment.NewLine + Environment.NewLine);
                sb.Append("        protected void cLang_SelectedIndexChanged(object sender, EventArgs e)" + Environment.NewLine);
                sb.Append("        {" + Environment.NewLine);
                sb.Append("            if (!string.IsNullOrEmpty(cLang.SelectedValue))" + Environment.NewLine);
                sb.Append("            {" + Environment.NewLine);
                sb.Append("                base.LUser.CultureId = short.Parse(cLang.SelectedValue);" + Environment.NewLine);
                sb.Append("                base.LUser.Culture = (new AdminSystem()).SetCult(base.LUser.UsrId, base.LUser.CultureId);" + Environment.NewLine);
                sb.Append("                base.LImpr = null; SetImpersonation(LUser.UsrId);" + Environment.NewLine);
                sb.Append("                base.VMenu = new DataView((new MenuSystem()).GetMenu(base.LUser.CultureId, base.LCurr.SystemId, base.LImpr, base.SysConnectStr(base.LCurr.SystemId), base.AppPwd(base.LCurr.SystemId), null, null, null));" + Environment.NewLine);
                sb.Append("                this.Redirect(Request.Url.PathAndQuery);    // No need to SetCultureId(cLang, LUser.CultureId.ToString());" + Environment.NewLine);
                sb.Append("            }" + Environment.NewLine);
                sb.Append("        }" + Environment.NewLine + Environment.NewLine);
                sb.Append("        protected void lanResetBtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)" + Environment.NewLine);
                sb.Append("        {" + Environment.NewLine);
                sb.Append("            base.LUser.CultureId = 1;" + Environment.NewLine);
                sb.Append("            base.LUser.Culture = (new AdminSystem()).SetCult(base.LUser.UsrId, base.LUser.CultureId);" + Environment.NewLine);
                sb.Append("            if ((LUser.LoginName ?? string.Empty).ToLower() != \"anonymous\") { base.LImpr = null; SetImpersonation(LUser.UsrId); } else { base.LImpr.Cultures = base.LUser.CultureId.ToString(); }" + Environment.NewLine);
                sb.Append("            base.VMenu = new DataView((new MenuSystem()).GetMenu(base.LUser.CultureId, base.LCurr.SystemId, base.LImpr, base.SysConnectStr(base.LCurr.SystemId), base.AppPwd(base.LCurr.SystemId), null, null, null));" + Environment.NewLine);
                sb.Append("            this.Redirect(Request.Url.PathAndQuery);" + Environment.NewLine);
                sb.Append("        }" + Environment.NewLine);
            }
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            return sb;
        }

        private StringBuilder MakeCss(DataTable dtObj, string SectionNm, CurrPrj CPrj, CurrSrc CSrc)
        {
            StringBuilder sb = new StringBuilder();
            string str;
            DataView dvLnk = null;
            sb.Append(Environment.NewLine + "/* " + SectionNm + " */" + Environment.NewLine);
            foreach (DataRowView drv in dtObj.DefaultView)
            {
                str = sb.ToString();
                if (!string.IsNullOrEmpty(drv["SctGrpRowId"].ToString()) && str.IndexOf(".SctGrpRow" + drv["SctGrpRowId"].ToString()) < 0)
                {
                    sb.Append(".SctGrpRow" + drv["SctGrpRowId"].ToString() + " { " + drv["SctGrpRowCss"].ToString() + " }" + Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(drv["SctGrpColId"].ToString()))
                {
                    if (str.IndexOf(".SctGrpCol" + drv["SctGrpColId"].ToString()) < 0)
                    {
                        sb.Append(".SctGrpCol" + drv["SctGrpColId"].ToString() + " { " + drv["SctGrpColCss"].ToString() + " }" + Environment.NewLine);
                    }
                    if (str.IndexOf(".SctGrpDiv" + drv["SctGrpColId"].ToString()) < 0)
                    {
                        sb.Append(".SctGrpDiv" + drv["SctGrpColId"].ToString() + " { " + drv["SctGrpColDiv"].ToString() + " }" + Environment.NewLine);
                    }
                }
                sb.Append(".PageObj" + drv["PageObjId"].ToString() + " { " + drv["PageObjCss"].ToString() + " }" + Environment.NewLine);
                using (Access3.GenSectionAccess dac = new Access3.GenSectionAccess())
                {
                    dvLnk = dac.GetPageLnk(drv["PageObjId"].ToString()).DefaultView;
                }
                foreach (DataRowView drvn in dvLnk)
                {
                    sb.Append(".PageLnk" + drvn["PageLnkId"].ToString() + " { " + drvn["PageLnkCss"].ToString() + " }" + Environment.NewLine);
                }
            }
            return sb;
        }
    }
}
