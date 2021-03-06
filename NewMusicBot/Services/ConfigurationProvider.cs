﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Services
{
    public interface IConfigurationProvider
    {
        string DiscordToken { get; }
        int CheckInterval { get; }
        string CheckSchedule { get; }
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IConfiguration configuration;

        public ConfigurationProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string DiscordToken => configuration.GetValue<string>("DiscordToken");
        public string SpotifyClientId => configuration.GetValue<string>(nameof(SpotifyClientId));
        public string SpotifyClientSectret => configuration.GetValue<string>(nameof(SpotifyClientSectret));
        public string CosmosConnectionString => configuration.GetValue<string>(nameof(CosmosConnectionString));
        public string CheckSchedule => configuration.GetValue<string>(nameof(CheckSchedule));
        public int CheckInterval => configuration.GetValue<int>(nameof(CheckInterval));
    }
}
