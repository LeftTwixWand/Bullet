using Core.Models;
using DesktopClient.Services;
using DesktopClient.ViewModels;
using System;
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
            LoadWallAsync();
        }

        private async void LoadWallAsync()
        {
            InvertedListView.Items.Clear();
            foreach (var item in await OrleansClient.User.GetWall())
            {
                InvertedListView.Items.Add(new Message(OrleansClient.Login, item.Text, HorizontalAlignment.Center, item.DateTime));
            }
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

        private void ShowNameMenu(bool isTransient)
        {
            FlyoutShowOptions myOption = new FlyoutShowOptions();
            myOption.ShowMode = isTransient ? FlyoutShowMode.Transient : FlyoutShowMode.Standard;
            EditNameBarFlyout.ShowAt(nameTextBlock, myOption);
        }

        private void ShowDescriptionMenu(bool isTransient)
        {
            FlyoutShowOptions myOption = new FlyoutShowOptions();
            myOption.ShowMode = isTransient ? FlyoutShowMode.Transient : FlyoutShowMode.Standard;
            EditDescriptionBarFlyout.ShowAt(descriptionTextBlock, myOption);
        }

        private void Button_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            ShowMenu(false);
        }

        private void NameButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNameMenu((sender as Button).IsPointerOver);
        }

        private void NameButton_ContentRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            ShowNameMenu(false);
        }

        private void DescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDescriptionMenu((sender as Button).IsPointerOver);
        }

        private void DescriptionButton_ContentRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            ShowDescriptionMenu(false);
        }

        private void DescriptionAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (descriptionTextBlock.Visibility == Visibility.Visible)
            {
                descriptionTextBox.Visibility = Visibility.Visible;
                descriptionTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                descriptionTextBox.Visibility = Visibility.Collapsed;
                descriptionTextBlock.Visibility = Visibility.Visible;
                OrleansClient.User.SetDescription(ViewModel.Description);
            }
        }

        private void NameAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBlock.Visibility == Visibility.Visible)
            {
                nameTextBox.Visibility = Visibility.Visible;
                nameTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                nameTextBox.Visibility = Visibility.Collapsed;
                nameTextBlock.Visibility = Visibility.Visible;
                OrleansClient.User.SetName(ViewModel.Name);
            }
        }

        private void messageTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Message message = new Message(OrleansClient.Login, messageTextBox.Text, HorizontalAlignment.Center, DateTime.Now);
                OrleansClient.User.AddWallPost(new WallPost(message.Text, message.DateTime));
                InvertedListView.Items.Add(message);
                messageTextBox.Text = string.Empty;
            }
        }
    }
}