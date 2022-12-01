using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microworking.Iot.Telegram.Webhook.Api.Application;
using Microworking.Iot.Telegram.Webhook.Api.Application.Services;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Controllers
{
    [ApiController]
    [Route("api/telegram/iot")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Resultado))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Resultado))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Resultado))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
 
    public class TelegramIotController : ControllerBase
    {
        private readonly IMqttActionsService _mqttActionsService;
        private readonly ILogger<TelegramIotController> _logger;
        private readonly IAuthTokenHandler _authTokenHandler;
        private readonly IMediator _mediator;

        public TelegramIotController(IMqttActionsService mqttActionsService,
                                     IAuthTokenHandler authTokenHandler,
                                     ILogger<TelegramIotController> logger,
                                     IMediator mediator)
        {
            _mqttActionsService = mqttActionsService;
            _authTokenHandler = authTokenHandler;
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("getToken/{token}")]
        public async Task<IActionResult> ValidateToken(string Token)
        {
            try
            {
                _logger.LogInformation($"[{GetType()}] Inicio do processamento de ConsultaHistorico.");

                await _mqttActionsService.StartAsync(new System.Threading.CancellationToken());

                _logger.LogInformation($"[{GetType()}] Fim do  processamento de ConsultaHistorico.");

                return Ok(null);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "xxxxxxxxxxxxxxxx");
                return StatusCode(500, new Resultado { Mensagem = exception.Message, Retorno = exception.StackTrace });
            }
        }

        [HttpPost("setAction")]
        public async Task<IActionResult> SetAction([FromBody] SetActionRequest request)
        {
            try
            {
                _logger.LogInformation($"[{GetType()}] Request action: { request.message.text }");

                IdentityDTO identity = _authTokenHandler.GetIdentity(Request);
                if (!identity.IsAuthorized)
                    return StatusCode(StatusCodes.Status401Unauthorized, new Resultado { Mensagem = "Unauthorized.", Retorno = null });
                request.Identity = identity;

                _logger.LogInformation($"[{GetType()}] IndentyToken: { identity.IndentyToken }, AuthToken: { identity.AuthToken }, ChatId: { identity.ChatId }");

                var retorno = await _mediator.Send(request);

                _logger.LogInformation($"[{GetType()}] Action executed with success");

                return Ok(null);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Has a error executing set action request");
                return StatusCode(500, new Resultado { Mensagem = exception.Message, Retorno = exception.StackTrace });
            }
        }

        [HttpPost("notify")]
        public async Task<IActionResult> Notify(NotifyRequest request)
        {
            try
            {
                _logger.LogInformation($"[{GetType()}] Inicio do processamento de ConsultaHistorico.");

                var retorno = await _mediator.Send(request);

                _logger.LogInformation($"[{GetType()}] Fim do  processamento de ConsultaHistorico.");

                return Ok(null);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "xxxxxxxxxxxxxxxx");
                return StatusCode(500, new Resultado { Mensagem = exception.Message, Retorno = exception.StackTrace });
            }
        }
    }
}