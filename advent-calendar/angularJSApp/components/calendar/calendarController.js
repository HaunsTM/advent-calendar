"use strict";
//app.controller('calendarController', ['$scope', 'calendarFactory',  function ($scope, calendarFactory) {
app.controller('calendarController', ['$scope', 'calendarFactory', 'modalService', function ($scope, calendarFactory, modalService) {
        $scope.Message = "This is calendarController page";

        $scope.currentYear = new Date().getFullYear().toString();

        $scope.calendar = {};
        $scope.slot = {};

        $scope.slotContentLoaded = false;

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

        $scope.OpenSlot = function (slotNumber) {
           calendarFactory.OpenSlot($scope.currentYear, slotNumber)
            .then(
                function (answer) {
                    // do something
                    $scope.slot = answer;
                    var modalCalendarSettings = {
                        backdrop: true,
                        keyboard: true,
                        modalFade: true,
                        templateUrl: '/angularJSApp/components/calendar/slotContentModal.html'
                    };

                    var modalOptions = {
                        closeButtonText: 'Close',
                        actionButtonText: 'OK',
                        headerText: 'Lucka ' + answer.Number,
                        bodyText: answer.SlotMessage ,
                        slotImgSrcData: "data:" + answer.ContentType + ";base64," + answer.ContentAsBase64
                    };
                    modalService.show(modalCalendarSettings, modalOptions);
                },
                function (error) {
                    // report something
                    console.log(new Date().toString() + " **ERROR** " + " From calendarController.js OpenSlot, error reported " + error);
                },
                function (progress) {
                    // report progress
                });

        
        }

        // run while your controller loads
        initializeController();
    }
]);