using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

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
//builder.Services.AddDbContext<MyBlogContext>(option =>
//{
//    //Đọc chuỗi kết nối từ file cấu hình của ứng dụng
//    string connectstring = builder.Configuration.GetConnectionString("MyBlogDbContext");
//    //Gọi dịch vụ SQLServer
//    option.UseSqlServer(connectstring);
//});

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
