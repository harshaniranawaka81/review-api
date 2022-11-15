using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAPI.Domain
{
    public interface IReviewSummary
    {
        double AverageScore { get; set; }

        double RecommendedPercentage { get; set; }
    }
}
