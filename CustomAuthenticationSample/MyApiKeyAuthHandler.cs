using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomAuthenticationSample
{
    public class MyApiKeyAuthHandler : AuthenticationHandler<MyApiKeyAuthOption>
    {
        public MyApiKeyAuthHandler(IOptionsMonitor<MyApiKeyAuthOption> options,
                                   ILoggerFactory logger,
                                   UrlEncoder encoder,
                                   ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            await Task.CompletedTask;

            var name = string.Empty;
            var role = string.Empty;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, "ApiKey");
            return AuthenticateResult.Success(authenticationTicket);
        }
    }
}