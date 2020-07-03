using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthenticationSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "ok";
        }

        [HttpGet]
        [Authorize]
        public string GetAuth()
        {
            return "ok auth";
        }
    }
}