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
		private Log _Log;

		public State(T element, Log log)
		{
			this._Log = log;
			this.Element = element;
			this.Guid = Guid.NewGuid();
		}

		public T Element { get; private set; }
		public Guid Guid { get; private set; }

		protected void WriteLog(string message, int level = 0, bool sameLine = false)
		{
			if (_Log != null)
				_Log.WriteLog(message, level, sameLine);
		}
	}
}
