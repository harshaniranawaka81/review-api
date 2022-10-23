using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Review.API.Controllers;
using ReviewAPI.BusinessLogicLayer;
using ReviewAPI.DataAccessLayer;
using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewAPI.UnitTests
{
    [TestClass]
    public class TestReviewController
    {
        private readonly DbContextOptions<ReviewDataContext> dbContextOptions = new DbContextOptionsBuilder<ReviewDataContext>()
               .UseInMemoryDatabase(databaseName: "ReviewDB")
               .Options;

        private ReviewController controller;
        private IEnumerable<ReviewEntry> testReviews;

        public void Setup()
        {
            SeedDb();

            var reviewRepository = new ReviewRepository(new ReviewDataContext(dbContextOptions));
            var reviewService = new ReviewService(reviewRepository);
            controller = new ReviewController(reviewService);
        }

        private void SeedDb()
        {
            using var context = new ReviewDataContext(dbContextOptions);
            testReviews = new List<ReviewEntry>
            {
                new ReviewEntry() { ReviewID = 1, ReviewTitle = "Review1", ProductID = 1, ReviewComment = "Review comment 1", IsRecommendedProduct = true, ReviewScore = 5 },
                new ReviewEntry() { ReviewID = 2, ReviewTitle = "Review2", ProductID = 1, ReviewComment = "Review comment 2", IsRecommendedProduct = false, ReviewScore = 2 },
                new ReviewEntry() { ReviewID = 3, ReviewTitle = "Review3", ProductID = 2, ReviewComment = "Review comment 3", IsRecommendedProduct = true, ReviewScore = 4 },
                new ReviewEntry() { ReviewID = 4, ReviewTitle = "Review4", ProductID = 2, ReviewComment = "Review comment 4", IsRecommendedProduct = false, ReviewScore = 1 },
                new ReviewEntry() { ReviewID = 5, ReviewTitle = "Review5", ProductID = 3, ReviewComment = "Review comment 5", IsRecommendedProduct = true, ReviewScore = 3 },
                new ReviewEntry() { ReviewID = 6, ReviewTitle = "Review6", ProductID = 3, ReviewComment = "Review comment 6", IsRecommendedProduct = true, ReviewScore = 4 },
                new ReviewEntry() { ReviewID = 7, ReviewTitle = "Review7", ProductID = 4, ReviewComment = "Review comment 7", IsRecommendedProduct = true, ReviewScore = 5 },
                new ReviewEntry() { ReviewID = 8, ReviewTitle = "Review8", ProductID = 4, ReviewComment = "Review comment 8", IsRecommendedProduct = true, ReviewScore = 4 },
                new ReviewEntry() { ReviewID = 9, ReviewTitle = "Review9", ProductID = 5, ReviewComment = "Review comment 9", IsRecommendedProduct = false, ReviewScore = 2 },
                new ReviewEntry() { ReviewID = 10, ReviewTitle = "Review10", ProductID = 5, ReviewComment = "Review comment 10", IsRecommendedProduct = false, ReviewScore = 1 }
             };

            context.AddRange(testReviews);
            context.SaveChanges();
        }

        [TestMethod]
        public async Task GetReviewsForProduct_ShouldReturnProducts()
        {
            Setup();

            var result = await controller.GetAllReviews();
            Assert.AreEqual(result.Count(), testReviews.ToList().Count); ;
        }

        [TestMethod]
        public async Task GetReviewsForProduct_GetReviewsSummary(int productId)
        {
            Setup();

            var result = await controller.GetReviewsSummary(productId);

            var averageReviewsForProduct = testReviews.Where(r => r.ProductID == productId).ToList().Average(r => r.ReviewScore);
            averageReviewsForProduct = Math.Round(averageReviewsForProduct, 2);

            var reviewsForProduct = testReviews.Where(r => r.ProductID == productId).Count();
            var recommendedReviewsForProduct = testReviews.Where(r => r.ProductID == productId && r.IsRecommendedProduct == true).Count();
            var percentageOfRecommendedProducts = Math.Round(recommendedReviewsForProduct / averageReviewsForProduct * 100, 2);

            Assert.AreEqual(result.ElementAt(0).Value, averageReviewsForProduct);
            Assert.AreEqual(result.ElementAt(1).Value, percentageOfRecommendedProducts);
        }
    }
}
