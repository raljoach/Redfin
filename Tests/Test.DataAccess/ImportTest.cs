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
            readerMock.Setup(x => x.ReadLine()).Returns(
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
                //It.Is<string[]>(h => EvaluateHeaders(h)),
                //It.Is<string[]>(v => EvaluateValues(v)),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<RedfinContext>()));

            // Act

            var _instance = importerMock.Object;
            _instance.Run(/*false*/);

            // Assert

            contextMock.Verify(x => x.SaveChanges());

            importerMock.Verify(
                x => x.AddRecord(
                    //It.IsAny<string[]>(),
                    //It.IsAny<string[]>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<RedfinContext>()),
                Times.Once);
        }
        /*
        [Test]
        [Category("NotImplemented")]
        public void ImportParallel()
        {
            throw new NotImplementedException();
        }

        private bool EvaluateValues(string[] values)
        {
            var expected = new string[] {
                "1","O5440207","ACT","141 E 12TH ST","APOPKA","32703","CLARKSVILLE","1,144","$37,000","3","1","0","1959","None","Single Family Home","$1,298","1","1","","","","$32.34","","","CHARLES RUTENBERG REALTY","Sara Nicholas","269503895","","","","O","Orlando Regional"
            };

            Assert.AreEqual(expected.Length, values.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], values[i]);
            }
            return true;
        }

        private bool EvaluateHeaders(string[] headers)
        {
            var expected = new string[] {
                "#","ML Number","Status","Address","City","Postal Code","Legal Subdivision Name","Sq Ft Heated","Current Price","Beds","Full Baths","Half Baths","Year Built","Pool","Property Style","Taxes","CDOM","ADOM","Days to Contract","Sold Terms","Close Date","LP/SqFt","SP/SqFt","SP/LP","List Office Name","List Agent Full Name","List Agent ID","Selling Agent Name","Selling Office ID","Selling Agent ID","LSC List Side","Office Primary Board ID"
            };

            Assert.AreEqual(expected.Length, headers.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], headers[i]);
            }
            return true;
        }

        [Test]
        [Category("Regression")]
        public void AddRecord()
        {
            var data = new StringDictionary();
            var list = new List<dynamic>
            {
                //{ "#", "a" },
                new {Field="ML Number",Value="a", Invoke= (Func<Listing,object>)((Listing t) => { return t.MLNumber; }) },
                new {Field="Status",Value="b" , Invoke= (Func<Listing,object>)((Listing t) => { return t.Status; }) },
                new {Field="Address",Value="c" , Invoke= (Func<Listing,object>)((Listing t) => { return t.Address; }) },
                new {Field="City",Value="d" , Invoke= (Func<Listing,object>)((Listing t) => { return t.City; }) },
                new {Field="Postal Code",Value="e" , Invoke= (Func<Listing,object>)((Listing t) => { return t.PostalCode; }) },
                new {Field="Legal Subdivision Name",Value="f" , Invoke= (Func<Listing,object>)((Listing t) => { return t.LegalSubdivisionName; }) },
                new {Field="Sq Ft Heated",Value="3" , Invoke= (Func<Listing,object>)((Listing t) => { return t.SqFtHeated; }) },
                new {Field="Current Price",Value="$12.99" , Invoke= (Func<Listing,object>)((Listing t) => { return t.CurrentPrice; }) },
                new {Field="Beds",Value="4" , Invoke= (Func<Listing,object>)((Listing t) => { return t.Beds; }) },
                new {Field= "Full Baths",Value="15" , Invoke= (Func<Listing,object>)((Listing t) => { return t.FullBaths; }) },
                new {Field="Half Baths",Value="3" , Invoke= (Func<Listing,object>)((Listing t) => { return t.HalfBaths; }) },
                new {Field="Year Built",Value="1981" , Invoke= (Func<Listing,object>)((Listing t) => { return t.YearBuilt; }) },
                new {Field="Pool",Value="No" , Invoke= (Func<Listing,object>)((Listing t) => { return t.Pool; }) },
                new {Field="Property Style",Value="Ranch" , Invoke= (Func<Listing,object>)((Listing t) => { return t.PropertyStyle; }) },
                new {Field="Taxes",Value="$1,299.43" , Invoke= (Func<Listing,object>)((Listing t) => { return t.Taxes; }) },
                new {Field= "CDOM",Value="223" , Invoke= (Func<Listing,object>)((Listing t) => { return t.CDOM; }) },
                new {Field="ADOM",Value="15" , Invoke= (Func<Listing,object>)((Listing t) => { return t.ADOM; }) },
                new {Field="Days to Contract",Value="3" , Invoke= (Func<Listing,object>)((Listing t) => { return t.DaystoContract; }) },
                new {Field="Sold Terms",Value="hello" , Invoke= (Func<Listing,object>)((Listing t) => { return t.SoldTerms; }) },
                new {Field="Close Date",Value="11/23/2081" , Invoke= (Func<Listing,object>)((Listing t) => { return t.CloseDate; }) },
                new {Field="LP/SqFt",Value="$17.21" , Invoke= (Func<Listing,object>)((Listing t) => { return t.LPSqFt; }) },
                new {Field="SP/SqFt",Value="$15.22" , Invoke= (Func<Listing,object>)((Listing t) => { return t.SPSqFt; }) },
                new {Field="SP/LP",Value=".81" , Invoke= (Func<Listing,object>)((Listing t) => { return t.SPLP; }) },
                new {Field="List Office Name",Value="haha" , Invoke= (Func<Listing,object>)((Listing t) => { return t.ListOfficeName; }) },
                new {Field="List Agent Full Name",Value="meknow" , Invoke= (Func<Listing,object>)((Listing t) => { return t.ListAgentFullName; }) },
                new {Field="List Agent ID",Value="43" , Invoke= (Func<Listing,object>)((Listing t) => { return t.ListAgentID; }) },
                new {Field="Selling Agent Name",Value="jajaja" , Invoke= (Func<Listing,object>)((Listing t) => { return t.SellingAgentName; }) },
                new {Field="Selling Office ID",Value="3" , Invoke= (Func<Listing,object>)((Listing t) => { return t.SellingOfficeID; }) },
                new {Field="Selling Agent ID",Value="433" , Invoke= (Func<Listing,object>)((Listing t) => { return t.SellingAgentID; }) },
                new {Field="LSC List Side",Value="cool" , Invoke= (Func<Listing,object>)((Listing t) => { return t.LSCListSide; }) },
                new {Field="Office Primary Board ID",Value="83838" , Invoke= (Func<Listing,object>)((Listing t) => { return t.OfficePrimaryBoardID; }) },
                new {Field="County", Value="Suffolk", Invoke= (Func<Listing,object>)((Listing t) => { return t.County; }) }
            };
            string[] headers = new string[list.Count];
            string[] values = new string[list.Count];
            var i = 0;
            foreach (dynamic d in list)
            {
                data.Add(headers[i] = d.Field, values[i++] = d.Value);
            }


            // Arrange

            var contextMock = new Mock<MlsContext>();
            var listingData = new List<Listing> {
                                  new Listing() { MLNumber = "123" }
                               }.AsQueryable();

            var mockListingsSet = new Mock<DbSet<Listing>>();

            mockListingsSet.As<IQueryable<Listing>>().Setup(m => m.Provider).Returns(listingData.Provider);
            mockListingsSet.As<IQueryable<Listing>>().Setup(m => m.Expression).Returns(listingData.Expression);
            mockListingsSet.As<IQueryable<Listing>>().Setup(m => m.ElementType).Returns(listingData.ElementType);
            mockListingsSet.As<IQueryable<Listing>>().Setup(m => m.GetEnumerator()).Returns(listingData.GetEnumerator());

            contextMock.Setup(m => m.Listings.Find(It.IsAny<object[]>())).Returns(new Listing() { MLNumber = "123" });

            contextMock.Setup(e => e.Listings).Returns(mockListingsSet.Object);

            // Act

            var row = new Importer(null, //contextMock.Object
            false).AddRecord(
                headers,
                values);

            // Assert

            Assert.IsNotNull(row);

            foreach (dynamic o in list)
            {
                Assert.AreEqual(
                    o.Value,
                    o.Invoke(row));
            }

            mockListingsSet.Verify(
              x =>
                x.Add(
                  It.Is<Listing>(
                    u => Evaluate(u, list)
                  )),

                Times.Once);
        }

        private bool Evaluate(Listing u, List<dynamic> list)
        {
            foreach (var o in list)
            {
                Assert.AreEqual(
                    o.Value,
                    o.Invoke(u));
            }
            return true;
        }*/
    }
}
