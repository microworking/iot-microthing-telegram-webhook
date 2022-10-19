using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Repositories
{
    public interface ITelegramApiRepository
    {
        Task<Message> Notify(SendMessageRequest request, IdentityDTO identity);
    }
}