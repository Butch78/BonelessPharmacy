/**
 * Help Controller
 * Controller associated with the home page of the application
 */
app.controller("helpCtrl", ($scope) => {
    $('.collapsible').collapsible();

    $scope.HelpItems = [{title:"example help item", desc:"Description on how to do stuff"}];
});
