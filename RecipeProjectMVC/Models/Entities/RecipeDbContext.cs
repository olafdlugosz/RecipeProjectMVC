using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace RecipeProjectMVC.Models.Entities
{
    public partial class RecipeDbContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public RecipeDbContext()
        {
            
        }
     
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<HealthLabel> HealthLabel { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Nutritioninfo> Nutritioninfo { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ContextConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<HealthLabel>(entity =>
            {
                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.HealthLabel)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HealthLab__Recip__3B75D760");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__Recip__3C69FB99");
            });

            modelBuilder.Entity<Nutritioninfo>(entity =>
            {
                entity.Property(e => e.Label).HasMaxLength(50);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Nutritioninfo)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nutrition__Recip__3D5E1FD2");
            });
        }
    }
}
