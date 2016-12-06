"use strict";
app.controller('uploadSlotsToCalendarController', ['$scope', '$state', 'Upload', 'calendarEntityService', 'calendarFactory', 'sessionService', function ($scope, $state, Upload, calendarEntityService, calendarFactory, sessionService) {
    $scope.Message = "This is createCalendarController page";

    $scope.slots = [];

    var initiateSlotArray = function() {
        var firstSlotNumber = 1;
        var lastSlotNumber = 24;
        //låt oss skapa en array
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

    // anropa när control anropas
    initializeController();

    $scope.UploadSlots = function () {
        //TODO: Det här borde egentligen ligga i factory
        angular.forEach($scope.slots, function (slot) {
            
            //har vi en korrekt fil i upload? 
            if (slot.imageFile) {

                var formData = {
                    calendarYear: calendarEntityService.getCurrentCalendarYear(),
                    file: slot.imageFile,
                    slotNumber: slot.slotNumber,
                    slotMessage: slot.slotMessage
                }

                var upload = Upload.upload({
                    method: 'POST',
                    url: 'api/Slots/UploadSlot',
                    data: formData,
                    headers: {
                        'Authorization': 'Bearer ' + sessionService.GetToken()
                    }
                });

                // returnerar ett promise
                upload.then(function(resp) {
                        // filen har laddats upp
                        console.log('file ' +
                            resp.config.data.file.name +
                            'is uploaded successfully. Response: ' +
                            resp.data);

                        //vad ska vi göra när vi har laddat upp luckan?
                        $state.go('stateAssignUsersToCalendar');
                    },
                    function(resp) {
                        // hantera felet
                    },
                    function(evt) {
                        // skriv ut hur det går
                        console.log('progress: ' +
                            parseInt(100.0 * evt.loaded / evt.total) +
                            '% file :' +
                            evt.config.data.file.name);
                    });
            }

        });

    };
    


}]);