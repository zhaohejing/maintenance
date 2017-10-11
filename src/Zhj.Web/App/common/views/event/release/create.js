(function () {
    appModule.controller('common.views.event.release.create', [
        '$scope', '$uibModal','appSession', '$stateParams',  'abp.services.app.event', '$state',
        function ($scope, $uibModal,appSession, $stateParams, eventService, $state) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            vm.main = {
                profileList:[]
            };
            vm.imagecount = 0;
            vm.cancel = function () {
                $state.go('eventrelease');
            }
            vm.date = {
                leftopen: false,
                rightopen: false,
                //left: new Date(),
                //right:new date(),
                inlineOptions: {
                    // customClass: getDayClass,
                    // minDate: new Date(),
                    showWeeks: false
                },
                dateOptions: {
                    //dateDisabled: disabled,
                    formatYear: 'yyyy',
                    maxDate: new Date(2020, 1, 1),
                    minDate: new Date(1900, 1, 1),
                    startingDay: 1
                },
                openleft: function () {
                    vm.date.leftopen = !vm.date.leftopen;
                    // $scope.popup2.opened = !$scope.popup2.opened;
                },
                openright: function () {
                    vm.date.rightopen = !vm.date.rightopen;
                    // $scope.popup2.opened = !$scope.popup2.opened;
                }
            }

            vm.save = function () {
                eventService.insertFaultInfo(vm.main).success(function () {
                    abp.notify.info('报修成功');
                    $state.go('eventrelease');
                }).finally(function () {
                });
               
            }
            vm.upload = function () {
                var modal = $uibModal.open({
                    templateUrl: '~/App/common/views/common/multipleUpload.cshtml',
                    controller: 'common.views.common.multipleupload as vm',
                    backdrop: 'static'
                });
                modal.result.then(function (guidlist) {
                    if (guidlist != null && guidlist.length > 0) {
                        vm.main.profileList = guidlist;
                        if (vm.main.profileList.length>0) {
                            vm.imagecount = 12 / vm.main.profileList.length;
                        }

                    }
                });
            }
            vm.init=function() {
                
            }
            vm.init();

        }
    ]);
})();