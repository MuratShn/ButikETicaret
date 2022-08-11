using Entities.Concrete;
using Entities.Concrete.Identitiy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Context : IdentityDbContext<AppUser,AppRole,int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .;Database=ButikDeneme;Integrated Security=True;");
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Address> Addresses{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<LikedProduct> LikedProducts{ get; set; }
        public DbSet<ProductFeature> ProductFeatures{ get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SoldProduct> SoldProducts{ get; set; }
    }
}
