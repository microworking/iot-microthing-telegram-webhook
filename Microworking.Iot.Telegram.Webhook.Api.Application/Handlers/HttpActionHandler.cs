using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class HttpActionHandler : IHttpActionHandler
    {
        private readonly IWorldTimeApiRepository _worldTimeApiRepository;
        private readonly IOpenWeatherApiRepository _openWeatherApiRepository;
        private readonly IViaCepApiRepository _viaCepApiRepository;
        private readonly INotifyHandler _notifyHandler;
        private readonly ILogger<HttpActionHandler> _logger;

        public HttpActionHandler(IWorldTimeApiRepository worldTimeApiRepository, 
                                 IOpenWeatherApiRepository openWeatherApiRepository,
                                 IViaCepApiRepository viaCepApiRepository,
                                 INotifyHandler notifyHandler,
                                 ILogger<HttpActionHandler> logger)
        {
            _worldTimeApiRepository = worldTimeApiRepository;
            _openWeatherApiRepository = openWeatherApiRepository;
            _viaCepApiRepository = viaCepApiRepository;
            _notifyHandler = notifyHandler;
            _logger = logger;
        }

        public async Task<string> Handle(ActionCommandDTO Action, IdentityDTO Identity, List<string> Terms)
        {
            SendMessageRequest message = new SendMessageRequest
            {
                chat_id = Identity.ChatId
            };

            try
            {
                bool s;

                Terms.ForEach(x => 
                { 
                    if(x.ToString().CompareTo("LOCALIDADE") == 0) 
                        s = true; 
                });

                string d = Terms.FindIndex(x => x == "LOCALIDADE").ToString();

                string g = Terms[2];

                if (g.ToString().Equals("LOCALIDADE")) s = true;

                if (g.CompareTo("LOCALIDADE") == 0) s = true;

                if ("LOCALIDADE" == "LOCALIDADE") s = true;

                if (Terms[2].ToString().Equals("LOCALIDADE")) s = true;

                var x = Terms.Find(local => local.ToUpper().Equals("LOCALIDADE"));

                var y = Terms.BinarySearch("LOCALIDADE");   
            }
            catch(Exception e)
            {
                throw e;
            }

            if (Terms.Where(local => local.ToUpper() == "LOCALIDADE") != null)
            {
                ViaCepResponse location = _viaCepApiRepository.GetLocation(Identity).Result;
                message.text = string.Format("{0}, {1} - {2}/{3}-BR CEP {4} / DDD +55 ({5})", location.logradouro, location.bairro, location.localidade, location.uf, location.cep, location.ddd);
            }

            if (Terms.Where(local => local.ToUpper() == "TEMPO") != null)
            {
                //OpenWeatherResponse localWeather = _openWeatherApiRepository.GetWeather().Result; TODO: não autorizado
                //message.text = _templateHandler(localWeather);
                message.text = "Não implementado ainda";
            }

            if (Terms.Where(local => local.ToUpper() == "TEMPO") != null)
            {
                WorldTimeResponse localTime = _worldTimeApiRepository.GetTime().Result;
                //message.text = _templateHandler(localTime);
                message.text = "Não implementado ainda";
            }            

            await _notifyHandler.Handle(message, Identity);

            return null;
        }
    }
}

/**
//data por localidade (cep)
//hora por localidade (cep)
//data e hora por localidade (cep)
//temperatura por localidade (cep)
//humidade por localidade (cep)
//velocidade vento por localidade (cep)
//dia da semana
//dia do mês
//hora
//minuto
https://viacep.com.br/ws/03334070/json
{
    "cep": "03334-070",
    "logradouro": "Rua Dante Pellacani",
    "complemento": "",
    "bairro": "Vila Regente Feijó",
    "localidade": "São Paulo",
    "uf": "SP",
    "ibge": "3550308",
    "gia": "1004",
    "ddd": "11",
    "siafi": "7107"
}
https://api.openweathermap.org/data/2.5/weather?q=São Paulo&appid=cb1be847ed4e91e84070a20470e50ab3&lang=pt_br
???
 */