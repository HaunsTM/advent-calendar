adventCalendarApp.controller('RegisterController', ['$scope', function ($scope) {
    $scope.registerForm = {
        emailAddress: '',
        password: '',
        confirmPassword: ''
    };

    $scope.register = function () {
        alert("hej");
    }
}]);