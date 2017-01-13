using MyCompanyName.AbpZeroTemplate.Authorization;
using MyCompanyName.AbpZeroTemplate.EntityFramework;
using MyCompanyName.AbpZeroTemplate.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.Migrations.Seed {
    public class DefaultNavigationBuilder {
        private readonly ZhjDbContext _context;

        public DefaultNavigationBuilder(ZhjDbContext context) {
            _context = context;
        }

        public void Build() {
            CreateDefaultNavigation();
        }
        /// <summary>
        /// 添加默认导航
        /// </summary>
        private void CreateDefaultNavigation() {
            if (_context.BaseNavigation.Count() > 0) {
                return;
            }
            var list = GetNavigationList();
            foreach (var level in list) {

                var one = _context.BaseNavigation.Add(
                    new BaseNavigation(
                    level.Name, level.DisplayName, level.Url,
                    level.Icon, !string.IsNullOrWhiteSpace(level.RequiredPermissionName), level.RequiredPermissionName,
                    level.Name == "GeneralBoard" || level.Name == "GeneralBoard" ? 1 : 0, null));

                _context.SaveChanges();

                if (level.ChildLevel != null) {
                    foreach (var leveltwo in level.ChildLevel) {
                        _context.BaseNavigation.Add(new BaseNavigation(leveltwo.Name, leveltwo.DisplayName, leveltwo.Url,
                            leveltwo.Icon,
                            !string.IsNullOrWhiteSpace(leveltwo.RequiredPermissionName), leveltwo.RequiredPermissionName,
                            one.Id));
                    }
                    _context.SaveChanges();
                }
            }
        }

        private List<LevelOne> GetNavigationList() {
            var list = new List<LevelOne>();
            //工作台
            var dashboard = new LevelOne("GeneralBoard", "工作台", "dashboard", "fa fa-dashboard", AppPermissions.Pages_Dashboard, null);
            list.Add(dashboard);
          

            //故障事件
            var events = new List<LevelTwo>() {
                new LevelTwo("EventRelease","故障列表","eventrelease","fa fa-commenting",AppPermissions.Pages_FaultRelease_EventRelease),
            };
            var eve=new LevelOne("FaultRelease","故障事件","", "fa fa-building", AppPermissions.Pages_FaultRelease, events);
            list.Add(eve);

            var deal = new List<LevelTwo>() {
                new LevelTwo("EventDeal","报修处理","eventdeal","fa fa-gear",AppPermissions.Pages_FaultDeal_EventDeal),
            };
            var dealevent = new LevelOne("FaultDeal", "报修事件", "", "fa fa-gavel", AppPermissions.Pages_FaultDeal, deal);
            list.Add(dealevent);
            //知识库
            var knowledgebase = new List<LevelTwo>() {
                new LevelTwo("Maintenance","维修知识","maintenance","fa fa-plug",AppPermissions.Pages_KnowledgeBase_Maintenance)
            };
            var knowledges = new LevelOne("Knowledges", "知识库", "knowledge", "fa fa-mortar-board", AppPermissions.Pages_KnowledgeBase, knowledgebase);
            list.Add(knowledges);

            //统计分析
            var statistical = new List<LevelTwo>() {
                new LevelTwo("Statistical","按需报表","statistical","fa fa-qrcode",AppPermissions.Pages_Analysis_Statistical)
            };
            var analysis = new LevelOne("Analysis", "统计分析", "analysis", "fa fa-pie-chart", AppPermissions.Pages_Analysis, statistical);
            list.Add(analysis);
            //系统
            var rolechilds = new List<LevelTwo>() {
                new LevelTwo("User","用户管理","user","fa fa-user",AppPermissions.Pages_Administration_Users),
                new LevelTwo("Role","角色管理","role","fa fa-user-plus",AppPermissions.Pages_Administration_Roles)
            };
            var system = new LevelOne("System", "系统设置", "system", "fa fa-cogs", AppPermissions.Pages_Administration, rolechilds);
            list.Add(system);
            return list;
           
        }
    }


    public class LevelOne {
        public LevelOne(string name, string displayname, string url, string icon, List<LevelTwo> twolist) {
            Name = name;
            ChildLevel = twolist;
            Url = url;
            DisplayName = displayname;
            Icon = icon;
        }
        public LevelOne(string name, string displayname, string url, string icon, string requiredPermissionName, List<LevelTwo> twolist) {
            Name = name;
            ChildLevel = twolist;
            Url = url;
            DisplayName = displayname;
            Icon = icon;
            RequiredPermissionName = requiredPermissionName;
        }
        public string RequiredPermissionName { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public List<LevelTwo> ChildLevel { get; set; }
    }
    public class LevelTwo {
        public LevelTwo(string name, string displayname, string url, string icon, string requiredPermissionName) {
            Name = name;
            DisplayName = displayname;
            Url = url;
            Icon = icon;
            RequiredPermissionName = requiredPermissionName;
        }
        public string RequiredPermissionName { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
    }
}
