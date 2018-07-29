using Common.General;
using Common.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation
{
	internal class RangeValue
	{
		public RangeValue(LinkedState ls, double start, double end)
		{
			this.LinkedState = ls;
			this.Start = start;
			this.End = end;
		}

		public LinkedState LinkedState { get; private set; }
		public double Start;
		public double End;
	}

	/// <summary>
	/// Classe que representa a máquina de estados
	/// </summary>
	public class StateMachine
	{
		public StateMachine(List<IState> states, int maxInteraction = 30, Log log = null)
		{
			this._States = states;
			this._MaxInteraction = maxInteraction != 0 ? maxInteraction : 30;
			this._Log = log;

			_DicStates = this._States.ToDictionary(s => s.Guid);
		}

		public List<IState> States { get { return _States; } }

		private List<IState> _States;
		private int _MaxInteraction;
		private IState _CurrentState;
		private Log _Log;
		private Dictionary<Guid, IState> _DicStates;

		public List<IState> Run()
		{
			WriteLog("Início da execução da Máquina de estado");

			_CalculateNextStateProbability();

			if (_CurrentState == null)
				_CurrentState = _States[0];

			List<IState> statesSequence = new List<IState>();
			IState state;
			List<RangeValue> rangeValues;
			double randomValue;
			Random randomNumber = new Random();

			for (int i = 0; i < _MaxInteraction; i++)
			{
				WriteLog(string.Format("Calculando o Range de valor do estado: {0}", _CurrentState.Guid));
				rangeValues = _CreateRangeValues(_CurrentState.LinkedStates);

				randomValue = randomNumber.Next(1, 100);
				WriteLog(string.Format("Valor aleatório: {0}", randomValue));

				WriteLog("Encontrando o próximo estado");
				state = _GetNextState(randomValue, rangeValues);
				_CurrentState = state == null ? _CurrentState : state;
				WriteLog(string.Format("Próximo estado: {0}", _CurrentState.Guid));

				statesSequence.Add(_CurrentState);
			}

			WriteLog("Fim da execução da Máquina de estado");

			return statesSequence;
		}

		private List<RangeValue> _CreateRangeValues(List<LinkedState> list)
		{
			double aggregate = 0;
			List<RangeValue> rangeValues = new List<RangeValue>();
			list.ForEach(
				ls =>
				{
					double start = aggregate + 1;
					aggregate = start + (ls.OutProbability * 100) - 1;
					RangeValue range = new RangeValue(ls, start, aggregate);
					WriteLog(string.Format("\t\tStart: {0}\tEnd: {1}", range.Start, range.End));
					rangeValues.Add(range);
				});

			return rangeValues;
		}

		private IState _GetNextState(double randomValue, List<RangeValue> rangeValues)
		{
			Guid owner = Guid.Empty;

			rangeValues.ForEach(rv =>
			{
				if (rv.Start <= randomValue && rv.End >= randomValue)
					owner = rv.LinkedState.NextState.Guid;
			});

			return _DicStates.ContainsKey(owner) ? _DicStates[owner] : null;
		}

		private void _CalculateNextStateProbability()
		{
			if (_States != null && _States.Count > 0)
			{
				WriteLog("\tInício do calculo de probabilidade");
				_States.ForEach(s => s.CalculateLinkedStatesProbability());
				WriteLog("\tFim do cálculo de probabilidade");
			}
		}

		private void WriteLog(string message, int level = 0, bool sameLine = false)
		{
			if (_Log != null)
				_Log.WriteLog(message, level, sameLine);
		}
	}
}
