"use strict";
adventCalendarApp.controller('registerController', ['$scope', 'loginFactory', 'registerFactory', 'sessionService', function ($scope, loginFactory, registerFactory, sessionService) {
    $scope.registerForm = {
        username: undefined,
        password: undefined,
        confirmPassword: undefined,
        errorMessage: undefined
    };

    $scope.register = function () {
        registerFactory($scope.registerForm.username, $scope.registerForm.password, $scope.registerForm.confirmPassword)
        .then(function () {
            loginFactory($scope.registerForm.username, $scope.registerForm.password)
            .then(function (response) {
                sessionService.setToken(response.access_token);
            }, function (response) {
                $scope.loginForm.errorMessage = response;
            });
        }, function (response) {
            $scope.registerForm.errorMessage = response;
        });
    }
}]);