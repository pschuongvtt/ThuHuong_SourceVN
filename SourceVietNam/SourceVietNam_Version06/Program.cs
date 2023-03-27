using SourceVietNam_Version06.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SourceVietNam_Version06.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SourceVietNam_Version06ContextConnection") ?? throw new InvalidOperationException("Connection string 'SourceVietNam_Version06ContextConnection' not found.");

builder.Services.AddDbContext<SourceVietNam_Version06Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SourceVietNam_Version06Context>();

// Add services to the container.
builder.Services.AddRazorPages();

//Khai báo 1 dịch vụ như là 1 dịch vụ của hệ thống 
builder.Services.AddSingleton<ProductService>();

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
