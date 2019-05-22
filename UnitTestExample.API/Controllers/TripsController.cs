using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnitTestExample.Business;
using UnitTestExample.Business.Models;
using UnitTestExample.Business.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnitTestExample.API.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private readonly TripService _tripService;

        public TripsController(TripService tripService)
        {
            _tripService = tripService;
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var trip = _tripService.Get(id);
            if (trip == null)
                return NotFound();
            else
                return new ObjectResult(trip);
        }

        // POST api/trips
        [HttpPost]
        public Result Post([FromBody]Trip trip)
        {
            return _tripService.Add(trip);
        }

        // PUT api/trips/5
        [HttpPut]
        public Result Put([FromBody]Trip trip)
        {
            return _tripService.Update(trip);
        }

        // DELETE api/trips/5
        [HttpDelete]
        public Result Delete([FromBody]Guid tripId)
        {
            return _tripService.Delete(tripId);
        }
    }
}
