﻿
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class ProximityAlertTriggered
    {
        public User? traveler { get; set; }
        public User? watcher { get; set; }
        public int? distance { get; set; }
    }
}