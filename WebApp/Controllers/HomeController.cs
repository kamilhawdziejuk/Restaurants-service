using WebApp.Repositories;
using WebApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantsService _restaurantsService;

        public HomeController(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        public async Task<ActionResult> Index(string searchString = "se19")
        {
            //TODO some validation of parameter needed here
            var restaurants = await _restaurantsService.GetRestaurants(searchString);

            //TODO do the server processing to prepare the paging
            var results = new List<RestaurantView>();
            restaurants.ForEach(r => results.Add(new RestaurantView(r)));
            return View(results);
        }
    }
}