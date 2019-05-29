﻿(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams']
    function productEditController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.product;
        $scope.moreImages = [];
        $scope.EditProduct = EditProduct;
      
        function loadProductByID() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
                notificationService.displaySuccess('OK');
            }, function (error) {
                notificationService.displayError('Not GET');
            });
        }
        function EditProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages)
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess("Cập nhật thành công ");
                $state.go('products');
            }, function (error) {
                notificationService.displayError("Cập nhật không thành công");
            });
        }
        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }
      
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {             
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })

            }
            finder.popup();
        }

        loadParentCategory();
        loadProductByID();
    }
})(angular.module('shop.products'));