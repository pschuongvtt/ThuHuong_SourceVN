using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SourceVietNam_Version07.Model;
using SourceVietNam_Version07.Services;

namespace SourceVietNam_Version07.Pages
{
    public class ProductPagesModel : PageModel
    {
        /*
        Trong Model xây dựng sẵn phương thức khởi tạo OnGet() 
        Có thể sử dụng phương thức khởi tạo khác theo public ProductPagesModel() 
        Ngoài ra cũng có thể Inject các dịch vụ khác của Model: ILogger (Hệ thống sẽ khởi tạo qua đối tượng Ilogger dùng cho ProductPagesModel) 
        Qua đó, viết log trong quá trình hoạt động của Model. Sau khi Inject có thể lưu nó lại thông qua trường dữ liệu "private ILogger<ProductPagesModel> _logger"
         */
        private ILogger<ProductPagesModel> _logger;

        /*
        4. Khai báo 1 dịch vụ như là 1 dịch vụ của hệ thống: builder.Services.AddSingleton<ProductService>();
        5. Thực hiện Inject của class ProductService vào ProductPagesModel 
        */
        public readonly ProductService _productService;
        public ProductPagesModel(ILogger<ProductPagesModel> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
            _logger.LogInformation("Init Contact New...");
        }

        //Trong ProductPagesModel tạo 1 property có kiểu Product để xem sản phẩm này thuộc sản phẩm gì
        public Product product { get; set; }

        /*Khai báo các phương thức trong Model bắt đầu bằng tiền tố OnGet(), OnPost(), OnGetABC()
         --> Các phương thức này được gọi là các Handler tùy theo HTTP Request thì theo dùng phương thức Get, Post,...
        Lưu ý: 1 trong các phương thức OnGet(), OnPost(), OnGetABC(),... Nó thường trả về "void" hoặc "IActionResult"
        */
        public void OnGet(int? id)
        {
            //Thiết lập dữ liệu truyền đến View thông qua ViewData. Trong PageModel có 1 thuộc tính tên là ViewData
            ViewData["Title"] = "Trang sản phẩm";
            /*
            Nếu truy cập vào địa chỉ Url không có Id thì hiển thị : "Danh sách các sản phẩm" 
            Ngược lại thì nếu url truyền 123 thì hiển thị "Sản phẩm 123"
            Đọc được giá trị Id có hay không thông qua đối tượng Request. Trong đối tượng Resquest này, có thể truy cập qua "RouteValues" và truy cập theo key của nó
            --> VD: Request.RouteValues["id"]
            Trong phương thức Handler OnGet() có thể khai báo tham số, những tham số này có thể tự động thiệt lập dựa trên giá trị thiết lập các tham số được truyền tới. 
            Những gtrị truyền tới có thể là: "Url, Router, từ các Query của Url, Từ Post dữ liệu,..."
            VD: Tham số là kiểu số nguyên, tên tham số là id, tham số này có thể nhận giá trị null: public void OnGet(int? id){...}
            Khi lúc này thực hiện truy vấn OnGet được thực thi, nó sẽ tìm trên những nguồn dữ liệu truy vấn gửi đến RouteValues xem có trường dữ liệu nào có tên id hay không. Nếu có thì sẽ convert về giá trị
            --> Có thể tự động có được Id luôn mà khai báo là 1 tham số Handler. Thay vì đọc, convert dử liệu thì cần kiểm tra xem id đó có null hay không
            */
            var s = Request.RouteValues["id"];
            if (s != null)
            {
                ViewData["Content"] = $"Sản phẩm mã (ID = {s})";
                product = _productService.SearchProduct(id.Value);
            }
            else
            {
                ViewData["Content"] = $"Danh sách các sản phẩm";
            }

            /*Xây dựng mới: Khi truy cập vào danh sách các sản phẩm thì nó hiển thị ra các tên của 1 số sản phẩm. 
             Sau đó bấm vào xem chi tiết của sản phẩm nào đó. Danh sách các sản phẩm này được lấy từ 1 dịch vụ, dịch vụ đó được inject vào PageModel
             1. Tạo ra thư mục Model, trong đó thiết lập 1 class Product.cs trong đó có thuộc tính: Id, Name, Decription, Price
             2. Khai báo 1 lớp như là dịch vụ, lớp dịch vụ này cung cấp danh sách các sản phẩm. Tạo ra thư mục Services -> ProductServices.cs.
             Khai báo 1 trường dữ liệu chứa danh sách các sản phẩm:  private List<Product> lsProduct = new List<Product>();
             3. //Tạo ra 1 phương thức tạo ra 1 số sản phẩm trong danh sách sản phẩm:  
                private void LoadProduct()
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
                public Product SearchProduct(int id)
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
            4. Khai báo 1 dịch vụ như là 1 dịch vụ của hệ thống: builder.Services.AddSingleton<ProductService>();
            5. Thực hiện Inject của class ProductService vào ProductPagesModel 
            6. Trong ProductPagesModel tạo 1 property có kiểu Product để xem sản phẩm này thuộc sản phẩm gì
            7. Khi id != null, thì có thể tìm sản phẩm là gì, ngược lại thì lấy toàn bộ sản phẩm trong ProductService. Tìm sản phẩm theo id
            */


            /* MODEL BINDING - LIÊN KẾT DỮ LIỆU
             Đọc trực tiếp dữ liệu từ những nguồn được gửi đến
             + Để truy cập vào đối tượng HttpRequest trong PageModel, truy cập vào thuộc tính "this.Request.DoiTuongTruyCap["<key>"]"
             + Trong View .cshtml thì cũng tương tự có thể truy cập thông qua thuộc tính "this.Request.DoiTuongTruyCap["<key>"]"
             + Có 4 cách đọc trực tiếp từ nguồn dữ liệu được gửi đến. Sau khi đọc được dữ liệu, tùy thuộc vào kiểu dữ liệu vào dữ liệu chuyển đổi convert mà ta muốn sử dụng 
             Trong dữ liệu AspNet thì ít khi đọc dữ liệu trực tiếp trong 4 cách trên, mà sử dụng cơ chế "BINDING - Liên kết dữ liệu" 
             Những dữ liệu được gửi đến có thể tự động đọc và chuyển đổi thành kiểu dữ liệu mong muốn
             Để thực hiện liên kết từ các nguồn dữ liệu được gửi đến vào các thuộc tính trong Model, hoặc các tham số của Action, các Handler --> Sử dụng thuộc tính Attribute bổ sung như sau: 
             Sử dụng thuộc tính Attribute, trong đó có 2 kiểu binding dữ liệu: 
             + Liên kết dữ liệu trong các tham số của Action, Handler gọi là : "Parameter Binding"
             + Liên kết dữ liệu với các thuộc tính, các property gọi là: "Property Binding"
             1. Parameter Binding: Cơ chế của Parameter Binding, nghĩa là trong các Handler: OnGet(), OnGetABC(),.., hoặc trong các Action của Controller 

             */
            //var data = this.Request.Form["key"]; //Đọc dữ liệu từ form 
            //var data = this.Request.Query["key"]; //Đọc dữ liệu từ Query trên Url, trả về string 
            //var data = this.Request.RouteValues["key"]; //Đọc dữ liệu từ Route
            var data = this.Request.Headers["user-agent"]; //Đọc dữ liệu từ Header, xem network header: https://localhost:7030/Product/2?id=4
            if (!string.IsNullOrEmpty(data))
            {
                Console.WriteLine(data.ToString());
            }

        }

