(function () {
    appModule.controller('common.views.common.comment', [
        '$scope', 'appSession', '$uibModalInstance', 'FileUploader','knowid','abp.services.app.knowledge',
    function ($scope, appSession, $uibModalInstance, fileUploader, knowid, knowledgeService) {
            var vm = this;
            vm.comment = '';
            var url = abp.appPath + 'Profile/FileUpload';
            vm.uploader = new fileUploader({
                url: url,
                queueLimit: 1,
                filters: [{
                    name: 'fileFilter',
                    fn: function (item, options) {
                        //File type check
                        //var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                        //if ('|jpg|jpeg|'.indexOf(type) === -1) {
                        //    abp.message.warn("上传仅支持jpg和jpeg格式");
                        //    return false;
                        //}
                        //File size check
                        if (item.size > 3072000) //30KB
                        {
                            abp.message.warn('请上传3M以下大小的文件');
                            return false;
                        }
                        return true;
                    }
                }]
            });

            vm.save = function () {
                if ((!vm.uploader.queue || vm.uploader.queue.length <= 0)) {
                    var model = { knowid: knowid, comment: vm.comment };
                    knowledgeService.insertComment(model).success(function () {
                        abp.notify.info('评论成功');
                        $uibModalInstance.close();
                    }).finally(function () {

                    });
                } else {
                    vm.uploader.uploadAll();
                }
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.uploader.onSuccessItem = function (fileItem, response, status, headers) {
                if (response.success) {
                    var guid = response.result.guid;
                    var model = { knowid: knowid, comment: vm.comment, profileId: guid };
                    knowledgeService.insertComment(model).success(function () {
                        abp.notify.info('评论成功');
                        $uibModalInstance.close();
                    }).finally(function () {

                    });
                } else {
                    abp.notify.info('回复失败,请重试');

                }
            };
        }
    ]);
})();