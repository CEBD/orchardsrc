using System.Web.Mvc;

namespace Contrib.KeepAlive.Controllers {

    public class PingController : Controller {
        public ActionResult Index() {
            return Content("alive");
        }
    }
}