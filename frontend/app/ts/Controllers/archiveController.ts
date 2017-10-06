/**
 * Stock Controller
 * Controller associated with the home page of the application
 */
app.controller("archiveCtrl", ($scope, $http) => {
    // init the modal so it can actually be opened properly (href doesn't play nicely with routing)
    $('.modal').modal();
    // $('#stockMeasureInput').material_select();
    $('.tabs').tabs();

    // GET SalesItems which are Archived
    $http(Boneless.CreateRequest("api/SalesItems/Archived", "get")).then(
        (res) => {
            $scope.archiveItems = res.data as SalesItem[];
            console.log($scope.archiveItems);
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
});
