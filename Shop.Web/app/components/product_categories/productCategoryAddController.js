(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['$scope', 'apiService','notificationService','$state']
    function productCategoryAddController($scope, apiService, notificationService, $state) {
        $scope.productCategory = {
            Status: true
        };
        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {
            apiService.post('/api/productcategory/add', $scope.productCategory, function (result) {
                notificationService.displaySuccess("Thêm thành công ");
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError("Thêm không thành công");
            });
        }
    }
})(angular.module('shop.product_categories'));