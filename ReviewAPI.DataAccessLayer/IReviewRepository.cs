using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewAPI.DataAccessLayer
{
    public interface IReviewRepository
    {
        //Not required for this implementation - but added for consistency
        Task<IReviewEntry?> GetReviewAsync(int reviewId);

        Task SaveReviewAsync(IReviewEntry review);

        //Not required for this implementation - but added for consistency
        Task UpdateReviewAsync(ReviewEntry review);

        //Not required for this implementation - but added for consistency
        Task DeleteReviewAsync(int reviewId);

        //Not required for this implementation - but added for consistency
        Task<IEnumerable<IReviewEntry>?> GetAllReviewsAsync();

        Task<int> GetTotalReviewsForProductAsync(int productId);

        Task<IEnumerable<IReviewEntry>?> GetReviewsForProductAsync (int productId);
    }
}
