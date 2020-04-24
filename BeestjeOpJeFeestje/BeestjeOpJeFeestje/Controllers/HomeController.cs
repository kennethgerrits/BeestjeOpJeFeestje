using System.Web.Mvc;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using BeestjeOpJeFeestje.Domain.Models;

namespace BeestjeOpJeFeestje.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBoekingRepository _bookRepo;
        private readonly IBeastRepository _beastRepo;

        public HomeController(IBoekingRepository BookRepo, IBeastRepository beastRepository)
        {
            _bookRepo = BookRepo;
            _beastRepo = beastRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,ContactpersonID,Date")]
            BookingVM booking)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.TempBooking = booking;
                _beastRepo.SetFiltersToDefault();
                //_bookRepo.TempBooking.Date = booking.Date;
                return RedirectToAction("Step1", "Booking");
            }

            return View(booking);
        }
    }
}