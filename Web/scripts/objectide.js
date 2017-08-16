/* Object IDE */
//init functions
function buildColTree(columnList) {
    var myList = [];
    var prevTabId = "", dl = "", icon = "", objCnt = "", tabCnt;
    $.each(columnList, function (index, element) {
        try {
            if (element.MasterTable == "Y" && (element.ScreenTypeName == "I1" || element.ScreenTypeName == "I2")) { icon = "obj_drag_icon"; } else { icon = "grid_drag_icon"; }
            tabCnt = '<div class="tabContainer" tabId="' + element.ScreenTabId + '" tabText="' + element.TabFolderName + '"><div class="tabFolder"><span class="tab_drag_icon" href="javascript:void(0)" style="display: block;"></span><a class="tabfolderName">' + element.TabFolderName + '</a><button class="tab_delete" onclick="DelScreenTab(); return false;" title="delete"></button></div><div class="tabCnt">';
            objCnt = '<div class="colItem" MasterTb="' + element.MasterTable + '" ScreenObjId="' + element.ScreenObjId + '" columnHeader="' + element.ColumnHeader + '"><span class="' + icon + '" href="javascript:void(0)" style="display: block;"></span><a>' + element.ColumnHeader + '</a><button class="col_delete" onclick="DelScreenObj(this); return false;" title="delete"></button></div>'
            if (prevTabId == "") { if (element.ScreenObjId == "") { dl += tabCnt; } else { dl += tabCnt + objCnt; } }
            else if (prevTabId != "" && element.ScreenObjId == "") { dl += '</div></div>' + tabCnt }
            else if (element.ScreenTabId == prevTabId) { dl += objCnt; }
            else { dl += '</div></div>' + tabCnt + objCnt; }
            prevTabId = element.ScreenTabId;
        } catch (e) { }
    });

    myList.push(dl + '</div></div>');
    dl = "";

    if (myList.length > 0) { return myList.join(''); }
    return null;
};

