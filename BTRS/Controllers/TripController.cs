using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTRS.Controllers
{
    public class TripController : Controller
    {
        private SystemDbContext _context;
        public TripController(SystemDbContext context)
        {
            this._context = context;
        }
        // GET: TripController
        public ActionResult Index()
        {
            return View(_context.trip.ToList());
        }

        // GET: TripController/Details/5
        public ActionResult Details(int id)
        {
            Trip trip = _context.trip.Find(id);

            return View(trip);
        }

        // GET: TripController/Create
        public ActionResult Create()
        {
            ViewBag.Trip = _context.trip.ToList();
            return View();
        }

        // POST: TripController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trip trip)
        {
            try
            {

                int adminid = (int)HttpContext.Session.GetInt32("adminID");

                Admin admin = _context.admin.Where(
                  a => a.Id == adminid
                  ).FirstOrDefault();

                trip.admin = admin;

                _context.trip.Add(trip);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TripController/Edit/5
        public ActionResult Edit(int id)
        {
            Trip trip = _context.trip.Find(id);

            return View(trip);
        }

        // POST: TripController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Trip trip)
        {
            try
            {

                _context.trip.Update(trip);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: TripController/Delete/5
        public ActionResult Delete(int id)
        {
           
           
            Trip trip = _context.trip.Find(id);
            var bus = _context.bus.Where(c => c.trip.Id == id).ToList();

            if (bus.Any())
            {
                foreach(var item in bus)
                {
                    _context.bus.Remove(item);
                }
            }
            _context.trip.Remove(trip);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        // POST: TripController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Trip trip)
        {
            try
            {
                _context.trip.Remove(trip);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

         public IActionResult BusDets(int id)
            {


            List<Bus> tripBuses = _context.bus.Where(b => b.trip.Id == id).ToList();

            return View(tripBuses);
        }
        
 
    }
}
