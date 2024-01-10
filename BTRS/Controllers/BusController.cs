using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BTRS.Controllers
{
    public class BusController : Controller
    {
        private SystemDbContext _context;

        public BusController(SystemDbContext context)
        {
            this._context = context;
        }
        // GET: BusController
        public async Task<IActionResult> Index()
        {
            return _context.bus != null ?
                          View(await _context.bus.ToListAsync()) :
                          Problem("Entity set 'SystemDbContext.bus'  is null.");
        }

        // GET: BusController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.bus == null)
            {
                return NotFound();
            }

            var bus = await _context.bus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: BusController/Create
        public ActionResult Create()
        {


           
            ViewBag.Trip = _context.trip.ToList();
           

            return View();
        }

        // POST: BusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            
            
            int tripID = int.Parse(form["tripID"]);
            

            int adminid = (int)HttpContext.Session.GetInt32("adminID");

            Admin admin = _context.admin.Where(
              a => a.Id == adminid
              ).FirstOrDefault();

            


            string CaptainName = form["CaptinName"].ToString();
            int Num_of_S =int.Parse(form["Num_of_S"]);

            Bus bus = new Bus();
            bus.CaptinName = CaptainName;
            bus.Num_of_S = Num_of_S;

            bus.trip = _context.trip.Find(tripID);
            bus.admin = admin;
            

            _context.bus.Add(bus);
            _context.SaveChanges();

            return RedirectToAction("Index");




        }

        // GET: BusController/Edit/5
        public async Task<IActionResult> Edit(int? id)

        {
            ViewBag.Trip = _context.trip.ToList();
            if (id == null || _context.bus == null)
            {
                return NotFound();
            }

            var bus = await _context.bus.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        // POST: BusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
            int tripID = int.Parse(form["tripID"]);
            string CaptainName = form["CaptinName"].ToString();
            int Num_of_S = int.Parse(form["Num_of_S"]);
            id = int.Parse(form["Id"]);

            Bus bus = _context.bus.Where(p => p.Id== id).FirstOrDefault();
            bus.CaptinName = CaptainName;
            bus.Num_of_S = Num_of_S;

            bus.trip = _context.trip.Find(tripID);

            _context.bus.Update(bus);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: BusController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.bus == null)
            {
                return NotFound();
            }

            var bus = await _context.bus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // POST: BusController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.bus == null)
            {
                return Problem("Entity set 'SystemDbContext.bus'  is null.");
            }
            var bus = await _context.bus.FindAsync(id);
            if (bus != null)
            {
                _context.bus.Remove(bus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BusExist(int id)
        {
            return (_context.bus?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
