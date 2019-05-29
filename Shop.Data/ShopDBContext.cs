using Microsoft.AspNet.Identity.EntityFramework;
using Shop.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public partial class ShopDBContext : IdentityDbContext<ApplicationUser>
    {
        public ShopDBContext() : base("name=shop")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<ProductCategory> ProductCategories { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<OrderDetail> OrderDetails { set; get; }
        public virtual DbSet<Post> Posts { set; get; }
        public virtual DbSet<PostTag> PostTags { set; get; }
        public virtual DbSet<Tag> Tags { set; get; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<ContactDetail> ContactDetails { get; set; }
        public static ShopDBContext Create()
        {
            return new ShopDBContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
        }
    }
}
