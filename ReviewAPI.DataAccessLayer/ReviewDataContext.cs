using Microsoft.EntityFrameworkCore;
using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAPI.DataAccessLayer
{
    public class ReviewDataContext : DbContext
    {
        public ReviewDataContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ReviewsDB");
        }

        public DbSet<ReviewEntry> Reviews { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductID = 1, ProductName = "P1", ProductDescription = "Product1", ProductPrice = 10  },
                new Product() { ProductID = 2, ProductName = "P2", ProductDescription = "Product2", ProductPrice = 20 },
                new Product() { ProductID = 3, ProductName = "P3", ProductDescription = "Product3", ProductPrice = 30 },
                new Product() { ProductID = 4, ProductName = "P4", ProductDescription = "Product4", ProductPrice = 40 },
                new Product() { ProductID = 5, ProductName = "P5", ProductDescription = "Product5", ProductPrice = 50 }
                );

            modelBuilder.Entity<ReviewEntry>().HasData(
                new ReviewEntry() {  ReviewID = 1, ReviewTitle = "Review1", ProductID = 1, ReviewComment = "Review comment 1", IsRecommendedProduct = true, ReviewScore = 5 },
                new ReviewEntry() { ReviewID = 2, ReviewTitle = "Review2", ProductID = 1, ReviewComment = "Review comment 2", IsRecommendedProduct = false, ReviewScore = 2 },
                new ReviewEntry() { ReviewID = 3, ReviewTitle = "Review3", ProductID = 2, ReviewComment = "Review comment 3", IsRecommendedProduct = true, ReviewScore = 4 },
                new ReviewEntry() { ReviewID = 4, ReviewTitle = "Review4", ProductID = 2, ReviewComment = "Review comment 4", IsRecommendedProduct = false, ReviewScore = 1 },
                new ReviewEntry() { ReviewID = 5, ReviewTitle = "Review5", ProductID = 3, ReviewComment = "Review comment 5", IsRecommendedProduct = true, ReviewScore = 3 },
                new ReviewEntry() { ReviewID = 6, ReviewTitle = "Review6", ProductID = 3, ReviewComment = "Review comment 6", IsRecommendedProduct = true, ReviewScore = 4 },
                new ReviewEntry() { ReviewID = 7, ReviewTitle = "Review7", ProductID = 4, ReviewComment = "Review comment 7", IsRecommendedProduct = true, ReviewScore = 5 },
                new ReviewEntry() { ReviewID = 8, ReviewTitle = "Review8", ProductID = 4, ReviewComment = "Review comment 8", IsRecommendedProduct = true, ReviewScore = 4 },
                new ReviewEntry() { ReviewID = 9, ReviewTitle = "Review9", ProductID = 5, ReviewComment = "Review comment 9", IsRecommendedProduct = false, ReviewScore = 2 },
                new ReviewEntry() { ReviewID = 10, ReviewTitle = "Review10", ProductID = 5, ReviewComment = "Review comment 10", IsRecommendedProduct = false, ReviewScore = 1 }
                );
        }
    }
}
