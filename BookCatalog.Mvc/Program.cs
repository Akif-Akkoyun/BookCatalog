using BookCatalog.Mvc;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddControllersWithViews();
builder.Services.AddMvcLayer();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();