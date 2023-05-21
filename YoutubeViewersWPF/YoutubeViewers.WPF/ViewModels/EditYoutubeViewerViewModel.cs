using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.WPF.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class EditYoutubeViewerViewModel : ViewModelBase
    {
        public YoutubeViewerDetailsFormViewModel YoutubeViewerDetailsFormViewModel { get; }

        public EditYoutubeViewerViewModel(YoutubeViewer youtubeViewer, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new EditYoutubeViewerCommand(modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            YoutubeViewerDetailsFormViewModel = new YoutubeViewerDetailsFormViewModel(submitCommand, cancelCommand)
            {
                Username = youtubeViewer.Username,
                IsSubscribed = youtubeViewer.IsSubscribed,
                IsMember = youtubeViewer.IsMember
            };
        }
    }
}
