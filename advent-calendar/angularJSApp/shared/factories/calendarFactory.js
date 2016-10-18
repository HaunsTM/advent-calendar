"use strict";
adventCalendarApp.factory('calendarFactory', ['$q', '$http', 'sessionService', function ($q, $http, sessionService) {

    var fac = {};

    fac.GetCalendar =  function (yearForCurrentLoggedUsersCalendar) {
        var result = $q.defer();
        $http({
            method: 'GET',
            url: 'api/Calendars/GetCalendarByYearAndCurrentLoggedInUser',
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + sessionService.token },
            params: { year: yearForCurrentLoggedUsersCalendar }
        })
        .success(function (response) {
            result.resolve(response);
            })
        .error(function (response) {
            result.reject(response);
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