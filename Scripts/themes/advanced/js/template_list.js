tinyMCEPopup.requireLangPack();


var SpanTemplateList = {

    init: function () {
        SpanTemplateList.updateIndexSelector(tinyMCEPopup.editor.changeList, $('#select_list'));
    },
    create: function () {
        var itemName = $('#new_list_item_name').val(),
        optionsLength = $('option', '#select_list').length;
        if (itemName === "") {
            alert("New item Name is empty!");
            return;
        }

        $('#select_list').append($('<option value="' + optionsLength + '">' + itemName + '</option>'));
        $('#new_list_item_name').val("");
    },
    remove: function () {
        var index = $(" option:selected", $('#select_list')).remove();
    },
    insert: function () {

        tinyMCEPopup.execCommand('mceTemplateListEdited', $('#select_list'));
        tinyMCEPopup.close();
    },
    updateIndexSelector: function () {
        var ul = tinyMCEPopup.editor.changeList,
        selectlist = $('#select_list');
        selectlist.empty();
        for (var i = 0; i < $('li', ul).length; i++) {
            var itemName = $('li', ul)[i].innerText;
            selectlist.append($('<option value="' + i + '">' + itemName + '</option>'));
        }
    }
};

tinyMCEPopup.onInit.add(SpanTemplateList.init, SpanTemplateList);
