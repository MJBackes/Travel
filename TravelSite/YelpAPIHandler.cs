using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TravelSite.Models;
namespace TravelSite
{
    public class YelpAPIHandler
    {
        public static async Task<YelpBusinessResponse> GetActivities(string location, int radius, string type, string keyword)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", APIKey.Yelp);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string lat = location.Split(',')[0];
            string lng = location.Split(',')[1];
            string YelpBusinessSearchURI = $"https://api.yelp.com/v3/businesses/search?term={type}&latitude={lat}&longitude={lng}&radius={radius}";
            using (HttpResponseMessage message = await client.GetAsync(YelpBusinessSearchURI).ConfigureAwait(false))
            {
                if (message.IsSuccessStatusCode)
                {
                    string yelpResponse = await message.Content.ReadAsStringAsync();
                    YelpBusinessResponse activities = JsonConvert.DeserializeObject<YelpBusinessResponse>(yelpResponse);
                    return activities;
                }
                else
                {
                    throw new Exception(message.ReasonPhrase);
                }
            }
        }
    }
}