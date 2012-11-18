
var Category = {


    init: function () {


        // This initialises carousels on the container elements specified, in this case, carousel1.
        $("#carousel1").CloudCarousel(
		{
		    xPos: 328,
		    yPos: 32,
		    buttonLeft: $("#left-but"),
		    buttonRight: $("#right-but"),
		    altBox: $("#alt-text"),
		    titleBox: $("#title-text"),
		    minScale: 0.5,
		    xRadius: 370,
            yRadius:20,
            xPos:320
         
		});
    }


};
