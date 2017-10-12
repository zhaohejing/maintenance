using System.Web.Mvc;
using MyCompanyName.AbpZeroTemplate.EntityModel;
using MyCompanyName.AbpZeroTemplate.Storage;
using MyCompanyName.AbpZeroTemplate.Web.Controllers;

namespace Maintenance.Web.Controllers {
    public class HomeController : AbpZeroTemplateControllerBase {

        private readonly ISqlExecuter _sqlhelper;
        public HomeController(ISqlExecuter sqlhelper) {
            _sqlhelper = sqlhelper;
        }



        public ActionResult Index() {
           // var userIp = Request.UserHostAddress;
           ////  userIp = "192.168.34.61";
            
           // var model = _sqlhelper.GetUserClientInfo(userIp);
           // // var model = _sqlhelper.GetUserClientInfo(userIp);
           // ViewData["User"] = model;
           // return View();



            var userIp = Request.UserHostAddress;
            // var model = _sqlhelper.GetUserClientInfo(userIp);
            var model = new TempModel()
            {
                RepairTime = System.DateTime.Now,
                DeviceAlias =1,
                DeviceAssetNo = userIp,
                DeviceDescription = "我是描述",
                DeviceSIgnkey = "我是标识",
                DeviceName = "我是设备名",
                DeviceType = "我是设备类型",
                DeviceLocation = "我是位置",
                DeviceHead = "我是所有人名字",
                DeviceManufacture = "我是厂商信息"
            };
            ViewData["User"] = model;
            return View();
        }
        [HttpPost]
        public JsonResult GetUserMacInfo() {
            var userIp = Request.UserHostAddress;
            var model = _sqlhelper.GetUserClientInfo(userIp);
            return Json(model);
        }
    }
}