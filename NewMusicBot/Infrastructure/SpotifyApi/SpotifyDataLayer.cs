using NewMusicBot.Infrastructure.SpotifyApi.Queries;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.Infrastructure.SpotifyApi
{
    public interface ISpotifyDataLayer
    {
        T ExecuteQuery<T>(ISpotifyQuery<T> query);
    }

    public class SpotifyDataLayer : ISpotifyDataLayer
    {
        private readonly ISpotifyClient client;

        public SpotifyDataLayer(ISpotifyClient client) => 
            this.client = client;

        public T ExecuteQuery<T>(ISpotifyQuery<T> query) => query.ExecuteAsync(client);
    }
}
