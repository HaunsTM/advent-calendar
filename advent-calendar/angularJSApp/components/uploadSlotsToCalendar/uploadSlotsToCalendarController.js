"use strict";
adventCalendarApp.controller('uploadSlotsToCalendarController', ['$scope', '$http', function ($scope, $http) {
    $scope.Message = "This is createCalendarController page";

    $scope.slots = [];

    var initiateSlotArray = function() {
        var firstSlotNumber = 1;
        var lastSlotNumber = 24;
        //let's create an array
        for (var i = firstSlotNumber; i < lastSlotNumber + 1; i++) {
            $scope.slots.push({
                number: i,
                imageFile: '',
                clue: ''
            });
        }
    }

    var initializeController = function() {
        initiateSlotArray();
    }

    // run while your controller loads
    initializeController();


}]);