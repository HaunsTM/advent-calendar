"use strict";
app.controller('baseController', ['$scope', 'sessionService', 'constants', function ($scope, sessionService, constants) {
    debugger;
    $scope.constants = function() {
        return constants; 
    }

    $scope.LoggedIn = function () {
        return sessionService.GetToken() !== undefined;
    }

    $scope.CurrentLoggedInUserRole = function() {
         return sessionService.GetCurrentLoggedInUserRoleName();
    }

}]);