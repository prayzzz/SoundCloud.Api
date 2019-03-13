using System;

using NUnit.Framework;

using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Test.QueryBuilders
{
    [TestFixture]
    public class TrackQueryBuilderTest
    {
        [Test]
        public void Test_Bpm()
        {
            var builder = new TrackQueryBuilder();
            builder.BpmFrom = 100;
            builder.BpmTo = 120;

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?bpm[from]=100&bpm[to]=120"));
        }

        [Test]
        public void Test_CreatedFromTo()
        {
            var builder = new TrackQueryBuilder();
            builder.CreatedAtFrom = new DateTime(2015, 01, 02, 03, 04, 05);
            builder.CreatedAtTo = new DateTime(2015, 06, 07, 08, 09, 10);

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?created_at[from]=2015-01-02 03:04:05&created_at[to]=2015-06-07 08:09:10"));
        }

        [Test]
        public void Test_Default()
        {
            var builder = new TrackQueryBuilder();

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?"));
        }

        [Test]
        public void Test_Duration()
        {
            var builder = new TrackQueryBuilder();
            builder.DurationFrom = 100;
            builder.DurationTo = 200;

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?duration[from]=100&duration[to]=200"));
        }

        [Test]
        public void Test_Genres()
        {
            var builder = new TrackQueryBuilder();
            builder.Genres.Add("pop");
            builder.Genres.Add("rap");
            builder.Genres.Add("house");

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?genres=pop%2Crap%2Chouse"));
        }

        [Test]
        public void Test_Ids()
        {
            var builder = new TrackQueryBuilder();
            builder.Ids.Add(1);
            builder.Ids.Add(2);
            builder.Ids.Add(3);

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?ids=1%2C2%2C3"));
        }

        [Test]
        public void Test_License()
        {
            var builder = new TrackQueryBuilder();
            builder.License = License.CcBy;

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?license=cc-by"));
        }

        [Test]
        public void Test_SearchString()
        {
            var builder = new TrackQueryBuilder();
            builder.SearchString = "major lazer";

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?q=major lazer"));
        }

        [Test]
        public void Test_Sharing()
        {
            var builder = new TrackQueryBuilder();
            builder.Sharing = Sharing.Private;

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?filter=private"));
        }

        [Test]
        public void Test_Tags()
        {
            var builder = new TrackQueryBuilder();
            builder.Tags.Add("pop");
            builder.Tags.Add("rap");
            builder.Tags.Add("house");

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?tags=pop%2Crap%2Chouse"));
        }

        [Test]
        public void Test_TrackTypes()
        {
            var builder = new TrackQueryBuilder();
            builder.TrackTypes.Add(TrackType.Demo);
            builder.TrackTypes.Add(TrackType.InProgress);
            builder.TrackTypes.Add(TrackType.Live);

            var query = builder.BuildUri();

            Assert.That(query.ToString(), Is.EqualTo("https://api.soundcloud.com/?types=demo%2Cin progress%2Clive"));
        }
    }
}