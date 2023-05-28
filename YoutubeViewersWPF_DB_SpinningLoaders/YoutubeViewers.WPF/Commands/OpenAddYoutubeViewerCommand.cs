using System;
using System.Windows.Input;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class OpenAddYoutubeViewerCommand : CommandBase
    {
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddYoutubeViewerCommand(YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }
        public override void Execute(object? parameter)
        {
            AddYoutubeViewerViewModel addYoutubeViewerViewModel = new(_youtubeViewersStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addYoutubeViewerViewModel;
        }
    }
}
