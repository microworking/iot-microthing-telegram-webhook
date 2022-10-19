using System.Threading.Tasks;
using System.Collections.Generic;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Repositories
{
    public interface ITelegramDbRepository
    {
        public Task<IEnumerable<string>> GetHomes(string Token);
        
        public Task<IEnumerable<string>> GetRooms(string Token);
        
        public Task<IEnumerable<string>> GetPeripherals(string Token, string Room = null);

        public IdentityDTO GetIdentity(string SecretToken);

        public ActionCommandDTO GetActionCommand(List<string> Terms);

        public string GetRoom(string Token, List<string> Terms);

        public DeviceModelDTO GetDeviceProperties(string Token, string Room, List<string> Terms);
    }
}