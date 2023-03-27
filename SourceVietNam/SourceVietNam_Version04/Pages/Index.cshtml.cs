using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SourceVietNam_Version04.Pages.Shared.Components.MessagePage;
using System.ComponentModel;

namespace SourceVietNam_Version04.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        //Khai báo trả về trực tiếp 1 Partial
        //public IActionResult OnGet()
        //{
        //    //Trả về 1 Partial
        //    return Partial("_Message");     
        //}

        //Khai báo trả về trực tiếp 1 Component 
        //public IActionResult OnGet()
        //{
        //    //Trả về 1 nội dung Component bằng cách gọi phương thức ViewComponent trong các Handler
        //    return ViewComponent("ProductBox", false); //Truyền false: Xắp xếp giảm dần theo giá
        //}

        //Thực hiện thi hành khi nhấn vào input type submit
        public IActionResult OnPost()
        {
            //Lấy thông tin Username từ Input với name = "username" 
            var username = this.Request.Form["username"];

            //Dữ liệu truyền cho MessagePage, đối tượng lớp Message trong MessagePage
            var message = new MessagePage.Message();
            
            //Thiết lập title - Nội dung thông báo - Thời gian chờ - Chuyển hướng đến trang nào
            message.title = "Thông báo";
            message.htmlcontent = $"Cảm ơn bạn {username} đã gửi thông tin";
            message.secondwait = 3;
            //message.urlredirect = "/"; //Chuyển đến trang chủ bằng phương thức Get
            message.urlredirect = Url.Page("Privacy");

            //Trả về Component MessagePage
            return ViewComponent("MessagePage", message);
        }
    }
}