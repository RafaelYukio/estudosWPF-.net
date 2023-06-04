using YoutubeViewers.Domain.Commands;
using YoutubeViewers.EntityFramework.DTOs;

namespace YoutubeViewers.EntityFramework.Commands
{
    public class DeleteYoutubeViewerCommand : IDeleteYoutubeViewerCommand
    {
        private readonly YoutubeViewersDbContextFactory _dbContextFactory;
        public DeleteYoutubeViewerCommand(YoutubeViewersDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task Execute(Guid youtubeViewerId)
        {
            //throw new Exception();

            using (YoutubeViewersDbContext context = _dbContextFactory.Create())
            {
                // No EF criamos a entidade do banco apenas com o ID e chamamos o Remove
                YoutubeViewerDto youtubeViewerDto = new()
                {
                    Id = youtubeViewerId,
                };

                await Task.Delay(2000);

                context.YoutubeViewers.Remove(youtubeViewerDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
