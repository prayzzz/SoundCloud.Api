using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SoundCloud.Api;
using SoundCloud.Api.Utils;

// ReSharper disable PossibleNullReferenceException
namespace ConsoleApp1
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.Write("ClientId: ");
            var clientId = Console.ReadLine().Trim();

            Console.Write("ClientSecret: ");
            var clientSecret = Console.ReadLine().Trim();

            Console.Write("Username: ");
            var username = Console.ReadLine().Trim();

            Console.Write("Password: ");
            var password = Console.ReadLine().Trim();

            var credentials = await SoundCloudOAuth.FromPassword(clientId, clientSecret, username, password);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSoundCloudClient(new SoundCloudAuthInfo(credentials.AccessToken, null));
            using (var provider = serviceCollection.BuildServiceProvider())
            {
                var client = provider.GetService<SoundCloudClient>();

                Console.WriteLine();
                Console.WriteLine($"AccessToken: {client.AuthInfo.AccessToken}");
                Console.WriteLine($"ClientId: {client.AuthInfo.ClientId}");

                var tracks = await client.Me.GetTracksAsync(10);

                if (tracks.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine("Your latest Tracks:");
                    foreach (var track in tracks)
                    {
                        Console.WriteLine(track.Title);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("You don't have any tracks");
                }
            }
        }
    }
}
