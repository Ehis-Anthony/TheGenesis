using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewAPIController.Models;

namespace NewAPIController.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository repository;

        public ReservationController(IRepository repo) => repository = repo;

        //API To get the entire list of the available reservations
        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;
        
        //API To get the details of a particular reservation
        [HttpGet("{id}")]
        public Reservation Get(int id) => repository[id];
        
        //API To post details of a new reservation created
        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
            repository.AddReservation(new Reservation {
                ClientName = res.ClientName,
                Location = res.Location
            });
        
        //API to update the details of a particuar API
        [HttpPut]
        public Reservation Put([FromBody] Reservation res) =>
            repository.UpdateReservation(res);
        
        //API to 
        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody]JsonPatchDocument<Reservation> patch)
        {
            Reservation res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }
        

        //API to delete a reservation
        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
        
    }
}
