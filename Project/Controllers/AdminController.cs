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

        //Page for the Users
        [Authorize(Roles = "Users")]
        public ActionResult Users()
        {
            return View();
        }

        //Page for the AddControl
        [Authorize(Roles = "AdControl")]
        public ActionResult AdControl()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdControl(AddControls record)
        {
            AdminContext db = new AdminContext();
            try
            {
                AddControls temp = db.AddControls.FirstOrDefault();
                if (temp != null)
                {
                    temp.Interstitial_1 = record.Interstitial_1;
                    temp.Interstitial_1_cb = record.Interstitial_1_cb;
                    temp.Bottom_banner = record.Bottom_banner;
                    temp.Bottom_banner_cb = record.Bottom_banner_cb;
                    temp.RewardVideo = record.RewardVideo;
                    temp.RewardVideo_cb = record.RewardVideo_cb;

                    db.SaveChanges();
                }
                else
                {
                    db.AddControls.Add(record);
                    db.SaveChanges();
                }
            }
            catch (Exception) { }
            return RedirectToAction("AdControl", "Admin");
        }

        //Page for the AddSettings
        [Authorize(Roles = "AdSettings")]
        public ActionResult AdSettings()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdSettings(AddSettings record)
        {
            AdminContext db = new AdminContext();
            try
            {
                AddSettings temp = db.AddSettings.FirstOrDefault();
                if (temp != null)
                {
                    temp.Android_Add_Mob_App_Id = record.Android_Add_Mob_App_Id;
                    temp.Android_Interstitial_Unit_Id_1 = record.Android_Interstitial_Unit_Id_1;
                    temp.Android_Bottom_banner_unit_Id = record.Android_Bottom_banner_unit_Id;
                    temp.Android_Reward_unit_Id = record.Android_Reward_unit_Id;

                    temp.Ios_Add_Mob_App_Id = record.Ios_Add_Mob_App_Id;
                    temp.Ios_Interstitial_Unit_Id_1 = record.Ios_Interstitial_Unit_Id_1;
                    temp.Ios_Bottom_banner_unit_Id = record.Ios_Bottom_banner_unit_Id;
                    temp.Ios_Reward_unit_Id = record.Ios_Reward_unit_Id;

                    db.SaveChanges();
                }
                else
                {
                    db.AddSettings.Add(record);
                    db.SaveChanges();
                }
            }
            catch (Exception) { }
            return RedirectToAction("AdSettings", "Admin");
        }

        //Page for Coupon List
        [Authorize(Roles = "CouponList")]
        public ActionResult CouponList()
        {
            AdminContext db = new AdminContext();
            ViewBag.message = TempData["message"];
            return View();
        }

        [Authorize(Roles = "CouponList")]
        public ActionResult SingleCreate()
        {

            AdminContext db = new AdminContext();
            try
            {
                int counter = 1;
                var Count = 1;
                var Seller = Request["Seller"];
                var AddUnit = Request["AddUnit"];

                try
                {
                    Count = Int32.Parse(Request["Count"]);
                }
                catch (Exception) { Count = 1; }
                if (Seller != null)
                {
                    for (int i = 1; i <= Count; i++)
                    {
                        string coupon = "";
                        coupon = Seller + i;
                        CouponLists list = db.CouponLists.FirstOrDefault(x => x.Coupon.Equals(coupon));
                        if (list == null)
                        {
                            CouponLists temp = new CouponLists
                            {
                                Device = "",
                                MacAddress = "",
                                AdUnit = AddUnit,
                            };

                            if (Count == 1)
                                temp.Coupon = coupon;
                            else
                                temp.Coupon = coupon;

                            TimeZoneInfo est;
                            try
                            {
                                var timeZones = TimeZoneInfo.GetSystemTimeZones();
                                est = TimeZoneInfo.FindSystemTimeZoneById("Gulf Standard Time");
                                var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);
                                temp.Updated = time;
                            }
                            catch (TimeZoneNotFoundException)
                            {
                                temp.Updated = DateTime.Now;
                            }

                            db.CouponLists.Add(temp);
                            db.SaveChanges();
                            counter = 0;
                        }
                    }
                    if (counter == 0)
                    {
                        if (Count == 1)
                            TempData["message"] = "Coupon Successfully Saved.";
                        else
                            TempData["message"] = "Coupons Successfully Saved.";
                    }
                    else
                    {
                        TempData["message"] = "Coupon Name Already exists.Please Enter new Coupon Name";
                    }
                }
                else
                {
                    TempData["message"] = "Coupon Name Already exists.Please Enter new Coupon Name";
                }
            }
            catch (Exception) { }
            return RedirectToAction("CouponList", "Admin");
        }

        [Authorize(Roles = "CouponList")]
        public ActionResult UpdateCreate()
        {
            var Id = Int32.Parse(Request["ID"]);
            var Seller = Request["Seller"];
            var AddUnit = Request["AddUnit"];
            AdminContext db = new AdminContext();
            try
            {
                CouponLists list = db.CouponLists.FirstOrDefault(x => x.ID == Id);
                if (list != null)
                {
                    list.Coupon = Seller;
                    list.AdUnit = AddUnit;
                    TimeZoneInfo est;
                    try
                    {
                        var timeZones = TimeZoneInfo.GetSystemTimeZones();
                        est = TimeZoneInfo.FindSystemTimeZoneById("Gulf Standard Time");
                        var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);
                        list.Updated = time;
                    }
                    catch (TimeZoneNotFoundException)
                    {
                        list.Updated = DateTime.Now;
                    }
                    db.SaveChanges(); 
                }
            }
            catch (Exception) { }
            return RedirectToAction("CouponList", "Admin");
        }
    }
}