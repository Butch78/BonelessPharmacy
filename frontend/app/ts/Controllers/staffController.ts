/**
 * Staff Controller
 * Controller associated with the staff page of the application
 */
app.controller("staffCtrl", ($scope, $http) => {

    // GET Staff details
    $http(Boneless.CreateRequest("api/Staff", "get")).then(
        (res) => {
            $scope.staff = res.data as Staff[];
        },
        (errorRes) => {
            alert(errorRes.data);
        });

    $(document).ready(function(){
        $('.collapsible').collapsible();
        });
});
