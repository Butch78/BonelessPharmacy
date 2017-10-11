/**
 * Help Controller
 * Controller associated with the home page of the application
 */
app.controller("helpCtrl", ($scope) => {

    $('.collapsible').collapsible();

    $scope.HelpItems =
    [{
            title:"Logging Into Boneless Pharmacy", 
            desc: ["1. Select the Your User Account","2. Type in your Password", "3. Press Login"],
        },{
            title:"Logging Out", 
            desc: ["1. Press the Button in the Top Right Corner displaying your Name", "2. Press the Logout Button"],
        },{
            title:"Creating a Sale", 
            desc: ["1. Press the plus button in the bottom right corner", "2. Enter the PLU of the Item", "3. Press the Enter Button on the Keyboard"],
        }, {
            title:"Viewing a Sale", 
            desc: ["1. Press on the More button next to the Sale you wish to view"],
        },{
            title:"Creating a Stock Item", 
            desc: ["1. Press the plus button in the bottom right corner", "2. Enter the Name, Retail Price, Quantity, Item Amount and Measurement", "3.Press the Add Button"],
        },{
            title:"Editing a Stock Item", 
            desc: ["1. Press the Edit Button next to the Sale item you wish to edit", "2. Change the relevant details", "3. Press Save changes to save the changes"],
        },{
            title:"Deleting a Stock Item", 
            desc: ["1. Press the Edit Button next to the Sale item you wish to delete", "2. Press the Delete Item Button", "3.Press confirm to delete the item", "The items are not delete, instead they are archived"],
        },{
            title:"Viewing information about a Stock Item", 
            desc: ["1. Press the More Button next to the Sale item you wish to view"],
        }{
            title:"Restoring a Stock Item", 
            desc: ["1. On the Stock Page, press the Archive Button","2. Press the Restore Button next to the Sale item you wish to restore", "3. Press the Confirm Button"],
        },
        {
            title:"Generating a report", 
            desc: ["1. Press on the Type of report and time frame for the report you wish to generate", "2. Press the Download CSV Button to download the file."],
        },{
            title:"Viewing a Report", 
            desc: ["1. Select the View button on the Reports & Prediction Page","2. Choose from the drop down menu which save report you would like to view", "3. Press the Stats Button to Download the Report or receive further information"],
        },
];
});

