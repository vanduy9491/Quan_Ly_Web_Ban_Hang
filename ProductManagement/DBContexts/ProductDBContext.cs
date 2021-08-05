
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.DBContexts
{
    public class ProductDBContext : IdentityDbContext<AppIdentityUser>
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedingCategory(modelBuilder);
            SeedingProduct(modelBuilder);
            SeedingAspNetUser(modelBuilder);
            SeedingAspNetRole(modelBuilder);
            SeedingAspNetUserRole(modelBuilder);
        }
        private void SeedingCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = 1,
                    CategoryName = "Laptop & Macbook",
                    IsDeleted = false
                },
                new Category()
                {
                    CategoryId = 2,
                    CategoryName = "Điện thoại di động",
                    IsDeleted = false
                },
                new Category()
                {
                    CategoryId = 3,
                    CategoryName = "PC - Máy tính đồng bộ",
                    IsDeleted = false
                },
                new Category()
                {
                    CategoryId = 4,
                    CategoryName = "Linh kiện Máy tính",
                    IsDeleted = false
                },
                new Category()
                {
                    CategoryId = 5,
                    CategoryName = "Phụ kiện điện thoại",
                    IsDeleted = false
                }
                );
        }
        private void SeedingProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().HasData(
                new Products()
                {
                    ProductId = 1,
                    ProductName = "Laptop Acer Aspire 3 A315 56 502X i5 ",
                    PublishYear = 2020,
                    CategoryId = 1,
                    Price = 13500000,
                    Quantity = 10,
                    IsDeleted = false,
                    Infomation = "15.6 Inch, 1920 x 1080 Pixel, IPS, 60 Hz, 200 nits, Acer ComfyView LED-backlit",
                    Discount = 0,
                    Photo = "/Images/Laptop-Acer.jpg"
                },
                new Products()
                {
                    ProductId = 2,
                    ProductName = "iPhone 12 Pro Max 128GB ",
                    PublishYear = 2020,
                    CategoryId = 2,
                    Price = 29900000,
                    Quantity = 10,
                    IsDeleted = false,
                    Infomation = "6.7 Inch, Super Retina XDR, OLED, 2778 x 1284 Pixel",
                    Discount = 0.1,
                    Photo = "/Images/Iphone12ProMax.jpg"
                }
                );
        }
        private void SeedingAspNetUser(ModelBuilder modelBuilder)
        {
            AppIdentityUser user = new AppIdentityUser()
            {
                Id = "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                UserName = "vanduy9491@gmail.com",
                NormalizedEmail = "vanduy9491@gmail.com",
                NormalizedUserName = "vanduy9491@gmail.com",
                LockoutEnabled = false,
                Avatar = "/Images/bg-1.jpg"
            };
            PasswordHasher<AppIdentityUser> passwordHasher = new PasswordHasher<AppIdentityUser>();
            var passwordHash = passwordHasher.HashPassword(user, "Zxcv0987!");
            user.PasswordHash = passwordHash;

            modelBuilder.Entity<AppIdentityUser>().HasData(user);
        }
        private void SeedingAspNetRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    Name = "SystemAdmin",
                    NormalizedName = "SystemAdmin",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole()
                {
                    Id = "32ffd287-205f-43a2-9f0d-80bc5309fb47",
                    Name = "Customer",
                    NormalizedName = "Customer",
                    ConcurrencyStamp = "2"
                });
        }
        private void SeedingAspNetUserRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    UserId = "2c0fca4e-9376-4a70-bcc6-35bebe497866"
                }
                );
        }
    }
}
