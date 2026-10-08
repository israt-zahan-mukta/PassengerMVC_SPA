using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassengerMVC_SPA.Models
{
    public class Passenger
    {
        public Passenger()
        {
            this.Facilities = new HashSet<Facility>();
        }

        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public DateTime BookingDate { get; set; }
        public string MobileNo { get; set; }
        public bool IsTransit { get; set; }
        public int SeatTypeId { get; set; }
        public decimal BasicPrice { get; set; }
        public string ImageUrl { get; set; }
        public virtual SeatType SeatType { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }

    }
}