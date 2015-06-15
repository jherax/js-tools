(function(utils, undefined) {
	utils.setDateTo("#spn-hora");

	var app = angular.module("nowplayingApp", ["ngRoute", "ngSanitize"]);

	app.config(function ($routeProvider) {
		$routeProvider
			.when("/home", {
				templateUrl: "views/home.html",
				controller: "homeController"
			})
			.otherwise({ redirectTo: "/home" });
	});

	app.filter('trustAsResourceUrl', ['$sce', function ($sce) {
	    return function (val) {
	        return $sce.trustAsResourceUrl(val);
	    };
	}]);

})(utils);