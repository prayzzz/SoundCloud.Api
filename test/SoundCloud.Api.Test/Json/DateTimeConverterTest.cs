using System;
using Newtonsoft.Json;
using NUnit.Framework;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Test.Json
{
    [TestFixture]
    public class DateTimeConverterTest
    {
        private const string DateTimeFormat = "yyyy/MM/dd HH:mm:ss zzz";

        private class TestClass
        {
            [JsonConverter(typeof(DateTimeConverter), DateTimeFormat)]
            public DateTime DateTime { get; set; }
        }

        [Test]
        public void Test_Read()
        {
            var dateTime = DateTime.Now;
            var dateTimeString = dateTime.ToString(DateTimeFormat);

            var json = "{ \"DateTime\":\"" + dateTimeString + "\" }";

            var testObject = JsonConvert.DeserializeObject<TestClass>(json);

            Assert.That(testObject.DateTime.Year, Is.EqualTo(dateTime.Year));
            Assert.That(testObject.DateTime.Month, Is.EqualTo(dateTime.Month));
            Assert.That(testObject.DateTime.Day, Is.EqualTo(dateTime.Day));
            Assert.That(testObject.DateTime.Hour, Is.EqualTo(dateTime.Hour));
            Assert.That(testObject.DateTime.Minute, Is.EqualTo(dateTime.Minute));
            Assert.That(testObject.DateTime.Second, Is.EqualTo(dateTime.Second));
        }

        [Test]
        public void Test_Write()
        {
            var dateTime = DateTime.Now;
            var dateTimeString = dateTime.ToString(DateTimeFormat);

            var testObject = new TestClass();
            testObject.DateTime = dateTime;

            var json = JsonConvert.SerializeObject(testObject);

            Assert.That(json, Does.Contain(dateTimeString));
        }
    }
}