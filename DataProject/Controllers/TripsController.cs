using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataProject;
using PagedList.Mvc;
using PagedList;
using System.Net.Mail;

namespace DataProject.Controllers
{
    public class TripsController : Controller
    {
        private AeroModel db = new AeroModel();

        // GET: Trips
        public ActionResult Index(string searchTownFrom, string searchTownTo, string searchCompany, string searchPlane, int? page, string sortOrder)
        {
            var TownToList = new List<string>();
            var CompanyList = new List<string>();
            var PlaneList = new List<string>();
            var TownFromList = new List<string>();

            var CompanyListQ = db.Trip.OrderBy(y => y.Company.name).Select(y => y.Company.name);
            CompanyList.AddRange(CompanyListQ.Distinct());
            ViewBag.searchCompany = new SelectList(CompanyList);

            var TownFromListQ = db.Trip.OrderBy(y => y.town_from).Select(y => y.town_from);
            TownFromList.AddRange(TownFromListQ.Distinct());
            ViewBag.searchTownFrom = new SelectList(TownFromList);

            var PlaneListQ = db.Trip.OrderBy(y => y.plane).Select(y => y.plane);
            PlaneList.AddRange(PlaneListQ.Distinct());
            ViewBag.searchPlane = new SelectList(PlaneList);

            var TownToListQ = db.Trip.OrderBy(y => y.town_to).Select(y => y.town_to);
            TownToList.AddRange(TownToListQ.Distinct());
            ViewBag.searchTownTo = new SelectList(TownToList);

            var trip = db.Trip.Include(t => t.Company);

            if (!String.IsNullOrEmpty(searchTownFrom))
            {
                trip = trip.Where(s => s.town_from==searchTownFrom);
            }
            if (!string.IsNullOrEmpty(searchTownTo))
            {
                trip = trip.Where(x => x.town_to == searchTownTo);
            }
            if (!String.IsNullOrEmpty(searchPlane))
            {
                trip = trip.Where(s => s.plane==searchPlane);
            }
            if (!String.IsNullOrEmpty(searchCompany))
            {
                trip = trip.Where(s => s.Company.name == searchCompany);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CompanySortParm = String.IsNullOrEmpty(sortOrder) ? "company_desc" : "";
            ViewBag.DepartureTime = sortOrder == "DepTime" ? "deptime_desc" : "DepTime";
            ViewBag.ArrivalTime = sortOrder == "ArrTime" ? "arrtime_desc" : "ArrTime";
            ViewBag.Plane = sortOrder == "Plane" ? "plane_desc" : "Plane";
            ViewBag.FromTown = sortOrder == "FromTown" ? "fromtown_desc" : "FromTown";
            ViewBag.ToTown = sortOrder == "ToTown" ? "totown_desc" : "ToTown";
            //  var trips = from s in db.Trip
            //               select s;
            switch (sortOrder)
            {
                case "company_desc":
                    trip = trip.OrderByDescending(s => s.Company.name);
                    break;
                case "DepTime":
                    trip = trip.OrderBy(s => s.time_out);
                    break;
                case "deptime_desc":
                    trip = trip.OrderByDescending(s => s.time_out);
                    break;
                case "ArrTime":
                    trip = trip.OrderBy(s => s.time_in);
                    break;
                case "arrtime_desc":
                    trip = trip.OrderByDescending(s => s.time_in);
                    break;
                case "Plane":
                    trip = trip.OrderBy(s => s.plane);
                    break;
                case "plane_desc":
                    trip = trip.OrderByDescending(s => s.plane);
                    break;
                case "FromTown":
                    trip = trip.OrderBy(s => s.town_from);
                    break;
                case "fromtown_desc":
                    trip = trip.OrderByDescending(s => s.town_from);
                    break;
                case "ToTown":
                    trip = trip.OrderBy(s => s.town_to);
                    break;
                case "totown_desc":
                    trip = trip.OrderByDescending(s => s.town_to);
                    break;
                default:
                    trip = trip.OrderBy(s => s.Company.name);
                    break;
            }

                    return View(trip.ToPagedList(pageNumber,pageSize));
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {

            ViewBag.ID_comp = new SelectList(db.Company, "ID_comp", "name");
            var Plane = db.Trip.Select(x => x.plane).Distinct();
            ViewBag.Plane = new SelectList (Plane);
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "trip_no,ID_comp,plane,town_from,town_to,time_out,time_in")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.trip_no = (db.Trip.Select(x => x.trip_no).ToList().Last()) + 1;
                db.Trip.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_comp = new SelectList(db.Company, "ID_comp", "name", trip.ID_comp);
            return View(trip);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_comp = new SelectList(db.Company, "ID_comp", "name", trip.ID_comp);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "trip_no,ID_comp,plane,town_from,town_to,time_out,time_in")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_comp = new SelectList(db.Company, "ID_comp", "name", trip.ID_comp);
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trip.Find(id);
            db.Trip.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Contact ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string title, string text)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("djlex05@gmail.com");
            mail.From = new MailAddress("djlex05@gmail.com", "Oleg Lyahovetskyi App");
            mail.Subject = title;
            mail.Body = text;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential("djlex05@gmail.com", "lexxel11");

            if (title != "" && text != "")
            {
                smtp.Send(mail);
                ViewBag.Text = "Thanks for your letter";
            }
            else
            {
                ViewBag.Error = "Submit \"Subject\" and \"Message\"";
            }
            return View();
        }
    }
}
