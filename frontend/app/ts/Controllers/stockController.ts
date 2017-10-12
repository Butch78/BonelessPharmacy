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
    $scope.currentChart = null;

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

    $scope.openModalStockFacts = (index: number) => {
        $scope.editingStock = $scope.salesItems[index];
        $http(Boneless.CreateRequest(`api/Predictions/${$scope.editingStock.id}`, "get")).then(
            (res) => {
                $scope.editingPrediction = res.data;
                console.log($scope.editingPrediction);
                $http(Boneless.CreateRequest(`api/SalesItemTrend/${$scope.editingStock.id}`, 'get')).then(
                    (resChart) => {
                        $scope.applyStockFactChart(resChart.data);
                        $("#modalStockFacts").modal('open');
                    },
                    (errorResChart) => Boneless.Notify(BonelessStatusMessage.INVALID_GET));
            },
            (erroRes) => Boneless.Notify(BonelessStatusMessage.INVALID_GET));
    };

    $scope.applyStockFactChart = (data: number[]) => {
        let chartOutput = document.getElementById("chartOutput") as HTMLCanvasElement;
        if ($scope.currentChart !== null) {
            $scope.currentChart.destroy();
        }
        $scope.currentChart = new Chart(chartOutput.getContext('2d'), {
            data: {
                labels: [
                    '4-5 Months Ago',
                    '3-4 Months Ago',
                    '2-3 Months Ago',
                    '1-2 Months Ago',
                    'Past Month',
                    'Today',
                ],
                // tslint:disable-next-line:object-literal-sort-keys
                datasets: [{
                    data,
                    label: 'Monthly Sales',
                }],
            },
            type: 'line',
        } as any);
        console.log($scope.currentChart);
    };

    $scope.openModalDeleteStockItem = () => {
        $scope.deletingStock = $scope.editingStock;
        $('#modalDeleteStockItem').modal('open');
    };

    $scope.discoverStockFeature = () => {
        $('.tap-target').tapTarget('open');
    };

    $scope.addNewStockItem = () => {
        if ($scope.newStock.stockOnHand > 0) {
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
        } else {
            Materialize.toast("Please enter a positive number for stock quantity", 4000);
        }
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