        /* Trong PageModel ngoài các handlet OnGet(), OnPost(),... Ngoài ra, còn có thể khai báo các handler theo tên 
        VD: public void OnGetLastProduct(int? id){...}
        --> Phương thức này có thể được gọi cho phương thức Get, ngoài ra nó còn có 1 chuỗi query là Handler
        // /product/{id: int?}?handler=lastproduct --> Truy cập theo url này nó sẽ setup OnGet theo phương thức là lastproduct
        lastproduct: Là sản phẩm cuối cùng trong danh sách các sản phẩm trong _productService

        */
        public IActionResult OnGetLastProduct()
        {
            ViewData["Title"] = $"Sản phẩm cuối";
            product = _productService.AllProduct().LastOrDefault();
            if (product != null)
                return Page();
            return this.Content("Không tìm thấy sản phẩm"); // return NotFound();
        }

        /*Các handler xây dụng có thể trả về "void", "IActionResult"
        VD: Xây dụng 1 Handler, khi handler thi hành xóa toàn bộ danh sách các sản phẩm trong ProductService
        */
        public IActionResult OnGetRemoveAll()
        {
            //Xóa toàn bộ sản phẩm trong _productService
            _productService.AllProduct().Clear();
            //Sau khi xóa xong trả về IActionResult, có những phương thức chuyển hướng trang truy cập về trang sản phẩm 
            return RedirectToPage("ProductPages");
            /* Kết quả thông tin được trả về
             Challenge                                  - 401 - Trả về khi xác thực không thành công
             Content                                    - 200 - Thiết lập nội dung trả về trực tiếp
             File	                                    - 200 - Trả về nội dung file.
             Forbid	                                    - 403 - Cấm truy cập.
             Page	                                    - 200 - Trả về nội dung trang Razor.
             NotFound	                                - 404 - Không thấy
             Partial	                                - 200 - Trả về nội dung từ một Partial Page
             RedirectToPagePermanent                    - 301 - Chuyển hướng
             RedirectToPage                             - 302 - Chuyển hướng
             RedirectToPagePreserveMethod               - 307 - Chuyển hướng
             RedirectToPagePreserveMethodPermanent      - 308 - Chuyển hướng
             */
        }

        /*
        Các handler xây dụng có thể trả về "void", "IActionResult"
        VD: Xây dụng 1 Handler, khi handler thi hành nạp toàn bộ sản phẩm trong ProductService
        */
        public IActionResult OnGetLoadAll()
        {
            _productService.LoadProduct();
            return RedirectToPage("ProductPages");
        }

        /*
        Các handler bắt đầu bằng OnPost(). 
        VD: Để thiết lập truy cập bằng phương thức Post, Handler xóa sản phẩm theo id, trả về IActionResult 
        */
        public IActionResult OnPostDelete(int? id)
        {
            if (id != null)
            {
                product = _productService.SearchProduct(id);
                //Kiểm tra product != null thì xóa sản phẩm
                if (product != null)
                {
                    _productService.AllProduct().Remove(product);
                }
            }
            //Sau khi thi hành chuyển hướng trang "Product sản phẩm chi tiết" thì mình chuyển hướng về "Trang product", thiết lập với id = string.empty
            return RedirectToPage("ProductPages", new { id = string.Empty });
        }
    }
}
