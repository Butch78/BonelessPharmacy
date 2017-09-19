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
});
