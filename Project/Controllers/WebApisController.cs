using Project.Models.Database;
using System.Collections.Generic;
using System.Web.Http;
using Project.Models;
using System.Linq;
using System;
using System.Xml;
using System.Web.Script.Serialization;

namespace Project.Controllers
{
    public class WebApisController : ApiController
    {
        // GET: api/WebApis 

        //Admin -> Apis
        public DataTableResponse GetAdminTable(DataTableRequest request)
        {
            return Models.DataClasses.AdminsClass.AdminTable(request);
        }
        public int PostAdmin(Admins temp)
        {
            return Models.DataClasses.AdminsClass.AddRecord(temp);
        }
        public int UpdateAdmin(Admins temp)
        {
            return Models.DataClasses.AdminsClass.UpdateRecord(temp);
        }
        public void PostImagesAdmin()
        {
            Models.DataClasses.AdminsClass.PostYourDetailsImage();
        }
        public Admins GetSingleAdminById(int Id)
        {
            return Models.DataClasses.AdminsClass.GetRecord(Id);
        }
        public int DeleteAdmin(int Id)
        {
            return Models.DataClasses.AdminsClass.DeleteRecord(Id);
        }
        public Admins GetCurrentUserInfo()
        {
            return Models.DataClasses.AdminsClass.GetCurrentUserInfo();
        }
        ////////////////////////////////////
        ////////////////////////////////////   

        ////////////////////////////////////
        //////////////////////////////////// 
        /// <summary>
        ///     //Coupon -> Apis
        public DataTableResponse GetCouponTable(DataTableRequest request, string data)
        {
            return Models.DataClasses.CouponListClass.Table(request,data);
        }
        [AllowAnonymous]
        public int PostCoupon(CouponLists temp)
        {
            return Models.DataClasses.CouponListClass.AddRecord(temp);
        }
        public string UpdateCoupon(CouponLists temp)
        {
            return Models.DataClasses.CouponListClass.UpdateRecord(temp);
        }
        public CouponLists GetSingleCouponById(int Id)
        {
            return Models.DataClasses.CouponListClass.GetRecord(Id);
        } 
        public int DeleteCoupon(int Id)
        {
            return Models.DataClasses.CouponListClass.DeleteRecord(Id);
        }

        ///////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        //AddControl -> Apis
        [AllowAnonymous]
        public ApiClass GetAddControl()
        {
            ApiClass temp = new ApiClass();
            AdminContext db = new AdminContext();
            try
            {
                temp.Status = true;
                temp.Message = "Ad Controls";
                temp.Data = db.AddControls.FirstOrDefault();
            }
            catch (Exception) { }
            return temp;
        }

        //AddSettings -> Apis
        [AllowAnonymous]
        public ApiClass GetAddSettings()
        {
            ApiClass temp = new ApiClass();
            AdminContext db = new AdminContext();
            try
            {
                temp.Status = true;
                temp.Message = "Ad Settings";
                temp.Data = db.AddSettings.FirstOrDefault();
            }
            catch (Exception) { }
            return temp;
        }
    }
}
