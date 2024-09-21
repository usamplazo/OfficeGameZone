using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GameZoneDbContext : DbContext
    {
        public GameZoneDbContext(DbContextOptions<GameZoneDbContext> options)
            : base(options)
        {
        }

        public DbSet<Scoreboard> Scoreboards { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Contestant> Contestants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scoreboard>(builder =>
            {
                builder.HasKey(s => s.ScoreboardId);

                builder.Property(s => s.ContestantId);
                builder.Property(s => s.DisciplineId);
                builder.Property(s => s.DateDisciplinePlayed);
                builder.Property(s => s.CreatedOnUtc);
                builder.Property(s => s.ModifiedOnUtc);

            });

            modelBuilder.Entity<Contestant>(builder =>
            {
                builder.HasKey(c => c.ContestantId);

                builder.Property(c => c.Username).HasMaxLength(15);
                builder.Property(c => c.Email).HasMaxLength(100);
                builder.Property(c => c.IsAdmin);
                builder.Property(c => c.Age);
                builder.Property(c => c.Image);
                builder.Property(c => c.CreatedOnUtc);
                builder.Property(c => c.ModifiedOnUtc);

                builder.HasIndex(c => c.Username).IsUnique();

            });

            modelBuilder.Entity<Discipline>(builder =>
            {
                builder.ToTable("Disciplines", tableBuilder =>
                {
                    tableBuilder.HasCheckConstraint(
                        "CK_Points_NotNegative",
                        sql: $"{nameof(Discipline.Points)} > 0"
                        );
                });

                builder.HasKey(d => d.DisciplineId);

                builder.Property(d => d.Name).HasMaxLength(20).IsRequired();
                builder.Property(d => d.Description);
                builder.Property(d => d.Points).HasPrecision(5, 2);
                builder.Property(d => d.CreatedOnUtc);
                builder.Property(d => d.ModifiedOnUtc);

            });
        }

    }
}
