namespace PassengerMVC_SPA.Migrations
{
    using PassengerMVC_SPA.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PassengerMVC_SPA.DAL.AirDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PassengerMVC_SPA.DAL.AirDbContext context)
        {
            var seatTypes = new List<SeatType>
            {
                new SeatType {SeatTypeName="First-Class"},
                new SeatType {SeatTypeName="Economy"},
                new SeatType {SeatTypeName="Business-Class"}
            };
            seatTypes.ForEach(s => context.SeatTypes.AddOrUpdate(p => p.SeatTypeName, s));
            context.SaveChanges();
        }
    }
}
