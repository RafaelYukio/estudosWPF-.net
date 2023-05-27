using System;
using System.Threading.Tasks;
using YoutubeViewers.WPF.Models;

namespace YoutubeViewers.WPF.Stores
{
    public class YoutubeViewersStore
    {
        public event Action<YoutubeViewer> YoutubeViewerAdded;
        public event Action<YoutubeViewer> YoutubeViewerUpdated;

        public async Task Add(YoutubeViewer youtubeViewer)
        {
            YoutubeViewerAdded?.Invoke(youtubeViewer);
        }

        public async Task Update(YoutubeViewer youtubeViewer)
        {
            YoutubeViewerUpdated?.Invoke(youtubeViewer);
        }
    }
}
