using Common;
using Common.General;
using Common.Generation;
using Extraction.KMeans;
using System;
using System.Collections.Generic;

namespace Generation.KMeans
{
	/// <summary>
	/// Classe que representa um estado da máquina de estados do Modelo KMeans
	/// </summary>
	public class KMeansState : State<KMeansResult>, IState
	{
		public KMeansState(KMeansResult element, Log log)
			: this(element, new List<LinkedState>(), log)
		{ }
		public KMeansState(KMeansResult element, List<LinkedState> linkedStates, Log log)
			: base(element, log)
		{
			this._Total = 0;
			this._LinkedStates = linkedStates;
		}

		private double _Total;
		private List<LinkedState> _LinkedStates;

		public List<LinkedState> LinkedStates { get { return _LinkedStates; } set { _LinkedStates = value; } }
		public EModelType ModelType { get { return EModelType.KMeans; } }
		public bool IsDeadState { get { return _LinkedStates != null && _LinkedStates.Count > 0; } }

		public void CalculateLinkedStatesProbability()
		{
			if (this._LinkedStates != null && this._LinkedStates.Count > 0)
			{
				string message = string.Format("Estado: {0}", this.Element.Note.GetDescription());
				WriteLog(message);
				_Total = _GetTotalOfSetValue();
				this._LinkedStates.ForEach(ls => ls.OutProbability = _CalculateProbability(ls.NextState as KMeansState));
			}
		}

		private double _GetTotalOfSetValue()
		{
			double value = this.Element.NumberOfElements;

			if (this._LinkedStates != null)
				foreach (var item in _LinkedStates)
				{
					value += ((KMeansState)item.NextState).Element.NumberOfElements;
					value += Utils.CalculateDistance(((KMeansState)item.NextState).Element.Pixel, this.Element.Pixel);
				}
			return value;
		}

		private double _CalculateProbability(KMeansState linkedState)
		{
			double distance = Utils.CalculateDistance(linkedState.Element.Pixel, this.Element.Pixel);
			double value = ((distance + (linkedState.Element.NumberOfElements)) / _Total);
			return Math.Round(value, 2);
		}
	}
}