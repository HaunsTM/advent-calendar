"use strict";
/* Vi behöver en plats att spara autentiseringstoken när de kommer från vårt API - en sak som vi kan göra i en Service. Services i AngularJS är singletons [Singleton är ett designmönster som innebär att man begränsar antalet instanser av en klass till ett och endast ett objekt.]
*/

app.service('sessionService', ['$localStorage', 'constants', function ($localStorage, constants) {

    this.GetToken = function () {
        return $localStorage.adventCalendarAppToken;
    }

    this.SetToken = function (token) {
        $localStorage.adventCalendarAppToken = token;
    }

    this.GetCurrentLoggedInUserRoleName = function () {
        var applicationUserRole;
        switch ($localStorage.currentLoggedInUserRoleName) {
            case 'SUPER_ADMINISTRATOR':
                applicationUserRole = constants.APPLICATION_USER_ROLE.ADMINISTRATOR;
                break;
            case 'USER_ADMINISTRATOR':
                applicationUserRole = constants.APPLICATION_USER_ROLE.ADMINISTRATOR;
                break;
            case 'STANDARD_USER':
                applicationUserRole = constants.APPLICATION_USER_ROLE.STANDARD_USER;
                break;
            default:
                applicationUserRole = undefined;
        }
        return applicationUserRole;
    }

    this.SetCurrentLoggedInUserRoleName = function (loggedInUserRoleName) {
        console.log(new Date().toString() + " **DEBUG** " + "From server, current logged in user role: " + loggedInUserRoleName);
        return $localStorage.currentLoggedInUserRoleName = loggedInUserRoleName;
    }

    this.apiUrl = 'http://localhost:5586';
}]);
