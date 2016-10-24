"use strict";
adventCalendarApp.controller('uploadSlotsToCalendarController', ['$scope', 'Upload', 'calendarEntityService', 'calendarFactory', function ($scope, Upload, calendarEntityService, calendarFactory) {
    $scope.Message = "This is createCalendarController page";

    $scope.slots = [];

    var initiateSlotArray = function() {
        var firstSlotNumber = 1;
        var lastSlotNumber = 2;
        //let's create an array
        for (var i = firstSlotNumber; i < lastSlotNumber + 1; i++) {
            $scope.slots.push({
                number: i,
                imageFile: undefined,
                clue: '',
                uploadProgress: '',
                uploadErrorMessage: ''
            });
        }
    }

    var initializeController = function() {
        initiateSlotArray();
    }

    // run while your controller loads
    initializeController();

    $scope.UploadSlots = function () {

        angular.forEach(slots, function (slot) {
            var formData = new FormData();

            formData.append('calendarYear', calendarEntityService.selectedYear);
            formData.append('number', $scope.slots.number); //The variable 'calendarName' is for future use.
            formData.append('clue', $scope.slots.clue);

            file.upload = Upload.upload({
                url: 'https://angular-file-upload-cors-srv.appspot.com/upload',
                data: formData,
                headers: {
                    'Content-Type': undefined,
                    'Authorization': 'Bearer ' + sessionService.token
                }
            });

        });
    };



}]);