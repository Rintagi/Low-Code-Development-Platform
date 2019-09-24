function CanPostBack(needPostback, el) {
    try { if (el.value == el.acInitVal) return false; } catch (e) { };
    if (needPostback && !el.inACPostBack) {
        el.inACPostBack = true;
        setTimeout('__doPostBack(\'' + el.id + '\',\'\')', 0);
        return false;
    }
    else {
        el.inACPostBack = null;
        return false;
    }
}

(function ($) {
    $.widget("ui.combobox", {
        _init: function () {

        },

        _create: function () {
            var self = this,
                background = $(this.element).css("background-color"),
				select = this.element.css({ position: "absolute",
                            marginLeft: 0, 
                            marginTop: 0,  
                            top: -1000, 
                            left: -1000 }),
				selected = select.children(":selected"),
                cache = {},
                keyCache = {},
                value = select.val();
            var input = this.input = $("<input>")
                .css({ background: background})
				.insertAfter(select)
				.val(value)
				.autocomplete({
				    delay: 50,
				    minLength: 0,
					open: function(event, ui){
                        $(this).autocomplete('widget').css('z-index', 27);
						setTimeout(function() { checkAutocompletePosition(input); }, 0);
				    },
				    source: function (request, response) {
				        var term = $.trim(request.term.toLowerCase());
				        var searchTerm = term;
                        var originalSearchTerm = searchTerm;
                        var topN = 50;
                        var moreCnt = 100;
                        //if (term.match(/^\*\*[0-9]+$/)) 
                        if (term.match(/^\*\*.+$/)) 
                        {
                            var k = term.substring(2);
                            if (k.toLowerCase() == input.val().toLowerCase()) 
                            {
                                var txt = $('#' + $(self.element).attr('id') + "Text").val();
                                input.val(txt);
                                $(input)[0].defaultValue = txt;
//				                input.attr("title", txt);          
//                              input.focus().select();
                                $('span.itemTotal', $(input).closest('div.search-grp')).html('(' + 0 + ' found)');
                                response([]);
                                return;                      
                            }
                            {
                            }
                        } 
                        else 
                        {
                            searchTerm = searchTerm.replace(/^\*+/,'').replace(/\*+/g,' ');
				            if (term.length > 0 && searchTerm.length == 0) {

				                $('span.itemTotal', $(input).closest('div.search-grp')).html('(' + 0 + ' found)');
				                response([]);
				                return;
				            }
                            if (searchTerm == "" || searchTerm==null) 
                            {
                                cache = {};
                            }
                            if (searchTerm == "+") { searchTerm = ""; term="";};
                            if (searchTerm == "^") { searchTerm = input.val(); term = ""; };

				            if (term in cache) {
                                if (cache[term].data != null && originalSearchTerm != '^') {
                                    $('span.itemTotal', $(input).closest('div.search-grp')).html('(' + (cache[term].count - (cache[term].count > 0 && !cache[term].data[0].key ? 1 : 0)) + (cache[term].count >= cache[term].topN ? '+' : '') + ' found)');
				                    response(cache[term].data);
				                    return;
				                }
				                else {
				                    topN = cache[term].nextTopN;
				                }
				            }
                        }
                        if (Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack()) { response([]); return;}
                        $(input).addClass("wait");
                        // handle cross field referencing 2012.1.20 gary
                        if (self.options.datasource_context["refColCID"] != null) {
                            // it is possible that the ref column is just textbox or dropdownlist
                            var refEle = $('#' + self.options.datasource_context["refColCID"]);
                            if (refEle.length == 0) {
                                self.options.datasource_context["refColVal"] = $('#' + self.options.datasource_context["refColCID"]+"_KeyId").val();
                            } 
                            else {
                                self.options.datasource_context["refColVal"] = refEle.val();
                                try {
                                    // handle multiple select
                                    if ($(refEle).prop("tagName") == "SELECT") self.options.datasource_context["refColValIsList"] = "Y";
                                } catch (e) { }
                            }
                        }
                        // handle primary key field reference 2012.11.13 gary
                        if (self.options.datasource_context["pMKeyColID"] != null) {
                            // it is possible that the ref column is just textbox or dropdownlist
                            var refEle = $('#' + self.options.datasource_context["pMKeyColID"]);
                            if (refEle.length == 0) {
                                self.options.datasource_context["pMKeyColVal"] = $('#' + self.options.datasource_context["pMKeyColID"]+"_KeyId").val();
                            } 
                            else {
                                self.options.datasource_context["pMKeyColVal"] = refEle.val();
                            }
                        }

                        var pendingFunc = function() {};
                        (input.data("autocomplete")||input.data('ui-autocomplete')).pendingFunc = pendingFunc;
				        $.ajax({
				            type: "POST",
				            contentType: "application/json; charset=utf-8",
				            dataType: "json",
				            url: self.options.datasource,
				            data: $.toJSON({ query: searchTerm, contextStr: $.toJSON(self.options.datasource_context), topN:topN }),
				            error: function (xhr, textStatus, errorThrown) {
                                $(input).removeClass("wait");

				                //response([]);
				            },

				            success: function (result, textStatus, xhr) {
				                $(input).removeClass("wait");
				                if (!term.match(/^\*\*/)) {
				                    cache[term] = { data: result.d.data, count: result.d.total, topN: result.d.topN, nextTopN: result.d.topN + moreCnt };
                                    if (result.d.total >= result.d.topN) 
                                    {
                                        result.d.data[result.d.data.length] = { key: null, label: moreCnt + ' More...', img: null, total: result.d.total, query: searchTerm };
                                    }
                                    // it is possibl that by the time the ajax return
                                    // the term has changed and we don't want to redraw the old one
                                    // as it is no longer valid and interferr with screen
                                    if ((input.data("autocomplete") || input.data('ui-autocomplete')).pendingFunc == pendingFunc && $(input).length > 0 && $(input).is(":visible")) {
                                        if (originalSearchTerm == '^') setTimeout(function () {
                                            input.autocomplete("search", input.val());
                                        }, 0);
                                        $('span.itemTotal', $(input).closest('div.search-grp')).show().html('(' + (result.d.total - (result.d.total > 0 ? 1 : 0)) + (result.d.total >= result.d.topN ? '+' : '') + ' found)');
                                        response(result.d.data);
                                        }
                                    else {
                                        //response([]);
                                    }
				                } else {
                                    var k = term.substring(2);
                                    keyCache[k] = null;
				                    for (i in result.d.data) {
				                        var x = result.d.data[i];
                                        if (k == x.key) {
                                            keyCache[k] = x.label;
//				                            input.val(x.label);
//				                            //input.attr("title", x.tooltips);
//				                            input.attr("title", self.title);
//				                            $('#' + $(self.element).attr('id') + "Text").val(x.label);
//                                            if (self.options.hasFocus) {
//                                                input.focus().select();
//                                            }
                                            }
				                    }
                                    if ((input.data("autocomplete")||input.data('ui-autocomplete')).pendingFunc == pendingFunc) {
				                        response([]);
                                        }
                                    else {
                                        //response([]);
                                    }
				                }
				            }
				        });
				    },
				    select: function (event, ui) {
				        if (ui.item.key != null) {
                            /* save old value for restore purpose */
                            var oldKey = $(self.element).val();
                            if (oldKey != ui.item.key) {
                                (input.data("autocomplete")||input.data('ui-autocomplete')).oldKey = $(self.element).val();
                                (input.data("autocomplete")||input.data('ui-autocomplete')).oldLabel =  $('#' + $(self.element).attr('id') + "Text").val();
                            
				                $(self.element).val(ui.item.key);
				                $('#' + $(self.element).attr('id') + "Text").val(ui.item.label);
				                $(self.element).change();

                            }
                        } 
                        else {
				            if (cache[ui.item.query]) {
                                cache[ui.item.query].data = null;
                                if (ui.item.query != "") {
                                    var query = ui.item.query;
                                    var label = ui.item.label;
                                    ui.item.label = query;
                                    ui.item.value = query;
                                    if (label && label.match(/More...$/i))
                                        input.autocomplete("search", "^");
                                    else
                                        input.autocomplete("search", ui.item.query);
                                }
                                else {
                                    ui.item.label = input.val();
                                    ui.item.value = input.val();
                                    setTimeout(function () { input.autocomplete("search", "+"); }, 0);
                                }
                            }
                        }
                        if((/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i).test(navigator.userAgent)){
                            input.parent().find('button').focus();
                        }
                        else {
                            setTimeout(function(){input.select();},0);
                        }
                      
				        //				        $(self.element).find("option")
				        //                            .remove()
				        //                            .end()
				        //                            .append("<option selected value=\"" + ui.item.key + "\">" + ui.item.label + "</option>")
				        //                            .val(ui.item.value);
				    },
				    change: function (event, ui) {
				        if (!ui.item) {
				            var term = $.trim($(this).val().toLowerCase());
                            //if (term.match(/^\*\*[0-9]+$/) && keyCache[term.substring(2)] != null)
                            if (term.match(/^\*\*.+$/) && keyCache[term.substring(2)] != null)
                            {
                                $(self.element).val(term.substring(2));
				                $('#' + $(self.element).attr('id') + "Text").val(keyCache[term.substring(2)]);
				                valid = true;
                                $(input).val(keyCache[term.substring(2)]);
                                select.change();
                                return;
                            }
                            var termE = $.ui.autocomplete.escapeRegex(term);
				            var termT = termE.replace(/(\\\*)+/g,".*").replace(/\\ /g,".*").replace(/\s+/g,".*");
//				            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(term) + "$", "i"),
				            var matcher = new RegExp(termT, "i");
							var	valid = false;
				            if (term in cache) {
				                $(cache[term].data).each(function (i, e) {
				                    if (this.label.match(matcher)) {
				                        $(self.element).val(e.key);
				                        $('#' + $(self.element).attr('id') + "Text").val(e.label);
				                        valid = true;
                                        $(input).val(e.label);
                                        select.change();
				                        return false;
				                    }
				                });
				            }

				            if (!valid) {
				                var priorValue = select.val();
                                var oldVal = term != "" ? select.val() : "" ;
                                var oldLabel = term != "" ? $('#' + $(self.element).attr('id') + "Text").val() : "";
//				                // remove invalid value, as it didn't match anything
//				                $(this).val("");
//				                select.val("");
//				                $('#' + $(self.element).attr('id') + "Text").val("");
//				                (input.data("autocomplete")||input.data('ui-autocomplete')).term = "";
                                $(this).val(oldLabel);
                                (input.data("autocomplete")||input.data('ui-autocomplete')).term = oldLabel;
                                if (term == "") {
                                    (input.data("autocomplete")||input.data('ui-autocomplete')).oldKey = $(self.element).val();
                                    (input.data("autocomplete")||input.data('ui-autocomplete')).oldLabel =  $('#' + $(self.element).attr('id') + "Text").val();
                                    select.val("");
                                    $('#' + $(self.element).attr('id') + "Text").val("");
                                    if (priorValue != term) select.change();
                                }
				                return false;
				            }
				        }
                        
				    },
				    close: function(event, ui)
				    { 
                    //if ($("input:focus").length == 0) input.focus(); 
                    }
				})
				.addClass("ui-widget ui-widget-content ui-corner-left autocomplete-box-input")
                .css({background:background})
                .blur(function(e) { if(IsMobile()){$body.removeClass('mobilekeyPop');}});
            input.attr("title", $(self.element).attr('title')); input.attr("disabled", $(self.element).attr('disabled'));
            var inputWidth = $(input).width();
            var autoComleteWidget = autoComleteWidget = input.data("autocomplete")||input.data('ui-autocomplete');
            autoComleteWidget._renderItem = function (ul, item) {
                if (Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack()) return "";
                var autoCompleteItem = $((item.key == null ? "<li>" : "<li>") + "</li>").data("item.autocomplete", item) || $((item.key == null ? "<li>" : "<li>") + "</li>").data('ui-autocomplete-item', item);
                //var anchor = $((item.key == null ? "<a style='color:#FF5C00'>" : "<a>") + item.label + "</a>")
                //    .append(!item.tooltips ? "" : "<p style='color:red'>" + item.tooltips + "</p>")
                //    .append(!item.iconUrl ? "" : "<img style='color:blue' src='" + item.iconUrl + "' alt=''/>")
                //    .append(!item.img ? "" : "<img style='color:yellow' src='" + item.img + "' alt=''/>")
                var imageSectionWidth = item.img == null ? 0 : 45;
                var iconSectionWidth = item.iconUrl == null ? 0 : 45;
                var rowHeight = item.detail == null && item.img == null && item.iconUrl == null ? 20 : 40;
                var anchor = $((!item.key ? "<a style='color:#FF5C00;height:20px;'" + (item.detail == null && item.img == null && item.iconUrl == null ? "" : " class='seperator' ") + ">" : "<a style='height:" + rowHeight + "px'" + (item.detail == null && item.img == null && item.iconUrl == null ? "" : "class='seperator'") + ">") +
                               "<div class='r-table' ><div class='r-tr'><div class='r-td'><b>" + item.label +"</b>" +
                               (!item.detail ? "" : "<span class='autoCompDesc' style='max-width:" + (inputWidth - imageSectionWidth - iconSectionWidth) + "px' title='" + item.detail + "'>" + item.detail + "</span>") +
                               "</div><div class='r-td iconArea' style='width:" + (imageSectionWidth + iconSectionWidth -4) + "px'>" +
                               (!item.iconUrl ? "" : "<img class='autoCompIcon' src='" + item.iconUrl + "' alt=''/>") +
                               (!item.img ? "" : "<img class='autoCompIcon' src='" + item.img + "' alt=''/>") +
                               "</div></div></div></a>");
                var x =
                autoCompleteItem
                    .append(anchor)
					//.append((item.key == null ? "<a style='color:#FF5C00'>" : "<a>") + item.label + "</a>")
                    //.append(!item.tooltips ? "" :  "<p style='color:red'>" + item.tooltips + "</p>")
                    //.append(!item.iconUrl ? "" : "<img style='color:blue' src='" + item.iconUrl + "' alt=''/>")
                    //.append(!item.img  ? "" : "<img style='color:yellow' src='" + item.img + "' alt=''/>")
					.appendTo(ul);
                //                    .attr("title", item.tooltips);
                return x;
            };
            if (value && value != "") {
                self.element[0].acInitVal = value;
                input.autocomplete("search", "**" + value);
                if (self.options.hasFocus) {
                    input.focus().select();
//                      input.parent().find('button').focus();
                }
            }
            else {
               if (self.options.hasFocus) {
                    input.focus().select();
//                    input.parent().find('button').focus();
                }
            }
            $(select).bind('change', function (ev) {
                try {
                    if ($(select).val() == (input.data("autocomplete")||input.data('ui-autocomplete')).oldKey) {
                        var oldTerm = (input.data("autocomplete")||input.data('ui-autocomplete')).oldLabel;
                        $(input).val(oldTerm);
                        $('#' + $(select).attr('id') + "Text").val(oldTerm);
                        (input.data("autocomplete")||input.data('ui-autocomplete')).term = oldTerm;
                    }
                } catch (err) { }
                /* STOP removing code that has been commented out if you are not the person responsible for it
                 * it may be there that needs to be resurrected later
                 */
//                ev.preventDefault();
//                ev.stopImmediatePropagation();
//                ev.stopPropagation();
            });

            if (select.css('max-width') != 'none') { $(input).css('max-width', select.css('max-width')); }
            else if (select.width() != 'none') { $(input).css('width', select.css('width')); }

            $(self.element).focus(function(a,b) {
                if (value && value != "") {
                    self.options.hasFocus = true;
                    input.focus().select();
                }
                else {
                    input.focus().select();
                }
                self.element.hide();
            });
            var wasOpen;

            this.button = $("<button type='button'>&nbsp;</button>")
				.attr("tabIndex", -1)
				.attr("title", "Show Top 50 Items from the start")
 				.insertAfter(input)
				.button({
				    icons: {
				        primary: "ui-icon-search"
				    },
				    text: false
				})
				.removeClass("ui-corner-all")
				.addClass("ui-corner-right ui-button-icon autocomplete-box-button")
                .mousedown(function () { wasOpen = input.autocomplete("widget").is(":visible"); })
				.click(function () {
                    if (input.attr('disabled')) return;
				    // close if already visible
                    if (wasOpen) {
                        if ($("input:focus").length == 0) input.focus();
                        return;
                    }
				    //if (input.autocomplete("widget").is(":visible")) {
				    //    input.autocomplete("close");
				    //    return;
				    //}

				    // work around a bug (likely same cause as #5265)
				    $(this).blur();

				    // pass empty string as value to search for, displaying all results
				    input.autocomplete("search", "");
                    // list and ready to search, do not disable unless you know what you are doing
                    if(IsMobile){ try {$body.addClass('mobilekeyPop');} catch (e) { };}
				    input.focus();
				});
        },

        destroy: function () {
            this.input.remove();
            this.button.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
		}
    });
})(jQuery);

