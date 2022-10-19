using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Repositories
{
    public interface IViaCepApiRepository
    {
        Task<ViaCepResponse> GetLocation(IdentityDTO identity);
    }
}