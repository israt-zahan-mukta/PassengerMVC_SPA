using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassengerMVC_SPA.Models
{
    public class SeatType
    {
        public SeatType()
        {
            this.Passengers = new HashSet<Passenger>();
        }
        public int SeatTypeId { get; set; }
        public string SeatTypeName { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; }
    }
}