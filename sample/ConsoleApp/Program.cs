using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SoundCloud.Api;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.QueryBuilders;

namespace ConsoleApp
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSoundCloudClient(string.Empty, args[0]);

            using (var provider = serviceCollection.BuildServiceProvider())
            {
                var client = provider.GetService<SoundCloudClient>();

                var entity = await client.Resolve.GetEntityAsync("https://soundcloud.com/diplo");
                if (entity.Kind != Kind.User)
                {
                    Console.WriteLine("Couldn't resolve account of diplo");
                    return;
                }

                var diplo = entity as User;
                Console.WriteLine($"Found: {diplo.Username} @ {diplo.PermalinkUrl}");

                var tracks = await client.Users.GetTracksAsync(diplo, 10);

                Console.WriteLine();
                Console.WriteLine("Latest 10 Tracks:");
                foreach (var track in tracks)
                {
                    Console.WriteLine(track.Title);
                }

                var majorLazerResults = await client.Tracks.GetAllAsync(new TrackQueryBuilder { SearchString = "Major Lazer", Limit = 10 });
                Console.WriteLine();
                Console.WriteLine("Found Major Lazer Tracks:");
                foreach (var track in majorLazerResults)
                {
                    Console.WriteLine(track.Title);
                }
            }
        }
    }
}