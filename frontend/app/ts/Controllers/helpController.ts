/**
 * Help Controller
 * Controller associated with the home page of the application
 */
app.controller("helpCtrl", ($scope) => {

    $('.collapsible').collapsible();

    $scope.HelpItems =
    [{
        title:"Logging In", 
        desc: ["1. Type in your Username and Password", "2. Press Login"],
        },{
            title:"Creating a Sale", 
            desc: ["1. Press the plus button in the bottom right corner", "2.Enter the PLU of the Item", "3.Complete the Transaction"],
        }, {
            title:"Editing a Sale", 
            desc: ["1. Press on the sale you wish to edit", "2. Edit the sale and press save changes"],
        }, {
            title:"Creating a report", 
            desc: ["1. Press on the kind of the report you require"],
    }];
});

