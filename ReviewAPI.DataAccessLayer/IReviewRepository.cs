using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewAPI.DataAccessLayer
{
    public interface IReviewRepository
    {
        Task<IReviewEntry> GetReviewAsync(int reviewId);

        Task SaveReviewAsync(ReviewEntry review);

        Task UpdateReviewAsync(ReviewEntry review);

        Task DeleteReviewAsync(int reviewId);

        Task<IEnumerable<IReviewEntry>> GetAllReviewsAsync();

        Task<IEnumerable<IReviewEntry>> GetReviewsForProductAsync (int productId);
    }
}
