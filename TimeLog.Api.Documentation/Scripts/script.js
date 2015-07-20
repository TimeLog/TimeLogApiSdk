(function () {
	var isFront = $(".js-flexslider--front"),
		activeClass = "is-active";

	if (isFront) {
		$(".js-feature-list-nav").on("click", ".feature__title", function (event) {
			if ($(window).width() < 800) return window.location = this.href;

			event.preventDefault();
			$(this)
				.parents("li")
				.addClass(activeClass)
				.parent("ul")
				.addClass(activeClass);

		}).on("click", ".feature__view-all", function (event) {
			event.preventDefault();
			$(this)
				.parents("li")
				.removeClass(activeClass)
				.parent("ul")
				.removeClass(activeClass);
		});

		$("#js-play-video").click(function (event) {
			event.preventDefault();

			var $mediaContent = $("#js-media-content"),
				$video = $mediaContent.find("iframe"),
				src = $video.data("source");

			$mediaContent.addClass("is-playing");
			$video.attr("src", src + "?autoplay=1");
		});
	}
}());

var lastScrollTop = 0;
$(window).scroll(function(event){
	var st = $(this).scrollTop(),
		topHeight = 70;
	if (st > lastScrollTop) {
		if(st > topHeight) {
			st = topHeight;
		}
		$('.header').css('top', -st+'px');

		if(st === topHeight) {
			$('.header').addClass('small-logo');
		}
	} else if (st < lastScrollTop) {
		if(st > topHeight || st < 0) {
			return false;
		} 
		$('.header').css('top', -st+'px');

		if(st < (topHeight + 1)) { 
			$('.header').removeClass('small-logo');
		}
	} 
	lastScrollTop = st;
});

function mobileNav($) {
	var open = $('.small-screen-menu__link').attr('data-opentext');
	var close = $('.small-screen-menu__link').attr('data-closetext');

	$('.small-screen-menu').click(function (e) {
		$('body').toggleClass('menu-toggled');

		if( $('body').hasClass('menu-toggled') ) {
			$('.small-screen-menu__text').text(close);
		} else {
			$('.small-screen-menu__text').text(open);
		}

		e.preventDefault();
	});

	$('li[class*=has-subnav]').click(function (e) {
		if( $(this).hasClass('sub-menu-toggled') ) {
			$(this).removeClass('sub-menu-toggled');
			$('li[class*=has-subnav]').removeClass('sub-menu-toggled');
		} else {
			$('li[class*=has-subnav]').removeClass('sub-menu-toggled');
			$(this).addClass('sub-menu-toggled');
		}
		e.preventDefault();
	});

	$('.pseudo-overlay').click(function() {
		$('body').removeClass('menu-toggled');
		$('.small-screen-menu__text').text(open);
	});
}

// Responsive navigation
var Flexnav = function(element) {
	this.element = $(element);
	this.settings = {
		container: "#slide-menu",
		dropdown: "<ul class='flexnav-dropdown'>"
	};

	this.dropdown = false;
	this.is_updating = false;

	this.setup();
};

Flexnav.prototype = {
	clean_up: function() {
		this.is_updating = false;
		// this.element.on("DOMSubtreeModified", this.listener());
	},

	get_available_width: function() {
		return this.element.innerWidth();
	},

	get_child_elements_width: function() {
		var width = 0, children = this.element.children();
		children.each(function() {
			var element = $(this);
			if (!element.hasClass("flexnav-container")) {
				width += element.outerWidth(true);
			}
		});
		return width;
	},

	has_room: function() {
		return this.get_available_width() > this.get_child_elements_width();
	},

	add_dropdown: function() {
		var dropdown;
		if (!this.dropdown) {
			dropdown = $(this.settings.container).prepend(this.settings.dropdown);
			dropdown.addClass("flexnav-container");

			this.container = $(this.settings.container);
			this.dropdown = $(this.settings.container).find(".flexnav-dropdown");
		}
	},

	remove_dropdown: function() {
		if (this.dropdown) {
			this.dropdown.remove();
			this.dropdown = false;
		}
	},

	push_to_flexnav: function() {
		var element = this.element.children().length > 0 && this.element.children()[this.element.children().length - 1];
		if (element) {
			this.dropdown.prepend(element);
			return true;
		}
		return false;
	},

	pull_from_flexnav: function() {
		var element = (this.dropdown && this.dropdown[0].firstElementChild);
		
		if (element) {
			this.element.append(element);
			return true;
		}
		return false;
	},

	update_flexnav: function() {
		if (this.has_room() && this.dropdown) {
			if (this.pull_from_flexnav()) {
				if (this.has_room()) return this.update_flexnav();
				this.push_to_flexnav();
			} else {
				this.remove_dropdown();
			}
		} else if ( !this.has_room() ){
			this.add_dropdown();
			if (this.push_to_flexnav()) {
				if (!this.has_room()) return this.update_flexnav();
				return this.clean_up();
			}
		}
		return this.clean_up();
	},

	listener: function() {
		return (function(context) {
			return function listener() {
				// console.log(e.type);
				// context.element.off("DOMSubtreeModified");	
				if (!context.is_updating) {
					context.is_updating = true;
					context.update_flexnav();
				}
			}
		}(this));	
	},

	setup: function() {
		this.update_flexnav();
		$(window).resize(this.listener());
		// this.element.on("DOMSubtreeModified", function(e){console.log(e.type);});
	}

};

