using YoutubeViewers.Domain.Models;

namespace YoutubeViewers.Domain.Queries
{
    // A interface das queries ficam no Domain, porém a implementação no projeto do ORM
    // Assim podemos mudar de ORM a qualquer momento, sem quebrar a aplicação
    public interface IGetAllYoutubeViewersQuery
    {
        Task<IEnumerable<YoutubeViewer>> Execute();
    }
}
