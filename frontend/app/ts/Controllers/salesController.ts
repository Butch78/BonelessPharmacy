/**
 * Home Controller
 * 
 * Controller associated with the home page of the application
 */
app.controller("salesCtrl", ($scope, $http) => {
    $('.modal').modal();
    $('.collapsible').collapsible();

    // GET SalesItems
    $http(Boneless.CreateRequest("api/SalesRecords", "get")).then(
        (res) => {
            $scope.salesRecords = res.data as SalesRecord[];
        },
        (errorRes) => {
            alert(errorRes.data);
    });

    $scope.openModalNewSale = function() {
        $('#modalNewSale').modal('open');
    }
});
