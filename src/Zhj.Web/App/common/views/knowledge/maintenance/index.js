(function () {
    appModule.controller('common.views.knowledge.maintenance.index', [
        '$scope', '$uibModal', 'abp.services.app.knowledge',
        function ($scope, $uibModal, knowledgeService) {
            var vm = this;
            vm.filter = '';
            vm.list = [];
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            vm.permissions = {
       
            create: abp.auth.hasPermission('Pages.KnowledgeBase.Maintenance.Create'),
                'delete': abp.auth.hasPermission('Pages.KnowledgeBase.Maintenance.Delete'),
            comment:abp.auth.hasPermission('Pages.KnowledgeBase.Maintenance.CreateComment'),
            deletecomment:abp.auth.hasPermission('Pages.KnowledgeBase.Maintenance.DeleteComment'),
            };
            //start from here...
            vm.getknowledges = function () {

                vm.loading = true;
                knowledgeService.getDevicesFaultInfoList({
                    skipCount: 0,
                    maxResultCount: 99,
                    filter: vm.filter,
                }).success(function (data) {
                    vm.list = data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };
            vm.deleteknow = function (knowid) {
                abp.message.confirm(
           '确定删除所选项目',
            function (isConfirmed) {
                if (isConfirmed) {
                    knowledgeService.deleteKnowAsync({ id: knowid }).success(function () {
                        abp.notify.info('删除成功');
                        vm.getknowledges();
                    }).finally(function () {
                    });
                }
            });
            }
            vm.deepwindows = function (know,type) {
                know.deep = type;
            }
            vm.dowmload = function (file) {
                app.downloadFile(file);
            }
            vm.sovleknow = function (knowid) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/common/comment.cshtml',
                    controller: 'common.views.common.comment as vm',
                    backdrop: 'static',
                    resolve: {
                        knowid: function () {
                            return knowid;
                        }
                    }
                });
                modalInstance.result.then(function (result) {
                    vm.getknowledges();
                });
            }

            vm.deletecomment = function (commentid) {
                abp.message.confirm(
               '确定删除所选回复',
                function (isConfirmed) {
                    if (isConfirmed) {
                        knowledgeService.deleteCommentAsync({ id: commentid }).success(function () {
                            abp.notify.info('删除成功');
                            vm.getknowledges();
                        }).finally(function () {
                        });
                    }
                });
            }
            vm.passcomment = function (commentid) {
                knowledgeService.passCommentAsync({ id: commentid }).success(function () {
                    abp.notify.info('审核成功');
                    vm.getknowledges();
                }).finally(function () {
                });
            }
            vm.addknow = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/knowledge/maintenance/modal.cshtml',
                    controller: 'common.views.knowledge.maintenance.modal as vm',
                    backdrop: 'static'
                });
                modalInstance.result.then(function (result) {
                    vm.getknowledges();
                });
            }
            vm.getknowledges();
        }
    ]);
})();