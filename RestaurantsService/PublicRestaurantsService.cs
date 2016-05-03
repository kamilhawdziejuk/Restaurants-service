using WebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
    public class PublicRestaurantsService : IRestaurantsService
    {
        private void InitializeRepositoryConnectionHeader(HttpClient client)
        {
            client.BaseAddress = new Uri(@"http://public.je-apis.com");
            client.DefaultRequestHeaders.Add("Host", "public.je-apis.com");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB");
            client.DefaultRequestHeaders.Add("Accept-Tenant", "uk");

            string accessToken = "VGVjaFRlc3RBUEk6dXNlcjI=";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", accessToken);
        }

        /// <summary>
        /// Gets the restaurants from the outcode, such as se19
        /// </summary>
        /// <param name="outcode"></param>
        /// <returns></returns>
        public async Task<List<Restaurant>> GetRestaurants(string outcode)
        {
            var restaurants = new List<Restaurant>();
            try
            {
                using (var client = new HttpClient())
                {
                    this.InitializeRepositoryConnectionHeader(client);
                    string queryRestaurantsWithOutCode = string.Format("/restaurants?q={0}&c=&name=", outcode);

                    HttpResponseMessage response = await client.GetAsync(queryRestaurantsWithOutCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var message = response.Content.ReadAsStringAsync().Result;
                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();

                        dynamic dyn = JsonConvert.DeserializeObject(jsonString.Result);

                        foreach (var obj in dyn.Restaurants)
                        {
                            Restaurant restaurant = new Restaurant()
                            {
                                Name = obj.Name,
                                RatingAverage = obj.RatingAverage
                            };

                            //TODO create the CuisineTypes class (cache possible)
                            foreach (var c in obj.CuisineTypes)
                            {
                                string name = c.Name;
                                restaurant.CuisineTypes.Add(name);
                            }

                            restaurants.Add(restaurant);
                        }
                    }
                }

            }
            catch (ArgumentException e)
            {
                //TODO Enable logging instead of console output
                Console.WriteLine(e.Message);
            }
            catch (WebException e)
            {
                Console.WriteLine("\nWebException is thrown. \nMessage is :" + e.Message);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                    Console.WriteLine("Server : {0}", ((HttpWebResponse)e.Response).Server);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception is thrown. Message is :" + e.Message);
            }
            return restaurants;
        }
    }
}