using System.Threading.Tasks;
using System.Collections.Generic;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces
{
    public interface IHttpActionHandler
    {
        public Task<string> Handle(ActionCommandDTO Action, IdentityDTO Identity, List<string> Terms);
    }
}