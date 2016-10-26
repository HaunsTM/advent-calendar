"use strict";
adventCalendarApp.controller('calendarController',['$scope', 'calendarFactory', function($scope, calendarFactory) {
        $scope.Message = "This is calendarController page";

        $scope.currentYear = new Date().getFullYear().toString();

        $scope.calendar = {};
        $scope.slot = {};

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
        //SinitializeController();

        $scope.OpenSlot = function (slotNumber) {
            calendarFactory.OpenSlot($scope.currentYear, slotNumber)
                .then(
                    function (answer) {
                        // do something
                        $scope.slot = answer;
                    },
                    function (error) {
                        // report something

                    },
                    function (progress) {
                        // report progress
                    });
        }

    }
]);