/* Object IDE right Side Content */
function buildColCnt(columnlist) {
    var gridTitle = 'Grid Layout (Runtime max-width can be defined in "OBJ PROPERTY")';
    var myCntList = [];
    var prevTabId = "", dl = "", grd = "", prevColGrp = "", prevRowGrp = "", tabCnt = "", rowCnt = "", objCnt = "", objElement = "", prevGridGrp = "",
        grdOuterCnt = "", grdInnerCnt = "", prevScreenObjId = "", grdDrop = "", grdOuterDrp = "", grdOuterCnt = "", grdInnerCnt = "", rowEmpty = "",
        colEmpty = "", tabLink = "", tabItem = "", screenType = "", rowClass = "", colClass = "", rowNum = "", colNum = "", grdAdd = "";
    var columnHeight = "";
    var fieldvalidate = "";
    screenType = columnlist[0].ScreenTypeName;
    if (screenType == "I1") {
        $.each(columnlist, function (index, element) {
            try {
                if (element.RequiredValid == "Y") {
                    fieldvalidate = "<span class='Mandatory'>*</span>";
                } else {
                    fieldvalidate = "";
                }
                columnHeight = effectiveHeight(element);
                rowNum = CheckPosition(element.RowCssClass.replace('rg-', ''));
                colNum = CheckPosition(element.ColCssClass.replace('rc-', ''));
                rowClass = "row_" + rowNum;
                colClass = "col_" + colNum;
                rowEmptyCnt = '<div class="r-tr"><div class="r-td col_12"><div class="screen-tabfolder"><div class="r-table"></div></div></div></div>';
                rowEmptyLastCnt = '<div class="r-tr"><div class="r-td col_12"><div class="screen-tabfolder"><div class="r-table"></div></div></div></div>';
                rowEmptyFirst = '<div class="r-table row_12 rowContainer rowEmpty first">' + rowEmptyCnt + '</div>';
                rowEmptyLast = '<div class="r-table row_12 rowContainer rowEmpty last">' + rowEmptyLastCnt + '</div><div class="folderBtnSection"></div>';
                colEmpty = '<div class="r-td rcEmpty"><div class="screen-tabfolder"><div class="r-table"></div></div></div>';

                tabCnt = '<div class="tabpanel" tabid="' + element.ScreenTabId + '" tabtext="' + element.TabFolderName + '"><div class="tabname"><h3>' + element.TabFolderName + '</h3><button class="AddFolderItemBtn" onclick="AddFolderItem(this); return false;" title="Add New Folder Item">ADD NEW ITEM</button><div style="clear:both;"></div></div><div class="colCnt">';
                rowCnt = '<div class="r-table ' + rowClass + ' rowContainer"><div class="r-tr">';
                objCnt = '<div class="r-td ' + colClass + '"><div class="screen-tabfolder"><div class="r-table">';

                if (columnHeight != "") {
                    objElement = '<div class="r-tr"><div class="drgItem" screenObjId="' + element.ScreenObjId + '" style="height: ' + columnHeight + 'px"><div class="drgItemCnt"><a title="' + element.ColumnHeader + '">' + element.ColumnHeader + fieldvalidate + '</a><button class="col_delete" onclick="DelFolderItem(this); return false;" title="delete"></button></div></div></div>';
                } else {
                    objElement = '<div class="r-tr"><div class="drgItem" screenObjId="' + element.ScreenObjId + '"><div class="drgItemCnt"><a title="' + element.ColumnHeader + '">' + element.ColumnHeader + fieldvalidate + '</a><button class="col_delete" onclick="DelFolderItem(this); return false;" title="delete"></button></div></div></div>';
                }
                if (prevTabId == "") {
                    tabItem += '<a href="#" tabid="' + element.ScreenTabId + '" onclick="activateTab(this);">' + element.TabFolderName + '<button class="delTabLink" onclick="DelTab(this); return false;" title="delete"></button></a>';

                    if (element.ScreenObjId == "") {
                        dl += tabCnt;
                    } else {
                        dl += tabCnt + rowEmptyFirst + rowCnt + colEmpty + objCnt + objElement;
                    }
                } else if (element.ScreenTabId == prevTabId) {
                    if (prevRowGrp == '') {
                        dl += rowCnt + colEmpty + objCnt + objElement;
                    } else if (prevRowGrp != '' && element.RowCssClass != prevRowGrp) {
                        dl += '</div></div></div>' + colEmpty + '</div></div>' + rowCnt + colEmpty + objCnt + objElement;
                    } else {
                        if (element.ColCssClass != prevColGrp) {
                            if (prevColGrp.indexOf("12") >= 0 || element.NewGroupRow == "Y") { //If previous item row group is ended with 12 or new row group is checked for current item
                                dl += '</div></div></div>' + colEmpty + '</div></div>' + rowCnt + colEmpty + objCnt + objElement;
                            } else {
                                dl += '</div></div></div>' + colEmpty + objCnt + objElement;
                            }
                        } else {
                            if (element.NewGroupRow == "Y") {
                                dl += '</div></div></div>' + colEmpty + '</div></div>' + rowCnt + colEmpty + objCnt + objElement;
                            } else {
                                dl += objElement;
                            }
                        }
                    }
                } else {
                    tabItem += '<a href="#" tabid="' + element.ScreenTabId + '" onclick="activateTab(this);">' + element.TabFolderName + '<button class="delTabLink" onclick="DelTab(this); return false;" title="delete"></button></a>';

                    if (prevScreenObjId == "") {
                        dl += rowEmptyLast + '</div></div>';
                    } else {
                        dl += '</div></div></div>' + colEmpty + '</div></div>' + rowEmptyLast + '</div></div>';
                    }

                    if (element.ScreenObjId == "") {
                        dl += tabCnt;
                    } else {
                        dl += tabCnt + rowEmptyFirst + rowCnt + colEmpty + objCnt + objElement;
                    }
                }

                prevScreenObjId = element.ScreenObjId;
                prevTabId = element.ScreenTabId;
                prevRowGrp = element.RowCssClass;
                prevColGrp = element.ColCssClass;

            } catch (e) { }
        });
        tabLink = '<div class="tabLinkSec">' + tabItem + '<button title="ADD NEW TAB" onclick="AddTabToHeaderList(this); return false;" class="AddTabBtn tabLinkBtn">ADD NEW TAB</button><div style="clear:both;"></div></div>';
        myCntList.push(tabLink + dl + (prevScreenObjId == "" ? "" : '</div></div></div>' + colEmpty + '</div></div>') + rowEmptyLast + '</div></div>');

    } else if (screenType == "I2") {
        $.each(columnlist, function (index, element) {
            columnHeight = effectiveHeight(element);
            if (element.RequiredValid == "Y") {
                fieldvalidate = "<span class='Mandatory'>*</span>";
            } else {
                fieldvalidate = "";
            }
            if (element.MasterTable == "Y" || !element.MasterTable) {
                try {
                    rowNum = CheckPosition(element.RowCssClass.replace('rg-', ''));
                    colNum = CheckPosition(element.ColCssClass.replace('rc-', ''));
                    rowClass = "row_" + rowNum;
                    colClass = "col_" + colNum;
                    rowEmptyCnt = '<div class="r-tr"><div class="r-td col_12"><div class="screen-tabfolder"><div class="r-table"></div></div></div></div>';
                    rowEmptyLastCnt = '<div class="r-tr"><div class="r-td col_12"><div class="screen-tabfolder"><div class="r-table"></div></div></div></div>';
                    rowEmptyFirst = '<div class="r-table row_12 rowContainer rowEmpty first">' + rowEmptyCnt + '</div>';
                    rowEmptyLast = '<div class="r-table row_12 rowContainer rowEmpty last">' + rowEmptyLastCnt + '</div><div class="folderBtnSection"></div>';
                    colEmpty = '<div class="r-td rcEmpty"><div class="screen-tabfolder"><div class="r-table"></div></div></div>';

                    tabCnt = '<div class="tabpanel" tabid="' + element.ScreenTabId + '" tabtext="' + element.TabFolderName + '"><div class="tabname"><h3>' + element.TabFolderName + '</h3><button class="AddFolderItemBtn" onclick="AddFolderItem(this); return false;" title="Add New Folder Item">ADD NEW ITEM</button><div style="clear:both;"></div></div><div class="colCnt">';
                    rowCnt = '<div class="r-table ' + rowClass + ' rowContainer"><div class="r-tr">';
                    objCnt = '<div class="r-td ' + colClass + '"><div class="screen-tabfolder"><div class="r-table">';
                    if (columnHeight != "") {
                        objElement = '<div class="r-tr"><div class="drgItem" screenObjId="' + element.ScreenObjId + '" style="height: ' + columnHeight + 'px"><div class="drgItemCnt"><a title="' + element.ColumnHeader + '">' + element.ColumnHeader + fieldvalidate + '</a><button class="col_delete" onclick="DelFolderItem(this); return false;" title="delete"></button></div></div></div>';
                    } else {
                        objElement = '<div class="r-tr"><div class="drgItem" screenObjId="' + element.ScreenObjId + '"><div class="drgItemCnt"><a title="' + element.ColumnHeader + '">' + element.ColumnHeader + fieldvalidate + '</a><button class="col_delete" onclick="DelFolderItem(this); return false;" title="delete"></button></div></div></div>';
                    }
                    if (prevTabId == "") {
                        tabItem += '<a href="#" tabid="' + element.ScreenTabId + '" onclick="activateTab(this);">' + element.TabFolderName + '<button class="delTabLink" onclick="DelTab(this); return false;" title="delete"></button></a>';

                        if (element.ScreenObjId == "") {
                            dl += tabCnt;
                        } else {
                            dl += tabCnt + rowEmptyFirst + rowCnt + colEmpty + objCnt + objElement;
                        }
                    } else if (element.ScreenTabId == prevTabId) {
                        if (prevRowGrp == '') {
                            dl += rowCnt + colEmpty + objCnt + objElement;
                        } else if (prevRowGrp != '' && element.RowCssClass != prevRowGrp) {
                            dl += '</div></div></div>' + colEmpty + '</div></div>' + rowCnt + colEmpty + objCnt + objElement;
                        } else {
                            if (element.ColCssClass != prevColGrp) {
                                if (prevColGrp.indexOf("12") >= 0 || element.NewGroupRow == "Y") { //If previous item row group is ended with 12 or new row group is checked for current item
                                    dl += '</div></div></div>' + colEmpty + '</div></div>' + rowCnt + colEmpty + objCnt + objElement;
                                } else {
                                    dl += '</div></div></div>' + colEmpty + objCnt + objElement;
                                }
                            } else {
                                if (element.NewGroupRow == "Y") {
                                    dl += '</div></div></div>' + colEmpty + '</div></div>' + rowCnt + colEmpty + objCnt + objElement;
                                } else {
                                    dl += objElement;
                                }
                            }
                        }
                    } else {
                        tabItem += '<a href="#" tabid="' + element.ScreenTabId + '" onclick="activateTab(this);">' + element.TabFolderName + '<button class="delTabLink" onclick="DelTab(this); return false;" title="delete"></button></a>';

                        if (prevScreenObjId == "") {
                            dl += rowEmptyLast + '</div></div>';
                        } else {
                            dl += '</div></div></div>' + colEmpty + '</div></div>' + rowEmptyLast + '</div></div>';
                        }

                        if (element.ScreenObjId == "") {
                            dl += tabCnt;
                        } else {
                            dl += tabCnt + rowEmptyFirst + rowCnt + colEmpty + objCnt + objElement;
                        }
                    }

                    prevScreenObjId = element.ScreenObjId;
                    prevTabId = element.ScreenTabId;
                    prevRowGrp = element.RowCssClass;
                    prevColGrp = element.ColCssClass;

                } catch (e) { }
            } else {
                try {
                    if (prevTabId != element.ScreenTabId) {
                        tabItem += '<a href="#" tabid="' + element.ScreenTabId + '" onclick="activateTab(this);">' + element.TabFolderName + '<button class="delTabLink" onclick="DelTab(this); return false;" title="delete"></button></a>';
                    }
                    grdDrop = "<td class='dropZoneTd'><div class='GrdOuter'></div></td>"
                    grdOuterDrp = "<div class='GrdOuter dropZoneEmpty'></div>";
                    grdOuterCnt = "<div class='GrdOuter'>";
                    grdAdd = "<button class='AddGrdItemBtn' title='Add New Grid Item' onclick='AddGrdItem(); return false;'>ADD NEW ITEM</button>"
                    if (columnHeight != "") {
                        grdInnerCnt = "<div class='GrdInner' gridGrpCd = '" + element.GridGrpCd + "' screenObjId='" + element.ScreenObjId + "' style='height: " + columnHeight + "px'><span>" + element.ColumnHeader + fieldvalidate + "</span><button class='col_delete' onclick='DelGridItem(this); return false;' title='delete'></button></div>";
                    } else {
                        grdInnerCnt = "<div class='GrdInner' gridGrpCd = '" + element.GridGrpCd + "' screenObjId='" + element.ScreenObjId + "'><span>" + element.ColumnHeader + fieldvalidate + "</span><button class='col_delete' onclick='DelGridItem(this); return false;' title='delete'></button></div>";
                    }
                    if (prevGridGrp == "") {
                        grd += '<div class="grdTable" tabid="' + element.ScreenTabId + '"><div class="grdName"><h3>' + gridTitle + '</h3>' + grdAdd + '<div style="clear:both;"></div></div><table><tr>' + grdDrop + '<td>' + grdOuterDrp + grdOuterCnt + grdInnerCnt;
                        // The following is WIP ...
                        if (element.ScreenTabId == $(".tabpanel").attr('tabid')) {
                        }
                    } else {
                        if (element.GridGrpCd == "I") {
                            grd += grdInnerCnt;
                        } else if (element.GridGrpCd == "Y") {
                            grd += '</div>' + grdOuterDrp + grdOuterCnt + grdInnerCnt;
                        } else {
                            grd += '</div>' + grdOuterDrp + '</td>' + grdDrop + '<td>' + grdOuterDrp + grdOuterCnt + grdInnerCnt;
                        }
                    }
                    prevGridGrp = element.GridGrpCd;
                    prevTabId = element.ScreenTabId;
                } catch (e) { }
            }
        });
        tabLink = '<div class="tabLinkSec">' + tabItem + '<button title="ADD NEW TAB" onclick="AddTabToHeaderList(this); return false;" class="AddTabBtn tabLinkBtn">ADD NEW TAB</button><div style="clear:both;"></div></div>';
        myCntList.push(tabLink + dl + (prevScreenObjId == "" ? "" : '</div></div></div>' + colEmpty + '</div></div>') + rowEmptyLast + '</div></div>' + grd + '</div>' + grdOuterDrp + '</td>' + grdDrop + /*grdAdd +*/ '</tr></table></div>');

    } else {
        $.each(columnlist, function (index, element) {
            columnHeight = effectiveHeight(element);
            try {
                if (element.RequiredValid == "Y") {
                    fieldvalidate = "<span class='Mandatory'>*</span>";
                } else {
                    fieldvalidate = "";
                }
                grdDrop = "<td class='dropZoneTd'><div class='GrdOuter'></div></td>"
                grdOuterDrp = "<div class='GrdOuter dropZoneEmpty'></div>";
                grdOuterCnt = "<div class='GrdOuter'>";
                grdAdd = "<button class='AddGrdItemBtn' title='Add New Grid Item' onclick='AddGrdItem(); return false;'>ADD NEW ITEM</button>"
                if (columnHeight != "") {
                    grdInnerCnt = "<div class='GrdInner' gridGrpCd = '" + element.GridGrpCd + "' screenObjId='" + element.ScreenObjId + "' style='height: " + columnHeight + "px'><span>" + element.ColumnHeader + fieldvalidate + "</span><button class='col_delete' onclick='DelGridItem(this); return false;' title='delete'></button></div>";
                } else {
                    grdInnerCnt = "<div class='GrdInner' gridGrpCd = '" + element.GridGrpCd + "' screenObjId='" + element.ScreenObjId + "'><span>" + element.ColumnHeader + fieldvalidate + "</span><button class='col_delete' onclick='DelGridItem(this); return false;' title='delete'></button></div>";
                }
                if (prevGridGrp == "") {
                    grd += '<div class="grdTable" tabid="' + element.ScreenTabId + '"><div class="grdName"><h3>' + gridTitle + '</h3>' + grdAdd + '<div style="clear:both;"></div></div><table><tr>' + grdDrop + '<td>' + grdOuterDrp + grdOuterCnt + grdInnerCnt;
                }
                else {
                    if (element.GridGrpCd == "I") {
                        grd += grdInnerCnt;
                    } else if (element.GridGrpCd == "Y") {
                        grd += '</div>' + grdOuterDrp + grdOuterCnt + grdInnerCnt;
                    } else {
                        grd += '</div>' + grdOuterDrp + '</td>' + grdDrop + '<td>' + grdOuterDrp + grdOuterCnt + grdInnerCnt;
                    }
                }
                prevGridGrp = element.GridGrpCd;
            } catch (e) { }
        });

        myCntList.push(grd + '</div>' + grdOuterDrp + '</td>' + grdDrop + '</tr></table></div>');
    }

    dl = ""; grd = "";

    if (myCntList.length > 0) {
        return myCntList.join('');
    }

    return null;
};

function effectiveHeight(element) {
    var columnHeight = "";
    if (element.ColumnHeight != "" || element.ColumnHeight != null) {
        if (element.DisplayMode == "StarRating" || element.DisplayMode == "ProgressBar") { columnHeight = ""; }
        else if (element.DisplayMode == "Document") { columnHeight = (element.ColumnHeight * 29) + 29 + 22; }
        else if (element.DisplayMode == "ListBox") { columnHeight = (element.ColumnHeight * 16) + 7; }
        else if (element.DisplayMode == "DataGridLink") { columnHeight = element.ColumnHeight * 24; }
        else { columnHeight = element.ColumnHeight; }
    }
    return columnHeight;
}


function ShowColListLayout(columnList) {
    var layoutFn = function (columnList) {
        var result = buildColTree(columnList);
        $("#columnMap").html("");
        $("#columnMap").append(result);
        ApplyColDrgDrp();
        FocusCurrentScreenObj(null, null); // no focus on load
        if ($('.pageListCnt').css('display') != 'none') { $('#tabProLink, #objProLink').css('display', 'none'); }
    }
    if (columnList) layoutFn(columnList);
    else GetScreenObjList(layoutFn);
}

function ShowScreenLayout(columnList) {
    var layoutFn = function (columnList) {
        var colCntResult = buildColCnt(columnList);
        $("#columnMapCnt").html("");
        $("#columnMapCnt").append(colCntResult);
        ApplyColDrgDrp();
        ShowTabHor();
        FocusCurrentScreenObj(null, null); // no focus on load 
        if ($('a.active').length > 0 && $('#columnMapCnt').css('display') == 'block') { FocusCurrentScreenTab($('a.active')); };
        if ($('#columnMapCnt').css('display') != 'none') { $('#tabProLink, #objProLink').css('display', 'inline-block'); }
    }
    if (columnList) layoutFn(columnList);
    else GetScreenObjList(layoutFn);
}

