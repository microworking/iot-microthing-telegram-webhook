
namespace Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses
{
    public class OpenWeatherResponse
    {
        public coord? coord { get; set; }
        public weather[]? weather { get; set; }
        public string? Base { get; set; }
        public main? main { get; set; }
        public long? visibility { get; set; }
        public wind? wind { get; set; }
        public clouds? clouds { get; set; }
        public long? dt { get; set; }
        public sys? sys { get; set; }
        public long? timezone { get; set; }
        public long? id { get; set; }
        public string? name { get; set; }
        public int? cod { get; set; }
    }

    public class coord
    {
        public int? lon { get; set; }
        public int? lat { get; set; }
    }

    public class weather
    {
        public long? id { get; set; }
        public string? main { get; set; }
        public string? description { get; set; }
        public string? icon { get; set; }
    }

    public class main
    {
        public float? temp { get; set; }
        public float? feels_like { get; set; }
        public float? temp_min { get; set; }
        public float? temp_max { get; set; }
        public float? pressure { get; set; }
        public float? humidity { get; set; }
    }

    public class wind
    {
        public float? speed { get; set; }
        public int? deg { get; set; }
    }

    public class clouds
    {
        public int? all { get; set; }
    }

    public class sys
    {
        public int? type { get; set; }
        public int? id { get; set; }
        public string? country { get; set; }
        public long? sunrise { get; set; }
        public long? sunset { get; set; }
    }
}