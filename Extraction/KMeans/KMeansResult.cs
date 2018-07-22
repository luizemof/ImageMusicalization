using Common;
using Common.Extraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraction.KMeans
{
    /// <summary>
    /// Classe que representa o resultado da execução do KMeans
    /// </summary>
    public class KMeansResult : IResult
    {
        public KMeansResult()
        {
            this._Note = ENote.Unknow;
        }

        private ENote _Note;

        public Color Pixel { get; set; }
        public double NumberOfElements { get; set; }
        public EModelType ModelType { get { return EModelType.KMeans; } }
        public Point Coordinator { get; set; }
        /// <summary>
        /// Nota gerada
        /// </summary>
        public ENote Note
        {
            get
            {
                if (_Note == ENote.Unknow)
                    GenerateNote();

                return _Note;
            }
            set { _Note = value; }
        }
        /// <summary>
        /// Caminho físico do som
        /// </summary>
        public string SoundNote { get { return _GetSoundFile(); } }
        /// <summary>
        /// Método para gerar a nota
        /// </summary>
        public void GenerateNote()
        {
            if (Pixel == null || Pixel == Color.Empty)
                throw new Exception("Não foi possível gerar a nota");

            _Note = Common.General.Utils.GetColorNote(Pixel);
        }

        private string _GetSoundFile()
        {
            string file;

            if (_Note == ENote.Unknow)
                GenerateNote();

            switch (_Note)
            {
                case ENote.C:
                    file = Resource.C;
                    break;
                case ENote.Dm:
                    file = Resource.Dm;
                    break;
                case ENote.Em:
                    file = Resource.Em;
                    break;
                case ENote.F:
                    file = Resource.F;
                    break;
                case ENote.G:
                    file = Resource.G;
                    break;
                case ENote.Am:
                    file = Resource.Am;
                    break;
                case ENote.Si:
                    file = Resource.GB;
                    break;
                default:
                    file = Resource.C;
                    break;
            }

            return string.Concat(Environment.CurrentDirectory, @"\", file);
        }
    }
}
