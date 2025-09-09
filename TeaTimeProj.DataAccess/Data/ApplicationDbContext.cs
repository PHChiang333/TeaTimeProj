using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeaTimeProj.Models;

namespace TeaTimeProj.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //預設資料
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "果汁", DisplayOrder = 1 },
                new Category { Id = 2, Name = "茶", DisplayOrder = 2 },
                new Category { Id = 3, Name = "咖啡", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1, Name = "台灣水果茶", Size = "大杯", Price = 60, Description = "天然果飲，迷人多變", CategoryId = 1, ImageUrl = "" },
               new Product { Id = 2, Name = "鐵觀音", Size = "中杯", Price = 35, Description = "品鐵關心，享受人生的味道", CategoryId = 2, ImageUrl = "" },
               new Product { Id = 3, Name = "美式咖啡", Size = "中杯", Price = 50, Description = "用咖啡體悟悠閒時光", CategoryId = 3, ImageUrl = "" }
               );

            modelBuilder.Entity<Store>().HasData(
                new Store { Id = 1, Name = "台中一中店", Address = "台中市北區三民路三段129號", City = "台中市", PhoneNumber = "04-12345678", Description = "台中一中學生最愛" },
                new Store { Id = 2, Name = "台北公館店", Address = "台北市中正區羅斯福路四段85號", City = "台北市", PhoneNumber = "02-12345678", Description = "台大、師大學生最愛" },
                new Store { Id = 3, Name = "高雄巨蛋店", Address = "高雄市左營區博愛二路777號", City = "高雄市", PhoneNumber = "07-12345678", Description = "高雄巨蛋附近最愛" }
                );
        }
    }
}
