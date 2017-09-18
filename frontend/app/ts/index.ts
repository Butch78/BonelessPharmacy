/**
 * The application global used for Angular directives
 */
const app = angular.module("bonelessPharmacy", ['ngRoute']);

app.controller("homeCtrl", ($scope, $location) => {
    $location.path('/sales');
});

// Hashprefix config
app.config(['$locationProvider', ($locationProvider) => {
    $locationProvider.hashPrefix('');
}]);

// Routing logic
app.config(($routeProvider) => {
    $routeProvider.when('/sales', {
        templateUrl: './views/sales.html',
        controller: 'salesCtrl'
    });
    $routeProvider.when('/reports', {
        templateUrl: './views/reports.html',
        controller: 'reportsCtrl'
    });
    $routeProvider.when('/stock', {
        templateUrl: './views/stock.html',
        controller: 'stockCtrl'
    });
    $routeProvider.when('/help', {
        templateUrl: './views/help.html',
        controller: 'helpCtrl'
    });
});


$(document).ready(function () {
    // the "href" attribute of the modal trigger must specify the modal ID that wants to be triggered
    $('.modal').modal();
    $(".button-collapse").sideNav();
});


