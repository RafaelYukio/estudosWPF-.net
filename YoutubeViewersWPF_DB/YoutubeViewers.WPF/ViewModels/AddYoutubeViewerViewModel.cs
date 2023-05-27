using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class AddYoutubeViewerViewModel : ViewModelBase
    {
        public YoutubeViewerDetailsFormViewModel YoutubeViewerDetailsFormViewModel { get; }

        public AddYoutubeViewerViewModel(YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new AddYoutubeViewerCommand(this, youtubeViewersStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            YoutubeViewerDetailsFormViewModel = new YoutubeViewerDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
