utils.setDateTo("#spn-hora");

angular.module("nowplayingApp", ["ngRoute", "ngSanitize"]);

angular.module("nowplayingApp").config(
	['$routeProvider',
	function router ($routeProvider) {
		$routeProvider
			.when("/home", {
				templateUrl: "views/home.html",
				controller: "homeController",
				controllerAs: "homeCtrl",
				reloadOnSearch: false
			})
			.otherwise({ redirectTo: "/home" });
}]);

angular.module("nowplayingApp").filter('trustAsResourceUrl',
	['$sce',
	function filterUrl ($sce) {
	    return function (val) {
	        return $sce.trustAsResourceUrl(val);
	    };
	}
]);
