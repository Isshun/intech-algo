using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo.Optim {
    public class SolutionInstanceMeeting : SolutionInstance
    {
        public SolutionInstanceMeeting( SolutionSpaceMeeting space )
            : base(space)
        {

        }

        public new SolutionSpaceMeeting Space
        {
            get { return (SolutionSpaceMeeting)base.Space; }
        }

        RoundTrip GetFlights( Guest g )
        {
            int iArrival = Coords[g.Index * 2];
            int iDeparture = Coords[g.Index * 2 + 1];
            SimpleFlight arrival = g.ArrivalFlights[iArrival];
            SimpleFlight departure = g.DepartureFlights[iDeparture];
            return new RoundTrip() { Arrival = arrival, Departure = departure };
        }

        protected override double ComputeCost()
        {
            var meeting = Space.Meeting;
            double cost = 0.0;

            var busTimeOnArrival = DateTime.MinValue;
            foreach( var g in meeting.Guests )
            {
                RoundTrip flights = GetFlights( g );
                cost += flights.Arrival.Price;
                cost += flights.Departure.Price;
                cost += flights.Arrival.Stops * 500;
                cost += flights.Departure.Stops * 10;

                if (flights.Arrival.ArrivalTime > busTimeOnArrival)
                {
                    busTimeOnArrival = flights.Arrival.ArrivalTime;
                }
            }

            foreach( var g in meeting.Guests )
            {
                RoundTrip flights = GetFlights( g );

                TimeSpan waitingArrival = busTimeOnArrival - flights.Arrival.ArrivalTime;
                TimeSpan waitingDeparture = flights.Departure.DepartureTime - meeting.MinBusTimeOnDeparture;
                TimeSpan TotalWaitingForGuest = waitingArrival + waitingDeparture;
                cost += TotalWaitingForGuest.TotalMinutes * 1.5;
            }

            return cost;
        }

        public SimpleFlight Departure { get; set; }
    }
}
