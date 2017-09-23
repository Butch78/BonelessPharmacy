/**
 * Home Controller
 * Controller associated with the home page of the application
 */
app.controller("salesCtrl", ($scope, $http) => {
    $('.modal').modal();
    $('.collapsible').collapsible();
    $scope.newSale = {};
    $scope.newSaleRecords = [];
    $scope.searchItems = [];
    // GET SalesRecords
    // $http(Boneless.CreateRequest("api/SalesRecords", "get")).then(
    //     (res) => {
    //         $scope.salesRecords = res.data as SalesRecord[];
    //     },
    //     (errorRes) => {
    //         alert(errorRes.data);
    //     });
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
        const successfullPosts: SalesRecord[] = [];
        for (const sr of $scope.newSaleRecords as SalesRecord[]) {
            if (sr.quantity === undefined) {
                sr.quantity = 0;
            }
            // Clear pre-defined object
            sr.salesItem = sr.sale = undefined;
            $http(Boneless.CreateRequest("api/SalesRecords", "post", sr)).then(
                (res) => successfullPosts.push(sr as SalesRecord),
            );
        }
        if (successfullPosts.length === $scope.newSaleRecords.length) {
            Boneless.NotifyCustom(`Sale #${$scope.newSale.id} (${successfullPosts.length} items)`);
        } else {
            Boneless.Notify(BonelessStatusMessage.INVALID_POST);
            for (const sr of successfullPosts) {
                $http(Boneless.CreateRequest(`api/SalesRecords/${sr.id}`, "delete"));
            }
        }
    };
});
