
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class VideoNote
    {
        public string? file_id { get; set; }
        public string? file_unique_id { get; set; }
        public int? length { get; set; }
        public int? duration { get; set; }
        public PhotoSize? thumb { get; set; }
        public int? file_size { get; set; }
    }
}