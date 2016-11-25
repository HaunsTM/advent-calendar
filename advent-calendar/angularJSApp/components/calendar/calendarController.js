"use strict";
app.controller('calendarController', ['$scope', '$uibModal', 'calendarFactory', function ($scope, $uibModal, calendarFactory) {
    $scope.currentYear = new Date().getFullYear().toString();

    $scope.calendar = {};

    var GetCalendarData = function (yearForCurrentLoggedUsersCalendar) {
        return calendarFactory.GetCalendar(yearForCurrentLoggedUsersCalendar)
            .then(
                function (answer) {
                    // do something
                    $scope.calendar = answer;
                },
                function (error) {
                    // report something

                },
                function (progress) {
                    // report progress
                });
    };

    var InitializeController = function () {
        GetCalendarData($scope.currentYear);
    };

    $scope.OpenSlot = function (slotNumber) {
        $scope.modalOptions = {};

        var initialModalOptions = {
            closeButtonText: 'Stäng',
            actionButtonText: 'OK',
            headerText: '',
            bodyText: 'Hämtar lucka...',
            slotImgSrcData: '/angularJSApp/images/santa-cheers.gif'
        };

        $scope.modalOptions = initialModalOptions;

        var slotInstanceModal = $uibModal.open({
            animation: $scope.animationsEnabled,
            backdrop: 'static',
            keyboard: true,
            modalFade: true,
            templateUrl: '/angularJSApp/components/calendar/slot/slotContentModalView.html',
            scope: $scope
        });


        slotInstanceModal.opened.then(function () {
            calendarFactory.GetSlotFromServer($scope.currentYear, slotNumber)
        .then(
            function (answer) {
                // do something
                $scope.modalOptions.headerText = 'Lucka ' + answer.Number;
                $scope.modalOptions.bodyText=answer.SlotMessage,
                    $scope.modalOptions.slotImgSrcData=" data:" + answer.ContentType + ";base64," + answer.ContentAsBase64


            },
            function (error) {
                // report something
                console.warn(new Date().toString() + " **ERROR** " + " From slotContentModalService.js OpenSlot, error reported " + error);
            },
            function (progress) {
                // report progress
            });
        });




        return slotInstanceModal.result;
    };
    

    // run while your controller loads
    InitializeController();
}]);