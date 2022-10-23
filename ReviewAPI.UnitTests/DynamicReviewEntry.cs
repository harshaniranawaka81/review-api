using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAPI.UnitTests
{
    [TestClass]
    public static class DynamicReviewEntry
    {
        static IReviewEntry DynamicReviewEntrySetup
        {
            get
            {
                return new ReviewEntry() { 
                    ReviewID = 11, 
                    ReviewTitle = "Review11", 
                    ProductID = 1, 
                    ReviewComment = "Review comment 11", 
                    IsRecommendedProduct = true, 
                    ReviewScore = 4 };

            }
        }
    }
}