function CreateComboAuto(id, url, suggestContext, pickFirst,isFocusing) {
    var hasFocus = isFocusing;
    $("input:focus").each(function(a,b) {
        if (b.id == id) hasFocus = true;
    }); 

    var combobox = jQuery('#'+id).combobox(
        { datasource_context: suggestContext,
          datasource: url,
          hasFocus: hasFocus
         }
        );
    return combobox;
}

function ApplyAutoComplete(ele, keyEleId, url, context, event) {
    /* this is only triggered by focus on the textbox or click on the search 
     * initPageLoad is set somewhere BEFORE this and it is global
    */
    var isMobile;
    if((/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i).test(navigator.userAgent)){
        isMobile = true;
    }else{
        isMobile = false;
    }
    var isTextBox = ele.tagName == "INPUT";
    var focusByProgram = typeof(initPageLoad) != "undefined" && initPageLoad && isTextBox;
    var keyField = $('#' + keyEleId);
    if (keyField.attr('disabled')) return;
    if (isTextBox) $('span.ui-button-icon-only', $(ele).parent()).hide();
    else $('input', $(ele).parent()).hide();


    initPageLoad = false;

    if (!focusByProgram  && !isMobile) keyField.show().focus();

    /* create */
    var combobox = CreateComboAuto(keyEleId, url, context, false, !focusByProgram || !isMobile);
    combobox.autocomplete();
    /* then hide */
    $(ele).hide();
    if (!isTextBox) $('button', combobox.parent()).click();
    if (focusByProgram && isMobile) setTimeout(function() { $('button', combobox.parent()).focus(); }, 0);
    return false;
}

// position autocomplete menu above autocomplete component if no room below
checkAutocompletePosition = function (input) {
    var obj = input.data("autocomplete") || input.data('ui-autocomplete');
    var offset = $((obj).menu.element).offset(),
		heightMenu = $((obj).menu.element).outerHeight();
    spaceAbove = undefined,
		spaceBelow = undefined;
    var windowHeight = $(window).height();

    if (offset.top + heightMenu > windowHeight) {
        // not enough room below component; check if above is better
        spaceBelow = windowHeight - offset.top;
        spaceAbove = input.offset().top;

        if (spaceAbove > spaceBelow) {
            $((obj).menu.element).css('top', (spaceAbove - heightMenu) + 'px');
        }
    }

    //    oldTop = jQuery(".ui-autocomplete").offset().top;
    //                newTop = oldTop - jQuery(".ui-autocomplete").height() - jQuery("#quick_add").height() - 10;
    //                jQuery(".ui-autocomplete").css("top", newTop);

};
