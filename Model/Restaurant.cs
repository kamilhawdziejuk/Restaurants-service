using System.Collections.Generic;

namespace WebApp.Models
{
    /// <summary>
    /// Simplified model of the Restaurant (TODO extend if more information needed)
    /// </summary>
    public class Restaurant
    {
        public string Name { get; set; }
        public double RatingAverage { get; set; }
        public List<string> CuisineTypes { get; }

        public Restaurant()
        {
            CuisineTypes = new List<string>();
        }
    }
}
