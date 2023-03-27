using Microsoft.AspNetCore.Mvc;

namespace SourceVietNam_Version04.Pages.Shared.Components.MessagePage
{
    public class MessagePage : ViewComponent
    {
        //Tạo ra 1 class Message để truyền thông tin vào đối tượng trong Invoke()
        public class Message
        {
            public string title { set; get; } = "Thông báo";     // Tiêu đề của Box hiện thị
            public string htmlcontent { set; get; } = "";         // Nội dung HTML hiện thị
            public string urlredirect { set; get; } = "/";        // Url chuyển hướng đến
            public int secondwait { set; get; } = 3;              // Sau secondwait giây thì chuyển
        }
        public MessagePage() { }
        public IViewComponentResult Invoke(Message message)
        {
            // Thiết lập Header tên REFRESH của HTTP Respone.Hết thời gian chờ -> chuyển hướng về trang đích Url
            this.HttpContext.Response.Headers.Add("REFRESH", $"{message.secondwait};URL={message.urlredirect}");
            //Trả về 1 View cshtml. View này được truyền Model từ message sang. Tạo ra 1 View mặc định là "Defaul.cshtml"
            return View(message);
        }
    }
}
