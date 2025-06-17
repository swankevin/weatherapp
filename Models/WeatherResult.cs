namespace WeatherApp.Models
{
    public class WeatherResult
    {
        public required CurrentWeather current_weather { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public class CurrentWeather
        {
            public double temperature { get; set; }
            public double windspeed { get; set; }
            public required string time { get; set; }
        }
    }
}
