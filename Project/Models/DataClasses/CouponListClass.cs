using Project.Models.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Project.Models.DataClasses
{
    public class CouponListClass
    {
        //Get Record By Id
        public static CouponLists GetRecord(int Id)
        {
            AdminContext db = new AdminContext();
            CouponLists temp = new CouponLists();
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
                    temp = db.CouponLists.FirstOrDefault(x => x.ID == Id);
                }
            }
            catch (Exception) { }

            return temp;
        }

        //Add New Record
        public static int AddRecord(CouponLists temp)
        {
            AdminContext db = new AdminContext();
            int Status = 0;
            try
            {
                db.CouponLists.Add(temp);
                db.SaveChanges();
                Status = temp.ID;
            }
            catch (Exception) { }
            return Status;
        }

        //Update Record
        public static string UpdateRecord(CouponLists temp)
        {
            AdminContext db = new AdminContext();
            string Status = "There wad an error updating a record";
            try
            {
                CouponLists user = db.CouponLists.FirstOrDefault(x => x.Coupon == temp.Coupon);
                if (user != null)
                {
                    user.Device = temp.Device;
                    user.MacAddress = temp.MacAddress;
                    TimeZoneInfo est;
                    try
                    {
                        var timeZones = TimeZoneInfo.GetSystemTimeZones();
                        est = TimeZoneInfo.FindSystemTimeZoneById("Gulf Standard Time");
                        var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);
                        user.Updated = time;
                    }
                    catch (TimeZoneNotFoundException)
                    {
                        user.Updated = DateTime.Now;
                    }

                    db.SaveChanges();
                    Status = user.AdUnit;
                }
                else
                {
                    Status = "There was an error updating a record";
                }
            }
            catch (Exception) { Status = "There was an error updating a record"; }
            return Status;
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
                    CouponLists temp = db.CouponLists.FirstOrDefault(x => x.ID == Id);
                    db.CouponLists.Remove(temp);
                    db.SaveChanges();
                    Status = 1;
                }
            }
            catch (Exception) { Status = 0; }
            return Status;
        }

        //Get Games Tables
        public static DataTableResponse Table(DataTableRequest request, string data)
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
                    string[] split = data.Split('$');
                    var splitStart = split[0].Split('/');
                    var splitEnd = split[1].Split('/');
                    DateTime start = new DateTime(Int32.Parse(splitStart[2]), Int32.Parse(splitStart[0]), Int32.Parse(splitStart[1]), 00, 00, 00);
                    DateTime end = new DateTime(Int32.Parse(splitEnd[2]), Int32.Parse(splitEnd[0]), Int32.Parse(splitEnd[1]), 23, 59, 59);

                    string cs = ConfigurationManager.ConnectionStrings["AdminContext"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        SqlCommand cmd = new SqlCommand("sP_CouponList", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramStartVriable = new SqlParameter()
                        {
                            ParameterName = "@StartDate",
                            Value = start
                        };
                        cmd.Parameters.Add(paramStartVriable);

                        SqlParameter paramEndVriable = new SqlParameter()
                        {
                            ParameterName = "@EndDate",
                            Value = end
                        };
                        cmd.Parameters.Add(paramEndVriable);

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
                            string[] CP = new string[6];
                            CP[0] = rdr["Device"].ToString();
                            CP[1] = rdr["MacAddress"].ToString();
                            CP[2] = rdr["AdUnit"].ToString();
                            CP[3] = rdr["Coupon"].ToString();
                            try
                            {
                                CP[4] = DateTime.Parse(rdr["Updated"].ToString()).ToShortDateString();
                            }
                            catch (Exception) { CP[4] = rdr["Updated"].ToString(); }

                            CP[5] = "<button type='button' class='btn btn-link' onclick='Edit(\"" + rdr["ID"].ToString() + "\");' data-toggle='tooltip' title='Edit'><i class='fa fa-edit'></i></button>";
                            CP[5] = CP[5] + "<button type='button' class='btn btn-link' onclick='Delete(\"" + rdr["ID"].ToString() + "\");' data-toggle='tooltip' title='Delete'><i class='fa fa-trash'></i></button>";

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