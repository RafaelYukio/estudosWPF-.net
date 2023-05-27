using Microsoft.EntityFrameworkCore;
using YoutubeViewers.EntityFramework.DTOs;

namespace YoutubeViewers.EntityFramework
{
    public class YoutubeViewersDbContext : DbContext
    {
        public YoutubeViewersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<YoutubeViewerDto> YoutubeViewers { get; set; }
    }
}
