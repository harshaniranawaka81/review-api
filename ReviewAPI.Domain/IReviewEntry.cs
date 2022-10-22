using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAPI.Domain
{
    public interface IReviewEntry
    {
        int ReviewScore { get; set; }

        int ReviewID { get; set; }

        string ReviewTitle { get; set; }

        int ProductID { get; set; }

        string ReviewComment { get; set; }

        bool IsRecommendedProduct { get; set; }
    }
}
