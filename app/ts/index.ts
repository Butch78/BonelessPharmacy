/**
 * The application global used for Angular directives
 */
var app = angular.module("bonelessPharmacy", ['ngRoute']);

// Hashprefix config
app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);

// Routing logic
app.config(($routeProvider) => {
    $routeProvider.when('/', {
        templateUrl: './views/home.html',
        controller: 'homeCtrl'
    });
});
