
var Admin = {

    upload: function (obj) {


        $.ajax({
            type: "get",
            url: "/Store/Create",
            success: function (data) {
                if (data != null) {
                    window.location.reload(data.Url);
                } else {
                    window.location.reload(true);
                }
            },
            failure: function () {
                window.location.reload(true);
            }
        });
    },

    makeOrderBtn: function (btn) {
        var orderTag = $('.order' + btn.alt).parent()[0].nextElementSibling.innerHTML;
        var id = $(orderTag)[0].value;
        var order = {
            OrderId: id,
            Comments: $("." + id + "comments").val(),
            Email: $("." + id + "email").val()
        };

        $.ajax({
            type: "post",
            url: "/Order/MakeOrder",
            data: order,
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    location.href = data;

                }

            }
        });
    },
    deleteFromBascet: function (btn) {
        var orderTag = $('.order' + btn.alt).parent()[0].nextElementSibling.innerHTML;
        var id = $(orderTag)[0].value;
        var order = {
            id: id
        };

        $.ajax({
            type: "post",
            url: "/Order/Delete",
            data: order,
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    location.href = data;

                }

            }
        });
    },

    init: function () {

    }
};
