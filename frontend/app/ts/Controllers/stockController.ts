/**
 * Stock Controller
 * Controller associated with the home page of the application
 */
app.controller("stockCtrl", ($scope, $http) => {
    // init the modal so it can actually be opened properly (href doesn't play nicely with routing)
    $('.modal').modal();

    // GET SalesItems
    $http(Boneless.CreateRequest("api/SalesItems", "get")).then(
        (res) => {
            $scope.salesItems = res.data as SalesItem[];
        },
        (errorRes) => {
            alert(errorRes.data);
    });

    // GET Measurements
    $http(Boneless.CreateRequest("api/Measurements", "get")).then(
        (res) => {
            $scope.measurements = res.data as Measurement[];
        },
        (errorRes) => {
            alert(errorRes.data);
    });
    $scope.openModalAddStock = () => {
        $('#modalAddStock').modal('open');
    };

    $scope.addNewStockItem = () => {
        alert("Will add a new stock item, is not currently functioning");
    };
});
