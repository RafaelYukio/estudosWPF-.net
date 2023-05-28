using System.ComponentModel;

namespace YoutubeViewers.WPF.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // Interface para que a cada mudança na ViewModel é lançado um evento para a UI atualizar
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Dispose() { }
    }
}
