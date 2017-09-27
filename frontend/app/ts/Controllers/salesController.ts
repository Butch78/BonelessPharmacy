/**
 * Home Controller
 * Controller associated with the home page of the application
 */
app.controller("salesCtrl", ($scope, $http) => {
    $('.modal').modal();
    $('.collapsible').collapsible();

    $scope.initValues = () => {
        $scope.newSale = {};
        $scope.sales = [];
        $scope.newSaleRecords = [];
        $scope.searchItems = [];
    };
    $scope.initValues();
    // GET SalesRecords
    // $http(Boneless.CreateRequest("api/SalesRecords", "get")).then(
    //     (res) => {
    //         $scope.salesRecords = res.data as SalesRecord[];
    //     },
    //     (errorRes) => {
    //         alert(errorRes.data);
    //     });
    $scope.getSales = () => $http(Boneless.CreateRequest("api/Sales", "get")).then(
        (res) => {
            $scope.sales = res.data;
            $scope.$apply();
        },
        (errorRes) => (Boneless.Notify(BonelessStatusMessage.INVALID_GET)),
    );
    $scope.getSales();

    /**
     * Retrieve the total value of a sales contents
     */
    $scope.totalValue = (sale: Sale) => `$${
        sale.contents
        .map((sr) => sr.quantity * sr.salesItem.price)
        .reduce((prev, curr) => prev + curr)}`;

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

    $('#stockSearchInput').keyup((e) => {
        if (e.which !== 13) {
            const searchVal = $('#stockSearchInput').val();
            const searchItems = $scope.searchItems = ($scope.salesItems as SalesItem[])
                .filter((s) => `${s.id}` === `${searchVal}`);
            console.log(searchItems);
        }
    });

    $('#stockSearchInput').keypress((e) => {
        const salesItem: SalesItem = $scope.searchItems[0];
        if (e.which === 13 && salesItem !== undefined) {
            const salesRecord: SalesRecord = {
                itemId: salesItem.id,
                quantity: 1,
                sale: $scope.newSale,
                saleId: $scope.newSale.id,
                salesItem,
            };
            $scope.newSaleRecords.push(salesRecord);
            console.log($scope.newSaleRecords);
            // Manually applying to fix error with angular
            $scope.$apply();
            $('#stockSearchInput').val('');
        }
    });

    $scope.discoverSaleFeature = () => {
        $('.tap-target').tapTarget('open');
    };

    // Will need to be adjusted later so that it takes in the ID of a sale, meaning it can present the relevant details
    $scope.openModalSaleDetails = () => {
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
                (res) => successfullPosts.push(sr as SalesRecord),
                (errorRes) => postSuccess = false,
            );
        }
        if (postSuccess) {
            Boneless.NotifyCustom(`Sale #${$scope.newSale.id} (${$scope.newSaleRecords.length} Item/s)`);
            $scope.initValues();
            $scope.getSales();
            $('#modalNewSale').modal('close');
        } else {
            Boneless.Notify(BonelessStatusMessage.INVALID_POST);
            for (const sr of successfullPosts) {
                $http(Boneless.CreateRequest(`api/SalesRecords/${sr.id}`, "delete"));
            }
        }
    };
});
