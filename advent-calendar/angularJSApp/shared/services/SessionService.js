"use strict";
/* Vi behöver en plats att spara autentiseringstoken när de kommer från vårt API - en sak som vi kan göra i en Service. Services i AngularJS är singletons [Singleton är ett designmönster som innebär att man begränsar antalet instanser av en klass till ett och endast ett objekt.]
*/

app.service('sessionService', ['$localStorage', function ($localStorage) {

    this.GetToken = function () {
        return $localStorage.adventCalendarAppToken;
    }

    this.SetToken = function (token) {
        $localStorage.adventCalendarAppToken = token;
    }

    this.GetCurrentLoggedInUserRoleName = function () {
        return $localStorage.currentLoggedInUserRoleName;
    }

    this.SetCurrentLoggedInUserRoleName = function (loggedInUserRoleName) {
        return $localStorage.currentLoggedInUserRoleName = loggedInUserRoleName;
    }

    this.apiUrl = 'http://localhost:5586';
}]);