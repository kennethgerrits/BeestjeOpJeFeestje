using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using BeestjeOpJeFeestje.Domain.Models;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BeastController : Controller
    {
        private readonly IBeastRepository _beastrepo;
        private IAccessoryRepository _accRepo;
        private IBoekingRepository _boekingRepository;
        public BeastController(IBeastRepository BeastRepo, IAccessoryRepository AccRepo, IBoekingRepository boekingRepository)
        {
            _beastrepo = BeastRepo;
            _accRepo = AccRepo;
            _boekingRepository = boekingRepository;
        }

        // GET: Beast
        public ActionResult Index()
        {
            var beast = _beastrepo.GetAll().Select(b => new BeastVM(b));
            return View(beast.ToList());
        }

        // GET: Beast/Details/5
        public ActionResult Details(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var beast = new BeastVM(_beastrepo.Get(id));
            if (beast == null) return HttpNotFound();
            return View(beast);
        }

        // GET: Beast/Create
        public ActionResult Create()
        {
            ViewBag.Type = new SelectList(_beastrepo.ContextDB().Type, "Type1", "Type1");
            return View();
        }

        // POST: Beast/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Type,Price")] BeastVM beast)
        {
            if (ModelState.IsValid)
            {
                _beastrepo.Add(beast.Beast);
                _beastrepo.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.Type = new SelectList(_beastrepo.ContextDB().Type, "Type1", "Type1", beast.Type);
            return View(beast);
        }

        // GET: Beast/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var beast = new BeastVM(_beastrepo.Get(id));
            if (beast == null) return HttpNotFound();
            ViewBag.Type = new SelectList(_beastrepo.ContextDB().Type, "Type1", "Type1", beast.Type);
            return View(beast);
        }

        // POST: Beast/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Type,Price")] BeastVM beast)
        {
            if (ModelState.IsValid)
            {
                _beastrepo.UpdateBeast(beast);
                _beastrepo.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.Type = new SelectList(_beastrepo.ContextDB().Type, "Type1", "Type1", beast.Type);
            return View(beast);
        }

        // GET: Beast/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var beast = new BeastVM(_beastrepo.Get(id));
            if (beast == null) return HttpNotFound();
            return View(beast);
        }

        // POST: Beast/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var beast = new BeastVM(_beastrepo.Get(id));
            _accRepo.RemoveRange(beast.Accessory);
            beast.Accessory.Clear();
            _accRepo.Complete();
            var temp = beast.Booking;
            foreach (var item in temp)
            {
                item.Beast.Remove(beast);
            }
            _boekingRepository.RecalculateTotalPrice(temp);
            _beastrepo.Remove(beast.Beast);
            _beastrepo.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}