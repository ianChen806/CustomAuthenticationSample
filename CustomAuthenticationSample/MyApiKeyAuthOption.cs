using Microsoft.AspNetCore.Authentication;

namespace CustomAuthenticationSample
{
    public class MyApiKeyAuthOption : AuthenticationSchemeOptions
    {
        public const string Scheme = "ApiKey";
    }
}