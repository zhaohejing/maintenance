namespace MyCompanyName.AbpZeroTemplate.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";
        

        public const string Pages_Dashboard = "Pages.Dashboard";


        //故障处理
        public const string Pages_FaultRelease = "Pages.FaultRelease";
        public const string Pages_FaultRelease_EventRelease = "Pages.FaultRelease.EventRelease";
        public const string Pages_FaultRelease_EventRelease_Release = "Pages.FaultRelease.EventRelease.Release";
        public const string Pages_FaultRelease_EventRelease_Delete = "Pages.FaultRelease.EventRelease.Delete";

        public const string Pages_FaultDeal = "Pages.FaultDeal";
        public const string Pages_FaultDeal_EventDeal = "Pages.FaultDeal.EventDeal";
        public const string Pages_FaultDeal_EventDeal_Deal = "Pages.FaultDeal.EventDeal.Deal";
        public const string Pages_FaultDeal_EventDeal_Delete = "Pages.FaultDeal.EventDeal.Delete";

        //知识库
        public const string Pages_KnowledgeBase = "Pages.KnowledgeBase";
        public const string Pages_KnowledgeBase_Maintenance = "Pages.KnowledgeBase.Maintenance";
        public const string Pages_KnowledgeBase_Maintenance_Create = "Pages.KnowledgeBase.Maintenance.Create";
        public const string Pages_KnowledgeBase_Maintenance_CreateComment = "Pages.KnowledgeBase.Maintenance.CreateComment";
        public const string Pages_KnowledgeBase_Maintenance_DeleteComment = "Pages.KnowledgeBase.Maintenance.DeleteComment";
        public const string Pages_KnowledgeBase_Maintenance_Delete = "Pages.KnowledgeBase.Maintenance.Delete";
        //统计分析
        public const string Pages_Analysis = "Pages.Analysis";
        public const string Pages_Analysis_Statistical = "Pages.Analysis.Statistical";
        public const string Pages_Analysis_Statistical_Export = "Pages.Analysis.Statistical_Export";

        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";
        
        //HOST-SPECIFIC PERMISSIONS
        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";

        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
    }
}