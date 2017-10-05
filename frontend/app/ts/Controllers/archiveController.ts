/**
 * Archive Controller
 * Controller associated with the home page of the application
 */
app.controller("archiveCtrl", ($scope, $http) => {
    // init the modal so it can actually be opened properly (href doesn't play nicely with routing)
    $('.modal').modal();
    // $('#stockMeasureInput').material_select();
    $('.tabs').tabs();

    // GET ArchiveItems
    $http(Boneless.CreateRequest("api/ArchiveItems", "get")).then(
        (res) => {
            $scope.archiveItems = res.data as Archive[];
        },
        (errorRes) => {
            Boneless.Notify(BonelessStatusMessage.INVALID_GET);
        });

    // GET Measurements
    $http(Boneless.CreateRequest("api/Measurements", "get")).then(
        (res) => {
            $scope.measurements = res.data as Measurement[];
        },
        (errorRes) => {
            Boneless.Notify(BonelessStatusMessage.INVALID_GET);
        });

    $scope.openModalDeleteStockItem = (index: number) => {
        console.log('start');
        $scope.deletingStock = $scope.archiveItems[index];
        $('#modalDeleteStockItem').modal('open');
        console.log('end');
    };

    $scope.deletingStockItem = () => {
        const updatedObject = $scope.deletingStock as Archive;
        $http(Boneless.CreateRequest(`api/Archive/${updatedObject.id}`, "delete"))
            .then((res) => {
                const data = res.data as Archive;
                $('modalDeleteStockItem').modal('close');
                Materialize.toast(`${data.name} Deleted`, 4000);
            }, (err) => Materialize.toast(`Error deleting Item`, 4000));
    };
});
