using BookCatalog.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(le =>
            le.MessageTemplate.Text.Contains("Kitap detaylarý getirildi") ||
            le.MessageTemplate.Text.Contains("Yeni kitap oluþturuldu") ||
            le.MessageTemplate.Text.Contains("Kitap silindi") ||
            le.MessageTemplate.Text.Contains("Kitap güncellendi"))
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day))
    .CreateLogger();
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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    if (await context.Database.EnsureCreatedAsync())
    {
        await DataSeed.SeedData(context);
    }
}
app.Run();