using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo.Optim {
    public class SolutionSpaceMeeting : SolutionSpace {

        public SolutionSpaceMeeting(Meeting m)
            : base(m.Guests.Count * 2)
        {
            Meeting = m;
            int iDomain = 0;
            foreach( Guest g in m.Guests )
            {
                DomainSize[iDomain] = g.ArrivalFlights.Count;
                iDomain++;
                DomainSize[iDomain] = g.DepartureFlights.Count;
            }
        }

        public Meeting Meeting { get; private set; }

        protected override SolutionInstance CreateInstance( SolutionSpace s )
        {
            return new SolutionInstanceMeeting(this);
        }
    }
}
