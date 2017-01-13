(function () {
    appModule.controller('common.views.knowledge.maintenance.modal', [
        '$scope', '$uibModalInstance', 'abp.services.app.knowledge',
        function ($scope, $uibModalInstance, knowledgeService) {
            var vm = this;

            vm.saving = false;
            vm.know = {};
            vm.save = function () {
                //保存
        
                knowledgeService.insertKnowledge(vm.know).success(function () {
                    abp.notify.info('创建成功');
                    $uibModalInstance.close();
                }).finally(function () {
                });
           
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
        }
    ]);
})();