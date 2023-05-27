using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.Domain.Queries;
using YoutubeViewers.EntityFramework.Commands;

namespace YoutubeViewers.WPF.Stores
{
    public class YoutubeViewersStore
    {
        // Referenciando a Interface, não a classe, assim não ficamos acoplados ao EF
        private readonly ICreateYoutubeViewerCommand _createYoutubeViewerCommand;
        private readonly IUpdateYoutubeViewerCommand _updateYoutubeViewerCommand;
        private readonly IDeleteYoutubeViewerCommand _deleteYoutubeViewerCommand;
        private readonly IGetAllYoutubeViewersQuery _getAllYoutubeViewersQuery;

        // É uma lista para poder adicionar e excluir
        private readonly List<YoutubeViewer> _youtubeViewers; 
        public IEnumerable<YoutubeViewer> YoutubeViewers => _youtubeViewers;

        public YoutubeViewersStore(ICreateYoutubeViewerCommand createYoutubeViewerCommand,
                                   IUpdateYoutubeViewerCommand updateYoutubeViewerCommand,
                                   IDeleteYoutubeViewerCommand deleteYoutubeViewerCommand,
                                   IGetAllYoutubeViewersQuery getAllYoutubeViewersQuery)
        {
            _createYoutubeViewerCommand = createYoutubeViewerCommand;
            _updateYoutubeViewerCommand = updateYoutubeViewerCommand;
            _deleteYoutubeViewerCommand = deleteYoutubeViewerCommand;
            _getAllYoutubeViewersQuery = getAllYoutubeViewersQuery;

            _youtubeViewers = new List<YoutubeViewer>();
        }
        
        // Eventos para que as ViewModels possam ver e atualizar a UI
        // O tipo da Action é o objeto que pode ser obtido por quem está vendo o evento
        // Como temos um propriedade para a lista de YoutubeViewers (loadaded), então não é necessário passar no evento
        public event Action YoutubeViewersLoaded;
        public event Action<YoutubeViewer> YoutubeViewerAdded;
        public event Action<YoutubeViewer> YoutubeViewerUpdated;
        public event Action<Guid> YoutubeViewerDeleted;

        public async Task Load()
        {
            IEnumerable<YoutubeViewer> youtubeViewers = await _getAllYoutubeViewersQuery.Execute();

            _youtubeViewers.Clear();
            _youtubeViewers.AddRange(youtubeViewers);

            YoutubeViewersLoaded?.Invoke();
        }

        public async Task Add(YoutubeViewer youtubeViewer)
        {
            await _createYoutubeViewerCommand.Execute(youtubeViewer);
            // Quando invocamos, passamos o objeto que queremos que peguem do outro lado
            _youtubeViewers.Add(youtubeViewer);
            YoutubeViewerAdded?.Invoke(youtubeViewer);
        }

        public async Task Update(YoutubeViewer youtubeViewer)
        {
            await _updateYoutubeViewerCommand.Execute(youtubeViewer);

            int index = _youtubeViewers.FindIndex(y => y.Id == youtubeViewer.Id);
            if(index != -1)
            {
                _youtubeViewers[index] = youtubeViewer;
            }
            YoutubeViewerUpdated?.Invoke(youtubeViewer);
        }

        public async Task Delete(Guid youtubeViewerId)
        {
            await _deleteYoutubeViewerCommand.Execute(youtubeViewerId);
            // É para ter apenas um Id igual, porém usamos o RemoveAll, para garantir
            _youtubeViewers.RemoveAll(y => y.Id == youtubeViewerId);

            YoutubeViewerDeleted.Invoke(youtubeViewerId);
        }
    }
}
