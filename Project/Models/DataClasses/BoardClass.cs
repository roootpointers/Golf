using Project.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models.DataClasses
{
    public class BoardClass
    {
        public static int AddBoard(List<Board> boards)
        {
            AdminContext db = new AdminContext();
            try
            {
                string id = Guid.NewGuid().ToString();
                for (int i = 0; i < boards.Count; i++)
                {
                    boards[i].BoardId = id;
                    boards[i].Updated = DateTime.Now;
                    db.Entry<Board>(boards[i]).State = EntityState.Added;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }

        public static List<Board> GetBoard(string boardId)
        {
            AdminContext db = new AdminContext();
            return db.Board.Where(o => o.BoardId == boardId).ToList();
        }

        public static List<Board> UpdateBoard(List<Board> boards)
        {
            AdminContext db = new AdminContext();
            try
            {
                for (int i = 0; i < boards.Count; i++)
                {
                    db.Entry<Board>(boards[i]).State = EntityState.Modified;
                }
                db.SaveChanges();
                return boards;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<Board> GetBoards(int limit, int page)
        {
            AdminContext db = new AdminContext();
            if (limit == 0 || page == 0)
            {
                //return (from a in db.Board
                //        group a by a.BoardId
                //        into groups
                //        select groups.First()).ToList();
                return db.Board.GroupBy(o => o.BoardId).Select(s => s.FirstOrDefault()).ToList();
            } else
            {
                return db.Board.GroupBy(o => o.BoardId).Select(s => s.FirstOrDefault()).Skip((page - 1) * limit).Take(limit).ToList();
            }
        }
    }
}