# New Music Bot

A discord bot for getting notifications about new music, powered by Spotify, Azure Cosmos DB, and .NET Core.

## Installation
Prerequisites:
- Linux host with Docker installed
- Azure Cosmos DB Instance (and connection string)
- Spotify client id and secret
- Discord Bot API Key

Once you have all of the above, start the bot with this command:
``` bash
sudo docker run \
    --name newmusicbot -d \
    moutansos/newmusicbot:latest \
    -e CosmosConnectionString="<<cosmos db connection string" \
    -e SpotifyClientId="<<spotify client id>>" \
    -e SpotifyClientSectret="<<spotify client secret>>" \
    -e CheckSchedule="* */30 * * * *" \ #every thirty minutes
    -e CheckInterval="10" \
    -e DiscordToken="<<discord api key>>" 
```

After this, use the Discord developer portal to generate a link to add the bot to a server.

## Usage

All commands for the bot start with the "!" character. The different sub-commands are listed below. After an artist has been registered with the bot in a channel, the bot will reach out to spotify on the schedule and notify the user of any new releases, and log them to the database for later comparison.

### Ping

This lets you test if the bot is alive and responding to commands.  
Example:

```
!ping
```

### Add Artist

This command initiates a search for an Artist and returns the ones that may be relevant to the search criteria.  
Example:
```
!add artist Pink Floyd
```
The bot will then respond with the top 5 items it finds via the Spotify search API for you to choose from.

### Select Artist

This command selects and artist that out of the list of artists that was returned from the search via the "!add artist" command. It selects the artist based on the number 1-5 for the search result.  
Example:
```
!select artist 1
```
This selects to top item in the search results and starts tracking that artist for releases

### List Artists

This command lets you see what artists the channel is currently subscribed to. It will return a list of names and the artist's ids.  
Example:
```
!list artists
```

### Remove Artist

This command removes an artist and stops checking if there are new releases for that artist.  
Example:
```
!remove artist <<artist id>>
```


## Technologies

- Software Platform: .NET Core 3 Worker Service  
- Audio Data API: [SpotifyAPI-NET](https://github.com/JohnnyCrazy/SpotifyAPI-NET)
- Database: [Azure Cosmos DB](https://azure.microsoft.com/en-us/services/cosmos-db/)
