using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.FavoriteRecipesController;
using api.Models.Recipe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContext) : base(dbContext)
        {
            
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<Recipe>()
            .HasMany(r => r.Instruction)
            .WithOne(i => i.Recipe)
            .HasForeignKey(i => i.RecipeId);

            builder.Entity<Recipe>()
            .HasMany(r => r.Ingredients)
            .WithOne(i => i.Recipe)
            .HasForeignKey(i => i.RecipeId);

            builder.Entity<FavoriteRecipe>().HasKey(fr => new {fr.RecipeId, fr.UserId});

            builder.Entity<FavoriteRecipe>()
            .HasOne(fr => fr.User)
            .WithMany(u => u.FavoriteRecipes)
            .HasForeignKey(fr => fr.UserId);

            builder.Entity<FavoriteRecipe>()
            .HasOne(fr => fr.Recipe)
            .WithMany(r => r.FavoriteRecipes)
            .HasForeignKey(fr => fr.RecipeId);
        }
    }
}