"use strict";
adventCalendarApp.factory('calendarFactory', ['$q', '$http', function ($q, $http) {

    var fac = {};

    fac.GetCalendar =  function (year) {
        var result = $q.defer();
        $http({
            method: 'GET',
            url: 'api/Calendar/GetCalendar' +'/' + year
        })
        .success(function (response) {
            result.resolve(response);
                debugger;
            })
        .error(function (response) {
            result.reject(response);
                debugger;
            });

        return result.promise;
    }

    fac.GetSlot = function (year, number) {
        return null;
        var result = $q.defer();
        $http({
            method: 'GET',
            url: sessionService.apiUrl + '/api/getCalendar' + '/' + year
        })
        .success(function (response) {
            result.resolve(response);
        })
        .error(function (response) {
            result.reject(response);
        });

        return result.promise;
    }

    return fac;

}]);