function ShowColMap(columnList) {
    if (columnList) {
        programName = columnList[0].ProgramName;
    }
    ShowColListLayout(columnList);
    ShowScreenLayout(null);

    $('#prevScreen').attr('target', '_blank').attr('href', programName + '.aspx?typ=N');
}

function ApplyColDrgDrp() {
    var ScreenTabId;
    var TabFolderOrder;
    var TabFolderName;
    var TabPrevIndex;
    var ScreenObjId;
    var ColumnHeader;
    var ScreenTabPrevId
    var SysId;

    var sortableTab = $('#columnMap').sortable({
        forcePlaceholderSize: true,
        connectWith: ".tabContainer",
        helper: 'clone',
        opacity: .6,
        placeholder: 'placeholder',
        revert: 50,
        tolerance: 'pointer',
        delay: 200,
        //Fires when the item is dragged to a new location.
        stop: function (event, ui) {
            ScreenTabId = $(ui.item).attr("tabId");
            TabFolderName = $(ui.item).attr("tabText");
            if ($(ui.item).prevAll().length * 10 <= TabPrevIndex) {
                TabFolderOrder = $(ui.item).prevAll().length * 10 + 1;
            } else {
                TabFolderOrder = $(ui.item).prevAll().length * 10 + 11;
            }
            SysId = $('.r-sysid option:selected').attr('value');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "AdminWs.asmx/WrUpdScreenTab",
                data: $.toJSON({ ScreenTabId: ScreenTabId, TabFolderOrder: TabFolderOrder, TabFolderName: TabFolderName, SysId: SysId }),

                error: function (xhr, textStatus, errorThrown) {
                    PopDialog('images/error.gif', 'Update Failed' + errorThrown, ''); return false;
                },
                success: function (mr, textStatus, xhr) {
                    if (mr.d.status == "") {
                        ApplyColDrgDrp();
                        FocusCurrentScreenTab($(ui.item).closest('.tabContainer').find('.tabfolderName').parent());
                    } else { PopDialog('images/error.gif', 'Update Error' + mr.d.errorMsg, ''); return false; };
                }
            });
        },
        //Fires when the item is dragged.
        sort: function (event, ui) {
            TabFolderName = $(ui.item).attr('tabText');
            TabPrevIndex = $(ui.item).prevAll().length * 10;
        }
    });

    var sortableColObj = $('#columnMap .tabCnt').sortable({
        forcePlaceholderSize: true,
        connectWith: ".tabCnt",
        helper: 'clone',
        opacity: .6,
        placeholder: 'placeholder',
        revert: 50,
        tolerance: 'pointer',
        delay: 200,
        //Fires when the item is dragged to a new location.
        stop: function (event, ui) {
            var PScreenObjId = "";
            var ScreenObjId = $(ui.item).attr("screenobjid");
            var ScreenTabId = $(ui.item).parent().parent().attr("tabId");
            if ($(ui.item).attr("MasterTb") == "Y") {
                var GridItmAfter = $(ui.item).prevAll(".colItem[mastertb='N']").length;
                if (GridItmAfter > 0) {
                    PopDialog('images/error.gif', 'Please do not put the tab folder item between or after grid item.', ''); return false;
                }
                if ($(ui.item).prev(".colItem[mastertb='Y']").length > 0) {
                    //have previous folder item in the same tab
                    PScreenObjId = $(ui.item).prev(".colItem[mastertb='Y']").attr("screenobjid");
                } else if ($(ui.item).prev(".colItem[mastertb='Y']").length <= 0 && $(ui.item).closest('.tabContainer').prevAll('.tabContainer:has(.colItem[mastertb="Y"])').find('.colItem[mastertb="Y"]').length > 0) {
                    //no previouse folder item in the same tab, but have it in previous tabs
                    PScreenObjId = $(ui.item).closest('.tabContainer').prevAll('.tabContainer:has(.colItem[mastertb="Y"])').last().find('.colItem[mastertb="Y"]').last().attr("screenobjid");
                } else {
                    PScreenObjId = "";
                }
            } else {
                var FolderItmAfter = $(ui.item).nextAll(".colItem[mastertb='Y']").length;
                if (FolderItmAfter > 0) {
                    PopDialog('images/error.gif', 'Please do not put the grid item between or before folder item.', ''); return false;
                }
                if (($(ui.item).closest(".tabContainer").prevAll().find(".colItem[mastertb='N']").length > 0 || $(ui.item).closest(".tabContainer").nextAll().find(".colItem[mastertb='N']").length > 0) && $(ui.item).parent().find(".colItem[mastertb='N']").length == 1) {
                    PopDialog('images/warning.gif', 'Warning: If splitting grid items into different tabs, the grid will show up in the first tab with grid item inside. Please group all grid items into one and only one tab.', '');
                }

                if ($(ui.item).prev(".colItem[mastertb='N']").length > 0) {
                    //have previous grid item in the same tab
                    PScreenObjId = $(ui.item).prev(".colItem[mastertb='N']").attr("screenobjid");
                } else if ($(ui.item).prev(".colItem[mastertb='N']").length <= 0 && $(ui.item).closest('.tabContainer').prevAll('.tabContainer:has(.colItem[mastertb="N"])').find('.colItem[mastertb="N"]').length > 0) {
                    //no previouse grid item in the same tab, but have it in previous tabs
                    PScreenObjId = $(ui.item).closest('.tabContainer').prevAll('.tabContainer:has(.colItem[mastertb="N"])').last().find('.colItem[mastertb="N"]').last().attr("screenobjid");
                } else {
                    if ($("#columnMap").find(".colItem[mastertb='Y']").length > 0) {
                        //no grid item in any tabs, but have folder items
                        PScreenObjId = $("#columnMap").find(".colItem[mastertb='Y']").last().attr("screenobjid");
                    } else {
                        PScreenObjId = "";
                    }
                }
            }

            var SysId = $('.r-sysid option:selected').attr('value');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "AdminWs.asmx/WrUpdScreenObj",
                data: $.toJSON({ ScreenObjId: ScreenObjId, PScreenObjId: PScreenObjId, TabFolderId: ScreenTabId, ColumnHeader: "", SysId: SysId }),
                error: function (xhr, textStatus, errorThrown) {
                    PopDialog('images/error.gif', 'Update Failed' + errorThrown, ''); return false;
                },
                success: function (mr, textStatus, xhr) {
                    if (mr.d.status == "") {
                        ApplyColDrgDrp();
                        FocusCurrentScreenObj($(ui.item), $(ui.item));
                    } else { PopDialog('images/error.gif', 'Update Error' + mr.d.errorMsg, ''); return false; };
                }
            });
        },
        //Fires when the item is dragged.
        sort: function (event, ui) {
            ColumnHeader = $(ui.item).attr("columnheader");
        }
    });


    /*----- drag and drop function for Tab-Folder elements -----*/
    var OuterLgh = "", grdInnerLgh = "", sortableFolderCnt = {}, removeTd = false;

    var sortableFolderStartFn = function (event, ui) {  //start
        rowLgh = $(this).closest('.rowContainer').find('> .r-tr > .r-td').length;
        colLgh = $(this).find('.r-tr').length;
    }

    var sortableFolderSortFn = function (event, ui) {  //sort
        if (rowLgh <= 3) {
            if (colLgh <= 3) {
                $(ui.item).closest('.r-td').addClass('oriElement');
                $(ui.item).closest('.rowContainer').addClass("toBeDelRow");
            }
        } else {
            if (colLgh <= 3) {
                //$(ui.item).closest('.r-td').prev('.rcEmpty').remove();
                //$(ui.item).closest('.r-td').next('.rcEmpty').remove();
                //$(ui.item).closest('.r-td').attr('class', 'r-td rcEmpty');
                $(ui.item).closest('.r-td').addClass('oriElement');
            } else {
                //do the default
            }
        }

        $('#columnMapCnt .rcEmpty .screen-tabfolder .r-table').addClass(' placeHolderWidth');
    }

    var sortableFolderStopFn = function (event, ui) { //stop
        $('#columnMapCnt .rcEmpty .screen-tabfolder .r-table').removeClass('placeHolderWidth');
        var rowEmptyCnt = '<div class="r-tr"><div class="r-td col_12"><div class="screen-tabfolder"><div class="r-table"></div></div></div></div>';
        var rowEmptyLastCnt = '<div class="r-tr"><div class="r-td col_12"><div class="screen-tabfolder"><div class="r-table"></div></div></div></div>';
        var rowEmptyFirst = '<div class="r-table row_12 rowContainer rowEmpty first">' + rowEmptyCnt + '</div>';
        var rowEmptyLast = '<div class="r-table row_12 rowContainer rowEmpty last">' + rowEmptyLastCnt + '</div><div class="folderBtnSection"></div>';
        var colEmpty = '<div class="r-td rcEmpty"><div class="screen-tabfolder"><div class="r-table ui-sortable"></div></div></div>';
        var myColCount = $(this).closest('.r-td').attr('class').replace('r-td col_', '');
        var colCount = RowColumnUsed(ui.item).colCount;
        //var colCount = 0;

        //$(ui.item).closest('.rowContainer > .r-tr').find('.r-td:not(.rcEmpty)').each(function (i, obj) {
        //    colCount += parseInt($(obj).attr('class').replace('r-td col_', ''));
        //});
        if (colCount >= 12 && $(ui.item).closest('.screen-tabfolder').parent().hasClass('rcEmpty')) {
            $('.oriElement').removeClass('oriElement');
            PopDialog('images/error.gif', 'Please remove one object or adjust the column size of one of the objects and try again.', ''); return false;
            return false;
        }

        if (!($(ui.item).closest('.rowContainer').hasClass('toBeDelRow'))) {
            $('.toBeDelRow').remove();
        }

        if ($(ui.item).closest('.rowContainer ').hasClass('rowEmpty')) {
            if ($(ui.item).closest('.rowContainer ').hasClass('first')) {
                $(ui.item).closest('.rowContainer').removeClass('rowEmpty').removeClass('first');
                $(rowEmptyFirst).insertBefore($(ui.item).closest('.rowContainer')).find('.screen-tabfolder > .r-table').sortable(sortableFolderCnt);
                $(colEmpty).insertBefore($(ui.item).closest('.screen-tabfolder').parent()).find('.screen-tabfolder > .r-table').sortable(sortableFolderCnt);
                $(colEmpty).insertAfter($(ui.item).closest('.screen-tabfolder').parent()).find('.screen-tabfolder > .r-table').sortable(sortableFolderCnt);
            } else {
                $(ui.item).closest('.colCnt').find('.folderBtnSection').remove();
                $(ui.item).closest('.rowContainer').removeClass('rowEmpty').removeClass('last');
                $(rowEmptyLast).insertAfter($(ui.item).closest('.rowContainer')).find('.screen-tabfolder > .r-table').sortable(sortableFolderCnt);
                $(colEmpty).insertBefore($(ui.item).closest('.screen-tabfolder').parent()).find('.screen-tabfolder > .r-table').sortable(sortableFolderCnt);
                $(colEmpty).insertAfter($(ui.item).closest('.screen-tabfolder').parent()).find('.screen-tabfolder > .r-table').sortable(sortableFolderCnt);
            }
        } else {
            if ($(ui.item).closest('.screen-tabfolder').parent().hasClass('rcEmpty')) {
                $(ui.item).closest('.screen-tabfolder').parent().attr('class', 'r-td col_1');
                $(colEmpty).insertBefore($(ui.item).closest('.screen-tabfolder').parent()).find('.screen-tabfolder > .r-table').sortable(sortableFolderCnt);
                $(colEmpty).insertAfter($(ui.item).closest('.screen-tabfolder').parent()).find('.screen-tabfolder > .r-table').sortable(sortableFolderCnt);
            } else {
                //do default
            }
        }

        var folderList = GetFolderObjLayout();

        SysId = $('.r-sysid option:selected').attr('value');
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "AdminWs.asmx/WrUpdTabFolderLayout",
            data: $.toJSON({ ScreenObjList: folderList, SysId: SysId }),
            error: function (xhr, textStatus, errorThrown) {
                PopDialog('images/error.gif', 'Update Failed' + errorThrown, ''); return false;
            },
            success: function (mr, textStatus, xhr) {
                if (mr.d.status == "") {
                    $('.oriElement').prev('.rcEmpty').remove();
                    $('.oriElement').next('.rcEmpty').remove();
                    $('.oriElement').attr('class', 'r-td rcEmpty');
                    ApplyColDrgDrp();
                    FocusCurrentScreenObj($(ui.item).find('.drgItem'), $(ui.item).find('.drgItem'));
                } else { PopDialog('images/error.gif', 'Update Error' + mr.d.errorMsg, ''); return false; };
            }
        });
    }

    sortableFolderCnt = {
        forcePlaceholderSize: true,
        connectWith: '#columnMapCnt .screen-tabfolder > .r-table',
        helper: 'clone',
        opacity: .8,
        revert: 50,
        tolerance: 'pointer',
        cursor: "move",
        cursorAt: { left: 5 },
        dropOnEmpty: true,
        delay: 150,
        //Only fires once each time sorting begins
        start: sortableFolderStartFn,

        //fires when the item is dragged.
        sort: sortableFolderSortFn,

        //fires when the item is dragged to a new location.
        stop: sortableFolderStopFn
    };

    var sortableFolder = $('#columnMapCnt .screen-tabfolder > .r-table').sortable(sortableFolderCnt);
    /*----- end of drag and drop function for Tab-Folder elements -----*/


    /*----- drag and drop function for the grid elements -----*/
    var grdOuterLgh = "", grdInnerLgh = "", sortableGrdCnt = {}, oldSideItmId = "", oldSideItmCd = "";

    var sortableGrdStartFn = function (event, ui) {
        grdOuterLgh = $(this).parent().find('.GrdOuter').length;
        grdInnerLgh = $(this).find('.GrdInner').length;
    }

    var sortableGrdSortFn = function (event, ui) {
        if (grdOuterLgh <= 3) {
            if (grdInnerLgh <= 3) {
                $(ui.item).parent().prev('.dropZoneEmpty').remove();
                $(ui.item).parent().next('.dropZoneEmpty').remove();
                $(ui.item).parent().parent().prev('.dropZoneTd').remove();
                $(ui.item).parent().parent().next('.dropZoneTd').remove();
                $(ui.item).parent().parent().addClass('dropZoneTd');
            } else {
                if ($(ui.item).index() <= 0) {
                    oldSideItmId = $(ui.item).next('.GrdInner').attr('screenObjId');
                    oldSideItmCd = "N";
                }
            }
        } else {
            if (grdInnerLgh <= 3) {
                $(ui.item).parent().prev('.dropZoneEmpty').remove();
                $(ui.item).parent().next('.dropZoneEmpty').remove();
                $(ui.item).parent().addClass('dropZoneEmpty');
            } else {
                if ($(ui.item).index() <= 0) {
                    oldSideItmId = $(ui.item).next('.GrdInner').attr('screenObjId');
                    oldSideItmCd = "N";
                }
            }
        }
        var GridItemPosition = "";
    }

    var sortableGrdStopFn = function (event, ui) {
        var dropZoneTd = '<td class="dropZoneTd"><div class="GrdOuter ui-sortable"></div></td>';
        var dropZoneEmpty = '<div class="GrdOuter dropZoneEmpty ui-sortable"></div>';
        var gridGrpCd = '', nextGridGrpCd = '', grdObjId = '', nextGrdObjId = '', prevGrdObjId = '';
        grdObjId = $(ui.item).attr('screenObjId');
        if ($(ui.item).parent().parent().hasClass('dropZoneTd')) {
            gridGrpCd = 'N';
            if ($(ui.item).closest('.dropZoneTd').index() > 0) {
                prevGrdObjId = $(ui.item).closest('.dropZoneTd').prev('td').find('.GrdOuter:not(.dropZoneEmpty)').last().find('.GrdInner').last().attr('screenObjId');
            }
        } else {
            if ($(ui.item).parent().hasClass('dropZoneEmpty')) {
                if ($(ui.item).parent().index() > 0) {
                    gridGrpCd = 'Y';
                    prevGrdObjId = $(ui.item).closest('.GrdOuter').prev('.GrdOuter:not(.dropZoneEmpty)').find('.GrdInner').last().attr('screenObjId');
                } else {
                    if ($(ui.item).parent().index() > 2) {
                        gridGrpCd = 'Y';
                    } else {
                        gridGrpCd = 'N';
                    }
                    nextGridGrpCd = 'Y';
                    nextGrdObjId = $(ui.item).parent().next('.GrdOuter:not(.dropZoneEmpty )').find('.GrdInner').attr('screenObjId');
                    if ($(ui.item).closest('td').prev('.dropZoneTd').prev('td').index() > 0) {
                        prevGrdObjId = $(ui.item).closest('td').prev('.dropZoneTd').prev('td').find('.GrdOuter:not(.dropZoneEmpty)').last().find('.GrdInner').last().attr('screenObjId');
                    }
                }
            } else {
                if ($(ui.item).index() > 0) {
                    gridGrpCd = 'I';
                    prevGrdObjId = $(ui.item).prev('.GrdInner').attr('screenObjId');
                } else {
                    if ($(ui.item).parent().index() > 2) {
                        gridGrpCd = 'Y';
                    } else {
                        gridGrpCd = 'N';
                    }
                    nextGridGrpCd = 'I';
                    nextGrdObjId = $(ui.item).next('.GrdInner').attr('screenObjId');
                    prevGrdObjId = $(ui.item).parent().prevAll('.GrdOuter:not(.dropZoneEmpty)').last().find('.GrdInner').last().attr('screenobjid');
                    if (!prevGrdObjId) prevGrdObjId = $(ui.item).closest('td').prev('.dropZoneTd').prev('td').find('.GrdOuter:not(.dropZoneEmpty)').last().find('.GrdInner').last().attr('screenobjid');
                }
            }
        }

        sysid = $('.r-sysid option:selected').attr('value');
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "AdminWs.asmx/WrUpdGridLayout",
            data: $.toJSON({ grdObjId: grdObjId, gridGrpCd: gridGrpCd, prevGrdObjId: prevGrdObjId || '', nextGrdObjId: nextGrdObjId, nextGridGrpCd: nextGridGrpCd, oldSideItmId: oldSideItmId, oldSideItmCd: oldSideItmCd, SysId: sysid }),
            error: function (xhr, textstatus, errorthrown) {
                PopDialog('images/error.gif', 'Update Failed' + errorThrown, ''); return false;
            },
            success: function (mr, textstatus, xhr) {
                if (mr.d.status == "") {
                    if ($(ui.item).parent().parent().hasClass('dropZoneTd')) {
                        $(dropZoneEmpty).insertBefore($(ui.item).parent()).sortable(sortableGrdCnt);
                        $(dropZoneEmpty).insertAfter($(ui.item).parent()).sortable(sortableGrdCnt);
                        $(dropZoneTd).insertBefore($(ui.item).parent().parent()).find('.GrdOuter').sortable(sortableGrdCnt);;
                        $(dropZoneTd).insertAfter($(ui.item).parent().parent()).find('.GrdOuter').sortable(sortableGrdCnt);;
                        $(ui.item).parent().parent().removeClass('dropZoneTd');
                    } else {
                        if ($(ui.item).parent().hasClass('dropZoneEmpty')) {
                            $(dropZoneEmpty).insertBefore($(ui.item).parent()).sortable(sortableGrdCnt);
                            $(dropZoneEmpty).insertAfter($(ui.item).parent()).sortable(sortableGrdCnt);
                            $(ui.item).parent().removeClass('dropZoneEmpty');
                        } else { }
                    }
                    oldSideItmId = ""; oldSideItmCd = "";
                    ApplyColDrgDrp();
                    FocusCurrentScreenObj($(ui.item), $(ui.item));
                } else { PopDialog('images/error.gif', 'Update Error' + mr.d.errorMsg, ''); return false; };
            }
        });

    }

    sortableGrdCnt = {
        connectWith: '.grdTable .GrdOuter',
        helper: 'clone',
        opacity: .8,
        revert: 50,
        tolerance: 'pointer',
        cursor: "move",
        cursorAt: { left: 5 },
        dropOnEmpty: true,
        delay: 150,

        //Only fires once each time sorting begins
        start: sortableGrdStartFn,

        //fires when the item is dragged.
        sort: sortableGrdSortFn,

        //fires when the item is dragged to a new location.
        stop: sortableGrdStopFn
    }

    var sortableGrd = $('.grdTable .GrdOuter').sortable(sortableGrdCnt);
    /*----- end of drag and drop function for the grid elements -----*/


    //resize function for grid elements
    $(".grdTable .GrdOuter > .GrdInner").resizable({
        autoHide: true,
        minHeight: 29,
        //handles: 'se',
        handles: 's',
        resize: function (event, ui) {
        },
        stop: function (event, ui) {
            var sysid = $('.r-sysid option:selected').attr('value');
            var grdObjId = $(ui.element).attr('screenobjid');
            var grdWidth = ui.size.width.toString();
            var grdHeight = ui.size.height.toString();
            //var gridWidth = 300;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "AdminWs.asmx/WrResizeGridLayout",
                data: $.toJSON({ grdObjId: grdObjId, grdItemWidth: grdWidth, grdItemHeight: grdHeight, SysId: sysid }),
                error: function (xhr, textstatus, errorthrown) {
                    PopDialog('images/error.gif', 'Update Failed' + errorThrown, ''); return false;
                },
                success: function (mr, textstatus, xhr) {
                    if (mr.d.status == "") {
                        ApplyColDrgDrp();
                        FocusCurrentScreenObj($(ui.element), $(ui.element));
                    } else { PopDialog('images/error.gif', 'Update Error' + mr.d.errorMsg, ''); return false; };
                }
            });
        }
    });

    //resize function for Tab-Folder container

    $('#columnMapCnt .rowContainer').resizable({
        autoHide: true,
        //grid: [x, 0],
        handles: "e",
        start: function (event, ui) {
            var maxWidth = $(this).closest('.colCnt').width();
            var oneRowWidth = $('.pageCnt').width() * 0.0833;
            $(this).resizable("option", "grid", [oneRowWidth, 0]);
            $(this).resizable("option", "maxWidth", maxWidth);
        },
        stop: function (event, ui) {
            var rowClassName = $(this).attr('class').split(' ');
            var classNum = "";
            //get the row classname
            for (var i = 0; i < rowClassName.length; i++) {
                if (rowClassName[i].indexOf('row_') > -1) {
                    classNum = rowClassName[i]; break;
                }
            }
            $(this).removeClass(classNum);
            var oneRowWidth = $('.pageCnt').width() * 0.0833;
            var rowNum = Math.round($(this).width() / oneRowWidth);
            $(this).addClass('row_' + rowNum);

            var folderList = GetFolderObjLayout();
            var SysId = $('.r-sysid option:selected').attr('value');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "AdminWs.asmx/WrUpdTabFolderLayout",
                data: $.toJSON({ ScreenObjList: folderList, SysId: SysId }),
                error: function (xhr, textStatus, errorThrown) {
                    PopDialog('images/error.gif', 'Update Failed' + errorThrown, ''); return false;
                },
                success: function (mr, textStatus, xhr) {
                    if (mr.d.status == "") {

                    } else { PopDialog('images/error.gif', 'Update Error' + mr.d.errorMsg, ''); return false; };
                }
            });
        }
    });

    ////resize function for Tab-Folder object
    //var elementWidth = 0, colNum = 0, dropzoneTotalWidth = 0;

    //$('#columnMapCnt .rowContainer:not(.rowEmpty) .r-td:not(.rcEmpty) .drgItem').each(function (i, e) {
    //    var rowWidth = $(this).closest('.rowContainer').width();
    //    var itemWidth = Math.floor((rowWidth / 12));
    //    var maxColWidth = rowWidth - (($(this).closest('.rowContainer').find('.r-td[class*="col_"]').length - 1) * itemWidth);
    //    $(e).resizable({
    //        autoHide: true,
    //        grid: [itemWidth, 28],
    //        maxWidth: maxColWidth,
    //        handles: "se",
    //        start: function (event, ui) {
    //        },
    //        resize: function (event, ui) {
    //            //$(this).closest('.r-td').css('width', 'auto');
    //        },
    //        stop: function (event, ui) {
    //            elementWidth = $(this).outerWidth();
    //            colNum = Math.round(elementWidth / itemWidth);
    //            $(this).closest('.r-td').attr('class', 'r-td col_' + colNum);
    //            $(this).css('width', 'auto');

    //        }
    //    });
    //});
    if ($('#columnMapCnt').is(":visible")) {
        ApplyEleResize();
    }

    //select the folder item
    $('#columnMapCnt .drgItem').click(function () {
        FocusCurrentScreenObj(this, $(this).parent());
        //$('#columnMapCnt .drgItem, #columnMapCnt .GrdInner').removeClass('active');
        //$(this).addClass("active");
        //$('.col_delete').hide()
        //$(this).parent().find('.col_delete').show();
    });

    //select the grid item
    $('#columnMapCnt .GrdInner').click(function () {
        FocusCurrentScreenObj(this, this);
        //$('#columnMapCnt .drgItem, #columnMapCnt .GrdInner').removeClass('active');
        //$(this).addClass("active");
        //$('.col_delete').hide();
        //$(this).find('.col_delete').show();
    });

    $('#columnMap .col_delete, #columnMapCnt .col_delete').hover(function () {
        // this is used in two different layout, must try both ! gary 2015.11.23
        var screenobjid = $(this).parent().attr('screenobjid') || $(this).parent().parent().attr('screenobjid');
        $('.tbDeleteId').val(screenobjid);
    });

    $('#columnMap .tab_delete').hover(function () {
        $('.tbDeleteId').val($(this).parent().parent().attr('tabid'));
    });

    $('#columnMap .tabfolderName').click(function () {
        FocusCurrentScreenTab($(this).parent());
    });

    $('#columnMap .colItem a').click(function () {
        var item = $(this).parent();
        FocusCurrentScreenObj(item, item);
    });

    $('#columnMap .colItem a').hover(
        function () {
            $(this).parent().addClass('hover');
        },
        function () {
            $(this).parent().removeClass('hover');
        }
    );
}

