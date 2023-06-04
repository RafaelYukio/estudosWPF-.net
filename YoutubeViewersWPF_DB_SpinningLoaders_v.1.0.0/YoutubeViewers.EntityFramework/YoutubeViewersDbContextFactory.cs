using Microsoft.EntityFrameworkCore;

namespace YoutubeViewers.EntityFramework
{
    public class YoutubeViewersDbContextFactory
    {
        private readonly DbContextOptions _options;

        public YoutubeViewersDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        // É necessário criar um novo DbContext a cada query, pois DbContext não é thread safe (pesquisar sobre)
        // então não podemos reusar a mesma instância DbContext
        // Usar a mesma instância pode causa concurrency (multiple threads trying to execute some sequel in the same instance)
        public YoutubeViewersDbContext Create()
        {
            return new YoutubeViewersDbContext(_options);
        }
    }
}
