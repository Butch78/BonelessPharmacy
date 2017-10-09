/**
 * Reports Controller
 * Controller associated with the home page of the application
 */
app.controller("reportsCtrl", ($scope, $http) => {
    $('.modal').modal();
    $('select').material_select();
    $('ul.tabs').tabs();
    $scope.reportHeaders = [];
    $scope.reportContent = [];
    $scope.savedReports = [];
    $scope.currentChart = null;
    $scope.reportGenerated = $scope.reportHeaders.length > 0;
    $scope.minStockThreshold = 5;
    $scope.savedReportId = -1;
    $scope.isChartedReport = false;
    Materialize.updateTextFields();

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

    $scope.genStockReport = (name: string = "Past Week") => {
        $('#modalReportView').modal('open');
        $http(Boneless.CreateRequest("api/Reports", "post", {
            begin: $scope.processSalesReportDateString(name),
            type: "stock",
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
        const reportData = Boneless.ParseCsv($scope.reportRaw);
        $scope.reportHeaders = reportData[0];
        let tempData = [];
        for (let i = 1; i < reportData.length; i++) {
            if (reportData[i][0] !== "") {
                tempData.push({
                    data: reportData[i],
                    index: Math.random() * 100,
                });
            }
        }
        $scope.reportContent = tempData;
        $scope.reportGenerated = $scope.reportHeaders.length > 0;
    };

    $scope.setChart = (reportType: string) => {
        let chartOutput = document.getElementById("chartOutput") as HTMLCanvasElement;
        let chartData = translateChartData(reportType);
        if ($scope.currentChart !== null) {
            $scope.currentChart.destroy();
        }
        $scope.currentChart = new Chart(chartOutput.getContext('2d'), chartData as any);
    };

    /**
     * Translate Boneless Pharmacy CSV data into a chart
     * @param reportType the type of report
     * @param data the data being used for the chart
     */
    const translateChartData = (reportType: string) => {
        $scope.isChartedReport = true;
        switch (reportType) {
            case "Stock Report":
                return {
                    data: {
                        datasets: [
                            {
                                backgroundColor: 'rgb(3, 155, 229)',
                                data: ($scope.reportContent).map((r) => r.data[2]),
                                label: "Item Amount Sold",
                            },
                        ],
                        labels: ($scope.reportContent).map((r) => r.data[1]),
                    },
                    options: {
                        scales: {
                            yAxes: [
                                {
                                    ticks: { beginAtZero: true },
                                },
                            ],
                        },
                    },
                    type: 'bar',
                };
            default:
                $scope.isChartedReport = false;
                break;
        }
    };

    $scope.getSavedReports = () => {
        $http(Boneless.CreateRequest("api/Reports", "get")).then((res) => {
            $scope.savedReports = (res.data as ReportFile[]);
        }, (errorRes) => {
            Boneless.Notify(BonelessStatusMessage.INVALID_GET);
        });
    };

    $scope.selectSavedReport = () => {
        $http(Boneless.CreateRequest(`api/ReportData/${$scope.savedReports[$scope.savedReportId].fileName}`, "GET"))
            .then((res) => {
                $scope.setCurrentReport(res.data);
                $scope.setChart($scope.savedReports[$scope.savedReportId].type);
            }, (erroRes) => Boneless.Notify(BonelessStatusMessage.INVALID_GET));
    };

    $('.dropdown-button').dropdown({
        alignment: 'left', // Displays dropdown with edge aligned to the left of button
        belowOrigin: false, // Displays dropdown below the button
        constrainWidth: false, // Does not change width of dropdown to that of the activator
        gutter: 0, // Spacing from edge
        hover: true, // Activate on hover
        inDuration: 300,
        outDuration: 225,
        stopPropagation: false, // Stops event propagation
    });

    $('label').addClass('active');

    $scope.getSavedReports();

    $scope.openReportModal = () => $('#modalReportView').modal('open');

});

$(".button-collapse").sideNav();
