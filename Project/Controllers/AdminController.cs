using Project.Models.Database;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Project.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin  

        //Page for the Admins
        [Authorize(Roles = "Admins")]
        public ActionResult Admins()
        {
            return View();
        }   
        //Page for Coupon List
        [Authorize(Roles = "Scoreboard")]
        public ActionResult Scoreboard()
        {
            AdminContext db = new AdminContext(); 
            return View();
        }

        public ActionResult CreateBoard()
        {
            return View();
        }

        public ActionResult CreateMatch()
        {
            return View();
        }
    }
}