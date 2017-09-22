/**
 * Home Controller
 * Controller associated with the home page of the application
 */
app.controller("salesCtrl", ($scope, $http) => {
    $('.modal').modal();
    $('.collapsible').collapsible();

    // GET SalesRecords
    $http(Boneless.CreateRequest("api/SalesRecords", "get")).then(
        (res) => {
            $scope.salesRecords = res.data as SalesRecord[];
        },
        (errorRes) => {
            alert(errorRes.data);
    });
    // GET SalesItems
    $http(Boneless.CreateRequest("api/SalesItems", "get")).then(
        (res) => ($scope.salesItems = res.data as SalesItem[]),
        (errorRes) => (Boneless.Notify(BonelessStatusMessage.INVALID_GET)),
    );
    $scope.openModalNewSale = () => {
        $('#modalNewSale').modal('open');
    };

    // Will need to be adjusted later so that it takes in the ID of a sale, meaning it can present the relevant details
    $scope.openModalSaleDetails = () => {
        $('#modalSaleDetails').modal('open');
    };
});
