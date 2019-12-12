using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TravelSite.Models;

namespace TravelSite
{
    public class GoogleAPIHandler
    {
        public static async Task<List<NearByResponse>> GetActivities(string location,int radius, string type, string keyword)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string GoogleNearBySeachURI = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location}&radius={radius}&type={type}&keyword={keyword}&key={APIKey.Google}";
            using(HttpResponseMessage message = await client.GetAsync(GoogleNearBySeachURI))
            {
                if (message.IsSuccessStatusCode)
                {
                    List<NearByResponse> activities = await message.Content.ReadAsAsync<List<NearByResponse>>();
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