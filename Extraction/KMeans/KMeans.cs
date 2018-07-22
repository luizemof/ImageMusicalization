using Common;
using Common.Extraction;
using Common.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraction.KMeans
{
    /// <summary>
    /// Modelo de extração utilizando KMeans
    /// </summary>
    public class KMeans : Model
    {
        public KMeans(string myImageFile, List<Point> initalCenters, Log log = null, bool parallel = false)
            :base(myImageFile, log, parallel)
        {
            this._K = initalCenters.Count;
            this._Result = InitialCenters = initalCenters;
            this._AllResults = new List<List<Point>>() { _Result };
        }

        private int _K;
        private List<Point> _Result;
        private bool _CentersFounded = false;
        private Dictionary<Point, List<Point>> _Groups = new Dictionary<Point, List<Point>>();
        private Dictionary<Point, double> _Distances = new Dictionary<Point, double>();
        private List<List<Point>> _AllResults;
        
        public override EModelType Type { get { return EModelType.KMeans; } }
        public override List<IResult> Result
        {
            get
            {
                List<IResult> kmeansResults = new List<IResult>();
                KMeansResult kResult;
                _Result.ForEach(center =>
                {
                    kResult = new KMeansResult()
                    {
                        Coordinator = center,
                        NumberOfElements = _Groups[center].Count,
                        Pixel = _MyImage.GetPixel(center.X, center.Y)
                    };
                    kmeansResults.Add(kResult);
                });

                return kmeansResults;
            }
        }
        /// <summary>
        /// Quantidade de grupos
        /// </summary>
        public int K { get { return _K; } set { _K = value; } }
        /// <summary>
        /// Centros iniciais
        /// </summary>
        public List<Point> InitialCenters { get; private set; }
        

        /// <summary>
        /// Executa o kmeans para achar os centros da imagem
        /// </summary>
        public override void Execute()
        {
            try
            {
                if (_K < 0 || _MyImage == null)
                    throw new Exception("Parâmetros inválidos. Verifique os valores da semente e da imagem.");

                int interations = 1;

                if (InitialCenters.Count < 1)
                    _ChooseInitialCenters();

                while (!_CentersFounded)
                {
                    _Log.WriteLog(string.Format("Interação número: {0}", interations++));
                    _SeparateInGroups();
                    if (_ExecuteParallel)
                        _FindNewCentersParallel();
                    else
                        _FindNewCenters();
                }

                _Log.WriteLog("Centro de cada grupo encontrado");
                for (int i = 0; i < _Result.Count; i++)
                    _Log.WriteLog(string.Format("\tCentro {0}: X={1}\tY={2}", i, _Result[i].X, _Result[i].Y));

            }
            catch (Exception e)
            {
                _Log.WriteLog(e.StackTrace);
            }

            // Separa novamente em grupos.
            _SeparateInGroups(false);

            _LogResults();
        }

        private void _LogResults()
        {
            _Log.WriteLog(Environment.NewLine+Environment.NewLine);
            this.Result.ForEach(r =>
                {
                    KMeansResult kmr = r as KMeansResult;
                    _Log.WriteLog("--------------------------------------------");
                    _Log.WriteLog(string.Format("Coordenada X:{0}", kmr.Coordinator.X));
                    _Log.WriteLog(string.Format("Coordenada Y:{0}", kmr.Coordinator.Y));
                    _Log.WriteLog(string.Format("Número de elementos: {0}", kmr.NumberOfElements));
                    _Log.WriteLog(string.Format("R: {0}\tG: {1}\tB: {2}", r.Pixel.R, r.Pixel.G, r.Pixel.B));
                });

        }

        private void _FindNewCentersParallel()
        {
            _Result = new List<Point>();
            
            _Log.WriteLog("\tEncontrando os novos centros em paralelo");
            Parallel.ForEach(_Groups.Keys, (key) =>
                {
                    Point newCenter;
                    List<Point> groupCoordinators = _Groups[key];
                    Bitmap im = new Bitmap(this._ImageFile);

                    newCenter = Utils.CalculateNewCenter(groupCoordinators, im);
                    _Result.Add(newCenter);

                    lock (_Log)
                    {
                        _Log.WriteLog(string.Format("\t\tX: {0}\tY:{1}", newCenter.X, newCenter.Y));
                    }
                });

            _CentersFounded = _CheckNewCenters(_Result);
            _AllResults.Add(_Result);
        }

        private void _FindNewCenters()
        {
            _Result = new List<Point>();
            Point newCenter;
            List<Point> groupCoordinators;

            _Log.WriteLog("\tEncontrando os novos centros");
            foreach (Point key in _Groups.Keys)
            {
                groupCoordinators = _Groups[key];
                newCenter = Utils.CalculateNewCenter(groupCoordinators, _MyImage);
                _Result.Add(newCenter);
                _Log.WriteLog(string.Format("\t\tX: {0}\tY:{1}", newCenter.X, newCenter.Y));
            }

            _CentersFounded = _CheckNewCenters(_Result);
            _AllResults.Add(_Result);
        }

        private bool _CheckNewCenters(List<Point> newCenters)
        {
            bool found = true;

            foreach (List<Point> res in _AllResults)
            {
                found = true;
                res.ForEach(coord => found &= newCenters.Exists(c => c == coord));
                if (found)
                    break;
            }

            return found;
        }

        private void _SeparateInGroups(bool log = true)
        {
            double distance;
            double closestDistante;
            Point coord;
            Point closeCenter = Common.General.Utils.EmptyPoint;
            Color pixel;
            Color centerPixel;
            _Groups.Clear();

            foreach (Point item in _Result)
                _Groups.Add(item, new List<Point>());

            _Distances.Clear();

            if(log)
                _Log.WriteLog(string.Format("\tSeparando em grupos de {0}", _K));

            for (int i = 0; i < this._MyImage.Width; i++)
                for (int j = 0; j < this._MyImage.Height; j++)
                {
                    coord = new Point(i, j);
                    distance = int.MinValue;
                    closestDistante = int.MaxValue;
                    pixel = this._MyImage.GetPixel(coord.X, coord.Y);
                    foreach (Point item in _Result)
                    {
                        centerPixel = this._MyImage.GetPixel(item.X, item.Y);
                        distance = Common.General.Utils.CalculateDistance(pixel, centerPixel);
                        if (distance < closestDistante)
                        {
                            closeCenter = item;
                            closestDistante = distance;
                        }
                    }

                    if (closeCenter == Common.General.Utils.EmptyPoint || !_Groups.ContainsKey(closeCenter))
                        throw new Exception("Não foi possível identificar o centro mais próximo");

                    _Groups[closeCenter].Add(coord);
                    _Distances.Add(coord, closestDistante);
                }

            if(log)
                foreach (Point key in _Groups.Keys)
                    _Log.WriteLog(string.Format("\t\tGrupo X:{0}\tY:{1}\tTotal de elementos {2}", key.X, key.Y, _Groups[key].Count));
        }

        private void _ChooseInitialCenters()
        {
            _Log.WriteLog(string.Format("Inferindo o(s) centro com k={0}", _K));
            throw new NotImplementedException();
        }
    }
}
