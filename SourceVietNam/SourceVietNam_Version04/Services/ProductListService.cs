using SourceVietNam_Version04.Model;

namespace SourceVietNam_Version04.Services
{
    public class ProductListService
    {
        public List<Product> lsProduct { get; set; } = new List<Product>() {
            new Product(){Name = "SP 1", Description = "Mô tả thông tin SP1", Price = 20000000},
            new Product(){Name = "SP 2", Description = "Mô tả thông tin SP2", Price = 30000000},
            new Product(){Name = "SP 3", Description = "Mô tả thông tin SP3", Price = 40000000},
            new Product(){Name = "SP 4", Description = "Mô tả thông tin SP4", Price = 12000000},
        };
    }
}
