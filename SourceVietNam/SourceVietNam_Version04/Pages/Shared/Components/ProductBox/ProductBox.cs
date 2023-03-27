using Microsoft.AspNetCore.Mvc;
using SourceVietNam_Version04.Model;
using SourceVietNam_Version04.Services;

namespace SourceVietNam_Version04.Pages.Shared.Components.ProductBox
{
    //[ViewComponent]
    public class ProductBox : ViewComponent
    {
        /*
         1. Để sử dụng class này là 1 component phải khai báo 1 phương thức có tên Invoke() hoặc InvokeAsync()
         2. Nếu phương thức Invoke có tham số: Invoke(object model)
         3. Mặc dù có khai báo phương thức "Invoke()" nhưng vẫn chưa sử dụng được
            -->C1: Phải khai báo cho lớp thêm 1 Attribute: "[ViewComponent]"
            -->C2: Phải khai báo tên class có hậu tố ViewComponent: public class ProductBoxViewComponent{...}
            -->C3: Khai báo lớp đó kế thừa từ ViewComponent: public class ProductBox: ViewComponent {...}
         Lưu ý: Phương thức Invoke() phải trả về 1 trong 2 kiểu dữ liệu: "string" - "IViewComponentResult". Nếu trả về kiểu dữ liệu khác nó sẽ phát sinh lỗi System
         Thông thường, phương thức Invoke() hoặc InvokeAsync() trả về IViewComponentResult là View(). 
         C1: Đối với phương thức View(), nó thi hành 1 file .cshtml 1 View, mặc định không thiết lập tham số gì, thì View thiết lập có tên mặc định "Default", mặc định thi hành View Default.cshtml
         Có thể tự định nghĩa file "Default.cshtml", khai báo như 1 view bình thường
         C2: Điền thông tin View được trả về:  return View("<TenFileDuocTraVe>");
         4. Khi gọi phương thức View(), có thể thiết lập Model truyền sang View Default.cshtml theo cú pháp 
            return View<KieuDuLieu>(<GiaTri>)
            Sau đó, qua View Default.cshtml gọi cú pháp: @model <KieuDuLieu>
            Để xuất nội dung của Model gọi : @Model
         5. Lợi ích của việc sửa dụng Component tạo ra phương thức khởi tạo của nó. 
            Phương thức khởi tạo này có thể nhận những dịch vụ DI, sẽ được hệ thống tự động Inject vào 
        
        */

        //C1:
        //public string Invoke()
        //{
        //    //Trả về chuỗi 
        //    return "Nội dung của ProductBox";
        //}

        //C2:
        //public IViewComponentResult Invoke()
        //{
        //    //Trả về View 
        //    //return View(); //Mặc định thi hành View Default.cshtml
        //    //return View("Default01"); //Điền thông tin View trả về là Default.cshtml
        //    //return View<string>("Thiết lập Model truyền sang View"); //thiết lập Model truyền sang View Default.cshtml

        //    /*
        //    Thiết kế Default.cshtml hiển thị danh sách 1 List tên những sản phẩm cà kèm theo giá các sản phẩm 
        //    THiết kế Model truyền sang cho View 1 list các Product, thiết lập Model có kiểu là: List<Product>() 
             
        //    */
        //    var lsProduct = new List<Product>()
        //    {
        //    new Product(){Name = "SP 1", Description = "Mô tả thông tin SP1", Price = 20000000},
        //    new Product(){Name = "SP 2", Description = "Mô tả thông tin SP2", Price = 30000000},
        //    };
        //    return View <List<Product>>(lsProduct);
        //}

        //C3: Có thể truyền thêm 1 tham số bất kỳ 
        //public IViewComponentResult Invoke(bool sapxeptang = true) //sapxeptang = false: Mặc định không truyền là true
        //{
        //    /*
        //    Biến sapxeptang = true, sắp xếp danh sách các sản phẩm giảm dần theo giá, ngược lại tăng dần theo giá
        //    Thiết kế Default.cshtml hiển thị danh sách 1 List tên những sản phẩm cà kèm theo giá các sản phẩm 
        //    THiết kế Model truyền sang cho View 1 list các Product, thiết lập Model có kiểu là: List<Product>()             
        //    */
        //    var lsProduct = new List<Product>()
        //    {
        //    new Product(){Name = "SP 1", Description = "Mô tả thông tin SP1", Price = 20000000},
        //    new Product(){Name = "SP 2", Description = "Mô tả thông tin SP2", Price = 30000000},
        //    };
        //    lsProduct = sapxeptang == true ? lsProduct.OrderBy(x => x.Price).ToList() : lsProduct.OrderByDescending(x => x.Price).ToList();
        //    return View<List<Product>>(lsProduct);
        //}

        /*1. Tạo ra dịch vụ: Cung cấp danh sách tên những sản phẩm" ProductListService.cs
          2. Đăng ký dịch vụ lấy danh sách các sản phẩm: ProductListService trong Program.cs
          builder.Services.AddSingleton<ProductListService>();
          3. Trong phương thức khởi tạo, khai báo có 1 dịch vụ ProductListService, thì ProductListService sẽ được hệ thống tự động Inject vào 
         */
        ProductListService productListService; //Khai báo biến kiểu dữ liệu ProductListService
        //Hàm khởi tạo 
        public ProductBox(ProductListService _productListService)
        {
            //Khi khởi tạo lưu dịch vụ _productListService
            productListService = _productListService;
        }
        //Sau đó gọi dịch vụ productListService này ra để sử dụng 
        public IViewComponentResult Invoke(bool sapxeptang = true) //sapxeptang = false: Mặc định không truyền là true
        {
            /*
            Biến sapxeptang = true, sắp xếp danh sách các sản phẩm giảm dần theo giá, ngược lại tăng dần theo giá
            Thiết kế Default.cshtml hiển thị danh sách 1 List tên những sản phẩm cà kèm theo giá các sản phẩm 
            Thiết kế Model truyền sang cho View 1 list các Product, thiết lập Model có kiểu là: ProductListService
            + Gọi dịch vụ từ productListService đươc khai báo trong hàm khởi tạo, truy cập vào productListService.lsProduct sắp xếp tăng dần
            */
            productListService.lsProduct = sapxeptang == true ? productListService.lsProduct.OrderBy(x => x.Price).ToList() : productListService.lsProduct.OrderByDescending(x => x.Price).ToList();
            return View<List<Product>>(productListService.lsProduct);
            
        }
    }
}
