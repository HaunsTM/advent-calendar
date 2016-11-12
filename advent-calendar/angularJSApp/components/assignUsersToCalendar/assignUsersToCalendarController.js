"use strict";
app.controller('assignUsersToCalendarController', ['$scope', 'registerFactory', function ($scope, registerFactory) {

    var emptyCalendarUserTemplate = {
        userName: undefined,
        userPassword: undefined
    };

    $scope.calendarUsers = [emptyCalendarUserTemplate];

    $scope.AddNewUser = function () {
        var newUserNo = $scope.calendarUsers.length + 1;
        $scope.calendarUsers.push({ 'id': 'choice' + newUserNo });
    };

    $scope.RemoveUser = function () {
        var lastUser= $scope.calendarUsers.length - 1;
        $scope.calendarUsers.splice(lastUser);
    };

    $scope.RegisterStandardUsers = function () {

        angular.forEach($scope.calendarUsers, function (calendarUser) {
                registerFactory.RegisterStandardUsers({
                    UserName: calendarUser.userName,
                    Password: calendarUser.userPassword
            })
            .then(function (successfulResponse) {

            }, function (errorResponse) {
                $scope.registerUserAdministratorForm.errorMessage = errorResponse;
            });
        });


        
    }
}]);