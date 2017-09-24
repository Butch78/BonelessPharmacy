/**
 * The application global used for Angular directives
 */
const app = angular.module("bonelessPharmacy", ['ngRoute']);

app.controller("homeCtrl", ($scope, $location) => {
    $location.path('/sales');

    $scope.initiateLogout = () => {
        let oldStaffBtn = document.getElementById("dropDown");
        let staffBtn = document.getElementById("staffTitle");
        staffBtn.innerText = "STAFF";
        oldStaffBtn.style.display = 'none';
        staffBtn.style.display = 'inline-block';
        BtnClick = true;
    };
});

// Hashprefix config
app.config(['$locationProvider', ($locationProvider) => {
    $locationProvider.hashPrefix('');
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
