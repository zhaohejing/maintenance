(function () {
    appModule.controller('common.views.event.release.index', [
        '$scope', '$uibModal','appSession', '$stateParams',  'abp.services.app.event', '$state',
        function ($scope, $uibModal,appSession, $stateParams, eventService, $state) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            vm.userId = appSession.user.id;
            vm.loading = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;

            vm.permissions = {
                deal: abp.auth.hasPermission('Pages.FaultDeal.EventDeal.Deal'),
                'delete': abp.auth.hasPermission('Pages.Administration.Users.Delete')
            };

     

            var requestParams = {
                skipCount: 0,
                maxResultCount: 10
            };
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
            vm.addrelease = function () {
                $state.go('release');
            }
            //展示编辑框
            vm.showeditinfo = function (item) {

                vm.fault = angular.copy(item);
                vm.jTableInfo.isShow = true;

            }
            //点击收回事件
            $(document).mouseup(function (e) {
                e.stopPropagation();

                var _con = $('#modalInfo');   // 设置目标区域
                if (!_con.is(e.target) && _con.has(e.target).length === 0) {
                    //不再区域内  关闭
                    vm.jTableInfo.isShow = false;
                } else {
                    vm.jTableInfo.isShow = true;
                }

            });
            //关闭
            vm.cancel = function () {
                vm.fault = {};
                vm.jTableInfo.isShow = false;
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
        }
    ]);
})();