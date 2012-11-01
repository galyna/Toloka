function getCoords(elem) {
    var box = elem.getBoundingClientRect();

    var body = document.body;
    var docElem = document.documentElement;

    var scrollTop = window.pageYOffset || docElem.scrollTop || body.scrollTop;
    var scrollLeft = window.pageXOffset || docElem.scrollLeft || body.scrollLeft;

    var clientTop = docElem.clientTop || body.clientTop || 0;
    var clientLeft = docElem.clientLeft || body.clientLeft || 0;

    var top  = box.top +  scrollTop - clientTop;
    var left = box.left + scrollLeft - clientLeft;

    return { top: Math.round(top), left: Math.round(left) };
}
/**
 * Перевіряємо елемент чи попадає у видиму частину екрану
 * Для попадяння достатньо, щоб верхня або нижня границя елемента була видна.
 */
function isVisible(elem) {

    var coords = getCoords(elem);
    var windowTop = window.pageYOffset || document.documentElement.scrollTop;
    var windowBottom = windowTop + document.documentElement.clientHeight;

    coords.bottom = coords.top + elem.offsetHeight;

    // Верхея границя elem попала у виидиму частину АБО нижня границя видима
    var topVisible = coords.top > windowTop && coords.top < windowBottom;
    var bottomVisible = coords.bottom < windowBottom && coords.bottom > windowTop;

    return topVisible || bottomVisible;
}
function showVisible() {
    var divs = document.getElementsByTagName('div');
    for(var b=0; b < divs.length; b++) {
        var div = divs[b];
        var title = div.getAttribute('title');
        if (!title) continue;
        if (isVisible(div)) {
            div.className = div.className + ' ' + title;
            div.setAttribute('title', '');
        }
    }

}

window.onscroll = showVisible;
showVisible();

var $menu = $('#menu_fix'), MenuFix = $menu.offset().top;
    $(window).scroll(function() {
        if ($(window).scrollTop() >= MenuFix) {
            $menu.addClass("menu_fix_active");
        }
        else {
            $menu.removeClass("menu_fix_active");
        }
    });
