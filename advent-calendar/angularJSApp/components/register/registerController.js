adventCalendarApp.controller('RegisterController', ['$scope', '$location', 'RegistrationFactory', function ($scope, $location, RegistrationFactory) {
    $scope.registerForm = {
        emailAddress: '',
        password: '',
        confirmPassword: ''
    };

    $scope.register = function () {

        var result = RegistrationFactory($scope.registerForm.emailAddress,
                                         $scope.registerForm.password,
                                         $scope.registerForm.confirmPassword);

        result.then(function (result) {
            if (result.success) {
                //till vilken route (definierad i app.routes.js) ska vi navigera om vi lyckas registera oss in?
                $location.path('/createCalendar');
            } else {
                $scope.registerForm.registrationFailure = true;
            }
        });
    }
}]);