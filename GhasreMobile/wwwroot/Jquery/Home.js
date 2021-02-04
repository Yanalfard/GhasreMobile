let last_known_scroll_position = 0;

window.addEventListener('scroll', function () {
    last_known_scroll_position = window.scrollY;

    if (last_known_scroll_position > 0) {
        document.getElementById('LayoutHeader').classList.add('layout-header-scrolled')
    }
    else {
        document.getElementById('LayoutHeader').classList.remove('layout-header-scrolled')
    }

});


var _smartsupp = _smartsupp || {};
_smartsupp.key = 'c7607366b0cb38ae98589e50eec801bed2bf7608';
window.smartsupp || (function (d) {
    var s, c, o = smartsupp = function () { o._.push(arguments) }; o._ = [];
    s = d.getElementsByTagName('script')[0]; c = d.createElement('script');
    c.type = 'text/javascript'; c.charset = 'utf-8'; c.async = true;
    c.src = 'https://www.smartsuppchat.com/loader.js?'; s.parentNode.insertBefore(c, s);
})(document);

$("#BtnSearch").click(function () {

    if ($("#inputSerach").val() == null || $("#inputSerach").val() == "") {
        return;
    } else {
        $("#BtnSearch").hide();
        window.location.href = "/Search=" + $("#inputSerach").val();
    }

});