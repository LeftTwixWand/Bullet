using DesktopClient.Helpers;
using DesktopClient.Services;
using DesktopClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

using WinUI = Microsoft.UI.Xaml.Controls;

namespace DesktopClient.ViewModels
{
    public class ShellViewModel : Observable
    {
        /// <summary>
        /// Keyboard accelerator, allows us navigate in the navigation view with Left ALT + arrows
        /// </summary>
        private readonly KeyboardAccelerator altLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);

        /// <summary>
        /// Keyboard accelerator, that handled back button click
        /// </summary>
        private readonly KeyboardAccelerator backKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);

        /// <summary>
        /// Is navigation view back button enabled 
        /// </summary>
        private bool isBackEnabled;

        /// <summary>
        /// List of keyboard hotkeys
        /// </summary>
        private IList<KeyboardAccelerator> keyboardAccelerators;

        /// <summary>
        /// Navigation view instance
        /// </summary>
        private WinUI.NavigationView navigationView;

        /// <summary>
        /// Selected menu item
        /// </summary>
        private WinUI.NavigationViewItem selectedItem;

        /// <summary>
        /// Load command
        /// </summary>
        private ICommand loadCommand;

        /// <summary>
        /// New item invoked commmand
        /// </summary>
        private ICommand itemInvokedCommand;

        /// <summary>
        /// Is back enabled property, which provides binding
        /// </summary>
        public bool IsBackEnabled
        {
            get => isBackEnabled;
            set { Set(ref isBackEnabled, value); }
        }

        /// <summary>
        /// Selected item property, which provides binding
        /// </summary>
        public WinUI.NavigationViewItem SelectedItem
        {
            get => selectedItem;
            set { Set(ref selectedItem, value); }
        }

        /// <summary>
        /// Command calls when page is loading
        /// </summary>
        public ICommand LoadedCommand => loadCommand ?? (loadCommand = new RelayCommand(OnLoaded));

        /// <summary>
        /// Command, calls when new item invoked
        /// </summary>
        public ICommand ItemInvokedCommand => itemInvokedCommand ?? (itemInvokedCommand = new RelayCommand<WinUI.NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        /// <summary>
        /// Generate new instance of <see cref="ShellViewModel"/>
        /// </summary>
        public ShellViewModel()
        {
        }

        /// <summary>
        /// Initialize events
        /// </summary>
        /// <param name="frame">Current frame</param>
        /// <param name="navigationView">Navigation view</param>
        /// <param name="keyboardAccelerators">Keyboard accelerators list</param>
        public void Initialize(Frame frame, WinUI.NavigationView navigationView, IList<KeyboardAccelerator> keyboardAccelerators)
        {
            this.navigationView = navigationView;
            this.keyboardAccelerators = keyboardAccelerators;
            NavigationService.Frame = frame;
            NavigationService.NavigationFailed += Frame_NavigationFailed;
            NavigationService.Navigated += Frame_Navigated;
            navigationView.BackRequested += OnBackRequested;
        }

        /// <summary>
        /// Back button click event
        /// </summary>
        private void OnBackRequested(WinUI.NavigationView sender, WinUI.NavigationViewBackRequestedEventArgs args) => NavigationService.GoBack();

        /// <summary>
        /// Set current page as selected
        /// </summary>
        /// <param name="sender">The frame, that was navigated</param>
        /// <param name="e">Event args</param>
        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            IsBackEnabled = NavigationService.CanGoBack;
            if (e.SourcePageType == typeof(SettingsPage))
            {
                SelectedItem = navigationView.SelectedItem as WinUI.NavigationViewItem;
                return;
            }

            SelectedItem = navigationView.MenuItems
                .OfType<WinUI.NavigationViewItem>()
                .FirstOrDefault(menuItem => IsMenuItemForPageType(menuItem, e.SourcePageType));
        }

        /// <summary>
        /// Check if menu item is Page
        /// </summary>
        /// <param name="menuItem">Menu item for check</param>
        /// <param name="sourcePageType">Default page type</param>
        /// <returns>True if menu item is page</returns>
        private bool IsMenuItemForPageType(WinUI.NavigationViewItem menuItem, Type sourcePageType) => menuItem.GetValue(NavHelper.NavigateToProperty) as Type == sourcePageType;

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e) => throw e.Exception;

        /// <summary>
        /// Goes to the selected page
        /// </summary>
        /// <param name="args">Navigation view arguments</param>
        private void OnItemInvoked(WinUI.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.Navigate(typeof(SettingsPage));
                return;
            }

            var item = navigationView.MenuItems
                .OfType<WinUI.NavigationViewItem>()
                .First(menuItem => (string)menuItem.Content == (string)args.InvokedItem);

            var pageType = item.GetValue(NavHelper.NavigateToProperty) as Type;
            NavigationService.Navigate(pageType);
        }

        /// <summary>
        /// Keyboard accelerators are added here to avoid showing 'Alt + left' tooltip on the page
        /// </summary>
        /// More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
        private async void OnLoaded()
        {
            keyboardAccelerators.Add(altLeftKeyboardAccelerator);
            keyboardAccelerators.Add(backKeyboardAccelerator);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Handeled keyboard click
        /// </summary>
        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            KeyboardAccelerator keyboardAccelerator = new KeyboardAccelerator() { Key = key };

            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += KeyboardAccelerator_Invoked;
            return keyboardAccelerator;
        }

        /// <summary>
        /// Change Handled state after navigation veiw go back.
        /// </summary>
        private static void KeyboardAccelerator_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args) => args.Handled = NavigationService.GoBack();
    }
}