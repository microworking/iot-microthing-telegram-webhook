
namespace Microworking.Iot.Telegram.Webhook.Api.Application
{
    public class Resultado : IResultado
    {
        public Resultado() {}

        public Resultado(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public Resultado(bool sucesso, string mensagem, object retorno)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Retorno = retorno;
        }

        public Resultado(int codigo, bool sucesso, string mensagem)
        {
            Codigo = codigo;
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public Resultado(int codigo, bool sucesso, string mensagem, object retorno)
        {
            Codigo = codigo;
            Sucesso = sucesso;
            Mensagem = mensagem;
            Retorno = retorno;
        }

        public int? Codigo { get; set; }

        public bool? Sucesso { get; set; }

        public string? Mensagem { get; set; }

        public object? Retorno { get; set; }
    }
}