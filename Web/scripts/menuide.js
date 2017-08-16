var sitemapHistory = {
    stack: new Array(),
    temp: null,
    saveState: function (item) {
        sitemapHistory.temp = { item: $(item), itemParent: $(item).parent(), itemAfter: $(item).prev() };
    },
    commit: function () {
        if (sitemapHistory.temp != null) sitemapHistory.stack.push(sitemapHistory.temp);
    },
    //restores the state of the last moved item.
    restoreState: function () {
        var h = sitemapHistory.stack.pop();
        if (h == null) return;
        if (h.itemAfter.length > 0) {
            h.itemAfter.after(h.item);
        }
        else {
            h.itemParent.prepend(h.item);
        }
        //checks the classes on the lists
        $('#sitemap li.sm2_liOpen').not(':has(li)').removeClass('sm2_liOpen');
        $('#sitemap li:has(ol li):not(.sm2_liClosed)').addClass('sm2_liOpen');
    }
}

var menuList = [];

function GetUpdatedMenuList(container, parentid) {
    var order = 10;
    $('>li', container).each(function (i, e) {
        var line = $('>.dl', e);
        var menuItemId = line.attr('itemId');
        var menuItemText = line.attr('itemtext');
        var menuItemParentId = parentid == null || typeof (parentid) == "undefined" ? "" : parentid;
        var myid = line.attr('itemId');
        var children = $('> ol', e);
        menuList.push({ newMenuId: menuItemId, newMenuItemText: menuItemText, newMenuItemParentId: menuItemParentId, order: order });
        order = order + 10;
        if (children.length != 0) {
            GetUpdatedMenuList(children, myid);
        }
    });
}

var newList = [];

//init functions
function buildTree(menuList, parentid) {
    var myList = [];
    $.each(menuList, function (idx, ele) {
        try {
            if (ele.ParentId == parentid) {
                var dl = '';
                var childStr = buildTree(menuList, ele.MenuId);
                if (childStr != null) {
                    dl = '<div class="dl" itemid="' + ele.MenuId + '" itemtext="' + ele.MenuText + '"><a href="javascript:void(0)" class="drag_icon">&nbsp;</a><div class="dt"><a class="sm2_title" menuUrl = "' + ele.MenuUrl + '" onclick="loadMenuUrl(this); return false;">' + ele.MenuText + '</a></div><button class="sm2_delete" onclick="DelMenu(); return false;" title="delete"></button></div>';
                    myList.push('<li class="js-nestedSortable-branch js-nestedSortable-expanded">' + dl + '<ol>' + childStr + '</ol>' + '</li>');
                } else {
                    dl = '<div class="dl" itemid="' + ele.MenuId + '" itemtext="' + ele.MenuText + '"><a href="javascript:void(0)" class="drag_icon">&nbsp;</a><div class="dt"><a class="sm2_title" menuUrl = "' + ele.MenuUrl + '" onclick="loadMenuUrl(this); return false;">' + ele.MenuText + '</a></div><button class="sm2_delete" onclick="DelMenu(); return false;" title="delete"></button></div>';
                    myList.push('<li class="js-nestedSortable-leaf">' + dl + '</li>');
                }
            }
        } catch (e) {
            var xxxxx = "";
        }
    });

    if (myList.length > 0) {
        return myList.join('');
    }
    return null;
};

function ShowMenuMap(addList) {
    var result = buildTree(addList, "");
    $("#sitemap").html("");
    $("#sitemap").append(result);
    $(".addMenuBtn").remove();
    $("#sitemap").after("<button class='addMenuBtn' onclick='AddMenu(); return false;'>ADD NEW ITEM</button>");
    $('#sitemap > li:first-child > .dl > .dt').addClass(' active');
    if ($('#sitemap').find('.active').length == 1) {
        $('.active a').click();
        $('.active').parent().find('.sm2_delete, .drag_icon').css('display', 'block');
    };

    ApplyDrgDrp();
}

