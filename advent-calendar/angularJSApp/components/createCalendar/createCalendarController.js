"use strict";
adventCalendarApp.controller('createCalendarController', ['$scope', '$http', function ($scope, $http) {
    //$scope.Message = "This is createCalendarController page";

    //$scope.slots = [];

    //var initiateSlotArray = function() {
    //    var firstSlotNumber = 1;
    //    var lastSlotNumber = 24;
    //    //let's create an array
    //    for (var i = firstSlotNumber; i < lastSlotNumber + 1; i++) {
    //        $scope.slots.push({
    //            number: i,
    //            imageFile: '',
    //            clue: ''
    //        });
    //    }
    //}

    //var initializeController = function() {
    //    initiateSlotArray();
    //}

    //// run while your controller loads
    //initializeController();

    //http://www.encodedna.com/angularjs/tutorial/angularjs-file-upload-using-http-post-formdata-webapi.htm
    var formData = new FormData();

    $scope.calendarYears = [
        {
            name: 'År 2015',
            value: '2015'
        },
        {
            name: 'År 2016',
            value: '2016'
        },
        {
            name: 'År 2017',
            value: '2017'
        },
        {
            name: 'År 2018',
            value: '2018'
        },
        {
            name: 'År 2019',
            value: '2019'
        },
        {
            name: 'År 2020',
            value: '2020'
        },
        {
            name: 'År 2021',
            value: '2021'
        }
    ];

    $scope.selectedYear = new Date().getFullYear().toString();

    $scope.getTheFiles = function ($files) {

        angular.forEach($files, function (value, key) {

            formData.append(key, value);
            formData.append('calendarName', 'hello world');
            formData.append('calendarYear', '2013');
        });
    };

    $scope.uploadCalendar = function() {
        var request = {
            method: 'POST',
            url: 'api/uploadCalendar',
            data: formData,
            headers: {
                'Content-Type': undefined
            }
        };

        $http(request)
            .success(function(data) {
                console.log(data);
                $scope.reset();
            })
            .error(function (data) {
                console.log(data);
            });
    }

    $scope.reset = function() {
        angular.forEach(
            angular.element("input [type = 'file']"),
            function(inputElement) {
                angular.element(inputElement).val(null);
            });
        $scope.imagesrc = [];
        formData = new FormData();
    }

}]);