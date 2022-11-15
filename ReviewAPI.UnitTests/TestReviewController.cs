using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Review.API.Controllers;
using ReviewAPI.BusinessLogicLayer;
using ReviewAPI.DataAccessLayer;
using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewAPI.UnitTests
{
    [TestClass]
    public class TestReviewController
    {
        private ReviewRepository reviewRepository;
        private ReviewService reviewService;
        private ReviewController controller;
        private List<ReviewEntry> testReviews;
        private ReviewDataContext dataContext;

        private void Setup()
        {
            SetTestReviews();
            SeedDb();

            reviewRepository = new ReviewRepository(dataContext);
            reviewService = new ReviewService(reviewRepository);
            controller = new ReviewController(reviewService);
        }

        private void SetTestReviews()
        {
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
        }

        private void SeedDb()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ReviewDataContext>()
              .UseInMemoryDatabase(databaseName: "ReviewDB")
              .Options;

            dataContext = new ReviewDataContext(dbContextOptions);

            if (dataContext.Reviews.Any())
            {
                dataContext.Reviews.RemoveRange(testReviews);
                dataContext.SaveChanges();
            }

            dataContext.AddRange(testReviews);
            dataContext.SaveChanges();
        }

        private static ReviewEntry GetTestReview()
        {
            return new ReviewEntry() { ReviewID = 11, ReviewTitle = "Review11", ProductID = 1, ReviewComment = "Review comment 11", IsRecommendedProduct = true, ReviewScore = 4 };
        }
        
        [TestMethod]
        public async Task SubmitReview_ShouldCreateNewReview()
        {
            Setup();

            var review = GetTestReview();
            testReviews.Add(review);

            await controller.SubmitReview(review);

            var result = await controller.GetAllReviews();
            Assert.AreEqual(result.Count(), testReviews.ToList().Count);
        }

        [TestMethod]
        public async Task GetAllReviews_ShouldReturnAllReviews()
        {
            Setup();

            var result = await controller.GetAllReviews();
            Assert.AreEqual(result.Count(), testReviews.ToList().Count);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task GetAllReviews_ShouldDeleteReview(int reviewId)
        {
            Setup();

            await controller.DeleteReview(reviewId);
            var testReview = testReviews.RemoveAll(r => r.ReviewID == reviewId);

            var allReviews = await controller.GetAllReviews();

            Assert.AreEqual(allReviews.Count(), testReviews.ToList().Count);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task GetAllReviews_ShouldReturnSingleReview(int reviewId)
        {
            Setup();

            var result = await controller.GetReview(reviewId);
            var testReview = testReviews.FirstOrDefault(r => r.ReviewID == reviewId);

            Assert.AreSame(result, testReview);
        }

        [TestMethod]
        [DataRow(2)]
        public async Task GetReviewsForProduct_ShouldReturnReviewsForProduct(int productId)
        {
            Setup();

            var result = await controller.GetReviewsForProduct(productId);

            var reviewsForProduct = testReviews.Where(r => r.ProductID == productId).Count();
            Debug.WriteLine($"reviewsForProduct = {reviewsForProduct}");

            Assert.AreEqual(result.Count(), reviewsForProduct);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task GetReviewsSummary_CheckValues(int productId)
        {
            Setup();

            IReviewSummary result = await controller.GetReviewsSummary(productId);

            var averageReviewsForProduct = testReviews.Where(r => r.ProductID == productId).ToList().Average(r => r.ReviewScore);

            averageReviewsForProduct = Math.Round(averageReviewsForProduct, 2);

            Debug.WriteLine($"averageReviewsForProduct = {averageReviewsForProduct}");

            var reviewsForProduct = testReviews.Where(r => r.ProductID == productId).Count();

            Debug.WriteLine($"reviewsForProduct = {reviewsForProduct}");

            var recommendedReviewsForProduct = testReviews.Where(r => r.ProductID == productId && r.IsRecommendedProduct).Count();

            Debug.WriteLine($"recommendedReviewsForProduct = {recommendedReviewsForProduct}");

            var percentageOfRecommendedProducts = Math.Round((double)recommendedReviewsForProduct / reviewsForProduct * 100, 2);

            Debug.WriteLine($"percentageOfRecommendedProducts = {percentageOfRecommendedProducts}");

            Assert.AreEqual(result.AverageScore, averageReviewsForProduct);
            Assert.AreEqual(result.RecommendedPercentage, percentageOfRecommendedProducts);
        }

    }
}
