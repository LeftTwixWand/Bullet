using DesktopClient.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DesktopClient.Services
{
    /// <summary>
    /// The ActivationService is in charge of handling the applications initialization and activation.
    /// </summary>
    // For more information on understanding and extending activation flow see
    // https://github.com/microsoft/WindowsTemplateStudio/blob/master/docs/UWP/activation.md
    internal class ActivationService
    {
        /// <summary>
        /// App.cs reference
        /// </summary>
        private readonly App app;

        /// <summary>
        /// Default navigation page
        /// </summary>
        private readonly Type defaultNavItem;

        /// <summary>
        /// Main container page
        /// </summary>
        private Lazy<UIElement> shell;

        /// <summary>
        /// Activation arguments
        /// </summary>
        private object lastActivationArgs;

        /// <summary>
        /// Constructor of <see cref="ActivationService"/>
        /// </summary>
        /// <param name="app">App.cs reference</param>
        /// <param name="defaultNavItem">Default navigation page</param>
        /// <param name="shell">Main container page</param>
        public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
        {
            this.app = app;
            this.shell = shell;
            this.defaultNavItem = defaultNavItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationArgs"></param>
        /// <returns></returns>
        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                await InitializeAsync();

                // Do not repeat app initialization when the Window already has content, just ensure that the window is active
                if (Window.Current.Content == null)
                {
                    // Create a Shell or Frame to act as the navigation context
                    Window.Current.Content = shell?.Value ?? new Frame();
                }
            }

            await HandleActivationAsync(activationArgs);
            lastActivationArgs = activationArgs;

            if (IsInteractive(activationArgs))
            {
                // Ensure the current window is active
                Window.Current.Activate();

                await StartupAsync();
            }
        }

        /// <summary>
        /// Tasks after activation
        /// </summary>
        /// <returns>Completed tasks</returns>
        private Task StartupAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Depending on activationArgs one of ActivationHandlers or DefaultActivationHandler will navigate to the first page
        /// </summary>
        /// <param name="activationArgs">Activation arguments of program. 
        /// <para>Example: last opened page.</para></param>
        /// <returns>Completed task</returns>
        private async Task HandleActivationAsync(object activationArgs)
        {
            var activationHandler = GetActivationHandlers().FirstOrDefault(handler => handler.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (IsInteractive(activationArgs))
            {
                var defaultHandler = new DefaultActivationHandler(defaultNavItem);
                if (defaultHandler.CanHandle(activationArgs))
                {
                    await defaultHandler.HandleAsync(activationArgs);
                }
            }
        }

        /// <summary>
        /// Activation services collection
        /// </summary>
        /// <returns>Collection of activation services</returns>
        private IEnumerable<ActivationHandler> GetActivationHandlers()
        {
            yield break;
        }

        /// <summary>
        /// Check if arguments are interactive
        /// <para>Example: It's possible to go to the last open page</para>
        /// </summary>
        /// <param name="args">Activation arguments</param>
        /// <returns>True if args are interactive</returns>
        private static bool IsInteractive(object args) => args is IActivatedEventArgs;

        /// <summary>
        /// Initialize services that you need before app activation
        /// <para>The splash screen is shown while this code runs</para>
        /// </summary>
        /// <returns>Completed task</returns>
        private Task InitializeAsync()
        {
            //await ThemeSelectorService.InitializeAsync().ConfigureAwait(false);
            return Task.CompletedTask;
        }
    }
}