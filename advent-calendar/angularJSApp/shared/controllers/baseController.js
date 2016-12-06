"use strict";
app.controller('baseController', ['$scope', 'sessionService', '$state', 'logoutFactory', function ($scope, sessionService,  $state, logoutFactory) {
    
    $scope.LoggedIn = function () {
        return sessionService.GetToken() !== undefined;
    }

    $scope.currentLoggedInUserRoleName = sessionService.GetCurrentLoggedInUserRoleName();

    $scope.$watch(function () { return sessionService.GetCurrentLoggedInUserRoleName(); }, function (newValue, oldValue) {
        /* Do the stuff */
        if (newValue !== oldValue) {
            console.log(new Date().toString() +
                " **DEBUG** " +
                "From baseController.js $watch.sessionService.GetCurrentLoggedInUserRoleName(); newValue = " +
                newValue);
            $scope.currentLoggedInUserRoleName = newValue;
        }
    }, true);

    $scope.AdministratorMenuShouldBeDisplayed = function () {

        var isLoggedInAdministrator = $scope.currentLoggedInUserRoleName === 'SUPER_ADMINISTRATOR' ||
                                      $scope.currentLoggedInUserRoleName === 'USER_ADMINISTRATOR';
        return isLoggedInAdministrator;
    }

    $scope.LogOut = function () {
        logoutFactory()
            .then(function (response) {
                sessionService.SetToken(undefined);
                sessionService.SetCurrentLoggedInUserRoleName(undefined);
                //what should we do when we log out?
                $state.go('otherwise');
            }, function (response) {
                var logoutError = response.error_description;
                // report something
                console.log(new Date().toString() + " **ERROR** " + " From baseController.js LogOut(), error reported " + logoutError);
            });
    }
}]);