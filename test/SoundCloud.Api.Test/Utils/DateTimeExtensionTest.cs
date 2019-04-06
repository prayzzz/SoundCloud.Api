using System;
using NUnit.Framework;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Test.Utils
{
    [TestFixture]
    public class DateTimeExtensionTest
    {
        [Test]
        public void Test_ToSoundCloudString()
        {
            var dateTime = DateTime.Now;
            var soundCloudString = dateTime.ToSoundCloudString();
            var expectedString = dateTime.ToString(Settings.SoundCloudDateTimePattern);

            Assert.That(soundCloudString, Is.EqualTo(expectedString));
        }
    }
}