! function ($) {
    "use strict";

    function sideNav() { $(".side-nav .side-nav-menu li a").on("click", function (e) { $(this).parent().hasClass("open") ? $(this).parent().children(".dropdown-menu").slideUp(200, function () { $(this).parent().removeClass("open") }) : ($(this).parent().parent().children("li.open").children(".dropdown-menu").slideUp(200), $(this).parent().parent().children("li.open").children("a").removeClass("open"), $(this).parent().parent().children("li.open").removeClass("open"), $(this).parent().children(".dropdown-menu").slideDown(200, function () { $(this).parent().addClass("open") })) }) }

    function sideNavToggle() { $(".side-nav-toggle").on("click", function (e) { $(".wrapper").toggleClass("is-collapsed"), e.preventDefault() }) }

    function sidePanelToggle() { $(".side-panel-toggle").on("click", function (e) { $(".side-panel").toggleClass("side-panel-open"), e.preventDefault() }) }

    function chatToggle() { $(".chat-toggle").on("click", function (e) { $(".chat").toggleClass("open"), e.preventDefault() }) }

    function todoToggle() { $(".todo-toggle").on("click", function (e) { $(".todo-wrapper").toggleClass("open"), e.preventDefault() }) }

    function searchToggle() { $(".search-toggle").on("click", function (e) { $(".search-box, .search-input").toggleClass("active"), $(".search-input input").focus(), e.preventDefault() }) }

    function advanceSearch() { $(".search-input input").on("keyup", function () { $(this).val().length > 0 ? $(".advanced-search").addClass("active") : $(".advanced-search").removeClass("active"), $(".serach-text-bind").html($(this).val()) }) }

    function themeConfig() { $(".theme-toggle, .config-close").on("click", function (e) { $(".theme-configurator").toggleClass("theme-config-open"), e.preventDefault() }) }

    //function perfectSB() { $(".scrollable").perfectScrollbar() }

    function cardPortletCtrl() {
        $("[data-toggle=card-refresh]").on("click", function (e) {
            var cardRefreshSelector = $(this).parents(".card");
            cardRefreshSelector.addClass("card-refresh"), window.setTimeout(function () { cardRefreshSelector.removeClass("card-refresh") }, 2e3), e.preventDefault(), e.stopPropagation()
        }), $("[data-toggle=card-delete]").on("click", function (e) {
            var cardDeleteSelector = $(this).parents(".card");
            cardDeleteSelector.addClass("animated zoomOut"), cardDeleteSelector.bind("animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd", function () { cardDeleteSelector.remove() }), e.preventDefault(), e.stopPropagation()
        })
    }


    $(document).ready(() => {
        $("#alertBox").removeClass('hide'); $("#alertBox").addClass('show');
        setTimeout(() => { $("#alertBox").addClass('hide'); $("#alertBox").removeClass('show'); }, 3000);
    });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })

    // Start Upload File 

    // will hold the local preview url
    var _PREVIEW_URL;

    /* Show Select File dialog */
    $("#upload-dialog").click(function () {
        $("#image-file").click();
    });

    /* Selected File has changed */
    $("#image-file").change(function () {
        // user selected file
        var file = this.files[0];

        // allowed MIME types
        var mime_types = ['image/jpeg', 'image/png'];

        // validate MIME
        if (mime_types.indexOf(file.type) == -1) {
            alert('Error : Incorrect file type');
            return;
        }

        // validate file size
        if (file.size > 2 * 1024 * 1024) {
            alert('Error : Exceeded size 2MB');
            return;
        }

        // validation is successful

        // hide upload dialog button
        document.querySelector("#upload-dialog").style.display = 'none';

        // object url
        _PREVIEW_URL = URL.createObjectURL(file);

        // set src of image and show
        document.querySelector("#preview-image").setAttribute('src', _PREVIEW_URL);
        document.querySelector("#preview-image").style.display = 'inline-block';
        document.querySelector("#delete-image").style.display = 'inline-block';

    });
    $("#delete-image").click(function () {
        document.querySelector("#upload-dialog").style.display = 'inline-block';

        // object url
        _PREVIEW_URL = "";

        // set src of image and show
        document.querySelector("#preview-image").setAttribute('src', _PREVIEW_URL);
        document.querySelector("#preview-image").style.display = 'none';
        document.querySelector("#delete-image").style.display = 'none';
        //$("#image-file").click();


    });





    // End Upload File 


    $(document).ready(function () {
        $('.download').click(function () {
            var inputValue = $(this).attr("value");
            var targetBox = $("." + inputValue);
            $(".download-form").not(targetBox).hide();
            $(targetBox).show();

        });
    });
    $(document).ready(function () {
        $('.cost').click(function () {
            var inputValue = $(this).attr("value");
            var targetBox = $("." + inputValue);
            $(".cost-form").not(targetBox).hide();
            $(targetBox).show();

        });
    });



    function themeColorConfig() { $(".header-default input").change(function () { $(".wrapper").removeClass("header-primary header-info header-success header-danger header-dark") }), $(".header-info input").change(function () { $(".wrapper").addClass("header-info"), $(".wrapper").removeClass("header-primary header-success header-danger header-dark") }), $(".header-primary input").change(function () { $(".wrapper").addClass("header-primary"), $(".wrapper").removeClass("header-info header-success header-danger header-dark") }), $(".header-success input").change(function () { $(".wrapper").addClass("header-success"), $(".wrapper").removeClass("header-info header-primary header-danger header-dark") }), $(".header-danger input").change(function () { $(".wrapper").addClass("header-danger"), $(".wrapper").removeClass("header-info header-primary header-success header-dark") }), $(".header-dark input").change(function () { $(".wrapper").addClass("header-dark"), $(".wrapper").removeClass("header-info header-primary header-success header-danger") }), $(".theme-colors.side-nav-dark input").change(function () { $(".app").addClass("side-nav-dark"), $(".app").removeClass("side-nav-default") }), $(".theme-colors.sidenav-default input").change(function () { $(".wrapper").addClass("side-nav-default"), $(".wrapper").removeClass("side-nav-dark") }), $("#rtl-toogle").on("click", function (e) { $(".wrapper").toggleClass("rtl"), e.preventDefault() }) } ! function () { sideNav(), sideNavToggle(), sidePanelToggle(), chatToggle(), todoToggle(), searchToggle(), advanceSearch(), themeConfig(), perfectSB(), cardPortletCtrl(), themeColorConfig() }()
}(jQuery);