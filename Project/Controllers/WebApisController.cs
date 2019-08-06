using Project.Models.Database;
using System.Collections.Generic;
using System.Web.Http;
using Project.Models;
using System.Linq;
using System;
using System.Xml;
using System.Web.Script.Serialization;
using Project.Models.DataClasses;

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
        /**
         * Holes
         */

        public int AddHoleRecord(Holes hole)
        {
            if (!ModelState.IsValid)
            {
                return -1;
            }
            return HolesClass.AddHoleValue(hole);
        }

        public List<Holes> GetMatchDetails(int MatchId)
        {
            return HolesClass.GetMatchDetails(MatchId);
        }

        public Matches GetCommpleteMatch(int MatchId)
        {
            Matches match = HolesClass.GetCompleteMatch(MatchId);
            return match;
        }
    }
}