function FocusCurrentScreenObj(ele, delEle) {
    // remove all 'active' in the 2D layout
    $('#columnMapCnt .drgItem, #columnMapCnt .GrdInner').removeClass('active');
    // remove all 'active' in the list layout
    $(".colItem").removeClass("active");
    // remove all 'active' in the tab object
    $(".tabFolder").removeClass("active");
    // remove all 'delete' icon 
    $('.col_delete,.tab_delete').hide()
    // disable tab property
    //$('#tabProLink').addClass(' disabled');
    //$('#tabProLink').css('background-color', '#90beE8 !important');
    // disable screen property
    $('#objProLink, #objPerLink, #objLblLink').addClass(' disabled');
    $('#objProLink, #objPerLink, #objLblLink').css('background-color', '#90beE8 !important');
    if ($(ele).length > 0) {
        // enable screen obj property
        $('#objProLink, #objPerLink, #objLblLink').removeClass('disabled');
        $('#objProLink, #objPerLink, #objLblLink').attr('style', '');
        // show element as active
        $(ele).addClass("active");
        // enable delete button
        $(delEle).find('.col_delete').show();
    }

    if ($('.pageListCnt').css('display') == 'table') {
        //$('#objProLink').css('display', 'none');
        if (ele != null) {
            $('.iFrameIdt').show();
            var screenObjId = ele.attr('screenobjid');
            $('#listIframe').attr('src', 'AdmScreenObj.aspx?typ=n&key=' + screenObjId);
            $('#navigateCntMsg').hide();
            $('#listIframe').show();
        } else {
            $('#listIframe').attr('src', '');
            $('#navigateCntMsg').show();
            $('#listIframe').hide();
        }
    } else {
        //$('#objProLink').css('display', 'inline-block');
    }
}

