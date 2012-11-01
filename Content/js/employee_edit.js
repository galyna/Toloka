
var Employee = {
   
    addProductInit: function () {

        var employee = {
            employeeId: $(".addProduct")[0].alt
        };

        $(".addProduct").unbind("click");
        $(".addProduct").click(function () {
            $.ajax({
                type: "POST",
                url: "/Product/Create",
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
    },

    addPublishing: function () {
        if ($(".publishBtn").length>0) {
            var employee = {
                Id: $(".publishBtn")[0].alt
            };

            $(".publishBtn").unbind("click");
            $(".publishBtn").click(function () {
                $.ajax({
                    type: "POST",
                    url: "/Employee/Publish",
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

        if ($(".unpublishBtn").length > 0) {
            var employee = {
                Id: $(".unpublishBtn")[0].alt
            };

            $(".unpublishBtn").click(function () {
                $.ajax({
                    type: "POST",
                    url: "/Employee/Unpublish",
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
        $("#FirstName").change(function () {
            var first = $("#FirstName").val();
            var last = $("#LastName").val();
            $(".box_main_item_text .firstname").html(first);

            $(".addProduct").attr("title", "Додати продукт " + first + " " + last);

        });
        $("#LastName").change(function () {
            var first = $("#FirstName").val();
            var last = $("#LastName").val();
            $(".box_main_item_text .firstname").html(first);

            $(".addProduct").attr("title", "Додати продукт " + first + " " + last);
        });
        $("#Email").change(function () {
            $(".box_main_item_text .email").html($("#Email").val());
        });
            
    //  Employee.addProductInit();
     // Employee.addPublishing();
    }
};