var ImageDialog = {

    preInit: function () {
        var url;

        tinyMCEPopup.requireLangPack();

        if (url = tinyMCEPopup.getParam("external_image_list_url"))
            document.write('<script language="javascript" type="text/javascript" src="' + tinyMCEPopup.editor.documentBaseURI.toAbsolute(url) + '"></script>');
    },
    init: function () {
        //image upload frame
        $("#uploadImage").attr('src', tinyMCEPopup.editor.documentBaseURI.toAbsolute(tinyMCE.Controller+'ImageUpload'));
    },
    initSliderThumbs: function (options) {

        var update = function (data) {
            this.ImageDialog.InitGalleria(data);
        };
        var url =tinyMCEPopup.editor.documentBaseURI.toAbsolute(options.ActionUrl);
        $.ajax({
            type: 'GET',
            url: url,
            dataType: "json",
            data: { "selectedFolder": options.SelectedFolder },
            success: function (data) {
                update(data);
            },
            error: function (error) {
                alert(error);
            }
        });

    },
  
    insert: function () {
        var imgSrc = ImageDialog.InsertImgSrc;
        tinyMCEPopup.execCommand('mceTemplateImageSelected', imgSrc);
        tinyMCEPopup.close();
    },
    InitGalleria: function (l) {

        if (l.length > 0) {

            var thumbCount = 0,
            containerId = "galleriaThumbs";

            //clearslider root tag
            $("#galleria").empty();

            //append    container
            var container = $("#galleria").append('<div id="' + containerId + '" style="height:400px;width:770px; background: #000; "></div>');

            //fill container
            tinymce.each(l, function (o) {
                ///put src
                var url = tinyMCEPopup.editor.documentBaseURI.toAbsolute(o.src);
                var thumb = "<img  src=" + url + "  alt=" + o.name + " /> "
                var thumbContainer = $("#" + containerId).append(thumb);
                //iterate thumbs
                thumbCount += 1;
            });

            //check thumb count
            if (thumbCount > 0) {
                //init slider
                this.InitGalleriaView(containerId);
            }

        }
    },
    InitGalleriaView: function (containerId) {
        $("#" + containerId).height = 400;
        Galleria.loadTheme(tinyMCEPopup.editor.baseURI.source + "/themes/advanced/js/galleria.classic.min.js");
        Galleria.run("#" + containerId, {
            transition: 'fade',
            width: 927,
            thumbCrop: true,
            height: 400
        });

        //set selection
        Galleria.ready(function () {
            this.bind('image', function (e) {
                ImageDialog.InsertImgSrc = e.imageTarget.src; // src is the currently showing image source
            });
        });
    }

};
ImageDialog.preInit();
tinyMCEPopup.onInit.add(ImageDialog.init, ImageDialog);
