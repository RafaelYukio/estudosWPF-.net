﻿using System;
using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.WPF.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YoutubeViewersListingItemViewModel : ViewModelBase
    {
        public YoutubeViewer YoutubeViewer { get; private set; }

        public string Username => YoutubeViewer.Username;

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }


        public YoutubeViewersListingItemViewModel(YoutubeViewer youtubeViewer, YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewer = youtubeViewer;

            EditCommand = new OpenEditYoutubeViewerCommand(this, youtubeViewersStore, modalNavigationStore);
        }

        public void Update(YoutubeViewer youtubeViewer)
        {
            YoutubeViewer = youtubeViewer;
            OnPropertyChanged(nameof(Username));
        }
    }
}
