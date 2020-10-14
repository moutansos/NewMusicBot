using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.Infrastructure.SpotifyApi.Queries
{
    public class ArtistByIdQuery : ISpotifyQuery<Task<FullArtist>>
    {
        private readonly string id;

        public ArtistByIdQuery(string id) => this.id = id;

        public async Task<FullArtist> ExecuteAsync(ISpotifyClient client)
        {
            FullArtist? artist = await client.Artists.Get(id);

            if (artist is null)
                throw new ArgumentNullException(nameof(artist));

            return artist;
        }
    }
}
