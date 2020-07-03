using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomAuthenticationSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = MyApiKeyAuthOption.Scheme;
                options.DefaultAuthenticateScheme = MyApiKeyAuthOption.Scheme;
            });
            authenticationBuilder.AddScheme<MyApiKeyAuthOption, MyApiKeyAuthHandler>(MyApiKeyAuthOption.Scheme, option =>
            {
            });

            services.AddHttpContextAccessor();
            
            services.AddScoped<ApiKeyQuery>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}