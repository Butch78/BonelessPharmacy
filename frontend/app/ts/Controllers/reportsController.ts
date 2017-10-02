/**
 * Reports Controller
 * Controller associated with the home page of the application
 */
app.controller("reportsCtrl", ($scope, $http) => {
    $('#modalReportView').modal();
    $('select').material_select();
    $scope.reportHeaders = [];
    $scope.reportContent = [];
    $scope.genSalesReport = (name: string = "Past Week") => {
        $('#modalReportView').modal('open');
        $http(Boneless.CreateRequest("api/Reports", "post", {
            begin: $scope.processSalesReportDateString(name),
            type: "sales",
        })).then(
            (res) => {
                $scope.reportRaw = `${res.data}`;
                $scope.reportUrl = Boneless.CreateFile($scope.reportRaw);
                const reportData = Boneless.ParseCsv(res.data);
                $scope.reportHeaders = reportData[0];
                let tempData = [];
                for (let i = 1; i < reportData.length; i++) {
                    tempData.push(reportData[i]);
                }
                $scope.reportContent = tempData;
                $scope.reportName = $scope.processSalesReportTitle(name);
            },
            (errRes) => Boneless.Notify(BonelessStatusMessage.INVALID_POST));
    };
    $scope.processSalesReportTitle = (name: string) => {
        const processDate = (date: Date) => `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
        let timeFrame = "";
        const today = new Date();
        switch (name) {
            case "Past Week":
                const lastWeek = new Date();
                lastWeek.setDate(today.getDate() - 7);
                timeFrame = `${processDate(lastWeek)} - ${processDate(today)}`;
                break;
            case "Past 2 Weeks":
                const last2Week = new Date();
                last2Week.setDate(today.getDate() - 14);
                timeFrame = `${processDate(last2Week)} - ${processDate(today)}`;
                break;
            case "Past Month":
                const lastMonth = new Date();
                lastMonth.setMonth(lastMonth.getMonth() - 1);
                timeFrame = `${processDate(lastMonth)} - ${processDate(today)}`;
                break;
        }
        return `Stock Out for ${timeFrame}`;
    };
    $scope.processSalesReportDateString = (name: string) => {
        const today = new Date();
        switch (name) {
            case "Past Week":
                const lastWeek = new Date();
                lastWeek.setDate(today.getDate() - 7);
                return lastWeek.toISOString();
            case "Past 2 Weeks":
                const last2Week = new Date();
                last2Week.setDate(today.getDate() - 14);
                return last2Week.toISOString();
            case "Past Month":
                const lastMonth = new Date();
                lastMonth.setMonth(lastMonth.getMonth() - 1);
                return lastMonth.toISOString();
        }
    };
    $scope.genLowStockReport = () => {
    };
});

$(".button-collapse").sideNav();
