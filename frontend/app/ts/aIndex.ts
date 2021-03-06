/**
 * The application global used for Angular directives
 */
const app = angular.module("bonelessPharmacy", ['ngRoute']);

app.controller("homeCtrl", ($scope, $location, $http) => {
    $location.path(Boneless.IsLoggedIn() ? '/sales' : '/staff');
    $scope.IsLoggedIn = Boneless.IsLoggedIn();
    $scope.initiateLogout = () => {
        Boneless.Logout();
        window.location.reload();
    };
    $scope.lowStock = 0;

    // $http(Boneless.CreateRequest("api/SalesItemsLow", "GET")).then(
    //     (res) => {
    //         $scope.lowStock = res.data;
    //     },
    //     (errorRes) => {
    //         Boneless.Notify(BonelessStatusMessage.INVALID_GET);
    //     });

});

// Hashprefix config
app.config(['$locationProvider', ($locationProvider) => {
    $locationProvider.hashPrefix('');
}]);

app.config(['$compileProvider', ($compileProvider) => {
    ($compileProvider as angular.ICompileProvider)
        .aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|chrome-extension|blob):/);
}]);

// Routing logic
app.config(($routeProvider) => {
    $routeProvider.when('/', {
        controller: 'homeCtrl',
        templateUrl: './views/home.html',
    });
    $routeProvider.when('/sales', {
        controller: 'salesCtrl',
        templateUrl: './views/sales.html',
    });
    $routeProvider.when('/reports', {
        controller: 'reportsCtrl',
        templateUrl: './views/reports.html',
    });
    $routeProvider.when('/staff', {
        controller: 'staffCtrl',
        templateUrl: './views/staff.html',
        // controller: 'staffCtrl',
        // templateUrl: './views/staff.html',
    });
    $routeProvider.when('/stock', {
        controller: 'stockCtrl',
        templateUrl: './views/stock.html',
    });
    $routeProvider.when('/help', {
        controller: 'helpCtrl',
        templateUrl: './views/help.html',
    });
});

$(document).ready(() => {
    // the "href" attribute of the modal trigger must specify the modal ID that wants to be triggered
    $('.modal').modal();
    $(".button-collapse").sideNav();
});
