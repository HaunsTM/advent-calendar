"use strict";
adventCalendarApp.controller('loginController', ['$scope', '$location', 'loginFactory', 'sessionService', function ($scope, $location, loginFactory, sessionService) {

    $scope.Message = "This is loginController page";
    $scope.loginForm = {
        username: undefined,
        password: undefined,
        errorMessage: undefined
    };

    $scope.login = function () {
        loginFactory($scope.loginForm.username, $scope.loginForm.password)
        .then(function (response) {
            sessionService.setToken(response.access_token);
            $location.path('/');
        }, function (response) {
            $scope.loginForm.errorMessage = response.error_description;
        });
    }
}]);