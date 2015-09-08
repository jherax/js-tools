angular.module("nowplayingApp").controller("homeController",
	['$scope', '$window', '$sce', 'searchService',
	//Home controller
    function homeController (model, window, sce, searchService) {
		model.url = "http://youtube.com/";
		model.comments = "I like this video!";
		model.datasource = { "items": [] };
		model.geoStatus = "checking...";
		model.showLocation = false;
		model.coordinates = {};

		model.fnSubmitTweet = function (topic, comments) {
			searchService
				.submitTweet(topic, comments)
				.then(function (data) {
				    window.console.log("%c" + data.Message, "color:green");
				});
		};

		model.onEnter = function (event) {
			if (event.which === 13 && model.url.length)
			    model.fnSubmitTweet(model.url, model.comments);
		};

		model.fnDisabled = function (text) {
			return !text.length;
		};

		model.fnShowResults = function () {
		    return !!(model.datasource.items.length);
		};

		model.fnShowLocation = function () {
		    model.showLocation = !model.showLocation;
		}

        //regular expression to format the text
        var reHttp = /(http[^\s]+)/gm,
            reHash = /#([^\s]+)/gm;

        //https://docs.angularjs.org/api/ng/directive/ngBindHtml
        //https://docs.angularjs.org/api/ngSanitize/service/$sanitize
		model.fnFormatText = function (text) {
		    var formatted;
		    text = (text || "").trim();
		    formatted = text.replace(reHttp, '<a href="$1" target="_blank">$1</a>');
		    formatted = formatted.replace(reHash, '<a href="https://twitter.com/hashtag/$1" target="_blank">#$1</a>');
		    return sce.trustAsHtml(formatted);
		}

		model.fnGetUrl = function (text) {
		    var match;
		    text = (text || "").trim();
		    match = text.match(reHttp);
		    return (match ? match[0] : "");
		}

        //Geolocation API
		function getGeolocation () {
		    if (window.navigator.geolocation) {
		        window.navigator.geolocation.getCurrentPosition(onSuccess, onError);
		    } else {
		        onError('not supported');
		    }
		}

        //callback on error
		function onError (msg) {
		    model.geoStatus = typeof msg == 'string' ? msg : "failed";
		    model.coordinates = {};
		    getTweets();
		}

        //callback on success
		function onSuccess (position) {
		    if (model.geoStatus == "success") return;
		    var coords = position.coords;
		    model.geoStatus = "success";
		    model.coordinates = {
		        "latitude": coords.latitude,
		        "longitude": coords.longitude,
		        "accuracy": coords.accuracy + "m",
		        "radius": "10km"
		    };
		    getTweets();
		}

        //get the last 5 tweets with #nowplaying hashtag
		function getTweets () {
		    searchService
				.getTweets(model.coordinates)
				.then(function (data) {
				    model.datasource.items = data.statuses || [];
				    model.datasource.Message = data.Message;
				});
		}

        //initial actions
        (function init () {
            getGeolocation();
		})();

	}//end homeController
]);
