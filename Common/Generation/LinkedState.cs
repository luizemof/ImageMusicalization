using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Generation
{
    public class LinkedState
    {
        public LinkedState(IState nextState, Guid owner)
            : this(nextState, default(double), owner)
        { }

        public LinkedState(IState nextState, double probability, Guid owner)
        {
            this._NextState = nextState;
            this._OutProbability = probability;
            this.Owner = owner;
        }

        private IState _NextState;
        private double _OutProbability;

        public IState NextState { get { return _NextState; } }
        public double OutProbability { get { return _OutProbability; } set { _OutProbability = value; } }
        public Guid Owner { get; private set; }
    }
}