function FocusCurrentScreenTab(ele) {
    // remove all 'active' in the 2D layout
    $('#columnMapCnt .drgItem, #columnMapCnt .GrdInner').removeClass('active');
    // remove all 'active' in the list layout
    $(".colItem").removeClass("active");
    // remove all 'active' in the tab object
    $(".tabFolder").removeClass("active");
    // remove all 'delete' icon 
    $('.col_delete,.tab_delete').hide()
    // disable screen property
    $('#objProLink, #objPerLink, #objLblLink').addClass(' disabled');
    $('#objProLink, #objPerLink, #objLblLink').css('background-color', '#90beE8!important');
    // disable tab property
    $('#tabProLink').addClass(' disabled');
    $('#tabProLink').css('background-color', '#90beE8!important');
    if ($(ele).length > 0) {
        // enable tab property
        $('#tabProLink').removeClass('disabled');
        $('#tabProLink').attr('style', '');
        // show element as active
        $(ele).addClass("active");
        // enable delete button
        $(ele).find('.tab_delete').css('display', 'block');
    }

    if ($('.pageListCnt').css('display') == 'table') {
        //$('#tabProLink').css('display', 'none');
        if (ele != null) {
            $('.iFrameIdt').show();
            var tabId = ele.parent('.tabContainer').attr('tabid');
            $('#listIframe').attr('src', 'AdmScreenTab.aspx?typ=n&key=' + tabId);
            $('#navigateCntMsg').hide();
            $('#listIframe').show();
        } else {
            $('#listIframe').attr('src', '');
            $('#navigateCntMsg').show();
            $('#listIframe').hide();
        }
    } else {
        //$('#tabProLink').css('display', 'inline-block');
    }
}

function RowColumnUsed(ele) {
    var colCount = 0;
    var colWidth = 0;
    $(ele).closest('.rowContainer > .r-tr').find('.r-td:not(.rcEmpty):not(.oriElement)').each(function (i, obj) {
        colCount += parseInt($(obj).attr('class').replace('r-td col_', ''));
        colWidth += $(obj).width();
    });
    return { colCount: colCount, colWidth: colWidth };
}

