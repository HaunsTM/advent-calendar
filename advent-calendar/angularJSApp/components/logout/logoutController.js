"use strict";
app.controller('logoutController', ['$scope', '$location', 'logoutFactory', 'sessionService', function ($scope, $location, logoutFactory, sessionService) {

        $scope.Message = "This is logoutController page";

        $scope.logoutResponse = undefined;

        $scope.logout = function () {
            logoutFactory()
                .then(function (response) {
                    sessionService.SetToken(undefined);
                    sessionService.SetCurrentLoggedInUserRoleName(undefined);
                    $location.path('/');
                }, function (response) {
                    $scope.logoutResponse = response.error_description;
                });
        }
    }
]);