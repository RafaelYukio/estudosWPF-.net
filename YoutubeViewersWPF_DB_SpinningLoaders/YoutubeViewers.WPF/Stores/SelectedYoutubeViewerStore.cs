using System;
using System.Linq;
using YoutubeViewers.Domain.Models;

namespace YoutubeViewers.WPF.Stores
{
    public class SelectedYoutubeViewerStore
    {
        private readonly YoutubeViewersStore _youtubeViewersStore;

        private YoutubeViewer _selectedYoutubeViewer;
        public YoutubeViewer SelectedYoutubeViewer
        {
            get
            {
                return _selectedYoutubeViewer;
            }
            set
            {
                _selectedYoutubeViewer = value;
                SelectedYoutubeViewerChanged?.Invoke();
            }
        }

        // Evento para informar que a seleção mudou
        public event Action SelectedYoutubeViewerChanged;

        public SelectedYoutubeViewerStore(YoutubeViewersStore youtubeViewersStore)
        {
            _youtubeViewersStore = youtubeViewersStore;

            _youtubeViewersStore.YoutubeViewerUpdated += YoutubeViewersStore_YoutubeViewerUpdated;
            _youtubeViewersStore.YoutubeViewerAdded += YoutubeViewersStore_YoutubeViewerAdded;
        }

        private void YoutubeViewersStore_YoutubeViewerAdded(YoutubeViewer youtubeViewer)
        {
            SelectedYoutubeViewer = youtubeViewer;
        }

        private void YoutubeViewersStore_YoutubeViewerUpdated(YoutubeViewer youtubeViewer)
        {
            if(youtubeViewer.Id == SelectedYoutubeViewer?.Id)
            {
                SelectedYoutubeViewer = youtubeViewer;
            }
        }
    }

}
