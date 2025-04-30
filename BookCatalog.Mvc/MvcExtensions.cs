using Serilog;
namespace BookCatalog.Mvc
{
    public static class MvcExtensions
    {
        public static void AddMvcLayer(this IServiceCollection services)
        {
            services.AddHttpClient("api", c =>
            {
                c.BaseAddress = new Uri("https://localhost:7257/");
            });
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/all-logs.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(le => 
                le.MessageTemplate.Text.Contains("Yeni kitap eklendi") ||
                le.MessageTemplate.Text.Contains("Kitap silindi") ||
                le.MessageTemplate.Text.Contains("Kitap güncellendi") ||
                le.MessageTemplate.Text.Contains("Kitap listesi görüntülendi.") ||
                le.MessageTemplate.Text.Contains("Kitap detayları getirildi") ||
                le.MessageTemplate.Text.Contains("Geçersiz model verisi gönderildi.") ||
                le.MessageTemplate.Text.Contains("Kitap ekleme sırasında hata oluştu") ||
                le.MessageTemplate.Text.Contains("Kitap güncellenirken hata oluştu") ||
                le.MessageTemplate.Text.Contains("Kitap silinirken hata oluştu") ||
                le.MessageTemplate.Text.Contains("Geçersiz kitap ID gönderildi") ||
                le.MessageTemplate.Text.Contains("Kitap detayları alınamadı") ||
                le.MessageTemplate.Text.Contains("Kitap Detayları Alınırken Hata Oluştu"))
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day))
                .CreateLogger();
        }
    }
}
