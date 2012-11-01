//  MenuResizer.resize("tabs_wrap", "tabs_list");
var MenuResizer = {

    resize: function (menuOptions) {
        var frame = window.frames['templatesrc'],
         menu = frame.$('.' + menuOptions.listContainerClass),
         wrapper = frame.$("." + menuOptions.menuContainerClass),
         menuItems = $('ul > li', menu),
         numMenuItems = menuItems.length,
         totalMenuItemWidth = this.countTotalMenuItemWidth(menuItems),
         menuWidthRemainder = 0,
         wrapperWidth = 0;

        if (menuOptions.isBordered) {
            wrapperWidth = wrapper.width() - (numMenuItems + menuOptions.isBorderAdditional); //950px+ 1px borders,
        } else {
            wrapperWidth = wrapper.width(); //950px,   
        }

        /* Primary menu items (combined) are less width than the
        wrapper. */
        if (totalMenuItemWidth < wrapperWidth) {
            this.increaseSize(menuItems, wrapperWidth, menu);
        }
        /* Primary menu items (combined) are greater than the width of
        the wrapper. */
        else if (totalMenuItemWidth > wrapperWidth) {
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
            var tmp_width = $(this).text().length * 8;
            $(this).width(tmp_width);
        });
    }
};