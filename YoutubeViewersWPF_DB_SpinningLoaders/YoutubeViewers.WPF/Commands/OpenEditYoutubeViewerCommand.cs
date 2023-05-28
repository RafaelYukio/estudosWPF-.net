using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class OpenEditYoutubeViewerCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly YoutubeViewersListingItemViewModel _youtubeViewersListingItemViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;

        public OpenEditYoutubeViewerCommand(YoutubeViewersListingItemViewModel youtubeViewersListingItemViewModel, YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _youtubeViewersListingItemViewModel = youtubeViewersListingItemViewModel;
            _youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            YoutubeViewer youtubeViewer = _youtubeViewersListingItemViewModel.YoutubeViewer;

            EditYoutubeViewerViewModel editYoutubeViewerViewModel = new(youtubeViewer, _youtubeViewersStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editYoutubeViewerViewModel;
        }
    }
}
