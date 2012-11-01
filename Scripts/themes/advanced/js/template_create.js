var TemplateCreateDialog = {

    preInit: function () {
        var url;

        tinyMCEPopup.requireLangPack();

        if (url = tinyMCEPopup.getParam("external_link_list_url"))
            document.write('<script language="javascript" type="text/javascript" src="' + tinyMCEPopup.editor.documentBaseURI.toAbsolute(url) + '"></script>');
    },

    init: function () {
        var f = document.forms[0], ed = tinyMCEPopup.editor;

        // Setup browse button
        document.getElementById('hrefbrowsercontainer').innerHTML = getBrowserHTML('hrefbrowser', 'href', 'file', 'theme_advanced_link');
        if (isVisible('hrefbrowser'))
            document.getElementById('href').style.width = '180px';

        ///fill pages list
        this.fillFileList('link_list', 'tinyMCELinkList');

        if (e = ed.dom.getParent(ed.selection.getNode(), 'A')) {
            f.href.value = ed.dom.getAttrib(e, 'href');
            f.insert.value = ed.getLang('update');
            selectByValue(f, 'link_list', f.href.value);
        }


    },

    insert: function () {
        var template = {};
        template.name = $("#templateName").val();
        //check name
        if (template.name == "") {
            alert("Please enter new template name to continue");
            return;
        }

        //remove hover     
        template.html = $(":first", PageTemplateBuilder.BodySpan).removeClass("template-hover")[0].outerHTML;

        if (template.html == "") {
            alert("Sevected template not valid choose another");
            return;
        }

        tinyMCEPopup.editor.execCommand('mceTemplateCreated', template);
        tinyMCEPopup.close();
    },

    loadPoge: function (page) {
        var f = document.forms[0], href = f.href.value;
        var templteComtainer = $(".templatesLabel");
        var oldframe = $(page);
        var src = '<iframe id="templatePage" name="templatePage" style="width: 100%;min-height:200px;" frameborder="0" runat="server" src="' + href + '" ></iframe>';
        if (oldframe.length != 0) { oldframe.remove(); }

        templteComtainer.before(src);

        var ed = tinyMCEPopup.editor;
        $("#templatePage").load(function () {
            if (ed.Rowed) {
                PageTemplateBuilder.buildRows($(this.contentDocument));
            } else {
                PageTemplateBuilder.buildSpans($(this.contentDocument));
            }

        });
    },

    checkPrefix: function (n) {

    },

    fillFileList: function (id, l) {
        var dom = tinyMCEPopup.dom, lst = dom.get(id), v, cl;

        l = window[l];

        if (l && l.length > 0) {
            lst.options[lst.options.length] = new Option('', '');

            tinymce.each(l, function (o) {
                lst.options[lst.options.length] = new Option(o[0], o[1]);
            });
        } else
            dom.remove(dom.getParent(id, 'tr'));
    }

};
TemplateCreateDialog.preInit();
tinyMCEPopup.onInit.add(TemplateCreateDialog.init, TemplateCreateDialog);
