window.init = function () {
    var template = $('.template').html();
    var params = $('.parameters .parameter');
    function apply() {
        var txt = template;
        params.each(function () {
            txt = txt.replace(new RegExp('\\{\\$' + this.name + '\\}', 'g'), $(this).val());
        });
        $('.template').html(txt);
    }
    params.change(apply);
    $('.parameters .jscript').each(function () {
        var func = new Function($(this).html());
        func();
    });

}