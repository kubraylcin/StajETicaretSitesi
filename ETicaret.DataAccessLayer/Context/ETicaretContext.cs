using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Context
{
    public class ETicaretContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ETicaretContext()
        {
        }

        public ETicaretContext(DbContextOptions<ETicaretContext> options) : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Ordering> Orderings { get; set; }
        public DbSet<Coupon> Coupones { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CargoCompany> CargoCompanies { get; set; }
        public DbSet<CargoCustomer> CargoCustomers { get; set; }
        public DbSet<CargoDetail> CargoDetails { get; set; }
        public DbSet<FeatureSlider> FeatureSliders { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
