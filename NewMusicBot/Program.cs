using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewMusicBot.BackgroundServices;
using NewMusicBot.Infrastructure.CosmosDb;
using NewMusicBot.Infrastructure.SpotifyApi;
using NewMusicBot.Services;
using SpotifyAPI.Web;

namespace NewMusicBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    Services.ConfigurationProvider configProvider = new Services.ConfigurationProvider(hostContext.Configuration);
                    services.AddTransient<ISpotifyClient>(s =>
                    {
                        SpotifyClientConfig config = SpotifyClientConfig
                            .CreateDefault()
                            .WithAuthenticator(new ClientCredentialsAuthenticator(
                                configProvider.SpotifyClientId, 
                                configProvider.SpotifyClientSectret));

                        return new SpotifyClient(config);
                    });
                    services.AddTransient(s =>
                    {
                        CosmosClientOptions options = new CosmosClientOptions()
                        {
                            SerializerOptions = new CosmosSerializationOptions()
                            {
                                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                            }
                        };
                        return new CosmosClient(configProvider.CosmosConnectionString, options);
                    });
                    services.AddTransient<ISpotifyDataLayer, SpotifyDataLayer>();
                    services.AddSingleton<IConfigurationProvider>(configProvider);
                    services.AddTransient<IMusicInfoService, MusicInfoService>();
                    services.AddTransient<ICosmosDataLayer, CosmosDataLayer>();
                    services.AddTransient<ISubscriptionService, SubscriptionService>();
                    services.AddTransient<INewMusicBotService, NewMusicBotService>();
                    services.AddSingleton<IDiscordClientWrapper, DiscordClientWrapper>();
                    services.AddHostedService<Worker>();
                    services.AddHostedService<MyTestHostedService>();
                });
    }
}
