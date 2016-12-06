"use strict";
app.controller('calendarController', ['$scope', '$uibModal', 'calendarFactory', function ($scope, $uibModal, calendarFactory) {

    $scope.currentYear = new Date().getFullYear().toString();

    $scope.modalOptions = {};
    
    $scope.calendar = {};

    var GetCalendarData = function (yearForCurrentLoggedUsersCalendar) {
        var imgSrcCalendarInitial = '/angularJSApp/images/santa-and-mrs-claus-kiss-smiley-emoticon.gif';

        $scope.calendar.imgSrc = imgSrcCalendarInitial;
        
        return calendarFactory.GetCalendar(yearForCurrentLoggedUsersCalendar)
            .then(
                function (answer) {
                    // do something
                    $scope.calendar.imgSrc = 'data:' + answer.ContentType + ';base64,' + answer.ContentAsBase64;
                },
                function (error) {
                    // report something

                },
                function (progress) {
                    // report progress
                });
    };

   
    $scope.OpenSlot = function (slotNumber) {

        // do something
        $scope.modalOptions.actionButtonText = 'Stäng';
        $scope.modalOptions.headerText = '...';
        $scope.modalOptions.bodyText = 'Hämtar lucka...',
        $scope.modalOptions.slotImgSrcData = '/angularJSApp/images/santa-cheers.gif';

        var slotInstanceModal = $uibModal.open({
            animation: $scope.animationsEnabled,
            backdrop: 'static',
            keyboard: true,
            modalFade: true,
            templateUrl: '/angularJSApp/components/calendar/slot/slotContentModalView.html',
            controller: 'slotContentModalController',
            scope: $scope
        });


        slotInstanceModal.opened.then(function () {
            calendarFactory.GetSlotFromServer($scope.currentYear, slotNumber)
        .then(
            function (answer) {
                // do something
                $scope.modalOptions.headerText = 'Lucka ' + answer.Number;
                $scope.modalOptions.bodyText = answer.SlotMessage,
                $scope.modalOptions.slotImgSrcData = "data:" + answer.ContentType + ";base64," + answer.ContentAsBase64;
                $scope.playSound('angularJSApp/sounds/slot-opening.mp3');

            },
            function (response) {
                //error – {string|Object} – The response body transformed with the transform functions.
                //status – {number} – HTTP status code of the response.
                console.warn(new Date().toString() + " **ERROR** " + " From slotContentModalService.js OpenSlot, error reported " + response.error);

                var headerText = '';
                var bodyText = '';
                var slotImgSrcData = '';

                headerText = 'Lucka ' + slotNumber;
                slotImgSrcData = '/angularJSApp/images/santa-at-computer.gif';
                switch (response.status) {
                    case 403:
                        bodyText = 'Den här luckan får du inte öppna än...';
                        break;
                    case 404:
                        bodyText = 'Oj! Den här luckan har visst försvunnit. ';
                        break;
                    default:
                        headerText = '';
                        bodyText = '';
                        slotImgSrcData = '';
                }
                $scope.modalOptions.headerText = headerText;
                $scope.modalOptions.bodyText = bodyText,
                $scope.modalOptions.slotImgSrcData = slotImgSrcData;

                $scope.playSound('angularJSApp/sounds/failure-sound.mp3');
            },
            function (progress) {
                // report progress
            });
        });

        return slotInstanceModal.result;
    };
    
    $scope.playSound = function (soundURL) {
        var audio = new Audio(soundURL);
        audio.play();
    };

    var InitializeController = function () {
        GetCalendarData($scope.currentYear);
    };

    // anropa när control har lästs in
    InitializeController();
}]);