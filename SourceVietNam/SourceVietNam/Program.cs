var builder = WebApplication.CreateBuilder(args);

/*Phương thức add các Services Razor đã được đăng ký vào hệ thống
  Mặc định các điểm Endpoint Routing theo đúng cấu trúc UsePages
  Trong trường hợp muốn đổi thư mục Pages này, thì cấu hình thông tin .AddRazorPagesOptions
  + Bên trong khai báo options - delegate để sử dụng các phương thức cấu hình thêm cho các trang RazorPages
*/
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    //Muốn đổi thư mục mặc định trong các trang lưu trữ của RazorPages 
    options.RootDirectory = "/Pages";
    /* Muốn đổi đường dẫn truy cập Url
     C1: Sử dụng cấu hình viết lại địa chỉ Url truy cập đến RazorPage - RewriteUrl
     C2: vào trang cshtml sử dụng "@page "URL""
     */
    //options.Conventions.AddPageRoute("/Login/FirstPage", "/FirstPage.html");
});

/*Mặc định, khi phát sinh ra địa chỉ Url dựa theo tên @page của RazorPage
 --> Muốn chuyển thông tin tất cả các địa chỉ Url thành chữ thường
*/
builder.Services.Configure<RouteOptions>(routeOptions =>
{
    //Chuyển tất cả các url thành chữ in thường
    routeOptions.LowercaseUrls = true;
});

//Khởi tạo 
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

app.UseAuthorization();

/*
 Khi thiết lập thông tin Endpoint MapRazorPages thì nó tìm trên toàn bộ mã nguồn những trang RazorPage cshtml và nó sử dụng trang này như 1 Endpoint
 Vd: FirstPage.cshtml -> Tạo ra 1 Endpoint với URL: "/Login/FirstPage", "/Login/SecondPage",...
 */
app.MapRazorPages();

//Khởi tạo app
app.Run();
