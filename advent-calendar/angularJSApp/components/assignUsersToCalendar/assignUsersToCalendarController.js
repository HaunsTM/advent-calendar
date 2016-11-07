"use strict";
app.controller('assignUsersToCalendarController', function ($scope) {

    $scope.calendarUsers = {};

    $scope.addNewUser = function () {
        var newItemNo = $scope.calendarUsers.length + 1;
        $scope.calendarUsers.push({ 'id': 'choice' + newItemNo });
    };

    $scope.removeUser = function () {
        var lastItem = $scope.calendarUsers.length - 1;
        $scope.calendarUsers.splice(lastItem);
    };

})