using System;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class EditYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private YoutubeViewersStore _youtubeViewersStore;
        private EditYoutubeViewerViewModel _editYoutubeViewerViewModel;

        public EditYoutubeViewerCommand(EditYoutubeViewerViewModel editYoutubeViewerViewModel, YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _editYoutubeViewerViewModel = editYoutubeViewerViewModel;
            _youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            YoutubeViewerDetailsFormViewModel formViewModel = _editYoutubeViewerViewModel.YoutubeViewerDetailsFormViewModel;
            YoutubeViewer youtubeViewer = new(_editYoutubeViewerViewModel.YoutubeViewerId, formViewModel.Username, formViewModel.IsSubscribed, formViewModel.IsMember);

            try
            {
                // Edit Youtube viewer to database
                await _youtubeViewersStore.Update(youtubeViewer);
                _modalNavigationStore.Close();

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
