using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Utils
{
    public static class CosmosUtils
    {
        public static async IAsyncEnumerable<T> AsEnumerableAsync<T>(this FeedIterator<T> feed)
        {
            while (feed.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await feed.ReadNextAsync();
                foreach (T record in currentResultSet)
                    yield return record;
            }
        }
    }
}
