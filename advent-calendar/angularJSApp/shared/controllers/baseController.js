"use strict";
app.controller('baseController', ['$scope', 'sessionService', function ($scope, sessionService) {
    $scope.loggedIn = function () {
        return sessionService.getToken() !== undefined;
    }
}]);