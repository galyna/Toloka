tinyMCEPopup.requireLangPack();


var SpanTemplateHeader = {
    init: function () {
        document.getElementById('tittle').value = tinyMCEPopup.editor.headerText;
    },
    insert: function () {

        tinyMCEPopup.execCommand('mceTemplateHeaderEdited', document.getElementById('tittle').value);
        tinyMCEPopup.close();
    }
};

tinyMCEPopup.onInit.add(SpanTemplateHeader.init, SpanTemplateHeader);
