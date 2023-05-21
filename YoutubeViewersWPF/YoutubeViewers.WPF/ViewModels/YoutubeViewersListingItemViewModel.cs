using System.Windows.Input;
using YoutubeViewers.WPF.Models;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YoutubeViewersListingItemViewModel : ViewModelBase
    {
        public YoutubeViewer YoutubeViewer;

        public string Username => YoutubeViewer.Username;

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public YoutubeViewersListingItemViewModel(YoutubeViewer youtubeViewer, ICommand editCommand)
        {
            YoutubeViewer = youtubeViewer;
            EditCommand = editCommand;
        }
    }
}
