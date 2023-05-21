using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        public YoutubeViewersListingViewModel(SelectedYoutubeViewerStore selectedYoutubeViewerStore, ModalNavigationStore modalNavigationStore)
        {
            _selectedYoutubeViewerStore = selectedYoutubeViewerStore;
            _youtubeViewersListingItemViewModels = new ObservableCollection<YoutubeViewersListingItemViewModel>();

            AddYoutubeViewer(new YoutubeViewer("Rafael", true, true), modalNavigationStore);
            AddYoutubeViewer(new YoutubeViewer("Maria", true, false), modalNavigationStore);
            AddYoutubeViewer(new YoutubeViewer("Sean", false, true), modalNavigationStore);
            AddYoutubeViewer(new YoutubeViewer("Teste", false, false), modalNavigationStore);
        }

        private void AddYoutubeViewer(YoutubeViewer youtubeViewer, ModalNavigationStore modalNavigationStore)
        {
            ICommand editCommand = new OpenEditYoutubeViewerCommand(youtubeViewer, modalNavigationStore);
            _youtubeViewersListingItemViewModels.Add(new YoutubeViewersListingItemViewModel(youtubeViewer, editCommand));
        }
    }
}
