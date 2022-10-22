using System;
using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Domain
{
    public class Product : IProduct
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public double ProductPrice { get; set; }
    }
}
