using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace football_api_from_scratch.Models
{
    public partial class FootballDBContext : DbContext
    {
        public FootballDBContext()
        {
        }

        public FootballDBContext(DbContextOptions<FootballDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\mssqllocaldb;Initial Catalog = FootballDB;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.PlayerName).HasMaxLength(50);

                entity.Property(e => e.PlayerPosition).HasMaxLength(50);

                entity.Property(e => e.TeamName).HasMaxLength(50);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.TeamLocation).HasMaxLength(50);

                entity.Property(e => e.TeamName).HasMaxLength(50);

                entity.Property(e => e.TeamStadium).HasMaxLength(50);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK__Team__PlayerId__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
