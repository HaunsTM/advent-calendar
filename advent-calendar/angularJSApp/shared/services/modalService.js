/**/"use strict";
app.service('modalService', ['$uibModal', function ($uibModal) {

    this.showModal = function (customModalDefaults, customModalOptions) {
        if (!customModalDefaults) customModalDefaults = {};
        customModalDefaults.backdrop = 'static';
        return this.show(customModalDefaults, customModalOptions);
    };

    this.show = function (modalDefaults, modalOptions) {

        if (!modalDefaults.controller) {
            modalDefaults.controller = function ($scope, $uibModalInstance) {
                $scope.modalOptions = modalOptions;
                $scope.modalOptions.ok = function (result) {
                    $uibModalInstance.close(result);
                };
                $scope.modalOptions.close = function (result) {
                    $uibModalInstance.dismiss('cancel');
                };
            }
        }

        return $uibModal.open(modalDefaults).result;
    };

}]);