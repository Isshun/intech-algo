using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo.Optim
{
    public class Guest
    {
        public int Index { get; set; }

        public string Name { get; set; }
        
        public Airport Location { get; set; }

        internal void BuildFlights( Meeting m, FlightDatabase db )
        {
            // Arrival
            {
                var fDay = db.GetFlights( m.MaxBusTimeOnArrival.Date, Location, m.Location );
                var fDayBefore = db.GetFlights( m.MaxBusTimeOnArrival.Date.AddDays( -1 ), Location, m.Location );
                var all = fDayBefore.Concat( fDay ).Where( f => f.ArrivalTime < m.MaxBusTimeOnArrival );
                all = all.OrderByDescending( f => f.ArrivalTime ).Take( 50 );
                ArrivalFlights = all.ToList();
            }

            // Departure
            {
                var fDay = db.GetFlights( m.MinBusTimeOnDeparture.Date, m.Location, Location );
                var fDayBefore = db.GetFlights( m.MinBusTimeOnDeparture.Date.AddDays( 1 ), m.Location, Location );
                var all = fDayBefore.Concat( fDay ).Where( f => f.DepartureTime > m.MinBusTimeOnDeparture );
                all = all.OrderByDescending( f => f.DepartureTime ).Take( 50 );
                DepartureFlights = all.ToList();
            }
        }

        public List<SimpleFlight> ArrivalFlights { get; set; }

        public List<SimpleFlight> DepartureFlights { get; set; }
    }

    public class Meeting
    {
        public Meeting()
        {
            FlightDatabase db = new FlightDatabase( @"C:\Users\Alex\Documents\ALGO SPI\Code\ThirdParty\FlightData\" );

            Location = Airport.FindByCode( "LHR" );
            MaxBusTimeOnArrival = new DateTime( 2010, 7, 27, 17, 0, 0 );
            //MinBusTimeOnDeparture = new DateTime( 2010, 7, 27, 17, 0, 0 );
            MinBusTimeOnDeparture = new DateTime( 2010, 8, 3, 15, 0, 0 );

            Guests = new List<Guest>();
            Guests.Add( new Guest() { Name = "User 1", Location = Airport.FindByCode( "BER" ) } );
            Guests.Add( new Guest() { Name = "User 2", Location = Airport.FindByCode( "CDG" ) } );
            Guests.Add( new Guest() { Name = "User 3", Location = Airport.FindByCode( "MRS" ) } );
            Guests.Add( new Guest() { Name = "User 4", Location = Airport.FindByCode( "LYS" ) } );
            Guests.Add( new Guest() { Name = "User 5", Location = Airport.FindByCode( "MAN" ) } );
            Guests.Add( new Guest() { Name = "User 6", Location = Airport.FindByCode( "BIO" ) } );
            Guests.Add( new Guest() { Name = "User 7", Location = Airport.FindByCode( "JFK" ) } );
            Guests.Add( new Guest() { Name = "User 8", Location = Airport.FindByCode( "TUN" ) } );
            Guests.Add( new Guest() { Name = "User 9", Location = Airport.FindByCode( "MXP" ) } );

            int iGuest = 0;
            foreach( var g in Guests )
            {
                g.Index = iGuest++;
                g.BuildFlights(this, db);
            }

            //var f0 = db.GetFlights( new DateTime( 2010, 7, 26 ), Airport.FindByCode( "BER" ), Airport.FindByCode( "LHR" ) );

        }

        public Airport Location { get; set; }

        public List<Guest> Guests { get; set; }


        public DateTime MaxBusTimeOnArrival { get; set; }

        public DateTime MinBusTimeOnDeparture { get; set; }
    }
}
