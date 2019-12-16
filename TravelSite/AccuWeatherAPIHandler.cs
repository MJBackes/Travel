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
    public class AccuWeatherAPIHandler
    {
        public async static Task<List<AccuWeatherLocationResponse>> GetLocation(string State, string City)
        {
            using (HttpClient client = new HttpClient())
            {
                string LocationURI = $"http://dataservice.accuweather.com/locations/v1/cities/US/{State}/search?apikey={APIKey.AccuWeather}&q={City}";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage message = await client.GetAsync(LocationURI).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string responseString = await message.Content.ReadAsStringAsync();
                        List<AccuWeatherLocationResponse> responses = JsonConvert.DeserializeObject<List<AccuWeatherLocationResponse>>(responseString);
                        return responses;
                    }
                    else
                    {
                        throw new Exception(message.ReasonPhrase);
                    }
                }
            }
        }
        public async static Task<AccuWeatherForecast> GetForecast(string locationKey)
        {
            using (HttpClient client = new HttpClient())
            {
                string ForecastURI = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{locationKey}?apikey={APIKey.AccuWeather}";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage message = await client.GetAsync(ForecastURI).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string responseString = await message.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<AccuWeatherForecast>(responseString);
                    }
                    else
                    {
                        throw new Exception(message.ReasonPhrase);
                    }
                }
            }
        }
    }
}