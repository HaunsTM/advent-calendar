"use strict";
app.controller('assignUsersToCalendarController', ['$scope', '$state', 'registerFactory', function ($scope, $state, registerFactory) {

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
                        console.log(new Date().toString() +
                            " **DEBUG** " +
                            "From assignUsersToCalendarController.js, RegisterStandardUsers() reported following successful response: " +
                            successfulResponse);

                //what should we do when we have uploaded our slots?
                $state.go('stateCalendar');
            }, function (errorResponse) {
                console.log(new Date().toString() + " **ERROR** " + "From server, current logged in user role: " +
                            "From assignUsersToCalendarController.js, RegisterStandardUsers() reported following failure: " +
                            errorResponse);
            });
        });


        
    }
}]);