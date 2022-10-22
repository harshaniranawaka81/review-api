using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewAPI.BusinessLogicLayer;
using ReviewAPI.Domain;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Get the reviews for a single product 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("~/getReviewsForProduct")]
        public async Task<IEnumerable<IReviewEntry>> GetReviewsForProduct(int productId)
        {
            return await _reviewService.GetReviewsForProductAsync(productId);
        }

        /// <summary>
        /// Get a summary of reviews for product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("~/getReviewsSummary")]
        public async Task<Dictionary<string, double>> GetReviewsSummary(int productId)
        {
            return await _reviewService.GetReviewSummaryAsync(productId);
        }

        /// <summary>
        /// Get all the reviews
        /// </summary>
        /// <returns></returns>   
        //Not required for this implementation - but added for consistency
        [HttpGet("~/getAllReviews")]
        public async Task<IEnumerable<IReviewEntry>> GetAllReviews()
        {
            return await _reviewService.GetAllReviewsAsync();
        }

        /// <summary>
        /// Save a review 
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost("~/submitReview")]
        public async Task SubmitReview(ReviewEntry review)
        {
            await _reviewService.SubmitReviewAsync(review);
        }       
    }
}