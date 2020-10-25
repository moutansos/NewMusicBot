using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Models
{
    public class ReleaseMessage
    {
        public IEnumerable<Release> Releases { get; }
        public ulong GuildId { get; }
        public ulong ChannelId { get; }

        public ReleaseMessage(IEnumerable<Release> releases, ulong guildId, ulong channnelId) 
        {
            this.Releases = releases;
            this.GuildId = guildId;
            this.ChannelId = channnelId;
        }
    }
}
