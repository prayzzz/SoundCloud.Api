using NUnit.Framework;

using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Test.QueryBuilders
{
    public class GroupQueryBuilderTest
    {
        [Test]
        public void Test_No_Parameters()
        {
            var builder = new GroupQueryBuilder();

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?"));
        }

        [Test]
        public void Test_Paged()
        {
            var builder = new GroupQueryBuilder();
            builder.Paged = true;

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?limit=50&linked_partitioning=1"));
        }

        [Test]
        public void Test_SearchString()
        {
            var builder = new GroupQueryBuilder();
            builder.SearchString = "SearchString";

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?q=SearchString"));
        }
    }
}