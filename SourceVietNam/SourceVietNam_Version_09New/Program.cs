using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SourceVietNam_Version_09New.Model;
using SourceVietNam_Version_09New.Service;
using System.Configuration;

//Khởi tạo 1 đối tượng builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

/*Tiến hành đăng ký MyBlogDbContext như 1 dịch vụ của ứng dụng
 AddDbContext: Do có tích hợp EntityFramework
 <MyBlogDbContext>: Dịch vụ dký là MyBlogDbContext
 option: Tham số là 1 option DelegateBuilder. 
 Trong Delegate sử dụng đối tượng options là DBContextOptionBuilder để thiết lập cho dịch vụ MyBlogDbContext
 + Thiết lập 1: Chuỗi kết nối tạo ra dịch vụ MyBlogDbContext
 + Thiết lập 2: Gọi dịch vụ SQLServer
 Trên hệ thống có 1 dịch vụ MyBlogDbContext: Là 1 DBContext của EF, có thể inject vào trong các đối tượng của Controller, PageModel, View 
 Và có thể sử dụng nó để tương tác vs SQLServer. Nó báo lỗi do chưa có CSDL nào cả, để làm việc vs SQL Server thì phải có CSDL
 --> Sử dụng kỹ thuật Migration để tạo ra CSDL đúng như khai báo trong MyBlogDbContext
 Tải gói pagekage: Microsoft.EntityFrameworkCore.Tools
 PM> dotnet ef migration list : Kiểm tra xem có migration nào được tạo ra hay không 
 PM> Add-Migration Initial: Tạo ra migration
 PM> Update-Database: Để sử dụng Migration tên Initial (DB) thì gọi lệnh tạo ra cấu trúc CSDL
 (Lưu ý: Trong ConnectionString phải có tham số TrustServerCertificate=True 
 */
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    //Đọc chuỗi kết nối từ file cấu hình của ứng dụng
    string connectstring = builder.Configuration.GetConnectionString("ApplicationDbContext");
    //Gọi dịch vụ SQLServer
    option.UseSqlServer(connectstring);
});

/*AddIdentity: Đăng ký dịch vụ của Identity
  AppUser: Tương ứng với Model 
  IdentityRole: Model tương ứng Role của Identity 
  AddEntityFrameworkStores: Dùng làm việc với cơ sở dữ liệu
  MyBlogContext: Tên của DbContext sử dụng
  1. Khi ứng dụng này hoạt động, truy cập địa chỉ theo Url để đăng nhập: /Identity/Account/Login 
  2. Khi đăng nhập rồi có thể truy cập để quản lý tài khoản: /Identity/Account/Manage
  Tuy nhiên vẫn chưa truy cập được vào mục 1, mục 2 theo địa chỉ Url. 
  Vì Identity cung cấp cấu trúc Logic, dịch vụ làm việc với bảng lưu trữ thông tin về User
  Nếu muốn sử dụng những trang giao diện mặc định, những trang Razor, thì phải tích hợp vào ứng dụng package "Microsoft.AspNetCore.Identity.UI" 
  Sử dụng giao diện mặc định trước, sau đó phát sinh code theo giao diện mặc định 
  --> Tạm thời chuyển dịch vụ "AddIdentity" thành "AddDefaultIdentity" và không dùng "IdentityRole".
  Bản chất đều giống nhau nhưng AddIdentity có sử dụng package Identity.UI cho những trang "LogIn", "Logout",...
  Sau đó Run nếu báo lỗi thiếu layout /Areas/Identity/Pages/Account/_LoginPartial.cshtml thì thêm _Layout vào
 */
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//builder.Services.AddDefaultIdentity<AppUser>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//Thiết lập thêm cho các dịch vụ Identity, thay đổi cấu hình mặc định của Identity thì thay đổi dịch vụ
builder.Services.Configure<IdentityOptions>(options => {
    // Thiết lập về Password
    options.Password.RequireDigit = true; // Bắt phải có số
    options.Password.RequireLowercase = true; // Bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Bắt ký tự đặc biệt
    options.Password.RequireUppercase = true; //Bắt buộc chữ in
    options.Password.RequiredLength = 3; //Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 0; //Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lần thì khóa 5 phút
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = //Các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  //Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = false; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại
});

/*
Sau khi Logout, không Login lại được do sau khi hoàn thành đăng ký thì tự động LogIn. Lần LogIn tiếp theo phải xác thực email mới đăng nhập được (Program.cs): options.SignIn.RequireConfirmedEmail = true;
Trong ứng dụng đăng ký dịch vụ gửi Email - EmailSender: Install Pakage: MailKit và MimeKit
Đăng ký dịch vụ MailSettings
 */
var mailSetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSetting);

/*Đăng ký dịch vụ SenMailService.cs. 
  AddSingleton: Dịch vụ tạo 1 lần tồn tại suốt quá trình hoạt động của ứng dụng 
  (Có thể thay thế dịch vụ .Services.AddTransient: môi khi truy vấn nó Transient send email service)
  Db thay đổi thông tin 2 colums sau khi xác nhận Enail: EmailConfirmed và ConcurrencyStamp
*/
builder.Services.AddSingleton<IEmailSender, SendMailService>();

//Xây dựng app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//Thiết lập cấu hình cho Endpoint Routing
app.UseRouting();

//Phục hồi thông tin đã đăng nhập, đã xác thực
app.UseAuthentication();

//Phục hồi thông tin vế quyền của User. Dòng lệnh cung cấp chức năng xác thực định quyền, chức năng lưu thông tin người dùng truy cập , thông tin lưu tại HttpContext.User
app.UseAuthorization();

/*
 Khi thiết lập thông tin Endpoint MapRazorPages thì nó tìm trên toàn bộ mã nguồn những trang RazorPage cshtml và nó sử dụng trang này như 1 Endpoint
 Vd: FirstPage.cshtml -> Tạo ra 1 Endpoint với URL: "/Login/FirstPage", "/Login/SecondPage",...
 */
app.MapRazorPages();

//Thực hiện run app
app.Run();
