
var ProductDetailEdit = {

    uploadBigImg: function () {
   /// fhfgjhfgh
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

    uploadSmallImg: function () {
        $(".imgbigshow").dialog({
            autoOpen: false,
            height: 600,
            width: 620,
            modal: true,
            close: function () {

            }
        });


        $(".templateuploadsmall").dialog({
            autoOpen: false,
            height: 350,
            width: 550,
            modal: true,
            title: "Завантажити зображення",
            buttons: {
                "Зберегти": function () {
                    var image = top.frames["templatesrcsmall"].document.getElementById('ImageUploaded');
                    var src = $(image).attr("src");
                    if ($(image).length > 0) {
                        $(".addsmallImg ").append("<div class='span3 box_thumb_item'   ><img class='thumb'   src=" + src + " /><img  class='removebtn'   src='../../Content/img/q/delete_small.png'/></div>");
                        var last = $(".addsmallImg :last-child");

                        $(".removebtn").click(function () {
                            $(this).parent().remove();
                        });

                        $(last).click(function () {
                            var smallsrc = $(".thumb", $(this)).attr("src");

                            var srcbig = smallsrc.replace("imgThumbs", "imgFull");
                            $("#imgbig").attr("src", srcbig)
                            $(".imgbigshow").dialog("open");
                        });

                    }

                    $(this).dialog("close");

                },
                "Відміна": function () {
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
                    $("#paragraphtext").val("");
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

    save: function () {
        $(".removebtn").remove();

        var model = {
            Id: $("#Id").val(),
            HtmlDetail: $(".htmldetail").html()
        };

        var reuqest = $.ajax({
            type: "post",
            url: "/Product/EditDetails",
            data: model,
            dataType: "json",
            success: function (data) {

                window.location.reload(data);
              
            },
            failure: function () {
                window.location.reload(true);
            }
        });

        $("#addtext")
            .click(function () {
                $(".paragraph").dialog("open");
            });
        },
    deleteSmallImg: function () {

        $(".span3.box_thumb_item").append("<img  class='removebtn'   src='../../Content/img/q/delete_small.png'/>");
          $(".removebtn").click(function () {
                            $(this).parent().remove();
                        });
    },

    init: function () {
        ProductDetailEdit.uploadBigImg();
        ProductDetailEdit.uploadSmallImg();
        ProductDetailEdit.addText();
        ProductDetailEdit.deleteSmallImg();
        $("#save")
            .click(function () {
                ProductDetailEdit.save();
            });

    }
};