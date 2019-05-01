using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SoundCloud.Api;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Base;
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

                Console.WriteLine("Search for:");
                Console.WriteLine("1: Tracks");
                Console.WriteLine("2: User");
                Console.WriteLine("3: Playlists");

                Console.WriteLine();
                Console.Write("Use: ");
                var answer = Console.ReadLine();

                Console.WriteLine();
                if (answer == "1")
                {
                    Console.Write("Search string: ");
                    var term = Console.ReadLine().Trim();

                    // Get first page of Tracks
                    var tracks = await client.Tracks.GetAllAsync(new TrackQueryBuilder { SearchString = term, Limit = 10 });
                    await PageThrough(tracks, t => $"{t.Title} by {t.User.Username}");
                }
                else if (answer == "2")
                {
                    Console.Write("Search string: ");
                    var term = Console.ReadLine().Trim();

                    // Get first page of Playlists
                    var users = await client.Users.GetAllAsync(new UserQueryBuilder { SearchString = term, Limit = 10 });
                    await PageThrough(users, u => u.Username);
                }
                else if (answer == "3")
                {
                    Console.Write("Search string: ");
                    var term = Console.ReadLine().Trim();

                    // Get first page of Users
                    var playlists = await client.Playlists.GetAllAsync(new PlaylistQueryBuilder { SearchString = term, Limit = 10 });
                    await PageThrough(playlists, p => $"{p.Title} by {p.User.Username}");
                }
                else
                {
                    Console.WriteLine($"Invalid answer: {answer}. Use the displayed number.");
                    Environment.Exit(1);
                }
            }
        }

        private static async Task PageThrough<T>(SoundCloudList<T> list, Func<T, string> selector) where T : Entity
        {
            Console.WriteLine();
            Console.WriteLine("Results:");

            while (true)
            {
                Console.WriteLine();

                foreach (var item in list)
                {
                    Console.WriteLine(selector(item));
                }

                if (!list.HasNextPage)
                {
                    Console.WriteLine();
                    Console.Write("No more items.");
                    return;
                }

                Console.WriteLine();
                Console.Write("Next page? [Y|n]: ");
                var answer = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(answer) || answer == "y")
                {
                    // Get next page of current list
                    list = await list.GetNextPageAsync();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
