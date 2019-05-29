namespace Shop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Shop.Model.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.Data.ShopDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Shop.Data.ShopDBContext context)
        {
            CreateProductCategory(context);
            CreateProduct(context);
            CreateContactDetail(context);
        }
        void CreateUser(ShopDBContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShopDBContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShopDBContext()));

            var user = new ApplicationUser()
            {
                UserName = "ThucPug",
                Email = "Pugcon@gmail.com",
                EmailConfirmed = true,
                Birthday = DateTime.Now,
                FullName = "Hoang Thuc"

            };

            manager.Create(user, "thuc0533");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("Pugcon@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
        void CreateProductCategory(ShopDBContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> l = new List<ProductCategory>()
            {
                new ProductCategory() {Name= "Iphone6",Description="Dien thoai apple",Status=false},
                new ProductCategory() {Name= "Iphone7",Description="Dien thoai apple",Status=true},
                new ProductCategory() {Name= "Iphone8",Description="Dien thoai apple",Status=true},
                new ProductCategory() {Name= "IphoneX",Description="Dien thoai apple",Status=true}
            };
                context.ProductCategories.AddRange(l);
                context.SaveChanges();
            }

        }
        void CreateProduct(ShopDBContext context)
        {
            if (context.Products.Count() == 0)
            {
                List<Product> l = new List<Product>()
            {
                new Product() {Name= "Iphone6",Price=4200000,PromotionPrice= 4099999,Warranty = 4,Description="Dien thoai apple 6",Content="ffff",Status=false,CategoryID=1},
                new Product() {Name= "Iphone6s",Price=4500000,PromotionPrice= 4399999,Warranty = 4,Description="Dien thoai apple 6s",Content="ffff",Status=true,CategoryID=1},
                new Product() {Name= "Iphone6 se",Price=4800000,PromotionPrice= 4699999,Warranty = 4,Description="Dien thoai apple 6 se",Content="ffffd",Status=true,CategoryID=1}
            };
                context.Products.AddRange(l);
                context.SaveChanges();
            }

        }
        private void CreateContactDetail(ShopDBContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new ContactDetail()
                    {
                        Name = "Shop Ipug sell iphone ",
                        Address = "120 Lê Duẩn, Thạch Thang, Q. Hải Châu, Đà Nẵng",
                        Email = "IpugMobile@gmail.com",
                        Lat = 16.07086,
                        Lng = 108.2159,
                        Phone = "0999666999",
                        Website = "http://IpugMobile.com.vn",
                        Other = "",
                        Status = true

                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }

            }
        }
    }
}
