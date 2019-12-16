using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TravelSite.Models;

namespace TravelSite
{
    public class AccuWeatherAPIHandler
    {
        public async static Task<AccuWeatherLocationResponse> GetLocation(string State, string City)
        {
            HttpClient client = new HttpClient();
            string LocationURI = $"http://dataservice.accuweather.com/locations/v1/cities/US/{State}/search?apikey={APIKey.AccuWeather}&q={City}";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage message = await client.GetAsync(LocationURI).ConfigureAwait(false))
            {
                if (message.IsSuccessStatusCode)
                {
                    AccuWeatherLocationResponse key = await message.Content.ReadAsAsync<AccuWeatherLocationResponse>();
                    return key;
                }
                else
                {
                    throw new Exception(message.ReasonPhrase);
                }
            }
        }
        //public async static Task<> GetForecast(int locationKey)
        //{

        //}
    }
}