tinyMCEPopup.requireLangPack();


var PageTemplateBuilder = {

    init: function () {

    },
    buildSpans: function (page) {
        var templatsFrame = $($("#templates")[0].contentDocument);
        var options = {};

        options.rootSpan = page.find("body");
        options.spans = PageTemplateBuilder.getSpans(options);

        //clear templatsFrame
        templatsFrame.find("body").empty();
        templatsFrame.find("head").empty();

        var ed = tinyMCEPopup.editor;

        //incert css
        tinymce.each(ed.getParam("content_css", '').split(','), function (u) {
            templatsFrame.find("head").append('<link href="' + ed.documentBaseURI.toAbsolute(ed.getParam("content_css")) + '" rel="stylesheet" type="text/css" />');
        });
        templatsFrame.find("body").append("<div class='span24'><div class='page-builder-container'></div></div>");
        PageTemplateBuilder.BodySpan = templatsFrame.find("body").find(".page-builder-container");


        $.each(options.spans, function (rowIndex) {
            var span = options.spans[rowIndex];

            var newElementsAppended = PageTemplateBuilder.BodySpan.append(span);
            PageTemplateBuilder.bindOver(span);
            PageTemplateBuilder.bindClick(span);
        });

    },
    buildRows: function (page) {
        var templatsFrame = $($("#templates")[0].contentDocument);
        var options = {};

        options.rootSpan = page.find("body");
        options.rows = PageTemplateBuilder.getRows(options);

        //clear templatsFrame
        templatsFrame.find("body").empty();
        templatsFrame.find("head").empty();

        var ed = tinyMCEPopup.editor;

        //incert css
        tinymce.each(ed.getParam("content_css", '').split(','), function (u) {
            templatsFrame.find("head").append('<link href="' + ed.documentBaseURI.toAbsolute(ed.getParam("content_css")) + '" rel="stylesheet" type="text/css" />');
        });
        templatsFrame.find("body").append("<div class='row'><div class='page-builder-container'></div></div>");
        PageTemplateBuilder.BodySpan = templatsFrame.find("body").find(".page-builder-container");


        $.each(options.rows, function (rowIndex) {
            var row = options.rows[rowIndex];

            var newElementsAppended = PageTemplateBuilder.BodySpan.append(row);
            PageTemplateBuilder.bindOver(row);
            PageTemplateBuilder.bindClick(row);
        });

    },
    insertRow: function (spans) {
        $.each(spans, function (spanIndex) {
            PageTemplateBuilder.bindClick(spans[spanIndex]);
        });
    },
   
    bindClick: function (element) {
      
        $(element).click(function () {
            PageTemplateBuilder.BodySpan.html($(element));

        });

    },
    bindOver: function (element) {
        $(element).mouseover(function () {
            $(element).addClass("template-hover");
        });
        $(element).mouseout(function () {
            $(element).removeClass("template-hover");
        });
    },
    getSpans: function (options) {
        var spans = $(".span8, .span6", options.rootSpan);
//        var spans = [];
//        spans.push($(" div[class='row'] >  div[class^='span8']", options.rootSpan));
//        spans.push($(" div[class='row'] >  div[class='span6']", options.rootSpan));
//        $.each(spans, function () {
//            var links = $("a", this).attr("disabled", true);
        //        });
        var links = $("a", spans).attr("href", "#");
        $.each(spans, function () {
            var links = $("a", this).attr("href", "#");
        });

        return spans;
    },
    getRows: function (options) {

        var rows = $("div[class='row'] > div[class^='span24']", options.rootSpan);
      
        if (rows.length === 0) {
            rows.push(options.rootSpan);
        }
        return rows;
    }
};

tinyMCEPopup.onInit.add(PageTemplateBuilder.init, PageTemplateBuilder);
