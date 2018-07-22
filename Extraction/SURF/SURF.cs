using Common;
using Common.Extraction;
using Common.General;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraction.SURF
{
    /// <summary>
    /// Classe para a execução do Modelo SURF
    /// </summary>
    public class SURF : Model
    {
        public SURF(string imageFile, Log log, bool analize=false)
           :base(imageFile, log, false)
       {
           _Result = new List<SURFResult>();
           _AnalizeImage = analize;
       }

       private List<SURFResult> _Result;
       private bool _AnalizeImage;

       public override List<IResult> Result
       {
           get 
           {
               return new List<IResult>(_Result); 
           }
       }

       public override EModelType Type
       {
           get { return EModelType.SURF; }
       }

       public override void Execute()
       {
           List<Point> keyPoints;
           List<SURFResult> allResults = new List<SURFResult>();
           Dictionary<ENote, List<SURFResult>> dicModel = new Dictionary<ENote, List<SURFResult>>();
           
           this._Log.WriteLog("Iniciando a execução do SURF");
           this._Log.WriteLog("Encontrando pontos", 1);

           keyPoints = _FindKeyPoints();

           if (_AnalizeImage)
           {
               string directory = Directory.Exists(Environment.CurrentDirectory + "\\logs") ? Environment.CurrentDirectory + "\\logs" : Environment.CurrentDirectory;
               string newImageFile = directory + @"\" + "Analize_Files.jpg";
               this.SaveImageResult(newImageFile, keyPoints);
           }
           this._Log.WriteLog(string.Format("Total de pontos encontrados: {0}", keyPoints.Count), 1);

           this._Log.WriteLog("Gerando notas", 1);
           keyPoints.ForEach(p =>
               {
                   SURFResult res = new SURFResult();
                   res.Coordinator = p;
                   res.Pixel = this.MyImage.GetPixel(p.X, p.Y);
                   res.GenerateNote();

                   if (!dicModel.ContainsKey(res.Note))
                       dicModel.Add(res.Note, new List<SURFResult>());

                   dicModel[res.Note].Add(res);
                   allResults.Add(res);
               });

           _Result = new List<SURFResult>();
           this._Log.WriteLog("Notas geradas", 1);
           foreach (ENote key in dicModel.Keys)
           {
               this._Log.WriteLog(string.Format("{0}: {1}", key.GetDescription(), dicModel[key].Count), 2);
               _Result.Add(dicModel[key][0]);
           }

           _Result.ForEach(msr => msr.NumberOfElements = dicModel[msr.Note].Count);

           _LogResult();

           this._Log.WriteLog("Finalizando Execução");
       }
       
       private void _LogResult()
       {
           this.Result.ForEach(r =>
           {
               SURFResult ksr = r as SURFResult;
               _Log.WriteLog("--------------------------------------------");
               _Log.WriteLog(string.Format("Coordenada X:{0}", ksr.Coordinator.X));
               _Log.WriteLog(string.Format("Coordenada Y:{0}", ksr.Coordinator.Y));
               _Log.WriteLog(string.Format("Nota: {0}", r.Note));
               _Log.WriteLog(string.Format("Número de elementos: {0}", ksr.NumberOfElements));
               _Log.WriteLog(string.Format("R: {0}\tG: {1}\tB: {2}", r.Pixel.R, r.Pixel.G, r.Pixel.B));
               _Log.WriteLog(string.Format("Sound: {0}", r.SoundNote));
           });
       }

       private List<Point> _FindKeyPoints()
       {
           Mat modelImage = new Mat(this.ImageFile, LoadImageType.AnyColor);
           double hessianThresh = 300;
           MKeyPoint[] modelKeyPoints;
           List<Point> keys = new List<Point>();

           using (UMat uModelImage = modelImage.ToUMat(AccessType.Read))
           {
               Emgu.CV.XFeatures2D.SURF surfCPU = new Emgu.CV.XFeatures2D.SURF(hessianThresh);
               //extract features from the object image
               modelKeyPoints = surfCPU.Detect(modelImage);
           }

           for (int i = 0; i < modelKeyPoints.Length; i++)
               keys.Add(Point.Round(modelKeyPoints[i].Point));

           return keys;
       }
    }
}
