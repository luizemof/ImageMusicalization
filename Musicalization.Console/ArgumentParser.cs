using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalization.Console
{
    public class ArgumentParser
    {
        public const char SEP = '=';
        public const string IMAGEM = "imagem";
        public const string CENTERS = "centros";
        public const string LOG_PATH = "logpath";
        public const string KMEANS_LOG = "kmeanslog";
        public const string KMEANS_STATE_LOG = "kmeansstatelog";
        public const string SEQUENCE_LOG = "sequencelog";
        public const string PARALLEL = "paralelo";
        public const string MAX_NOTES = "maxnotas";
        public const string TARGET = "target";
        public const string SURF_LOG = "surflog";
        public const string MODEL_LOG = "modellog";
        public const string SURF_STATE_LOG = "surfstatelog";
        public const string STATE_LOG = "statelog";

        public static MusicalizationArgs Parse(string[] args)
        {
            string[] splitArgs;
            MusicalizationArgs argParser = new MusicalizationArgs();

            foreach (string arg in args)
            {
                splitArgs = arg.Split(SEP);

                switch (splitArgs[0].ToLowerInvariant())
                {
                    case ArgumentParser.CENTERS:
                        argParser.Centers = ConvertStringToCenter(splitArgs[1]);
                        break;
                    case ArgumentParser.IMAGEM:
                        argParser.ImageFile = splitArgs[1];
                        break;
                    case ArgumentParser.MODEL_LOG:
                    case ArgumentParser.SURF_LOG:
                    case ArgumentParser.KMEANS_LOG:
                        argParser.ModelLog = splitArgs[1];
                        break;
                    case ArgumentParser.STATE_LOG:
                    case ArgumentParser.SURF_STATE_LOG:
                    case ArgumentParser.KMEANS_STATE_LOG:
                        argParser.StateLog = splitArgs[1];
                        break;
                    case ArgumentParser.LOG_PATH:
                        argParser.LogPath = splitArgs[1];
                        break;
                    case ArgumentParser.SEQUENCE_LOG:
                        argParser.SequenceLog = splitArgs[1];
                        break;
                    case ArgumentParser.PARALLEL:
                        argParser.Parallel = bool.Parse(splitArgs[1]);
                        break;
                    case ArgumentParser.MAX_NOTES:
                        int maxNotes = 30;
                        argParser.MaxNotes = maxNotes;
                        if (int.TryParse(splitArgs[1], out maxNotes))
                            argParser.MaxNotes = maxNotes;
                        break;
                    case ArgumentParser.TARGET:
                        argParser.TargetType = _ConvertToTargetType(splitArgs[1]);
                        break;
                    default:
                        break;
                }
            }

            return argParser;
        }

        public static List<Point> ConvertStringToCenter(string p)
        {
            List<Point> value = new List<Point>();
            string[] firstSplit = p.Split(' ');
            string[] secondSplit;
            foreach (string item in firstSplit)
            {
                secondSplit = item.Split(';');

                if (secondSplit.Length != 2)
                    throw new Exception("Não foi possível encontrar um centro, valores incorretos. Valor esperado CoordX;CoordY");

                value.Add(new Point(int.Parse(secondSplit[0]), int.Parse(secondSplit[1])));
            }
            return value;
        }

        private static EModelType _ConvertToTargetType(string typeDescription)
        {
            EModelType type = EModelType.Unknow;

            foreach (EModelType t in Enum.GetValues(typeof(EModelType)))
            {
                if (t != EModelType.Unknow && t.GetDescription().ToLower().Equals(typeDescription.ToLower()))
                {
                    type = t;
                    break;
                }
            }

            return type;
        }
    }
}
