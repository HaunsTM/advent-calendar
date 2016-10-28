"use strict";
adventCalendarApp.controller('createCalendarController', ['$scope', 'calendarFactory', 'calendarEntityService', function ($scope, calendarFactory, calendarEntityService) {
    $scope.Message = "This is createCalendarController page";

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

    $scope.selectedYear = calendarEntityService.defaultCalendarYear;

    $scope.updateCalendarYear = function () {
        calendarEntityService.setCurrentCalendarYear($scope.selectedYear);
    }

    $scope.getTheFiles = function ($files) {

        formData.append('calendarName', 'NO_CALENDAR_NAME'); //The variable 'calendarName' is for future use.
        formData.append('calendarYear', $scope.selectedYear);

        angular.forEach($files, function (value, key) {
            formData.append(key, value);
        });
    };

    $scope.UploadCalendar = function() {
        calendarFactory.UploadCalendar(formData)
            .then(
                function(answer) {
                    // do something
                    $scope.calendar = answer;
                },
                function(error) {
                    // report something
                    $scope.reset();
                },
                function(progress) {
                    // report progress
                });
    };

    $scope.reset = function() {
        angular.forEach(
            angular.element("input [type = 'file']"),
            function(inputElement) {
                angular.element(inputElement).val(null);
            });
        $scope.imagesrc = [];
        formData = new FormData();
    };

}]);