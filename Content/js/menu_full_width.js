//     MenuResizer.resize(".row", ".tabs_list.typ_sd");
var MenuResizer = {

    resize: function (menuContainerClass, listContainerClass, isBordered) {
        var menu = $(listContainerClass);
        var wrapper = $(menuContainerClass);
		
		if (typeof menu[0] == "undefined" || typeof wrapper[0] == "undefined") {
			return;
		}
		
        var menuItems = $('ul > li', menu);
        var numMenuItems = menuItems.length;
        var totalMenuItemWidth = this.countTotalMenuItemWidth(menuItems);
        var menuWidthRemainder = 0;
        var wrapperWidth = wrapper.width();
		var windowWidth = $(window).width();
	
		wrapperWidth = windowWidth < 987 ? windowWidth - 37 : 950;
		wrapper.width(wrapperWidth);
		
        if(isBordered) {
            wrapperWidth -= numMenuItems + 1; //950px+ 1px borders,
        }
        
        if (wrapperWidth != totalMenuItemWidth) {	
            this.castMinWidthToText(menuItems);
            this.increaseSize(menuItems, wrapperWidth, menu);
        }
    },
    increaseSize: function (menuItems, wrapperWidth, menu) {
        var totalMenuItemWidth = this.countTotalMenuItemWidth(menuItems),
        menuWidthRemainder = wrapperWidth - totalMenuItemWidth;

        menuItems.each(function () {
            var tmp_width = $(this).width();
            $(this).width(tmp_width + parseInt(menuWidthRemainder / menuItems.length));
        });
        //
        totalMenuItemWidth = this.countTotalMenuItemWidth(menuItems);
        menuWidthRemainder = wrapperWidth - totalMenuItemWidth;
        menuItems.each(function (i) {
            if (i == 0) {
                var tmp_width = $(this).width();
                $(this).width(tmp_width + menuWidthRemainder);
            }
        });
    },

    countTotalMenuItemWidth: function (menuItems) {
        var totalMenuItemWidth = 0;
        menuItems.each(function () {
            totalMenuItemWidth += $(this).width();
        });
        return totalMenuItemWidth;
    },

    castMinWidthToText: function (menuItems) {
        menuItems.each(function () {
			$(this).css("width", "auto");
        });
    }
};