function ApplyDrgDrp() {
    var menuItemId;
    var menuItemText;
    var menuItemParentId;
    var SysId;
    var PMenuId;
    var menuPrevIndex;
    var menuPrevParentId;

    var ns = $('ol.sortable').nestedSortable({
        forcePlaceholderSize: true,
        handle: 'div',
        helper: 'clone',
        items: 'li',
        opacity: .6,
        placeholder: 'placeholder',
        revert: 50,
        tabSize: 25,
        tolerance: 'pointer',
        toleranceElement: '> div',
        maxLevels: 6,
        isTree: true,
        expandOnHover: 700,
        delay: 200,
        startCollapsed: false,
        //Fires when the item is dragged to a new location.
        stop: function (event, ui) {
            if ($(ui.item).parent().hasClass('ui-sortable')) {
                menuItemParentId = "";
            } else {
                menuItemParentId = $(ui.item).parent().parent().children('.dl').attr('itemid');
            };
            SysId = $('.r-sysid option:selected').attr('value');
            if ($(ui.item).prev('li').length > 0) {
                PMenuId = $(ui.item).prev('li').find('>.dl').attr('itemid');
            } else {
                PMenuId = "";
            }

            sitemapHistory.commit();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "AdminWs.asmx/UpdMenu",
                data: $.toJSON({ MenuId: menuItemId, PMenuId: PMenuId, ParentId: menuItemParentId, MenuText: menuItemText, SysId: SysId }),
                error: function (xhr, textStatus, errorThrown) {
                    alert("Update Failed" + errorThrown);
                },
                success: function (mr, textStatus, xhr) {
                    if (mr.d.status == "") {
                        ApplyDrgDrp();
                    } else { alert("Update Error: " + mr.d.errorMsg) };
                }
            });
        },
        //Fires when the item is dragged.
        sort: function (event, ui) {
            var ItemId = $(ui.item).children('.dl').attr('itemid');
            var ItemText = $(ui.item).children('.dl').attr('itemtext');
            menuItemId = ItemId;
            menuItemText = ItemText;
            menuPrevIndex = $(ui.item).prevAll().length * 10;
            if ($(ui.item).parent().hasClass('ui-sortable')) {
                menuPrevParentId = "";
            } else {
                menuPrevParentId = $(ui.item).parent().parent().children('.dl').attr('itemid');
            };
        }
    });

    //restore the state
    $('.sitemap_undo').click(sitemapHistory.restoreState);
    $(document).bind('keypress', function (e) {
        if (e.ctrlKey && (e.which == 122 || e.which == 26))
            sitemapHistory.restoreState();
    });

    $('#sitemap .dl').click(function () {
        $(this).find('.dt').removeClass('hover');
        $('#sitemap .dl .dt').removeClass('active');
        $(this).find('.dt').addClass('active');
        if ($('#sitemap').find('.active').length == 1) {
            $('.sm2_delete, .drag_icon').css('display', 'none');
            $('.active').parent().find('.sm2_delete, .drag_icon').css('display', 'block');
        };
    });

    $('#sitemap .dl').hover(
         function () {
             $(this).find('.dt').addClass('hover');
         },
         function () {
             $(this).find('.dt').removeClass('hover');
         }
     );

    $('#sitemap .sm2_delete').hover(function () {
        $('.pDeleteId input').val($(this).parent().attr('itemid'));
    });
}

function AddMenu() {
    var position = $('#sitemap > li').length;
    var PMenuId = $('#sitemap').find('>li').last().find('.dl').attr('itemid') ;
    var ParentId = $('.pMenuId input').val();
    var MenuId = "";
    var SysId = $('.r-sysid option:selected').attr('value');
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/AddMenu",
        data: $.toJSON({ PMenuId: PMenuId === undefined ? "0" : PMenuId, ParentId: ParentId, SysId: SysId }),
        error: function (xhr, textStatus, errorThrown) {
            alert("Add Failed" + errorThrown);
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                dl = '<div class="dl" itemid="' + mr.d.menuId + '" itemtext="' + mr.d.label + '"><a href="javascript:void(0)" class="drag_icon">&nbsp;</a><div class="dt"><a class="sm2_title" menuUrl = "" onclick="loadMenuUrl(this); return false;">' + mr.d.label + '</a> </div><button class="sm2_delete" onclick="DelMenu(); return false;"></button></div>';
                if ($('#sitemap').find('li').length == 0) {
                    $('#sitemap').append('<li class="js-nestedSortable-leaf">' + dl + '</li>');
                } else {
                    $('#sitemap > li:last-child').after('<li class="js-nestedSortable-leaf">' + dl + '</li>');
                }

                $('#sitemap .dl .dt').removeClass('active');
                $('#sitemap .dl[itemid = ' + mr.d.menuId + '] > .dt').addClass('active');

                if ($('#sitemap').find('.active').length == 1) {
                    $('.sm2_delete, .drag_icon').css('display', 'none');
                    $('.active').parent().find('.sm2_delete, .drag_icon').css('display', 'block');
                    $('.active a').click();
                    $('.pageBtnSec, .navigateCntMsg, .pgPreview').show();
                };
                $('#sitemap').animate({ scrollTop: ($('#sitemap')[0].scrollHeight) }, 1000);

                ApplyDrgDrp();

            } else { alert("Add Error: " + mr.d.errorMsg) };
        }
    });
};

