using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

// Create after creating models

namespace PokemonReviewApp.Data
{
    public class DataContext : DbContext // 1 DB SETUP
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) // 2
        {
            //DataContext inherits from DbContext, this is a constructor that tells the database your configurations/settings for your app
        }

        public DbSet<Category> Categories { get; set; } //DbSet tells DB what tables you have and what objects they are, allowing querying in C# // 3
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //many to many, how you go in and customize tables without going into DB
        {
            //Pokemon Category Many to Many
            modelBuilder.Entity<PokemonCategory>() // 4
                .HasKey(pc => new { pc.PokemonId, pc.CategoryId }); // Link two IDs together or EF won't know
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(c => c.CategoryId);

            //Pokemon Owner Many to Many
            modelBuilder.Entity<PokemonOwner>() // 5 ----> 6 is in Program.cs
                .HasKey(po => new { po.PokemonId, po.OwnerId });
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonOwners)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Owner)
                .WithMany(pc => pc.PokemonOwners)
                .HasForeignKey(c => c.OwnerId);
        }

    }
}
