using System;
using NUnit.Framework;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Test.QueryBuilders
{
    [TestFixture]
    public class PlaylistQueryBuilderTest
    {
        [Test]
        // ReSharper disable ObjectCreationAsStatement
        public void Test_Empty_SearchString_In_Constructor()
        {
            Assert.Throws<ArgumentException>(() => new PlaylistQueryBuilder(""));
        }

        [Test]
        public void Test_Paged()
        {
            var builder = new PlaylistQueryBuilder("SearchString");
            builder.Paged = true;

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?limit=10&q=SearchString&linked_partitioning=1"));
        }

        [Test]
        public void Test_Paged_Empty_SearchString()
        {
            var builder = new PlaylistQueryBuilder("SearchString");
            builder.Paged = true;
            builder.SearchString = string.Empty;

            Assert.Throws<ArgumentException>(() => builder.BuildUri());
        }

        [Test]
        public void Test_RepresentationMode()
        {
            var builder = new PlaylistQueryBuilder("SearchString");
            builder.Representation = RepresentationMode.Compact;

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?q=SearchString&representation=compact"));
        }

        [Test]
        public void Test_SearchString()
        {
            var builder = new PlaylistQueryBuilder("SearchString");

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?q=SearchString"));
        }
    }
}