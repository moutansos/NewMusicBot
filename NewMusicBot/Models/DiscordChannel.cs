using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewMusicBot.Models
{
    public class DiscordChannel
    {
        [JsonConstructor]
        public DiscordChannel(string id, IReadOnlyDictionary<int, string>? currentArtistOptions, IEnumerable<SubscribedArtist>? subscribedArtists)
        {
            this.Id = id;
            this.CurrentArtistOptions = currentArtistOptions;
            this.SubscribedArtists = subscribedArtists ?? new SubscribedArtist[] { };
        }

        public string Id { get; }
        public IReadOnlyDictionary<int, string>? CurrentArtistOptions { get; }
        public IEnumerable<SubscribedArtist> SubscribedArtists { get; }

        public DiscordChannel WithNewCurrentArtistOptions(IReadOnlyDictionary<int, string> currentArtistOptions) => new DiscordChannel(this.Id,
                                                                                                                                       currentArtistOptions,
                                                                                                                                       this.SubscribedArtists);

        public DiscordChannel WithSubscribedArtistAdded(SubscribedArtist newArtist) => new DiscordChannel(this.Id,
                                                                                                          this.CurrentArtistOptions,
                                                                                                          this.SubscribedArtists.Concat(new SubscribedArtist[] { newArtist }));
    }
}
