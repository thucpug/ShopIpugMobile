(function (app) {
    app.controller('productList6Controller', productList6Controller);
    productListController.$inject = ['$scope', 'apiService', '$filter', 'notificationService', '$ngBootbox', '$filter']
    function productList6Controller($scope, apiService, $filter, notificationService, $ngBootbox, $filter) {
        $scope.Products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.getProducts = getProducts;
        $scope.Search = Search;
        $scope.deleteProduct = deleteProduct;
        $scope.deleteMultiple = deleteMultiple;
        $scope.selectAll = selectAll;

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.Products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.Products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        };
        $scope.$watch("Products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            }
            apiService.del('/api/product/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getProducts();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        };


        function deleteProduct(id) {
            $ngBootbox.confirm('Ban có chắc xóa ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/api/product/delete', config, function (result) {
                    notificationService.displaySuccess('Xoa thanh cong');
                    getProducts();
                }, function () {
                    notificationService.displayError('Xoa khong thanh cong');
                });
            }), function (error) {
                notificationService.displayError('Xoa khong thanh cong');
            }
        }

        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keys: $scope.keyword,
                    page: page,
                    pageSize: 5
                }
            };
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                else {
                    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                }
                $scope.Products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load product failed.');
            });
        };
        function Search() {
            $scope.getProducts();
        }

        $scope.getProducts();
    }
})(angular.module('shop.products'));