using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace SourceVietNam.Pages.Login
{
    /*Việc khai báo trong file cshtml thuộc tính, phương thức trong .cshtml bằng @functions{..} chỉ sử dụng các hàm đơn giản
     Còn thông thường được phân chia ra
     + View: .cshtml, tại đó sử dụng code HTML và 1 phần code C# phát sinh ra mã HTML
     + Controller / function / Back Model: Được trình bày trong 1 file khác cshtml.cs xử lý các vấn đề logic, code C#
     Lớp cshtml.cs phải được kế thừa từ lớp "PageModel". Trong lớp này sẽ được khai báo các thuộc tính, phương thức thay vì khia báo trong khối @functions{..}
     */
    public class FirstPageModel : PageModel
    {
        /*
        Khai báo phương thức với :
        + Phạm vi truy cập là public
        + Trả về kiểu gì đó
        + Tên phương thức bắt đầu bằng OnGet, OnGetABC, OnGetXyz,... ,OnPost(), OnPostXYZ(),...
        -> Các phương thức này gọi là "Handler"
        Trong đó hàm OnGet tự động được gọi khi truy vấn đến trang bằng địa chỉ Url @page "/TrangThu1/{solanlap-key:int?}" và truy vấn bằng phương thức Get"
        Phương thức Get được chạy đầu tiên 
         */

        public void OnGet()
        {
            Console.WriteLine("Truy cập phương thức Get trang FirsrPage");
            ViewData["MyData"] = "Thu Hương - Dùng Hàm OnGet()";
        }
        /*
        Tương tự như vậy tạo ra 1 Handler khác, cũng truy cập bằng phương thức Get, truy cập bằng phương thức ABC
        Cấu trúc: GET, Url?handler=abc
        + Truy cập bằng phương thức Get như bình thường đến địa chỉ URL mặc định và có thêm chuỗi tham số mặc định thì phương thức được gọi
        --> KQ được gọi: https://localhost:7145/trangthu1?handler=abc
        */
        public void OnGetABC()
        {
            Console.WriteLine("Truy cập phương thức Get trang FirsrPage");
            ViewData["MyData"] = "Thu Hương - Dùng Hàm OnGetABC()";
        }

        /*Khai báo biến được sử dụng cho lớp View .cshtml*/
        public string Content = "Nội dung Content - Thông tin lấy từ FirstPageModel";

    }
}
