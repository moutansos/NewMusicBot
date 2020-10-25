using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Models
{
    public class Release
    {
        public Release(string id, string name, string artistId, string artistName, string url)
        {
            this.Id = id;
            this.Name = name;
            this.ArtistId = artistId;
            this.ArtistName = artistName;
            this.Url = url;
        }

        public string Id { get; }
        public string Name { get; }
        public string ArtistId { get; }
        public string ArtistName { get; }
        public string Url { get; }
    }
}
