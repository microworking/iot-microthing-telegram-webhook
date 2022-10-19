using System.Threading.Tasks;
using Refit;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Interfaces.Repositories
{
    public interface ITelegramRestApi
    {
        [Post("/sendMessage")]
        Task<Message> Notify(SendMessageRequest request);
    }
}