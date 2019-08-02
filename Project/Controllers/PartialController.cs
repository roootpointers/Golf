using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        [ChildActionOnly]
        public ActionResult TopNaveBar()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult AsileBar()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult UsersPartial()
        {
            return PartialView();
        }
    }
}