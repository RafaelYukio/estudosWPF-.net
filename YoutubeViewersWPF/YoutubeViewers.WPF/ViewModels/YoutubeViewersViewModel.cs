﻿using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YoutubeViewersViewModel : ViewModelBase
    {
        public YoutubeViewersListingViewModel YoutubeViewersListingViewModel { get; }
        public YoutubeViewersDetailsViewModel YoutubeViewersDetailsViewModel { get; }
        public ICommand AddYoutubeViewerCommand { get; }

        public YoutubeViewersViewModel(SelectedYoutubeViewerStore selectedYoutubeViewerStore,
                                       YoutubeViewersStore youtubeViewersStore,
                                       ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewersListingViewModel = new YoutubeViewersListingViewModel(selectedYoutubeViewerStore,
                                                                                youtubeViewersStore,
                                                                                modalNavigationStore);
            YoutubeViewersDetailsViewModel = new YoutubeViewersDetailsViewModel(selectedYoutubeViewerStore);

            AddYoutubeViewerCommand = new OpenAddYoutubeViewerCommand(youtubeViewersStore, modalNavigationStore);
        }
    }
}
