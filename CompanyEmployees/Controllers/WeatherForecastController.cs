using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Here is info message from out values controller");
            _logger.LogDebug("Here is debug message from out value controller");
            _logger.LogWarn("Here is warn message from our value controller");
            _logger.LogError("Here is error message from out value controller");

            return new string[] { "value1", "value2" };
        }
    }
}
