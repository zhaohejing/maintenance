using MyCompanyName.AbpZeroTemplate.EntityModel;
using MyCompanyName.AbpZeroTemplate.Storage;
using System.Web.Mvc;

namespace MyCompanyName.AbpZeroTemplate.Web.Controllers {
    public class HomeController : AbpZeroTemplateControllerBase {

        private readonly ISqlExecuter _sqlhelper;
        public HomeController(ISqlExecuter sqlhelper) {
            _sqlhelper = sqlhelper;
        }



        public ActionResult Index() {
            var userIp = Request.UserHostAddress;
            // var model = _sqlhelper.GetUserClientInfo(userIp);
            var model = new TempModel() {
                RepairTime = System.DateTime.Now,
                DeviceAlias = "别名",
                DeviceAssetNo = "192.168.1.1",
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
            var model = new TempModel() {
                RepairTime = System.DateTime.Now,
                DeviceAlias = "别名",
                DeviceAssetNo = "192.168.1.1",
                DeviceDescription = "我是描述",
                DeviceSIgnkey = "我是标识",
                DeviceName = "我是设备名",
                DeviceType = "我是设备类型",
                DeviceLocation = "我是位置",
                DeviceHead = "我是所有人名字",
                DeviceManufacture = "我是厂商信息"
            };
            // model = _sqlhelper.GetUserClientInfo(userIp);
            return Json(model);
        }
    }
}