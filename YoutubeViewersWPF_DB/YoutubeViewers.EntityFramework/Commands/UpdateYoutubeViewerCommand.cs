using YoutubeViewers.Domain.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.EntityFramework.DTOs;

namespace YoutubeViewers.EntityFramework.Commands
{
    public class UpdateYoutubeViewerCommand : IUpdateYoutubeViewerCommand
    {
        private readonly YoutubeViewersDbContextFactory _dbContextFactory;
        public UpdateYoutubeViewerCommand(YoutubeViewersDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task Execute(YoutubeViewer youtubeViewer)
        {
            using (YoutubeViewersDbContext context = _dbContextFactory.Create())
            {
                YoutubeViewerDto youtubeViewerDto = new()
                {
                    Id = youtubeViewer.Id,
                    Username = youtubeViewer.Username,
                    IsSubscribed = youtubeViewer.IsSubscribed,
                    IsMember = youtubeViewer.IsMember,
                };

                context.YoutubeViewers.Update(youtubeViewerDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
