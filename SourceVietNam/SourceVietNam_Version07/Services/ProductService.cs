using SourceVietNam_Version07.Model;

namespace SourceVietNam_Version07.Services
{
    public class ProductService
    {
        //Khai báo 1 trường dữ liệu chứa danh sách các sản phẩm
        private List<Product> lsProduct = new List<Product>();

        //Tạo ra 1 phương thức tạo ra 1 số sản phẩm trong danh sách sản phẩm
        public void LoadProduct()
        {
            //Xóa danh sách các sản phẩm có trước đó
            lsProduct.Clear();
            //Thêm sản phẩm vào danh sách các sản phẩm
            lsProduct.Add(new Product() { Id = 1, Name = "Iphone", Description = "Điện thoại Iphone", Price = 10000000 });
            lsProduct.Add(new Product() { Id = 2, Name = "Samsung", Description = "Điện thoại Samsung", Price = 8999000 });
            lsProduct.Add(new Product() { Id = 3, Name = "Nokia", Description = "Điện thoại Nokia", Price = 4299000 });
            lsProduct.Add(new Product() { Id = 4, Name = "Asus", Description = "Điện thoại Asus", Price = 4450000 });
            lsProduct.Add(new Product() { Id = 5, Name = "Oppo", Description = "Điện thoại Oppo", Price = 7999000 });
        }

        //Xây dựng phương thức tìm sản phẩm theo ID
        public Product SearchProduct(int? id)
        {
            var linq_product = from p in lsProduct where p.Id == id select p;
            //Trả về thông tin Sản phẩm tìm kiếm được đầu tiên nếu tìm thấy 
            return linq_product.FirstOrDefault();
        }

        //Nếu không tìm thấy Product khi ProductService thì trả về danh sách các sản phẩm. Khi thi hành phương thức này, nó trả về chính dữ liệu của Product 
        public List<Product> AllProduct() => lsProduct;

        //Xây dựng hàm khởi tạo cho ProductService
        public ProductService()
        {
            //Tạo danh sách các sản phẩm
            LoadProduct();
        }
    }
}
