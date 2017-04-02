using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateExample;
using RealEstateExample.Controllers;

namespace RealEstateExample.Tests.Controllers
{
    [TestClass]
    public class ListingsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ListingsController controller = new ListingsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            ListingsController controller = new ListingsController();

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            //  Assert.AreEqual("Your application description page.", result.ViewBag.Message);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void New()
        {
            // Arrange
            ListingsController controller = new ListingsController();

            // Act
            ViewResult result = controller.New() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ListingsController controller = new ListingsController();

            // Act
            ViewResult result = controller.Delete(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
