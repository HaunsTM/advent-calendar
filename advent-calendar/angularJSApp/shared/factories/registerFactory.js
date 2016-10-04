"use strict";
/*http://www.codeproject.com/Articles/806029/Getting-started-with-AngularJS-and-ASP-NET-MVC-Par */
adventCalendarApp.factory('registerFactory', ['$http', '$q','sessionService', function ($http, $q, sessionService) {
    return function (email, password, confirmPassword) {
        var result = $q.defer();
        $http({
            method: 'POST',
            url: sessionService.apiUrl + '/register',
            data: { Email: email, Password: password, ConfirmPassword: confirmPassword },
            headers: { 'Content-Type': 'application/json' }
        })
        .success(function (response) {
            result.resolve(response);
        })
        .error(function (response) {
            result.reject(response);
        });

        return result.promise;
    }
}]);