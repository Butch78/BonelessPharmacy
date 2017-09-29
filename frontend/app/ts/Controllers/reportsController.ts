/**
 * Reports Controller
 * Controller associated with the home page of the application
 */
app.controller("reportsCtrl", ($scope, $http) => {
    $('#modalReportView').modal();
    $('select').material_select();
    $scope.reportHeaders = [];
    $scope.reportContent = [];
    $scope.genSalesReportWeek = () => {
        $('#modalReportView').modal('open');
        $http(Boneless.CreateRequest("api/Reports", "post", {
            type: "sales",
        })).then(
            (res) => {
                const reportData = Boneless.ParseCsv(res.data);
                $scope.reportHeaders = reportData[0];
                let tempData = [];
                for (let i = 1; i < reportData.length; i++) {
                    tempData.push(reportData[i]);
                }
                $scope.reportContent = tempData;
            },
            (errRes) => Boneless.Notify(BonelessStatusMessage.INVALID_POST));
    };
    $scope.genLowStockReport = () => {
        alert("function call is working!");
    }
});

$(".button-collapse").sideNav();
