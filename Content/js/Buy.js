
var Buyer = {

    buy: function (obj) {
        var order = {
            Id: obj.alt
        };

        $.ajax({
            type: "GET",
            url: "/Order/Create",
            data: order,
            dataType: "json",
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

        $(".orderBtn").click(function () {
            Buyer.buy(this);
        });
        $(".deleteBtn").click(function () {
            Buyer.deleteFromBascet(this);
        });
        $(".makeOrder").click(function () {
            Buyer.makeOrderBtn(this);
        });


        $("#waterwheel-carousel-default").waterwheelCarousel({

            startingWaveSeparation: 0,
            centerOffset: 30,
            startingItemSeparation: 100,
            itemSeparationFactor: .7,
            itemDecreaseFactor: .75,
            opacityDecreaseFactor: 1,
            flankingItems: 6

        });
    }


};
