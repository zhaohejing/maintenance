using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyCompanyName.AbpZeroTemplate.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)


            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("页面"));
            //控制台
            var dashboard = pages.CreateChildPermission(AppPermissions.Pages_Dashboard, L("工作台"));
            //故障
            var faultevent = pages.CreateChildPermission(AppPermissions.Pages_FaultRelease, L("故障事件管理"));
            var release = faultevent.CreateChildPermission(AppPermissions.Pages_FaultRelease_EventRelease, L("故障事件发布"));
            var dorelease = faultevent.CreateChildPermission(AppPermissions.Pages_FaultRelease_EventRelease_Release, L("故障事件发布"));
            var deleterelease = release.CreateChildPermission(AppPermissions.Pages_FaultRelease_EventRelease_Delete, L("故障事件删除"));

            var deal = pages.CreateChildPermission(AppPermissions.Pages_FaultDeal, L("故障事件处理"));
            var dealfault = deal.CreateChildPermission(AppPermissions.Pages_FaultDeal_EventDeal, L("故障事件处理"));
            var dodeal = deal.CreateChildPermission(AppPermissions.Pages_FaultDeal_EventDeal_Deal, L("故障处理"));
            var deletefault = deal.CreateChildPermission(AppPermissions.Pages_FaultDeal_EventDeal_Delete, L("故障删除"));
            //知识库
            var knowledge = pages.CreateChildPermission(AppPermissions.Pages_KnowledgeBase, L("知识库"));
            var maintenance = knowledge.CreateChildPermission(AppPermissions.Pages_KnowledgeBase_Maintenance, L("维修知识"));
            var maincreate = knowledge.CreateChildPermission(AppPermissions.Pages_KnowledgeBase_Maintenance_Create, L("添加知识"));
            var commentcreate = knowledge.CreateChildPermission(AppPermissions.Pages_KnowledgeBase_Maintenance_CreateComment, L("添加评论"));
            var commentdelete = knowledge.CreateChildPermission(AppPermissions.Pages_KnowledgeBase_Maintenance_DeleteComment, L("删除评论"));
            var maindelete = knowledge.CreateChildPermission(AppPermissions.Pages_KnowledgeBase_Maintenance_Delete, L("删除知识"));

            //统计分析
            var analysis = pages.CreateChildPermission(AppPermissions.Pages_Analysis, L("统计分析"));
            var statistical = analysis.CreateChildPermission(AppPermissions.Pages_Analysis_Statistical, L("按需报表"));
            var exportstatistical = statistical.CreateChildPermission(AppPermissions.Pages_Analysis_Statistical_Export, L("导出报表"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("系统设置"));
            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("角色管理"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("创建角色"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("编辑角色"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("删除角色"));
            
            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("用户管理"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("创建用户"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("编辑用户"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("删除用户"));
     }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ZhjConsts.LocalizationSourceName);
        }
    }
}
