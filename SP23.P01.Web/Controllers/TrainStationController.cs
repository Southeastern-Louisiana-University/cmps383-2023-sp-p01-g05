using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Common;
using SP23.P01.Web.Entities;
using System.Net;
using static System.Collections.Specialized.BitVector32;

namespace SP23.P01.Web.Controllers

// dotnet watch run, reload hot after changes
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
            // This code also works
            //var station = _context.TrainStations.FirstOrDefault(s => s.Id == Id);
            //if (station == null)
            //{
            //    return NotFound();
            //}

            //return new TrainStationDto { Id = station.Id, Name = station.Name };  

            var trainStationReturn = _context.TrainStations.FirstOrDefault(x => x.Id == Id);

            if (trainStationReturn == null)
            {
                return NotFound($"Unable to find Id {Id}");
            }

            return Ok(new TrainStationDto
            {
                Id = trainStationReturn.Id,
                Name = trainStationReturn.Name,
                Address = trainStationReturn.Address,
            });
        }

        [HttpPost]
        public ActionResult<TrainStationDto> Create(TrainStationDto trainStation)
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

            return CreatedAtAction(nameof(Details), new { Id = returnCreatedStation.Id }, returnCreatedStation);

        }

        [HttpPut("{id}")]
        public IActionResult Edit(int Id, [FromBody] TrainStationDto trainStationDto)
        {
            var response = new Response();

            var stationToEdit = _context.TrainStations.FirstOrDefault(x => x.Id == Id);


            if (stationToEdit == null)
            {
                return NotFound();
            }
            if (trainStationDto == null)
            {
                return NotFound(response);
            }
            if (trainStationDto.Name == null)
            {
                response.AddError("Name", "Name must be provided ");
            }
            if (trainStationDto.Name.Length > 120)
            {
                response.AddError("Name", "Name cannot be longer than 120 characters");
            }
            if (trainStationDto.Name == "")
            {
                response.AddError("Name", "No name provided");
            }
            if (trainStationDto.Address == null)
            {
                response.AddError("Address", "Must have an address");

            }
            if (trainStationDto.Address == "")
            {
                response.AddError("Address", "Must have address");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            stationToEdit.Name = trainStationDto.Name;
            stationToEdit.Address = trainStationDto.Address;

            _context.SaveChanges();

            trainStationDto.Id = stationToEdit.Id;


            return Ok(trainStationDto);
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