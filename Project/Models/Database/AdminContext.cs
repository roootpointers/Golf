using System.Data.Entity;

namespace Project.Models.Database
{
    public class AdminContext : DbContext
    {
        public DbSet<Admins> Admins { get; set; }  
        public DbSet<Matches> Matches { get; set; }
        public DbSet<Golfers> Golfers { get; set; }
        public DbSet<MatchesGolfers> MatchesGolfers { get; set; }
        public DbSet<Holes> Holes { get; set; }
        
        public AdminContext() : base("name=AdminContext")  
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matches>().HasMany(g => g.Golfers).WithMany(g => g.Matches)
                .Map(m =>
                {
                    m.MapLeftKey("MatchID");
                    m.MapRightKey("GolferID");
                    m.ToTable("MatchesGolfers");
                });

            modelBuilder.Entity<Holes>().HasOptional(h => h.Golfers).WithMany(h => h.Holes).HasForeignKey(i => i.GolfersID);
            modelBuilder.Entity<Golfers>().HasMany(h => h.Holes).WithOptional();
            //modelBuilder.Entity<Golfers>().HasMany(g => g.Holes).WithOptional(g => g.Golfers)
            //    .Map(m =>
            //    {
            //        m.MapKey("GolfersID");
            //        m.ToTable("Holes");
            //    });

            base.OnModelCreating(modelBuilder);
        }
    }
}