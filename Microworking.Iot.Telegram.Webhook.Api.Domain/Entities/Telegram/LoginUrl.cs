﻿
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class LoginUrl
    {
        public string? url { get; set; }
        public string? forward_text { get; set; }
        public string? bot_username { get; set; }
        public bool? request_write_access { get; set; }
    }
}