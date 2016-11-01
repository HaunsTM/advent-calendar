"use strict";
adventCalendarApp.controller('calendarController', ['$scope', 'calendarFactory',  function ($scope, calendarFactory) {
//adventCalendarApp.controller('calendarController', ['$scope', 'calendarFactory', 'slotContentModalService', function ($scope, calendarFactory, slotContentModalService) {
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
                        console.log(answer);
                    },
                    function(error) {
                        // report something
                        console.log(error);
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

        $scope.OpenSlot = function (slotNumber) {
            calendarFactory.OpenSlot($scope.currentYear, slotNumber)
                .then(
                    function (answer) {
                        // do something
                        $scope.slot = answer;

                        var modalDefaults = {
                            backdrop: true,
                            keyboard: true,
                            modalFade: true,
                            templateUrl: '/angularJSApp/components/calendar/slotContentModal.html'
                        };

                        var modalOptions = {
                            closeButtonText: 'Close',
                            actionButtonText: 'OK',
                            headerText: 'Proceed?',
                            bodyText: '',
                            slot: answer
                        };
                        //slotContentModalService.show(modalDefaults, modalOptions);
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