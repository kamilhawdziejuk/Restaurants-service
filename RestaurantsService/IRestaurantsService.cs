using WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebApp.Repositories
{
    /// <summary>
    /// The interface for getting restaurants - can be reused in different applications (Web, Mobile App, Desktop...)
    /// </summary>
    public interface IRestaurantsService
    {
        /// <summary>
        /// Returns the restaurants list from given outcode area
        /// </summary>
        /// <param name="outcode">The area to search data, such as "se19"</param>
        /// <returns></returns>
        Task<List<Restaurant>> GetRestaurants(string outcode);
    }
}