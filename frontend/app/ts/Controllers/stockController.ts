/**
 * Stock Controller
 * 
 * Controller associated with the home page of the application
 */
app.controller("stockCtrl", ($scope) => {
   //init the modal so it can actually be opened properly (href doesn't play nicely with routing)
    $('.modal').modal();

    $scope.openModalAddStock = function() {
        $('#modalAddStock').modal('open');
    }

    $scope.addNewStockItem = function() {
        alert("Will add a new stock item, is not currently functioning");
    }
});



