using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace RocketLauncher.Host.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index() 
        {
            return View("Login");
        }

        [System.Web.Mvc.HttpPost] 
        public ActionResult Index([FromBody]string loginName)
        {
            if (loginName.Equals("magicpassword1", StringComparison.InvariantCultureIgnoreCase))
            {
                FormsAuthentication.SetAuthCookie("magicuser", false);
                return new RedirectResult("~/");
            }

            throw new SecurityAccessDeniedException("Incorrect login name.");
        }
    }
}