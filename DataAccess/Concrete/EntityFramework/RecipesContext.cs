using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class RecipesContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // PostgreSQL bağlantı stringi
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=recipes_db;Username=postgres;Password=AnnamammedowM2108;Timeout=10;SslMode=Prefer");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("recipes");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Source).HasColumnName("source");
                entity.Property(e => e.PrepTime).HasColumnName("preptime");
                entity.Property(e => e.WaitTime).HasColumnName("waittime");
                entity.Property(e => e.CookTime).HasColumnName("cooktime");
                entity.Property(e => e.Servings).HasColumnName("servings");
                entity.Property(e => e.Comments).HasColumnName("comments");
                entity.Property(e => e.Calories).HasColumnName("calories");
                entity.Property(e => e.Fat).HasColumnName("fat");
                entity.Property(e => e.SatFat).HasColumnName("satfat");
                entity.Property(e => e.Carbs).HasColumnName("carbs");
                entity.Property(e => e.Fiber).HasColumnName("fiber");
                entity.Property(e => e.Sugar).HasColumnName("sugar");
                entity.Property(e => e.Protein).HasColumnName("protein");
                entity.Property(e => e.Instructions).HasColumnName("instructions");
                entity.Property(e => e.Ingredients).HasColumnName("ingredients").HasColumnType("jsonb");
                entity.Property(e => e.Tags).HasColumnName("tags").HasColumnType("jsonb");
            });
        }
    }
}
