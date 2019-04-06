using System.Text;
using NUnit.Framework;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Test.Utils
{
    [TestFixture]
    public class ValidationMessagesTest
    {
        [Test]
        public void Test_Add_String()
        {
            var errors = new ValidationMessages();
            errors.Add("error!");

            Assert.That(errors.ToString(), Is.EqualTo("error!"));
        }

        [Test]
        public void Test_Add_StringBuilder()
        {
            var errors = new ValidationMessages();
            errors.Add(new StringBuilder("error!"));

            Assert.That(errors.ToString(), Is.EqualTo("error!"));
        }

        [Test]
        public void Test_HasErrors()
        {
            var errors = new ValidationMessages();
            errors.Add("error!");

            Assert.That(errors.HasErrors, Is.True);
        }
    }
}