tinyMCEPopup.requireLangPack();


var RowTemplateBuilder = {
    preInit: function () {

    },
    init: function () {

    },
    build: function (options) {
        var frame = window.frames['templatesrc'],
        spanEditorBuilder = this,
        options = {},

        template = frame.$('.template');
        options.rootSpan = $("div:first-child", template)[0];
        options.spans = this.getSpans(options);

        $.each(options.spans, function (spanIndex) {
            spanEditorBuilder.buildSpan(spanIndex, options);
        });
    },
    buildSpan: function (spanIndex, options) {
        var span = options.spans[spanIndex],
        spanEditorBuilder = this;
        options.headers = $('h1,h2,h3,span', span),
        options.texts = $('p', span),
        options.images = $('img', span),
        options.lists = $('ul', span);
        options.links = $('a', span);
        this.handleImages(options);
        this.handleHeaders(options);
        this.handleLists(options);
        this.handleTexts(options);
       // this.handleLinks(options);

    },
    handleImages: function (options) {
        $.each(options.images, function (imageIndex) {
            var image = options.images[imageIndex],
              imgElement = $(image),
            url = "";
            //update View 
            if (tinymce.isIE) {
                url = tinyMCE.ContentRootFolder + "img" + imgElement.attr("src").split("/img")[1];
            } else {
                url = tinyMCE.ContentRootFolder + imgElement.attr("src").substring("/img/");
            }

            imgElement.attr("src", url);
            //bind click
            imgElement.bind("click", function () {
                TemplateDialog.initChangeImage(image);
            });
            //bind hover
            RowTemplateBuilder.bindOver(imgElement);

        });
    },
    handleLists: function (options) {
        $.each(options.lists, function () {
            var list = this,
                listElement = $(list);
            //bind click
            listElement.bind("click", function () {
                TemplateDialog.initEditList(this);
            });
            //bind hover
            RowTemplateBuilder.bindOver(listElement);
        });
    },
    handleHeaders: function (options) {
        $.each(options.headers, function (headerIndex) {
            var header = options.headers[headerIndex],
                headerElement = $(header);
            //bind click
            headerElement.bind("click", function () {
                TemplateDialog.initEditHeader(this);
            });
            //bind hover
            RowTemplateBuilder.bindOver(headerElement);
        });
    },
    handleTexts: function (options) {
        $.each(options.texts, function (textIndex) {
            var text = options.texts[textIndex],
                textElement = $(text);
            //bind click
            textElement.bind("click", function () {
                TemplateDialog.initEditText(this);
            });
            //bind hover
            RowTemplateBuilder.bindOver(textElement);
        });
    },
    handleLinks: function (options) {
        $.each(options.links, function (linkIndex) {
            var link = options.links[linkIndex],
                linkElement = $(link);
            //bind click
            linkElement.bind("click", function () {
                TemplateDialog.initEditLink(this);
            });
            //bind hover
            RowTemplateBuilder.bindOver(linkElement);
        });
    },
    handleMenus: function (options) {
        $.each(options.menus, function (index) {
            var menu = options.menus[index],
                menuElement = $(menu);
            //bind click
            linkElement.bind("click", function () {
                TemplateDialog.initEditMenu(this);
            });
            //bind hover
            RowTemplateBuilder.bindOver(menuElement);
            var menuItemOptions = {};
            menuItemOptions.menu = target.parent;
            menuItemOptions.container = target;
            menuItemOptions.isBordered = true;
            menuItemOptions.isBorderAdditional = 15;
            MenuResizer.resize(menuItemOptions);
        });
    },
    bindOver: function (element) {
        element.bind("mouseover", function () {
            $(element).addClass("template-hover");
        });
        element.bind("mouseout", function () {
            $(element).removeClass("template-hover");
        });
    },
    getSpans: function (options) {
        spans = $("div[class^='span']", options.rootSpan);

        if (spans.length === 0) {
            spans.push(options.rootSpan);
        }
        return spans;
    },
    getRootSpans: function (options) {
        var options = options;
        spans = $("div[class^='span']", options.rootSpan);
        return spans;
    }
};

RowTemplateBuilder.preInit();
tinyMCEPopup.onInit.add(RowTemplateBuilder.init, RowTemplateBuilder);
