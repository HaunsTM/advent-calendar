adventCalendarApp.controller('logoutController', ['$scope', '$location', 'logoutFactory', 'sessionService', function ($scope, $location, logoutFactory, sessionService) {

        $scope.Message = "This is logoutController page";

        $scope.loginForm = {
            username: undefined,
            password: undefined,
            errorMessage: undefined
        };

        $scope.logout = function () {
            debugger;
            logoutFactory()
                .then(function (response) {
                    debugger;
                    sessionService.setToken(response.access_token);
                    $location.path('/');
                }, function (response) {
                    debugger;
                    $scope.loginForm.errorMessage = response.error_description;
                });
        }
    }
]);