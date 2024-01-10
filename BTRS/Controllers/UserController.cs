using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BTRS.Controllers
{

    public class UserController : Controller
    {
        private SystemDbContext _context;

        public UserController(SystemDbContext context)
        {
            this._context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Passenger passenger)
        {
            bool empty = checkEmpty(passenger);
            bool duplicat = checkUsername(passenger.Username);

            if (empty)
            {
                if (duplicat)
                {
                    _context.passenger.Add(passenger);
                    _context.SaveChanges();

                    TempData["Msg"] = "the data was saved";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Msg"] = "Please Change the username";
                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "Please fill all input ";
                return View();
            }



        }

        public bool checkUsername(string username)
        {

            Passenger passenger = _context.passenger.Where(u => u.Username.Equals(username)).FirstOrDefault();
            if (passenger != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool checkEmpty(Passenger passenger)
        {
            if (String.IsNullOrEmpty(passenger.Username)) return false;
            else if (String.IsNullOrEmpty(passenger.password)) return false;
            else if (String.IsNullOrEmpty(passenger.Name)) return false;
            else if (String.IsNullOrEmpty(passenger.phoneNumber)) return false;
            else if (String.IsNullOrEmpty(passenger.gender)) return false;

            else return true;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login userlogin)
        {
            if (ModelState.IsValid)
            {
                string username = userlogin.username;
                string password = userlogin.password;

                Passenger user = _context.passenger.Where(
                     u => u.Username.Equals(username) &&
                     u.password.Equals(password)
                     ).FirstOrDefault();

                Admin admin = _context.admin.Where(
                    a => a.Username.Equals(username)
                    &&
                    a.password.Equals(password)
                    ).FirstOrDefault();




                if (user != null)
                {
                    HttpContext.Session.SetInt32("userID", user.PassengerId);

                    return RedirectToAction("TripList");
                }
                else if (admin != null)
                {

                    HttpContext.Session.SetInt32("adminID", admin.Id);

                    return RedirectToAction("Index", "Trip");
                }
                else
                {
                    TempData["Msg"] = "The user Not Found";
                }


            }
            else
            {

            }
            return View();
        }
        public IActionResult TripList()
        {

            //int userid = (int)HttpContext.Session.GetInt32("userID");
            ////List<int> lst_Trip_Passenger = _context.Trip_Passenger.Where(
            ////    u => u.Passenger.PassengerId == userid).Select(t => t.trip.Id).ToList();
            //List<Trip> lst_trip = _context.trip.Where(t => lst_Trip_Passenger.Contains(t.Id) == false).ToList();
            return View(_context.trip.ToList());

            ViewBag.Message = TempData["Message"];


            //return View(lst_trip);
        }

        public IActionResult BusDets(int id)
        {
            List<Bus> tripBuses = _context.bus.Where(b => b.trip.Id == id).ToList();

            return View(tripBuses);
        }
        public IActionResult Book(int id)
        {
            
            int userID = (int)HttpContext.Session.GetInt32("userID");
            bool isAlreadyBooked = _context.Trip_Passenger.Any(t => t.Passenger.PassengerId == userID && t.trip.Id == id);

            if (isAlreadyBooked)
            {
                TempData["Message"] = "Trip already booked!";
                return RedirectToAction("TripList");
            }


            Trip_Passenger trip_Passenger = new Trip_Passenger();
            trip_Passenger.Passenger = _context.passenger.Find(userID);
            trip_Passenger.trip = _context.trip.Find(id);
            _context.Trip_Passenger.Add(trip_Passenger);
            _context.SaveChanges();



            return RedirectToAction("BookList");
            
        }
        public IActionResult BookList()
        {
            int userID = (int)HttpContext.Session.GetInt32("userID");

            List<int> lst_user = _context.Trip_Passenger.Where(
                t => t.Passenger.PassengerId == userID).Select(s => s.trip.Id).ToList();

            List<Trip> lst_trip = _context.trip.Where(
                t => lst_user.Contains(t.Id)
                ).ToList();

            return View(lst_trip);
        }
        public IActionResult DeleteBook(Trip_Passenger trip_Passenger,int id)
        {
            int userID = (int)HttpContext.Session.GetInt32("userID");
            trip_Passenger = _context.Trip_Passenger.Where(t => t.trip.Id == id && t.Passenger.PassengerId == userID).FirstOrDefault();


            _context.Trip_Passenger.Remove(trip_Passenger);
            _context.SaveChanges();


            return RedirectToAction("BookList");
        }




    }
}


