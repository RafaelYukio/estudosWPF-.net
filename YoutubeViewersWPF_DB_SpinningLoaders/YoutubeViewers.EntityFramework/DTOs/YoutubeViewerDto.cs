namespace YoutubeViewers.EntityFramework.DTOs
{
    // DTO apenas salvar a entidade no banco (precisa do set, para modificar o objeto e salvar no banco)
    // Neste caso o DTO é bom para adicionar qualquer prop. extra para salvar no banco,
    // sem alterar a classe principal do Domain
    public class YoutubeViewerDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsMember { get; set; }
    }
}
