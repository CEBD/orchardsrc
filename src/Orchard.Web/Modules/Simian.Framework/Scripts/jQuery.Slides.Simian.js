$('document').ready(function () {
	var $slider = $(".slides");
	var updateCaption = function (number) {
		var $captionZone = $slider.find('.caption-zone');
		var $thisImage = $slider.find('img[slidesjs-index="' + (number - 1) + '"]:first');
		var title = $thisImage.data('title');
		var description = $thisImage.data('description');
		var titleLabel = '<h6>' + title + '</h6>';
		var descriptionLabel = '<p>' + description + '</p>';
		$captionZone.html(titleLabel + descriptionLabel);

	    
	};
	var makeCaptions = {
		loaded: function (number) {
			var $captionZone = $slider.find('.caption-zone');
			if ($captionZone.length === 0) {
				$captionZone = $('<div class="caption-frame"><div class="waffles"><div class="w-12 w-alpha w-omega"><div class="caption-zone"></div></div></div></div>');
				$slider.prepend($captionZone);
			}
			updateCaption(number);
		},
		start: function (number) { },
		complete: function (number) {
			updateCaption(number);
		}
	};
	$slider.slidesjs({
		width: 1024,
		height: 450,
		play: {
			active: false,
			auto: true,
			interval: 2000,
			swap: true,
			effect: "slide"
		},
		pagination: {
			active: true,
			effect: "slide"
		},
		navigation: {
		    active: false,
		    effect: "slide"
		},
		effect: {
		    slide:
		        { speed: 2000 },
		    fade:
		        { speed: 2000, crossfade: true }
		},
		callback: makeCaptions,
	});
});

