(function () {
    appModule.controller('common.views.dashboard.index', [
        '$scope', '$uibModal','abp.services.app.event',
        function ($scope, $uibModal,eventService) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            vm.last = [];
            vm.mast = [];

            //页面属性
            vm.jTableInfo = {
                data: [],               //数据集
                checkModel: {},         //选择的集合
                selectlength: 0,        //选中池
                isShow: false,
                filter: "",
                pageConfig: {           //分页配置
                    currentPage: 1,
                    itemsPerPage: 5,
                    totalItems: 0
                },
                backModel: {
                    back: function (count) {
                        vm.jTableInfo.selectlength = count;
                    }
                }
            }

            var init = function () {
                eventService.getSolveInfo().success(function (data) {
                    vm.last = data.last;
                    vm.mast = data.mast;
                }).finally(function () {
                    vm.loading = false;
                });
            
            }



            vm.getFaults = function () {
                var page = vm.jTableInfo.pageConfig.currentPage;
                var display = vm.jTableInfo.pageConfig.itemsPerPage;
                vm.loading = true;
                eventService.getDevicesFaultInfoList({
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
                        vm.getFaults();
                    }
                }).finally(function () {
                    vm.loading = false;
                });
            };
            vm.getFaults();

          
            //start from here...
            init();
        }
    ]);
})();