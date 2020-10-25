using System;
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
        private readonly DiscordSocketClient client;
        private readonly IConfigurationProvider configuration;
        private readonly CommandService commandService;
        private readonly IServiceProvider serviceProvider;
        private readonly IDiscordClientWrapper clientWrapper;

        public Worker(ILogger<Worker> logger, IConfigurationProvider configuration, IServiceProvider serviceProvider, IDiscordClientWrapper clientWrapper)
        {
            this.configuration = configuration;
            this.commandService = new CommandService();

            this.serviceProvider = serviceProvider;
            this.clientWrapper = clientWrapper;
            this.client = clientWrapper.Client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                                 services: serviceProvider);

            await clientWrapper.StartAsync();

            client.MessageReceived += MessageReceived;

            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            if (!(messageParam is SocketUserMessage message)) return;

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
    }
}