var first;
first = new Flexnav("#nav-items");


$(document).ready(function(){
	mobileNav($);
});
// (function () {
// 	var title = $('<p class="featherlight-header"></p>'),
// 		content = $('<div class="featherlight-description"><p></p><button class="btn btn--right">Luk</button></div>'),
// 		description = content.find("p"),
// 		button = content.find("button");

// 	button.click(function(event) {
// 		event.preventDefault();
// 		$.featherlight.close();
// 	});

// 	$.featherlight.defaults.beforeContent = function (event) {
// 		description.text("test");
// 		console.log(event);
// 		$(".featherlight-content").prepend(title.text("title")).append(content);
// 	}
// }());

(function (global) {
	var Lp = Lightbox.prototype,
		build = Lp.build,
		start = Lp.start,
		// end = Lp.end,
		changeImage = Lp.changeImage;

	Lp.changeImage = function () {
		var data = this.album[arguments[0]].data,
			temp = [];

		changeImage.apply(this, arguments);

		this.$title.text(data.title);
		this.$text.text(data.text);
	};

	Lp.build = function () {
		var $text = $('<div class="lb__text"></div>'),
			$title = $('<div class="lb__title"></div>');

		this.$title = $title;
		this.$text = $text;

		build.apply(this, arguments);

		this.$outerContainer.before($title).after($text);
		console.log("build called");
	};

	Lp.start = function () {
		start.apply(this, arguments);
		// Maybe do all this conditionally - check if flexslider is looping

		// to compensate for flexsliders duplication of elements for looping
		this.album.pop();
		this.album.shift();

		// we need to subtract one from the current index since we popped an element
		// this.changeImage will set the new currentImageIndex
		this.changeImage(this.currentImageIndex - 1);
	};

	// Lp.end = function () {
	// 	end.apply(this, arguments);
	// }
}(this));

$(function () {
	$("#lightbox-start").on("click", function (event) {
		event.preventDefault();
		lightboxInstance.start($(".js-flexslider .flex-active-slide a"));
	})
});

(function () {
	var $flexslider = $('.js-flexslider'),
		activeClass = "is-active";

		$flexslider.each(function () {
			var $element = $(this),
				data = $.extend({
					animation: "slide",
					animationLoop: true,

					slideshow: true,
					directionNav: false,

					useCSS: false /* Chrome fix*/,
				}, $element.data("flex-slider") || {});
			$element.flexslider(data);
		});
}());

(function () {
	var $spider = $("#spider"),
		$popUps = $(".js-pop-ups");

	if ($spider.length && $popUps) {
		$spider.on("click", "g", function (event) {
			// If we are on a small device redirect to where-ever the "read-more"-link points to
			if ($(window).width() < 1000) return window.location = $popUps.find(".js-" + this.id + " a.read-more")[0].href;

			return (
				$popUps
					.find(".is-active")
					.removeClass("is-active")
					.hasClass("js-" + this.id) ||

				$popUps
					.find(".js-" + this.id)
					.addClass("is-active")
			);
		});
	}
}());

(function () {
	var $container = $(".js-client-testimonials"),
		$testimonial = $container.find(".js-testimonial"),
		$testimonials = $container.find(".js-testimonials"),
		arrowClass = "arrow-top arrow-center",
		$temp = $testimonials.find("." + arrowClass.split(" ")[0]);

	$testimonials.on("click", "li", function() {
		$temp.removeClass(arrowClass);
		$temp = $(this);
		$temp.addClass(arrowClass);
		$testimonial.html($temp.data("testimonial"));
	});
}());
