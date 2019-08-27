using Project.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models.DataClasses
{
    public class MatchesClass
    {
        public static Matches GetMatch(int matchId)
        {
            AdminContext db = new AdminContext();
            try
            {
                return db.Matches.Where(o => o.ID == matchId).FirstOrDefault();
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public static Matches UpdateMatch(Matches match)
        {
            AdminContext db = new AdminContext();
            try
            {
                db.Entry<Matches>(match).State = EntityState.Modified;
                db.SaveChanges();
                return match;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}