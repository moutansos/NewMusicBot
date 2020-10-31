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
        public DiscordChannel(string id, string guildId, IReadOnlyDictionary<int, string>? currentArtistOptions, IEnumerable<SubscribedArtist>? subscribedArtists)
        {
            this.Id = id;
            this.GuildId = guildId;
            this.CurrentArtistOptions = currentArtistOptions;
            this.SubscribedArtists = subscribedArtists ?? new SubscribedArtist[] { };
        }

        public string Id { get; }
        public string GuildId { get; }
        public IReadOnlyDictionary<int, string>? CurrentArtistOptions { get; }
        public IEnumerable<SubscribedArtist> SubscribedArtists { get; }

        public DiscordChannel WithNewCurrentArtistOptions(IReadOnlyDictionary<int, string> currentArtistOptions) => new DiscordChannel(this.Id,
                                                                                                                                       this.GuildId,
                                                                                                                                       currentArtistOptions,
                                                                                                                                       this.SubscribedArtists);

        public DiscordChannel WithSubscribedArtistAdded(SubscribedArtist newArtist) => new DiscordChannel(this.Id,
                                                                                                          this.GuildId,
                                                                                                          this.CurrentArtistOptions,
                                                                                                          this.SubscribedArtists.Concat(new SubscribedArtist[] { newArtist }));

        public DiscordChannel WithUpdatedSubscribedArtist(SubscribedArtist subscribedArtits) =>
            new DiscordChannel(this.Id,
                               this.GuildId,
                               this.CurrentArtistOptions,
                               this.SubscribedArtists.Select(artist => artist.Id == subscribedArtits.Id ? subscribedArtits : artist));
    }
}
