using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using NUnit.Framework;
using Moq;
using Redfin.DataAccess.Urls;
using Redfin.DataAccess.DB;
using Redfin.DataAccess;

namespace Test.Redfin.DataAccess
{
    [TestFixture]
    public class ImporterTest
    {
        [Test]
        [Category("NotImplemented")]
        public void Import()
        {
            // Arrange
            var path = @"C:\Users\ralph.joachim\Desktop\3153\Desktop\NotesArchive\R\MachineLearningWithR\chapter 3\problems\01.redfin\issaquahurls.txt";
            var readerMock = new Mock<UrlsReader>(path);
            readerMock.Setup(x => x.Read()).Returns(
                  new List<Tuple<Uri,string>>()
                  {
                      new Tuple<Uri,string>(
                          new Uri("http://yourwebsite.com"),
                          "like"),

                       new Tuple<Uri,string>(
                          new Uri("http://mywebsite.com"),
                          "dislike")
                  }
                );

            var contextMock = new Mock<RedfinContext>();
            var propertiesMock = new Mock<DbSet<Property>>();


            propertiesMock.Setup(x => x.Add(It.IsAny<Property>())).Returns((Property u) => u);
            contextMock.Setup(x => x.Properties).Returns(propertiesMock.Object);

            var importerMock = new Mock<Importer>(readerMock.Object, contextMock.Object);
            importerMock.Setup(x => x.AddRecord(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<RedfinContext>()));

            // Act

            var _instance = importerMock.Object;
            _instance.Run();

            // Assert

            contextMock.Verify(x => x.SaveChanges());

            importerMock.Verify(
                x => x.AddRecord(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<RedfinContext>()),
                Times.Once);
        }        
    }
}
