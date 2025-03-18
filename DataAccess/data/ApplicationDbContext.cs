using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "action", DisplayOrder = 1 },
            new Category { Id = 2, Name = "scifi", DisplayOrder = 2 },
            new Category { Id = 3, Name = "history", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id=1,Title= "Shoes",Description= "For gentlemen and for running",Price=30 },
                new Product { Id=2,Title= "pants",Description="For gentlemen and for party",Price=25 }
               
                );
        }
    }
}
