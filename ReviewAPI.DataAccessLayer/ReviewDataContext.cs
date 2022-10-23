using Microsoft.EntityFrameworkCore;
using ReviewAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAPI.DataAccessLayer
{
    public class ReviewDataContext : DbContext
    {
        public ReviewDataContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ReviewsDB");
        }

        public DbSet<ReviewEntry> Reviews { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
