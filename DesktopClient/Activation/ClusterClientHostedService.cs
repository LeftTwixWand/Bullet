using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Runtime;
using Windows.UI.Xaml;

namespace DesktopClient.Activation
{
    public class ClusterClientHostedService : IHostedService
    {

        public ClusterClientHostedService()
        {
            ((App)Application.Current).Client = new ClientBuilder().UseLocalhostClustering().Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            int attempt = 0, maxAttempts = 100;
            TimeSpan delay = TimeSpan.FromSeconds(1);

            return ((App)Application.Current).Client.Connect(async error =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }

                if (++attempt < maxAttempts)
                {
                    try
                    {
                        await Task.Delay(delay, cancellationToken);
                        Debug.WriteLine("Connected!");
                    }
                    catch (OperationCanceledException)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await ((App)Application.Current).Client.Close();
            }
            catch (OrleansException error)
            {
                Debug.WriteLine($"Error while gracefully disconnecting from Orleans cluster. Will ignore and continue to shutdown. {error.Message}");
            }
        }
    }
}