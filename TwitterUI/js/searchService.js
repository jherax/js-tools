(function(app, utils) {

    var searchService = function (http, log) {

	    function getTweets(coordinates) {
	        utils.showLoading();
			return http.post(
                "http://localhost:3000/service/api/rest/nowplaying",
				coordinates) //object literal
			.then(onSuccess, onError);
		}

	    function submitTweet(video, comments) {
	        utils.showLoading();
	        return http.post(
                "http://localhost:3000/service/api/rest/sendtweet",
                { "video": video, "comments": comments })
            .then(onSuccess, onError);
		}

        //callback on success
	    function onSuccess(response) {
	        utils.showLoading({ hide: true });
			log.info("api: ", response);
			return response.data;
		}

        //callback on error
	    function onError(error) {
	        utils.showLoading(false);
			log.info(error);
		}

		//Public API
		return {
		    "getTweets": getTweets,
		    "submitTweet": submitTweet
		};
		
	}; //end searchService

	searchService.$inject = ["$http", "$log"];
	app.factory("searchService", searchService);

})(angular.module("nowplayingApp"), utils);