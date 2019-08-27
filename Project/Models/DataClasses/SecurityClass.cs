using Project.Models.Database;
using System; 
using System.Linq; 

namespace Project.Models.DataClasses
{
    public class SecurityClass
    {
        //Admin Login Check Class
        public static Admins GetLoginStatus(LoginViewModel lv)
        {
            AdminContext db = new AdminContext();
            Admins temp = new Admins(); 
            try
            {
                temp = db.Admins.FirstOrDefault(x => x.Email.Equals(lv.Email) && x.Password.Equals(lv.Password));
                if (temp != null) 
                    return temp; 
            }
            catch (Exception) { 
                return temp;
            }
            return temp;
        } 

        public static Golfers GetGolferLoginStatus(LoginViewModel lv)
        {
            AdminContext db = new AdminContext();
            Golfers temp = null;
            try
            {
                temp = db.Golfers.FirstOrDefault(x => x.Email.Equals(lv.Email) && x.Password.Equals(lv.Password));
            }
            catch (Exception ex)
            {
            }
            return temp;
        }
    }
}