﻿"use strict";
app.controller('loginController', ['$scope', '$location', 'loginFactory', 'sessionService', function ($scope, $location, loginFactory, sessionService) {

    $scope.loginForm = {
        userName: undefined,
        userPassword: undefined,
        errorMessage: undefined
    };

    $scope.login = function () {
        loginFactory($scope.loginForm.userName, $scope.loginForm.userPassword)
        .then(function (response) {
            sessionService.SetToken(response.access_token);
            sessionService.SetCurrentLoggedInUserRoleName(response.currentLoggedInUserRoleName); /*se ApplicationOAuthProvider.cs, där */
            $location.path('/');
        }, function (response) {
            if (response.error_description !== "The user name or password is incorrect.") {
                $scope.loginForm.errorMessage = response.error_description;
            } else {
                $scope.loginForm.errorMessage = "Felaktigt användarnamn eller lösenord!";
                
            }
        });
    }
}]);