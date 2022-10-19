using MediatR;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses
{
    public class GetActionsResponse : IRequest<IDeviceResult<GetActionsResponse>> 
    {
        public GetActionsResponse() {}
        public string? uid { get; set; }
        public long? update_id { get; set; }
        public long? message_id { get; set; }
        public string? action { get; set; }
        public int? gpio { get; set; }
    }
}