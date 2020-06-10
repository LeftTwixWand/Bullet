using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using DesktopClient.Views;
using DesktopClient.Services;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using DesktopClient.Activation;
using System.Diagnostics;
using Interfaces;

namespace DesktopClient
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        
        public IClusterClient Client;

        public IHostBuilder builder = new HostBuilder().ConfigureServices(service =>
        {
            service.AddSingleton<ClusterClientHostedService>();
            service.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
            //service.AddSingleton(_ => _.GetService<ClusterClientHostedService>().Client);

            //service.AddHostedService<HelloWorlClientHostedService>();
        });
        
        /// <summary>
        /// Activation service object
        /// </summary>
        private Lazy<ActivationService> activationService;

        /// <summary>
        /// Activation service property
        /// </summary>
        internal ActivationService ActivationService => activationService.Value;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.

            builder.Build().StartAsync().GetAwaiter().GetResult();
            IUserGrain user = Client.GetGrain<IUserGrain>("LeftTwixWand");
            var response = user.SayHello().GetAwaiter().GetResult();

            activationService = new Lazy<ActivationService>(CreateActivationService);

            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Create new activation service
        /// </summary>
        /// <returns>New activation service object</returns>
        private ActivationService CreateActivationService() => new ActivationService(this, typeof(ProfilePage), new Lazy<UIElement>(CreateShell));

        /// <summary>
        /// Create new shell page
        /// </summary>
        /// <returns>New shell page</returns>
        private UIElement CreateShell() => new ShellPage();

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (e.PrelaunchActivated)
            {
                return;
            }

            await ActivationService.ActivateAsync(e);
        }
        
        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}