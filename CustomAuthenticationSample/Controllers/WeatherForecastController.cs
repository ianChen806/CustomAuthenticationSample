using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthenticationSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly HttpContext _contextAccessor;

        public WeatherForecastController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor.HttpContext;
        }

        [HttpGet]
        public string Get()
        {
            return "ok";
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public string GetAdmin()
        {
            var name = _contextAccessor.User.Identity.Name;
            return $"ok {name}";
        }

        [HttpGet]
        [Authorize]
        public string GetAll()
        {
            var name = _contextAccessor.User.Identity.Name;
            return $"ok all {name}";
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public string GetUser()
        {
            var name = _contextAccessor.User.Identity.Name;
            return $"ok {name}";
        }
    }
}