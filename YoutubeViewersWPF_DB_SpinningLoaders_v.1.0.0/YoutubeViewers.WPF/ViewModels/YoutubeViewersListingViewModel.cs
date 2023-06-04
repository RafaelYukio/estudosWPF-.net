using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.Domain.Models;
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

        // Prop para export o ObservableCollection como IEnumerable (encapsulamento)
        public IEnumerable<YoutubeViewersListingItemViewModel> YoutubeViewersListingItemViewModels => _youtubeViewersListingItemViewModels;
        public YoutubeViewersListingItemViewModel SelectedYoutubeViewerListingItemViewModel
        {
            get
            {
                return _youtubeViewersListingItemViewModels
                    .FirstOrDefault(y => y.YoutubeViewer.Id == _selectedYoutubeViewerStore.SelectedYoutubeViewer?.Id);
            }
            set
            {
                _selectedYoutubeViewerStore.SelectedYoutubeViewer = value?.YoutubeViewer;
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

            // Subscribing no YoutubeViewersStore para Added, Updated e Loaded
            _youtubeViewersStore.YoutubeViewersLoaded += YoutubeViewersStore_YoutubeViewersLoaded;
            _youtubeViewersStore.YoutubeViewerAdded += YoutubeViewersStore_YoutubeViewerAdded;
            _youtubeViewersStore.YoutubeViewerUpdated += YoutubeViewersStore_YoutubeViewerUpdated;
            _youtubeViewersStore.YoutubeViewerDeleted += YoutubeViewersStore_YoutubeViewerDeleted;

            _selectedYoutubeViewerStore.SelectedYoutubeViewerChanged += SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged;

            // Não precisamos do dispose desse subscribe, pois é algo da própria classe (lives here)
            _youtubeViewersListingItemViewModels.CollectionChanged += YoutubeViewersListingItemViewModels_CollectionChanged;
        }

        private void YoutubeViewersStore_YoutubeViewersLoaded()
        {
            _youtubeViewersListingItemViewModels.Clear();

            foreach (YoutubeViewer youtubeViewer in _youtubeViewersStore.YoutubeViewers)
            {
                AddYoutubeViewer(youtubeViewer);
            }
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

        private void YoutubeViewersStore_YoutubeViewerDeleted(Guid youtubeViewerId)
        {
            YoutubeViewersListingItemViewModel youtubeViewerViewModel =
                _youtubeViewersListingItemViewModels.FirstOrDefault(y => y.YoutubeViewer.Id == youtubeViewerId);

            if (youtubeViewerViewModel != null)
            {
                _youtubeViewersListingItemViewModels.Remove(youtubeViewerViewModel);
            }
        }
        private void SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged()
        {
            OnPropertyChanged(nameof(SelectedYoutubeViewerListingItemViewModel));
        }

        private void YoutubeViewersListingItemViewModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedYoutubeViewerListingItemViewModel));
        }

        // Unsubscribe para prevenir memory leak (pesquisar sobre)
        protected override void Dispose()
        {
            _youtubeViewersStore.YoutubeViewersLoaded -= YoutubeViewersStore_YoutubeViewersLoaded;
            _youtubeViewersStore.YoutubeViewerAdded -= YoutubeViewersStore_YoutubeViewerAdded;
            _youtubeViewersStore.YoutubeViewerUpdated -= YoutubeViewersStore_YoutubeViewerUpdated;
            _youtubeViewersStore.YoutubeViewerDeleted -= YoutubeViewersStore_YoutubeViewerDeleted;
            _selectedYoutubeViewerStore.SelectedYoutubeViewerChanged -= SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged;
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
