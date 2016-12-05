"use strict";
app.controller('registerUserAdministratorController', ['$scope', '$state', 'loginFactory', 'registerFactory', 'sessionService', function ($scope, $state, loginFactory, registerFactory, sessionService) {
    $scope.registerUserAdministratorForm = {
        username: undefined,
        password: undefined,
        confirmPassword: undefined,
        errorMessage: undefined
    };

    $scope.RegisterUserAdministrator = function () {
        //låt oss försöka registrera oss
        registerFactory.RegisterUserAdministrator($scope.registerUserAdministratorForm.username, $scope.registerUserAdministratorForm.password, $scope.registerUserAdministratorForm.confirmPassword)
        .then(function () {
            //sådär, vi lyckades! Låt oss logga in!
            loginFactory($scope.registerUserAdministratorForm.username, $scope.registerUserAdministratorForm.password)
            .then(function (response) {
                //ok, vi har loggat in, låt oss spara autentiseringsdata som vi kan använda senare
                sessionService.SetToken(response.access_token);
                sessionService.SetCurrentLoggedInUserRoleName(response.currentLoggedInUserRoleName);
                //vad ska vi göra när vi har loggat in?
                $state.go('stateCreateCalendar');
            }, function (errorResponse) {
                $scope.loginForm.errorMessage = errorResponse;
                console.log(new Date().toString() +
                    " **ERROR** " +
                    "From registerUserAdministratorController.js, couldn't login: " +
                    errorResponse);
            });
        }, function (errorResponse) {
            $scope.registerUserAdministratorForm.errorMessage = errorResponse;
                console.log(new Date().toString() +
                    " **ERROR** " +
                    "From registerUserAdministratorController.js, couldn't register: " +
                    errorResponse);
        });
    }
}]);