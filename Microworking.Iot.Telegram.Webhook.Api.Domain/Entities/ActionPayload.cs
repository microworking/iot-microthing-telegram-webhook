using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class ActionPayload
    {
        public string? identity_token { get; set; }
        public string? uid { get; set; }
        public int? gpio { get; set; }
        public string? action { get; set; }
        public string? owner { get; set; }
        public long? update_id { get; set; }
        public long? chat_id { get; set; }
        public int? message_id { get; set; }
        public string? peripheral { get; set; }
        public string? message { get; set; }
        public DateTime? date { get; set; }
    }
}