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

        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.ReviewID == reviewId);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReviewEntry> GetReviewAsync(int reviewId)
        {
            return await _context.Reviews.FindAsync(reviewId);
        }

        public async Task<IEnumerable<IReviewEntry>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<IReviewEntry>> GetReviewsForProductAsync(int productId)
        {
            return await _context.Reviews.Where(r => r.ProductID == productId).ToListAsync();
        }

        public async Task SaveReviewAsync(ReviewEntry review)
        {
            _context.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(ReviewEntry review)
        {
            _context.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
