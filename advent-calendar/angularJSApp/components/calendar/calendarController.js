"use strict";
adventCalendarApp.controller('calendarController',['$scope', 'calendarFactory', function($scope, calendarFactory) {
        $scope.Message = "This is calendarController page";

        $scope.currentYear = new Date().getFullYear().toString();

        $scope.calendar = {};

        var GetCalendarData = function(yearForCurrentLoggedUsersCalendar) {
            return calendarFactory.GetCalendar(yearForCurrentLoggedUsersCalendar)
                .then(
                    function(answer) {
                        // do something
                        $scope.calendar = answer;
                    },
                    function(error) {
                        // report something

                    },
                    function(progress) {
                        // report progress
                    });
        };

        var initializeController = function() {
            GetCalendarData($scope.currentYear);
        };

        // run while your controller loads
        initializeController();

    }
]);