using System;
using YoutubeViewers.WPF.Models;

namespace YoutubeViewers.WPF.Stores
{
    public class SelectedYoutubeViewerStore
    {
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
    }
}
