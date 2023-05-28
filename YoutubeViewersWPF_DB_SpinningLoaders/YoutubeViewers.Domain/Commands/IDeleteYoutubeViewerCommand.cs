namespace YoutubeViewers.Domain.Commands
{
    public interface IDeleteYoutubeViewerCommand
    {
        Task Execute(Guid youtubeViewerId);
    }
}
