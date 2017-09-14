/**
 * Home Controller
 * Controller associated with the home page of the application
 */
app.controller("homeCtrl", ($scope, $http) => {
    $scope.name = "Alex";
    // Example request
    $http(Boneless.CreateRequest('api/Measurements', 'GET')).then((res) => console.log(res.data));
});
