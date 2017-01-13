(function () {
    appModule.controller('common.views.common.multipleupload', [
        '$scope', 'appSession', '$uibModalInstance', 'FileUploader', 'images',
        function ($scope, appSession, $uibModalInstance, fileUploader, images) {
            //var imageurl = "http://image.huaeryun.com/"
            //var vm = this;
            //var url = /*abp.appPath*/  imageurl + 'Profile/UploadProfilePicture';

            var vm = this;
            var url =abp.appPath + 'Profile/MultipleUpload';
            vm.uploader = new fileUploader({
                url: url,
                queueLimit: 9,
                filters: [{
                    name: 'imageFilter',
                    fn: function (item, options) {
                        //File type check
                        var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                        if ('|jpg|jpeg|'.indexOf(type) === -1) {
                            abp.message.warn("上传仅支持jpg和jpeg格式");
                            return false;
                        }
                        //File size check
                        if (item.size > 307200) //30KB
                        {
                            abp.message.warn('请上传300k以下大小的图像');
                            return false;
                        }
                        return true;
                    }
                }]
            });
            vm.imageslist = (images == null || images.length <= 0 ? [] : images);
            vm.save = function () {
              
                    vm.uploader.uploadAll();

            };
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            vm.uploader.onCompleteAll = function () {
           
                $uibModalInstance.close(vm.imageslist);

            };
            vm.deleteimages = function (index) {
                vm.imageslist.splice(index, 1);
            }

            vm.uploader.onSuccessItem = function (fileItem, response, status, headers) {
                if (response.result.state == 4) {
                    var guid = response.result.guid;
                    vm.imageslist.push(guid);
                } else {
                    abp.message.error(response.error.message);
                }
            };
        }
    ]);
})();