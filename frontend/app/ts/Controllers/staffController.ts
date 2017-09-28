/**
 * Staff Controller
 * Controller associated with the staff page of the application
 */
let BtnClick: boolean;

app.controller("staffCtrl", ($scope, $http) => {
    // BtnClick = false;

    // GET Staff details
    $http(Boneless.CreateRequest("api/Staff", "get")).then(
        (res) => {
            $scope.staff = res.data as Staff[];
        },
        (errorRes) => {
            alert(errorRes.data);
        });

    $(document).ready(() => {
        $('.collapsible').collapsible();
    });

    $scope.changeStaffButton = (StaffName) => {
        let oldStaffBtn = document.getElementById("staffTitle");
        let staffBtn = document.getElementById("dropDown");
        staffBtn.innerText = StaffName;
        oldStaffBtn.style.display = 'none';
        staffBtn.style.display = 'inline-block';
        staffBtn.setAttribute("href", "#!"); // Maybe this can be imporved??
        BtnClick = true;
    };
});

// function changeStaffButton(name) {
//     // alert("This works");
//     let temp = document.getElementById("staffTitle");
//     temp.innerText = name;
// }
