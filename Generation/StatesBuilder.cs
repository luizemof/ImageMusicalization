using Common;
using Common.Extraction;
using Common.General;
using Common.Generation;
using Extraction.KMeans;
using Extraction.SURF;
using Generation.KMeans;
using Generation.SURF;
using System;
using System.Collections.Generic;

namespace Generation
{
	public class StatesBuilder : IBuilder<List<IState>>
	{
		private readonly List<IResult> Results;
		private readonly EModelType ModelType;
		private readonly Log Log;

		public StatesBuilder(List<IResult> results, EModelType type, Log log = null)
		{
			if (results.IsNullOrEmpty())
				throw new ArgumentException(nameof(results));

			if (type == EModelType.Unknow)
				throw new ArgumentException(nameof(type));

			Results = results;
			ModelType = type;
			Log = log;
		}

		public List<IState> Build()
		{
			List<IState> states = new List<IState>();
			Results.ForEach(r =>
			{
				IState newState = null;

				switch (ModelType)
				{
					case EModelType.KMeans:
						newState = new KMeansState(r as KMeansResult, Log);
						break;
					case EModelType.SURF:
						newState = new SURFState(r as SURFResult, Log);
						break;
					default:
						break;
				}

				if (newState != null)
					states.Add(newState);
			});


			states?.ForEach(s =>
			{
				if (s.ModelType == EModelType.KMeans)
					((KMeansState)s).LinkedStates = CreateKMeansLinkedStates(s, states.RemoveState(s));
				else if (s.ModelType == EModelType.SURF)
					((SURFState)s).LinkedStates = CreateKMeansLinkedStates(s, states.RemoveState(s));
			});

			return states;
		}

		private List<LinkedState> CreateKMeansLinkedStates(IState s, List<IState> states)
		{
			List<LinkedState> returnValue = new List<LinkedState>();

			states.ForEach(ks => returnValue.Add(new LinkedState(ks, s.Guid)));

			return returnValue;
		}
	}
}
