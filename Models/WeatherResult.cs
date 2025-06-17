namespace WeatherApp.Models
{
    public class WeatherResult
    {
        public CurrentWeather current_weather { get; set; }

        public class CurrentWeather
        {
            public double temperature { get; set; }
            public double windspeed { get; set; }
            public string time { get; set; }
        }
    }
}
