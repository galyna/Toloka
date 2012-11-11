﻿
var ProductCreator = {
    uploadBigImg: function () {

        $(".templateupload").dialog({
            autoOpen: false,
            height: 600,
            width: 700,
            modal: true,
            title: "Завантажити зображення",
            buttons: {
                "Зберегти": function () {
                    var image = top.frames["templatesrc"].document.getElementById('ImageUploaded');
                    $(" .uploadbigImg").attr("src", $(image).attr("src"));

                    $(this).dialog("close");

                },
                "Відміна": function () {
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

    addPublishing: function () {
        if ($(".publishBtn").length > 0) {
            var employee = {
                Id: $(".publishBtn")[0].alt
            };

            $(".publishBtn").unbind("click");
            $(".publishBtn").click(function () {
                $.ajax({
                    type: "POST",
                    url: "/Product/Publish",
                    data: employee,

                    dataType: "json",
                    success: function (data) {
                        window.location.reload(data);
                    },
                    failure: function () {
                        window.location.reload(true);
                    }
                });

            });
        }

        if ($(".unpublishBtn").length > 0) {
            var employee = {
                Id: $(".unpublishBtn")[0].alt
            };

            $(".unpublishBtn").click(function () {
                $.ajax({
                    type: "POST",
                    url: "/Product/Unpublish",
                    data: employee,

                    dataType: "json",
                    success: function (data) {
                        window.location.reload(true);
                    },
                    failure: function () {
                        window.location.reload(true);
                    }
                });

            });
        }

    },
    init: function () {
        $("#Name").change(function () {
            $(" .box_main_item_text h3").html($(this).val());
        });
        var description = $("#Price").change(function () {
            $(".box_main_item_text span").html($(this).val());
        }); ;

        //set src
        $('#templatesrc').load(function () {
            $(" .box_main_item_img img").attr("src", $(this.contentDocument).find('#ImageUploaded').attr("src"));
            $("#ImagePath").val($(this.contentDocument).find('#ImageUploaded').attr("src"));

        });

       // ProductCreator.uploadBigImg();
    }
};
