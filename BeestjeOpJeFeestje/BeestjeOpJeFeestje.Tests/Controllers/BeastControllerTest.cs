using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using BeestjeOpJeFeestje.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BeastControllerTest
    {

        private Mock<IBoekingRepository> _boekingsRepository;
        private Mock<IBeastRepository> _beastRepository;
        private Mock<IAccessoryRepository> _accessoryRepository;
        private BeastController _beastcontroller;
        [TestInitialize]
        public void Init()
        {
            _boekingsRepository = new Mock<IBoekingRepository>();
            _beastRepository = new Mock<IBeastRepository>();
            _accessoryRepository = new Mock<IAccessoryRepository>();
        }
        [TestMethod]
        public void CreateBeast_BeastIsAdded_Test()
        {
            //1. Arrange
            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);

            var Beast = new BeastVM { Name = "Leeuw" };

            //2. Act
            _beastcontroller.Create(Beast);

            //3.Assert

            _beastRepository.Verify(b => b.Add(Beast.Beast), Times.Once());
        }

        [TestMethod]
        public void EditBeast_ReturnsInput_Test()
        {
            //1. Arrange

            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);

            var Beast = new BeastVM { ID = 1, Name = "Leeuw", Type = "Jungle" };
            _beastRepository.Setup(b => b.Get(Beast.ID)).Returns(Beast.Beast);
            _beastRepository.Setup(b => b.ContextDB()).Returns(new Domain.BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_beastcontroller.Edit(Beast.ID);
            var SameBeast = (BeastVM)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(Beast.ID, SameBeast.ID);
            Assert.AreEqual(Beast.Name, SameBeast.Name);
            Assert.AreEqual(Beast.Type, SameBeast.Type);

        }

        [TestMethod]
        public void CreateBeast_ReturnsBeast_Test()
        {
            //1. Arrange
            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);
            _beastcontroller.ModelState.AddModelError("test", "test");
            var Beast = new BeastVM { ID = 1, Name = "Leeuw", Type = "Jungle" };
            _beastRepository.Setup(b => b.ContextDB()).Returns(new Domain.BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_beastcontroller.Create(Beast);
            var SameBeast = (BeastVM)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(Beast.ID, SameBeast.ID);
        }

        [TestMethod]
        public void EditBeast_BeastIsUpdated_Test()
        {
            //1. Arrange
            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);

            var Beast = new BeastVM { Name = "Leeuw" };

            //2. Act
            _beastcontroller.Edit(Beast);

            //3.Assert

            _beastRepository.Verify(b => b.UpdateBeast(Beast), Times.Once());
        }

        [TestMethod]
        public void EditBeast_ReturnsBeast_Test()
        {
            //1. Arrange
            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);
            _beastcontroller.ModelState.AddModelError("test", "test");
            var Beast = new BeastVM { ID = 1, Name = "Leeuw", Type = "Jungle" };
            _beastRepository.Setup(b => b.ContextDB()).Returns(new Domain.BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_beastcontroller.Edit(Beast);
            var SameBeast = (BeastVM)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(Beast.ID, SameBeast.ID);
        }

        [TestMethod]
        public void IndexBeast_ReturnsBeastList_Test()
        {
            //1. Arrange
            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);
            var beast = new BeastVM { ID = 1, Name = "Leeuw", Type = "Jungle" };
            var List = new List<Beast>();
            List.Add(beast.Beast);
            _beastRepository.Setup(b => b.GetAll()).Returns(List);

            //2. Act
            var result = (ViewResult)_beastcontroller.Index();
            var SameList = (List<BeastVM>)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(List[0], SameList[0].Beast);
        }

        [TestMethod]
        public void DetailsBeast_ReturnsInput_Test()
        {
            //1. Arrange

            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);

            var Beast = new BeastVM { ID = 1, Name = "Leeuw", Type = "Jungle" };
            _beastRepository.Setup(b => b.Get(Beast.ID)).Returns(Beast.Beast);
            _beastRepository.Setup(b => b.ContextDB()).Returns(new BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_beastcontroller.Details(Beast.ID);
            var SameBeast = (BeastVM)result.ViewData.Model;
            //3.Assert

            Assert.AreEqual(Beast.ID, SameBeast.ID);
            Assert.AreEqual(Beast.Name, SameBeast.Name);
            Assert.AreEqual(Beast.Type, SameBeast.Type);

        }

        [TestMethod]
        public void DeleteBeast_ReturnsInput_Test()
        {
            //1. Arrange

            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);

            var Beast = new BeastVM { ID = 1, Name = "Leeuw", Type = "Jungle" };
            _beastRepository.Setup(b => b.Get(Beast.ID)).Returns(Beast.Beast);
            _beastRepository.Setup(b => b.ContextDB()).Returns(new BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_beastcontroller.Delete(Beast.ID);
            var SameBeast = (BeastVM)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(Beast.ID, SameBeast.ID);
            Assert.AreEqual(Beast.Name, SameBeast.Name);
            Assert.AreEqual(Beast.Type, SameBeast.Type);
        }

        [TestMethod]
        public void DeleteConfirmed_RemovesBeast_Test()
        {
            //1. Arrange

            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);

            var Beast = new BeastVM { ID = 1, Name = "Leeuw", Type = "Jungle" };
            _beastRepository.Setup(b => b.Get(Beast.ID)).Returns(Beast.Beast);
            _beastRepository.Setup(b => b.ContextDB()).Returns(new BeesteOpJeFeestjeEntities());

            //2. Act
            _beastcontroller.DeleteConfirmed(Beast.ID);
            //3.Assert

            _beastRepository.Verify(b => b.Remove(Beast.Beast), Times.Once());
        }


    }
}
