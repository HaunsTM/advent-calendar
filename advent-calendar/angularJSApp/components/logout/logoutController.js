"use strict";
adventCalendarApp.controller('logoutController', ['$scope', '$location', 'logoutFactory', 'sessionService', function ($scope, $location, logoutFactory, sessionService) {

        $scope.Message = "This is logoutController page";

        $scope.logoutResponse = undefined;

        $scope.logout = function () {
            logoutFactory()
                .then(function (response) {
                    sessionService.setToken(response.access_token);
                    $location.path('/');
                }, function (response) {
                    $scope.logoutResponse = response.error_description;
                });
        }
    }
]);