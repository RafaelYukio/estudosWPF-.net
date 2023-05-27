using Microsoft.EntityFrameworkCore;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.Domain.Queries;
using YoutubeViewers.EntityFramework.DTOs;

namespace YoutubeViewers.EntityFramework.Queries
{
    public class GetAllYoutubeViewersQuery : IGetAllYoutubeViewersQuery
    {
        private readonly YoutubeViewersDbContextFactory _dbContextFactory;
        public GetAllYoutubeViewersQuery(YoutubeViewersDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<YoutubeViewer>> Execute()
        {
            // using para dar Dispose quando acabar de executar
            using (YoutubeViewersDbContext context = _dbContextFactory.Create())
            {
                IEnumerable<YoutubeViewerDto> youtubeViewersDtos = await context.YoutubeViewers.ToListAsync();
                return youtubeViewersDtos.Select(y => new YoutubeViewer(y.Id, y.Username, y.IsSubscribed, y.IsMember));
            }
        }
    }
}
