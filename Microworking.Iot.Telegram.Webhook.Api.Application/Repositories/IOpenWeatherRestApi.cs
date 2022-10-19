using System.Threading.Tasks;
using Refit;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Repositories
{
    public interface IOpenWeatherRestApi
    {
        [Post("/")]
        public Task<OpenWeatherResponse> GetWeather();
    }
}