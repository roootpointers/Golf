using Project.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models.DataClasses
{
    public class HolesClass
    {

        public static int AddHoleValue(Holes hole)
        {
            AdminContext db = new AdminContext();
            try
            {
                hole.Updated = DateTime.Now;
                db.Entry<Holes>(hole).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }

        

        public static List<Holes> GetMatchDetails(int MatchId)
        {
            List<Holes> holes = new List<Holes>();
            AdminContext db = new AdminContext();
            try
            {
                var yardage = db.Holes.OrderByDescending(o => o.Updated).FirstOrDefault(o => o.MatchId == MatchId && o.Type == (int)HoleType.Yardage);
                var handicap = db.Holes.OrderByDescending(o => o.Updated).FirstOrDefault(o => o.MatchId == MatchId && o.Type == (int)HoleType.Handicap);
                var par = db.Holes.OrderByDescending(o => o.Updated).FirstOrDefault(o => o.MatchId == MatchId && o.Type == (int)HoleType.Par);

                if (yardage != null)
                {
                    holes.Add(yardage);
                }
                if (handicap != null)
                {
                    holes.Add(handicap);
                }
                if (par != null)
                {
                    holes.Add(par);
                }

            }
            catch (Exception)
            {
            }
            return holes;
        }

        public static Matches GetCompleteMatch(int MatchId)
        {
            AdminContext db = new AdminContext();
            try
            {
                Matches match = db.Matches.Include(o => o.Golfers).Where(w => w.ID == MatchId)
                .FirstOrDefault();

                foreach (var golfer in match.Golfers)
                {
                    golfer.Holes = (from a in db.Holes
                                    where a.GolfersID == golfer.ID
                                    group a by a.GolfersID
                                   into groups
                                    select groups.OrderByDescending(z => z.Updated).FirstOrDefault()).ToList();
                }



                return match;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static List<Holes> GetGolferScores(int MatchId)
        {
            List<Holes> holes = new List<Holes>();
            AdminContext db = new AdminContext();
            try
            {
                //var golfers = db.Golfers.Where(o => o.)
                //var golfersScores = db.Holes.OrderByDescending(o => o.Updated).Where(o => o.MatchId == MatchId && o.Type == (int)HoleType.Golfer).GroupBy(o => o.GolferId).Select(x => x.FirstOrDefault());

                //return golfers.ToList<Holes>();

            }
            catch (Exception)
            {
            }
            return holes;
        }
    }
}