using Common;
using Common.Extraction;
using Common.General;
using Common.Generation;
using Extraction;
using Extraction.KMeans;
using Extraction.SURF;
using Generation;
using Generation.KMeans;
using Generation.SURF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalization
{
	/// <summary>
	/// Classe para execução em geral
	/// </summary>
	public class General
	{
		private static General _Instance;

		private General() { }

		public static General Instance
		{
			get
			{
				if (_Instance == null)
					_Instance = new General();

				return _Instance;
			}
		}

		public event EventHandler<LogArgs> NotificationRequested;

		public void Execute(MusicalizationArgs parser)
		{
			parser.Validade();
			Model model = ExecuteModel(parser);
			List<IState> states = ExecuteStateMachine(parser, model);
			Musicalization.GenerateAndPlay(states);
		}

		private List<IState> ExecuteStateMachine(MusicalizationArgs parser, Model model)
		{
			StateMachineExecutor.Parameters parameters = new StateMachineExecutor.Parameters()
			{
				LogPath = parser.LogPath,
				MaxNotes = parser.MaxNotes,
				ProbabilityCalculation = parser.ProbabilityCalculation,
				Results = model.Result,
				SequenceLog = parser.SequenceLog,
				StateLog = parser.StateLog,
				TargetType = parser.TargetType
			};
			StateMachineExecutor stateMachineExecutor = new StateMachineExecutor(parameters);
			stateMachineExecutor.NotificationRequested += LogNotifier;
			return stateMachineExecutor.Execute();
		}

		private Model ExecuteModel(MusicalizationArgs parser)
		{
			Model model = null;
			Log log = log = new Log(parser.LogPath, parser.ModelLog, ELogType.Extraction);
			log.Notify += LogNotifier;

			if (parser.TargetType == EModelType.KMeans)
				model = new KMeans(parser.ImageFile, parser.Centers, log, parser.Parallel);
			else if (parser.TargetType == EModelType.SURF)
				model = new SURF(parser.ImageFile, log, parser.AnalizeImage);

			model.Execute();
			return model;
		}

		private void LogNotifier(object sender, LogArgs e)
		{
			NotificationRequested?.Invoke(sender, e);
		}
	}
}
