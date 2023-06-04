using YoutubeViewers.Domain.Models;

namespace YoutubeViewers.Domain.Commands
{
    public interface ICreateYoutubeViewerCommand
    {
        Task Execute(YoutubeViewer youtubeViewer);
    }
}
