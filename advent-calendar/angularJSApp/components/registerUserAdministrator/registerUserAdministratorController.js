"use strict";
app.controller('registerUserAdministratorController', ['$scope', '$state', 'loginFactory', 'registerFactory', 'sessionService', function ($scope, $state, loginFactory, registerFactory, sessionService) {
    $scope.registerUserAdministratorForm = {
        username: undefined,
        password: undefined,
        confirmPassword: undefined,
        errorMessage: undefined
    };

    $scope.RegisterUserAdministrator = function () {
        //let's try to register
        registerFactory.RegisterUserAdministrator($scope.registerUserAdministratorForm.username, $scope.registerUserAdministratorForm.password, $scope.registerUserAdministratorForm.confirmPassword)
        .then(function () {
            //we have successfully registered, try to log in
            loginFactory($scope.registerUserAdministratorForm.username, $scope.registerUserAdministratorForm.password)
            .then(function (response) {
                //ok, we have successfully authenticated, let's store returned authentication data for further use
                sessionService.SetToken(response.access_token);
                sessionService.SetCurrentLoggedInUserRoleName(response.currentLoggedInUserRoleName);
                //what should we do when we are logged in?
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