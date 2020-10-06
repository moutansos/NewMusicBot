using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.Infrastructure.SpotifyApi.Queries
{
    public class ArtistSearchQuery : ISpotifyQuery<IAsyncEnumerable<FullArtist>>
    {
        private const int ITEM_LIMIT = 5;

        private readonly string searchQuery;

        public ArtistSearchQuery(string searchQuery) => this.searchQuery = searchQuery;

        public async IAsyncEnumerable<FullArtist> ExecuteAsync(ISpotifyClient client)
        {
            SearchResponse response = await client.Search.Item(new SearchRequest(SearchRequest.Types.Artist, searchQuery));

            await foreach (FullArtist artist in client.Paginate(response.Artists, response => response.Artists).Take(ITEM_LIMIT))
                yield return artist;
        }
    }
}
