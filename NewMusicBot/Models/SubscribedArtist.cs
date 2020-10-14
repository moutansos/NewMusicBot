using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Models
{
    public class SubscribedArtist
    {
        [JsonConstructor]
        public SubscribedArtist(string id, string name, IEnumerable<string> albums)
        {
            this.Id = id;
            this.Name = name;
            this.Albums = albums;
        }
        public string Id { get; }
        public string Name { get; }
        public IEnumerable<string> Albums { get; }
    }
}
