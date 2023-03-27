using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace SourceVietNam_Version05.Pages
{
    public class IndexModel : PageModel
    {
        //Khai báo thuộc tính 

        [DisplayName("Tên Người dùng")]
        public string UserName { get; set; } = "ThuHuong"; //Khởi tạo giá trị ban đầu cho Input là "ThuHuong" 

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}