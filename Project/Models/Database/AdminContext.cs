using System.Data.Entity;

namespace Project.Models.Database
{
    public class AdminContext : DbContext
    {
        public DbSet<Admins> Admins { get; set; } 
        public DbSet<AddControls> AddControls { get; set; } 
        public DbSet<AddSettings> AddSettings { get; set; }
        public DbSet<CouponLists> CouponLists { get; set; } 
        public AdminContext() : base("name=AdminContext")  
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}