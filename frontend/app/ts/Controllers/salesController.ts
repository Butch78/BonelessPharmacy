/**
 * Home Controller
 * 
 * Controller associated with the home page of the application
 */
app.controller("salesCtrl", ($scope) => {
    $('.modal').modal();
    $('.collapsible').collapsible();

    $scope.openModalNewSale = function() {
        $('#modalNewSale').modal('open');
    }

    //will need to be adjusted later so that it takes in the ID of a sale, meaning it can present the relevant details
    $scope.openModalSaleDetails = function() {
        $('#modalSaleDetails').modal('open');
    }
});
