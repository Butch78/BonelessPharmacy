/**
 * Reports Controller
 * Controller associated with the home page of the application
 */
app.controller("reportsCtrl", ($scope, $http) => {
    $('#modalReportView').modal();
    $('select').material_select();
    $('ul.tabs').tabs();
    $scope.reportHeaders = [];
    $scope.reportContent = [];
    $scope.minStockThreshold = 5;

    $scope.genSalesReport = (name: string = "Past Week") => {
        $('#modalReportView').modal('open');
        $http(Boneless.CreateRequest("api/Reports", "post", {
            begin: $scope.processSalesReportDateString(name),
            type: "sales",
        })).then(
            (res) => {
                $scope.setCurrentReport(res.data);
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
        $('#modalReportView').modal('open');
        $http(Boneless.CreateRequest("api/Reports", "post", {
            threshold: `${$scope.minStockThreshold}`,
            type: "low",
        })).then((res) => {
            $scope.setCurrentReport(res.data);
            $scope.reportName = `Low Stock (Min ${$scope.minStockThreshold} SOH)`;
        }, (errorRes) => {
            Boneless.Notify(BonelessStatusMessage.INVALID_REPORT);
        });
    };

    /**
     * Set the current report assuming it is structured as a CSV
     */
    $scope.setCurrentReport = (data) => {
        $scope.reportRaw = `${data}`;
        $scope.reportUrl = Boneless.CreateFile($scope.reportRaw);
        const reportData = Boneless.ParseCsv(data);
        $scope.reportHeaders = reportData[0];
        let tempData = [];
        for (let i = 1; i < reportData.length; i++) {
            tempData.push(reportData[i]);
        }
        $scope.reportContent = tempData;
    };

    $scope.setChart = (reportType: string) => {
        let chartOutput = document.getElementById("chartOutput") as HTMLCanvasElement;
        let chartData = translateChartData("sales");
        let chart = new Chart(chartOutput.getContext('2d'), {
            data: {
                datasets: [{data: $scope.reportContent}],
                labels: $scope.reportHeaders,
            },
            type: 'bar',
        });
        console.log(chart);
    };

    /**
     * Translate Boneless Pharmacy CSV data into a chart
     * @param reportType the type of report
     * @param data the data being used for the chart
     */
    const translateChartData = (reportType: string, data = $scope.reportData) => {
        console.log("Not Implemented");
    };
});

$(".button-collapse").sideNav();
