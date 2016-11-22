"use strict";
app.factory('loginFactory', ['$http', '$q', 'sessionService', function ($http, $q, sessionService) {
    return function (username, password) {
        var result = $q.defer();

        var params = { grant_type: "password", userName: username, password: password };

        $http({
            method: 'POST',
            url: sessionService.apiUrl + '/token',
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: params,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
        })
        .success(function (successfulResponse) {
            console.log(new Date().toString() +
                            " **DEBUG** " +
                            "From loginFactory.js, reported following successful successfulResponse: " +
                            successfulResponse);
            result.resolve(successfulResponse);
        })
        .error(function (response) {
            result.reject(response);
        });

        return result.promise;
    }
}]);