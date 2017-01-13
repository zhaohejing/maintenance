/* 'app' MODULE DEFINITION */
var appModule = angular.module("app", [
    "ui.router",
    "ui.bootstrap",
    'ui.utils',
    "ui.jq",
    'ui.grid',
    'ui.grid.pagination',
    "oc.lazyLoad",
    "ngSanitize",
    'angularFileUpload',
    'daterangepicker',
    'angularMoment',
      'objectTable',
    'objPagination',
    'frapontillo.bootstrap-switch',
    'abp'
]);

/* LAZY LOAD CONFIG */

/* This application does not define any lazy-load yet but you can use $ocLazyLoad to define and lazy-load js/css files.
 * This code configures $ocLazyLoad plug-in for this application.
 * See it's documents for more information: https://github.com/ocombe/ocLazyLoad
 */
appModule.config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        cssFilesInsertBefore: 'ng_load_plugins_before', // load the css files before a LINK element with this ID.
        debug: false,
        events: true,
        modules: []
    });
}]);

/* THEME SETTINGS */
App.setAssetsPath(abp.appPath + 'metronic/assets/');
appModule.factory('settings', ['$rootScope', function ($rootScope) {
    var settings = {
        layout: {
            pageSidebarClosed: false, // sidebar menu state
            pageContentWhite: true, // set page content layout
            pageBodySolid: false, // solid body color state
            pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
        },
        layoutImgPath: App.getAssetsPath() + 'admin/layout4/img/',
        layoutCssPath: App.getAssetsPath() + 'admin/layout4/css/',
        assetsPath: abp.appPath + 'metronic/assets',
        globalPath: abp.appPath + 'metronic/assets/global',
        layoutPath: abp.appPath + 'metronic/assets/layouts/layout4'
    };

    $rootScope.settings = settings;

    return settings;
}]);

/* ROUTE DEFINITIONS */

appModule.config([
    '$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        //Welcome page
        //$stateProvider.state('welcome', {
        //    url: '/welcome',
        //    templateUrl: '~/App/common/views/welcome/index.cshtml'
        //});
        // Default route (overrided below if user has permission)
      
      //  $urlRouterProvider.otherwise("/eventrelease");



        if (abp.auth.hasPermission('Pages.FaultRelease.EventRelease.Release')) {
            $urlRouterProvider.otherwise("/release");

            //添加隐藏页面的方法 跳转 传参等等
            $stateProvider.state('release', {
                url: '/release', data: { pageTitle: "添加课件包" },
                templateUrl: '~/App/common/views/event/release/create.cshtml',
                menu: ''
            })
         
        }
        if (abp.auth.hasPermission('Pages.Dashboard')) {
            $urlRouterProvider.otherwise("/dashboard");
            $stateProvider.state('dashboard', {
                url: '/dashboard',
                templateUrl: '~/App/common/views/dashboard/index.cshtml',
                menu: 'Dashboard'
            });
            $urlRouterProvider.otherwise("/dashboard");
        }
        //COMMON routes

        if (abp.auth.hasPermission('Pages.Administration.Roles')) {
            $stateProvider.state('role', {
                url: '/role',
                templateUrl: '~/App/common/views/roles/index.cshtml',
                menu: 'Administration.Roles'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.Users')) {
            $stateProvider.state('user', {
                url: '/user?filterText',
                templateUrl: '~/App/common/views/users/index.cshtml',
                menu: 'Administration.Users'
            });
        }

        if (abp.auth.hasPermission('Pages.FaultRelease.EventRelease')) {
            $stateProvider.state('eventrelease', {
                url: '/eventrelease',
                templateUrl: '~/App/common/views/event/release/index.cshtml',
                menu: 'EventRelease'
            });
        }
        if (abp.auth.hasPermission('Pages.FaultDeal.EventDeal')) {
            $stateProvider.state('eventdeal', {
                url: '/eventdeal',
                templateUrl: '~/App/common/views/event/deal/index.cshtml',
                menu: 'EventDeal'
            });
        }

        if (abp.auth.hasPermission('Pages.KnowledgeBase.Maintenance')) {
            $stateProvider.state('maintenance', {
                url: '/maintenance',
                templateUrl: '~/App/common/views/knowledge/maintenance/index.cshtml',
                menu: 'Maintenance'
            });
        }

        if (abp.auth.hasPermission('Pages.Analysis.Statistical')) {
            $stateProvider.state('statistical', {
                url: '/statistical',
                templateUrl: '~/App/common/views/analysis/statistical/index.cshtml',
                menu: 'Maintenance'
            });
        }

      

       
    }
]);

appModule.run(["$rootScope", "settings", "$state", 'i18nService', function ($rootScope, settings, $state, i18nService) {
    $rootScope.$state = $state;
    $rootScope.$settings = settings;

    //Set Ui-Grid language
    if (i18nService.get(abp.localization.currentCulture.name)) {
        i18nService.setCurrentLang(abp.localization.currentCulture.name);
    } else {
        i18nService.setCurrentLang("en");
    }

    $rootScope.safeApply = function (fn) {
        var phase = this.$root.$$phase;
        if (phase == '$apply' || phase == '$digest') {
            if (fn && (typeof (fn) === 'function')) {
                fn();
            }
        } else {
            this.$apply(fn);
        }
    };
}]);