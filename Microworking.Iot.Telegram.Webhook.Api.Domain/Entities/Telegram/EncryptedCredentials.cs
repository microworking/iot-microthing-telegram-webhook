﻿
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class EncryptedCredentials
    {
        public string? data { get; set; }
        public string? hash { get; set; }
        public string? secret { get; set; }
    }
}