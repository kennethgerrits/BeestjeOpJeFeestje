using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeestjeOpJeFeestje;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Domain.Repositories;
using Moq;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using System.Diagnostics.CodeAnalysis;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HomeControllerTest
    {
        private Mock<IBoekingRepository> _boekingsRepository;
        private Mock<IBeastRepository> _beastRepository;

        [TestInitialize]
        public void Init()
        {
            _boekingsRepository = new Mock<IBoekingRepository>();
            _beastRepository = new Mock<IBeastRepository>();
        }
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_boekingsRepository.Object, _beastRepository.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
