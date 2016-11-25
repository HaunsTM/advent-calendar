"use strict";
app.controller('slotContentModalController', ['$scope', '$uibModalInstance', function ($scope, $uibModalInstance) {
        
    $scope.modalOptions.ok = function () {
        $uibModalInstance.close();
    }

}]);