function DelMenu() {
    var MenuId = $('.pDeleteId input').val();
    var SysId = $('.r-sysid option:selected').attr('value');
    $('#sitemap .dl .dt').removeClass(' active');
    $('#sitemap .dl[itemid = ' + MenuId + ']').find('> .dt').addClass(' active');
    if (confirm("Are you sure you want to delete this menu item?")) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "AdminWs.asmx/DelMenu",
            data: $.toJSON({ MenuId: MenuId, SysId: SysId }),
            error: function (xhr, textStatus, errorThrown) {
                alert("Delete Failed" + errorThrown);
            },
            success: function (mr, textStatus, xhr) {
                if (mr.d.status == "") {
                    var curElement = $('#sitemap .dl[itemid = ' + MenuId + ']').closest('li');
                    if (curElement.find('> ol').length == 0) {
                        if (curElement.prevAll('li').length == 0 && curElement.nextAll('li').length == 0) {
                            if (curElement.parent().attr('id') == 'sitemap') {
                                curElement.next('li').find('> .dl > .dt').addClass('active');
                            } else {
                                curElement.parent().parent().find('> .dl > .dt').addClass('active');
                            }
                        } else if (curElement.prevAll('.li').length == 0 && curElement.nextAll('li').length != 0) {
                            curElement.next('li').find('> .dl > .dt').addClass('active');
                        } else {
                            curElement.prev('li').find('> .dl > .dt').addClass('active');
                        }

                        if (curElement.parent().has('ol') && curElement.parent().children('li').length == 1) {
                            if (curElement.parent().is('#sitemap')) {
                                curElement.remove();
                            } else {
                                curElement.parent().parent().removeClass('sm2_liOpen');
                                curElement.parent().remove();
                            }
                        } else {
                            curElement.remove();
                        }
                    } else {
                        if (curElement.find('> ol > li').length == 1) {
                            curElement.find(' > ol > li > .dl > .dt').addClass(' active');
                        } else {
                            curElement.find(' > ol > li:first-child > .dl > .dt').addClass(' active');
                        }

                        if (curElement.next('li').length == 0 && curElement.parent().attr('id') == 'sitemap') {
                            curElement.parent().find(' > .dropzone-last').remove();
                        } else {
                            curElement.find(' > ol > .dropzone-last').remove();
                        }

                        curElement.find(' > .dropzone').remove();
                        curElement.find(' > .dl').remove();
                        curElement.find(' > ol > li').unwrap().unwrap();
                    }

                    if ($('#sitemap').find('.active').length == 1) {
                        $('.active a').click();
                    } else {
                        $('.pageBtnSec, .navigateCntMsg, .pgPreview').hide();
                    };

                    if ($('#sitemap').find('li').length == 0) {
                        $('#sitemap').css({ 'height': '400px' });
                    }

                } else { alert("Delete Error" + mr.d.errorMsg) };
            }
        });
    }
};

//Definition button iframe
function loadDefiUrl() {
    $('.menuIframeIdt').show();
    var url = $('.active > a').attr('menuurl');
    if (url == null || url == '') {
        $('.definition').hide();
        PopDialog('images/warning.gif', 'It is a direct URL or there is no \'Definition\' for this menu item. Please click \'Menu Item\' button to modify the attributes.', '');
    } else {
        SearchLink(url, '', '1200px', '650px');
        $('.definition').show();
    }
};

//Item button iframe
function loadItemUrl() {
    $('.menuIframeIdt').show();
    var keyId = $('.active').closest('.dl').attr('itemid');
    SearchLink('AdmMenu.aspx?typ=n&key=' + keyId, '', '800px', '500px');
};

//Perm button iframe
function loadPermUrl() {
    $('.menuIframeIdt').show();
    var keyId = $('.active').closest('.dl').attr('itemid');
    SearchLink('AdmMenuPerm.aspx?typ=n&key=' + keyId, '', '800px', '500px');
};

//Translation button iframe
function loadTranUrl() {
    $('.menuIframeIdt').show();
    var keyId = $('.active').closest('.dl').attr('itemid');
    SearchLink('AdmMenuHlp.aspx?typ=n&key=' + keyId, '', '800px', '550px');
};

//Menu Definition Iframe
function loadMenuUrl(elem) {
    $('.iFrameIdt').show();
    var MenuId = $(elem).closest('.dl').attr('itemid');
    var SysId = $('.r-sysid option:selected').attr('value');
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/GetMenuUrl",
        data: $.toJSON({ MenuId: MenuId, SysId: SysId }),

        error: function (xhr, textStatus, errorThrown) {
            alert("Get Menu Failed" + errorThrown);
        },
        success: function (mr, textStatus, xhr) {
            if (mr.d.status == "") {
                if (mr.d.navigateUrl == '' || mr.d.navigateUrl == null) {
                    $('#navigateCntMsg').show();
                    $('#pgPreviewIframe').hide();
                    $('.iFrameIdt').hide();
                } else {
                    if (mr.d.navigateUrl.indexOf("~/") > -1) {
                        $('#pgPreviewIframe').attr('src', mr.d.navigateUrl.replace('~/', ''));
                    } else {
                        if (mr.d.navigateUrl.indexOf("?") > -1) {
                            $('#pgPreviewIframe').attr('src', mr.d.navigateUrl + '&typ=n&enb=N');
                        } else {
                            $('#pgPreviewIframe').attr('src', mr.d.navigateUrl + '?typ=n&enb=N');
                        }
                    }

                    $('#navigateCntMsg').hide();
                    $('#pgPreviewIframe').show();
                };

            } else { alert("Get Menu Error: " + mr.d.errorMsg) };
        }
    });
};
/*End Menu IDE*/
