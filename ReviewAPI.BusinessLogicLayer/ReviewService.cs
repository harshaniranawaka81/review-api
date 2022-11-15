using ReviewAPI.DataAccessLayer;
using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ReviewAPI.BusinessLogicLayer
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
            => _repository = repository;
        
        public async Task SubmitReviewAsync(ReviewEntry review)
        {
            await _repository.SaveReviewAsync(review);
        }

        public async Task EditReviewAsync(ReviewEntry review)
        {
            await _repository.UpdateReviewAsync(review);
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            await _repository.DeleteReviewAsync(reviewId);
        }

        public async Task<IReviewEntry> GetReviewAsync(int reviewId)
        {
            var review = await _repository.GetReviewAsync(reviewId);

            if (review == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return review;
        }

        public async Task<IEnumerable<IReviewEntry>> GetAllReviewsAsync()
        {
            return await _repository.GetAllReviewsAsync();
        }

        public async Task<IEnumerable<IReviewEntry>> GetReviewsForProductAsync(int productId)
        {
            if (productId == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return await _repository.GetReviewsForProductAsync(productId);
        }

        public async Task<IReviewSummary> GetReviewSummaryAsync(int productId)
        {
            if (productId == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var results = new Dictionary<string, double>();

            var reviews = await _repository.GetReviewsForProductAsync(productId);

            double avgScore = Math.Round(reviews.Average(r => r.ReviewScore), 2);
            int recommendedCount = reviews.Count(r => r.IsRecommendedProduct);
            int totalCount = reviews.Count();

            double recommendedPercentage = 0;
            if (recommendedCount > 0)
            {
                recommendedPercentage = Math.Round((double)recommendedCount / totalCount * 100, 2);
            }

            return new ReviewSummary
            {
                AverageScore = avgScore,
                RecommendedPercentage = recommendedPercentage
            };
        }

      
    }
}
