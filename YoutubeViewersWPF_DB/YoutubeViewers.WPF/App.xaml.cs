using Microsoft.EntityFrameworkCore;
using System.Windows;
using YoutubeViewers.Domain.Commands;
using YoutubeViewers.Domain.Queries;
using YoutubeViewers.EntityFramework;
using YoutubeViewers.EntityFramework.Commands;
using YoutubeViewers.EntityFramework.Queries;
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
        private readonly YoutubeViewersDbContextFactory _youtubeViewersDbContextFactory;

        private readonly ICreateYoutubeViewerCommand _createYoutubeViewerCommand;
        private readonly IUpdateYoutubeViewerCommand _updateYoutubeViewerCommand;
        private readonly IDeleteYoutubeViewerCommand _deleteYoutubeViewerCommand;
        private readonly IGetAllYoutubeViewersQuery _getAllYoutubeViewersQuery;

        private readonly SelectedYoutubeViewerStore _selectedYoutubeViewerStore;
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public App()
        {
            string connectionString = "Data Source=YoutubeViewersDb";

            _youtubeViewersDbContextFactory = new YoutubeViewersDbContextFactory(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            _createYoutubeViewerCommand = new CreateYoutubeViewerCommand(_youtubeViewersDbContextFactory);
            _updateYoutubeViewerCommand = new UpdateYoutubeViewerCommand(_youtubeViewersDbContextFactory);
            _deleteYoutubeViewerCommand = new DeleteYoutubeViewerCommand(_youtubeViewersDbContextFactory);
            _getAllYoutubeViewersQuery = new GetAllYoutubeViewersQuery(_youtubeViewersDbContextFactory);

            // É normal stores dependerem de outras stores
            // O que é bom para ter certeza de que toda informação estão atualizadas entre si na store
            // Sendo as stores a única fonte de dado verdadeira/atualizada
            // *Tomar cuidado com dependência circular
            _youtubeViewersStore = new YoutubeViewersStore(_createYoutubeViewerCommand,
                                                           _updateYoutubeViewerCommand,
                                                           _deleteYoutubeViewerCommand,
                                                           _getAllYoutubeViewersQuery);
            _modalNavigationStore = new ModalNavigationStore();
            _selectedYoutubeViewerStore = new SelectedYoutubeViewerStore(_youtubeViewersStore);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            // Executa as migrations automaticamente no start
            // Instalar extensão para o SQLite no Visual Studio
            // Encontrar o arquivo "YoutubeViewersDb"
            // Caminho: "YoutubeViewersWPF_DB\YoutubeViewers.WPF\bin\Debug\net6.0-windows"
            using (YoutubeViewersDbContext context = _youtubeViewersDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

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
