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

        [HttpPost]
        public int AddBoard(List<Board> board)
        {
            return BoardClass.AddBoard(board);
        }

        public int UpdateMatch(Matches match)
        {
            try
            {
                foreach (var hole in match.Holes)
                {
                    hole.MatchId = 1;
                    HolesClass.AddHoleValue(hole);
                }
                var golfers = GolferClass.GetGolfers(1);
                if (golfers.Count >= 2)
                {
                    if (match.Golfers.Count > 0)
                    {
                        match.Golfers.ToList()[0].ID = golfers[0].ID;
                        foreach (var hole in match.Golfers.ToList()[0].Holes)
                        {
                            hole.GolfersID = golfers[0].ID;
                            hole.MatchId = 1;
                            HolesClass.AddHoleValue(hole);
                        }
                        GolferClass.UpdateGolfer(match.Golfers.ToList()[0]);
                    }
                    if (match.Golfers.Count > 1)
                    {
                        match.Golfers.ToList()[1].ID = golfers[1].ID;
                        foreach (var hole in match.Golfers.ToList()[1].Holes)
                        {
                            hole.GolfersID = golfers[1].ID;
                            hole.MatchId = 1;
                            HolesClass.AddHoleValue(hole);
                        }
                        GolferClass.UpdateGolfer(match.Golfers.ToList()[1]);
                    }
                }
                var dbMatch = MatchesClass.GetMatch(1);
                dbMatch.Title = match.Title;
                dbMatch.CourseSlop = match.CourseSlop;
                dbMatch.TName = match.TName;
                MatchesClass.UpdateMatch(dbMatch);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        [HttpPatch]
        public List<Board> UpdateBoard(List<Board> board)
        {
            if (board.Count > 0)
            {
                if (board[0].BoardId != "" && board[0].BoardId != null)
                {
                    if (ModelState.IsValid)
                    {
                        return BoardClass.UpdateBoard(board);
                    }
                }
            }
            return null;
        }

        [HttpGet]
        public List<Board> GetBoards(int limit, int page)
        {
            List<Board> boards = BoardClass.GetBoards(limit, page);
            return boards;
        }

        [HttpGet]
        public List<Board> GetBoard(string boardId)
        {
            return BoardClass.GetBoard(boardId);
        }
    }
}
