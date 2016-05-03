using WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.ViewModels
{
    public class RestaurantView
    {
        private Restaurant _restaurant;

        public RestaurantView(Restaurant restaurant)
        {
            _restaurant = restaurant;
        }

        public string Name
        {
            get { return _restaurant.Name; }
        }

        public double RatingAverage
        {
            get { return _restaurant.RatingAverage; }
        }

        public List<SelectListItem> FoodTypes
        {
            get { return _restaurant.CuisineTypes.Select(t => new SelectListItem() { Text = t }).ToList(); }
        }

        public int Selected { get; set; }
    }
}