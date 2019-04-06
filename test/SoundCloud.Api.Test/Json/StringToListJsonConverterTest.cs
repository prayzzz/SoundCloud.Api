using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Test.Json
{
    [TestFixture]
    public class StringToListJsonConverterTest
    {
        private class TestClass
        {
            [JsonConverter(typeof(StringToListJsonConverter), ' ', '"')]
            public List<string> StringList { get; set; }
        }

        private class TestClass2
        {
            [JsonConverter(typeof(StringToListJsonConverter))]
            public List<string> StringList { get; set; }
        }

        [Test]
        public void Test_Read()
        {
            const string json = "{ \"StringList\" : \"Item1 \\\"Item with spaces\\\" \\\"Item with, comma\\\" Item2 Item3\"}";

            var testObject = JsonConvert.DeserializeObject<TestClass>(json);

            Assert.That(testObject.StringList[0], Is.EqualTo("Item1"));
            Assert.That(testObject.StringList[1], Is.EqualTo("Item with spaces"));
            Assert.That(testObject.StringList[2], Is.EqualTo("Item with, comma"));
            Assert.That(testObject.StringList[3], Is.EqualTo("Item2"));
            Assert.That(testObject.StringList[4], Is.EqualTo("Item3"));
        }

        [Test]
        public void Test_Read_Default_Ctor()
        {
            const string json = "{ \"StringList\" : \"Item1,\\\"Item with spaces\\\",\\\"Item with, comma\\\",Item2,Item3\"}";

            var testObject = JsonConvert.DeserializeObject<TestClass2>(json);

            Assert.That(testObject.StringList[0], Is.EqualTo("Item1"));
            Assert.That(testObject.StringList[1], Is.EqualTo("Item with spaces"));
            Assert.That(testObject.StringList[2], Is.EqualTo("Item with, comma"));
            Assert.That(testObject.StringList[3], Is.EqualTo("Item2"));
            Assert.That(testObject.StringList[4], Is.EqualTo("Item3"));
        }

        [Test]
        public void Test_Read_Wrong_Type()
        {
            const string json = "{ \"StringList\" : \"Item1,\\\"Item with spaces\\\",\\\"Item with, comma\\\",Item2,Item3\"}";

            var ex = Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<WrongPropertyTypeClass>(json));
            Assert.That(ex.Message, Is.EqualTo("Expected type is not List<string>"));
        }

        [Test]
        public void Test_Write()
        {
            var testObject = new TestClass { StringList = new List<string> { "Item1", "Item with spaces", "Item with, comma", "Item2", "Item3" } };

            var json = JsonConvert.SerializeObject(testObject);

            Assert.That(json, Does.Contain("Item1 \\\"Item with spaces\\\" \\\"Item with, comma\\\""));
        }

        [Test]
        public void Test_Write_Default_Ctor()
        {
            var testObject = new TestClass2 { StringList = new List<string> { "Item1", "Item with spaces", "Item with, comma", "Item2", "Item3" } };

            var json = JsonConvert.SerializeObject(testObject);

            Assert.That(json, Does.Contain("Item1,Item with spaces,\\\"Item with, comma\\\",Item2,Item3"));
        }

        [Test]
        public void Test_Write_Wrong_Type()
        {
            var testObject = new WrongPropertyTypeClass { StringList = "I'm a String" };

            var json = JsonConvert.SerializeObject(testObject);

            Assert.That(json, Is.EqualTo("{\"StringList\":null}"));
        } // ReSharper disable UnusedAutoPropertyAccessor.Local
        private class WrongPropertyTypeClass
        {
            [JsonConverter(typeof(StringToListJsonConverter))]
            public string StringList { get; set; }
        }
    }
}