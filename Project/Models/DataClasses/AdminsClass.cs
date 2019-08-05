using System;
using System.Collections.Generic;
using System.Web;
using Project.Models.Database;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web.Helpers;

namespace Project.Models.DataClasses
{
    public class AdminsClass
    {
        //Get Current User Info
        public static Admins GetCurrentUserInfo()
        {
            AdminContext db = new AdminContext();
            Admins li = new Admins();
            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;

                if (!expired)
                {
                    int Id = Int32.Parse(CookieId);
                    li = db.Admins.FirstOrDefault(x => x.ID == Id); 
                }
            }
            catch (Exception) { }
            return li;
        }

        //Get Admin Record By Id
        public static Admins GetRecord(int id)
        {
            AdminContext db = new AdminContext();
            Admins temp = new Admins();
            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;
                if (!expired)
                {
                    temp = db.Admins.FirstOrDefault(x => x.ID == id);
                }
            }
            catch (Exception) { }

            return temp;
        }

        //Add New Record
        public static int AddRecord(Admins admin)
        {
            AdminContext db = new AdminContext();
            int Status = 0;
            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;
                if (!expired)
                {
                    var exist = db.Admins.FirstOrDefault(x => x.Email.Equals(admin.Email));
                    if (exist == null)
                    {
                        TimeZoneInfo est;
                        try
                        {
                            var timeZones = TimeZoneInfo.GetSystemTimeZones();
                            est = TimeZoneInfo.FindSystemTimeZoneById("Gulf Standard Time");
                            var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);
                            admin.AddDate = time;
                        }
                        catch (TimeZoneNotFoundException)
                        {
                            admin.AddDate = DateTime.Now;
                        }

                        db.Admins.Add(admin);
                        db.SaveChanges();
                        Status = admin.ID;
                    }
                }
            }
            catch (Exception) { }
            return Status;
        }

        //Update Record
        public static int UpdateRecord(Admins UA)
        {
            AdminContext db = new AdminContext();
            int Status = 0;
            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;
                if (!expired)
                {
                    var exist = db.Admins.FirstOrDefault(x => x.Email.Equals(UA.Email) && x.ID != UA.ID);
                    if (exist == null)
                    {
                        Admins user = db.Admins.FirstOrDefault(x => x.ID == UA.ID);
                        if (user != null)
                        {
                            if (user.Occupation == "MasterAdmin")
                            {
                                user.FullName = UA.FullName;
                                user.Email = UA.Email;
                                if (!string.IsNullOrWhiteSpace(UA.Password))
                                {
                                    user.Password = UA.Password;
                                }
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Status = user.ID;
                            }
                            else
                            {
                                user.FullName = UA.FullName;
                                user.Email = UA.Email; 
                                if (!string.IsNullOrWhiteSpace(UA.Password))
                                {
                                    user.Password = UA.Password;
                                }
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Status = user.ID;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            return Status;
        }

        //Add Photo
        public static void PostYourDetailsImage()
        {
            AdminContext db = new AdminContext();
            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;

                if (!expired)
                {
                    var typeOfImage = "";
                    if (HttpContext.Current.Request.Files.AllKeys.Any())
                    {
                        // Get the uploaded image from the Files collection
                        int Id = Int32.Parse(HttpContext.Current.Request["ImageId"].ToString());
                        var httpPostedFile = HttpContext.Current.Request.Files["Picture"];
                        string httpPostedFile1 = httpPostedFile.FileName;
                        string[] httpPostedFile2 = httpPostedFile1.Split('.');
                        typeOfImage = "." + httpPostedFile2[httpPostedFile2.Length - 1];
                        string Date = DateTime.Now.ToString();
                        Date = Date.Replace(" ", String.Empty);
                        Date = Date.Replace("/", String.Empty);
                        Date = Date.Replace(":", String.Empty);
                        if (httpPostedFile != null)
                        {
                            WebImage img = new WebImage(httpPostedFile.InputStream);
                            if (img.Width > 160 || img.Height > 160)
                                img.Resize(160, 160);
                            img.Save(@"~\Content\Images\Admin\" + (Id.ToString() + Date + typeOfImage));
                        }
                        try
                        {
                            string image = "../../Content/Images/Admin/" + Id.ToString() + Date + typeOfImage.ToString();
                            Admins user = db.Admins.FirstOrDefault(x => x.ID == Id);
                            if (user != null)
                            {
                                user.Image = image;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }
            catch (Exception) { }
        }

        //Delete Record
        public static int DeleteRecord(int Id)
        {
            AdminContext db = new AdminContext();
            int Status = 0;
            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;
                if (!expired)
                {
                    Admins temp = db.Admins.FirstOrDefault(x => x.ID == Id);
                    db.Admins.Remove(temp);
                    db.SaveChanges();
                    Status = 1;
                }
            }
            catch (Exception) { Status = 0; }
            return Status;
        } 

        //Get Admin Tables
        public static DataTableResponse AdminTable(DataTableRequest request)
        {
            List<string[]> li = new List<string[]>();
            int filteredRows = 0;
            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;
                if (!expired)
                {
                    string cs = ConfigurationManager.ConnectionStrings["AdminContext"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        SqlCommand cmd = new SqlCommand("sP_Admins", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramDisplayLength = new SqlParameter()
                        {
                            ParameterName = "@DisplayLength",
                            Value = request.Length
                        };
                        cmd.Parameters.Add(paramDisplayLength);

                        SqlParameter paramDisplayStart = new SqlParameter()
                        {
                            ParameterName = "@DisplayStart",
                            Value = request.Start
                        };
                        cmd.Parameters.Add(paramDisplayStart);

                        SqlParameter paramSortCol = new SqlParameter()
                        {
                            ParameterName = "@SortCol",
                            Value = request.Order[0].Column
                        };
                        cmd.Parameters.Add(paramSortCol);

                        SqlParameter paramSortDir = new SqlParameter()
                        {
                            ParameterName = "@SortDir",
                            Value = request.Order[0].Dir
                        };
                        cmd.Parameters.Add(paramSortDir);

                        SqlParameter paramSearchString = new SqlParameter()
                        {
                            ParameterName = "@Search",
                            Value = string.IsNullOrEmpty(request.Search.Value) ? null : request.Search.Value
                        };
                        cmd.Parameters.Add(paramSearchString);

                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            string[] CP = new string[7];
                            CP[0] = "<img src='"+ rdr["Image"].ToString() + "' class='img-circle' style='width:60px !important; height:60px !important; margin-left:25px;'>";
                            CP[1] = rdr["Occupation"].ToString();
                            CP[2] = rdr["FullName"].ToString();
                            CP[3] = rdr["Email"].ToString();
                            CP[4] = "**********"; 
                            try
                            {
                                CP[5] = DateTime.Parse(rdr["addDate"].ToString()).ToShortDateString();
                            }
                            catch (Exception) { CP[5] = rdr["addDate"].ToString(); }
                            if (CP[1] == "MasterAdmin")
                            {
                                CP[6] = "<button type='button' class='btn btn-link' onclick='Edit(\"" + rdr["ID"].ToString() + "\");' data-toggle='tooltip' title='Edit'><i class='fa fa-edit'></i></button>"; 
                            }
                            else
                            { 
                                CP[6] = "<button type='button' class='btn btn-link' onclick='Edit(\"" + rdr["ID"].ToString() + "\");' data-toggle='tooltip' title='Edit'><i class='fa fa-edit'></i></button>";
                                CP[6] = CP[6] + "<button type='button' class='btn btn-link' onclick='Delete(\"" + rdr["ID"].ToString() + "\");' data-toggle='tooltip' title='Delete'><i class='fa fa-trash'></i></button>"; 
                            }
                            li.Add(CP);
                            filteredRows = Int32.Parse(rdr["TotalCount"].ToString());
                        }
                    }
                }
            }
            catch (Exception) { }
            return new DataTableResponse
            {
                draw = request.Draw,
                recordsTotal = filteredRows,
                recordsFiltered = filteredRows,
                data = li.ToArray()
            };
        }
    }
}