using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAPI.Domain
{
    public class ReviewSummary : IReviewSummary
    {
        public double AverageScore { get; set; }
        public double RecommendedPercentage { get; set; }
    }
}
