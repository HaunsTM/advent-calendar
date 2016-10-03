adventCalendarApp.factory('logoutFactory', ['$http', '$q', 'sessionService', function ($http, $q, sessionService) {

    return function () {
        var result = $q.defer();
        $http({
            method: 'POST',
            url: sessionService.apiUrl + '/Account/Logout'
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