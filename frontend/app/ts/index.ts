/**
 * The application global used for Angular directives
 */
const app = angular.module("bonelessPharmacy", ['ngRoute']);

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
});
