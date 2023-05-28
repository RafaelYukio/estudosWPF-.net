using YoutubeViewers.Domain.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.EntityFramework.DTOs;

namespace YoutubeViewers.EntityFramework.Commands
{
    // Daria para fazer uma classe abstrata base para os Commands e Query
    public class CreateYoutubeViewerCommand : ICreateYoutubeViewerCommand
    {
        private readonly YoutubeViewersDbContextFactory _dbContextFactory;
        public CreateYoutubeViewerCommand(YoutubeViewersDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task Execute(YoutubeViewer youtubeViewer)
        {
            //throw new Exception();

            using (YoutubeViewersDbContext context = _dbContextFactory.Create())
            {
                YoutubeViewerDto youtubeViewerDto = new()
                {
                    Id = youtubeViewer.Id,
                    Username = youtubeViewer.Username,
                    IsSubscribed = youtubeViewer.IsSubscribed,
                    IsMember = youtubeViewer.IsMember,
                };

                await Task.Delay(2000);

                context.YoutubeViewers.Add(youtubeViewerDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
