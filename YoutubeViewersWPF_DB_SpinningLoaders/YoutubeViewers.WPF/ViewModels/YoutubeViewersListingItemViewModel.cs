using System;
using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YoutubeViewersListingItemViewModel : ViewModelBase
    {
        public YoutubeViewer YoutubeViewer { get; private set; }

        public string Username => YoutubeViewer.Username;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
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

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }


        public YoutubeViewersListingItemViewModel(YoutubeViewer youtubeViewer, YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewer = youtubeViewer;

            DeleteCommand = new DeleteYoutubeViewerCommand(this, youtubeViewersStore);
            EditCommand = new OpenEditYoutubeViewerCommand(this, youtubeViewersStore, modalNavigationStore);
        }

        public void Update(YoutubeViewer youtubeViewer)
        {
            YoutubeViewer = youtubeViewer;
            OnPropertyChanged(nameof(Username));
        }
    }
}
