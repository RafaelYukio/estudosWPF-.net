using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.Commands
{
    public class LoadYoutubeViewersCommand : AsyncCommandBase
    {
        private readonly YoutubeViewersStore _youtubeViewersStore;
        public LoadYoutubeViewersCommand(YoutubeViewersStore youtubeViewersStore)
        {
            _youtubeViewersStore = youtubeViewersStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _youtubeViewersStore.Load();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
