using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microworking.Iot.Telegram.Webhook.Api.Application.Helpers;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Common
{
    public class AuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;        
        private readonly IDistributedCache _cache;

        public AuthorizationHandler(IHttpContextAccessor httpContextAccessor,IDistributedCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {            
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            string jwtTokenAcesso = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(jwtTokenAcesso, out var headerValue))
            {            
                var token = headerValue.Parameter;
                var username = context.User.FindFirst("preferred_username")?.Value;

                var tokenCache = await _cache.GetStringAsync(username);
                if (string.IsNullOrEmpty(tokenCache) || token != tokenCache)
                {
                    context.Fail();
                    return;
                }
            }
            
            var usuarioTemAcesso = false;
            if (requirement.AllowedRoles == null ||
                requirement.AllowedRoles.Any() == false)
            {
                usuarioTemAcesso = true;
            }
            else
            {
                var jwtUserData = _httpContextAccessor.HttpContext.Request.Headers["JwtUserData"];

                if (!string.IsNullOrEmpty(jwtUserData))
                {
                    var claims = DecodeJwtTokenHelper.ObterClaims(jwtUserData);

                    var perfilDoUsuario = claims.First(claim => claim.Type == "perfil").Value;

                    int.TryParse(perfilDoUsuario, out int idPerfilUsuario);

                    var roles = requirement.AllowedRoles;
                    var descricaoPerfil = "";//await _usuarioRepository.GetDescricaoPerfilPorId(idPerfilUsuario);

                    usuarioTemAcesso = roles.Contains(descricaoPerfil);
                }
            }

            if (usuarioTemAcesso)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}