function GetFolderObjLayout() {
    //Get a list of updated tab-folder Item position properties
    var folderList = '', rowNum = 0, colNum = 0, prevRowNum = 0, prevColNum = 0, prevRowIndex = "", prevColIndex = "", itemId = "", rowPosition = "", colPosition = "";
    $('.tabpanel .rowContainer:not(.rowEmpty) .r-td:not(.rcEmpty) .drgItem').each(function (i, obj) {
        var rowClassName = $(this).closest('.rowContainer').attr('class').split(' ');
        var colClassName = $(this).closest('.r-td').attr('class').split(' ');
        var tabId = $(this).closest('.tabpanel').attr('tabid');
        var firstItemInRowScreenId = $(this).closest('.rowContainer').find('.drgItem').first().attr('screenobjid');
        var colHeight = $(this).attr('style') ? $(this).css('height').replace('px', '') : "";

        //get the row classname
        for (var i = 0; i < rowClassName.length; i++) {
            if (rowClassName[i].indexOf('row_') > -1) {
                rowNum = parseInt(rowClassName[i].replace('row_', '')); break;
            }
        }
        //get the column classname
        for (var i = 0; i < colClassName.length; i++) {
            if (colClassName[i].indexOf('col_') > -1) {
                colNum = parseInt(colClassName[i].replace('col_', '')); break;
            }
        }

        var curRowIndex = $(this).closest('.rowContainer').index('.rowContainer:not(.rowEmpty)');
        var curColIndex = $(this).closest('.r-td').index('.rowContainer > .r-tr >.r-td:not(.rcEmpty)');
        if (rowPosition == "") {
            rowPosition = "rg-1-" + rowNum;
            colPosition = "rc-1-" + colNum;
            prevRowNum += rowNum;
            prevColNum += colNum;
        } else if (rowPosition != "" && curRowIndex == prevRowIndex) {
            rowPosition = prevRowPosition;
            if (curColIndex == prevColIndex) {
                colPosition = prevColPosition;
            } else {
                var startCol = prevColNum + 1;
                var endCol = prevColNum + colNum;
                if (endCol > 12) { startCol = 1; endCol = 12; };
                colPosition = "rc-" + (startCol) + "-" + (endCol);
                prevColNum += colNum;
            }
        } else if (rowPosition != "" && curRowIndex != prevRowIndex) {
            if (prevRowNum == 12 || (prevRowNum + rowNum > 12)) {
                rowPosition = "rg-1-" + rowNum;
            } else {
                rowPosition = "rg-" + ((prevRowNum) + 1) + "-" + (prevRowNum + rowNum);
            }
            prevRowNum += rowNum;
            colPosition = "rc-1-" + colNum;
            prevColNum = colNum;
        }

        prevRowIndex = curRowIndex;
        prevColIndex = curColIndex;
        prevRowPosition = rowPosition;
        prevColPosition = colPosition;

        itemId = $(this).attr('screenobjid');
        folderList += ',{"ID": "' + itemId + '", "ROW": "' + rowPosition + '", "COL": "' + colPosition + '", "TABID": "' + tabId + '", "NEWROWGRP": "' + (firstItemInRowScreenId == itemId ? "Y" : "N") + '", "COLHEIGHT": "' + colHeight + '"}';

    });
    return folderList;

}

function ApplyEleResize() {
    var elementWidth = 0, colNum = 0, dropzoneTotalWidth = 0;

    $('#columnMapCnt .rowContainer:not(.rowEmpty) .r-td:not(.rcEmpty) .drgItem').each(function (i, e) {
        var stateParam = { stop: 0 };
        $(e).resizable({
            autoHide: true,
            handles: "se",
            ghost: true,
            helper: "ui-resizable-helper",
            start: function (event, ui) {
                var maxRowWidth = $(this).closest('.rowContainer').width();
                var myColCount = parseInt($(this).closest('.r-td').attr('class').replace('r-td col_', ''));
                var myWidth = ui.size.width;
                var oneColWidth = Math.floor(myWidth / myColCount);
                //var oneColWidth = Math.floor((maxRowWidth / 12)) - 1;
                var parentColUsed = RowColumnUsed(this);
                var parentColCnt = parentColUsed.colCount;
                var parentColWidth = parentColUsed.colWidth;
                var maxWidth = maxRowWidth - parentColWidth + myWidth;
                $(e).resizable("option", "minWidth", oneColWidth);
                $(e).resizable("option", "minHeight", ui.size.height + 1);
                $(e).resizable("option", "maxHeight", ui.size.height + 1);
                if (12 - parentColCnt > 0) {
                    $(e).resizable("option", "maxWidth", maxWidth);
                    $(e).resizable("option", "grid", [oneColWidth, 0]);
                }
                else {
                    $(e).resizable("option", "maxWidth", ui.size.width);
                }
                stateParam.oneColWidth = oneColWidth;
            },
            resize: function (event, ui) {
                stateParam.stop = stateParam.stop + 1;
            },
            stop: function (event, ui) {
                //TO DO: link to SP, Resize restriction 
                colNum = Math.round(ui.size.width / stateParam.oneColWidth);
                colNum = colNum < 1 ? 1 : (colNum > 12 ? 12 : colNum);
                $(this).closest('.r-td').attr('class', 'r-td col_' + colNum);
                $(this).css('width', 'auto');
                var folderList = GetFolderObjLayout();
                var SysId = $('.r-sysid option:selected').attr('value');
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: "AdminWs.asmx/WrUpdTabFolderLayout",
                    data: $.toJSON({ ScreenObjList: folderList, SysId: SysId }),
                    error: function (xhr, textStatus, errorThrown) {
                        PopDialog('images/error.gif', 'Update Failed' + errorThrown, ''); return false;
                    },
                    success: function (mr, textStatus, xhr) {
                        if (mr.d.status == "") {
                            ApplyColDrgDrp();
                            FocusCurrentScreenObj($(ui.element), $(ui.element));
                        } else { PopDialog('images/error.gif', 'Update Error' + mr.d.errorMsg, ''); return false; };
                    }
                });

            }
        });
    });
}


//Check Tab-Folder element position
function CheckPosition(ele) {
    if (ele.match(/^(1-1|2-2|3-3|4-4|5-5|6-6|7-7|8-8|9-9|10-10|11-11|12-12)$/)) {
        return 1;
    } else if (ele.match(/^(1-2|2-3|3-4|4-5|5-6|6-7|7-8|8-9|9-10|10-11|11-12)$/)) {
        return 2;
    } else if (ele.match(/^(1-3|2-4|3-5|4-6|5-7|6-8|7-9|8-10|9-11|10-12)$/)) {
        return 3;
    } else if (ele.match(/^(1-4|2-5|3-6|4-7|5-8|6-9|7-10|8-11|9-12)$/)) {
        return 4;
    } else if (ele.match(/^(1-5|2-6|3-7|4-8|5-9|6-10|7-11|8-12)$/)) {
        return 5;
    } else if (ele.match(/^(1-6|2-7|3-8|4-9|5-10|6-11|7-12)$/)) {
        return 6;
    } else if (ele.match(/^(1-7|2-8|3-9|4-10|5-11|6-12)$/)) {
        return 7;
    } else if (ele.match(/^(1-8|2-9|3-10|4-11|5-12)$/)) {
        return 8;
    } else if (ele.match(/^(1-9|2-10|3-11|4-12)$/)) {
        return 9;
    } else if (ele.match(/^(1-10|2-11|3-12)$/)) {
        return 10;
    } else if (ele.match(/^(1-11|2-12)$/)) {
        return 11;
    } else {
        return 12;
    }
}

//Add Tab Folder
function AddTab(ele) {
    var TabFolderOrder = ($('#columnMapCnt .tabpanel').length + 1) * 10;
    var SysId = $('.r-sysid option:selected').attr('value');
    var ScreenId = $('.tbScreenId').val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/WrAddScreenTab",
        data: $.toJSON({ TabFolderOrder: TabFolderOrder, ScreenId: ScreenId, SysId: SysId }),

        error: function (xhr, textStatus, errorThrown) {
            PopDialog('images/error.gif', 'Add Failed' + errorThrown, ''); return false;
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                var newScreenTabId = mr.d.tabId;
                var newTabLabel = mr.d.label;
                var newtabItem = '<div class="tabpanel" tabtext="' + newTabLabel + '" tabid="' + newScreenTabId + '"><div class="tabname"><h3>' + newTabLabel + '</h3><button title="ADD NEW ITEM" onclick="AddFolderItem(this); return false;" class="AddFolderItemBtn">ADD NEW ITEM</button><div style="clear:both;"></div></div><div class="colCnt"><div class="r-table row_12 rowContainer rowEmpty last"><div class="r-tr"><div class="r-td col_12"><div class="screen-tabfolder"><div class="r-table ui-sortable"></div></div></div></div></div><div class="folderBtnSection"></div></div></div>';
                //$('#columnMapCnt').append(newtabItem);
                $(newtabItem).insertAfter($('.tabpanel:last'));
                ApplyColDrgDrp();
                FocusCurrentScreenTab(null);
            } else { PopDialog('images/error.gif', 'Add Error' + mr.d.errorMsg, ''); return false; };
        }
    });
}

function AddTabToHeaderList(ele) {
    var TabFolderOrder = ($('#columnMapCnt .tabpanel').length + 1) * 10;
    var SysId = $('.r-sysid option:selected').attr('value');
    var ScreenId = $('.tbScreenId').val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/WrAddScreenTab",
        data: $.toJSON({ TabFolderOrder: TabFolderOrder, ScreenId: ScreenId, SysId: SysId }),

        error: function (xhr, textStatus, errorThrown) {
            PopDialog('images/error.gif', 'Add Failed' + errorThrown, ''); return false;
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                $('.tabLinkSec a').removeClass('active');
                var newScreenTabId = mr.d.tabId;
                var newTabLabel = mr.d.label;
                var newTabBtn = '<a href="#" tabid="' + newScreenTabId + '" onclick="activateTab(this);" class="active">' + newTabLabel + '<button class="delTabLink" onclick="DelTab(this); return false;" title="delete"></button></a>';
                $('.tabpanel, .grdTable').hide();
                var newtabItem = '<div class="tabpanel" tabtext="' + newTabLabel + '" tabid="' + newScreenTabId + '"><div class="tabname"><h3 style="display:none;">' + newTabLabel + '</h3><button title="ADD NEW ITEM" onclick="AddFolderItem(this); return false;" class="AddFolderItemBtn">ADD NEW ITEM</button><div style="clear:both;"></div></div><div class="colCnt"><div class="r-table row_12 rowContainer rowEmpty last"><div class="r-tr"><div class="r-td col_12"><div class="screen-tabfolder"><div class="r-table ui-sortable"></div></div></div></div></div><div class="folderBtnSection"></div></div></div>';
                //$('#columnMapCnt').append(newtabItem);
                $(newtabItem).insertAfter($('.tabpanel:last'));
                $(newTabBtn).insertBefore($(ele));
                ApplyColDrgDrp();
                FocusCurrentScreenTab($('a.active'));
            } else { PopDialog('images/error.gif', 'Add Error' + mr.d.errorMsg, ''); return false; };
        }
    });
}

