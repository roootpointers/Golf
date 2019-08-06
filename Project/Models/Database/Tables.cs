using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Project.Models.Database
{   
    //Admin Class
    public class Admins
    {
        public int ID { get; set; }

        [MaxLength(11)]
        public string Occupation { get; set; }

        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }

        [MaxLength(50)]
        public string Email { get; set; } 

        [MaxLength(50)]
        public string Password { get; set; } 

        public DateTime? AddDate { get; set; }  
    }  

    public class Matches
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double CourseSlop { get; set; }

        public virtual ICollection<Golfers> Golfers { get; set; }
    }

    public class MatchesGolfers
    {
        public int ID { get; set; }
        [ForeignKey("Matches")]
        public int MatchID { get; set; }
        public Matches Matches { get; set; }

        [ForeignKey("Golfers")]
        public int GolferID { get; set; }
        public Golfers Golfers { get; set; }
    }

    public class Golfers
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double PlayerIndex { get; set; }
        public string TeeBoxes { get; set; }

        public virtual ICollection<Matches> Matches { get; set; }
        public virtual ICollection<Holes> Holes { get; set; }
    }

    public class Holes
    {
        public int ID { get; set; }

        public int Type { get; set; }

        public int H1 { get; set; }
        public int H2 { get; set; }
        public int H3 { get; set; }
        public int H4 { get; set; }
        public int H5 { get; set; }
        public int H6 { get; set; }
        public int H7 { get; set; }
        public int H8 { get; set; }
        public int H9 { get; set; }
        public int H10 { get; set; }
        public int H11 { get; set; }
        public int H12 { get; set; }
        public int H13 { get; set; }
        public int H14 { get; set; }
        public int H15 { get; set; }
        public int H16 { get; set; }
        public int H17 { get; set; }
        public int H18 { get; set; }

        public int? MatchId { get; set; }

        //[ForeignKey("Golfers")]
        public int? GolfersID { get; set; }
        public Golfers Golfers { get; set; }

        public DateTime Updated { get; set; }
    } 

}
