using NUnit.Framework;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Test.QueryBuilders
{
    [TestFixture]
    public class SoundCloudQueryBuilderTest
    {
        private class TestQueryBuilder : SoundCloudQueryBuilder
        {
            public TestQueryBuilder(int customMaxLimit)
            {
                CustomMaxLimit = customMaxLimit;
            }

            public int CustomLimit => CustomMaxLimit;
        }

        [Test]
        public void Test_Limit_CustomMaxValue()
        {
            var builder = new TestQueryBuilder(78) { Limit = 100 };

            Assert.That(builder.Limit, Is.EqualTo(78));
            Assert.That(builder.CustomLimit, Is.EqualTo(78));
        }

        [Test]
        public void Test_Limit_CustomMaxValue_To_Large([Values(201, 205)] int customMaxLimit)
        {
            var builder = new TestQueryBuilder(customMaxLimit) { Limit = 300 };

            Assert.That(builder.Limit, Is.EqualTo(200));
            Assert.That(builder.CustomLimit, Is.EqualTo(200));
        }

        [Test]
        public void Test_Limit_CustomMaxValue_To_Small([Values(0, -5)] int customMaxLimit)
        {
            var builder = new TestQueryBuilder(customMaxLimit) { Limit = 10 };

            Assert.That(builder.Limit, Is.EqualTo(1));
            Assert.That(builder.CustomLimit, Is.EqualTo(1));
        }

        [Test]
        public void Test_Limit_MaxValue([Values(201, 202, 300, 400)] int limit)
        {
            var builder = new TrackQueryBuilder { Limit = limit };

            Assert.That(builder.Limit, Is.EqualTo(200));
        }

        [Test]
        public void Test_Limit_MinValue([Values(0, -1, -200, -300)] int limit)
        {
            var builder = new TrackQueryBuilder { Limit = limit };

            Assert.That(builder.Limit, Is.EqualTo(1));
        }

        [Test]
        public void Test_Limit([Values(1, 2, 100, 199, 200)] int limit)
        {
            var builder = new TrackQueryBuilder { Limit = limit };

            Assert.That(builder.Limit, Is.EqualTo(limit));
        }

        [Test]
        public void Test_Offset_MinValue([Values(0, -1, -200, -300)] int offset)
        {
            var builder = new TrackQueryBuilder { Offset = offset };

            Assert.That(builder.Offset, Is.EqualTo(1));
        }

        [Test]
        public void Test_Offset([Values(1, 2, 100, 199, 200)] int offset)
        {
            var builder = new TrackQueryBuilder { Offset = offset };

            Assert.That(builder.Offset, Is.EqualTo(offset));
        }
    }
}