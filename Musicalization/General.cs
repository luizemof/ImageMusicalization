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

            Model model = null;
            Log log = log = new Log(parser.LogPath, parser.ModelLog, ELogType.Extraction);
            log.Notify += log_Notify;

            if (parser.TargetType == EModelType.KMeans)
                model = new KMeans(parser.ImageFile, parser.Centers, log, parser.Parallel);
            else if (parser.TargetType == EModelType.SURF)
                model = new SURF(parser.ImageFile, log, parser.AnalizeImage);

            model.Execute();

            _ExecuteStateMachine(model.Result, parser);
        }

        private void _ExecuteStateMachine(List<IResult> results, MusicalizationArgs parser)
        {
            Log SMlog = new Log(parser.LogPath, parser.StateLog, ELogType.GenerationStates);
            Log SSlog = new Log(parser.LogPath, parser.SequenceLog, ELogType.Sequence);
            Log PClog = new Log(parser.LogPath, parser.ProbabilityCalculation, ELogType.ProbabilityCalculation);
            
            SSlog.Notify += log_Notify;
            SMlog.Notify += log_Notify;
            PClog.Notify += log_Notify;

            StateMachine stMachine = new StateMachine(_CreateStates(results, parser.TargetType, PClog), parser.MaxNotes, SMlog);
            List<IState> stateSequence = stMachine.Run();

            stateSequence.ForEach(ss =>
            {
                string message = string.Empty;

                if (ss.ModelType == EModelType.KMeans)
                    message = string.Format("Sequence of notes:{0}", ((KMeansState)ss).Element.Note);
                else if (ss.ModelType == EModelType.SURF)
                    message = string.Format("Sequence of notes:{0}", ((SURFState)ss).Element.Note);

                SSlog.WriteLog(message);
            });

            Musicalization.GenerateAndPlay(stateSequence);
        }

        private List<IState> _CreateStates(List<IResult> results, EModelType type, Log PCLog)
        {
            List<IState> states = new List<IState>();
            results.ForEach(r =>
            {
                IState newState = null;

                if (type == EModelType.KMeans)
                    newState = new KMeansState(r as KMeansResult, PCLog);
                else if (type == EModelType.SURF)
                    newState = new SURFState(r as SURFResult, PCLog);

                if (newState != null)
                    states.Add(newState);
            });


            states.ForEach(s =>
            {
                if (s.ModelType == EModelType.KMeans)
                    ((KMeansState)s).LinkedStates = _CreateKMeansLinkedStates(s, states.RemoveState(s));
                else if (s.ModelType == EModelType.SURF)
                    ((SURFState)s).LinkedStates = _CreateKMeansLinkedStates(s, states.RemoveState(s));
            });

            return states;
        }

        private List<LinkedState> _CreateKMeansLinkedStates(IState s, List<IState> states)
        {
            List<LinkedState> returnValue = new List<LinkedState>();

            states.ForEach(ks => returnValue.Add(new LinkedState(ks, s.Guid)));

            return returnValue;
        }

        private void log_Notify(object sender, LogArgs e)
        {
            if (NotificationRequested != null)
                NotificationRequested.Invoke(sender, e);
        }
    }
}
