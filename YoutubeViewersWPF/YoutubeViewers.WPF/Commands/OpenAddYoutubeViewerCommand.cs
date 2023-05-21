using System;
using System.Windows.Input;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class OpenAddYoutubeViewerCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddYoutubeViewerCommand(ModalNavigationStore modalNavigationStore)
        {
             _modalNavigationStore = modalNavigationStore;
        }
        public override void Execute(object? parameter)
        {
            AddYoutubeViewerViewModel addYoutubeViewerViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addYoutubeViewerViewModel;
        }
    }
}
