using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuth2Demo.Controllers
{
    [Authorize]
    public class ProtectedResourceController : Controller
    {
        // GET: ProtectedResource
        public ActionResult Index()
        {
            return Json(new { Content = "Hello World!"});
        }
    }
}