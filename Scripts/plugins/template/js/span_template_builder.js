tinyMCEPopup.requireLangPack();


var SpanTemplateBuilder = {
    preInit: function () {

    },
    init: function () {

    },

    build: function () {
        var frame = window.frames['templatesrc'],
        spanEditorBuilder = this,
        options = {},
        template = frame.$('.template');
        //get options
        options.rootSpan = $("div:first-child", template)[0];
        options.spans = spanEditorBuilder.getSpans(options);
        //clear buttons bar
        $('#tmplBtns').empty();
        //fill spans
        $.each(options.spans, function (spanIndex) {
            spanEditorBuilder.buildSpan(spanIndex, options);
        });
    },

    buildSpan: function (spanIndex, options) {
        var span = options.spans[spanIndex],
        spanEditorBuilder = this;

        options.headers = $('h1,h2,h3,h4,h5,h6,span', span),
        options.texts = $('p', span),
        options.images = $('img', span),
        options.lists = $('ul', span);
        options.links = $('a', span);
        this.handleImages(options);
        this.handleHeaders(options);
        this.handleLists(options);
        this.handleTexts(options);

    },
    handleImages: function (options) {
        $.each(options.images, function (imageIndex) {
            var image = options.images[imageIndex],
              imgElement = $(image),
              url = "",
             name = "",
             imgOptions = {};
            imgOptions.mainImg = imgElement;


            url = tinyMCE.ContentRootFolder + "img" + imgElement.attr("src").split("/img")[1];
            name = (url.split('img/')[1]).split('.')[0];
            btn = '<img  src="' + url + '" id="' + 'btn' + name + '" tittle="Change image" style="width: 40px;height:40px;" />';


            $('#tmplBtns').append(btn);
            imgOptions.btnImg = $('#btn' + name);

            //bind click
            $('#btn' + name).bind("click", function () {
                TemplateDialog.initChangeImage(imgOptions);
            });
            imgElement.bind("click", function () {
                TemplateDialog.initChangeImage(imgOptions);
            });

            //bind hover
            SpanTemplateBuilder.bindOver(imgElement);
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
            SpanTemplateBuilder.bindOver(listElement);
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
            SpanTemplateBuilder.bindOver(headerElement);
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
            SpanTemplateBuilder.bindOver(textElement);
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
            SpanTemplateBuilder.bindOver(linkElement);
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
            SpanTemplateBuilder.bindOver(menuElement);
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

SpanTemplateBuilder.preInit();
tinyMCEPopup.onInit.add(SpanTemplateBuilder.init, SpanTemplateBuilder);
