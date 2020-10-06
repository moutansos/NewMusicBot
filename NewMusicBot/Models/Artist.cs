using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Models
{
    public class Artist
    {
        public string Id { get; }
        public string Name { get; }
        public string Url { get; }

        public Artist(string id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }
    }
}
