using NUnit.Framework;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Test.QueryBuilders
{
    [TestFixture]
    public class UserQueryBuilderTest
    {
        [Test]
        public void Test_No_Parameters()
        {
            var builder = new UserQueryBuilder();

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?"));
        }

        [Test]
        public void Test_Paged()
        {
            var builder = new UserQueryBuilder();
            builder.Paged = true;
            builder.SearchString = "SearchString";

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?limit=200&offset=0&q=SearchString&linked_partitioning=1"));
        }

        [Test]
        public void Test_SearchString()
        {
            var builder = new UserQueryBuilder();
            builder.SearchString = "SearchString";

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?q=SearchString"));
        }
    }
}