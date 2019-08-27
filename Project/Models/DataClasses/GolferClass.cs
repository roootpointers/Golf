using Project.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models.DataClasses
{
    public class GolferClass
    {

        public static List<Golfers> GetGolfers(int MatchId)
        {
            AdminContext db = new AdminContext();
            try
            {
                var match = db.Matches.Include(o => o.Golfers).Where(o => o.ID == MatchId).FirstOrDefault();
                return match.Golfers.ToList();
            }
            catch (Exception ex)
            {
            }
            return new List<Golfers>();
        }

        public static Golfers UpdateGolfer(Golfers golfer)
        {
            AdminContext db = new AdminContext();
            try
            {
                db.Entry<Golfers>(golfer).State = EntityState.Modified;
                db.SaveChanges();
                return golfer;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}