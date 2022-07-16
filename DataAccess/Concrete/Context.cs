using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .;Database=ButikDeneme;Integrated Security=True;");
        }
        public DbSet<User> Users{ get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims{ get; set; }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Address> Addresses{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<LikedProduct> LikedProducts{ get; set; }
        public DbSet<ProductFeature> ProductFeatures{ get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
