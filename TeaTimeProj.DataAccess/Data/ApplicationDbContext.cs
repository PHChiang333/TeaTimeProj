using Microsoft.EntityFrameworkCore;
using TeaTimeProj.Models;

namespace TeaTimeProj.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //預設資料
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "果汁", DisplayOrder = 1 },
                new Category { Id = 2, Name = "茶", DisplayOrder = 2 },
                new Category { Id = 3, Name = "咖啡", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1, Name = "台灣水果茶", Size = "大杯", Price = 60, Description = "天然果飲，迷人多變", CategoryId =1, ImageUrl=""},
               new Product { Id = 2, Name = "鐵觀音", Size = "中杯", Price =35 , Description = "品鐵關心，享受人生的味道",CategoryId = 2, ImageUrl = "" },
               new Product { Id = 3, Name = "美式咖啡", Size = "中杯", Price =50 , Description = "用咖啡體悟悠閒時光",CategoryId=3, ImageUrl = "" }
               );
        }
    }
}
