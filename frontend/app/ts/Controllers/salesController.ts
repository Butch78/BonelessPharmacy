/**
 * Home Controller
 * Controller associated with the home page of the application
 */
app.controller("salesCtrl", ($scope, $http) => {
    Boneless.Login();
    $('.modal').modal();
    $('.collapsible').collapsible();
    $scope.searchItemName = "Enter a Barcode/PLU number";
    $scope.initValues = () => {
        $scope.newSale = {};
        $scope.sales = [];
        $scope.newSaleRecords = [];
        $scope.searchItems = [];
        $scope.searchItemName = "Enter a Barcode/PLU number";
        $scope.searchValue = "";
        $scope.viewedSale = {};
    };

    $scope.initValues();
    $scope.getSales = () => $http(Boneless.CreateRequest("api/Sales", "get")).then(
        (res) => {
            $scope.sales = res.data;
            // $scope.$apply();
        },
        (errorRes) => (Boneless.Notify(BonelessStatusMessage.INVALID_GET)),
    );
    $scope.checkDailyReport = () => $http(Boneless.CreateRequest("api/DailySalesReport", "get")).then(
        (res) => {
            if (res.data) {
                Boneless.NotifyCustom("Daily Sales report generated, view in the Reports section.");
            }
        }, (errorRes) => {
            console.log(errorRes.data);
            Boneless.Notify(BonelessStatusMessage.INVALID_REPORT);
        },
    );

    $scope.getSales();
    $scope.checkDailyReport();

    /**
     * Retrieve the total value of a sales contents
     */
    $scope.totalValue = (sale: Sale) => `$${
        sale.contents
            .map((sr) => sr.quantity * sr.salePrice)
            .reduce((prev, curr) => prev + curr)}`;

    $scope.niceDate = (sale: Sale) => `${
        new Date(sale.createdAt).toDateString()}`;

    $scope.getTime = (sale: Sale) => `${
        new Date(sale.createdAt).toLocaleTimeString()}`;

    // GET SalesItems
    $http(Boneless.CreateRequest("api/SalesItems", "get")).then(
        (res) => ($scope.salesItems = res.data as SalesItem[]),
        (errorRes) => (Boneless.Notify(BonelessStatusMessage.INVALID_GET)),
    );
    $scope.openModalNewSale = () => {
        $('#modalNewSale').modal('open');
        $('#stockSearchInput').focus();
        let newSale: Sale = {
            createdAt: new Date().toISOString(),
        };
        if (Object.keys($scope.newSale).length === 0) {
            $http(Boneless.CreateRequest("api/Sales", "post", newSale)).then(
                (res) => ($scope.newSale = newSale = res.data),
                (errorRes) => (Boneless.Notify(BonelessStatusMessage.INVALID_POST)),
            );
        }
    };

    $scope.processNewSalesItem = (e) => {
        const searchVal = $scope.searchValue;
        const searchItems = $scope.searchItems = ($scope.salesItems as SalesItem[])
            .filter((s) => `${s.id}` === `${searchVal}`);
        $scope.searchItemName = $scope.searchItems[0] !== undefined ?
            $scope.searchItems[0].name :
            ($scope.searchValue as string).length > 0 ? "Invalid Barcode/PLU Number" : "Enter a Barcode/PLU number";
        console.log($scope.searchItemName);
    };

    $scope.submitNewSalesItem = (e) => {
        const salesItem: SalesItem = $scope.searchItems[0];
        console.log(e == null && salesItem !== undefined);
        if (e == "CLICK" && salesItem !== undefined || e != "CLICK" && e.which === 13 && salesItem !== undefined) {
            const salesRecord: SalesRecord = {
                itemId: salesItem.id,
                quantity: 1,
                sale: $scope.newSale,
                saleId: $scope.newSale.id,
                salesItem,
            };
            $scope.newSaleRecords.push(salesRecord);
            console.log($scope.newSaleRecords);
            $scope.searchValue = "";
            // Manually applying to fix error with angular
        }
    };

    $scope.discoverSaleFeature = () => {
        $('.tap-target').tapTarget('open');
    };

    // Will need to be adjusted later so that it takes in the ID of a sale, meaning it can present the relevant details
    $scope.openModalSaleDetails = (index: number) => {
        $scope.viewedSale = $scope.sales[index];
        $('#modalSaleDetails').modal('open');
    };

    $scope.addNewSale = () => {
        let postSuccess = true;
        const successfullPosts: SalesRecord[] = [];
        for (const sr of $scope.newSaleRecords as SalesRecord[]) {
            if (sr.quantity === undefined) {
                sr.quantity = 0;
            }
            // Clear pre-defined object
            sr.salesItem = sr.sale = undefined;
            $http(Boneless.CreateRequest("api/SalesRecords", "post", sr)).then(
                (res) => {
                    successfullPosts.push(sr as SalesRecord);
                    Boneless.NotifyCustom(`Sale #${$scope.newSale.id} (${$scope.newSaleRecords.length} Item/s)`);
                    $scope.initValues();
                    $scope.getSales();
                },
                (errorRes) => postSuccess = false,
            );
        }
        if (postSuccess) {
            $('#modalNewSale').modal('close');
        } else {
            Boneless.Notify(BonelessStatusMessage.INVALID_POST);
            for (const sr of successfullPosts) {
                $http(Boneless.CreateRequest(`api/SalesRecords/${sr.id}`, "delete"));
            }
        }
    };
});
