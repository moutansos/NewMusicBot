using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Models
{
    public class DiscordChannel
    {
        [JsonConstructor]
        public DiscordChannel(string id, Dictionary<int, string>? currentArtistOptions)
        {
            this.Id = id;
            this.CurrentArtistOptions = currentArtistOptions;
        }

        public string Id { get; }
        public IReadOnlyDictionary<int, string>? CurrentArtistOptions { get; }

        public DiscordChannel WithNewCurrentArtistOptions(Dictionary<int, string> currentArtistOptions)
        {
            return new DiscordChannel(this.Id, currentArtistOptions);
        }
    }
}
