using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo.Optim {
    public abstract class SolutionSpace {
        SolutionInstance _best;
        int _nbDomain;

        public SolutionSpace( int nbDomain )
        {
            _nbDomain = nbDomain;
            DomainSize = new int[nbDomain];
        }

        public Random Random { get; private set; }

        public SolutionInstance BestEverSeen { get { return _best; } internal set { _best = value; } }

        public int NbDomain { get { return _nbDomain; } }

        public int[] DomainSize { get; private set; }

        public double Cardinality {
            get
            {
                double d = 1;
                foreach( var size in DomainSize )
                {
                    d *= size;
                }
                return d;
            }
        }

        protected abstract SolutionInstance CreateInstance(SolutionSpace s);

        public void SearchRandom(int nbTry)
        {
            var s = CreateInstance( this );
            double c = s.Cost;
        }
    }
}
