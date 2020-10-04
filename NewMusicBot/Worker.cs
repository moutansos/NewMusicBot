using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NewMusicBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly DiscordSocketClient client;

        public Worker(ILogger<Worker> logger)
        {
            this.logger = logger;
            client = new DiscordSocketClient();
            client.Log += Log;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken"));
            await client.StartAsync();

            client.MessageReceived += MessageReceived;

            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content.ToLower() == "!ping")
                await message.Channel.SendMessageAsync("Pong!");
        }

        private Task Log(LogMessage msg)
        {
            logger.LogInformation(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
