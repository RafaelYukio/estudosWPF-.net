using YoutubeViewers.Domain.Models;

namespace YoutubeViewers.Domain.Commands
{
    public interface IUpdateYoutubeViewerCommand
    {
        Task Execute(YoutubeViewer youtubeViewer);
    }
}
