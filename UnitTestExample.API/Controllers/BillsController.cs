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
    public class BillsController : Controller
    {
        private readonly BillService _billService;

        public BillsController(BillService billService)
        {
            _billService = billService;
        }


        // GET: api/bills
        [HttpGet, Route("bills/trip/{tripId}")]
        public IEnumerable<Bill> GetTripBills(Guid tripId)
        {
            return _billService.GetBills(tripId);
        }

        // GET api/bills/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var bill = _billService.Get(id);
            if (bill == null)
                return NotFound();
            else
                return new ObjectResult(bill);
        }

        // POST api/bills
        [HttpPost]
        public Result Post([FromBody]Bill bill)
        {
            return _billService.Add(bill);
        }

        // PUT api/bills/5
        [HttpPut]
        public Result Put([FromBody]Bill bill)
        {
            return _billService.Update(bill);
        }

        // DELETE api/bills/5
        [HttpDelete]
        public Result Delete([FromBody]Guid billId)
        {
            return _billService.Delete(billId);
        }
    }
}
