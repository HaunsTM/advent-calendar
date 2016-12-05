"use strict";
app.controller('welcomeController', ['$scope', '$state', 'sessionService', function ($scope, $state, sessionService) {

    var initializeController = function () {
        //har vi loggat in eller inte?
        if (sessionService.GetToken() !== undefined) {
            //ja,, gå till kalendersidan
            $state.go('stateCalendar');
        }
        else
        {
          //do nothing  
        }
    }

    $scope.Register = function() {
        
    }

    $scope.Login = function () {

    }

    // anropa när control laddas
    initializeController();

}]);