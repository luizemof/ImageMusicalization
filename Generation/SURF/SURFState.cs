using Common;
using Common.General;
using Common.Generation;
using Extraction.SURF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.SURF
{
	/// <summary>
	/// Classe um estado da máquina de estados do Modelo SURF
	/// </summary>
	public class SURFState : State<SURFResult>, IState
	{
		public SURFState(SURFResult element, Log log)
			: this(element, new List<LinkedState>(), log)
		{ }
		public SURFState(SURFResult element, List<LinkedState> ls, Log log)
			: base(element, log)
		{
			_Total = 0;
			_LinkedStates = ls;
		}

		private double _Total;
		private List<LinkedState> _LinkedStates;

		public List<LinkedState> LinkedStates { get { return _LinkedStates; } set { _LinkedStates = value; } }
		public EModelType ModelType { get { return EModelType.SURF; } }
		public bool IsDeadState { get { return _LinkedStates != null && _LinkedStates.Count > 0; } }

		public void CalculateLinkedStatesProbability()
		{
			if (this.LinkedStates != null && this.LinkedStates.Count > 0)
			{
				string message = string.Format("Estado: {0}", this.Element.Note.GetDescription());
				WriteLog(message);
				_Total = _GetTotalOfSetValue();
				this.LinkedStates.ForEach(ls => ls.OutProbability = _CalculateProbability(ls.NextState as SURFState));
			}
		}

		private double _GetTotalOfSetValue()
		{
			double value = this.Element.NumberOfElements;

			if (this.LinkedStates != null)
				foreach (var item in LinkedStates)
				{
					value += ((SURFState)item.NextState).Element.NumberOfElements;
					value += Utils.CalculateDistance(((SURFState)item.NextState).Element.Pixel, this.Element.Pixel);
				}
			return value;
		}

		private double _CalculateProbability(SURFState linkedState)
		{
			int numberOfElements = linkedState.Element.NumberOfElements;
			double distance = Utils.CalculateDistance(linkedState.Element.Pixel, this.Element.Pixel);
			double value = ((distance + (numberOfElements)) / _Total);
			double roundValue = Math.Round(value, 2);
			WriteLog(string.Format("Próximo estado: {0}", linkedState.Element.Note), 1);
			WriteLog(string.Format("Número de elementos: {0}", numberOfElements), 2);
			WriteLog(string.Format("distance: {0}", distance), 2);
			WriteLog(string.Format("Probabilidade: {0}", roundValue), 2);
			return roundValue;
		}
	}
}
