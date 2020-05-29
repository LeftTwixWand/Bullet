using System;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace DesktopClient.Helpers
{
    /// <summary>
    /// Navigation helper class
    /// </summary>
    public class NavHelper
    {
        /// <summary>
        /// Dependency property 
        /// </summary>
        public static readonly DependencyProperty NavigateToProperty = DependencyProperty.RegisterAttached("NavigateTo", typeof(Type), typeof(NavHelper), new PropertyMetadata(null));

        /// <summary>
        /// This helper class allows to specify the page that will be shown when you click on a NavigationViewItem
        /// <para>Usage in xaml:</para>
        /// <para>winui:NavigationViewItem x:Uid="Shell_Main" Icon="Document" helpers:NavHelper.NavigateTo="views:MainPage"</para>
        /// <para>Usage in code:</para>
        /// <para>NavHelper.SetNavigateTo(navigationViewItem, typeof(MainPage));</para>
        /// </summary>
        /// <param name="item">Navigation view item, which call event</param>
        /// <returns>Type of the page, you'll navigated to</returns>
        public static Type GetNavigateTo(NavigationViewItem item)
        {
            return (Type)item.GetValue(NavigateToProperty);
        }

        /// <summary>
        /// Go to next page
        /// </summary>
        /// <param name="item">Navigation view, whith cals the event</param>
        /// <param name="value">Page type you'll navigate</param>
        public static void SetNavigateTo(NavigationViewItem item, Type value)
        {
            item.SetValue(NavigateToProperty, value);
        }
    }
}