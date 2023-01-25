using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Common;
using SP23.P01.Web.Entities;
using System.Net;

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

            response.Data =  _context.TrainStations.ToList();
            if(response.Data == null) 
            { 
                return NotFound();
            }

            return Ok(response);
        }  
        [HttpGet("{Id:int}")]
        public ActionResult<TrainStationDto> Details([FromRoute] int Id)
        {
            var response = new Response();

            var trainStationReturn = _context.TrainStations.FirstOrDefault(x => x.Id == Id);

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
            _context.TrainStations.Add(station);
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
        [HttpPut("{id}")]
        public IActionResult Edit(int Id, [FromBody] TrainStationDto trainStationDto)
        {
            var response = new Response();

            var stationToEdit = _context.TrainStations.Find(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (trainStationDto == null)
            {
                response.AddError("ID", "Id Not Found");
                return NotFound(response);
            }
            if (trainStationDto.Name == null || trainStationDto.Name.Length > 120)
            {
            response.AddError("Name", "Name must be provided or Name cannot be longer than 120 characters");
            }
            if(trainStationDto.Name == "")
            {
                response.AddError("Name", "No name provided");
            }
            if(trainStationDto.Address == null || trainStationDto.Address == "")
            {
                response.AddError("Address", "Must have an address");

            }
            if(response.HasErrors)
            {
                return BadRequest(response);
            }

            stationToEdit.Name = trainStationDto.Name;
            stationToEdit.Address = trainStationDto.Address;

            _context.SaveChanges();

            response.Data = stationToEdit;

            return Ok(response);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteStation(int Id)
        {

            var response = new Response();

            var station = _context.TrainStations.Find(Id);
            if (station == null)
            {
                response.AddError("Id", "There was problem deleting a station");
                return NotFound();
            }
            _context.TrainStations.Remove(station);
            _context.SaveChanges();

            response.Data = true;


            return Ok(response);    
        }
    }
}