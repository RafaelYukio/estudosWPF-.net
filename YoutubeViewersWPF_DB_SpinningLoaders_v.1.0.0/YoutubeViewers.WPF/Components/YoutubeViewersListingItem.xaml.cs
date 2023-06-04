using System.Windows;
using System.Windows.Controls;

namespace YoutubeViewers.WPF.Components
{
    /// <summary>
    /// Interaction logic for YoutubeViewersListingItem.xaml
    /// </summary>
    public partial class YoutubeViewersListingItem : UserControl
    {
        public YoutubeViewersListingItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dropdown.IsOpen = false;
        }
    }
}
