using System;
using System.Collections.Generic;
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
    public class AccessoryControllerTest
    {
        private Mock<IBoekingRepository> _boekingsRepository;
        private Mock<IAccessoryRepository> _accessoryRepository;
        private AccessoryController _accessorycontroller;
        [TestInitialize]
        public void Init()
        {
            _boekingsRepository = new Mock<IBoekingRepository>();
            _accessoryRepository = new Mock<IAccessoryRepository>();
        }
        [TestMethod]
        public void CreateAccessory_AccessoryIsAdded_Test()
        {
            //1. Arrange
            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);

            var Accessory = new AccessoryVM { Name = "Speelbal" };

            //2. Act
            _accessorycontroller.Create(Accessory);

            //3.Assert

            _accessoryRepository.Verify(b => b.Add(Accessory.Accessory), Times.Once());
        }

        [TestMethod]
        public void EditAccessory_ReturnsInput_Test()
        {
            //1. Arrange

            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);

            var Accessory = new AccessoryVM { ID = 1, Name = "Speelbal" };
            _accessoryRepository.Setup(b => b.Get(Accessory.ID)).Returns(Accessory.Accessory);
            _accessoryRepository.Setup(b => b.ContextDB()).Returns(new Domain.BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_accessorycontroller.Edit(Accessory.ID);
            var SameAccessory = (AccessoryVM)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(Accessory.ID, SameAccessory.ID);
            Assert.AreEqual(Accessory.Name, SameAccessory.Name);

        }

        [TestMethod]
        public void CreateAccessory_ReturnsAccessory_Test()
        {
            //1. Arrange
            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);
            _accessorycontroller.ModelState.AddModelError("test", "test");
            var Accessory = new AccessoryVM { ID = 1, Name = "Speelbal" };
            _accessoryRepository.Setup(b => b.ContextDB()).Returns(new Domain.BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_accessorycontroller.Create(Accessory);
            var SameAccessory = (AccessoryVM)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(Accessory.ID, SameAccessory.ID);
        }

        [TestMethod]
        public void EditAccessory_AccessoryIsUpdated_Test()
        {
            //1. Arrange
            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);

            var Accessory = new AccessoryVM { Name = "Speelbal" };

            //2. Act
            _accessorycontroller.Edit(Accessory);

            //3.Assert

            _accessoryRepository.Verify(b => b.UpdateAccessory(Accessory), Times.Once());
        }

        [TestMethod]
        public void EditAccessory_ReturnsAccessory_Test()
        {
            //1. Arrange
            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);
            _accessorycontroller.ModelState.AddModelError("test", "test");
            var Accessory = new AccessoryVM { ID = 1, Name = "Speelbal" };
            _accessoryRepository.Setup(b => b.ContextDB()).Returns(new Domain.BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_accessorycontroller.Edit(Accessory);
            var SameAccessory = (AccessoryVM)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(Accessory.ID, SameAccessory.ID);
        }

        [TestMethod]
        public void IndexAccessory_ReturnsAccessoryList_Test()
        {
            //1. Arrange
            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);
            var accessory = new AccessoryVM { ID = 1, Name = "Speelbal" };
            var List = new List<Accessory>();
            List.Add(accessory.Accessory);
            _accessoryRepository.Setup(b => b.GetAll()).Returns(List);

            //2. Act
            var result = (ViewResult)_accessorycontroller.Index();
            var SameList = (List<AccessoryVM>)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(List[0], SameList[0].Accessory);
        }

        [TestMethod]
        public void DetailsAccessory_ReturnsInput_Test()
        {
            //1. Arrange

            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);

            var Accessory = new AccessoryVM { ID = 1, Name = "Speelbal" };
            _accessoryRepository.Setup(b => b.Get(Accessory.ID)).Returns(Accessory.Accessory);
            _accessoryRepository.Setup(b => b.ContextDB()).Returns(new BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_accessorycontroller.Details(Accessory.ID);
            var SameAccessory = (AccessoryVM)result.ViewData.Model;
            //3.Assert

            Assert.AreEqual(Accessory.ID, SameAccessory.ID);
            Assert.AreEqual(Accessory.Name, SameAccessory.Name);

        }

        [TestMethod]
        public void DeleteAccessory_ReturnsInput_Test()
        {
            //1. Arrange

            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);

            var Accessory = new AccessoryVM { ID = 1, Name = "Speelbal" };
            _accessoryRepository.Setup(b => b.Get(Accessory.ID)).Returns(Accessory.Accessory);
            _accessoryRepository.Setup(b => b.ContextDB()).Returns(new BeesteOpJeFeestjeEntities());

            //2. Act
            var result = (ViewResult)_accessorycontroller.Delete(Accessory.ID);
            var SameAccessory = (AccessoryVM)result.ViewData.Model;
            //3.Assert
            Assert.AreEqual(Accessory.ID, SameAccessory.ID);
            Assert.AreEqual(Accessory.Name, SameAccessory.Name);
        }

        [TestMethod]
        public void DeleteConfirmed_RemovesAccessory_Test()
        {
            //1. Arrange

            _accessorycontroller = new AccessoryController(_accessoryRepository.Object, _boekingsRepository.Object);

            var Accessory = new AccessoryVM { ID = 1, Name = "Speelbal" };
            _accessoryRepository.Setup(b => b.Get(Accessory.ID)).Returns(Accessory.Accessory);
            _accessoryRepository.Setup(b => b.ContextDB()).Returns(new BeesteOpJeFeestjeEntities());

            //2. Act
            _accessorycontroller.DeleteConfirmed(Accessory.ID);
            //3.Assert

            _accessoryRepository.Verify(b => b.Remove(Accessory.Accessory), Times.Once());
        }
    }
}
