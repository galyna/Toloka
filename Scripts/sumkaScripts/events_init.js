window.onload = window.onresize = function(event) {
    /*tabs*/
    MenuResizer.resize(".tabs_wrap", ".tabs_list");
    /*Filter menu*/
    MenuResizer.resize(".menu_filter_wrap", ".menu_filter_list");
    /*Menu top*/
    MenuResizer.resize(".menu_top", ".menu_top_list", true);
}

$(function () {
    
    // Company Management portlet 
    
    if (isSelectorExists(".company_managmen_wrap")) {
        $(".company_managmen_wrap").click(function () {
            $(this).addClass("active");
        });        
    }
    
    // R&D Team Person portlet 
    
    if (isSelectorExists(".information_person_wrap")) {
        $(".information_person_wrap").each(function (index) {
            if (index % 3 == 0) {
                $(this).addClass("first");
            } else if (index % 3 == 2) {
                $(this).addClass("last");
            }
        });
        
        $(".information_person_img_info").click(function () {
            $(this).closest(".information_person_wrap").addClass("active");
        });
        
        $(".information_person_about .close").click(function () {
            $(this).closest(".information_person_wrap").removeClass("active");
        });
    }
    
    // Contact Us Maps
    
    if (isSelectorExists("#google_map_wrap")) {
        var places = [
            {
                coord: new google.maps.LatLng(49.80254124506291, 24.00038480758667),
                icon: "/Themes/EleksCookiesTheme/Content/img/contact_us/icon_office.png"
            },
            {
                coord: new google.maps.LatLng(36.026642, -115.086801),
                icon: "/Themes/EleksCookiesTheme/Content/img/contact_us/icon_star.png"
            },
            {
                coord: new google.maps.LatLng(42.485102, -71.208921),
                icon: "/Themes/EleksCookiesTheme/Content/img/contact_us/icon_star.png"
            }
        ];
        
        var myOptions = {
            zoom: 3,
            center: new google.maps.LatLng(0, 0),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        
        var map = new google.maps.Map(document.getElementById("google_map_wrap"), myOptions);
        var bounds = new google.maps.LatLngBounds();
        
        
        for (i = 0; i < places.length; i++) {
            var marker = new google.maps.Marker({
                position: places[i].coord,
                icon: places[i].icon,
                map: map
            });
            
            google.maps.event.addListener(marker, 'click', function() {
                this.map.setZoom(10);
                this.map.setCenter(this.getPosition());
            });
            
            bounds.extend(places[i].coord);
        }
        
        map.fitBounds(bounds);
    }
    
    // Contact Us
    
    if (isSelectorExists(".form_contact")) {
        $(".btn_contact_us input").click(function () {
            if (validateContantUsForm()) {
                var formData = $(".form_contact").closest("form").serialize();
                $.ajax({
                    type: "POST",
                    url: "/contact-us-submit",
                    data: formData,
                    success: function(data) {
                    },
                    error: function(error) {
                    }
                });
                $(".contact_us_form_wrap").addClass("active");
            }
            return false;
        });
        
        // Popup mode
        
        if (isSelectorExists(".btn_contact")) {
            $(".btn_contact").click(function () {
                showPopup(".form_contact");
            });
        }
        
        if (isSelectorExists(".contact_us_form_wrap .close")) {
            $(".contact_us_form_wrap .close").click(function () {
                closePopup();
            });
        }
    }
    
    // Contact Us Country Autocompleter
    
    if (isSelectorExists("#typeahead")) {
        $('#typeahead').typeahead({
            source: [
             /*A*/
                 'Afghanistan',
                 'Albania',
                 'Algeria',
                 'Andorra',
                 'Angola',
                 'Antigua and Barbuda',
                 'Argentina',
                 'Armenia',
                 'Australia',
                 'Austria',
                 'Azerbaijan',
             /*B*/
                'Bahamas, The',
                'Bahrain',
                'Bangladesh',
                'Barbados',
                'Belarus',
                'Belgium',
                'Belize',
                'Benin',
                'Bhutan',
                'Bolivia',
                'Bosnia and Herzegovina',
                'Botswana',
                'Brazil',
                'Brunei',
                'Bulgaria',
                'Burkina Faso',
                'Burma',
                'Burundi',
             /*C*/
                'Cambodia',
                'Cameroon',
                'Canada',
                'Cape Verde',
                'Central African Republic',
                'Chad',
                'Chile',
                'China',
                'Colombia',
                'Comoros',
                'Congo, Democratic Republic of the',
                'Congo, Republic of the',
                'Costa Rica',
                'Cote d"Ivoire',
                'Croatia',
                'Cuba',
                'Cyprus',
                'Czech Republic',
             /*D*/
                'Denmark',
                'Djibouti',
                'Dominica',
                'Dominican Republic',
             /*E*/
                'East Timor (see Timor-Leste)',
                'Ecuador',
                'Egypt',
                'El Salvador',
                'Equatorial Guinea',
                'Eritrea',
                'Estonia',
                'Ethiopia',
             /*F*/
                'Fiji',
                'Finland',
                'France',
             /*G*/
                'Gabon',
                'Gambia, The',
                'Georgia',
                'Germany',
                'Ghana',
                'Greece',
                'Grenada',
                'Guatemala',
                'Guinea',
                'Guinea-Bissau',
                'Guyana',
             /*H*/
                'Haiti',
                'Holy See',
                'Honduras',
                'Hong Kong',
                'Hungary',
             /*I*/
                'Iceland',
                'India',
                'Indonesia',
                'Iran',
                'Iraq',
                'Ireland',
                'Israel',
                'Italy',
             /*J*/
                'Jamaica',
                'Japan',
                'Jordan',
             /*K*/
                'Kazakhstan',
                'Kenya',
                'Kiribati',
                'Korea, North',
                'Korea, South',
                'Kosovo',
                'Kuwait',
                'Kyrgyzstan',
             /*L*/
                'Laos',
                'Latvia',
                'Lebanon',
                'Lesotho',
                'Liberia',
                'Libya',
                'Liechtenstein',
                'Lithuania',
                'Luxembourg',
             /*M*/
                'Macau',
                'Macedonia',
                'Madagascar',
                'Malawi',
                'Malaysia',
                'Maldives',
                'Mali',
                'Malta',
                'Marshall Islands',
                'Mauritania',
                'Mauritius',
                'Mexico',
                'Micronesia',
                'Moldova',
                'Monaco',
                'Mongolia',
                'Montenegro',
                'Morocco',
                'Mozambique',
             /*N*/
                'Namibia',
                'Nauru',
                'Nepal',
                'Netherlands',
                'Netherlands Antilles',
                'New Zealand',
                'Nicaragua',
                'Niger',
                'Nigeria',
                'North Korea',
                'Norway',
             /*O*/
                'Oman',
             /*P*/
                'Pakistan',
                'Palau',
                'Palestinian Territories',
                'Panama',
                'Papua New Guinea',
                'Paraguay',
                'Peru',
                'Philippines',
                'Poland',
                'Portugal',
             /*Q*/
                'Qatar',
             /*R*/
                'Romania',
                'Russia',
                'Rwanda',
             /*S*/
                'Saint Kitts and Nevis',
                'Saint Lucia',
                'Saint Vincent and the Grenadines',
                'Samoa',
                'San Marino',
                'Sao Tome and Principe',
                'Saudi Arabia',
                'Senegal',
                'Serbia',
                'Seychelles',
                'Sierra Leone',
                'Singapore',
                'Slovakia',
                'Slovenia',
                'Solomon Islands',
                'Somalia',
                'South Africa',
                'South Korea',
                'South Sudan',
                'Spain',
                'Sri Lanka',
                'Sudan',
                'Suriname',
                'Swaziland',
                'Sweden',
                'Switzerland',
                'Syria',
             /*T*/
                'Taiwan',
                'Tajikistan',
                'Tanzania',
                'Thailand',
                'Timor-Leste',
                'Togo',
                'Tonga',
                'Trinidad and Tobago',
                'Tunisia',
                'Turkey',
                'Turkmenistan',
                'Tuvalu',
             /*U*/
                'Uganda',
                'Ukraine',
                'United Arab Emirates',
                'United Kingdom',
                'Uruguay',
                'Uzbekistan',
             /*V*/
                'Vanuatu',
                'Venezuela',
                'Vietnam',
             /*Y*/
                'Yemen',
             /*Z*/
                'Zambia',
                'Zimbabwe'
            ]
        })
    }
    
    // Popup
    
    if (isSelectorExists(".popup_bg")) {
        $(".popup_bg").click(function () {
            closePopup();
        });
    }
    
    // Main page slider
    // TODO: refactor this code!!!

    if (isSelectorExists(".slider_main_page_controls")) {
        $(".slider_main_page_controls .select_items ul").empty();
    
        $(".slider_main_page_items ul li").each(function () {
            var self = this;
            $(".slider_main_page_controls .select_items ul").append("<li><a></a></li>");
            if ($(self).hasClass("active")) {
                $(".slider_main_page_controls .select_items ul li").last().addClass("active");
            }
            $(".slider_main_page_controls .select_items ul li").last().click(function() {
                $(".slider_main_page_items ul li").removeClass("active");
                $(".slider_main_page_controls .select_items ul li").removeClass("active");
                $(self).addClass("active");
                $(this).addClass("active");
            });
        });
        
        $(".slider_main_page_controls .arrow_left").click(function () {
            var item = $(".slider_main_page_items ul li.active").prev();
            if (typeof item[0] == "undefined") {
                item = $(".slider_main_page_items ul li").last();
            }
            $(".slider_main_page_items ul li").removeClass("active");
            item.addClass("active");
            var itemDot = $(".slider_main_page_controls .select_items ul li.active").prev();
            if (typeof itemDot[0] == "undefined") {
                itemDot = $(".slider_main_page_controls .select_items ul li").last();
            }
            $(".slider_main_page_controls .select_items ul li").removeClass("active");
            itemDot.addClass("active");
        });
    
        $(".slider_main_page_controls .arrow_right").click(function () {
            var item = $(".slider_main_page_items ul li.active").next();
            if (typeof item[0] == "undefined") {
                item = $(".slider_main_page_items ul li").first();
            }
            $(".slider_main_page_items ul li").removeClass("active");
            item.addClass("active");
            var itemDot = $(".slider_main_page_controls .select_items ul li.active").next();
            if (typeof itemDot[0] == "undefined") {
                itemDot = $(".slider_main_page_controls .select_items ul li").first();
            }
            $(".slider_main_page_controls .select_items ul li").removeClass("active");
            itemDot.addClass("active");
        });
    }
});

function isSelectorExists(selector) {
    return (typeof $(selector)[0] != "undefined");
}

function validateContantUsForm() {
    var $name = $(".form_contact input[name='Name']");
    var $email = $(".form_contact input[name='Email']");
    var $message = $(".form_contact textarea[name='message']");
    var emailRegex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        
    var isValid = true;    
    
    if ($name.val().length > 0) {
        $name.parent().removeClass("error");
    } else {
        $name.parent().addClass("error");
        isValid = false;
    }
    
    if (emailRegex.test($email.val())) {
        $email.parent().removeClass("error");
    } else {
        $email.parent().addClass("error");
        isValid = false;
    }
    
    if ($message.val().length > 0) {
        $message.parent().removeClass("error");
    } else {
        $message.parent().addClass("error");
        isValid = false;
    }
    
    return isValid;
}

function closePopup() {
    $(".popup_bg").hide();
    $(".box_popup_wrap").hide();
}

function showPopup(selector) {
    closePopup();
    $(".popup_bg").show();
    $(selector).closest(".box_popup_wrap").show();
}
