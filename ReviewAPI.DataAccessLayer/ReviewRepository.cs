using Microsoft.EntityFrameworkCore;
using ReviewAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ReviewAPI.DataAccessLayer
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewDataContext _context;

        public ReviewRepository(ReviewDataContext context)
            => _context = context;

        //Not required for this implementation - but added for consistency
        public async Task DeleteReviewAsync(int reviewId)
        {
            _context.Remove(reviewId);
            await _context.SaveChangesAsync();
        }

        //Not required for this implementation - but added for consistency
        public async Task<IReviewEntry?> GetReviewAsync(int reviewId)
        {
            return await _context.Reviews.FindAsync(reviewId);
        }

        //Not required for this implementation - but added for consistency
        public async Task<IEnumerable<IReviewEntry>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<IReviewEntry>?> GetReviewsForProductAsync(int productId)
        {
            return await _context.Reviews.Where(r => r.ProductID == productId).ToListAsync();
        }

        public async Task<int> GetTotalReviewsForProductAsync(int productId)
        {
            return await _context.Reviews.CountAsync(r => r.ProductID == productId);
        }

        public async Task SaveReviewAsync(ReviewEntry review)
        {
            _context.Add(review);
            await _context.SaveChangesAsync();
        }

        //Not required for this implementation - but added for consistency
        public async Task UpdateReviewAsync(ReviewEntry review)
        {
            _context.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
