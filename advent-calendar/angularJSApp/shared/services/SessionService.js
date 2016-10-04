"use strict";
/* Vi behöver en plats att spara autentiseringstoken när de kommer från vårt API - en sak som vi kan göra i en Service. Services i AngularJS är singletons [Singleton är ett designmönster som innebär att man begränsar antalet instanser av en klass till ett och endast ett objekt.]
*/

adventCalendarApp.service('sessionService', ['$cookies', function ($cookies) {
    this.token = undefined;


    this.getToken = function () {
        if (!$cookies.awesomeAngularWebAppToken) {
            if (!this.token) {
                return undefined;
            }
            this.setToken(this.token);
        }
        return $cookies.awesomeAngularWebAppToken;
    }

    this.setToken = function (token) {
        this.token = token;
        $cookies.awesomeAngularWebAppToken = token;
    }

    this.apiUrl = 'http://localhost:5586';
}]);