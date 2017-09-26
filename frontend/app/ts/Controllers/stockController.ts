/**
 * Stock Controller
 * Controller associated with the home page of the application
 */
app.controller("stockCtrl", ($scope, $http) => {
    // init the modal so it can actually be opened properly (href doesn't play nicely with routing)
    $('.modal').modal();
    $('#stockMeasureInput').material_select();
    $scope.newStock = {};
    $scope.editingStock = {};
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

    $scope.openModalAddStock = () => $('#modalAddStock').modal('open');

    $scope.openModalStockDetails = (index: number) => {
        $scope.editingStock = $scope.salesItems[index];
        $("#modalStockDetails").modal("open");
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

});
