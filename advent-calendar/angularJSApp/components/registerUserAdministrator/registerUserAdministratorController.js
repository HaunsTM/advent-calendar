"use strict";
app.controller('registerUserAdministratorController', ['$scope', 'loginFactory', 'registerFactory', 'sessionService', function ($scope, loginFactory, registerFactory, sessionService) {
    $scope.registerUserAdministratorForm = {
        username: undefined,
        password: undefined,
        confirmPassword: undefined,
        errorMessage: undefined
    };

    $scope.RegisterUserAdministrator = function () {
        registerFactory.RegisterUserAdministrator($scope.registerUserAdministratorForm.username, $scope.registerUserAdministratorForm.password, $scope.registerUserAdministratorForm.confirmPassword)
        .then(function () {
            loginFactory($scope.registerUserAdministratorForm.username, $scope.registerUserAdministratorForm.password)
            .then(function (response) {
                sessionService.SetToken(response.access_token);
            }, function (response) {
                $scope.loginForm.errorMessage = response;
            });
        }, function (response) {
            $scope.registerUserAdministratorForm.errorMessage = response;
        });
    }
}]);