
namespace Microworking.Iot.Telegram.Webhook.Api.Application
{
    public interface IResultado
    {
        public bool? Sucesso { get; }
        public string? Mensagem { get; }
        public object? Retorno { get; }
    }
}