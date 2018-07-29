using Common;
using Common.Extraction;
using Common.General;
using Common.Generation;
using Generation.KMeans;
using Generation.SURF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation
{
	public class StateMachineExecutor
	{
		public class Parameters
		{
			public string LogPath { get; set; }
			public List<IResult> Results { get; set; }
			public string StateLog { get; set; }
			public string SequenceLog { get; set; }
			public string ProbabilityCalculation { get; set; }
			public EModelType TargetType { get; set; }
			public int MaxNotes { get; set; }
		}

		public event EventHandler<LogArgs> NotificationRequested;

		private Parameters _Parameters;

		public StateMachineExecutor(Parameters parameters)
		{
			Validate(parameters);
			_Parameters = parameters;
		}

		private void Validate(Parameters parameters)
		{
			parameters.ThrowIfNull(nameof(parameters));
			parameters.Results.ThrowIfNull(nameof(parameters.Results));

			if (parameters.Results.Any(r => r.ModelType != parameters.TargetType))
				throw new InvalidOperationException();
		}

		public List<IState> Execute()
		{
			Log SMlog = null;
			Log SSlog = null;
			Log PClog = null;

			if (!string.IsNullOrWhiteSpace(_Parameters.LogPath))
			{
				SMlog = new Log(_Parameters.LogPath, _Parameters.StateLog, ELogType.GenerationStates);
				SSlog = new Log(_Parameters.LogPath, _Parameters.SequenceLog, ELogType.Sequence);
				PClog = new Log(_Parameters.LogPath, _Parameters.ProbabilityCalculation, ELogType.ProbabilityCalculation);

				SSlog.Notify += Log_Notify;
				SMlog.Notify += Log_Notify;
				PClog.Notify += Log_Notify;
			}

			StatesBuilder statesBuilder = new StatesBuilder(_Parameters.Results, _Parameters.TargetType, PClog);
			StateMachine stMachine = new StateMachine(statesBuilder.Build(), _Parameters.MaxNotes, SMlog);
			List<IState> stateSequence = stMachine.Run();

			if (SSlog != null)
				stateSequence.ForEach(ss =>
				{
					string message = string.Empty;
					if (ss.ModelType == EModelType.KMeans)
						message = string.Format("Sequence of notes:{0}", ((KMeansState)ss).Element.Note);
					else if (ss.ModelType == EModelType.SURF)
						message = string.Format("Sequence of notes:{0}", ((SURFState)ss).Element.Note);

					SSlog.WriteLog(message);
				});

			return stateSequence;
		}

		private void Log_Notify(object sender, LogArgs e)
		{
			if (NotificationRequested != null)
				NotificationRequested.Invoke(sender, e);
		}
	}
}
