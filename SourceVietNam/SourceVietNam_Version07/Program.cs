/* MODEL BINDING
Model Binding: Cơ chế liên kết dữ liệu. Các dữ liệu xuất phát từ ứng dụng đều có nhiều nguồn
Dữ liệu này đều lưu trong cấu trúc mà có thể truy cập được dưới dạng key - value 
Nguồn dữ liệu gửi đến ứng dụng bao gồm: 
1. Dữ liệu từ Form HTML gửi đến, khi submit các form HTML bằng phương thức POST dữ liệu
2. Trên các chuỗi query Url, lấy giá trị từ query khi nhập trực tiếp từ Url hoăc do submit form từ phương thức GET 
3. Từ trong Header, trong HTTP request gửi đến, nguồn dữ liệu xác định trong các Route, Route Data, và Value 
VD: Trang Product Page có thiết lập các tham số của Route: @page "/Product/{id:int?}", có ID thì có thể đọc được giá trị dữ liệu key là "id" 
4. Nguồn dữ liệu từ trong việc Upload file, tất cả dữ liệu này khi gửi đến ứng dụng được thông qua ứng dụng HTTP Request. 
5. Dữ liệu được gửi đến từ Body HTTP Request. VD: submit dữ liệu nó là json nằm trong các Http Request 
Để đọc dữ liệu của đối tượng thông qua HTTP Request, đối tượng này có ở trong các Controller của PageModel, View, file cshtml
Khi truy cập vào HTTP Request, sẽ truy cập vào, những thuộc tính, đọc được những nguồn dữ liệu tương ứng
VD: Đọc dữ liệu từ Form HTML (post): HttpRequest.Form["key"]
    Đọc dữ liệu từ Query (form HTML Get): HttpRequest.Query["key"]
    Đọc dữ liệu từ các Header: HttpRequest.Header["key"]
    Đọc dữ liệu từ Row Data: HttpRequest.RouteValues["key"]
    Đọc dữ liệu file Uplaod (Xem sau)
    Đọc dữ liệu được gửi đến từ Body HTTP Request (Xem sau) 
Cơ bản có những phương thức đọc dữ liệu trực tiếp từ các nguồn gửi đến
VD: Khi truy cập vào trang Product, tại handler OnGet(), có thể đọc trực tiếp dữ liệu được gửi đến 
 */

using SourceVietNam_Version07.Services;

var builder = WebApplication.CreateBuilder(args);

//Khai báo 1 dịch vụ như là 1 dịch vụ của hệ thống 
builder.Services.AddSingleton<ProductService>();

// Add services to the container.
builder.Services.AddRazorPages();

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
