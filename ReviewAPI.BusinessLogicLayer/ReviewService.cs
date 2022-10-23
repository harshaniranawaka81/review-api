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

        public async Task<IEnumerable<IReviewEntry>> GetReviewsForProductAsync(int productId)
        {
            if (productId == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var review = await _repository.GetReviewsForProductAsync(productId);

            if (review == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return review;
        }

        public async Task<Dictionary<string, double>> GetReviewSummaryAsync(int productId)
        {
            if (productId == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var results = new Dictionary<string, double>();

            var reviews = await _repository.GetReviewsForProductAsync(productId);

            if (reviews == null || reviews.Count() == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            double avgScore = Math.Round(reviews.Average(r => r.ReviewScore), 2);
            int recommendedCount = reviews.Count(r => r.IsRecommendedProduct);

            //Here rather than using GetAllReviewsAsync and counting the total I introduced a new method to the repository considering the performance
            //To calculate just the count there is no point to pulling all records from database to the application
            //Rather than pulling all we can just get the count
            int totalCount = await _repository.GetTotalReviewsForProductAsync(productId);

            double recommendedPercentage = 0;
            if (recommendedCount > 0)
            {
                recommendedPercentage = Math.Round((double)recommendedCount / totalCount * 100, 2);
            }

            results.Add($"Average Score for Product {productId}", avgScore);
            results.Add($"Percentage recommended for Product {productId} %", recommendedPercentage);

            return results;
        }

        public async Task SubmitReviewAsync(ReviewEntry review)
        {
            await _repository.SaveReviewAsync(review); 
        }

        //Not required for this implementation - but added for consistency
        public async Task<IEnumerable<IReviewEntry>> GetAllReviewsAsync()
        {
            var reviews = await _repository.GetAllReviewsAsync();

            if (reviews == null || reviews.Count() == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return reviews;
        }
    }
}
