using BookCatalog.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Data
{
    public static class DataExtensions
    {
        public static void AddDataLayer(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<DbContext,AppDbContext>(options =>
                options.UseSqlite(connectionString));
            services.AddScoped<IDataRepository, DataRepository>();
        }
    }
}
