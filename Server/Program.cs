using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Server
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder
                        .AddCosmosDBGrainStorageAsDefault(options =>
                        {
                            options.ConnectionMode = ConnectionMode.Direct;
                            options.AccountEndpoint = "https://lefttwixwand.documents.azure.com:443/";
                            options.AccountKey = "2xhxMTvvXcu1E7UbAAnmRoJidvRDaPgEbjkZPjpSVRzeQLiYWQT1CktcA14gfWdZUFfu7ETRG4dYY8HyhUcTog==";
                            options.DB = "test";

                            // options.Collection = "Users";
                            // options.DropDatabaseOnInit = true; // Comment it
                            options.CanCreateResources = true;
                        })
                        .UseCosmosDBMembership(options =>
                        {
                            options.ConnectionMode = ConnectionMode.Direct;
                            options.AccountEndpoint = "https://lefttwixwand.documents.azure.com:443/";
                            options.AccountKey = "2xhxMTvvXcu1E7UbAAnmRoJidvRDaPgEbjkZPjpSVRzeQLiYWQT1CktcA14gfWdZUFfu7ETRG4dYY8HyhUcTog==";
                            options.DB = "test";
                            //options.Collection = "Connector";
                            // options.DropDatabaseOnInit = true; // Comment it
                            options.CanCreateResources = true;
                        })
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "HelloWorldApp";
                        })
                        .Configure<EndpointOptions>(options =>
                        {
                            options.AdvertisedIPAddress = IPAddress.Loopback;
                            // options.SiloPort = 11112;
                            // options.GatewayPort = 30001;
                        });
                })
                .ConfigureServices(services =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}
