"use strict";
/* Vi behöver en plats att spara autentiseringstoken när de kommer från vårt API - en sak som vi kan göra i en Service. Services i AngularJS är singletons [Singleton är ett designmönster som innebär att man begränsar antalet instanser av en klass till ett och endast ett objekt.]
*/

adventCalendarApp.service('sessionService', ['$localStorage', function ($localStorage) {
    this.token = undefined;


    this.getToken = function () {
        if (!$localStorage.adventCalendarAppToken) {
            if (!this.token) {
                return undefined;
            }
            this.setToken(this.token);
        }
        return $localStorage.adventCalendarAppToken;
    }

    this.setToken = function (token) {
        this.token = token;
        $localStorage.adventCalendarAppToken = token;
    }

    this.apiUrl = 'http://localhost:5586';
}]);