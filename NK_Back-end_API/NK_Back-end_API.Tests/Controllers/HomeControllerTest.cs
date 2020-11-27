using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NK_Back_end_API;
using NK_Back_end_API.Controllers;

namespace NK_Back_end_API.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
