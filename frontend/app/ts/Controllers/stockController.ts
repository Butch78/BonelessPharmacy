/**
 * Stock Controller
 * Controller associated with the home page of the application
 */
app.controller("stockCtrl", ($scope, $http, $rootScope) => {
    // init the modal so it can actually be opened properly (href doesn't play nicely with routing)
    $('.modal').modal();
    $('#stockMeasureInput').material_select();
    $('.tabs').tabs();
    $scope.newStock = {};
    $scope.editingStock = {};
    $scope.deletingStock = {};
    $scope.postValue = {};

    // GET SalesItems
    $scope.updateStockPage = () => {
        $http(Boneless.CreateRequest("api/SalesItems", "get")).then(
            (res) => {
                $scope.salesItems = res.data as SalesItem[];
            },
            (errorRes) => {
                Boneless.Notify(BonelessStatusMessage.INVALID_GET);
            });
        };
    $scope.updateStockPage();

    // Allows other controllers to update the page
    $scope.$on('updateStockPage', () => {
        $scope.updateStockPage();
    });

    // GET Measurements
    $http(Boneless.CreateRequest("api/Measurements", "get")).then(
        (res) => {
            $scope.measurements = res.data as Measurement[];
        },
        (errorRes) => {
            Boneless.Notify(BonelessStatusMessage.INVALID_GET);
        });

    $scope.openModalAddStock = () => $('#modalAddStock').modal('open');

    $scope.openModalStockDetails = (index: number) => {
        $scope.editingStock = $scope.salesItems[index];
        $("#modalStockDetails").modal('open');
    };

    $scope.openModalDeleteStockItem = (index: number) => {
        console.log('start');
        $scope.deletingStock = $scope.salesItems[index - 1];
        console.log($scope.deletingStock);
        $('#modalDeleteStockItem').modal('open');
        console.log('end');
    };

    $scope.addNewStockItem = () => {
        console.log($scope.newStock);
        $scope.newStock.measurementId = (document.getElementById("stockMeasureInput") as HTMLSelectElement).value;
        $http(Boneless.CreateRequest("api/SalesItems", "post", $scope.newStock)).then((res) => {
            if (res.status === 201) {
                const data = res.data as SalesItem;
                ($scope.salesItems as SalesItem[]).push(data);
                $('#modalAddStock').modal('close');
                Materialize.toast(`${data.name} added to SOH`, 4000);
                $scope.newStock = {};
            }
        }, (err) => Materialize.toast(`Error Adding Item,
        ensure you are connected and all fields are valid`, 4000));
    };

    $scope.updateStockItem = () => {
        const updatedObject = $scope.editingStock as SalesItem;
        $http(Boneless.CreateRequest(`api/SalesItems/${updatedObject.id}`, "put", updatedObject))
            .then((res) => {
                const data = res.data as SalesItem;
                $('#modalStockDetails').modal('close');
                Materialize.toast(`${data.name} Updated`, 4000);
                $scope.editingStock = {};
            }, (err) => Materialize.toast(`Error Updating Item,
            ensure you are connected and all fields are valid`, 4000));
    };

    $scope.deleteStockItem = (stock: SalesItem) => {
        stock.isArchived = 1;

        let successVar = `${stock.name} ${stock.amount}  ${stock.measurement.suffix}`;
        $http(Boneless.CreateRequest(`api/SalesItems/${stock.id}`, "put", stock))
            .then((res) => {
                $('#modalDeleteStockItem').modal('close');
                $('#modalStockDetails').modal('close');

                $scope.updateStockPage();
                $rootScope.$broadcast('updateRestorePage');

                Materialize.toast(successVar + ` ( PLU : ${stock.id} ) archived Successfully`, 4000);
                console.log(stock);
            }, (err) => Materialize.toast(`Error Archiving item`, 4000));
    };
});
