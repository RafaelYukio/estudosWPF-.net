using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YoutubeViewers.EntityFramework;

namespace YoutubeViewers.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtension
    {
        public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
        {

            hostBuilder.ConfigureServices((context, services) =>
            {
                // Singleton: apenas uma instância é criada durante toda a lifetime da aplicação
                string connectionString = context.Configuration.GetConnectionString("sqlite");
                services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                services.AddSingleton<YoutubeViewersDbContextFactory>();
            });

            return hostBuilder;
        }

    }
}
