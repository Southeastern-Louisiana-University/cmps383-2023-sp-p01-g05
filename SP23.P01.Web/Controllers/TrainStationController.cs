using Microsoft.AspNetCore.Mvc;
using SP23.P01.Web.Common;

namespace SP23.P01.Web.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class WeatherForecastController : ControllerBase
//    {
//        private static readonly string[] Summaries = new[]
//        {
//        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//    };

//        private readonly ILogger<WeatherForecastController> _logger;

//        public WeatherForecastController(ILogger<WeatherForecastController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpGet(Name = "GetWeatherForecast")]
//        public IEnumerable<WeatherForecast> Get()
//        {
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//    }
//}
{
    [ApiController]
    [Route("[controller]")]
    public class TrainStationController : ControllerBase
    {
        private readonly DataContext _context;

        public TrainStationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("list-all")]
        public IActionResult GetAll()
        {
            var response = new Response();
             response.Data = _context.TrainStations.ToList();

            return Ok(response);
        }
    }

}