using Entity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        //public virtual DbSet<TvShow> TvShow { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConnection());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<User>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }

        protected string GetStringConnection()
        {
            var strCon = "Data Source=LAPTOP-KAMILLA;Initial Catalog=TvShowsDb;User ID=sa;Password=sa@2021;Persist Security Info=True";
            return strCon;
        }
    }
}
