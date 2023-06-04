using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YoutubeViewersViewModel : ViewModelBase
    {
        public YoutubeViewersListingViewModel YoutubeViewersListingViewModel { get; }
        public YoutubeViewersDetailsViewModel YoutubeViewersDetailsViewModel { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;  
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        
        public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);


        public ICommand AddYoutubeViewerCommand { get; }
        public ICommand LoadYoutubeViewersCommand { get; }

        public YoutubeViewersViewModel(SelectedYoutubeViewerStore selectedYoutubeViewerStore,
                                       YoutubeViewersStore youtubeViewersStore,
                                       ModalNavigationStore modalNavigationStore)
        {
            // Um bom exemplo do uso de métodos estáticos (que não precisam do objeto instanciado para chamar)
            // Aqui instanciamos a ViewModel por um método estático, já que ela ainda não existe
            // Queremos chamar o LoadViewModel, pois nele é feito a query de carregar a lista de YoutubeViewers da DB
            YoutubeViewersListingViewModel = new YoutubeViewersListingViewModel(selectedYoutubeViewerStore,
                                                                                youtubeViewersStore,
                                                                                modalNavigationStore);
            YoutubeViewersDetailsViewModel = new YoutubeViewersDetailsViewModel(selectedYoutubeViewerStore);

            AddYoutubeViewerCommand = new OpenAddYoutubeViewerCommand(youtubeViewersStore, modalNavigationStore);
            LoadYoutubeViewersCommand = new LoadYoutubeViewersCommand(this, youtubeViewersStore);
        }

        // Para não fazer o Load direto no construtor, instanciamos nosso ViewModel
        // Chama: Factory Method
        // Então ao invés de chamar o construto apra instanciar essa ViewModel, chamamos esse LoadViewModel

        // -------- O Loader era aqui, mas para fazer o spinning loader, movemos para o YoutubeViewersViewModel (acima daqui)
        public static YoutubeViewersViewModel LoadViewModel(SelectedYoutubeViewerStore selectedYoutubeViewerStore,
                                                                   YoutubeViewersStore youtubeViewersStore,
                                                                   ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewersViewModel viewModel = new YoutubeViewersViewModel(selectedYoutubeViewerStore,
                                                                            youtubeViewersStore,
                                                                            modalNavigationStore);

            viewModel.LoadYoutubeViewersCommand.Execute(null);
            return viewModel;
        }
    }
}
