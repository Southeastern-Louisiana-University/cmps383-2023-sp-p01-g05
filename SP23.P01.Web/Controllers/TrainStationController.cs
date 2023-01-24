using Microsoft.AspNetCore.Mvc;
using SP23.P01.Web.Common;
using System.Reflection.Metadata.Ecma335;

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
    [Route("[[/api/stations]]")]
    public class TrainStationController : ControllerBase
    {
        private readonly DataContext _context;

        public TrainStationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();
            response.Data = _context.TrainStation.ToList();

            return Ok(response);
        }
        [HttpGet("{Id:int}")]
        public IActionResult Details([FromRoute] int Id)
        {
            var response = new Response();

            var trainStationReturn = _context.TrainStation.FirstOrDefault(x => x.Id == Id);

            if (trainStationReturn == null)
            {
                return NotFound($"Unable to find Id {Id}");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            response.Data = trainStationReturn;

            return Ok(response);

        }
        [HttpPost]
        public IActionResult Create(TrainStationDto trainStation)
        {
            var response = new Response();

            if (string.IsNullOrEmpty(trainStation.Name))
            {
                return BadRequest("Name must be provided");
            }
            if (trainStation.Name.Length > 120)
            {
                return BadRequest("Name cannot be longer than 120 characters");
            }
            if (string.IsNullOrEmpty(trainStation.Address))
            {
                return BadRequest("Must have an address");
            }
            var station = new TrainStation
            {
                Name = trainStation.Name,
                Address = trainStation.Address
            };
            // Save the new station to the database
            _context.TrainStation.Add(station);
            _context.SaveChanges();

            var trainStationToReturn = new TrainStation
            {
                Id = station.Id,
                Name = station.Name,
                Address = station.Address,

            };

            response.Data = trainStationToReturn;

            return Created("", response);
        }
    }
}