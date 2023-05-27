using System.Windows.Input;
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
            // Um bom exemplo do uso de métodos estáticos (que não precisam do objeto instanciado para chamar)
            // Aqui instanciamos a ViewModel por um método estático, já que ela ainda não existe
            // Queremos chamar o LoadViewModel, pois nele é feito a query de carregar a lista de YoutubeViewers da DB
            YoutubeViewersListingViewModel = YoutubeViewersListingViewModel.LoadViewModel(selectedYoutubeViewerStore,
                                                                                youtubeViewersStore,
                                                                                modalNavigationStore);
            YoutubeViewersDetailsViewModel = new YoutubeViewersDetailsViewModel(selectedYoutubeViewerStore);

            AddYoutubeViewerCommand = new OpenAddYoutubeViewerCommand(youtubeViewersStore, modalNavigationStore);
        }
    }
}
