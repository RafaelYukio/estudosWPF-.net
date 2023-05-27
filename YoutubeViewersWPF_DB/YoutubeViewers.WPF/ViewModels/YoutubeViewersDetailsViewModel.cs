using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YoutubeViewersDetailsViewModel : ViewModelBase
    {
        private readonly SelectedYoutubeViewerStore _selectedYoutubeViewerStore;
        private YoutubeViewer SelectedYoutubeViewer => _selectedYoutubeViewerStore.SelectedYoutubeViewer;

        // Há o input dos valores caso SelectedYoutubeViewer seja null (através do "??"), porém podemos usar uma prop. para indicar:
        public bool HasSelectedYoutubeViewer => SelectedYoutubeViewer != null;

        // SelectedYoutubeViewer pode ser null, caso não tenha selecionado, então o "??" coloca o valor
        // Devido o Grid no xaml ser condicional pelo "HasSelectedYoutubeViewer", os valores de null abaixo não são exibidos
        public string Username => SelectedYoutubeViewer?.Username ?? "Unknown";
        public string IsSubscribedDisplay => (SelectedYoutubeViewer?.IsSubscribed ?? false) ? "Yes" : "No";
        public string IsMemberDisplay => (SelectedYoutubeViewer?.IsMember ?? false) ? "Yes" : "No";

        public YoutubeViewersDetailsViewModel(SelectedYoutubeViewerStore selectedYoutubeViewerStore)
        {
            _selectedYoutubeViewerStore = selectedYoutubeViewerStore;

            // Handle event da store (faz um subscribe)
            // Devido a esse subscribe ao store (que é global), o lifetime desta ViewModel será para todo o tempo da aplicação (nunca é limpa)
            // Precisa ter um método de dispose para limpar aqui
            _selectedYoutubeViewerStore.SelectedYoutubeViewerChanged += SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged;
        }

        protected override void Dispose()
        {
            _selectedYoutubeViewerStore.SelectedYoutubeViewerChanged -= SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged;
            base.Dispose();
        }

        private void SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged()
        {
            // Muda todas as props quando muda a seleção
            OnPropertyChanged(nameof(HasSelectedYoutubeViewer));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(IsSubscribedDisplay));
            OnPropertyChanged(nameof(IsMemberDisplay));
        }
    }
}
