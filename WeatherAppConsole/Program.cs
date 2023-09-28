using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppConsole
{

    public class Location
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string tz_id { get; set; }
        public long localtime_epoch { get; set; }
        public string localtime { get; set; }
    }

    public class Condition
    {
        public string text { get; set; }
        public string icon { get; set; }
        public int code { get; set; }
    }

    public class Current
    {
        public long last_updated_epoch { get; set; }
        public string last_updated { get; set; }
        public double temp_c { get; set; }
        public double temp_f { get; set; }
        public int is_day { get; set; }
        public Condition condition { get; set; }
        public double wind_mph { get; set; }
        public double wind_kph { get; set; }
        public int wind_degree { get; set; }
        public string wind_dir { get; set; }
        public double pressure_mb { get; set; }
        public double pressure_in { get; set; }
        public double precip_mm { get; set; }
        public double precip_in { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public double feelslike_c { get; set; }
        public double feelslike_f { get; set; }
        public double vis_km { get; set; }
        public double vis_miles { get; set; }
        public double uv { get; set; }
        public double gust_mph { get; set; }
        public double gust_kph { get; set; }
    }

    public class Day
    {
        public double maxtemp_c { get; set; }
        public double maxtemp_f { get; set; }
        public double mintemp_c { get; set; }
        public double mintemp_f { get; set; }
        public double avgtemp_c { get; set; }
        public double avgtemp_f { get; set; }
        public double maxwind_mph { get; set; }
        public double maxwind_kph { get; set; }
        public double totalprecip_mm { get; set; }
        public double totalprecip_in { get; set; }
        public double totalsnow_cm { get; set; }
        public double avgvis_km { get; set; }
        public double avgvis_miles { get; set; }
        public double avghumidity { get; set; }
        public int daily_will_it_rain { get; set; }
        public int daily_chance_of_rain { get; set; }
        public int daily_will_it_snow { get; set; }
        public int daily_chance_of_snow { get; set; }
        public Condition condition { get; set; }
        public double uv { get; set; }
    }

    public class Astro
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string moonrise { get; set; }
        public string moonset { get; set; }
        public string moon_phase { get; set; }
        public string moon_illumination { get; set; }
        public int is_moon_up { get; set; }
        public int is_sun_up { get; set; }
    }

    public class Hour
    {
        public long time_epoch { get; set; }
        public string time { get; set; }
        public double temp_c { get; set; }
        public double temp_f { get; set; }
        public int is_day { get; set; }
        public Condition condition { get; set; }
        public double wind_mph { get; set; }
        public double wind_kph { get; set; }
        public int wind_degree { get; set; }
        public string wind_dir { get; set; }
        public double pressure_mb { get; set; }
        public double pressure_in { get; set; }
        public double precip_mm { get; set; }
        public double precip_in { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public double feelslike_c { get; set; }
        public double feelslike_f { get; set; }
        public double windchill_c { get; set; }
        public double windchill_f { get; set; }
        public double heatindex_c { get; set; }
        public double heatindex_f { get; set; }
        public double dewpoint_c { get; set; }
        public double dewpoint_f { get; set; }
        public double will_it_rain { get; set; }
        public double will_it_snow { get; set; }
        public double is_uv_index { get; set; }
        public double uv_index { get; set; }
    }

    public class Forecast
    {
        public List<Forecastday> forecastday { get; set; }
    }

    public class Forecastday
    {
        public string date { get; set; }
        public long date_epoch { get; set; }
        public Day day { get; set; }
        public Astro astro { get; set; }
        public List<Hour> hour { get; set; }
    }

    public class Root
    {
        public Location location { get; set; }
        public Current current { get; set; }
        public Forecast forecast { get; set; }
    }
    public class WeatherAPI
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string baseUrl = "https://api.weatherapi.com/v1";
        private const string currentMode = "/current";
        private const string forecast = "/forecast";
        private const string forecastConditions = "&aqi=yes&alerts=no";
        private const string jsonMode = ".json?";
        private const string daysMode = "&days=";
        private const string key = "key=352af375ca184367bdc131507231108&q=";

        public static async Task<string> GetCurrentWeather(string city)
        {
            string apiUrl = $"{baseUrl}{currentMode}{jsonMode}{key}{city}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to fetch weather data. Status code: {response.StatusCode}");
            }
        }

        public static async Task<string> GetDailyWeather(string city, int daysCount)
        {
            string apiUrl = $"{baseUrl}{forecast}{jsonMode}{key}{city}{daysMode}{daysCount}{forecastConditions}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to fetch weather data. Status code: {response.StatusCode}");
            }
        }

        public static async Task<string> GetWeeklyWeather(string city)
        {
            string apiUrl = $"{baseUrl}{forecast}{jsonMode}{key}{city}{daysMode}7{forecastConditions}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to fetch weather data. Status code: {response.StatusCode}");
            }
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter City ");
            string city = Console.ReadLine();

            try
            {

                string jsonData = await WeatherAPI.GetDailyWeather(city,2);
                Root weatherData = JsonConvert.DeserializeObject<Root>(jsonData);

                Console.WriteLine("Enter time \"from\" ");
                int from = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter time \"to\" ");
                int to = int.Parse(Console.ReadLine());

                if (weatherData != null && weatherData.forecast != null && weatherData.forecast.forecastday.Count >= 2)
                {
                    Forecastday secondDay = weatherData.forecast.forecastday[1];

                    Console.WriteLine($"Temperatures for tommorov from {from}:00 to {to}:00");

                    foreach (Hour hourData in secondDay.hour)
                    {
                        if (DateTime.TryParse(hourData.time, out DateTime time))
                        {
                            if (time.Hour >= from && time.Hour<= to)
                            {
                                Console.WriteLine($"{time.Hour}:00 - {hourData.temp_c}°C");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Weather data not available for tommorow.");
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
