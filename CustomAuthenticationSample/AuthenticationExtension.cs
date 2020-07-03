using Microsoft.AspNetCore.Authentication;

namespace CustomAuthenticationSample
{
    public static class AuthenticationExtension
    {
        public static void AddApiKeySupport(this AuthenticationBuilder authenticationBuilder)
        {
            authenticationBuilder
                .AddScheme<MyApiKeyAuthOption, MyApiKeyAuthHandler>(MyApiKeyAuthOption.Scheme,
                                                                    option =>
                                                                    {
                                                                    });
        }
    }
}