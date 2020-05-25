using DesktopClient.Services;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace DesktopClient.Activation
{
    /// <summary>
    /// <see cref="DefaultActivationHandler"/> starts program with empty args
    /// </summary>
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        /// <summary>
        /// Default navigation element
        /// </summary>
        private readonly Type navElement;

        /// <summary>
        /// Creates new instance of <see cref="DefaultActivationHandler"/>
        /// </summary>
        /// <param name="navElement">Default navigation element</param>
        public DefaultActivationHandler(Type navElement)
        {
            this.navElement = navElement;
        }

        /// <summary>
        /// When the navigation stack isn't restored, navigate to the first page and configure
        /// <para>the new page by passing required information in the navigation parameter</para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Completed task</returns>
        protected override async Task HandleInternalAsync(IActivatedEventArgs args)
        {
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }

            NavigationService.Navigate(navElement, arguments);
            await Task.CompletedTask;
        }

        /// <summary>
        /// None of the ActivationHandlers has handled the app activation
        /// </summary>
        /// <param name="args">activation arguments</param>
        /// <returns>True if it's possible to handle the arguments</returns>
        protected override bool CanHandleInternal(IActivatedEventArgs args) => NavigationService.Frame.Content == null && navElement != null;

    }
}