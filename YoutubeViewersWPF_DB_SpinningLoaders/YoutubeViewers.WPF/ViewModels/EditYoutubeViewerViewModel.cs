using System;
using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class EditYoutubeViewerViewModel : ViewModelBase
    {
        public Guid YoutubeViewerId { get;  }
        public YoutubeViewerDetailsFormViewModel YoutubeViewerDetailsFormViewModel { get; }

        public EditYoutubeViewerViewModel(YoutubeViewer youtubeViewer, YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewerId = youtubeViewer.Id;

            ICommand submitCommand = new EditYoutubeViewerCommand(this, youtubeViewersStore, modalNavigationStore);
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
