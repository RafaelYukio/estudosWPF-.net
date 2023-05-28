
using System;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class AddYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly AddYoutubeViewerViewModel _addYoutubeViewerViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        public AddYoutubeViewerCommand(AddYoutubeViewerViewModel addYoutubeViewerViewModel,
                                       YoutubeViewersStore youtubeViewersStore,
                                       ModalNavigationStore modalNavigationStore)
        {
            _addYoutubeViewerViewModel = addYoutubeViewerViewModel;
            _youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            YoutubeViewerDetailsFormViewModel formViewModel = _addYoutubeViewerViewModel.YoutubeViewerDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;
            
            YoutubeViewer youtubeViewer = new(Guid.NewGuid(), formViewModel.Username, formViewModel.IsSubscribed, formViewModel.IsMember);

            try
            {
                // Add Youtube viewer to database
                await _youtubeViewersStore.Add(youtubeViewer);
                _modalNavigationStore.Close();

            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to add Youtube viewer!";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }

        }
    }
}
