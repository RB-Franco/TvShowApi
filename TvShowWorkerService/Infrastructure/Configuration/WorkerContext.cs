﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TvShowWorkerService.Infrastructure.Entity;

namespace TvShowWorkerService.Infrastructure.Configuration
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
            var strCon = "Data Source=LAPTOP-KAMILLA;Initial Catalog=TvShowsDb;User ID=sa;Password=sa@2021;Persist Security Info=True";
            return strCon;
        }
    }
}
