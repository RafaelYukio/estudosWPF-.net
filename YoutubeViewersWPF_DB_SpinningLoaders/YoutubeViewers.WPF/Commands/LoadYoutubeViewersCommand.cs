using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class LoadYoutubeViewersCommand : AsyncCommandBase
    {
        private readonly YoutubeViewersViewModel _youtubeViewersViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;
        public LoadYoutubeViewersCommand(YoutubeViewersViewModel youtubeViewersViewModel, YoutubeViewersStore youtubeViewersStore)
        {
            _youtubeViewersViewModel = youtubeViewersViewModel;
            _youtubeViewersStore = youtubeViewersStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _youtubeViewersViewModel.IsLoading = true;
            _youtubeViewersViewModel.ErrorMessage = null;

            try
            {
                await _youtubeViewersStore.Load();
            }
            catch (Exception)
            {
                _youtubeViewersViewModel.ErrorMessage = "Failed to load Youtube viewers!";
            }
            finally
            {
                _youtubeViewersViewModel.IsLoading = false;
            }
        }
    }
}
