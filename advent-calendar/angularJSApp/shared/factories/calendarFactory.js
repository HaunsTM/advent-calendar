﻿"use strict";
app.factory('calendarFactory', ['$q', '$http', 'sessionService', function ($q, $http, sessionService) {

    var fac = {};

    fac.UploadCalendar = function (formData) {
        var result = $q.defer();
        var request = {
            method: 'POST',
            url: 'api/Calendars/UploadCalendar',
            data: formData,
            headers: {
                'Content-Type': undefined,
                'Authorization': 'Bearer ' + sessionService.GetToken()
            }
        };

        $http(request)
            .success(function (response) {
                result.resolve(response);
            })
            .error(function (response) {
                result.reject(response);
            });

        return result.promise;
    }

    fac.GetCalendar =  function (yearForCurrentLoggedUsersCalendar) {
        var result = $q.defer();
        var request = {
            method: 'GET',
            url: 'api/Calendars/GetCalendarByYearAndCurrentLoggedInUser',
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + sessionService.GetToken() },
            params: { year: yearForCurrentLoggedUsersCalendar }
        };

        $http(request)
        .success(function (response) {
            result.resolve(response);
            })
        .error(function (response) {
            result.reject(response);
            });

        return result.promise;
    }

    fac.GetSlotFromServer = function (calendarYear, slotNumber) {

        var result = $q.defer();

        var request = {
            method: 'GET',
            url: sessionService.apiUrl + '/api/Slots/OpenSlot',
            params: { calendarYear: calendarYear, slotNumber: slotNumber },
            headers: {
                'Content-Type': undefined,
                'Authorization': 'Bearer ' + sessionService.GetToken()
            }
        };

        $http(request)
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