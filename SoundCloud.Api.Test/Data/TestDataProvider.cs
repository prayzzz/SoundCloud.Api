using System.IO;
using System.Reflection;

namespace SoundCloud.Api.Test.Data
{
    public static class TestDataProvider
    {
        private const string ArtworkPath = "SoundCloud.Api.Test.Data.artwork.jpeg";
        private const string FollowingsPath = "SoundCloud.Api.Test.Data.followings.json";
        private const string SoundPath = "SoundCloud.Api.Test.Data.artwork.jpeg";
        private const string UserPath = "SoundCloud.Api.Test.Data.user.json";

        public static Stream GetArtwork() => GetEmbeddedFile(ArtworkPath);

        public static string GetFollowings() => new StreamReader(GetEmbeddedFile(FollowingsPath)).ReadToEnd();

        public static Stream GetSound() => GetEmbeddedFile(SoundPath);

        public static string GetUser() => new StreamReader(GetEmbeddedFile(UserPath)).ReadToEnd();

        private static Stream GetEmbeddedFile(string path) => Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
    }
}