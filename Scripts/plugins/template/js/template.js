tinyMCEPopup.requireLangPack();


var TemplateDialog = {
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

        //tmplate  create
        var frame = window.frames['templatesrc'],
        tmplcreate = $('#tmplcreate');
        tmplcreate.bind("click", function () {
            TemplateDialog.initTemplateCreate();
        });

        //add commands


        ed.addCommand('mceTemplateInitEditHtml', TemplateDialog.initEditHtml);

        ed.addCommand('mceWebTemplateSave', TemplateDialog.saveWebTemplate);
    },
    // building start

    //builder calls
    //TemplateCreate start
    initTemplateCreate: function () {
        var ed = tinyMCEPopup.editor;
        var rowedLength = $("input[name='rowed']:checked").length;
        var template = {};
        ed.Rowed = false;
        if (rowedLength === 1) {
            ed.Rowed = true;
        }

        ed.windowManager.open({
            url: ed.theme.url + '/template_create.htm',
            width: 1000,
            height: 850,
            inline: true
        }, {
            theme_url: ed.theme.url
        });
        // add command   and callback
        ed.addCommand('mceTemplateCreated', TemplateDialog.initNewTemplate);
    },
    initNewTemplate: function (templateSourse) {
        var tsrc = this.tsrc,
          template = {};

            template.html = "<div class='template' style='width:970px; height:10px;'> " + templateSourse.html + "</div>";
       

        var d = window.frames['templatesrc'].document;

        d.body.innerHTML = template.html;
        template.name = templateSourse.name

        if (window.frames['templatesrc'].init)
            window.frames['templatesrc'].init();

        //build    SpanTemplateBuilder
        SpanTemplateBuilder.build();
        //save Web template
        tinyMCEPopup.execCommand('mceWebTemplateSave', template);
    },
    createWebTemplate: function (template) {
        return '<div class="parameters" ><div style="display: none" class="jscript"> $(function() { parent.TemplateDialog.buildTemplate();});</div></div>' + template;
    },
    saveWebTemplate: function (template) {
        var webTemplateHtml = template.html,
       url = tinyMCEPopup.editor.documentBaseURI.toAbsolute(tinyMCE.Controller+"SaveWebTemplate"),
        webTemplateName = template.name
        $.ajax({
            type: 'GET',
            url: url,
            dataType: "json",
            data: { "html": webTemplateHtml, "name": webTemplateName },
            success: function (data) {
                TemplateDialog.updateTemplatesList(data);
            },
            error: function (error) {
                alert(error);
            }
        });

    },
    updateTemplatesList: function (data) {
        var sel = $('#tpath').empty();
        var lastIndex = {};
        for (x = 0; x < data.templates.length; x++) {
            option = new Option(data.templates[x].title, tinyMCEPopup.editor.documentBaseURI.toAbsolute(data.templates[x].src))
            sel.append(option);
            lastIndex = x;
        }
        //set selection
        $($('#tpath option').get(lastIndex)).attr('selected', 'selected');
        $('#tpath').attr("selectedIndex", lastIndex)

    },
    //TemplateCreate end
    //builder calls
    //image handling start
    initChangeImage: function (image) {
        var ed = tinyMCEPopup.editor;
        tinyMCEPopup.editor.changeImg = image;

        ed.windowManager.open({
            url: ed.theme.url + '/template_image.htm',
            width: 1000,
            height: 650,
            inline: true
        }, {
            theme_url: ed.theme.url
        });
        // add command   and callback
        ed.addCommand('mceTemplateImageSelected', TemplateDialog.imageSelected);
    },
    imageSelected: function (imageSrc) {
        var image = tinyMCEPopup.editor.changeImg;
        var frame = window.frames['templatesrc'];
        var url = tinyMCEPopup.editor.documentBaseURI.toAbsolute(imageSrc);
        //set src
        frame.$(image.mainImg).attr("src", url);
        $(image.btnImg).attr("src", url);
    },
    //image handling end

    //header handling start
    initEditHeader: function (header) {
        var ed = tinyMCEPopup.editor;
        tinyMCEPopup.editor.changeHeader = header;
        tinyMCEPopup.editor.headerText = header.innerText;
        ed.windowManager.open({
            url: ed.theme.url + '/template_header.htm',
            width: 300,
            height: 160,
            inline: true
        }, {
            theme_url: ed.theme.url
        });

        // add command   and callback
        ed.addCommand('mceTemplateHeaderEdited', TemplateDialog.changeHeader);
    },
    changeHeader: function (text) {
        tinyMCEPopup.editor.changeHeader.innerText = text;
    },
    //header handling end

    //text handling start
    initEditText: function (text) {
        var ed = tinyMCEPopup.editor;
        tinyMCEPopup.editor.changeHeader = text;
        tinyMCEPopup.editor.PText = text.innerText;
        ed.windowManager.open({
            url: ed.theme.url + '/template_text.htm',
            width: 350,
            height: 250,
            inline: true
        }, {
            theme_url: ed.theme.url
        });

        // add command   and callback
        ed.addCommand('mceTemplateTextEdited', TemplateDialog.changeText);
    },
    changeText: function (text) {
        tinyMCEPopup.editor.changeHeader.innerText = text;
    },
    //text handling end

    //list handling start
    initEditList: function (list) {
        var ed = tinyMCEPopup.editor;
        tinyMCEPopup.editor.changeList = list;

        ed.windowManager.open({
            url: ed.theme.url + '/template_list.htm',
            width: 330,
            height: 260,
            inline: true
        }, {
            theme_url: ed.theme.url
        });

        // add command   and callback
        ed.addCommand('mceTemplateListEdited', TemplateDialog.changeList);
    },
    changeList: function (selectList) {
        var frame = window.frames['templatesrc'],
        list = $('li', tinyMCEPopup.editor.changeList).clone(),
        li = list[0],
         target = $(tinyMCEPopup.editor.changeList).empty();

        $.each($('option', selectList), function (index, listOptionet) {
            li.innerText = this.innerText;
            target.append(li.outerHTML);
        });


    },
    //list handling end

    //link handling start
    initEditLink: function (link) {
        var ed = tinyMCEPopup.editor;
        tinyMCEPopup.editor.Link = link;

        ed.windowManager.open({
            url: ed.theme.url + '/template_link.htm',
            width: 600,
            height: 200,
            inline: true
        }, {
            theme_url: ed.theme.url
        });
        ed.addCommand('mceTemplatelinkSelected', TemplateDialog.linkSelectedCallback);
    },
    linkSelectedCallback: function (href) {
        var link = tinyMCEPopup.editor.Link,
        frame = window.frames['templatesrc'];
        var html = frame.$(link).attr("href", href);
    },
    //link handling end

    //Finall html handling start
    initEditHtml: function (html) {
        var ed = tinyMCEPopup.editor;

        tinyMCEPopup.editor.ThmplateHtml = html;
        tinyMCE.closeOnLoad = true;
        ed.windowManager.open({
            url: ed.theme.url + '/source_editor.htm',
            width: 600,
            height: 400,
            inline: true
        }, {
            theme_url: ed.theme.url
        });


        tinyMCEPopup.close();
    },

    //Finall html handlingend

    // menus handling
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
        var url = tinyMCE.Host + imgOptions.ImgSrc;
        var html = frame.$('.' + imgOptions.ImgContainerClass + " img").attr("src", url);
        tinyMCEPopup.editor.changeImgClassName = 'mceIcon';
        var imageUrl = tinyMCEPopup.editor.baseURI.source + "/themes/advanced/img/icons.gif";
        this.imageSelectedForBg(imageUrl);
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

    //menu end

    //template original
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
        for (x = 0; x < tsrc.length; x++) {
            if (tsrc[x].title == ti)
                document.getElementById('tmpldesc').innerHTML = tsrc[x].description || '';
        }
        if (window.frames['templatesrc'].init)
            window.frames['templatesrc'].init();

        //build
        SpanTemplateBuilder.build();
    },

    insert: function () {
        var frame = window.frames['templatesrc'];
        var html = frame.$('.template').html();
        var rowedLength = $("input[name='rowed']:checked").length;
        var template = {};
        if (rowedLength === 1) {
            template = tinyMCEPopup.editor.getContent() + "<div class='row'> " + html + "</div>";
        } else {
            template = tinyMCEPopup.editor.getContent() + html;
        }

        tinyMCEPopup.editor.setContent(template);
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
