(function () {
    appModule.controller('common.views.analysis.statistical.index', [
        '$scope', '$uibModal','abp.services.app.event',
        function ($scope, $uibModal,eventService) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            //页面属性
            vm.jTableInfo = {
                data: [],               //数据集
                checkModel: {},         //选择的集合
                selectlength: 0,        //选中池
                isShow: false,
                filter: "",
                pageConfig: {           //分页配置
                    currentPage: 1,
                    itemsPerPage: 10,
                    totalItems: 0
                },
                backModel: {
                    back: function (count) {
                        vm.jTableInfo.selectlength = count;
                    }
                }
            }
            vm.devicerepair = function () {
                var page = vm.jTableInfo.pageConfig.currentPage;
                var display = vm.jTableInfo.pageConfig.itemsPerPage;
                vm.loading = true;
                eventService.getDevicesRepairList({
                    skipCount: (page - 1) * display,
                    maxResultCount: display,
                    filter: vm.jTableInfo.filter,
                }).success(function (data) {
                    vm.jTableInfo.pageConfig.totalItems = data.totalCount;
                    angular.forEach(data.items, function (item) {
                        item.checked = vm.jTableInfo.checkModel[item.id] ? true : false;
                    });
                    vm.jTableInfo.data = data.items;
                    vm.jTableInfo.pageConfig.onChange = function () {
                        vm.devicerepair();
                    }
                }).finally(function () {
                    vm.loading = false;
                });
            };
            vm.devicerepair();


            //页面属性
            vm.tTableInfo = {
                data: [],               //数据集
                checkModel: {},         //选择的集合
                selectlength: 0,        //选中池
                isShow: false,
                filter: "",
                pageConfig: {           //分页配置
                    currentPage: 1,
                    itemsPerPage: 10,
                    totalItems: 0
                },
                backModel: {
                    back: function (count) {
                        vm.tTableInfo.selectlength = count;
                    }
                }
            }
            vm.enginerrepair = function () {
                var page = vm.tTableInfo.pageConfig.currentPage;
                var display = vm.tTableInfo.pageConfig.itemsPerPage;
                vm.loading = true;
                eventService.getEnginerRepairList({
                    skipCount: (page - 1) * display,
                    maxResultCount: display,
                    filter: vm.tTableInfo.filter,
                }).success(function (data) {
                    vm.tTableInfo.pageConfig.totalItems = data.totalCount;
                    angular.forEach(data.items, function (item) {
                        item.checked = vm.tTableInfo.checkModel[item.id] ? true : false;
                    });
                    vm.tTableInfo.data = data.items;
                    vm.tTableInfo.pageConfig.onChange = function () {
                        vm.enginerrepair();
                    }
                }).finally(function () {
                    vm.loading = false;
                });
            };
        }
    ]);
})();