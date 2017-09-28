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

    $scope.changeStaff = (staff: Staff) => {
        $http(Boneless.CreateRequest("auth", "post", { name: staff.name })).then((res) => {
            BtnClick = true;
            Boneless.NotifyCustom(`Welcome`);
            Boneless.Login(staff);
            Boneless.SetToken(res.data.token);
        }, (errorRes) => Boneless.NotifyCustom("Login Failed"));
    };
});

// function changeStaffButton(name) {
//     // alert("This works");
//     let temp = document.getElementById("staffTitle");
//     temp.innerText = name;
// }