//Add Tab Folder Item
function AddFolderItem(ele) {
    var PScreenObjId = $(ele).closest('.tabpanel').find('.drgItem').last().attr('screenobjid');
    if (!PScreenObjId) {
        PScreenObjId = $(ele).closest('.tabpanel').prevAll('.tabpanel').find('.drgItem').last().attr('screenobjid');
    }
    var SysId = $('.r-sysid option:selected').attr('value');
    var TabFolderId = $(ele).closest('.tabpanel').attr("tabid");
    var ScreenId = $('.tbScreenId').val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/WrAddScreenObj",
        data: $.toJSON({ ScreenId: ScreenId, PScreenObjId: PScreenObjId || "", TabFolderId: TabFolderId || "", IsTab: "Y", NewRow: "Y", SysId: SysId }),

        error: function (xhr, textStatus, errorThrown) {
            PopDialog('images/error.gif', 'Add Failed' + errorThrown, ''); return false;
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                var newScreenObjId = mr.d.columnId;
                var newFolderItem = '<div class="r-table row_12 rowContainer"><div class="r-tr"><div class="r-td rcEmpty"><div class="screen-tabfolder"><div class="r-table ui-sortable"></div></div></div><div class="r-td col_6"><div class="screen-tabfolder"><div class="r-table ui-sortable"><div class="r-tr"><div screenobjid="' + newScreenObjId + '" class="drgItem active"><div class="drgItemCnt"><a>New Item</a><button title="delete" onclick="DelFolderItem(this); return false;" class="col_delete"></button></div></div></div></div></div></div><div class="r-td rcEmpty"><div class="screen-tabfolder"><div class="r-table ui-sortable"></div></div></div></div></div>'
                var newItem = $(newFolderItem).insertBefore($(ele).closest('.tabpanel').find('.folderBtnSection').prev('.last'));
                ApplyColDrgDrp();
                var newItemDrag = newItem.find('.drgItem');
                FocusCurrentScreenObj(newItemDrag, newItemDrag.parent());
            } else { PopDialog('images/error.gif', 'Add Error' + mr.d.errorMsg, ''); return false; };
        }
    });
}

//Delete Tab Folder Item
function DelFolderItem(ele) {
    var ScreenObjId = $('.tbDeleteId').val();
    var SysId = $('.r-sysid option:selected').attr('value');

    if (confirm("Are you sure you want to delete this Tab Folder item?")) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "AdminWs.asmx/WrDelScreenObj",
            data: $.toJSON({ ScreenObjId: ScreenObjId, SysId: SysId }),
            error: function (xhr, textStatus, errorThrown) {
                PopDialog('images/error.gif', 'Delete Failed' + errorThrown, ''); return false;
            },
            success: function (mr, textStatus, xhr) {
                if (mr.d.status == "") {
                    if ($(ele).closest('.rowContainer').find('>.r-tr>.r-td').length <= 3) {
                        if ($(ele).closest('.r-table').find('.r-tr').length <= 1) {
                            $(ele).closest('.rowContainer').remove();
                        } else {
                            $(ele).closest('.r-tr').remove();
                        }
                    }
                    if ($(ele).closest('.r-table').find('.r-tr').length <= 1) {
                        $(ele).closest('.screen-tabfolder').parent().prev('.rcEmpty').remove();
                        $(ele).closest('.screen-tabfolder').parent().remove();
                    } else {
                        $(ele).closest('.r-tr').remove();
                    }
                    FocusCurrentScreenObj(null, null); // no current selected screen obj
                } else { PopDialog('images/error.gif', 'Delete Error' + mr.d.errorMsg, ''); return false; };
            }
        });
    }
}

//Add Grid Item
function AddGrdItem() {
    //    $('#columnMapCnt .drgItem, #columnMapCnt .GrdInner').removeClass('active');
    var PScreenObjId = $('.grdTable .GrdOuter:not(.dropZoneEmpty) .GrdInner').last().attr('screenobjid');
    var SysId = $('.r-sysid option:selected').attr('value');
    var ScreenId = $('.tbScreenId').val();
    var TabFolderId;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/WrAddScreenObj",
        data: $.toJSON({ ScreenId: ScreenId, PScreenObjId: PScreenObjId || "", TabFolderId: TabFolderId || "", IsTab: "N", NewRow: "N", SysId: SysId }),

        error: function (xhr, textStatus, errorThrown) {
            PopDialog('images/error.gif', 'Add Failed' + errorThrown, ''); return false;
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                var newScreenObjId = mr.d.columnId;
                var lastDropZoneTd = $('.grdTable .dropZoneTd').last();
                var newGridItem = '<td><div class="GrdOuter dropZoneEmpty ui-sortable"></div><div class="GrdOuter ui-sortable"><div screenobjid="' + newScreenObjId + '" class="GrdInner active"><span>New Item</span><button title="delete" onclick="DelGridItem(this); return false;" class="col_delete"></button></div></div><div class="GrdOuter dropZoneEmpty ui-sortable"></div></td><td class="dropZoneTd"><div class="GrdOuter ui-sortable"></div></td>';
                var newGridItemDrg = $(newGridItem).insertAfter(lastDropZoneTd);
                ApplyColDrgDrp();
                FocusCurrentScreenObj(newGridItemDrg.find('.GrdInner'), newGridItemDrg);
            } else { PopDialog('images/error.gif', 'Add Error' + mr.d.errorMsg, ''); return false; };
        }
    });
}

//Delete Grid Item
function DelGridItem(ele) {
    var ScreenObjId = $('.tbDeleteId').val();
    var SysId = $('.r-sysid option:selected').attr('value');
    var curGridItem = $('#columnMapCnt .GrdInner[screenobjid = ' + ScreenObjId + ']');

    if (confirm("Are you sure you want to delete this grid item?")) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "AdminWs.asmx/WrDelScreenObj",
            data: $.toJSON({ ScreenObjId: ScreenObjId, SysId: SysId }),
            error: function (xhr, textStatus, errorThrown) {
                PopDialog('images/error.gif', 'Delete Failed' + errorThrown, ''); return false;
            },
            success: function (mr, textStatus, xhr) {
                if (mr.d.status == "") {
                    if ($(ele).closest('td').find('.GrdOuter').length <= 3) {
                        if ($(ele).closest('.GrdOuter').find('.GrdInner').length <= 1) {
                            $(ele).closest('td').prev('.dropZoneTd').remove();
                            $(ele).closest('td').remove();
                        } else {
                            curGridItem.remove();
                        }
                    } else {
                        if ($(ele).closest('.GrdOuter').find('.GrdInner').length <= 1) {
                            $(ele).closest('.GrdOuter ').prev('.dropZoneEmpty').remove();
                            $(ele).closest('.GrdOuter').remove();
                        } else {
                            curGridItem.remove();
                        }
                    }
                    FocusCurrentScreenObj(null, null); // no focus after delete
                } else { PopDialog('images/error.gif', 'Delete Error' + mr.d.errorMsg, ''); return false; };
            }
        });
    }
}


function AddScreenTab() {
    var position = $('#columnMap > .tabContainer').length;
    var TabFolderOrder = (position + 1) * 10;
    var SysId = $('.r-sysid option:selected').attr('value');
    var ScreenId = $('.tbScreenId').val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/WrAddScreenTab",
        data: $.toJSON({ TabFolderOrder: TabFolderOrder, ScreenId: ScreenId, SysId: SysId }),

        error: function (xhr, textStatus, errorThrown) {
            PopDialog('images/error.gif', 'Add Failed' + errorThrown, ''); return false;
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                $('#columnMap').append('<div class="tabContainer" tabId="' + mr.d.tabId + '" tabText="' + mr.d.label + '"><div class="tabFolder"><span class="tab_drag_icon" href="javascript:void(0)" style="display: block;"></span><a class="tabfolderName">' + mr.d.label + '</a><button class="tab_delete" onclick="DelScreenTab(); return false;" title="delete"></button></div><div class="tabCnt"></div></div>');
                $('#columnMap').animate({ scrollTop: ($('#columnMap')[0].scrollHeight) }, 1000);
                //$('#columnMap .tabContainer[tabId = ' + mr.d.tabId + '] .tabFolder').addClass('active').find('.tab_delete').css('display', 'block');
                ApplyColDrgDrp();
                FocusCurrentScreenTab($('#columnMap .tabContainer[tabId = ' + mr.d.tabId + ']').find('.tabfolderName').parent());
            } else { PopDialog('images/error.gif', 'Add Error' + mr.d.errorMsg, ''); return false; };
        }
    });
};

function AddScreenObj() {
    var PScreenObjId = $('#columnMap').find('.colItem.colItem[mastertb="Y"]').last().attr('screenobjid');
    var SysId = $('.r-sysid option:selected').attr('value');
    var TabFolderId = $('#columnMap .tabContainer').last().attr("tabid");
    var ScreenId = $('.tbScreenId').val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/WrAddScreenObj",
        data: $.toJSON({ ScreenId: ScreenId, PScreenObjId: PScreenObjId || "", TabFolderId: TabFolderId || "", IsTab: "Y", NewRow: "N", SysId: SysId }),

        error: function (xhr, textStatus, errorThrown) {
            PopDialog('images/error.gif', 'Add Failed' + errorThrown, ''); return false;
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                var firstGridItem = $('#columnMap .tabContainer').last().find(".colItem[mastertb='N']").first();
                var insertCnt = '<div class="colItem" MasterTb="Y" ScreenObjId="' + mr.d.columnId + '" columnHeader="' + mr.d.label + '"><span class="obj_drag_icon" href="javascript:void(0)" style="display: block;"></span><a>' + mr.d.label + '</a><button class="col_delete" onclick="DelScreenObj(); return false;" title="delete"></button></div>';
                if (firstGridItem.length > 0) {
                    $(insertCnt).insertBefore(firstGridItem);
                } else {
                    $('#columnMap .tabContainer').last().find('.tabCnt').append(insertCnt);
                }
                $('#columnMap').animate({ scrollTop: ($('#columnMap')[0].scrollHeight) }, 1000);
                //$('#columnMap .colItem[screenobjid = ' + mr.d.columnId + ']').addClass('active').find('.col_delete').css('display', 'block');
                ApplyColDrgDrp();
                var newItem = $('#columnMap .colItem[screenobjid = ' + mr.d.columnId + ']');
                FocusCurrentScreenObj(newItem, newItem);
            } else { PopDialog('images/error.gif', 'Add Error' + mr.d.errorMsg, ''); return false; };
        }
    });
};

//Delete tabs in list layout
function DelScreenTab() {
    var ScreenTabId = $('.tbDeleteId').val();
    var SysId = $('.r-sysid option:selected').attr('value');
    var curElement = $('#columnMap .tabContainer[tabid = ' + ScreenTabId + ']');

    if (curElement.find('> .tabCnt > .colItem').length > 0) {
        PopDialog('images/warning.gif', 'Please remove all screen objects inside this tab before deleting', '');
    } else {
        if (confirm("Are you sure you want to delete this screen tab?")) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "AdminWs.asmx/WrDelScreenTab",
                data: $.toJSON({ ScreenTabId: ScreenTabId, SysId: SysId }),
                error: function (xhr, textStatus, errorThrown) {
                    PopDialog('images/error.gif', 'Delete Failed' + errorThrown, ''); return false;
                },
                success: function (mr, textStatus, xhr) {
                    if (mr.d.status == "") {
                        curElement.remove();
                        FocusCurrentScreenObj(null, null); // no current selected screen obj
                    } else { PopDialog('images/error.gif', 'Delete Error' + mr.d.errorMsg, ''); return false; };
                }
            });
        }
    }
};

