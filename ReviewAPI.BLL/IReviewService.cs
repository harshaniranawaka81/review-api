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

        Task<IEnumerable<IReviewEntry>> GetReviewsForProductAsync(int productId);

        Task<Dictionary<string, double>> GetReviewSummaryAsync(int productId);

        //Not required for this implementation - but added for consistency
        Task<IEnumerable<IReviewEntry>> GetAllReviewsAsync();

        Task<int> GetAllReviewsCountAsync();
    }
}
