using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Repositories
{
    public interface IOpenWeatherApiRepository
    {
        public Task<OpenWeatherResponse> GetWeather();
    }
}