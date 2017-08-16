function firstLevelMenu() {
    if ($('.sb-menu>li>div>.sb-toggle-submenu').hasClass('sb-submenu-active') && !$(this).hasClass('sb-submenu-active')) {
        var currentActive = $('.sb-submenu-active');
        currentActive.removeClass('sb-submenu-active');
        $('ul.sb-submenu').removeClass('sb-submenu-active');
        currentActive.parent().children('ul').slideUp(200);
    }

    $submenu = $(this).parent().parent().children('.sb-submenu');
    $(this).add($submenu).toggleClass('sb-submenu-active');
    if ($submenu.hasClass('sb-submenu-active')) {
        $submenu.slideDown(200);
    } else {
        $submenu.slideUp(200);
    }
}

function windowResize() {
    if ($("#mobileMenu").css('display') != "none") {
        $("#TpMobileMenu").css('display', 'block');
        $("#TpMobileMenu").appendTo("#mobileMenuArea");
        $("#mobileMenuArea").css('display', 'block');
        $("#VrMenu").css('display', 'none');
    } else {
        $("#TpMobileMenu").css('display', 'none');
        $("#VrMenu").css('display', 'block');
        $("#mobileMenuArea").css('display', 'none');
    }

    if ($('#TpMobileMenu').children().length > 0) {
        $('#mobileMenu').show();
    } else {
        $('#mobileMenu').hide();
    }

    var sliderBarExpanded = $(".sb-navbar[style*='translate(-250px)']");
    if ($(window).width() >= 767 && sliderBarExpanded.length > 0) {
        $(".sb-toggle-right").click();
    }
}

$(window).resize(function () {
    windowResize();
});

$(document).ready(function () {
    windowResize();
    $("ul#TpMobileMenu").addClass("sb-menu");
    $("#TpMobileMenu li>.TpMenuSub").addClass("sb-submenu");
    $("#TpMobileMenu .TpMenuGrpTitle>a").each(function (i, j) {
        if ($(this).parent().parent().find("ul").length > 0) {
            $(this).addClass("sb-toggle-submenu");
        }
    });

    var MenuLink = ""; var MenuText = "";

    $('.sb-toggle-submenu').each(function (i, j) {
        if ($(this).attr('href') != "" && $(this).attr('href').indexOf("javascript:") < 0 && $(this).attr('href').length != 0) {
            MenuLink = $(this).attr('href');
            MenuText = $(this).html();
            $(this).attr('href', '#');
            $(this).parent().parent().children('ul').prepend("<li class='TpMenuNode copy'><div><a class='TpMenuLink' href='" + MenuLink + "'>" + MenuText + "</a></div></li>");
        } else if ($(this).attr('href').indexOf("javascript:") >= 0) {
            $(this).attr('href', '#');
        }

        if ($(this).children().hasClass("sb-caret")) {
            return false;
        } else {
            $(this).append("<span class='sb-caret'></span>");
        }
    });

    $.slidebars();
    $('.sb-menu>li>div>.sb-toggle-submenu').off('click') // Stop submenu toggle from closing Slidebars.
    .on('click', firstLevelMenu);

    $('.sb-submenu>li>div>.sb-toggle-submenu').off('click') // Stop submenu toggle from closing Slidebars.
    .on('click', function () {
        $submenu = $(this).parent().parent().children('.sb-submenu');
        $(this).add($submenu).toggleClass('sb-submenu-active'); // Toggle active class.
        if ($submenu.hasClass('sb-submenu-active')) {
            $submenu.slideDown(200);
        } else {
            $submenu.slideUp(200);
        }
    });
});