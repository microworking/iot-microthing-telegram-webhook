using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Helpers
{
    public static class DecodeJwtTokenHelper
    {
        public static List<Claim> ObterClaims(string token)
        {
            var listaClaims = new List<Claim>();
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenSecurity = (JwtSecurityToken)jwtTokenHandler.ReadToken(token);

            if (tokenSecurity != null)
            {
                listaClaims = (List<Claim>)tokenSecurity.Claims;
            }

            return listaClaims;
        }
    }
}