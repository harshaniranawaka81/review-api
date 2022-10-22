using System;

namespace ReviewAPI.Domain
{
    public interface IProduct
    {
        int ProductID { get; set; }

        string ProductName { get; set; }

        string ProductDescription { get; set; }

        double ProductPrice { get; set; }
    }
}
