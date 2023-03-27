using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceVietNam_Version_09New.Model
{
    //Khai báo Model AppUser để sau này có thể bổ sung AppUser: Địa chỉ nhà riêng, STD cơ quan,.... 
    public class AppUser: IdentityUser
    {
        /*
         ThuHuong Note
         Khai báo thêm các thuộc tính ngoài thuộc tính UserName, Email, ... được cung cấp sẵn bởi IdentityUser 
         Ngoài lớp IdentityUser, các lớp khác cũng được sử dụng ASP.NET Core: 
         + IdentityRole
         + IdentityUserRole
         + IdentityUserClaim
         + IdentittyUserLogin
         + IdentityUserToken 
         + IdentityRoleClaim
         Khi cập nhật các trường dữ liệu mới thì cập nhật App Model của User. Sau đó tiến hành tạo Migration để tiến hành cập nhật 
         PM> Add-Migration UpdateUser : Cập nhật
         PM> Update-Database
        */

        //Thêm Địa chỉ nhà riêng của User
        [Column(TypeName = "nvarchar")]//Thông tin type lưu xuống CSDL là nvarchar
        [StringLength(400)]//Thiết lập chiều dài 400 kí tự
        public string? HomeAddress { get; set; }


    }
}
