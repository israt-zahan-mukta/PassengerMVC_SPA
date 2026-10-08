using PassengerMVC_SPA.DAL;
using PassengerMVC_SPA.Models;
using PassengerMVC_SPA.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PassengerMVC_SPA.Controllers
{
    public class PassengersController : Controller
    {
        AirDbContext db = new AirDbContext();
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Passenger> passengers = db.Passengers.Include(s => s.SeatType).Include(f => f.Facilities).ToList();
            return View(passengers);
        }

        [HttpGet]
        public ActionResult CreatePartial()
        {
            PassengerViewModel passenger = new PassengerViewModel();
            passenger.SeatTypes = db.SeatTypes.ToList();
            passenger.Facilities.Add(new Facility() { FacilityId = 1 });
            return PartialView("_CreatePassengerPartial", passenger);
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public JsonResult CreatePassenger(PassengerViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.SeatTypes = db.SeatTypes.ToList();
                return Json(new { success = false });
            }
            Passenger passenger = new Passenger
            {
                PassengerName = vobj.PassengerName,
                BookingDate = vobj.BookingDate,
                MobileNo = vobj.MobileNo,
                SeatTypeId = vobj.SeatTypeId,
                BasicPrice = vobj.BasicPrice,
                IsTransit = vobj.IsTransit,
                Facilities = vobj.Facilities
            };

            if (vobj.ProfileFile != null)
            {
                string uniqueFileName = GetFileName(vobj.ProfileFile);
                passenger.ImageUrl = uniqueFileName;
            }
            else
            {
                passenger.ImageUrl = "noimage.png";
            }

            db.Passengers.Add(passenger);
            try
            {
                db.SaveChanges();
                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }
            catch (Exception)
            {
                vobj.SeatTypes = db.SeatTypes.ToList();
                return Json(new { success = false });
            }
        }
        private string GetFileName(HttpPostedFileBase file)
        {
            string fileName = "";
            if (file != null)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                file.SaveAs(path);
            }
            return fileName;
        }

        [HttpPost]
        public JsonResult DeletePassenger(int id)
        {
            Passenger passenger = db.Passengers.Find(id);
            if (passenger != null)
            {
                var facilities = db.Facilities.Where(p => p.PassengerId == id).ToList();
                db.Facilities.RemoveRange(facilities);
                db.Entry(passenger).State = EntityState.Deleted;
                db.SaveChanges();
                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }
            return Json(new { success = false, message = "Passenger not found." });
        }

        public ActionResult EditPartial(int id)
        {
            var passenger = db.Passengers.Include(c => c.SeatType).Include(c => c.Facilities).Where(p => p.PassengerId == id).FirstOrDefault();
            if (passenger == null)
            {
                return HttpNotFound("Passenger Not found");
            }
            var vObj = new PassengerViewModel
            {
                PassengerName = passenger.PassengerName,
                PassengerId = passenger.PassengerId,
                BookingDate = passenger.BookingDate,
                BasicPrice = passenger.BasicPrice,
                MobileNo = passenger.MobileNo,
                SeatTypeId = passenger.SeatTypeId,
                IsTransit = passenger.IsTransit,
                ImageUrl = passenger.ImageUrl,
                Facilities = passenger.Facilities.ToList(),
                SeatTypes = db.SeatTypes.ToList()
            };
            return PartialView("_EditPassengerPartial", vObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditPassenger(PassengerViewModel vobj, string OldImageUrl)
        {
            if (!ModelState.IsValid)
            {
                vobj.SeatTypes = db.SeatTypes.ToList();
                return Json(new { success = false });
            }
            Passenger obj = db.Passengers
                .Include(a => a.Facilities)
                .FirstOrDefault(x => x.PassengerId == vobj.PassengerId);
            if (obj != null)
            {
                obj.PassengerName = vobj.PassengerName;
                obj.SeatTypeId = vobj.SeatTypeId;
                obj.MobileNo = vobj.MobileNo;
                obj.IsTransit = vobj.IsTransit;
                obj.BookingDate = vobj.BookingDate;
                obj.BasicPrice = vobj.BasicPrice;
                if (vobj.ProfileFile != null)
                {
                    string uniqueFileName = GetFileName(vobj.ProfileFile);
                    obj.ImageUrl = uniqueFileName;
                }
                else
                {
                    obj.ImageUrl = OldImageUrl;
                }

                var existingFacilityIds = obj.Facilities.Select(m => m.FacilityId).ToList();
                var updatedFacilityIds = vobj.Facilities.Where(m => m.FacilityId > 0).Select(m => m.FacilityId).ToList();
                var facilitiesToRemove = obj.Facilities.Where(m => !updatedFacilityIds.Contains(m.FacilityId)).ToList();
                foreach (var facilityToRemove in facilitiesToRemove)
                {
                    db.Facilities.Remove(facilityToRemove);
                }
                foreach (var item in vobj.Facilities)
                {
                    if (item.FacilityId > 0)
                    {
                        var existingFacility = obj.Facilities.FirstOrDefault(m => m.FacilityId == item.FacilityId);
                        if (existingFacility != null)
                        {
                            existingFacility.FacilityName = item.FacilityName;
                            existingFacility.Price = item.Price;
                        }
                    }
                    else
                    {
                        var newFacility = new Facility
                        {
                            PassengerId = obj.PassengerId,
                            FacilityName = item.FacilityName,
                            Price = item.Price
                        };
                        obj.Facilities.Add(newFacility);
                    }
                }

                try
                {
                    db.SaveChanges();
                    return Json(new { success = true, redirectUrl = Url.Action("Index") });
                }
                catch (Exception)
                {

                    vobj.SeatTypes = db.SeatTypes.ToList();
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false, errors = new[] { "Passenger not found." } });
        }

    }
}