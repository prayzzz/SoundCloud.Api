using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Moq;

using NUnit.Framework;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Test.Utils
{
    [TestFixture]
    public class SoundCloudListTest
    {
        [Test]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Test_Multiple_Take_Operations()
        {
            var firstPageUri = new Uri("http://1");
            var secondPageUri = new Uri("http://2");

            var firstPage = new Mock<IPagedResult<int>>();
            firstPage.Setup(x => x.next_href).Returns(secondPageUri);
            firstPage.Setup(x => x.HasNextPage).Returns(true);
            firstPage.Setup(x => x.collection).Returns(new List<int> {1, 2, 3});

            var secondPage = new Mock<IPagedResult<int>>();
            secondPage.Setup(x => x.next_href).Returns((Uri)null);
            secondPage.Setup(x => x.HasNextPage).Returns(false);
            secondPage.Setup(x => x.collection).Returns(new List<int> {4, 5, 6});

            var pages = new Dictionary<Uri, IPagedResult<int>>();
            pages.Add(firstPageUri, firstPage.Object);
            pages.Add(secondPageUri, secondPage.Object);

            var list = new SoundCloudList<int>(firstPageUri, x => pages[x]);

            var enumerable = list.Get();

            var batch1 = enumerable.Take(5).ToList();
            Assert.That(batch1.Count, Is.EqualTo(5));
            Assert.That(batch1[0], Is.EqualTo(1));
            Assert.That(batch1[4], Is.EqualTo(5));

            var batch2 = enumerable.Take(1).ToList();
            Assert.That(batch2.Count, Is.EqualTo(1));
            Assert.That(batch2[0], Is.EqualTo(1));
        }

        [Test]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Test_Skip_Take()
        {
            var firstPageUri = new Uri("http://1");
            var secondPageUri = new Uri("http://2");
            var thirdPageUri = new Uri("http://3");

            var firstPage = new Mock<IPagedResult<int>>();
            firstPage.Setup(x => x.next_href).Returns(secondPageUri);
            firstPage.Setup(x => x.HasNextPage).Returns(true);
            firstPage.Setup(x => x.collection).Returns(new List<int> {1, 2, 3});

            var secondPage = new Mock<IPagedResult<int>>();
            secondPage.Setup(x => x.next_href).Returns(thirdPageUri);
            secondPage.Setup(x => x.HasNextPage).Returns(true);
            secondPage.Setup(x => x.collection).Returns(new List<int> {4, 5, 6});

            var thirdPage = new Mock<IPagedResult<int>>();
            thirdPage.Setup(x => x.next_href).Returns((Uri)null);
            thirdPage.Setup(x => x.HasNextPage).Returns(false);
            thirdPage.Setup(x => x.collection).Returns(new List<int> {7, 8, 9});

            var pages = new Dictionary<Uri, IPagedResult<int>>();
            pages.Add(firstPageUri, firstPage.Object);
            pages.Add(secondPageUri, secondPage.Object);
            pages.Add(thirdPageUri, thirdPage.Object);

            var list = new SoundCloudList<int>(firstPageUri, x => pages[x]);

            var enumerable = list.Get();

            var batch1 = enumerable.Take(2).ToList();
            Assert.That(batch1.Count, Is.EqualTo(2));
            Assert.That(batch1[0], Is.EqualTo(1));
            Assert.That(batch1[1], Is.EqualTo(2));

            var batch2 = enumerable.Skip(2).Take(2).ToList();
            Assert.That(batch2.Count, Is.EqualTo(2));
            Assert.That(batch2[0], Is.EqualTo(3));
            Assert.That(batch2[1], Is.EqualTo(4));

            var batch3 = enumerable.Skip(4).Take(2).ToList();
            Assert.That(batch3.Count, Is.EqualTo(2));
            Assert.That(batch3[0], Is.EqualTo(5));
            Assert.That(batch3[1], Is.EqualTo(6));

            var batch4 = enumerable.Skip(6).Take(2).ToList();
            Assert.That(batch4.Count, Is.EqualTo(2));
            Assert.That(batch4[0], Is.EqualTo(7));
            Assert.That(batch4[1], Is.EqualTo(8));
        }

        [Test]
        public void Test_With_3_Pages()
        {
            var firstPageUri = new Uri("http://1");
            var secondPageUri = new Uri("http://2");
            var thirdPageUri = new Uri("http://3");

            var firstPage = new Mock<IPagedResult<int>>();
            firstPage.Setup(x => x.next_href).Returns(secondPageUri);
            firstPage.Setup(x => x.HasNextPage).Returns(true);
            firstPage.Setup(x => x.collection).Returns(new List<int> {1, 2, 3});

            var secondPage = new Mock<IPagedResult<int>>();
            secondPage.Setup(x => x.next_href).Returns(thirdPageUri);
            secondPage.Setup(x => x.HasNextPage).Returns(true);
            secondPage.Setup(x => x.collection).Returns(new List<int> {4, 5, 6});

            var thirdPage = new Mock<IPagedResult<int>>();
            thirdPage.Setup(x => x.next_href).Returns((Uri)null);
            thirdPage.Setup(x => x.HasNextPage).Returns(false);
            thirdPage.Setup(x => x.collection).Returns(new List<int> {7, 8, 9});

            var pages = new Dictionary<Uri, IPagedResult<int>>();
            pages.Add(firstPageUri, firstPage.Object);
            pages.Add(secondPageUri, secondPage.Object);
            pages.Add(thirdPageUri, thirdPage.Object);

            var list = new SoundCloudList<int>(firstPageUri, x => pages[x]);

            var result = list.Get().ToList();

            Assert.That(result.Count, Is.EqualTo(9));
            Assert.That(result[0], Is.EqualTo(1));
            Assert.That(result[8], Is.EqualTo(9));
        }

        [Test]
        public void Test_With_Second_Page_Empty()
        {
            var firstPageUri = new Uri("http://1");
            var secondPageUri = new Uri("http://2");

            var firstPage = new Mock<IPagedResult<int>>();
            firstPage.Setup(x => x.next_href).Returns(secondPageUri);
            firstPage.Setup(x => x.HasNextPage).Returns(true);
            firstPage.Setup(x => x.collection).Returns(new List<int> {1, 2, 3});

            var secondPage = new Mock<IPagedResult<int>>();
            secondPage.Setup(x => x.next_href).Returns((Uri)null);
            secondPage.Setup(x => x.HasNextPage).Returns(false);
            secondPage.Setup(x => x.collection).Returns(new List<int>());

            var pages = new Dictionary<Uri, IPagedResult<int>>();
            pages.Add(firstPageUri, firstPage.Object);
            pages.Add(secondPageUri, secondPage.Object);

            var list = new SoundCloudList<int>(firstPageUri, x => pages[x]);

            var result = list.Get().ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo(1));
            Assert.That(result[2], Is.EqualTo(3));
        }
    }
}