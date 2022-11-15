using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAPI.BusinessLogicLayer
{
    public interface IReviewService
    {
        Task SubmitReviewAsync(ReviewEntry review);

        Task EditReviewAsync(ReviewEntry review);

        Task DeleteReviewAsync(int reviewId);

        Task<IReviewEntry> GetReviewAsync(int reviewId);

        Task<IEnumerable<IReviewEntry>> GetAllReviewsAsync();

        Task<IReviewSummary> GetReviewSummaryAsync(int productId);

        Task<IEnumerable<IReviewEntry>> GetReviewsForProductAsync(int productId); 
        
    }
}
