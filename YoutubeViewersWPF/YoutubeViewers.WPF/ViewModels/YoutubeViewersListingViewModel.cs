﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.WPF.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YoutubeViewersListingViewModel : ViewModelBase
    {
        // ObservableCollection atualiza a UI quando adiciona ou remove itens
        private readonly ObservableCollection<YoutubeViewersListingItemViewModel> _youtubeViewersListingItemViewModels;
        private readonly SelectedYoutubeViewerStore _selectedYoutubeViewerStore;
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private YoutubeViewersListingItemViewModel _selectedYoutubeViewerListingItemViewModel;

        // Prop para export o ObservableCollection como IEnumerable (encapsulamento)
        public IEnumerable<YoutubeViewersListingItemViewModel> YoutubeViewersListingItemViewModels => _youtubeViewersListingItemViewModels;
        public YoutubeViewersListingItemViewModel SelectedYoutubeViewerListingItemViewModel
        {
            get
            {
                return _selectedYoutubeViewerListingItemViewModel;
            }
            set
            {
                _selectedYoutubeViewerListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedYoutubeViewerListingItemViewModel));

                _selectedYoutubeViewerStore.SelectedYoutubeViewer = _selectedYoutubeViewerListingItemViewModel.YoutubeViewer;
            }
        }


        public YoutubeViewersListingViewModel(SelectedYoutubeViewerStore selectedYoutubeViewerStore,
                                              YoutubeViewersStore youtubeViewersStore,
                                              ModalNavigationStore modalNavigationStore)
        {
            _selectedYoutubeViewerStore = selectedYoutubeViewerStore;
            _youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
            _youtubeViewersListingItemViewModels = new ObservableCollection<YoutubeViewersListingItemViewModel>();

            // Subscribing no YoutubeViewersStore para Added e Updated
            _youtubeViewersStore.YoutubeViewerAdded += YoutubeViewersStore_YoutubeViewerAdded;
            _youtubeViewersStore.YoutubeViewerUpdated += YoutubeViewersStore_YoutubeViewerUpdated;

            AddYoutubeViewer(new YoutubeViewer(Guid.NewGuid(), "RafaelMock", true, true));
            AddYoutubeViewer(new YoutubeViewer(Guid.NewGuid(), "MariaMock", true, false));
            AddYoutubeViewer(new YoutubeViewer(Guid.NewGuid(), "SeanMock", false, true));
            AddYoutubeViewer(new YoutubeViewer(Guid.NewGuid(), "TesteMock", false, false));
        }

        private void YoutubeViewersStore_YoutubeViewerUpdated(YoutubeViewer youtubeViewer)
        {
            YoutubeViewersListingItemViewModel youtubeViewerViewModel =
                _youtubeViewersListingItemViewModels.FirstOrDefault(y => y.YoutubeViewer.Id == youtubeViewer.Id);

            if (youtubeViewerViewModel != null)
            {
                youtubeViewerViewModel.Update(youtubeViewer);
            }
        }

        // Unsubscribe para prevenir memory leak (pesquisar sobre)
        protected override void Dispose()
        {
            _youtubeViewersStore.YoutubeViewerAdded -= YoutubeViewersStore_YoutubeViewerAdded;
            _youtubeViewersStore.YoutubeViewerUpdated -= YoutubeViewersStore_YoutubeViewerUpdated;
            base.Dispose();
        }

        private void YoutubeViewersStore_YoutubeViewerAdded(YoutubeViewer youtubeViewer)
        {
            AddYoutubeViewer(youtubeViewer);
        }

        private void AddYoutubeViewer(YoutubeViewer youtubeViewer)
        {
            _youtubeViewersListingItemViewModels.Add(new YoutubeViewersListingItemViewModel(youtubeViewer,
                                                                                            _youtubeViewersStore,
                                                                                            _modalNavigationStore));
        }
    }
}
