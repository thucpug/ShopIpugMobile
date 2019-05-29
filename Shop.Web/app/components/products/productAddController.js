(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state']
    function productAddController($scope, apiService, notificationService, $state) {
        $scope.product = {
            Status: true
        };
        $scope.AddProduct = AddProduct;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        };

        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages)
            apiService.post('/api/product/add', $scope.product, function (result) {
                notificationService.displaySuccess("Thêm thành công ");
                $state.go('products');
            }, function (error) {
                notificationService.displayError("Thêm không thành công");
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

        $scope.moreImages = [];
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
    }
})(angular.module('shop.products'));