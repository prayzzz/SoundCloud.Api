using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SoundCloud.Api;
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
                var users = await client.Users.GetAsync(new UserQueryBuilder { SearchString = "diplo" });
                var diplo = users.First();

                Console.WriteLine($"Found: {diplo.Username} @ {diplo.PermalinkUrl}");

                var tracks = await client.Users.GetTracksAsync(diplo, 10);

                Console.WriteLine();
                Console.WriteLine("Latest 10 Tracks:");
                foreach (var track in tracks)
                {
                    Console.WriteLine(track.Title);
                }
            }
        }
    }
}