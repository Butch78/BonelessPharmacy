/**
 * Home Controller
 * Controller associated with the home page of the application
 */
app.controller("homeCtrl", ($scope, $http) => {
    $scope.name = "Alex";
    $http({
        method: 'GET',
        url: `${Boneless.API_ENDPOINT}Measurements`,
    }).then((res) => console.log(res));
});
