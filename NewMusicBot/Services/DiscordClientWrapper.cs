using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.Services
{
    public interface IDiscordClientWrapper
    {
        DiscordSocketClient Client { get; }

        Task StartAsync();
    }

    public class DiscordClientWrapper : IDiscordClientWrapper
    {
        private readonly ILogger<DiscordClientWrapper> logger;
        private readonly IConfigurationProvider configurationProvider;

        public DiscordClientWrapper(IConfigurationProvider configurationProvider, ILogger<DiscordClientWrapper> logger)
        {
            this.logger = logger;
            this.configurationProvider = configurationProvider;

            this.Client = new DiscordSocketClient();
            this.Client.Log += Log;
        }

        public async Task StartAsync()
        {
            if (Client.ConnectionState != ConnectionState.Disconnected)
                return;

            await Client.LoginAsync(TokenType.Bot, configurationProvider.DiscordToken);
            await Client.StartAsync();
        }

        public DiscordSocketClient Client { get; }

        private Task Log(LogMessage msg)
        {
            logger.LogInformation(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
