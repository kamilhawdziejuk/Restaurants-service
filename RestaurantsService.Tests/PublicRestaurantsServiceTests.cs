using WebApp.Models;
using WebApp.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsRepository.Tests
{
    [TestClass]
    public class PublicRestaurantsServiceTests
    {
        [TestMethod]
        public void WhenGettingRestaurantsFromProperAreaThenShouldReturnPositiveAmountOfRestaurants()
        {
            IRestaurantsService repo = new PublicRestaurantsService();

            Task<List<Restaurant>> restaurants_se19 = repo.GetRestaurants("se19");
            restaurants_se19.Wait();
            //The amount may change but in this area should return at least some
            Assert.IsTrue(restaurants_se19.Result.Count() > 0);
        }

        [TestMethod]
        public void WhenGettingRestaurantsFromWrongAreaThenShouldReturnNoneRestaurants()
        {
            IRestaurantsService repo = new PublicRestaurantsService();

            Task<List<Restaurant>> restaurantsUknownArea = repo.GetRestaurants("se000");
            restaurantsUknownArea.Wait();
            Assert.IsFalse(restaurantsUknownArea.Result.Count() > 0);
        }

        [TestMethod]
        public void WhenGettingRestaurantsFromEmptyAreaThenShouldReturnNoneRestaurants()
        {
            IRestaurantsService repo = new PublicRestaurantsService();

            Task<List<Restaurant>> restaurantsUknownArea = repo.GetRestaurants("");
            restaurantsUknownArea.Wait();
            Assert.IsFalse(restaurantsUknownArea.Result.Count() > 0);
        }

        [TestMethod]
        public void WhenGettingRestaurantsFromNullAreaThenShouldReturnNoneRestaurants()
        {
            IRestaurantsService repo = new PublicRestaurantsService();

            Task<List<Restaurant>> restaurantsUknownArea = repo.GetRestaurants(null);
            restaurantsUknownArea.Wait();
            Assert.IsFalse(restaurantsUknownArea.Result.Count() > 0);
        }

        [TestMethod]
        public void AllRestaurantsFromSe19AreaShouldHaveNamesAndRatings()
        {
            IRestaurantsService repo = new PublicRestaurantsService();

            Task<List<Restaurant>> restaurants_se19 = repo.GetRestaurants("se19");
            restaurants_se19.Wait();
            foreach (var restaurant in restaurants_se19.Result)
            {
                Assert.IsNotNull(restaurant);
                Assert.IsFalse(restaurant.Name.Equals(string.Empty));
                Assert.IsTrue(restaurant.RatingAverage <= 10 && restaurant.RatingAverage >= 0);
            }
        }
    }
}
