using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using BeestjeOpJeFeestje.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RepositoryTest
    {
        IBeastRepository _beastRepo;
        Mock<BeesteOpJeFeestjeEntities> db;
        [TestInitialize]
        public void Init()
        {
            db = new Mock<BeesteOpJeFeestjeEntities>();
            
            
        }
        [TestMethod]
        public void BeastsAvailable_ReturnsAdder_Test()
        {
            //1. Arrange
            var testList = new List<Beast>(GetBeasts().ToList());
            var dbSetMock = new Mock<DbSet<Beast>>();
            dbSetMock.As<IQueryable<Beast>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Beast>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Beast>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Beast>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());
            db.Setup(x => x.Set<Beast>()).Returns(dbSetMock.Object);

            //2. Act
            _beastRepo = new BeastRepository(db.Object);
            _beastRepo.ExcludeDesert = true;
            _beastRepo.ExcludeFarm = true;
            _beastRepo.ExcludePinguin = true;
            _beastRepo.ExcludePolarLion = true;
            _beastRepo.ExcludeSnow = true;
            var result = _beastRepo.BeastsAvailable(DateTime.Now).ToList();

            //3. Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Adder", result.First().Name);
        }
        private List<Beast> GetBeasts()
        {
            List<Beast> list = new List<Beast>
        {
            new Beast
            {
                ID=1,
                Name="Leeuw",
                Type="Jungle"
            },
            new Beast
            {
                ID=2,
                Name="Koe",
                Type="Boerderij"
            },
            new Beast
            {
                 ID=3,
                Name="Pinguin",
                Type="Sneeuw"
            },
            new Beast
            {
                ID=4,
                Name="Kameel",
                Type="Woestijn"
            },
            new Beast
            {
                ID=5,
                Name="Adder",
                Type="Jungle"
            },
            new Beast
            {
                ID=6,
                Name="Ijsbeer",
                Type="Sneeuw"
            }

        };
            return list;

        }
    }

    
}
