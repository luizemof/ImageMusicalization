using Common;
using Common.Extraction;
using Common.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraction
{
    /// <summary>
    /// Classe abstrata que representa o Modelo que será executado para a extração de característica da imagem
    /// </summary>
    public abstract class Model
    {
        public Model(string imageFile, Log log, bool parallel)
        {
            this._ImageFile = imageFile;
            this._Log = log;
            this._MyImage = new Bitmap(imageFile);
            this._ExecuteParallel = parallel;
        }

        public abstract EModelType Type { get; }
        public abstract List<IResult> Result { get; }

        protected string _ImageFile;
        protected Log _Log;
        protected Bitmap _MyImage;
        protected bool _ExecuteParallel;

        public string ImageFile { get { return _ImageFile; } }
        public Log Log { get { return _Log; } }
        public Bitmap MyImage { get { return _MyImage; } }
        public bool ExecuteParallel { get { return _ExecuteParallel; } }

        public abstract void Execute();

        /// <summary>
        /// Salva a o resultado da imagem pintando os pontos encontrados
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="points"></param>
        public void SaveImageResult(string fileName, List<Point> points)
        {
            Bitmap newImage = _MyImage.Clone() as Bitmap;
            points.ForEach(r => _Draw(newImage, r));

            if (File.Exists(fileName))
                File.Delete(fileName);

            newImage.Save(fileName);
        }

        /// <summary>
        /// Desenha a imagem com os pontos encontrados na execução dos modelos
        /// </summary>
        /// <param name="newImage">Nova imagem que terá uma dada região pintada</param>
        /// <param name="r">ponto a ser pintado</param>
        private void _Draw(Bitmap newImage, Point r)
        {
            int x, y;
            for (int i = -6; i < 6; i++)
                for (int j = -6; j < 6; j++)
                {
                    x = r.X + i;
                    y = r.Y + j;
                    if (x >= 0 && x < newImage.Width && y >= 0 && y < newImage.Height)
                        newImage.SetPixel(x, y, Color.White);
                }
        }
    }
}
