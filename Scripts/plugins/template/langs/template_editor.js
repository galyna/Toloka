tinyMCEPopup.requireLangPack();


var TemplateEditor = {
    preInit: function () {
        var url = tinyMCEPopup.getParam("template_external_list_url");

        if (url != null)
            document.write('<sc' + 'ript language="javascript" type="text/javascript" src="' + tinyMCEPopup.editor.documentBaseURI.toAbsolute(url) + '"></sc' + 'ript>');
    },

    init: function () {
        var ed = tinyMCEPopup.editor, tsrc, sel, x, u;

        tsrc = ed.getParam("template_templates", false);
        sel = document.getElementById('tpath');

        // Setup external template list
        if (!tsrc && typeof (tinyMCETemplateList) != 'undefined') {
            for (x = 0, tsrc = []; x < tinyMCETemplateList.length; x++)
                tsrc.push({ title: tinyMCETemplateList[x][0], src: tinyMCETemplateList[x][1], description: tinyMCETemplateList[x][2] });
        }

        for (x = 0; x < tsrc.length; x++)
            sel.options[sel.options.length] = new Option(tsrc[x].title, tinyMCEPopup.editor.documentBaseURI.toAbsolute(tsrc[x].src));

        this.resize();
        this.tsrc = tsrc;

    },
    createMenuItem: function (createMenuItemOptions) {
        var options = createMenuItemOptions;
        if (createMenuItemOptions.Name === "") {
            alert("Menu item Name is empty!");
            return;
        }

        var frame = window.frames['templatesrc'],
         menu = frame.$('.' + createMenuItemOptions.listContainerClass),
         indexList = frame.$('#' + createMenuItemOptions.indexesListSelectClass);
        var index = $(" option:selected", indexList).val();
        var secondElement = $('ul > li', menu)[index];
        //insert menu item
        var insertBeforeIndex = frame.$("." + createMenuItemOptions.InsertBeforeIndexClass + ":checked").length;
        if (insertBeforeIndex === 1) {
            $(secondElement).before('<li><a href="#">' + createMenuItemOptions.Name + '</a></li> ');
        } else {
            $(secondElement).after('<li><a href="#">' + createMenuItemOptions.Name + '</a></li> ');
        }

        //clear select indexes
        this.updateIndexSelector(menu, indexList);
        //resize
        MenuResizer.resize(createMenuItemOptions);
    },
    resizeMenu: function (menuItemOptions) {
        //resize
        MenuResizer.resize(menuItemOptions);
    },
    makeMenuItemActive: function (menuItemOptions) {
        var frame = window.frames['templatesrc'],
          menu = frame.$('.' + menuItemOptions.listContainerClass),
          indexList = frame.$('#' + menuItemOptions.indexesListSelectClass),
          selectedMenuItemIndex = $(" option:selected", indexList).val(),
          menuItems = $('ul > li', menu);
        $.each(menuItems, function (index, item) {
            //remove active class
            $(this).removeClass(menuItemOptions.activeClass);
            //add active class if index equals to selected in dropdown of menu items indexes
            if (index == selectedMenuItemIndex) {
                $(this).addClass(menuItemOptions.activeClass);
            }

        });

    },
    removeMenuItem: function (removeMenuItemOptions) {
        var frame = window.frames['templatesrc'],
         menu = frame.$('.' + removeMenuItemOptions.listContainerClass),
         indexList = frame.$('#' + removeMenuItemOptions.indexesListSelectClass);
        var index = $(" option:selected", indexList).val();
        var secondElement = $('ul > li', menu)[index];
        secondElement.parentNode.removeChild(secondElement)
        //clear select indexes
        this.updateIndexSelector(menu, indexList);
        //resize
        MenuResizer.resize(removeMenuItemOptions);
    },
    updateIndexSelector: function (menu, indexList) {
        indexList.empty();
        for (var i = 0; i < $('ul > li', menu).length; i++) {
            var itemName = $('ul > li', menu)[i].innerText;
            indexList.append($('<option value="' + i + '">' + itemName + '</option>'));
        }
    },
    updateImage: function (imgOptions) {
        var frame = window.frames['templatesrc'];
        var url = tinyMCEPopup.editor.documentBaseURI.source + imgOptions.ActionCall;
        var html = frame.$('.' + imgOptions.ImgContainerClass + " img").attr("src", url);
    },
    initChangeImage: function (imgOptions) {
        var ed = tinyMCEPopup.editor;
        var options = imgOptions;
        tinyMCEPopup.editor.changeImgClassName = options.ImgContainerClass;

        ed.windowManager.open({
            url: ed.theme.url + '/template_image.htm',
            width: ed.getParam('template_popup_width', 970),
            height: ed.getParam('template_popup_height', 600),
            inline: true
        }, {
            theme_url: ed.theme.url
        });
        if (options.IsInnerImg) {
            ed.addCommand('mceTemplateImageSelected', this.imageSelectedForImg);
        } else {
            ed.addCommand('mceTemplateImageSelected', this.imageSelectedForBg);
        }

    },

    SetBackgroundContainer: function (bgOptions) {
        var options = bgOptions;
        var frame = window.frames['templatesrc'];
        var classList = frame.$('.' + options.BgContainerClass).attr('class').split(/\s+/);
        $.each(classList, function (index, item) {
            var itemIsOld = true;
            for (var i = 0; i < options.notBackgroundClasses.length; i++) {
                if (item === options.notBackgroundClasses[i]) {
                    itemIsOld = false;
                }
            }

            if (itemIsOld) {
                frame.$('.' + options.BgContainerClass).removeClass(item);
            }

        });

        frame.$('.' + options.notBackgroundClasses).addClass(options.newBackgroundClass);
    },

    imageSelectedForBg: function (imageUrl) {
        var src = imageUrl;
        var elementclassName = tinyMCEPopup.editor.changeImgClassName;
        var frame = window.frames['templatesrc'];
        var url = tinyMCEPopup.editor.documentBaseURI.toAbsolute(src);
        var html = frame.$('.' + elementclassName).css("background-image", "url(" + url + ")");

    },

    imageSelectedForImg: function (imageUrl) {
        var src = imageUrl;
        var elementclassName = tinyMCEPopup.editor.changeImgClassName;
        var frame = window.frames['templatesrc'];
        var url = tinyMCEPopup.editor.documentBaseURI.toAbsolute(src);
        var html = frame.$('.' + elementclassName + " img").attr("src", url);
    },

    initTemplateLink: function (linkOptions) {
        var ed = tinyMCEPopup.editor;
        tinyMCEPopup.editor.LinkClass = linkOptions.LinkClass;

        ed.windowManager.open({
            url: ed.theme.url + '/template_link.htm',
            width: 310 + parseInt(ed.getLang('advanced.link_delta_width', 0)),
            height: 200 + parseInt(ed.getLang('advanced.link_delta_height', 0)),
            inline: true
        }, {
            theme_url: ed.theme.url
        });


        ed.addCommand('mceTemplatelinkSelected', this.linkSelectedCallback);
    },
    linkSelectedCallback: function (link) {
        var _link = link;
        var linkClassName = tinyMCEPopup.editor.LinkClass;
        var frame = window.frames['templatesrc'];
        var html = frame.$('.' + linkClassName).attr("href", _link);
    },

    resize: function () {
        var w, h, e;

        if (!self.innerWidth) {
            w = document.body.clientWidth - 50;
            h = document.body.clientHeight - 160;
        } else {
            w = self.innerWidth - 50;
            h = self.innerHeight - 170;
        }

        e = document.getElementById('templatesrc');

        if (e) {
            e.style.height = Math.abs(h) + 'px';
            e.style.width = Math.abs(w - 5) + 'px';
        }
    },

    loadCSSFiles: function (d) {
        var ed = tinyMCEPopup.editor;

        tinymce.each(ed.getParam("content_css", '').split(','), function (u) {
            d.write('<link href="' + ed.documentBaseURI.toAbsolute(u) + '" rel="stylesheet" type="text/css" />');
        });
        d.write('<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>');
        d.write('<script type="text/javascript" src="js/apply.js"></script>');
    },

    selectTemplate: function (u, ti) {
        var d = window.frames['templatesrc'].document, x, tsrc = this.tsrc;

        if (!u)
            return;

        d.body.innerHTML = this.templateHTML = this.getFileContents(u);
        // d.styleSheets[1].addImport(u.replace("GetTemplate", "GetTemplateCss"),6);

        for (x = 0; x < tsrc.length; x++) {
            if (tsrc[x].title == ti)
                document.getElementById('tmpldesc').innerHTML = tsrc[x].description || '';
        }
        if (window.frames['templatesrc'].init)
            window.frames['templatesrc'].init();

        var stylesList = $('#style_list');
    },

    insert: function () {
        var frame = window.frames['templatesrc'];
        var html = frame.$('.template').html();
        tinyMCEPopup.execCommand('mceInsertTemplate', false, {
            content: html, // this.templateHTML,
            selection: tinyMCEPopup.editor.selection.getContent()
        });

        tinyMCEPopup.close();
    },

    getFileContents: function (u) {
        var x, d, t = 'text/plain';

        function g(s) {
            x = 0;

            try {
                x = new ActiveXObject(s);
            } catch (s) {
            }

            return x;
        };

        x = window.ActiveXObject ? g('Msxml2.XMLHTTP') || g('Microsoft.XMLHTTP') : new XMLHttpRequest();

        // Synchronous AJAX load file
        x.overrideMimeType && x.overrideMimeType(t);
        x.open("GET", u, false);
        x.send(null);

        return x.responseText;
    }
};

TemplateDialog.preInit();
tinyMCEPopup.onInit.add(TemplateDialog.init, TemplateDialog);
