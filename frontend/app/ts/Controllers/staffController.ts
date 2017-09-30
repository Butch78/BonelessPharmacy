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
            $http(Boneless.CreateRequest("api/Roles", "get")).then(
                (resRoles) => $scope.roles = resRoles.data as Role[],
                (errorResRoles) => Boneless.Notify(BonelessStatusMessage.INVALID_POST),
            );
        },
        (errorRes) => {
            Boneless.Notify(BonelessStatusMessage.INVALID_GET);
        });

    $(document).ready(() => {
        $('.collapsible').collapsible();
    });

    $scope.changeStaff = (staff: Staff) => {
        $http(Boneless.CreateRequest("auth", "post",
            {
                name: staff.name,
                password: (document.getElementById(`input-password-${staff.id}`) as HTMLInputElement).value,
            })).then((res) => {
                BtnClick = true;
                Boneless.NotifyCustom(`Welcome`);
                Boneless.Login(staff);
                Boneless.SetToken(res.data.token);
                window.location.reload();
            }, (errorRes) => Boneless.NotifyCustom("Login Failed"));
    };

    $scope.enumerateRole = (roleId: number) => {
        switch (roleId) {
            case 1:
                return "account_balance";
            case 2:
                return "local_pharmacy";
            default:
                return "person";
        }
    };

    $scope.getRole = (roleId: number) => ($scope.roles as Role[]).filter(r => r.id === roleId)[0];
});

// function changeStaffButton(name) {
//     // alert("This works");
//     let temp = document.getElementById("staffTitle");
//     temp.innerText = name;
// }
