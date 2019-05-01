using NUnit.Framework;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Test.QueryBuilders
{
    [TestFixture]
    public class PlaylistQueryBuilderTest
    {
        [Test]
        public void Test_Paged()
        {
            var builder = new PlaylistQueryBuilder { SearchString = "SearchString", Paged = true };

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?limit=50&q=SearchString&linked_partitioning=1"));
        }

        [Test]
        public void Test_RepresentationMode()
        {
            var builder = new PlaylistQueryBuilder { Representation = RepresentationMode.Compact };

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?representation=compact"));
        }

        [Test]
        public void Test_SearchString()
        {
            var builder = new PlaylistQueryBuilder { SearchString = "SearchString" };

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?q=SearchString"));
        }
    }
}
