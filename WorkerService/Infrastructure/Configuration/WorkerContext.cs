using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Infrastructure.Entity;

namespace WorkerService.Infrastructure.Configuration
{
    public class WorkenContext : IdentityDbContext
    {
        public WorkenContext(DbContextOptions<WorkenContext> options) : base(options)
        {
        }
        public virtual DbSet<WorkerTvShow> WorkerTvShow { get; set; }
        public virtual DbSet<WorkerEpisode> WorkerEpisode { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetStringConnection());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");
            base.OnModelCreating(modelBuilder);
        }

        protected string GetStringConnection()
        {
            var strCon = "Data Source=LAPTOP-R5VBMMC0\\SQLEXPRESS;Initial Catalog=TvShowsDb;User ID=sa;Password=sa;Persist Security Info=True";
            return strCon;
        }
    }
}
