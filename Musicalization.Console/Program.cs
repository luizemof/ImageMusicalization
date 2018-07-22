using Common;
using Common.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalization.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                double domi = Utils.CalculateDistance(Color.FromArgb(0, 0, 0), Color.FromArgb(0, 255, 0));
                double dosol = Utils.CalculateDistance(Color.FromArgb(0, 0, 0), Color.FromArgb(0, 255, 255));

                double mido = Utils.CalculateDistance(Color.FromArgb(0, 255, 0), Color.FromArgb(0, 0, 0));
                double misol = Utils.CalculateDistance(Color.FromArgb(0, 255, 0), Color.FromArgb(0, 255, 255));

                double soldo = Utils.CalculateDistance(Color.FromArgb(0, 255, 255), Color.FromArgb(0, 0, 0));
                double solmi = Utils.CalculateDistance(Color.FromArgb(0, 255, 255), Color.FromArgb(0, 255, 0));

                MusicalizationArgs parser;
                if (args != null && args.Length > 0)
                    parser = ArgumentParser.Parse(args);
                else
                    parser = _ReadArguments();

                DateTime start = DateTime.Now;
                System.Console.Clear();

                General.Instance.Execute(parser);

                System.Console.WriteLine("Início da execução: " + start.ToLongTimeString());
                System.Console.WriteLine("Fim da execução: " + DateTime.Now.ToLongTimeString());

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine(Environment.NewLine + e.StackTrace);
                System.Console.ReadKey();
            }
        }

        private static MusicalizationArgs _ReadArguments()
        {
            MusicalizationArgs args = new MusicalizationArgs();

            EModelType modelType = _ChooseModel();
            System.Console.Clear();

            switch (modelType)
            {
                case EModelType.KMeans:
                    args = _ReadKMeansArguments();
                    break;
                case EModelType.SURF:
                    args = _ReadSURFArguments();
                    break;
                default:
                    break;
            }

            args.TargetType = modelType;

            return args;
        }

        private static EModelType _ChooseModel()
        {
            ConsoleKeyInfo op;
            EModelType type = EModelType.Unknow;

            System.Console.WriteLine("Escolha o tipo de extração");
            System.Console.WriteLine("1- KMeans");
            System.Console.WriteLine("2- SURF");
            op = System.Console.ReadKey();

            switch (op.KeyChar)
            {
                case '1':
                    type = EModelType.KMeans;
                    break;
                case '2':
                    type = EModelType.SURF;
                    break;
                default:
                    break;
            }

            return type;
        }

        private static MusicalizationArgs _ReadGeneralArguments()
        {
            MusicalizationArgs args = new MusicalizationArgs();
            
            System.Console.Write("Imagem: ");
            args.ImageFile = System.Console.ReadLine();

            System.Console.Write("Diretório do Log (Opcional): ");
            args.LogPath = System.Console.ReadLine();

            System.Console.Write("Número de notas (Opcional): ");
            int maxNotes;

            if (int.TryParse(System.Console.ReadLine(), out maxNotes))
                args.MaxNotes = maxNotes;

            return args;
        }

        private static MusicalizationArgs _ReadKMeansArguments()
        {
            MusicalizationArgs args = _ReadGeneralArguments();

            System.Console.Write("Quantidade de centros: ");
            int qtCenters;
            while (!int.TryParse(System.Console.ReadLine(), out qtCenters))
                System.Console.Write("Quantidade de centros: ");

            string x;
            string y;
            Point coord;
            args.Centers = new List<Point>();
            for (int i = 0; i < qtCenters; i++)
            {
                System.Console.WriteLine("Centro " + i);
                x = string.Empty;
                y = string.Empty;
                while (!_PointTryParser(x, y, out coord))
                {
                    System.Console.Write("Coordenada X: ");
                    x = System.Console.ReadLine();

                    System.Console.Write("Coordenada Y: ");
                    y = System.Console.ReadLine();
                }
                args.Centers.Add(coord);
            }

            System.Console.Write("KMeansLog (Opcional): ");
            args.ModelLog = System.Console.ReadLine();

            System.Console.Write("KmeansStateLog (Opcional): ");
            args.StateLog = System.Console.ReadLine();

            System.Console.Write("SequenceLog (Opcional): ");
            args.SequenceLog = System.Console.ReadLine();

            string par = "a";
            bool parallel = true;
            while (par != string.Empty && !bool.TryParse(par, out parallel))
            {
                System.Console.Write("Paralelo (Opcional): ");
                par = System.Console.ReadLine();
                parallel = par == string.Empty;
            }
            args.Parallel = parallel;

            return args;
        }

        private static MusicalizationArgs _ReadSURFArguments()
        {
            MusicalizationArgs args = _ReadGeneralArguments();

            System.Console.Write("SURFLog (Opcional): ");
            args.ModelLog = System.Console.ReadLine();

            return args;
        }

        private static bool _PointTryParser(string x, string y, out Point outValue)
        {
            try
            {
                outValue = new Point(int.Parse(x), int.Parse(y));
                return true;
            }
            catch (Exception)
            {
                outValue = Common.General.Utils.EmptyPoint;
                return false;
            }
        }

    }
}
