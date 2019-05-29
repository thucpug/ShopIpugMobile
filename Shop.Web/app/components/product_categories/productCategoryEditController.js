(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);
    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state','$stateParams']
    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.productCategory;
        $scope.updateProductCategory = updateProductCategory;


        function updateProductCategory() {
            apiService.put('/api/productcategory/update', $scope.productCategory, function (result) {
                notificationService.displaySuccess("Update thành công ");
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError("Update không thành công");
            });
        };
        function loadProductCategory() {
            apiService.get('/api/productcategory/getbyid/'+ $stateParams.id,null, function (result) {
                notificationService.displaySuccess("Load thành công ");
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError("Load không thành công");
            });
        };
        loadProductCategory();
    }
})(angular.module('shop.product_categories'));