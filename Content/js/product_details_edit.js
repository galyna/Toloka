
var ProductDetailEdit = {

    uploadBigImg: function () {

        $(".templateupload").dialog({
            autoOpen: false,
            height: 780,
            width: 750,
            modal: true,
            title: "Завантажити зображення",
            buttons: {
                "Зберегти": function () {
                    var image = top.frames["templatesrc"].document.getElementById('ImageUploaded');
                    $(" .uploadbigImg").attr("src", $(image).attr("src"));

                    $(this).dialog("close");

                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {

            }
        });

        $("#addImg")
            .click(function () {
                $(".templateupload").dialog("open");
            });
    },

    uploadSmallImg: function () {



        $(".templateuploadsmall").dialog({
            autoOpen: false,
            height: 400,
            width: 550,
            modal: true,
            title: "Завантажити зображення",
            buttons: {
                "Зберегти": function () {
                    var image = top.frames["templatesrcsmall"].document.getElementById('ImageUploaded');
                    var src = $(image).attr("src");
                    if ($(image).length > 0) {
                       // if ($(".addsmallImg .span3").length == 0 ) {
                            $(".addsmallImg ").append("<div class='span3' ><img id=" + src + " src=" + src + " /></div>");

                        //}
                       // else if ( $(".addsmallImg .span3").length % 6 == 0) {
                        //    $(".addsmallImg ").append("<div class='box_main_item span3' ><img id=" + src + " src=" + src + " /></div>");

                       // } else {
                       //     $(".addsmallImg ").append("<div class='box_main_item span3' ><img id=" + src + "  src=" + src + " /></div>");
                       // }
                    }
//                    var Dom = YAHOO.util.Dom,
//	                Event = YAHOO.util.Event;
//                    var crop = new YAHOO.widget.ImageCropper(src);
                    $(this).dialog("close");

                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {

            }
        });

        $("#addsmall")
            .click(function () {
                $(".templateuploadsmall").dialog("open");

            });
    },
    addText: function () {

        $(".paragraph").dialog({
            autoOpen: false,
            height: 300,
            width: 350,
            modal: true,
            title: "Додати текст",
            buttons: {
                "Зберегти": function () {
                    var text = $("#paragraphtext").val();
                    $(".paragraphs ").append("<p >" + text + " </p>");

                    $(this).dialog("close");

                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {

            }
        });

        $("#addtext")
            .click(function () {
                $(".paragraph").dialog("open");
            });
    },
    init: function () {
        ProductDetailEdit.uploadBigImg();
        ProductDetailEdit.uploadSmallImg();
        ProductDetailEdit.addText();

    }
};