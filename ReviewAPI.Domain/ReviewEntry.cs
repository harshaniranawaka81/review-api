using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAPI.Domain
{
    public class ReviewEntry : IReviewEntry
    {
        [Range(1, 5, ErrorMessage = "Review score should be a value between 1 to 5")]
        public int ReviewScore { get; set; }

        [Key]
        public int ReviewID { get; set; }

        [Required]
        public string ReviewTitle { get; set; }

        [Required]
        public int ProductID { get; set; }

        [MaxLength(500)]
        public string ReviewComment { get; set; }

        public bool IsRecommendedProduct { get; set; }
    }
}
