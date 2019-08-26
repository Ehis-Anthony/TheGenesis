using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewAPIController.Models;

namespace NewAPIController.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private IRepository repository { get; set; }
        public HomeController(IRepository repo) => repository = repo;
        public ViewResult Index() => View(repository.Reservations);


        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            repository.AddReservation(reservation);
            return RedirectToAction("Index");
        }
    }
}
