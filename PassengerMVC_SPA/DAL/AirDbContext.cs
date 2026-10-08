using PassengerMVC_SPA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PassengerMVC_SPA.DAL
{
    public class AirDbContext: DbContext
    {
        public AirDbContext() : base("AirDbContext") { }
        
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<SeatType> SeatTypes { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
    }
}