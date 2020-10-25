using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using NewMusicBot.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewMusicBot.BackgroundServices
{
    public class MyTestHostedService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<MyTestHostedService> logger;
        private readonly DiscordSocketClient client;
        private readonly IDiscordClientWrapper clientWrapper;

        private string Schedule => "*/10 * * * * *"; //Runs every 10 seconds

        public MyTestHostedService(ILogger<MyTestHostedService> logger, IConfigurationProvider configuration, IDiscordClientWrapper clientWrapper)
        {
            this.logger = logger;

            this.clientWrapper = clientWrapper;
            this.client = clientWrapper.Client;
            client.Log += Log;

            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await clientWrapper.StartAsync();

            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task Process()
        {
            string message = "hello world" + DateTime.Now.ToString("F");
            Console.WriteLine(message);
            var guild = client.GetGuild(554510756289314826);
            var channel = guild.GetTextChannel(554510920823472138);
            await channel.SendMessageAsync(message);
        }

        private Task Log(LogMessage msg)
        {
            logger.LogInformation(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
