/**
 * Staff Controller
 * Controller associated with the staff page of the application
 */
let BtnClick: boolean;

app.controller("staffCtrl", ($scope, $http) => {
    $('.modal').modal();
    $('#userRoleId').material_select();
    $scope.openCreateUserModal = () => {
        $("#openCreateUserModal").modal('open');
    };
    $scope.newUser = {};
    $scope.isLoggedIn = Boneless.IsLoggedIn();
    if ($scope.isLoggedIn === false) {
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
                    Boneless.Logout();
                    BtnClick = true;
                    Boneless.NotifyCustom(`Welcome`);
                    Boneless.Login(staff);
                    Boneless.SetToken(res.data.token);
                    window.location.reload();
                }, (errorRes) => {
                    Boneless.NotifyCustom("Login Failed");
                    console.log('====================================');
                    console.log(`LOGIN MACHINE BROKE HARHAR NICE ONE CAMPBELL`);
                    console.log('====================================');
                });
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

        $scope.submitLogin = (keyEvent, staff: Staff) => {
            if (keyEvent.which === 13) {
                $scope.changeStaff(staff);
            }
        };
    } else {
        $scope.currentStaff = Boneless.Login();
        $http(Boneless.CreateRequest("api/Roles", "get")).then(
            (resRoles) => {
                $scope.roles = resRoles.data as Role[];
                $scope.currentRole = $scope.getRole($scope.currentStaff.roleId);
            },
            (errorResRoles) => Boneless.Notify(BonelessStatusMessage.INVALID_POST),
        );
        $scope.callCurrent = () => window.location.assign(`tel:${$scope.currentStaff.phoneNumber}`);
        $scope.createAccount = () => {
            $scope.newUser.roleid = $('#userRoleId').val();
            if (($scope.newUser.roleid as string).length === 0 ||
                ($scope.newUser.name as string).length === 0 ||
                ($scope.newUser.phone as string).length === 0) {
                Boneless.NotifyCustom("All fields required");
            } else {
                $http(Boneless.CreateRequest("api/Staff", "post", $scope.newUser)).then(
                    (res) => {
                        $("#openCreateUserModal").modal('close');
                        $('#newUserConfirmModal').modal('open');
                        $scope.newUserPassword = res.data;
                        $scope.newUser = {};
                    },
                    (errorRes) => {
                        Boneless.NotifyCustom("New User Creation Failed")
                        console.log(errorRes);
                    },
                );
            }
        };
    }
    $scope.getRole = (roleId: number) => ($scope.roles as Role[]).filter((r) => r.id === roleId)[0];
});
