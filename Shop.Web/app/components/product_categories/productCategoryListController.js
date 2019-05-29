(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', '$filter', 'notificationService', '$ngBootbox','$filter']
    function productCategoryListController($scope, apiService, $filter, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.getProductCagories = getProductCagories;
        $scope.Search = Search;

        $scope.deleteProductCategory = deleteProductCategory;
        $scope.deleteMultiple = deleteMultiple;
        $scope.selectAll = selectAll;

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        };
        $scope.$watch("productCategories", function (n, o) {
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
                    checkedProductCategories: JSON.stringify(listId)
                }
            }
            apiService.del('/api/productcategory/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getProductCagories();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        };


        function deleteProductCategory(id) {
            $ngBootbox.confirm('Ban có chắc xóa ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/api/productcategory/delete', config, function (result) {
                    notificationService.displaySuccess('Xoa thanh cong');
                    getProductCagories();
                }, function () {
                    notificationService.displayError('Xoa khong thanh cong');
                });
            }), function (error) {
                notificationService.displayError('Xoa khong thanh cong');
            }
        }

        function getProductCagories(page) {
            page = page || 0;
            var config = {
                params: {
                    keys: $scope.keyword,
                    page: page,
                    pageSize: 3
                }
            };
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                else {
                    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                }
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        };
        function Search() {
            $scope.getProductCagories();
        }

        $scope.getProductCagories();
    }
})(angular.module('shop.product_categories'));