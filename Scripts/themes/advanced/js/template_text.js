tinyMCEPopup.requireLangPack();


var SpanTemplateText = {
    init: function () {
        document.getElementById('tittle').value = tinyMCEPopup.editor.PText;
    },
    insert: function () {

        tinyMCEPopup.execCommand('mceTemplateTextEdited', document.getElementById('tittle').value);
        tinyMCEPopup.close();
    }
};

tinyMCEPopup.onInit.add(SpanTemplateText.init, SpanTemplateText);
