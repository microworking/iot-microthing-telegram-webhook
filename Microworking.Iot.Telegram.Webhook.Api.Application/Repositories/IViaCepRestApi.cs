using System.Threading.Tasks;
using Refit;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Repositories
{
    public interface IViaCepRestApi
    {
        [Get("/")]
        Task<ViaCepResponse> GetLocation();
    }
}