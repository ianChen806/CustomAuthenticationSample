using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomAuthenticationSample
{
    public class MyApiKeyAuthHandler : AuthenticationHandler<MyApiKeyAuthOption>
    {
        public MyApiKeyAuthHandler(IOptionsMonitor<MyApiKeyAuthOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }
    }
}