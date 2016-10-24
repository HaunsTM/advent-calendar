"use strict";
adventCalendarApp.service('calendarEntityService', [function () {

    this.defaultCalendarYear = new Date().getFullYear().toString();

    var currentCalendarYear = undefined;

    this.setCurrentCalendarYear = function (year) {
        currentCalendarYear = year;
    };

    this.getCurrentCalendarYear = function () {
        return currentCalendarYear;
    };


}]);