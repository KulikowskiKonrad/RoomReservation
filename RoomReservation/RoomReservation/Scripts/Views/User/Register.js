var app = angular.module('RR', []);
app.controller('RegisterCtrl', function ($scope, $http) {
    $scope.registerUser = function () {
        angular.forEach($scope.formRegisterUser.$error, function (field) {
            angular.forEach(field, function (errorField) {
                errorField.$setTouched();
            })
        });

        if ($scope.formRegisterUser.$valid) {
            $http.post("/api/ApiUser/RegisterUser",
                {
                    "Email": $scope.Email,
                    "Password": $scope.Password,
                    "ConfirmPassword": $scope.ConfirmPassword
                })
                .then(function (response) {
                    swal({
                        title: 'Użytkownik zarejestrowany',
                        type: 'success',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok!',
                    }).then(function () {
                        location.href = '/User/Login';
                    });
                })
                .catch(function (data, status) {
                    swal({
                        title: data.data,
                        type: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok!',
                    });
                });
        }
    }
});