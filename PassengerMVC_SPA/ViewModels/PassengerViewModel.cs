using PassengerMVC_SPA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassengerMVC_SPA.ViewModels
{
    public class PassengerViewModel
    {
        public int PassengerId { get; set; }
        [Display(Name ="Passenger Name")]
        [Required(ErrorMessage ="Passenger name is required")]
        public string PassengerName { get; set; }
        [Display(Name = "Booking Date")]
        [Required(ErrorMessage = "Booking date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BookingDate { get; set; } = DateTime.Now;
        [Display(Name = "Mobile No")]
        [Required(ErrorMessage = "Mobile no is required")]
        public string MobileNo { get; set; }
        [Display(Name = "Transit?")]
        public bool IsTransit { get; set; }
        [Display(Name = "Basic Price")]
        public decimal BasicPrice { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Facility")]
        [Required(ErrorMessage = "Facility is required")]
        public int FacilityId { get; set; }
        [Display(Name = "Seat-Type")]
        [Required(ErrorMessage = "Seat type is required")]
        public int SeatTypeId { get; set; }
        public string SeatTypeName { get; set; }
        public string FacilityName { get; set; }
        public decimal Price { get; set; }
        [Display(Name ="Upload Picture")]
        public HttpPostedFileBase ProfileFile {  get; set; }
        public virtual IList<SeatType> SeatTypes { get; set; }
        public virtual IList<Passenger> Passengers { get; set; }
        public virtual IList<Facility> Facilities { get; set; } = new List<Facility>();
    }
}