using MyCompanyName.AbpZeroTemplate.EntityModel;
using MyCompanyName.AbpZeroTemplate.Storage;
using System.Web.Mvc;
using MyCompanyName.AbpZeroTemplate.EntityFramework;

namespace MyCompanyName.AbpZeroTemplate.Web.Controllers {
    public class HomeController : AbpZeroTemplateControllerBase {

        private readonly ISqlExecuter _sqlhelper;
        public HomeController(ISqlExecuter sqlhelper) {
            _sqlhelper = sqlhelper;
        }



        public ActionResult Index() {
            var userIp = Request.UserHostAddress;
             userIp = "192.168.34.61";
            
            var model = _sqlhelper.GetUserClientInfo(userIp);
            // var model = _sqlhelper.GetUserClientInfo(userIp);
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