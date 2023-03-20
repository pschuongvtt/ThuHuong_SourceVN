using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SourceVietNam_Version06.Pages
{
    public class ContactRequestModel : PageModel
    {
        public string chuoi { get; set; }
        //Khi hàm chạy khởi tạo 
        public void OnGet()
        {
            Console.WriteLine("Init Contact...");
        }

        /*
         Khi khai báo lớp PageModel thì có thể Inject các dịch vụ của lớp PageModel
         Thông thường, thì có thể Inject vào các đối tượng Loger. 
         Mục dích để lưu đối tượng này lại chỉ đọc, khi nó Inject vào thì lưu nó lại.
         Lúc này có thể sử dụng _logger để truy xuất dữ liệu thay vì in Console.WriteLine("Init Contact...") có thể đọc trong log của ứng dụng
         Phát sinh trang RazorPage theo 1 cấu trúc chuẩn ta dùng: 
        PM> dot net new page - h: Xem thông tin help tạo cấu trúc RazorPage
        -o: Tên của thư mục lưu trữ file phát sinh 
        -n: Tên của RazorPage . Chứ k mặc định nó trỏ tới namespace "MyApp.Namespace"
        -na: Thiết lập namespace tạo ra Razor pages
        VD: Thiết lập 1 trang RazorPage "Product" để hiển thị danh sách các sản phẩm 
        PM> dotnet new page -n ProductPage -o Pages -na SourceVietNam_Version06.Pages.ProductstModel
        */
        private ILogger<ContactRequestModel> _logger;
        public ContactRequestModel(ILogger<ContactRequestModel> logger)
        {
            _logger = logger; 
            _logger.LogInformation("Init Contact New...");
        } 



    }
}
