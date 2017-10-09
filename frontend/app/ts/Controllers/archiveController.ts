/**
 * Archive Controller
 * Controller associated with the home page of the application
 */
app.controller("archiveCtrl", ($scope, $http, $rootScope) => {
    // init the modal so it can actually be opened properly (href doesn't play nicely with routing)
    $('.modal').modal();
    $scope.restoreStock = {};
    // $('#stockMeasureInput').material_select();
    $('.tabs').tabs();

    // GET SalesItems which are Archived
    $scope.updateRestorePage = () => {
        $http(Boneless.CreateRequest("api/SalesItems/Archived", "get")).then(
            (res) => {
                $scope.archiveItems = res.data as SalesItem[];
                console.log($scope.archiveItems);
            },
            (errorRes) => {
                Boneless.Notify(BonelessStatusMessage.INVALID_GET);
            });
        };
    $scope.updateRestorePage();

    // Allows other controllers to update the page
    $scope.$on('updateRestorePage', () => {
        $scope.updateRestorePage();
    });

    // GET Measurements
    $http(Boneless.CreateRequest("api/Measurements", "get")).then(
        (res) => {
            $scope.measurements = res.data as Measurement[];
        },
        (errorRes) => {
            Boneless.Notify(BonelessStatusMessage.INVALID_GET);
        });

    $scope.openModalRestoreItem = (stock: SalesItem) => {
        $scope.restoreStock = stock;
        $("#modalRestoreStockItem").modal('open');
    };

    $scope.restoreItem = (stock: SalesItem) => {
        stock.isArchived = 0;

        let successVar = `${stock.name} ${stock.amount}  ${stock.measurement.suffix}`;
        $http(Boneless.CreateRequest(`api/SalesItems/${stock.id}`, "put", stock))
            .then((res) => {
                $('#modalRestoreStockItem').modal('close');

                $scope.updateRestorePage();
                $rootScope.$broadcast('updateStockPage');

                Materialize.toast(successVar + ` ( PLU : ${stock.id} ) recovered Successfully`, 4000);
                console.log(stock);
            }, (err) => Materialize.toast(`Error recovering item`, 4000));
    };
});
