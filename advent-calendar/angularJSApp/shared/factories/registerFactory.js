"use strict";
/*http://www.codeproject.com/Articles/806029/Getting-started-with-AngularJS-and-ASP-NET-MVC-Par */
app.factory('registerFactory', ['$http', '$q', 'sessionService', function ($http, $q, sessionService) {

    var fac = {};

    fac.RegisterUserAdministrator = function(email, password, confirmPassword) {
        var result = $q.defer();
        $http({
                method: 'POST',
                url: '/api/Account/RegisterUserAdministrator',
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

    fac.RegisterStandardUsers = function (standardUsers) {
        var result = $q.defer();
        $http({
                method: 'POST',
                url: sessionService.apiUrl + '/api/Account/RegisterStandardUsers',
                data: standardUsers ,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + sessionService.GetToken()
                }
            })
            .success(function(response) {
                result.resolve(response);
            })
            .error(function(response) {
                result.reject(response);
            });

        return result.promise;
    };
    /**/
    return fac;
}]);