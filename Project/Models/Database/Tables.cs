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

    //Add Control
    public class AddControls
    {
        public int ID { get; set; }
         
        public float Interstitial_1 { get; set; }

        public bool Interstitial_1_cb { get; set; } 

        public float Bottom_banner { get; set; }

        public bool Bottom_banner_cb { get; set; }

        public float RewardVideo { get; set; }

        public bool RewardVideo_cb { get; set; } 
    }

    //AddMob Settings
    public class AddSettings
    {
        public int ID { get; set; }

        [MaxLength(200)]
        public string Android_Add_Mob_App_Id { get; set; }

        [MaxLength(200)]
        public string Android_Interstitial_Unit_Id_1 { get; set; } 

        [MaxLength(200)]
        public string Android_Bottom_banner_unit_Id { get; set; } 

        [MaxLength(200)]
        public string Android_Reward_unit_Id { get; set; }

        [MaxLength(200)]
        public string Ios_Add_Mob_App_Id { get; set; }

        [MaxLength(200)]
        public string Ios_Interstitial_Unit_Id_1 { get; set; }

        [MaxLength(200)]
        public string Ios_Bottom_banner_unit_Id { get; set; }

        [MaxLength(200)]
        public string Ios_Reward_unit_Id { get; set; }
    }

    //CouponList Table
    public class CouponLists
    {
        public int ID { get; set; }

        [MaxLength(200)]
        public string Device { get; set; } 

        [MaxLength(200)]
        public string MacAddress { get; set; }

        [MaxLength(200)]
        public string AdUnit { get; set; }  

        [MaxLength(200)]
        public string Coupon { get; set; } 
         
        public DateTime Updated { get; set; }
         
    } 

    public class ApiClass
    { 
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public class Response
    {
        public IEnumerable<object> List { get; set; }
    }

}
