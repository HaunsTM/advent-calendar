
/*http://www.codeproject.com/Articles/806029/Getting-started-with-AngularJS-and-ASP-NET-MVC-Par */
adventCalendarApp.factory('RegistrationFactory', ['$http', '$q', function ($http, $q) {
    return function (emailAddress, password, confirmPassword) {

        var deferredObject = $q.defer();

        $http.post(
            '/Account/Register', {
                Email: emailAddress,
                Password: password,
                ConfirmPassword: confirmPassword
            }
        )
        .success(function (data) {
            if (data == "True") {
                deferredObject.resolve({ success: true });
            } else {
                deferredObject.resolve({ success: false });
            }
        })
        .error(function () {
            deferredObject.resolve({ success: false });
        });

        return deferredObject.promise;
    }
}]);