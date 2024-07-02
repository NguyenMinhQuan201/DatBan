using EmailApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatBan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public WeatherForecastController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var message = new Message(new string[] { "quannm2812n5@gmail.com" }, "Test email", "This is the content from our email.");
            _emailSender.SendEmail(message);
            return Ok(message);
        }
    }
    
}
