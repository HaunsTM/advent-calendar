"use strict";
/*http://www.codeproject.com/Articles/806029/Getting-started-with-AngularJS-and-ASP-NET-MVC-Par */
adventCalendarApp.factory('registerFactory', ['$http', '$q', 'sessionService', function ($http, $q, sessionService) {

    var fac = {};

    fac.RegisterUserAdministrator = function(email, password, confirmPassword) {
        var result = $q.defer();
        $http({
                method: 'POST',
                url: sessionService.apiUrl + '/api/Account/RegisterUserAdministrator',
                data: { Email: email, Password: password, ConfirmPassword: confirmPassword },
                headers: { 'Content-Type': 'application/json' }
            })
            .success(function(response) {
                result.resolve(response);
            })
            .error(function(response) {
                result.reject(response);
            });

        return result.promise;
    };

    fac.RegisterStandardUser = function (email, password, confirmPassword) {
        var result = $q.defer();
        $http({
                method: 'POST',
                url: sessionService.apiUrl + '/api/Account/RegisterStandardUser',
                data: { Email: email, Password: password, ConfirmPassword: confirmPassword },
                headers: { 'Content-Type': 'application/json' }
            })
            .success(function(response) {
                result.resolve(response);
            })
            .error(function(response) {
                result.reject(response);
            });

        return result.promise;
    };

    return fac;
}]);