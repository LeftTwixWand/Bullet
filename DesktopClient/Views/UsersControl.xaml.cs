using Core.Models;
using DesktopClient.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DesktopClient.Views
{
    public sealed partial class UsersControl : UserControl
    {
        public User FriendUser
        {
            get { return GetValue(FriendUserProperty) as User; }
            set { SetValue(FriendUserProperty, value); }
        }

        public static readonly DependencyProperty FriendUserProperty = DependencyProperty.Register("FriendUser", typeof(User), typeof(UsersControl), new PropertyMetadata(null, OnFriendUserPropertyChanged));

        public UsersControl()
        {
            InitializeComponent();
        }

        private static void OnFriendUserPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UsersControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }

        private async void messageTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Message message = new Message(await OrleansClient.User.GetLogin(), messageTextBox.Text, HorizontalAlignment.Right, DateTime.Now);
                InvertedListView.Items.Add(message);
                messageTextBox.Text = string.Empty;
            }
            if (e.Key == Windows.System.VirtualKey.NumberPad0)
            {
                Message message = new Message(await OrleansClient.OtherUser.GetLogin(), "Привет", HorizontalAlignment.Left, DateTime.Now);
                InvertedListView.Items.Add(message);
                messageTextBox.Text = string.Empty;
            }
            if (e.Key == Windows.System.VirtualKey.NumberPad1)
            {
                Message message = new Message(await OrleansClient.OtherUser.GetLogin(), "Как дела?", HorizontalAlignment.Left, DateTime.Now);
                InvertedListView.Items.Add(message);
                messageTextBox.Text = string.Empty;
            }
        }
    }
}
