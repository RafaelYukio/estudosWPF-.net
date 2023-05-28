using System;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class DeleteYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly YoutubeViewersListingItemViewModel _youtubeViewersListingItemViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;

        public DeleteYoutubeViewerCommand(YoutubeViewersListingItemViewModel youtubeViewersListingItemViewModel, YoutubeViewersStore youtubeViewersStore)
        {
            _youtubeViewersListingItemViewModel = youtubeViewersListingItemViewModel;
            _youtubeViewersStore = youtubeViewersStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            YoutubeViewer youtubeViewer = _youtubeViewersListingItemViewModel.YoutubeViewer;

            _youtubeViewersListingItemViewModel.ErrorMessage = null;
            _youtubeViewersListingItemViewModel.IsDeleting = true;

            try
            {
                await _youtubeViewersStore.Delete(youtubeViewer.Id);
            }
            catch (Exception)
            {
                _youtubeViewersListingItemViewModel.ErrorMessage = "Failed to delete!";
            }
            finally
            {
                _youtubeViewersListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
