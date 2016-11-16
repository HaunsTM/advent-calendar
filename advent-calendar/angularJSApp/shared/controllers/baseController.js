"use strict";
app.controller('baseController', ['$scope', 'sessionService', function ($scope, sessionService) {
    $scope.LoggedIn = function () {
        return sessionService.GetToken() !== undefined;
    }
}]);