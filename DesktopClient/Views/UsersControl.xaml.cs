using Core.Models;
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
    }
}
