using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebApp.Repositories;
using WebApp.Controllers;

namespace WebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexShouldWork()
        {
            // Arrange
            HomeController controller = new HomeController(null);

            // Act
            var result = controller.Index() as Task<ActionResult>;

            result.Wait();
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod, Timeout(2500)]
        public void IndexShouldReturnDefaultResultsInLessThen2500Miliseconds()
        {
            // Arrange
            HomeController controller = new HomeController(new PublicRestaurantsService());

            // Act
            var result = controller.Index() as Task<ActionResult>;

            result.Wait();
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexShouldReturnDefaultResults()
        {
            // Arrange
            HomeController controller = new HomeController(new PublicRestaurantsService());

            // Act
            Task<ActionResult> result = controller.Index() as Task<ActionResult>;

            result.Wait();
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == TaskStatus.RanToCompletion);
        }

        [TestMethod]
        public void IndexShouldReturnFilteredResults()
        {
            // Arrange
            HomeController controller = new HomeController(new PublicRestaurantsService());

            // Act
            Task<ActionResult> result = controller.Index("se20") as Task<ActionResult>;

            result.Wait();
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == TaskStatus.RanToCompletion);
        }
    }
}
