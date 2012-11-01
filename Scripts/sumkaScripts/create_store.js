var StoreCreator = {

    init: function () {
        $("#Name").change(function () {
            $(" .box_main_item_img_bg span").html($(this).val());
            $(" .box_main_item_text h3").html($(this).val());
        });
        var description = $("#Description").change(function () {
            $(".box_main_item_text span").html($(this).val());
        }); ;

        //set src
        $('#templatesrc').load(function () {
            $(".store .box_main_item_img img").attr("src", $(this.contentDocument).find('#ImageUploaded').attr("src"));
            $(".store #ImagePath").val($(this.contentDocument).find('#ImageUploaded').attr("src"));
        });
    }
};