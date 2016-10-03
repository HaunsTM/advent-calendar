
/*http://www.codeproject.com/Articles/806029/Getting-started-with-AngularJS-and-ASP-NET-MVC-Par */
adventCalendarApp.factory('getValuesFactory', ['$http', '$q', 'sessionService', function ($http, $q, sessionService) {
    return function () {
        var result = $q.defer();

        $http({
            method: 'GET',
            url: sessionService.apiUrl + '/api/Values',
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + sessionService.getToken() }
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