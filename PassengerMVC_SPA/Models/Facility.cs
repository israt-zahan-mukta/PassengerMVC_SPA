using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassengerMVC_SPA.Models
{
    public class Facility
    {
        public int FacilityId { get; set; }
        public int PassengerId { get; set; }
        public string FacilityName { get; set; }
        public decimal Price { get; set; }
        public virtual Passenger Passenger { get; set; }
    }
}