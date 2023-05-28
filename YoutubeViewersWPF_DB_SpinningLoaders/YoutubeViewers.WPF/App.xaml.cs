using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using YoutubeViewers.Domain.Commands;
using YoutubeViewers.Domain.Queries;
using YoutubeViewers.EntityFramework;
using YoutubeViewers.EntityFramework.Commands;
using YoutubeViewers.EntityFramework.Queries;
using YoutubeViewers.WPF.HostBuilders;
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
        //private readonly YoutubeViewersDbContextFactory _youtubeViewersDbContextFactory;

        //private readonly ICreateYoutubeViewerCommand _createYoutubeViewerCommand;
        //private readonly IUpdateYoutubeViewerCommand _updateYoutubeViewerCommand;
        //private readonly IDeleteYoutubeViewerCommand _deleteYoutubeViewerCommand;
        //private readonly IGetAllYoutubeViewersQuery _getAllYoutubeViewersQuery;

        //private readonly SelectedYoutubeViewerStore _selectedYoutubeViewerStore;
        //private readonly YoutubeViewersStore _youtubeViewersStore;
        //private readonly ModalNavigationStore _modalNavigationStore;

        // .NET Generic Host
        // para DependencyInjection, App configuration o AppSettings.json e outras infraestruturas como logging
        // Microsoft.Extensions.Hosting
        private readonly IHost _host;

        public App()
        {
            // Verificar o CreateApplicationBuilder, onde é iniciliazado todas as configs. padrões (appsettings.json etc..)
            _host = Host.CreateDefaultBuilder()
                // AddDbContext é um método estático criado para injeção do banco, assim aqui não fica cheio
                .AddDbContext()
                .ConfigureServices((context,  services) =>
                {
                    // A injeção do banco foi feita em um método estático separado para aqui não ficar grande
                    //// Singleton: apenas uma instância é criada durante toda a lifetime da aplicação
                    //string connectionString = context.Configuration.GetConnectionString("sqlite");
                    //services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                    //services.AddSingleton<YoutubeViewersDbContextFactory>();

                    // Na instância dos Command é necessário o YoutubeViewersDbContextFactory no construtor
                    // porém como já injetamos acima, o app sabe que irá usar somente aquele instanciado
                    // Não há um estado nesses comandos/queries que outras partes do app necessitem, então ter uma instância Singleton é OK
                    services.AddSingleton<ICreateYoutubeViewerCommand, CreateYoutubeViewerCommand>();
                    services.AddSingleton<IUpdateYoutubeViewerCommand, UpdateYoutubeViewerCommand>();
                    services.AddSingleton<IDeleteYoutubeViewerCommand, DeleteYoutubeViewerCommand>();
                    services.AddSingleton<IGetAllYoutubeViewersQuery, GetAllYoutubeViewersQuery>();

                    // As stores são o estado da aplicação
                    // Nas stores queremos ter apenas uma instância, já que elas são as únicas fontes de dados verdadeira
                    services.AddSingleton<YoutubeViewersStore>();
                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<SelectedYoutubeViewerStore>();

                    // Transient cria uma nova instância a cada request de service
                    // Aqui temos que instanciar de modo diferente, mesmo que no construtor YoutubeViewersViewModel já
                    // tenham as classes injetadas, pois na verdade instanciamos esse pelo método estático LoadViewModel
                    services.AddTransient<YoutubeViewersViewModel>(CreateYoutubeViewersViewModel);
                    services.AddSingleton<MainViewModel>();
                    
                    // Aqui temos que fazer assim na MainWindow, pois o DataContext não vai direto no construtor
                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });

                    // A ordenação não importa, mesmo que uma classe da injeção dependa da outra
                })
                .Build();

            // --- Antes da injeção de dependência:

            //string connectionString = "Data Source=YoutubeViewersDb";

            //_youtubeViewersDbContextFactory = new YoutubeViewersDbContextFactory(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            //_createYoutubeViewerCommand = new CreateYoutubeViewerCommand(_youtubeViewersDbContextFactory);
            //_updateYoutubeViewerCommand = new UpdateYoutubeViewerCommand(_youtubeViewersDbContextFactory);
            //_deleteYoutubeViewerCommand = new DeleteYoutubeViewerCommand(_youtubeViewersDbContextFactory);
            //_getAllYoutubeViewersQuery = new GetAllYoutubeViewersQuery(_youtubeViewersDbContextFactory);

            // É normal stores dependerem de outras stores
            // O que é bom para ter certeza de que toda informação estão atualizadas entre si na store
            // Sendo as stores a única fonte de dado verdadeira/atualizada
            // *Tomar cuidado com dependência circular
            //_youtubeViewersStore = new YoutubeViewersStore(_createYoutubeViewerCommand,
            //                                               _updateYoutubeViewerCommand,
            //                                               _deleteYoutubeViewerCommand,
            //                                               _getAllYoutubeViewersQuery);
            //_modalNavigationStore = new ModalNavigationStore();
            //_selectedYoutubeViewerStore = new SelectedYoutubeViewerStore(_youtubeViewersStore);

            // ---
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            YoutubeViewersDbContextFactory youtubeViewersDbContextFactory = _host.Services.GetRequiredService<YoutubeViewersDbContextFactory>();

            // Executa as migrations automaticamente no start
            // Instalar extensão para o SQLite no Visual Studio
            // Encontrar o arquivo "YoutubeViewersDb"
            // Caminho: "YoutubeViewersWPF_DB\YoutubeViewers.WPF\bin\Debug\net6.0-windows"
            using (YoutubeViewersDbContext context = youtubeViewersDbContextFactory.Create())
            {
                context.Database.Migrate();
            }
            
            // Antes o Loader estava na listing model, porém colocamos aqui direto na ViewModel principal
            //YoutubeViewersViewModel youtubeViewersViewModel = YoutubeViewersViewModel.LoadViewModel(_selectedYoutubeViewerStore,
            //                                                                                        _youtubeViewersStore,
            //                                                                                        _modalNavigationStore);

            //MainWindow = new MainWindow()
            //{
            //    DataContext = new MainViewModel(_modalNavigationStore, youtubeViewersViewModel)
            //};

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

        private YoutubeViewersViewModel CreateYoutubeViewersViewModel(IServiceProvider services)
        {
            return YoutubeViewersViewModel.LoadViewModel(
                services.GetRequiredService<SelectedYoutubeViewerStore>(),
                services.GetRequiredService<YoutubeViewersStore>(),
                services.GetRequiredService<ModalNavigationStore>());
        }
    }
}
