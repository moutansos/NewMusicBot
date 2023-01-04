using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using NewMusicBot.Models;
using NewMusicBot.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewMusicBot.BackgroundServices
{
    public class NewMusicChecker : BackgroundService
    {
        private CrontabSchedule schedule;
        private DateTime nextRun;
        private readonly ILogger<NewMusicChecker> logger;
        private readonly DiscordSocketClient client;
        private readonly IDiscordClientWrapper clientWrapper;
        private readonly INewMusicBotService newMusicBotService;
        private readonly IConfigurationProvider configuration;

        public NewMusicChecker(ILogger<NewMusicChecker> logger,
                               INewMusicBotService newMusicBotService,
                               IDiscordClientWrapper clientWrapper,
                               IConfigurationProvider configurationProvider)
        {
            this.logger = logger;
            this.configuration = configurationProvider;

            this.clientWrapper = clientWrapper;
            this.client = clientWrapper.Client;
            client.Log += Log;
            this.newMusicBotService = newMusicBotService;

            schedule = CrontabSchedule.Parse(configuration.CheckSchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            nextRun = schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await clientWrapper.StartAsync();

            await Process();
            return;

            do
            {
                var now = DateTime.Now;
                var nextrun = schedule.GetNextOccurrence(now);
                if (now > nextRun)
                {
                    try
                    {
                        await Process();
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex, "An unexpected error occured in the background service.");
                    }
                    nextRun = schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task Process()
        {
            IAsyncEnumerable<ReleaseMessage>? result = newMusicBotService.CheckSubscriptions();
            await foreach(ReleaseMessage msg in result)
            {
                var guild = client.GetGuild(msg.GuildId);
                var channel = guild.GetTextChannel(msg.ChannelId);

                foreach (Release release in msg.Releases)
                    await channel.SendMessageAsync($"New release by {release.ArtistName}: {release.Name}\n{release.Url}");
            }
        }

        private Task Log(LogMessage msg)
        {
            logger.LogInformation(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
