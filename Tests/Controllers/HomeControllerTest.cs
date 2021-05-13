using System.Web.Mvc;
using Heuristics.TechEval.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heuristics.TechEval.Tests.Controllers {

	[TestClass]
	public class HomeControllerTest {

		[TestMethod]
		public void Index() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
	}
}
