var utils = (function ($) {
    //===================================
    /* PRIVATE MEMBERS */
    //===================================

    //returns the boolean value of the @obj parameter
    function bool (obj) {
        return (/^true$/i).test(obj);
    }

	//gets the date in the current location settings
	function getDate(date) {
		date = date || new Date();
		return date.toLocaleString();
	}

	//sets the date on a DOM Node element
	function setDateTo(selector, date) {
		if (!selector) return;
		$(selector).text(getDate(date));
	}

    // Shows the overlay screen with the loading animation
    function showLoading(o) {
    	if (o === false)
    		o = { hide: true };
        var d = $.extend({
            hide: false,
            async: true,
            of: null
        }, o);
        $("#floatingBarsG,#backBarsG").stop().remove();
        if (d.hide === true) return true;
        var target = $(d.of || "body"),
            blockG = [],
            i, loading, overlay;
        for (i = 1; i < 9; i+=1) blockG.push('<div class="blockG"></div>');
        loading = $('<div id="floatingBarsG">').append(blockG.join(""));
        overlay = $('<div id="backBarsG" class="bg-fixed bg-opacity">');
        if (d.of) {
        	i = $('<div class="loading-wrapper">');
        	overlay.add(i).css({
	            'border-radius': $(d.of).css('border-radius'),
	            'position': 'absolute',
	            'top': target.position().top,
	            'left': target.position().left,
	            'height': target.outerHeight(),
	            'width': target.outerWidth()
	        });
	        i.append(loading);
	     	overlay.add(i).appendTo(target);
        }
    	else overlay.add(loading).appendTo(target);
        if (bool(d.async)) overlay.hide().fadeIn();
        else overlay.show();
        loading.fnCenter(true);
        return true;
    }

    //===================================
    /* JQUERY EXTENSIONS */
    //===================================

    // Centers an element relative to another
    $.fn.fnCenter = function(absolute) {
        return this.each(function (i, dom) {
            var elem = $(dom);
            elem.css({
                'position': absolute ? 'absolute' : 'fixed',
                'left': '50%',
                'top': '50%',
                'margin-left': -elem.outerWidth() / 2 + 'px',
                'margin-top': -elem.outerHeight() / 2 + 'px'
            });
        });
    };

    //===================================
    /* PUBLIC API */
    //===================================
	return {
		"getDate": getDate,
		"setDateTo": setDateTo,
		"showLoading": showLoading
	};
}(jQuery));