using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewMusicBot.Services;

namespace NewMusicBot
{
    public class Worker : BackgroundService
    {
        private const char START_SEQUENCE = '!';
        private readonly ILogger<Worker> logger;
        private readonly DiscordSocketClient client;
        private readonly IConfigurationProvider configuration;
        private readonly CommandService commandService;
        private readonly IServiceProvider serviceProvider;

        public Worker(ILogger<Worker> logger, IConfigurationProvider configuration, IServiceProvider serviceProvider)
        {
            this.logger = logger;

            client = new DiscordSocketClient();
            client.Log += Log;

            this.configuration = configuration;
            this.commandService = new CommandService();

            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                                 services: serviceProvider);

            await client.LoginAsync(TokenType.Bot, configuration.DiscordToken);
            await client.StartAsync();

            client.MessageReceived += MessageReceived;

            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
                SocketUserMessage message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasCharPrefix(START_SEQUENCE, ref argPos) ||
                message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            SocketCommandContext context = new SocketCommandContext(client, message);
            await commandService.ExecuteAsync(context, argPos, serviceProvider);
        }

        private Task Log(LogMessage msg)
        {
            logger.LogInformation(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
