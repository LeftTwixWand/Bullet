using DesktopClient.ViewModels;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DesktopClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        public ProfileViewModel ViewModel { get; } = new ProfileViewModel();

        public ProfilePage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
            ViewModel.Initialize();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowMenu((sender as Button).IsPointerOver);
        }

        private void ShowMenu(bool isTransient)
        {
            FlyoutShowOptions myOption = new FlyoutShowOptions();
            myOption.ShowMode = isTransient ? FlyoutShowMode.Transient : FlyoutShowMode.Standard;
            MyCommandBarFlyout.ShowAt(myProfileImage, myOption);
        }

        private void Button_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            ShowMenu(false);
        }
    }
}