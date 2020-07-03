using System.Collections.Generic;
using System.Linq;
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
        private const string _apiKeyHeaderName = "X-Api-Key";
        private readonly ApiKeyQuery _apiKeyQuery;

        public MyApiKeyAuthHandler(IOptionsMonitor<MyApiKeyAuthOption> options,
                                   ILoggerFactory logger,
                                   UrlEncoder encoder,
                                   ISystemClock clock,
                                   ApiKeyQuery apiKeyQuery)
            : base(options, logger, encoder, clock)
        {
            _apiKeyQuery = apiKeyQuery;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // have header "api key"?
            if (!Request.Headers.TryGetValue(_apiKeyHeaderName, out var values))
            {
                return AuthenticateResult.NoResult();
            }

            // "api key" value is empty?
            var apiKey = values.FirstOrDefault();
            if (apiKey == null || string.IsNullOrWhiteSpace(apiKey))
            {
                return AuthenticateResult.NoResult();
            }

            // api key is same?
            var apiInfo = await _apiKeyQuery.Query(apiKey);
            if (apiInfo == null)
            {
                return AuthenticateResult.Fail("invalid api key");
            }

            // generate ticket
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, apiInfo.Name),
                new Claim(ClaimTypes.Role, apiInfo.Role),
            };

            var claimsIdentity = new ClaimsIdentity(claims, MyApiKeyAuthOption.Scheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, MyApiKeyAuthOption.Scheme);

            return AuthenticateResult.Success(authenticationTicket);
        }
    }

    public class ApiKeyQuery
    {
        private readonly Dictionary<string, ApiInfo> _apiKeys =
            new Dictionary<string, ApiInfo>
            {
                ["_MyApiKey_"] = new ApiInfo("ian", "Admin"),
                ["_UserApiKey_"] = new ApiInfo("test", "User")
            };

        public Task<ApiInfo> Query(string apiKey)
        {
            if (_apiKeys.ContainsKey(apiKey))
            {
                return Task.FromResult(_apiKeys[apiKey]);
            }

            return null;
        }
    }

    public class ApiInfo
    {
        public ApiInfo(string name, string role)
        {
            Name = name;
            Role = role;
        }

        public string Name { get; }

        public string Role { get; }
    }
}