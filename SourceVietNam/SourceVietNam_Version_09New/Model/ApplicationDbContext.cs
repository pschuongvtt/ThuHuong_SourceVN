using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SourceVietNam_Version_09New.Model
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //..
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*Xử lý khi tạo ra model không có tiền tố AspNet duyệt qua mỗi bảng được kế thừa IdentityUser.  
             modelBuilder.Model.GetEntityTypes(): Lấy tthông tin tên của bảng được tạo ra 
            */
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    //Kiểm tra nếu tên bảng có tiền tố AspNet thì bỏ đi 6 kí tự đầu tiên của tableName
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            //Tạo key haskey cho MasterData
            modelBuilder.Entity<MasterData>().HasKey(m => new { m.CodeId, m.Type });
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductHistory> ProductHistory { get; set; }
        public DbSet<ProductStg> ProductStg { get; set; }
        public DbSet<Manufacture> Manufacture { get; set; }
        public DbSet<ManufactureHistory> ManufactureHistory { get; set; }
        public DbSet<ManufactureStg> ManufactureStg { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<MasterData> MasterData { get; set; }
        public DbSet<Variant> Variant { get; set; }
        public DbSet<VariantHistory> VariantHistory { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<SearchHistory> SearchHistory { get; set; }
        public DbSet<QuoteRequestHistory> QuoteRequestHistory { get; set; }

    }
}
