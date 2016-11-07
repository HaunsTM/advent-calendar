"use strict";
app.controller('uploadSlotsToCalendarController', ['$scope', 'Upload', 'calendarEntityService', 'calendarFactory', 'sessionService', function ($scope, Upload, calendarEntityService, calendarFactory, sessionService) {
    $scope.Message = "This is createCalendarController page";

    $scope.slots = [];

    var initiateSlotArray = function() {
        var firstSlotNumber = 1;
        var lastSlotNumber = 3;
        //let's create an array
        for (var i = firstSlotNumber; i < lastSlotNumber + 1; i++) {
            $scope.slots.push({
                slotNumber: i,
                imageFile: undefined,
                slotMessage: '',
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

        angular.forEach($scope.slots, function (slot) {
            var formData =  {
                    calendarYear: calendarEntityService.getCurrentCalendarYear(),
                    file: slot.imageFile,
                    slotNumber:slot.slotNumber,
                    slotMessage: slot.slotMessage
            }

            var upload = Upload.upload({
                method: 'POST',
                url: 'api/Slots/UploadSlot',
                data: formData,
                headers: {
                    'Authorization': 'Bearer ' + sessionService.getToken()
                }
            });

            // returns a promise
            upload.then(function (resp) {
                // file is uploaded successfully
                console.log('file ' + resp.config.data.file.name + 'is uploaded successfully. Response: ' + resp.data);
            }, function (resp) {
                // handle error
            }, function (evt) {
                // progress notify
                console.log('progress: ' + parseInt(100.0 * evt.loaded / evt.total) + '% file :' + evt.config.data.file.name);
            });

        });

    };
    


}]);