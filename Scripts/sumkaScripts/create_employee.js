
var EmployeeCreator = {

    init: function () {
        $("#FirstName, #LastName").change(function () {
            $(".box_main_item_text h3").html($("#FirstName").val() + "  " + $("#LastName").val());
        });

        $("#Email").change(function () {
            $(".box_main_item_text span").html($(this).val());
        });
       
        //set src
        $('#templatesrc').load(function () {
            $(" .box_main_item_img img").attr("src", $(this.contentDocument).find('#ImageUploaded').attr("src"));
            $("#ImagePath").val($(this.contentDocument).find('#ImageUploaded').attr("src"));

        });
    }
};
