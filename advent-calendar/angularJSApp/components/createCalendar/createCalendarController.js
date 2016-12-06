"use strict";
app.controller('createCalendarController', ['$scope', '$state', 'calendarFactory', 'calendarEntityService', function ($scope, $state, calendarFactory, calendarEntityService) {

    $scope.calendar = {
        year: '',
        imageFile: '',
        name: 'NO_NAME'
    }

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

    
    
    $scope.UploadCalendar = function () {
        var formData = new FormData();
        formData = {
            calendarYear: $scope.calendar.year,
            imageFile: $scope.calendar.imageFile,
            calendarName: $scope.calendar.name
        }

        calendarFactory.UploadCalendar(formData)
            .then(
                function (answer) {

                    $scope.calendar = answer;

                    //vad ska vi göra när vi har laddat upp kalendern?
                    $state.go('stateUploadSlotsToCalendar');
                },
                function(error) {
                    // hantera felet
                },
                function(progress) {
                    // raportera hur progressionen fortskrider
                });
    };


    var InitializeController = function () {
        $scope.calendar.year = calendarEntityService.defaultCalendarYear;
    };

    // anropa när control har lästs in
    InitializeController();

}]);