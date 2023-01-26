using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Common;
using SP23.P01.Web.Entities;
using System.Net;
using static System.Collections.Specialized.BitVector32;

namespace SP23.P01.Web.Controllers
//{
    // NOT DELETE COMMENT, IN class notes
    // dotnet watch run, reload hot after changes



    //--------------------------------------------
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
    [Route("/api/stations")]
    public class TrainStationController : ControllerBase
    {
        private DataContext _context;

        public TrainStationController(DataContext context)
        {
           _context = context;
        }

        [HttpGet]
        public ActionResult<TrainStationDto[]> Get()
        {
            var result = _context.TrainStations;
                       return Ok(result.Select(x => new TrainStationDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
            }));         
        }
        
        [HttpGet("{Id}")]
        public ActionResult<TrainStationDto> Details([FromRoute] int Id)
        {
            var response = new Response();

            var trainStationReturn = _context.TrainStations;

            if (trainStationReturn == null)
            {
                return NotFound($"Unable to find Id {Id}");
            }

            return Ok(trainStationReturn.Where(x => x.Id == Id).Select(x => new TrainStationDto
            { 
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,          
            }).FirstOrDefault());
            
        }

        [HttpPost]
        public ActionResult<TrainStationDto> Create (TrainStationDto trainStation)
        {
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
            var returnCreatedStation = new TrainStation
            {
                Name = trainStation.Name,
                Address = trainStation.Address,
            };

            _context.TrainStations.Add(returnCreatedStation);
            _context.SaveChanges();

            trainStation.Id = returnCreatedStation.Id;
            
            return CreatedAtAction(nameof(Details), new {Id = returnCreatedStation.Id}, returnCreatedStation);

        }
       
        [HttpPut("{id}")]
        public IActionResult Edit(int Id, [FromBody] TrainStationDto trainStationDto)
        {
            //TODO

            return Ok();
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteStation(int Id)
        {
            //TODO
            
            
            
            return Ok();  
        }
    }
}