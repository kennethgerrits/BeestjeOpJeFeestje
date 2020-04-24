using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using BeestjeOpJeFeestje.Domain.Models;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BookingController : Controller
    {
        private readonly IAccessoryRepository _accrepo;
        private readonly IBeastRepository _beastrepo;
        private readonly IBoekingRepository _boekingRepository;
        private readonly IContactpersonRepository _contactrepo;

        public BookingController(IBoekingRepository boekingRepository, IBeastRepository BeastRepo,
            IAccessoryRepository AccRepo, IContactpersonRepository ContactRepo)
        {
            _boekingRepository = boekingRepository;
            _beastrepo = BeastRepo;
            _accrepo = AccRepo;
            _contactrepo = ContactRepo;
        }

        public BookingController(IBoekingRepository boekingRepository)
        {
            _boekingRepository = boekingRepository;
        }

        public List<BeastVM> AllBeasts { get; set; }

        // GET: Booking
        public ActionResult Index()
        {
            var booking = _boekingRepository.GetAll().Select(b => new BookingVM(b));
            return View(booking.ToList());
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var booking = new BookingVM(_boekingRepository.Get(id));

            if (booking == null) return HttpNotFound();
            return View(booking);
        }


        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var booking = new BookingVM(_boekingRepository.Get(id));

            if (booking == null) return HttpNotFound();
            return View(booking);
        }

        #region AddAnimal

        [HttpPost]
        public ActionResult AddCheckedAnimal()
        {
            BeastVM beastie = null;
            var temp = _boekingRepository.TempBooking;
            var id = int.Parse(Request.Form.Get("BeastID"));
            var beastieList = _boekingRepository.AnimalsBooked().ToList();
            foreach (var item in beastieList)
            {
                if(id == item.ID)
                {
                    beastie = item;
                }
            }
            if(beastie == null)
            {
               beastie = new BeastVM(_beastrepo.Get(id));
            }
            if (beastieList.Contains(beastie))
            {
                beastieList.Remove(beastie);
                temp.Beast = beastieList;
                _boekingRepository.TempBooking = temp;
                if (!_boekingRepository.PolarLionExists()) _beastrepo.ExcludeFarm = false;
                if (!_boekingRepository.FarmExists()) _beastrepo.ExcludePolarLion = false;
                InfoBar();
                return RedirectToAction("Step1");
            }

            if (beastie.Name == "Leeuw" || beastie.Name == "Ijsbeer")
                _beastrepo.ExcludeFarm = true;
            if (beastie.Type == "Boerderij")
                _beastrepo.ExcludePolarLion = true;
            beastieList.Add(beastie);
            temp.Beast = beastieList;
            _boekingRepository.TempBooking = temp;
            InfoBar();

            return RedirectToAction("Step1");
        }



        #endregion
        #region TestMethods
        public ActionResult AddCheckedAnimal(BeastVM beast)
        {
            BeastVM beastie = beast;
            var temp = _boekingRepository.TempBooking;
            var beastieList = _boekingRepository.AnimalsBooked().ToList();
            foreach (var item in beastieList)
            {
                if (beastie.ID == item.ID)
                {
                    beastie = item;
                }
            }
            if (beastieList.Contains(beastie))
            {
                beastieList.Remove(beastie);
                temp.Beast = beastieList;
                _boekingRepository.TempBooking = temp;
                if (!_boekingRepository.PolarLionExists()) _beastrepo.ExcludeFarm = false;
                if (!_boekingRepository.FarmExists()) _beastrepo.ExcludePolarLion = false;
                InfoBar();
                return RedirectToAction("Step1");
            }

            if (beastie.Name == "Leeuw" || beastie.Name == "Ijsbeer")
                _beastrepo.ExcludeFarm = true;
            if (beastie.Type == "Boerderij")
                _beastrepo.ExcludePolarLion = true;
            beastieList.Add(beastie);
            temp.Beast = beastieList;
            _boekingRepository.TempBooking = temp;
            InfoBar();

            return RedirectToAction("Step1");
        }
        #endregion

        #region AddAccessory

        [HttpPost]
        public ActionResult AddCheckedAccessory()
        {
            AccessoryVM accessory = null;
            var temp = _boekingRepository.TempBooking;
            var id = int.Parse(Request.Form.Get("AccID"));
            var accList = _boekingRepository.AccessoriesBooked().ToList();
            //var acc = new AccessoryVM(_accrepo.Get(int.Parse(Request.Form.Get("AccID"))));
            foreach (var item in accList)
            {
                if (id == item.ID)
                {
                    accessory = item;
                }
            }
            if (accessory == null)
            {
                accessory = new AccessoryVM(_accrepo.Get(id));
            }
            if (accList.Contains(accessory))
            {
                accessory.Selected = "Selecteren";
                accessory.IsSelected = false;
                accList.Remove(accessory);
                temp.Accessory = accList;
                _boekingRepository.TempBooking = temp;
                InfoBar();
                return RedirectToAction("Step2");
            }

            accessory.Selected = "Deselecteren";
            accessory.IsSelected = true;
            accList.Add(accessory);
            temp.Accessory = accList;
            _boekingRepository.TempBooking = temp;
            InfoBar();

            return RedirectToAction("Step2");
        }

        #endregion

        public ActionResult InfoBar()
        {
            return View(_boekingRepository.TempBooking);
        }

        // POST: Booking/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var booking = _boekingRepository.Get(id);
            booking.Accessory.Clear();
            booking.Beast.Clear();
            var person = booking.ContactPerson;
            
            _boekingRepository.Remove(booking);
            _contactrepo.Remove(person);
            _boekingRepository.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }

        #region Step1

        public ActionResult Step1()
        {
            var temp = _boekingRepository.TempBooking;
            _beastrepo.ExcludeUnavailable = true;
            _beastrepo.ExcludeDesert = Validator.ExcludeDesert(temp);
            _beastrepo.ExcludeSnow = Validator.ExcludeSnow(temp);
            _beastrepo.ExcludePinguin = Validator.IsWeekend(temp);
            AllBeasts = new List<BeastVM>(_beastrepo.BeastsAvailable(temp.Date));
            foreach (var item in AllBeasts)
            {
                for (int i = 0; i < temp.Beast.Count; i++)
                {
                    if (temp.Beast[i].ID == item.ID)
                    {
                        item.Selected = "Deselecteren";
                    }
                }
            }
            
            return View(AllBeasts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step1(string z)
        {
            return RedirectToAction("Step2", "Booking");
        }

        #endregion

        #region Step2

        public ActionResult Step2()
        {
            var temp = _boekingRepository.TempBooking;
            var booked = _boekingRepository.AvailableAccessories();
            foreach (var item in booked)
            {
                for (int i = 0; i < temp.Accessory.Count; i++)
                {
                    if (temp.Accessory[i].ID == item.ID)
                    {
                        item.Selected = "Deselecteren";
                    }
                }
            }
            return View(booked);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step2(string z)
        {
            return RedirectToAction("Step3", "Booking");
        }

        #endregion

        #region Step3

        public ActionResult Step3()
        {
            return View();
        }

        // POST: ContactPerson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step3([Bind(Include = "ID,FirstName,InBetween,LastName,Adress,Email,PhoneNumber")]
            ContactpersonVM contactPerson)
        {
            if (ModelState.IsValid)
            {
                _contactrepo.TempPerson = contactPerson;
                _boekingRepository.TempBooking.ContactPerson = contactPerson;
                return RedirectToAction("Step4");
            }

            return View(contactPerson);
        }

        #endregion

        #region Step4

        public ActionResult Step4()
        {
            var calc = new DiscountCalculator();
            var booking = _boekingRepository.TempBooking;
            booking.Discounts = calc.CalculateTotalDiscount(booking.Booking);
            booking.Price = calc.CalculateTotalPrice(booking.Booking);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step4(string z)
        {
            _contactrepo.Add(_contactrepo.TempPerson.ContactPerson);
            _contactrepo.Complete();
            _boekingRepository.Add(_boekingRepository.TempBooking.Booking);
            _boekingRepository.Complete();
            _boekingRepository.TempBooking = new BookingVM();
            return RedirectToAction("Index");
        }

        #endregion
    }
}