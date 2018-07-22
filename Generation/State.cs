using Common;
using Common.Extraction;
using Common.General;
using Common.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation
{
    /// <summary>
    /// Classe que representa um estado da máquina de estados
    /// </summary>
    /// <typeparam name="T">Resultado da extração de características</typeparam>
    public abstract class State<T> where T : IResult
    {
        public State(T element, Log log)
        {
            this._Log = log;
            this._Element = element;
            this.Guid = Guid.NewGuid();
        }

        private T _Element;
        protected Log _Log;

        public T Element { get { return _Element; } set { _Element = value; } }
        public Guid Guid { get; private set; }
    }
}
