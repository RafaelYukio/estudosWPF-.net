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

                _selectedYoutubeViewerStore.SelectedYoutubeViewer = _selectedYoutubeViewerListingItemViewModel?.YoutubeViewer;
            }
        }

        public ICommand LoadYoutubeViewersCommand { get; }

        public YoutubeViewersListingViewModel(SelectedYoutubeViewerStore selectedYoutubeViewerStore,
                                              YoutubeViewersStore youtubeViewersStore,
                                              ModalNavigationStore modalNavigationStore)
        {
            _selectedYoutubeViewerStore = selectedYoutubeViewerStore;
            _youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
            _youtubeViewersListingItemViewModels = new ObservableCollection<YoutubeViewersListingItemViewModel>();

            LoadYoutubeViewersCommand = new LoadYoutubeViewersCommand(youtubeViewersStore);
            // Poderiamos executar o comando de Load aqui no construtor
            // porém não é uma boa prática ter lógicas complicadas aqui dentro
            // ainda mais que este método faz uma query na DB
            // Ex.: LoadYoutubeViewersCommand.Execute();

            // Subscribing no YoutubeViewersStore para Added, Updated e Loaded
            _youtubeViewersStore.YoutubeViewersLoaded += YoutubeViewersStore_YoutubeViewersLoaded;
            _youtubeViewersStore.YoutubeViewerAdded += YoutubeViewersStore_YoutubeViewerAdded;
            _youtubeViewersStore.YoutubeViewerUpdated += YoutubeViewersStore_YoutubeViewerUpdated;
            _youtubeViewersStore.YoutubeViewerDeleted += YoutubeViewersStore_YoutubeViewerDeleted;
        }

        // Para não fazer o Load direto no construtor, instanciamos nosso ViewModel
        // Chama: Factory Method
        // Então ao invés de chamar o construto apra instanciar essa ViewModel, chamamos esse LoadViewModel
        public static YoutubeViewersListingViewModel LoadViewModel(SelectedYoutubeViewerStore selectedYoutubeViewerStore,
                                                                   YoutubeViewersStore youtubeViewersStore,
                                                                   ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewersListingViewModel viewModel = new YoutubeViewersListingViewModel(selectedYoutubeViewerStore,
                                                                                          youtubeViewersStore,
                                                                                          modalNavigationStore);

            viewModel.LoadYoutubeViewersCommand.Execute(null);
            return viewModel;
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

        // Unsubscribe para prevenir memory leak (pesquisar sobre)
        protected override void Dispose()
        {
            _youtubeViewersStore.YoutubeViewersLoaded -= YoutubeViewersStore_YoutubeViewersLoaded;
            _youtubeViewersStore.YoutubeViewerAdded -= YoutubeViewersStore_YoutubeViewerAdded;
            _youtubeViewersStore.YoutubeViewerUpdated -= YoutubeViewersStore_YoutubeViewerUpdated;
            _youtubeViewersStore.YoutubeViewerDeleted -= YoutubeViewersStore_YoutubeViewerDeleted;
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
