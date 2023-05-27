using System.Windows;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    // Projeto baseado no vídeo:
    // https://www.youtube.com/watch?v=54ZmhbpjBmg

    public partial class App : Application
    {
        private readonly SelectedYoutubeViewerStore _selectedYoutubeViewerStore;
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public App()
        {
            // É normal stores dependerem de outras stores
            // O que é bom para ter certeza de que toda informação estão atualizadas entre si na store
            // Sendo as stores a única fonte de dado verdadeira/atualizada
            // *Tomar cuidado com dependência circular
            _youtubeViewersStore = new YoutubeViewersStore();
            _modalNavigationStore = new ModalNavigationStore();
            _selectedYoutubeViewerStore = new SelectedYoutubeViewerStore(_youtubeViewersStore);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            YoutubeViewersViewModel youtubeViewersViewModel = new(_selectedYoutubeViewerStore,
                                                                  _youtubeViewersStore,
                                                                  _modalNavigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationStore, youtubeViewersViewModel)
            };


            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
