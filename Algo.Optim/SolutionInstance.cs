using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo.Optim {
    public abstract class SolutionInstance
    {

        readonly SolutionSpace _space;
        public double _cost = -1;
        int[] _coords;

        public SolutionInstance(SolutionSpace s) {
            _space = s;
            _coords = new int[s.NbDomain];
            for( int i = 0; i < _coords.Length; i++ )
            {
                _coords[i] = s.Random.Next(s.DomainSize[i]);
            }
        }

        public double Cost {
            get {
                if( _cost == -1 )
                {
                    _cost = ComputeCost();
                    if( _space.BestEverSeen == null || _space.BestEverSeen.Cost > _cost)
                        _space.BestEverSeen = this;

                }
                return _cost;
            }
        }

        public SolutionSpace Space { get { return _space; } }

        public int[] Coords { get { return _coords; } }

        protected abstract double ComputeCost();
    }
}
