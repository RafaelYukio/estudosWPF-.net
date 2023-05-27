
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YoutubeViewers.EntityFramework
{
    public class YoutubeViewersDesignDbContextFactory : IDesignTimeDbContextFactory<YoutubeViewersDbContext>
    {
        public YoutubeViewersDbContext CreateDbContext(string[] args = null)
        {
            return new YoutubeViewersDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=YoutubeViewersDb").Options);
        }
    }
}
