"use strict";
app.controller('welcomeController', ['$scope', '$state', 'sessionService', function ($scope, $state, sessionService) {

    var initializeController = function () {
        //are we logged in or not?
        if (sessionService.GetToken() !== undefined) {
            //yes, go to calendar page
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

    // run while your controller loads
    initializeController();

}]);