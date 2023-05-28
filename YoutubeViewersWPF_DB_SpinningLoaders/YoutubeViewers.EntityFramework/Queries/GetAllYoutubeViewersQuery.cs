using Microsoft.EntityFrameworkCore;
using System.Data;
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
            // Testar mensagem de erro no Load:
            //throw new Exception();

            // using para dar Dispose quando acabar de executar
            using (YoutubeViewersDbContext context = _dbContextFactory.Create())
            {
                // Para testar os loaders, há um delay antes da requisiçãp
                await Task.Delay(3000);

                IEnumerable<YoutubeViewerDto> youtubeViewersDtos = await context.YoutubeViewers.ToListAsync();
                return youtubeViewersDtos.Select(y => new YoutubeViewer(y.Id, y.Username, y.IsSubscribed, y.IsMember));
            }
        }
    }
}
