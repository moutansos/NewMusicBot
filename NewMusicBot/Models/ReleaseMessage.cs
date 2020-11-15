using System.Collections.Generic;

namespace NewMusicBot.Models
{
    public class ReleaseMessage
    {
        public IEnumerable<Release> Releases { get; }
        public ulong GuildId { get; }
        public ulong ChannelId { get; }
        public string ArtistName { get; }

        public ReleaseMessage(IEnumerable<Release> releases, string artistName, ulong guildId, ulong channnelId) 
        {
            this.Releases = releases;
            this.GuildId = guildId;
            this.ChannelId = channnelId;
            this.ArtistName = artistName;
        }
    }
}