//Delete tabs in graphical layout
function DelTab(ele) {
    var ScreenTabId = $(ele).closest('a').attr('tabid');
    var SysId = $('.r-sysid option:selected').attr('value');
    var curElement = $('.tabpanel[tabid = ' + ScreenTabId + ']');

    if (curElement.find('> .colCnt .drgItem').length > 0) {
        PopDialog('images/warning.gif', 'Please remove all screen objects inside this tab before deleting', '');
    } else {
        if (confirm("Are you sure you want to delete this screen tab?")) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "AdminWs.asmx/WrDelScreenTab",
                data: $.toJSON({ ScreenTabId: ScreenTabId, SysId: SysId }),
                error: function (xhr, textStatus, errorThrown) {
                    PopDialog('images/error.gif', 'Delete Failed' + errorThrown, ''); return false;
                },
                success: function (mr, textStatus, xhr) {
                    if (mr.d.status == "") {
                        curElement.remove();
                        $(ele).closest('a').remove();
                        if ($('.tabLinkSec > a').length > 0) {
                            $('.tabLinkSec > a:first').addClass('active').click();
                        }
                        FocusCurrentScreenObj(null, null); // no current selected screen obj
                        if ($('a.active').length > 0) FocusCurrentScreenTab($('a.active'));
                    } else { PopDialog('images/error.gif', 'Delete Error' + mr.d.errorMsg, ''); return false; };
                }
            });
        }
    }
};

function DelScreenObj() {
    var ScreenObjId = $('.tbDeleteId').val();
    var SysId = $('.r-sysid option:selected').attr('value');

    if (confirm("Are you sure you want to delete this screen column?")) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "AdminWs.asmx/WrDelScreenObj",
            data: $.toJSON({ ScreenObjId: ScreenObjId, SysId: SysId }),
            error: function (xhr, textStatus, errorThrown) {
                PopDialog('images/error.gif', 'Delete Failed' + errorThrown, ''); return false;
            },
            success: function (mr, textStatus, xhr) {
                if (mr.d.status == "") {
                    var curElement = $('#columnMap .colItem[screenobjid = ' + ScreenObjId + ']');
                    curElement.remove();
                    FocusCurrentScreenObj(null, null); // no current selected screen obj
                } else { PopDialog('images/error.gif', 'Delete Error' + mr.d.errorMsg, ''); return false; };
            }
        });
    }
};

function GetScreenObjList(callback) {
    var SysId = $('.r-sysid option:selected').attr('value');
    var ScreenId = $('.tbScreenId').val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/WrGetScreenObj",
        data: $.toJSON({ ScreenId: ScreenId, SysId: SysId }),
        error: function (xhr, textStatus, errorThrown) {
            PopDialog('images/error.gif', "Can't get Screen column definition:" + errorThrown, ''); return false;
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                callback && callback(mr.d.screenObjList);
                prgramName = mr.d.screenObjList[0].ProgramName;
                $('#prevScreen').attr('target', '_blank').attr('href', programName + '.aspx?typ=N');
            } else { PopDialog('images/error.gif', "Can't get screen column definition:" + mr.d.errorMsg, ''); return false; };
        }
    });
}

//Actual Screen preview
function loadPrevUrl() {
    window.open(programName + '.aspx?typ=n');
}

//Tab Property button iframe
function loadTabProUrl() {
    var keyId = $('.active').parent().attr('tabId');
    var GraphicalLayoutKeyId = $('.active').attr('tabId');

    if (keyId == null && GraphicalLayoutKeyId == null) {
        PopDialog('images/warning.gif', 'Please select a tab and try again.', '');
    } else if (keyId != null) {
        SearchLink('AdmScreenTab.aspx?typ=n&key=' + keyId, '', '800px', '500px');
    } else {
        SearchLink('AdmScreenTab.aspx?typ=n&key=' + GraphicalLayoutKeyId, '', '800px', '500px');
    }
};

//Object Property button iframe
function loadProUrl() {
    var keyId = $('.active[screenobjid]').attr('screenobjid');
    if (keyId == null) {
        PopDialog('images/warning.gif', 'Please select an object and try again.', '');
    } else {
        SearchLink('AdmScreenObj.aspx?typ=n&key=' + keyId + '&mod=' + ($('#columnMap:hidden').length == 0 ? 'A' : 'E'), '', '1060px', '500px');  // Use a wider screen to avoid scroll down.
    }
};

//Object Permision button iframe
function loadPerUrl() {
    var keyId = $('.active[screenobjid]').attr('screenobjid');
    if (keyId == null) {
        PopDialog('images/warning.gif', 'Please select an object and try again.', '');
    } else {
        SearchLink('AdmAuthCol.aspx?typ=n&key=' + keyId, '', '1060px', '500px');
    }
};

//Object Label button iframe
function loadLblUrl() {
    var keyId = $('.active[screenobjid]').attr('screenobjid');
    if (keyId == null) {
        PopDialog('images/warning.gif', 'Please select an object and try again.', '');
    } else {
        SearchLink('AdmColHlp.aspx?typ=n&key=' + keyId, '', '800px', '550px');
    }
};

function showFolderSec() {
    //$('.grdTable').hide();
    $('.tabpanel, .AddTabBtn').show();
    $('#folderBtn').addClass('disabled');
    $('#grdBtn').removeClass('disabled');
    FocusCurrentScreenObj(null, null);
};

function showGrdSec() {
    //$('.grdTable').show();
    $('.tabpanel, .AddTabBtn').hide();
    $('#grdBtn').addClass('disabled');
    $('#folderBtn').removeClass('disabled');
    FocusCurrentScreenObj(null, null);
};

function activateTab(ele) {
    $('.tabLinkSec a').removeClass('active');
    var tabId = $(ele).attr('tabId');
    //$(ele).addClass('active');
    $('.tabpanel, .grdTable').each(function (i, obj) {
        if (tabId == $(obj).attr('tabId')) {
            $(obj).show();
        } else {
            $(obj).hide();
        }
    });
    FocusCurrentScreenObj(null, null);
    FocusCurrentScreenTab($(ele));
}

//function ShowAllTab(ele) {
//    if (ele.checked) {

//    } else {

//    }
//}

function ShowTabHor() {
    $('.tabLinkSec, #showTabVer').show();
    $('.tabname > h3, #showTabHor').hide();
    $('.tabBtnSection').css('display', 'none');
    $('.tabLinkSec a:first').click();
}

function ShowTabVer() {
    $('.tabpanel, .grdTable, .tabname > h3, #showTabHor').show();
    $('.tabBtnSection').css('display', 'inline-block');
    $('.tabLinkSec, #showTabVer').hide();
}

function initObjectIDE() {
    var columnItemList = $('.columnItemList').val();
    if (columnItemList != '') { ShowColMap($.evalJSON(columnItemList)); }
    $('.search-grp > div > .rc-1-8').append('<span>All changes are instant and immediate. Beige spaces will be ignored on actual display.</span>');

    var height = $(window).height() - $('.page').offset().top - 160;
    var prevHeight = $(window).height() - $('.page').offset().top - 120;
    if ($('#columnMap').find('.tabContainer').length == 0) {
        $('#columnMap').css({ 'height': '380px' });
    } else {
        $('#columnMap').css({ 'height': height });
    };

    $('#showTabVer').hide();
    showFolderSec();
    ShowScreenLayout();
    ApplyEleResize();
    $('#objList, #columnMapCnt, #folderBtn, #grdBtn').show();
    $('.showAllTabSection, .tabBtnSection').css('display', 'inline-block');

    //$('#objList').click(function () {
    //    $('#objGraphic').show();
    //    $('.pageListCnt').css('display', 'table');
    //    ShowColListLayout();
    //    $('#objList, #columnMapCnt, #folderBtn, #grdBtn').hide();
    //    $('.showAllTabSection, .tabBtnSection').css('display', 'none');
    //});

    //$('#objGraphic').click(function () {
    //    $('.pageListCnt, #objGraphic').hide();
    //    showFolderSec();
    //    ShowScreenLayout();
    //    ApplyEleResize();
    //    $('#objList, #columnMapCnt, #folderBtn, #grdBtn').show();
    //    $('.showAllTabSection, .tabBtnSection').css('display', 'inline-block');
    //});

    $('#listIframe').on('load', function () {
        $('.iFrameIdt').hide();
    });

    $(window).resize(function () {
        var height = $(window).height() - $('.page').offset().top - 160;
        var prevHeight = $(window).height() - $('.page').offset().top - 40;
        if ($('#columnMap').find('.tabContainer').length == 0) {
            $('#columnMap').css({ 'height': '380px' });
        } else {
            $('#columnMap').css({ 'height': height });
        }
    });
}

function getUpdateItemList() {
    GetUpdatedTabList($('#columnMap'));
    $('.columnItemList').val($.toJSON(columnItemList));
}

function updObjText(objText) {
    var objCnt = "";
    if (!$('.drgItem.active a, .GrdInner.active span').children().hasClass('Mandatory')) {
        objCnt = objText;
    } else {
        objCnt = objText + '<span class="Mandatory">*</span>';
    }
    $('.drgItem.active a, .GrdInner.active span').html(objCnt);
    $('.active').attr('columnheader', objText);
};

function updObjMaster(objText) {
    $('.active').attr('MasterTb', objText);
    if (objText == 'N') {
        $('.active > span').removeClass().addClass('grid_drag_icon');
    } else {
        $('.active > span').removeClass().addClass('obj_drag_icon');
    }
};

function updTabText(tabText) {
    $('.tabLinkSec a.active').html(tabText + '<button class="delTabLink" onclick="DelTab(this); return false;" title="delete"></button>');
};

function updObjHeight(objHeight, type) {
    var columnHeight = effectiveHeight({ ColumnHeight: objHeight, DisplayMode: type });
    if (columnHeight) {
        $('.drgItem.active').attr('style', 'height:' + columnHeight + 'px');
    } else {
        $('.drgItem.active').attr('style', '');
    }
}

function updObjMandatory(objMandatory) {
    if (objMandatory == 'Y') {
        if (!$('.drgItem.active a, .GrdInner.active span').children().hasClass('Mandatory')) {

            $('.drgItem.active a, .GrdInner.active span').append('<span class="Mandatory">*</span>');
        }
    } else {
        $('.drgItem.active a .Mandatory, .GrdInner.active span .Mandatory').remove();
    }
}
